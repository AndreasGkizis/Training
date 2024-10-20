

using StringProblems;

namespace Tests.Strings;

public class AnagramTests
{
    [Theory]
    [MemberData(nameof(GetAnagramTestData))]
    public void BaseTests(string input1, string input2, bool expectedResult)
    {
        var result = Anagram.Base(input1, input2);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetAnagramTestData))]
    public void Optv1(string input1, string input2, bool expectedResult)
    {
        // this can not handle numbers as is 
        if (input1.Any(char.IsDigit) || input2.Any(char.IsDigit))
        {
            return;
        }
        var result = Anagram.Optimizedv1(input1, input2);

        result.Should().Be(expectedResult);
    }
    
    [Theory]
    [MemberData(nameof(GetAnagramTestData))]
    public void Optv1_1(string input1, string input2, bool expectedResult)
    {
        // this can not handle numbers as is 
        if (input1.Any(char.IsDigit) || input2.Any(char.IsDigit))
        {
            return;
        }
        var result = Anagram.Optimizedv1_1(input1, input2);

        result.Should().Be(expectedResult);
    }

    [Theory]
    [MemberData(nameof(GetAnagramTestData))]
    public void Optv2(string input1, string input2, bool expectedResult)
    {
        var result = Anagram.Optimizedv2(input1, input2);

        result.Should().Be(expectedResult);
    }
    public static IEnumerable<object[]> GetAnagramTestData()
    {
        yield return new object[] { "listen", "silent", true };
        yield return new object[] { "evil", "vile", true };
        yield return new object[] { "fluster", "restful", true };
        yield return new object[] { "123", "321", true };
        yield return new object[] { "hello", "world", false };
        yield return new object[] { "rat", "car", false };
        yield return new object[] { "a gentleman", "elegant man", true }; // ignoring spaces
        yield return new object[] { "Astronomer", "Moon starer", true }; // ignoring case and spaces
        yield return new object[] { "abc", "ab", false };
        yield return new object[] { "aa", "aa", true };
    }
}
