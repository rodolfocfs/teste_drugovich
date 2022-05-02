using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAPIGerenciadorDeClientes.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToHash(this string content)
            => BCrypt.Net.BCrypt.HashPassword(content);

        public static bool Verify(this string hash, string comparer)
            => BCrypt.Net.BCrypt.Verify(comparer, hash);

        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return (startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2")).ToLower();
        }

        public static string ToUpperSnakeCase(this string input) => input.ToSnakeCase().ToUpper();

        public static string ReplaceLastOccurrence(this string source, string find, string replace)
        {
            int place = source.LastIndexOf(find);

            if (place == -1)
                return source;

            string result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        public static string ReplaceFirstOccurrance(this string original, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(original))
                return string.Empty;
            if (string.IsNullOrEmpty(oldValue))
                return original;
            if (string.IsNullOrEmpty(newValue))
                newValue = string.Empty;
            int loc = original.IndexOf(oldValue);
            return original.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(letter);
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Returns a string without tag HTML
        /// </summary>
        /// <param name="stringHtml"></param>
        /// <returns></returns>
        public static string HtmlToText(this string stringHtml)
        {
            if (stringHtml != null)
            {
                string stringTxt = stringHtml.Replace("\n\n", " ")
                                        .Replace("<\n\t>", " ")
                                        .Replace("\n", " ")
                                        .Replace("<p>", " ")
                                        .Replace("</p>", "\n")
                                        .Replace("</li>", "\n")
                                        .Replace("</ul>", " ")
                                        .Replace("<li>", " ")
                                        .Replace("<ul>", " ")
                                        .Replace("&nbsp;", " ");
                var stringClear = RemoveHtmlTags(stringTxt);
                return stringClear;
            }
            else
            {
                return " ";
            }
        }
        public static string RemoveHtmlTags(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string TreatPartNumber(this string partNumber, string sos)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9.]");
            Regex rgxTexto = new Regex("[0-9]");
            Regex rgxPonto = new Regex("[.]");

            string cleanPartNumber = rgx.Replace(partNumber, "");
            string cleanSos = rgxPonto.Replace(sos, "");

            if (!rgxTexto.IsMatch(cleanPartNumber))
            {
                return partNumber;
            }

            if (!string.IsNullOrEmpty(cleanPartNumber) && !cleanPartNumber.Contains("."))
            {
                return cleanPartNumber + "." + cleanSos; // sos;
            }

            if (cleanPartNumber.IndexOf(".") == cleanPartNumber.Length - 1)
            {
                return cleanPartNumber + cleanSos; // sos;
            }

            return cleanPartNumber;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string[] SplitInArray(this string value, int lineLength = 72)
        {
            int linesCount = value.Length / lineLength;
            if (linesCount == 0)
                return new string[1] { value };

            return Enumerable.Range(0, linesCount)
                .Select(i => value.Substring(i * lineLength, lineLength)).ToArray();
        }

        public static bool IsNumeric(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }
        public static bool IsUnsignNumeric(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"^[+]?\d*[.]?\d*$");
        }
        public static bool IsInteger(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }
        public static bool IsUnsignInteger(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return Regex.IsMatch(value, @"^[+]?\d*$");
        }

        public static string ToMask(this string value, string mask, char matchCharacter = '9')
        {
            var patternMatch = value.Length == mask.Count(c => c == matchCharacter);

            if (string.IsNullOrEmpty(mask) || !patternMatch)
                return value;

            return value.Aggregate(mask, (current, character) =>
                current.ReplaceFirstOccurrance(matchCharacter.ToString(), character.ToString()));
        }
    }
}
