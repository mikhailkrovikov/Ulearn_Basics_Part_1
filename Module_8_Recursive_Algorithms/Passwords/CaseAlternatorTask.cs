using System.Linq;

namespace Passwords;

public class CaseAlternatorTask
{
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
        if (word.Length == 0)
        {
            result.Add("");
            return;
        }

        if (startIndex == word.Length)
        {
            result.Add(new string(word));
            result = result.Distinct().ToList();
            return;
        }

        else
        {
            if (char.IsLetter(word[startIndex]) && word[startIndex] != 223 && (word[startIndex] < 1425 || word[startIndex] > 1524))
            {
                word[startIndex] = char.ToLower(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = char.ToUpper(word[startIndex]);
                AlternateCharCases(word, startIndex + 1, result);
                word[startIndex] = char.ToLower(word[startIndex]);
            }

            else
            {
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
    }
}