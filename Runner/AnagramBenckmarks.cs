using BenchmarkDotNet.Attributes;
using StringProblems;
namespace Benchmarks;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class AnagramBenckmarks
{
	[Params(100, 1000, 10000)] // Specify lengths here
	public int StringLength { get; set; }
	private string _testString1 = default!;
	private string _testString2 = default!;
	[GlobalSetup]
	public void Setup()
	{
		_testString1 = new string('a', StringLength) + "b" + new string('a', StringLength);
		_testString2 = new string('a', StringLength) + "b" + new string('a', StringLength);
	}

	[Benchmark(Baseline = true)]
	public void Base()
	{
		Anagram.Base(_testString1, _testString2);
	}

	[Benchmark]
	public void opti1()
	{
		Anagram.Optimizedv1(_testString1, _testString2);
	}

	[Benchmark]
	public void opti2()
	{
		Anagram.Optimizedv2(_testString1, _testString2);
	}
}
