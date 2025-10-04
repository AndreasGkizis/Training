namespace Sorting;

public static class MergeSort
{
	public static int[] Sort(int[] input)
	{
		int[] leftArray;
		int[] rightArray;
		int[] result = new int[input.Length];

		// to break recursion
		if (input.Length <= 1) return input;

		int midpoint = input.Length / 2;
		leftArray = new int[midpoint];

		if (input.Length % 2 == 0) rightArray = new int[midpoint];
		// if odd right will be bigger 
		else rightArray = new int[midpoint + 1];

		// fill left array
		for (int i = 0; i < midpoint; i++)
		{
			leftArray[i] = input[i];
		}

		int x = 0;
		for (int i = midpoint; i < input.Length; i++)
		{
			rightArray[x] = input[i];
			x++;
		}

		leftArray = Sort(leftArray); // get sorted array
		rightArray = Sort(rightArray); // get sorted array

		result = Merge(leftArray, rightArray);
		return result;
	}

	private static int[] Merge(int[] leftArray, int[] rightArray)
	{
		int resultLength = leftArray.Length + rightArray.Length;
		int[] result = new int[resultLength];

		int indexLeft = 0, indexRight = 0, indexResult = 0;

		// while either array has any elements left 
		while (indexLeft < leftArray.Length || indexRight < rightArray.Length)
		{
			// if the both have more 
			if (indexLeft < leftArray.Length && indexRight < rightArray.Length)
			{
				if (leftArray[indexLeft] <= rightArray[indexRight])
				{
					result[indexResult] = leftArray[indexLeft];
					indexResult++;
					indexLeft++;
				}
				else
				{
					result[indexResult] = rightArray[indexRight];
					indexResult++;
					indexRight++;
				}
			}
			// only left still has things in it 
			else if (indexLeft < leftArray.Length)
			{
				result[indexResult] = leftArray[indexLeft];
				indexResult++;
				indexLeft++;
			}
			// or only left 
			else if (indexRight < rightArray.Length)
			{
				result[indexResult] = rightArray[indexRight];
				indexResult++;
				indexRight++;
			}
		}
		return result;
	}
}

public static class GenMergeSort<T> where T : IComparable
{
	public static T[] Sort(T[] input)
	{
		T[] leftArray;
		T[] rightArray;
		T[] result = new T[input.Length];

		// to break recursion
		if (input.Length <= 1) return input;

		int midpoint = input.Length / 2;
		leftArray = new T[midpoint];

		if (input.Length % 2 == 0) rightArray = new T[midpoint];
		// if odd right will be bigger 
		else rightArray = new T[midpoint + 1];

		// fill left array
		for (int i = 0; i < midpoint; i++)
		{
			leftArray[i] = input[i];
		}

		int x = 0;
		for (int i = midpoint; i < input.Length; i++)
		{
			rightArray[x] = input[i];
			x++;
		}

		leftArray = Sort(leftArray); // get sorted array
		rightArray = Sort(rightArray); // get sorted array

		result = Merge(leftArray, rightArray);
		return result;
	}

	private static T[] Merge(T[] leftArray, T[] rightArray)
	{
		int resultLength = leftArray.Length + rightArray.Length;
		T[] result = new T[resultLength];

		int indexLeft = 0, indexRight = 0, indexResult = 0;

		// while either array has any elements left 
		while (indexLeft < leftArray.Length || indexRight < rightArray.Length)
		{
			// if the both have more 
			if (indexLeft < leftArray.Length && indexRight < rightArray.Length)
			{
				if (leftArray[indexLeft].CompareTo(rightArray[indexRight]) <= 0)
				{
					result[indexResult] = leftArray[indexLeft];
					indexResult++;
					indexLeft++;
				}
				else
				{
					result[indexResult] = rightArray[indexRight];
					indexResult++;
					indexRight++;
				}
			}
			// only left still has things in it 
			else if (indexLeft < leftArray.Length)
			{
				result[indexResult] = leftArray[indexLeft];
				indexResult++;
				indexLeft++;
			}
			// or only left 
			else if (indexRight < rightArray.Length)
			{
				result[indexResult] = rightArray[indexRight];
				indexResult++;
				indexRight++;
			}
		}
		return result;
	}
}
