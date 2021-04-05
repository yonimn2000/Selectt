using System;
using System.Linq;

namespace Selectt
{
    public static class Utils
    {
        // I really hope that this is a random enough seed value.
        private static readonly Random random = new Random(
            (Guid.NewGuid().GetHashCode() + (int)(DateTime.UtcNow.Ticks + 7 % int.MaxValue)) % int.MaxValue);

        /// <summary> Generates a random token of the specified length. </summary>
        /// <param name="length">Token length</param>
        /// <param name="includeSpecialChars">If true, the generated token will include special characters</param>
        /// <param name="includeLowerCase">If true, the generated token will include lower case characters</param>
        /// <param name="includeUpperCase">If true, the generated token will include upper case characters</param>
        /// <param name="includeNumbers">If true, the generated token will include numbers</param>
        public static string GenerateToken(int length, bool includeSpecialChars = true, bool includeLowerCase = true,
            bool includeUpperCase = true, bool includeNumbers = true)
        {
            if (!(includeSpecialChars || includeLowerCase || includeUpperCase || includeNumbers))
                throw new ArgumentException("At least one character type must be enabled.");
            string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCaseLetters = "abcdefghijklmnopqrstuvdxyz";
            string numbers = "0123456789";
            string specialChars = "!@#$%^&*()_+=-`~ \\\"':|/";
            string tokenPossibleCharcaters = (includeUpperCase ? upperCaseLetters : "") + (includeLowerCase ? lowerCaseLetters : "")
                + (includeNumbers ? numbers : "") + (includeSpecialChars ? specialChars : "");
            return new string(Enumerable.Repeat(tokenPossibleCharcaters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}