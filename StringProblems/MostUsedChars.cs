namespace StringProblems;

public static class MostUsedChars
{
    // Q : find the most repeated chars in a string
    public static string Base(string input)
    {
        // why bother 
        if (input.Length <= 1) return input;

        input = input.ToLower();

        // each spot is the int representation of char
        int[] memory = new int[26];
        // take a char 
        for (int i = 0; i < input.Length; i++)
        {
            int position = input[i] - 'a';
            memory[position]++;
        }

        int max = memory.Max();
        string result = string.Empty;

        for (int i = 0; i < memory.Length; i++)
        {
            if (memory[i] == max)
            {
                result += new string((char)('a' + i), max);
            }
        }

        return result;
    }
}