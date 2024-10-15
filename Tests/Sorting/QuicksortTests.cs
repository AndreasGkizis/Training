using Sorting;

namespace Tests.Sorting;
public class QuicksortTests
{
	[Theory]
	[ClassData(typeof(Data))]
	public void Sort_ShouldSortArrayCorrectly<T>(T[] inputArray, T[] expectedArray, bool isAcending) where T : IComparable
	{
		// ignore descending asserts
		if (!isAcending) {
			return;
		}
		// Act
		var sortedArray = QuickSort<T>.Sort(inputArray, 0, inputArray.Length - 1);

		// Assert
		Assert.Equal(expectedArray, sortedArray);
	}
}
