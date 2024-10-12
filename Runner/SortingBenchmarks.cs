using BenchmarkDotNet.Attributes;
using Sorting;

namespace Benchmarks;
[MemoryDiagnoser]
[ThreadingDiagnoser]
public class SortingBenchmarks
{
	//[Params(10, 100, 1_000)]
	//public int ArraySize;

	private int[] _data;
	private int[] _data1;
	private int[] _data2;
	private int[] _data3;

	[GlobalSetup]
	public void Setup()
	{

		var random = new Random(123);
		_data = new int[100];
		_data1 = new int[1_000];
		_data2 = new int[100_000];
		_data3 = new int[1_000_000];

		for (int i = 0; i < 100; i++) _data[i] = random.Next();
		for (int i = 0; i < 1_000; i++) _data1[i] = random.Next();
		for (int i = 0; i < 100_000; i++) _data2[i] = random.Next();
		for (int i = 0; i < 1_000_000; i++) _data3[i] = random.Next();

	}
	[Benchmark]
	public void test_100() => _data.Order();
	[Benchmark]
	public void test_1_000() => _data1.Order(); 
	[Benchmark]
	public void test_100_000() => _data2.Order(); 
	[Benchmark]
	public void test_1_000_000() => _data3.Order();

	//	[Benchmark]
	//	public void Quick() => QuickSort<int>.SortArray(_data, 0, _data.Length - 1);

	//	[Benchmark]
	//	public void Linq() => _data.Order();
}

