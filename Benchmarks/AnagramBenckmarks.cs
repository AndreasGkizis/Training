using BenchmarkDotNet.Attributes;
using StringProblems;
namespace Benchmarks;

[MemoryDiagnoser]
[ThreadingDiagnoser]
public class AnagramBenckmarks
{
	[Params(10_000, 100_000, 1_000_000)] // Specify lengths here
	public int StringLength { get; set; }

	private string _testString1 = default!;
	private string _testString2 = default!;

	[GlobalSetup]
	public void Setup()
	{
		_testString1 = GenerateRandomString(StringLength);
		_testString2 = GenerateRandomString(StringLength);
	}

	private string GenerateRandomString(int length)
	{
		const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		var random = new Random();
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	[Benchmark(Baseline = true)]
	public void Base()
	{
		Anagram.Base(_testString1, _testString2);
	}

	[Benchmark]
	public void Option1()
	{
		Anagram.OptimizedV1(_testString1, _testString2);
	}

	[Benchmark]
	public void Option1_1()
	{
		Anagram.Optimizedv1_1(_testString1, _testString2);
	}
	
	[Benchmark]
	public void Option2()
	{
		Anagram.Optimizedv2(_testString1, _testString2);
	}
}
