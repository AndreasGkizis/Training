using Katas;

// ReSharper disable StringLiteralTypo

namespace Tests.Katas;

public class StripCommentsTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void StripComments_ExampleCases(string input,string expectedResult,string[] commentSymbols )
    {
        var uut = new StripComments();
        var result = uut.Solve(input, commentSymbols);

        result.Should().Be(expectedResult);
    }

    public static readonly TheoryData<string, string, string[]> Data = new()
    {
        // AddRow is implicit when using the collection initializer syntax
        { @"apples, pears # and bananas
grapes
bananas !apples", @"apples, pears 
grapes
bananas ", ["#", "!"] },

        // Add a second case for demonstration
        { @"a
 b # c
d", @"a
 b 
d", ["#"] },

        // Add a case with only one comment marker
        { "hello world # comment", "hello world ", ["#",] }
    };
}