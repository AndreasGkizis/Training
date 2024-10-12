using StringProblems;

namespace Tests;
public class ConsecutiveCharsTests
{
	[Theory]
	[InlineData("aaabb", 'a')]         // Lowercase characters
	[InlineData("AAAbb", 'A')]         // Uppercase characters
	[InlineData("aabbcc", 'a')]        // Multiple groups, first max count
	[InlineData("abcd", 'a')]          // No repetitions, return first
	[InlineData("A", 'A')]             // Single uppercase character
	[InlineData("", ' ')]              // Empty string
	[InlineData("AAaAAA", 'A')]        // Mixed case, separate groupings
	[InlineData("aaabbbccc", 'a')]     // Multiple groups with same max length
	[InlineData("xxxyyyZZZwwww", 'w')] // Longest sequence at the end
	[InlineData("aAaAaA", 'a')]        // Alternating case, treat as different chars
	[InlineData("bbbBBbaaa", 'b')]     // Lowercase and uppercase treated differently
	public void Base_ShouldReturnExpectedResult(string input, char expected)
	{
		// Act
		var result = ConsecutiveChars.Base(input);

		// Assert
		Assert.Equal(expected, result);
	}
}
