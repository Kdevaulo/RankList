using System.Globalization;

namespace Kdevaulo.RankList.Presentation.Utilities
{
    public static class IntExtensions
    {
        private static readonly NumberFormatInfo FormatInfo = new NumberFormatInfo
        {
            NumberGroupSeparator = " ",
            NumberGroupSizes = new[] { 3 }
        };

        public static string ToSpacedString(this int value)
        {
            return value.ToString("N0", FormatInfo);
        }
    }
}