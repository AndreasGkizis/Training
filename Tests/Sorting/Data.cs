using System.Collections;

namespace Tests;
public class Data : IEnumerable<object[]>
{
	public IEnumerator<object[]> GetEnumerator()
	{
		yield return new object[] { new int[] { 5, 3, 8, 4, 2 }, new int[] { 2, 3, 4, 5, 8 }, true }; // Ascending order
		yield return new object[] { new int[] { 10, 1, 5, 2, 7 }, new int[] { 1, 2, 5, 7, 10 }, true }; // Ascending order
		yield return new object[] { new string[] { "banana", "apple", "pear" }, new string[] { "apple", "banana", "pear" }, true }; // Ascending order
		yield return new object[] { new int[] { 1, 2, 3, 4, 5 }, new int[] { 1, 2, 3, 4, 5 }, true }; // Already sorted (Ascending)
		yield return new object[] { new int[] { 3 }, new int[] { 3 }, true }; // Single element (Ascending)
		yield return new object[] { new int[] { }, new int[] { }, true }; // Empty array (Ascending)

		// Descending order test cases
		yield return new object[] { new int[] { 5, 3, 8, 4, 2 }, new int[] { 8, 5, 4, 3, 2 }, false }; // Descending order
		yield return new object[] { new int[] { 10, 1, 5, 2, 7 }, new int[] { 10, 7, 5, 2, 1 }, false }; // Descending order
		yield return new object[] { new string[] { "banana", "apple", "pear" }, new string[] { "pear", "banana", "apple" }, false }; // Descending order
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
