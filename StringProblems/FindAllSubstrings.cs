namespace StringProblems;
public static class FindAllSubstrings
{
	//Q: find all occurrences of a substring in a string 
	public static List<int> Base(string baseString, string subString)
	{
		List<int> result = new();
		if (baseString == string.Empty || subString == string.Empty) return result;

		int index = baseString.IndexOf(subString);

		while (index != -1)
		{
			result.Add(index);
			index = baseString.IndexOf(subString, index + 1);
		}

		return result;
	}
}
