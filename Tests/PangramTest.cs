using StringProblems;

namespace Tests;

public class PangramTest
{
    [Theory]
    [InlineData("The quick brown fox jumps over the lazy dog", true)] // Contains all letters A-Z;
    [InlineData("The quick brown fox jumps over the lazy dog 1234!", true)] // Pangram with numbers; 
    [InlineData("Pack my box with five dozen liquor jugs", true)] // Another pangram example; 
    [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", true)] // All letters in order; 
    [InlineData("abcdefghij k lmnop qrstuvwxyz", true)] // All letters with spaces; 
    [InlineData("Hello world", false)] // Missing several letters; 
    [InlineData("Not a pangram!", false)] // Missing letters like "b" and "c"; 
    [InlineData("   ", false)] // only spaces; 
    public void BaseTests(string input, bool expected)
    {
        // Act
        var result = Pangram.Base(input);

        // Assert
        Assert.Equal(expected, result);
    }
}