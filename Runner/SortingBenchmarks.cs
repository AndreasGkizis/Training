using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Sorting;

namespace Benchmarks;
[MemoryDiagnoser]
[ThreadingDiagnoser]
public class SortingBenchmarks
{
	[Params(10, 50, 100)]
	public int ArraySize;
	private readonly Consumer _consumer = new();

	private int[] _data;

	[GlobalSetup]
	public void Setup()
	{
		var random = new Random(123);
		_data = new int[ArraySize];

		for (int i = 0; i < ArraySize; i++) _data[i] = random.Next();
	}

	[Benchmark]
	public void LinQ() => _data.Order().Consume(_consumer);

	[Benchmark]
	public void Quick() => QuickSort<int>.Sort(_data, 0, _data.Length - 1);

	[Benchmark]
	public void Bubble() => BubbleSort<int>.Sort(_data, isAscending: true);

	[Benchmark]
	public void GenMerge() => GenMergeSort<int>.Sort(_data);

	[Benchmark]
	public void Merge() => MergeSort.Sort(_data);
}