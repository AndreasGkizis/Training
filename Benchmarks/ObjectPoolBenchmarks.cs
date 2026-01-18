using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using Microsoft.Extensions.ObjectPool;

namespace Benchmarks;


// "Baseline = true" allows you to see how much faster/slower new versions are compared to .NET 6.
[SimpleJob(RuntimeMoniker.Net60, baseline: true)] 
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ObjectPoolBenchmarks
{
    // --- POOLS FOR LIGHT OBJECTS ---
    private DefaultObjectPool<LightObject> _lightDefaultPool;
    private DefaultObjectPool<LightObject> _lightPolicyPool;
    private DefaultObjectPool<LightObjectResettable> _lightResettablePool;

    // --- POOLS FOR HEAVY OBJECTS ---
    private DefaultObjectPool<HeavyObject> _heavyDefaultPool;
    private DefaultObjectPool<HeavyObject> _heavyPolicyPool;
    private DefaultObjectPool<HeavyObjectResettable> _heavyResettablePool;


    private DefaultObjectPool<HeavyObject> _heavyPool;
    private DefaultObjectPool<LightObject> _lightPool;

    [GlobalSetup]
    public void Setup()
    {
        var provider = new DefaultObjectPoolProvider();

        // Setup Light Pools
        _lightDefaultPool =
            (DefaultObjectPool<LightObject>)provider.Create(new DefaultPooledObjectPolicy<LightObject>());
        _lightPolicyPool = (DefaultObjectPool<LightObject>)provider.Create(new LightObjectPolicy());
        _lightResettablePool =
            (DefaultObjectPool<LightObjectResettable>)provider.Create(
                new DefaultPooledObjectPolicy<LightObjectResettable>());

        // Setup Heavy Pools
        _heavyDefaultPool =
            (DefaultObjectPool<HeavyObject>)provider.Create(new DefaultPooledObjectPolicy<HeavyObject>());
        _heavyPolicyPool = (DefaultObjectPool<HeavyObject>)provider.Create(new HeavyObjectPolicy());
        _heavyResettablePool =
            (DefaultObjectPool<HeavyObjectResettable>)provider.Create(
                new DefaultPooledObjectPolicy<HeavyObjectResettable>());
    }


    // ==========================================
    // BENCHMARKS: LIGHT OBJECTS
    // ==========================================

    [Benchmark(Description = "Light: New()")]
    public void Light_Allocate_New()
    {
        var obj = new LightObject();
        obj.Id = 123;
        obj.Tag = "data";
    }

    [Benchmark(Description = "Light: Pool (Unsafe)")]
    public void Light_Pool_Unsafe()
    {
        var obj = _lightDefaultPool.Get();
        obj.Id = 123;
        obj.Tag = "data";
        _lightDefaultPool.Return(obj);
    }

    [Benchmark(Description = "Light: Pool (Policy Reset)")]
    public void Light_Pool_Policy()
    {
        var obj = _lightPolicyPool.Get();
        obj.Id = 123;
        obj.Tag = "data";
        _lightPolicyPool.Return(obj);
    }

    [Benchmark(Description = "Light: Pool (IResettable)")]
    public void Light_Pool_Resettable()
    {
        var obj = _lightResettablePool.Get();
        obj.Id = 123;
        obj.Tag = "data";
        _lightResettablePool.Return(obj);
    }

    // ==========================================
    // BENCHMARKS: HEAVY OBJECTS
    // ==========================================

    [Benchmark(Description = "Heavy: New()")]
    public void Heavy_Allocate_New()
    {
        var obj = new HeavyObject();
        obj.UserId = 123;
        obj.IsAdmin = true;
    }

    [Benchmark(Description = "Heavy: Pool (Unsafe)")]
    public void Heavy_Pool_Unsafe()
    {
        var obj = _heavyDefaultPool.Get();
        obj.UserId = 123;
        obj.IsAdmin = true;
        _heavyDefaultPool.Return(obj);
    }

    [Benchmark(Description = "Heavy: Pool (Policy Reset)")]
    public void Heavy_Pool_Policy()
    {
        var obj = _heavyPolicyPool.Get();
        obj.UserId = 123;
        obj.IsAdmin = true;
        _heavyPolicyPool.Return(obj);
    }

    [Benchmark(Description = "Heavy: Pool (IResettable)")]
    public void Heavy_Pool_Resettable()
    {
        var obj = _heavyResettablePool.Get();
        obj.UserId = 123;
        obj.IsAdmin = true;
        _heavyResettablePool.Return(obj);
    }
}

// 2. HEAVY OBJECT (Expensive allocation due to arrays)
public class HeavyObject
{
    // The "Payload" makes this expensive to create new instances of
    public byte[] Buffer { get; set; } = new byte[4096];
    public int[] Data { get; set; } = new int[1000];

    // The "State" that needs resetting
    public int UserId { get; set; }
    public bool IsAdmin { get; set; }
}

public class HeavyObjectResettable : HeavyObject, IResettable
{
    public bool TryReset()
    {
        // We reset the logical state, but we KEEP the heavy buffer (that's the point of pooling)
        UserId = 0;
        IsAdmin = false;
        Array.Clear(Buffer, 0, Buffer.Length);
        Array.Clear(Data, 0, Data.Length);
        return true;
    }
}

public class HeavyObjectPolicy : IPooledObjectPolicy<HeavyObject>
{
    public HeavyObject Create() => new HeavyObject();

    public bool Return(HeavyObject obj)
    {
        Array.Clear(obj.Buffer, 0, obj.Buffer.Length);
        Array.Clear(obj.Data, 0, obj.Data.Length);
        obj.UserId = 0;
        obj.IsAdmin = false;
        return true;
    }
}

// 1. LIGHT OBJECT (Small, cheap to allocate)
public class LightObject
{
    public int Id { get; set; }
    public string Tag { get; set; }
}

public class LightObjectResettable : LightObject, IResettable
{
    public bool TryReset()
    {
        Id = 0;
        Tag = string.Empty;
        return true;
    }
}

public class LightObjectPolicy : IPooledObjectPolicy<LightObject>
{
    public LightObject Create() => new LightObject();

    public bool Return(LightObject obj)
    {
        obj.Id = 0;
        obj.Tag = string.Empty;
        return true;
    }
}