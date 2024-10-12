namespace StringProblems;
public static class ConsecutiveChars
{
	public static char Base(string input)
	{ // Q: find the maximum  consecutive repeating in given string
		if (input.Length == 0)
		{
			return ' ';
		}
		else if (input.Length == 1)
		{

			return input[0];
		}

		//get the first 
		var result = input[0];
		int count = 0;
		int cur_count = 1;

		for (int i = 0; i < input.Length; i++)
		{
			//same as the next
			if ((i + 1) < input.Length && input[i + 1] == input[i])
			{
				cur_count++;
			}
			else
			{
				//crown new king
				if (cur_count > count)
				{
					count = cur_count;
					result = input[i];
				}
				cur_count = 1;
			}
		}
		return result;
	}
}