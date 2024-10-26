namespace StringProblems;

public static class Pangram
{
    /* Desc.
    A pangram is a sentence that contains every single letter of the alphabet at least once.
    For example, the sentence "The quick brown fox jumps over the lazy dog" is a pangram, because it uses the letters A-Z at least once (case is irrelevant).
    Given a string, detect whether it is a Pangram. Return True if it is, False if not. Ignore numbers and punctuation.
     */

    public static bool Base(string input)
    {
        var result = true;
        char[] charArray = input.ToLower().Replace(" ", null).ToCharArray();
        int[] memory = new int[26];
        foreach (char kati in charArray)
        {
            if (char.IsDigit(kati) || char.IsPunctuation(kati) || char.IsSeparator(kati))
            {
                continue;
            }

            int index = (byte)kati - (byte)'a';
            memory[index]++;
        }

        foreach (var count in memory)
        {
            if (count == 0) result = false;
        }

        return result;
    }
}