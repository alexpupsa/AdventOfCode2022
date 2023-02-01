using System.Text;

namespace AdventOfCode13
{
    public static class Extensions
    {
        public static string[] SpecialSplit(this string item, char separator = ',')
        {
            var parts = new List<string>();
            if (item.Length > 0)
            {
                var currentPart = new StringBuilder();
                var bracketsOpen = 0;
                for (var i = 0; i < item.Length; i++)
                {
                    var appendChar = true;
                    if (item[i] == '[')
                    {
                        bracketsOpen++;
                    }
                    else if (item[i] == ']')
                    {
                        bracketsOpen--;
                    }
                    else if (item[i] == separator)
                    {
                        if (bracketsOpen == 0)
                        {
                            parts.Add(currentPart.ToString());
                            currentPart.Clear();
                            appendChar = false;
                        }
                    }
                    if (appendChar)
                    {
                        currentPart.Append(item[i]);
                    }
                }
                parts.Add(currentPart.ToString());
            }

            return parts.ToArray();
        }

        public static string TrimOneCharacter(this string item, char[] chars)
        {
            if (item.Length > 0)
            {
                if (chars.Contains(item[0]))
                {
                    item = item[1..];
                }
                if (chars.Contains(item[^1]))
                {
                    item = item[..^1];
                }
            }
            return item;
        }

    }
}
