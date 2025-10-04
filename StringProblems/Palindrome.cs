namespace StringProblems;

// Q : find out if a string is a palindrome
public static class Palindrome
{
    public static bool Base(string input)
    {
        if (input.Length == 0) return false;

        var charArray = input.ToCharArray();
        Array.Reverse(charArray);
        var reversedString = new string(charArray);

        if (reversedString.Equals(input, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        return false;
    }

    public static bool Optimized(string input)
    {
        if (input.Length == 0) return false;

        input = input.ToLower();
        for (int i = 0; i < input.Length; i++)
        {
            var letterA = input[i];
            var letterB = input[input.Length - (i + 1)];
            if (letterA != letterB)
                return false;
        }

        return true;
    }

    // winner! Does to lower once, allocate 0 bytes
    // O(n)
    public static bool Optimizedv2(string input)
    {
        if (input.Length == 0) return false;

        input = input.ToLower();

        var halflength = input.Length / 2;

        for (int i = 0; i < halflength; i++)
        {
            var letterA = input[i];
            var letterB = input[input.Length - (i + 1)];
            if (letterA != letterB)
            {
                return false;
            }
        }

        return true;
    }

    public static bool Optimizedv3(string input)
    {
        if (input.Length == 0) return false;

        var halflength = input.Length / 2;

        for (int i = 0; i < halflength; i++)
        {
            var letterA = char.ToLower(input[i]);
            var letterB = char.ToLower(input[input.Length - (i + 1)]);
            if (letterA != letterB)
            {
                return false;
            }
        }

        return true;
    }
}