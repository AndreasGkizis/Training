using BenchmarkDotNet.Running;
using Benchmarks;

namespace Bench;

public class Program
{
	public static void Main()
	{
		var sum = BenchmarkRunner.Run<SortingBenchmarks>();
	}
}
