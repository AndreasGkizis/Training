using StringProblems;

namespace Tests;


public class MostUsedCharsTests
{
	[Theory]
	[InlineData("aaaabbcc", "aaaa")] // No consecutive chars
	[InlineData("aaaa", "aaaa")]     // All characters the same
	[InlineData("aaAaaaAbbbaac", "aaaaaaaaa")] // Multiple consecutive groups
	[InlineData("xyzzz", "zzz")]       // Unique characters
	[InlineData("", "")]             // Empty string
	[InlineData("AabbCC", "aabbcc")]  // Mixed case input
	public void Base_ShouldReturnExpectedResult(string input, string expected)
	{
		// Act
		var result = MostUsedChars.Base(input);

		// Assert
		Assert.Equal(expected, result);
	}
}
