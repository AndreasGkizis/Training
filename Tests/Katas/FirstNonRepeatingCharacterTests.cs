using Katas;

// ReSharper disable StringLiteralTypo

namespace Tests.Katas;

public class FirstNonRepeatingCharacterTests
{
    [Theory]
    [InlineData("a", "a")] // 1. Single character
    [InlineData("stress", "t")] // 2. Standard lowercase
    [InlineData("sTreSS", "T")] // 3. Case preservation
    [InlineData("Go hang a sNoot, M'sNooter", "h")] // 6. Complex sentence
    [InlineData("moonmen", "e")] // 7. Non-repeating in the middle
    
    [InlineData("", "")] // 5. Empty string
    [InlineData("aabbccddeeff", "")] // 4. All repeating
    [InlineData("++--", "")] // 10. Non-alphabetic repeating
    public void FirstNonRepeatingCharacter_ExampleCases(string input, string expectedResult)
    {
        var uut = new FirstNonRepeatingCharacter();
        var result = uut.Solve(input);

        result.Should().Be(expectedResult);
    }
}