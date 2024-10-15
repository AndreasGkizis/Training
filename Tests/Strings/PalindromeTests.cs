using StringProblems;

namespace Tests.Strings;

public class PalindromeTests
{
    [Theory]
    [MemberData(nameof(GetPalindromeTestData))]
    public void BaseTests(string input, bool expectedResult)
    {
        var result = Palindrome.Base(input);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetPalindromeTestData))]
    public void OptTests(string input, bool expectedResult)
    {
        var result = Palindrome.Optimized(input);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetPalindromeTestData))]
    public void OptTestsv2(string input, bool expectedResult)
    {
        var result = Palindrome.Optimizedv2(input);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetPalindromeTestData))]
    public void OptTestsv3(string input, bool expectedResult)
    {
        var result = Palindrome.Optimizedv3(input);

        result.Should().Be(expectedResult);
    }

    public static IEnumerable<object[]> GetPalindromeTestData()
    {
        yield return new object[] { "", false };
        yield return new object[] { "anna", true };
        yield return new object[] { "Kanak", true };
        yield return new object[] { "Anna", true };
        yield return new object[] { "racecar", true };
        yield return new object[] { "something", false };
    }
}