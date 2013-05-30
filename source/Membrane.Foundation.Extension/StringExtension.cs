
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace Membrane.Foundation.Extension
{
    /// <summary>
    /// Extends functionality on strings.
    /// </summary>
    public static class StringExtension
    {
        #region - Private fields -

        private static string[] LOREM_IPSUM_WORDS =
            new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

        #endregion

        /// <summary>
        /// Generates a random text with "Lorem ipsum ..."
        /// </summary>
        /// <param name="text">The string to fill with the generated text.</param>
        /// <param name="minWords">The minimum number of words.</param>
        /// <param name="maxWords">The maximum number of words.</param>
        /// <param name="minSentences">The minimum number of sentences.</param>
        /// <param name="maxSentences">The maximum number of sentences.</param>
        /// <param name="numParagraphs">The number of paragraphs.</param>
        /// <returns>The generated text.</returns>
        public static string LoremIpsum(this string text, int minWords, int maxWords, int minSentences, int maxSentences, int numParagraphs)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            int numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            int numWords = rand.Next(maxWords - minWords) + minWords + 1;

            string result = string.Empty;

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result += " "; }

                        string word = StringExtension.LOREM_IPSUM_WORDS[rand.Next(StringExtension.LOREM_IPSUM_WORDS.Length)];
                        if (w == 0) { word = word.Substring(0, 1).ToUpper() + word.Substring(1); }
                        result += word;
                    }
                    result += ". ";
                }
                result += Environment.NewLine;
            }

            return result;
        }

        /// <summary>
        /// Formats a string with the given array of objects.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="parameters">The array of formatting parameters.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatWith(this string value, params object[] parameters)
        {
            return string.Format(value, parameters);
        }

        /// <summary>
        /// Returns the string value or a given default value if NULL or empty. 
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The string value or the the default value if NULL or empty.</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            if (!value.IsNotEmpty())
            {
                return defaultValue;
            }
            return value;
        }

        /// <summary>
        /// Checks if the given string is empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>TRUE if the string is empty, FALSE otherwise.</returns>
        public static bool IsEmpty(this string value)
        {
            if (value != null)
            {
                return (value.Length == 0);
            }
            return true;
        }

        /// <summary>
        /// Checks if the given string is not empty.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>TRUE if the string is not empty, FALSE otherwise.</returns>
        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        /// <summary>
        /// Truncates a string at the given length.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <returns>The truncated string.</returns>
        public static string TruncateAt(this string value, int maxLength)
        {
            if ((value != null) && (value.Length > maxLength))
            {
                return value.Substring(0, maxLength);
            }
            return value;
        }

        /// <summary>
        /// Truncates a string at the given length, thereafter the suffix is added.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="maxLength">The maximum length of the string.</param>
        /// <param name="suffix">The suffix to add to the string.</param>
        /// <returns>The truncated string with suffix.</returns>
        public static string TruncateAt(this string value, int maxLength, string suffix)
        {
            if ((value != null) && (value.Length > maxLength))
            {
                return (value.Substring(0, maxLength) + suffix);
            }
            return value;
        }

        /// <summary>
        /// Converts a camelcased string to a user friendly text by replacing the uppercase characters by a combination of a space and lowercase character
        /// </summary>
        /// <param argumentName="camelCaseString">Camel case formatted string</param>
        /// <returns></returns>
        public static string Humanize(this string value)
        {
            StringBuilder humanText = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(value))
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (value.Substring(i, 1) == value.Substring(i, 1).ToUpper())
                    {
                        if (i != 0)
                        {
                            humanText.Append(" ");
                            humanText.Append(value.Substring(i, 1).ToLower());
                        }
                        else
                        {
                            humanText.Append(value.Substring(i, 1));
                        }
                    }
                    else
                    {
                        humanText.Append(value.Substring(i, 1).ToLower());
                    }
                }

                humanText = humanText.Replace("_", string.Empty);
                humanText = humanText.Replace("  ", " ");
            }

            return humanText.ToString().Trim();
        }


        /// <summary>
        /// Calculates a hash for the specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The hash value.</returns>
        public static string Hash(this string value)
        {
            value.CatchNullArgument("value");

            byte[] data = value.ToByteArray();
            byte[] result;
            SHA256 sha256 = new SHA256Managed();
            result = sha256.ComputeHash(data);

            return result.ToBase64String();
        }

        /// <summary>
        /// Matches the mail address to a strict pattern.
        /// </summary>
        /// <param name="mailAddress">The mail address.</param>
        /// <returns>TRUE if the mail address matches a generic regex pattern, FALSE otherwise.</returns>
        public static bool IsValidMailAddress(this string mailAddress)
        {
            string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
                            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                            + @"[a-zA-Z]{2,}))$";

            Regex expression = new Regex(pattern);
            bool isValidMailAddress = expression.IsMatch(mailAddress);

            return isValidMailAddress;
        }
    }
}