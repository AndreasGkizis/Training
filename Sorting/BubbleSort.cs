namespace Sorting;

public static class BubbleSort<T> where T : IComparable
{
	public static T[] Sort(T[] inputArray, bool isAscending)
	{
		if (inputArray.Length <= 1) return inputArray; // no reason to sort
		int maxSwap = inputArray.Length - 1; // due to zero indexed arrays

		for (int i = 0; i < maxSwap; i++)
		{
			for (int j = 0; j < maxSwap - i; j++)
			{
				var currIdx = j;
				var nextIdx = j + 1;

				var compare = inputArray[currIdx].CompareTo(inputArray[nextIdx]);
				var isLargerThan = compare > 0;
				var isSmallerThan = compare < 0;

				// larger at the end => ascending || smaller at the end => descending 
				if (isAscending && isLargerThan || !isAscending && isSmallerThan)
				{
					Swap(inputArray, currIdx, nextIdx);
				}
			}
		}

		return inputArray;
	}

	private static void Swap(T[] inputArray, int currIdx, int nextIdx)
	{
		var temp = inputArray[currIdx];
		inputArray[currIdx] = inputArray[nextIdx];
		inputArray[nextIdx] = temp;
	}
}
