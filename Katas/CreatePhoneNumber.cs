namespace Katas;

public class CreatePhoneNumber
{
	/*
	Write a function that accepts an array of 10 integers (between 0 and 9), that returns a string of those numbers in the form of a phone number.

	Example
	Kata.CreatePhoneNumber(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}) // => returns "(123) 456-7890"
	The returned format must be correct in order to complete this challenge.
	Don't forget the space after the closing parentheses!
	*/
	public string Solve(int[] numbers)
	{
		string result = string.Empty;
		for (int i = 0; i < numbers.Length; i++)
		{
			if (numbers[i] > 9 || numbers[i] < 0) return "not accepted number array, should be between 0 and 9";
			if (0 == i)
			{
				result = $"({numbers[i]}";
			}
			else if (i == 2)
			{
				result += $"{numbers[i]}) ";
			}
			else if (i == 5)
			{
				result += $"{numbers[i]}-";
			}
			else
			{
				result += numbers[i];
			}
		}
		return result;

	}
}
