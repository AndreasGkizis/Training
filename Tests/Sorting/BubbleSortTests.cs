using Sorting;

namespace Tests.Sorting;
public class BubbleSortTests
{
	[Theory]
	[ClassData(typeof(Data))]
	public void Sort_ShouldSortArrayCorrectly<T>(T[] inputArray, T[] expectedArray, bool isAscending) where T : IComparable
	{
		// Act
		var sortedArray = BubbleSort<T>.Sort(inputArray,isAscending);

		// Assert
		Assert.Equal(expectedArray, sortedArray);
	}
}