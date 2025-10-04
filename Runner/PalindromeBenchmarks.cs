using BenchmarkDotNet.Attributes;
using StringProblems;

namespace Benchmarks;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class PalindromeBenchmarks
{

	[Params(100, 1000, 10000)] // Specify lengths here
	public int StringLength { get; set; }
	private string _testString = default!;
	[GlobalSetup]
	public void Setup()
	{
		_testString = new string('a', StringLength); // Create a palindrome of 'a' characters
	}

	[Benchmark(Baseline = true)]
	public void Base()
	{
		Palindrome.Base(_testString);
	}

	[Benchmark]
	public void Opt()
	{
		Palindrome.Optimized(_testString);
	}
	[Benchmark]
	public void Optv2()
	{
		Palindrome.Optimizedv2(_testString);
	}

	[Benchmark]
	public void Optv3()
	{
		Palindrome.Optimizedv3(_testString);
	}
}
