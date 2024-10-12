using Sorting;

namespace Tests.Sorting;
public class QuicksortTests
{
	[Theory]
	[ClassData(typeof(Data))]
	public void Sort_ShouldSortArrayCorrectly<T>(T[] inputArray, T[] expectedArray, bool discarded) where T : IComparable
	{
		// Act
		var sortedArray = QuickSort<T>.Sort(inputArray, 0, inputArray.Length - 1);

		// Assert
		Assert.Equal(expectedArray, sortedArray);
	}
}
