using System.Text.RegularExpressions;

namespace StaffBase.Core.Utils
{
    public class Check
    {
        public static (bool check, string error) TextFields(string? str, int MaxValue)
        {
            string error = string.Empty;
            if(string.IsNullOrEmpty(str))
            {
                error = "empty field!";
                return (false,error);
            }
            if(str.Length > MaxValue)
            {
                error = "Too long field value!";
                return (false, error);
            }
            if (str.Contains("\""))
            {
                error = "Don't use: \" !";
                return (false, error);
            }
            return (true, error);
        }

        public static (bool check, string error) PassportFields(string? str, int digitsCount)
        {
            string error = string.Empty;
            if(string.IsNullOrEmpty(str))
            {
                error = "empty field!";
                return (false, error);
            }
            if(str.Length != digitsCount)
            {
                error = "bad digit count!";
                return (false, error);
            }
            string pattern = @"^[0-9]+$";
            bool isMatch = Regex.IsMatch(str, pattern);
            if (!isMatch)
            {
                error = "Uncorrect symbols!";
                return (false, error);
            }
            return (true, error);
        }

        public static (bool check, string error) AddressFields(string? str, int MaxValue)
        {
            string error = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                error = "empty field!";
                return (false, error);
            }
            if (str.Length > MaxValue)
            {
                error = "Too long field value!";
                return (false, error);
            }
            if (str.Contains("\""))
            {
                error = "Don't use: \" !";
                return (false, error);
            }
            return (true, error);
        }
    }
}
