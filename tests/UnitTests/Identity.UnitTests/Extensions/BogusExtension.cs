using Bogus;
using Bogus.DataSets;

namespace Identity.UnitTests.Extensions
{
    internal static class BogusExtension
    {
        public static string PasswordCustom(this Internet internet)
        {
            var r = internet.Random;

            char lowerLetter = r.Char('a', 'z');
            char upperLetter = r.Char('A', 'Z');
            char digit = r.Char('0', '9');
            char symbol = r.Char((char)33, (char)47);

            string padding = r.String2(r.Number(4, 8));

            string combined = $"{lowerLetter}{upperLetter}{digit}{symbol}{padding}";
            string shuffled = new string(r.Shuffle(combined.ToArray()).ToArray());

            return shuffled;
        }
    }
}
