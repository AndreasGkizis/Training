using AsyncExamples;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Benchmarks;
[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
[IterationCount(10)]
public class ConfigureAwaitBenchmarks
{

        [Benchmark]
        public async Task ConfigureAwaitFalseBenchmark() => await ConfigureAwait.ConfigureAwaitFalse();

        [Benchmark]
        public async Task SimpleAwaitBenchmark() => await ConfigureAwait.SimpleAwait();

        [Benchmark]
        public async Task ConfigureAwaitTrueBenchmark() => await ConfigureAwait.ConfigureAwaitTrue();
}