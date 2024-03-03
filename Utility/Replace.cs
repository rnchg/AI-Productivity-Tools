using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace General.Apt.App.Utility
{
    public static class Replace
    {
        public static string ReplaceInvalidPathChars(string path)
        {
            var invalids = Path.GetInvalidPathChars();
            var search = new string(invalids.ToArray());
            var pattern = $"[{Regex.Escape(search)}]";
            var result = Regex.Replace(path, pattern, string.Empty);
            return result.Trim();
        }

        public static string ReplaceInvalidFileNameChars(string fileName)
        {
            var invalids = Path.GetInvalidFileNameChars();
            var search = new string(invalids.ToArray());
            var pattern = $"[{Regex.Escape(search)}]";
            var result = Regex.Replace(fileName, pattern, string.Empty);
            return result.Trim();
        }
    }
}
