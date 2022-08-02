using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KotClassLibrary.Helpers
{
    public class SuportedCultresHelper
    {
        public const string English = "en";
        public const string Dutch = "nl";
        public const string French = "fr";

        public static string[] GetList => new[]
        {
            English,
            Dutch,
            French
        };

        public static List<CultureInfo> GetCultrueInfoList => new List<CultureInfo>
        {
                new CultureInfo(English),
                new CultureInfo(Dutch),
                new CultureInfo(French)
        };
    }
}
