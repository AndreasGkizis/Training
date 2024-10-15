using Katas;

namespace Tests.Katas;
public class CreatePhoneNumberTests
{

	[Theory]
	[InlineData(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 }, "(123) 456-7890")]
	[InlineData(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, "(111) 111-1111")]
	public void CreatePhoneNumberExampleCases(int[] ints, string expectedResult)
	{
		var uut = new CreatePhoneNumber();
		var result =uut.Solve(ints);

		result.Should().Be(expectedResult);
	}
}
