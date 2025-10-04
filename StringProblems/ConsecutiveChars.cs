namespace StringProblems;
public static class ConsecutiveChars
{ // Q: find the maximum consecutive repeating in given string
	public static char? Base(string input)
	{
		if (input.Length == 0)
		{
			return null;
		}
		else if (input.Length == 1)
		{
			return input[0];
		}

		//get the first 
		var result = input[0];
		int count = 0;
		int curCount = 1;

		for (int i = 0; i < input.Length; i++)
		{
			//same as the next
			if ((i + 1) < input.Length && input[i + 1] == input[i])
			{
				curCount++;
			}
			else
			{
				//crown new king
				if (curCount > count)
				{
					count = curCount;
					result = input[i];
				}
				curCount = 1;
			}
		}
		return result;
	}
}