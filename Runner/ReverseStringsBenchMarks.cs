using BenchmarkDotNet.Attributes;

namespace Benchmarks;
public class ReverseStringsBenchMarks
{
	[Params(10_000, 100_000, 1_000_000)] // Specify lengths here
	public int StringLength { get; set; }

	private string _testString = default!;

	[GlobalSetup]
	public void Setup()
	{
		_testString= GenerateRandomString(StringLength);
	}

	private string GenerateRandomString(int length)
	{
		const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		var random = new Random();
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}

	[Benchmark]
	public string CharArrayAndReverse()
	{
		var charArray = _testString.ToCharArray();
		Array.Reverse(charArray);
		return new string(charArray);
	}

	[Benchmark]
	public string LINQReverse()
	{
		return new string(_testString.ToCharArray().Reverse().ToArray());
	}
}
