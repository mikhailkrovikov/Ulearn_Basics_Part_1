using System;
using System.Linq;
namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            string input = count.ToString();
            char[] symbols = input.ToCharArray();
            int length = symbols.Length;
            char lastSign = symbols[symbols.Length - 1];
            if (length < 2)
            {
                if (symbols[0] == '0' ||
                    symbols[0] == '5' ||
                    symbols[0] == '6' ||
                    symbols[0] == '7' ||
                    symbols[0] == '8' ||
                    symbols[0] == '9')
                    return "рублей";
                else if (symbols[0] == '1')
                    return "рубль";
                else return "рубля";
            }
            else if (length == 2 && count <= 20) return "рублей";
            else if (length >= 2 && count > 20)
            {
                char preLastSign = symbols[symbols.Length - 2];
                if (preLastSign == '1') return "рублей";
                if (lastSign == '0' ||
                    lastSign == '5' ||
                    lastSign == '6' ||
                    lastSign == '7' ||
                    lastSign == '8' ||
                    lastSign == '9')
                    return "рублей";
                else if (lastSign == '1')
                    return "рубль";
                else return "рубля";
            }
            return "ошибка";
        }
    }
}