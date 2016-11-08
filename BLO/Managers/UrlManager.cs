using System.Linq;

namespace BLO.Managers
{
    public static class UrlManager
    {
        public static string ToNormalizeName(this string name)
        {
            if (string.IsNullOrEmpty(name))
                return name;
            else
            {
                var result = string.Join("-", name.Split(" ~`!@#$%^&*()_+={}[]|\\:;\"'<>?,/.".ToArray()))
                    .Replace("---", "-")
                    .Replace("--", "-");
                if (result.EndsWith("-"))
                    result = result.Substring(0, result.Length - 1);
                return result;
            }
        }
    }
}
