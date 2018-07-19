using System.Text;
using System.Text.RegularExpressions;

namespace Nevara.ApplicationCore.Helpers
{
    public static class Util
    {
       public static string ConvertToUnsign(string input)
        {
            input = input.Trim();          
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            return str2;
        }
    }
}
