using StringProblems;

namespace Tests.Strings;
public class FindAllSubstringsTests
{
	[Theory]
	[InlineData("test test test", "test", new[] { 0, 5, 10 })] // Basic case: multiple non-overlapping occurrences of "test"
	[InlineData("hello world", "test", new int[] { })] // Substring not present in the base string
	[InlineData("", "test", new int[] { })] // Base string is empty
	[InlineData("hello", "", new int[] { })] // Substring is empty
	[InlineData("aaaaa", "aa", new[] { 0, 1, 2, 3 })] // Substring "aa" overlaps within "aaaaa" at indices 0, 1, 2, 3
	[InlineData("abcabcabc", "abc", new[] { 0, 3, 6 })] // Non-overlapping occurrences of "abc"
	[InlineData("abababab", "ab", new[] { 0, 2, 4, 6 })] // Overlapping occurrences of "ab" in "abababab"
	[InlineData("This is a test", "is", new[] { 2, 5 })] // Substring "is" found at multiple positions with spaces
	[InlineData("123456123456", "123", new[] { 0, 6 })] // Numeric characters, "123" found at index 0 and 6
	[InlineData("abcdef", "xyz", new int[] { })] // Substring "xyz" not found in base string
	[InlineData("aaa", "aaaa", new int[] { })] // Substring longer than the base string, no match
	public void Base_ShouldReturnCorrectIndices(string baseString, string subString, int[] expected)
	{
		// Act
		var result = FindAllSubstrings.Base(baseString, subString);

		// Assert
		Assert.Equal(expected, result);
	}
}
