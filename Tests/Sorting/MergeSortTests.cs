using Sorting;

namespace Tests.Sorting;
public class MergeSortTests
{

	[Theory]
	[MemberData(nameof(SortAscendingTestData))]
	public void Sort_ShouldSortArrayCorrectly(int[] inputArray, int[] expectedArray) //where T : IComparable
	{
		// Act
		var sortedArray = MergeSort.Sort(inputArray);

		// Assert
		Assert.Equal(expectedArray, sortedArray);
	}

	[Theory]
	[MemberData(nameof(SortAscendingTestData))]
	public void GenSort_ShouldSortArrayCorrectly<T>(T[] inputArray, T[] expectedArray) where T : IComparable
	{
		// Act
		var sortedArray = GenMergeSort<T>.Sort(inputArray);

		// Assert
		Assert.Equal(expectedArray, sortedArray);
	}

	public static IEnumerable<object[]> SortAscendingTestData()
	{
		yield return new object[] { new int[] { 5, 3, 8, 6, 2, 7, 4, 1 }, new int[] { 1, 2, 3, 4, 5, 6, 7, 8 } };
		yield return new object[] { new int[] { 1 }, new int[] { 1 } };
		yield return new object[] { new int[] { 2, 1 }, new int[] { 1, 2 } };
		yield return new object[] { new int[] { }, new int[] { } };
	}
}
