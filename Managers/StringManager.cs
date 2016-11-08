using System;
using DBO.Data.Objects;

namespace DBO.Data.Managers
{
    public static class StringManager
    {
        public static string Substring(this string value, int startIndex, int length, bool ignoreException)
        {
            if (ignoreException)
            {
                string result = "";
                if (length < 0)
                {
                    return result;
                }
                if (value.Length > startIndex + length && startIndex > -1)
                {
                    result = value.Substring(startIndex, length).Trim();
                }
                else if (value.Length > startIndex && startIndex > -1)
                {
                    result = value.Substring(startIndex, value.Length - startIndex).Trim();
                }
                else
                {
                    result = "";
                }
                return result;
            }
            else
                return value.Substring(startIndex, length);
        }

        public static string FormatWith(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static string FormatWhenNotNull(this string value, string format)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            else
                return format.FormatWith(value);
        }

        public static string Trim(this string value, bool nullToEmptyString)
        {
            if (value == null)
                value = "";
            return value.Trim();
        }

        private static string[] ones = {
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", 
            "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN",
        };
        private static string[] tens = { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
        private static string[] thous = { "HUNDRED", "THOUSAND", "MILLION", "BILLION", "TRILLION", "QUADRILLION" };
        public static string ToWords(this Currency number)
        {
            return number.OriginalValue.ToWords();
        }
        public static string ToWords(this decimal number)
        {
            if (number < 0)
                return "MINUS " + ToWords(Math.Abs(number));

            int intPortion = (int)number;
            int decPortion = (int)((number - intPortion) * (decimal)100);

            if (decPortion > 0)
                return string.Format("{0} AND CENTS {1} ONLY", ToWords(intPortion), ToWords(decPortion));
            else
                return string.Format("{0} ONLY", ToWords(intPortion));
        }
        public static string ToWords(this int number, string appendScale = "")
        {
            string numString = "";
            if (number < 100)
            {
                if (number < 20)
                    numString = ones[number];
                else
                {
                    numString = tens[number / 10];
                    if ((number % 10) > 0)
                        numString += "-" + ones[number % 10];
                }
            }
            else
            {
                int pow = 0;
                string powStr = "";

                if (number < 1000) // number is between 100 and 1000
                {
                    pow = 100;
                    powStr = thous[0];
                }
                else // find the scale of the number
                {
                    int log = (int)Math.Log(number, 1000);
                    pow = (int)Math.Pow(1000, log);
                    powStr = thous[log];
                }

                numString = string.Format("{0} {1}", ToWords(number / pow, powStr), ToWords(number % pow)).Trim();
            }

            return string.Format("{0} {1}", numString, appendScale).Trim();
        }
    }
}
