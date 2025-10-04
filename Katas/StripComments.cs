namespace Katas;

public class StripComments : IKata
{
    public string Solve(string text, string[] commentSymbols)
    {
        for (var i = 0; i < text.Length; i++)
        {
            if (!commentSymbols.Contains(text[i].ToString())) continue;

            int commentStartIndex = i;
            for (int j = i; j < text.Length; j++)
            {
                if (text[j].ToString() == "\\n") // line has ended 
                {
                    int commentEndIndex = j;
                    i = j;
                    text = text.Remove(commentStartIndex, commentEndIndex - commentStartIndex);
                    break; // stop looking for this occurrence of comment symbol
                }

                if (j == text.Length - 1)
                {
                    int commentEnd = j + 1;
                    i = j;
                    text = text.Remove(commentStartIndex, commentEnd - commentStartIndex);
                }
            }
        }

        return text;
    }

    public string Solve1(string text, string[] commentSymbols)
    {
        var lines = text.Split("\\n");
        for (int i = 0; i < lines.Length; i++)
        {
            foreach (var symbol in commentSymbols)
            {
                var idx = lines[i].IndexOf(symbol);
                if (idx >= 0)
                {
                    lines[i] = lines[i].Substring(0, idx).TrimEnd();
                    break;
                }
            }
        }
        return string.Join("\n", lines);
    }
}