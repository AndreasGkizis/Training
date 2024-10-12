namespace Sorting;
public static class QuickSort<T> where T : IComparable
{
	public static T[] Sort(T[] inputArray, int leftIdx, int rightIdx)
	{
		if (inputArray.Length <= 1) return inputArray;

		var left = 0;
		var right = inputArray.Length - 1;

		var pivot = inputArray[leftIdx];

		while (left < right)
		{
			while (inputArray[left].CompareTo(pivot) < 0) left++;

			while (inputArray[right].CompareTo(pivot) > 0) right--;

			if (left <= right)
			{
				Swap(inputArray, left, right);
				left++;
				right--;
			}

			if (leftIdx < right) Sort(inputArray, leftIdx, right);
			if (left < rightIdx) Sort(inputArray, left, rightIdx);
		}

		return inputArray;
	}


	public static int[] SortArray(int[] array, int leftIndex, int rightIndex)
	{
		var i = leftIndex;
		var j = rightIndex;
		var pivot = array[leftIndex];

		while (i <= j)
		{
			while (array[i] < pivot)
			{
				i++;
			}

			while (array[j] > pivot)
			{
				j--;
			}

			if (i <= j)
			{
				int temp = array[i];
				array[i] = array[j];
				array[j] = temp;
				i++;
				j--;
			}
		}

		if (leftIndex < j)
			SortArray(array, leftIndex, j);

		if (i < rightIndex)
			SortArray(array, i, rightIndex);

		return array;
	}
	private static void Swap(T[] inputArray, int currIdx, int nextIdx)
	{
		var temp = inputArray[currIdx];
		inputArray[currIdx] = inputArray[nextIdx];
		inputArray[nextIdx] = temp;
	}
}
