namespace Katas;

public class StripComments : IKata
{
    public string Solve(string text, string[] commentSymbols)
    {
        var lines = text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            foreach (var symbol in commentSymbols)
            {
                var idx = lines[i].IndexOf(symbol);
                if (idx >= 0)
                {
                    lines[i] = lines[i].Substring(0, idx);
                }
            }
        }
        return string.Join("\n", lines);
    }
}