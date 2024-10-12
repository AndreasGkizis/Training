using System.Collections;

namespace StringProblems
{
	public static class Anagram
	{
		// check if 2 strings are an anagram of each other 
		public static bool Base(string input1, string input2)
		{
			input1 = input1.ToLower().Replace(" ", null);
			input2 = input2.ToLower().Replace(" ", null);
			var charArray1 = input1.ToCharArray();
			var charArray2 = input2.ToCharArray();

			var sorted1 = charArray1.Order();
			var sorted2 = charArray2.Order();
			if (sorted1.SequenceEqual(sorted2)) return true;

			return false;
		}

		// only for non number strings
		public static bool Optimizedv1(string input1, string input2)
		{
			input1 = input1.Replace(" ", null);
			input2 = input2.Replace(" ", null);

			if (input1.Length != input2.Length) return false;

			input1 = input1.ToLower();
			input2 = input2.ToLower();

			//create array for the count of chars
			var input1Array = new int[26];
			var input2Array = new int[26];

			foreach (var character in input1)
			{
				var indexOfChar = (byte)character - (byte)'a';
				input1Array[indexOfChar]++;
			}

			foreach (var character in input2)
			{
				var indexOfChar = (byte)character - (byte)'a';
				input2Array[indexOfChar]++;
			}

			return input1Array.SequenceEqual(input2Array);
		}

		public static bool Optimizedv2(string input1, string input2)
		{
			input1 = input1.Replace(" ", null);
			input2 = input2.Replace(" ", null);
			if (input1.Length != input2.Length) return false;

			input1 = input1.ToLower();
			input2 = input2.ToLower();

			//create array for the count of chars
			var dict1 = new Dictionary<char, int>();
			var dict2 = new Dictionary<char, int>();

			foreach (var character in input1)
			{
				if (dict1.ContainsKey(character))
				{
					dict1[character]++;
				}
				else
				{
					dict1.Add(character, 1);
				}
			}

			foreach (var character in input2)
			{
				if (dict2.ContainsKey(character))
				{
					dict2[character]++;
				}
				else
				{
					dict2.Add(character, 1);
				}
			}

			return AreDictionariesEqual(dict1, dict2);
		}

		static bool AreDictionariesEqual(Dictionary<char, int> dict1, Dictionary<char, int> dict2)
		{
			if (dict1.Count != dict2.Count)
			{
				return false; // Different counts, not equal
			}

			foreach (var kvp in dict1)
			{
				// Check if dict2 contains the same key and value
				if (!dict2.ContainsKey(kvp.Key))
				{
					return false;

				}
				else
				{
					if (dict2[kvp.Key] != kvp.Value)
					{
						return false;
					};
				}
			}

			return true; // All keys and values match
		}
	}
}
