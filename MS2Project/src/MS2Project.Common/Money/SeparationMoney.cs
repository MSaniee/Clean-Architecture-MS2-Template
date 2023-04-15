using System;

namespace $ext_safeprojectname$.Common.Money
{
    public static class SeparationMoney
    {
        public static string Seprate(string Money)
        {
            var res = "";
            int i = 0;
            var arrLen = Money.Length / 3;
            char[] revcharArray = Money.ToCharArray();
            Array.Reverse(revcharArray);
            var newstr = new string(revcharArray);
            while (i < 3 * arrLen)
            {
                res += newstr.Substring(i, 3);
                res += ",";
                i += 3;
            }

            char[] charArray = res.ToCharArray();
            Array.Reverse(charArray);
            var strres = new string(charArray);
            if (Money.Length % 3 == 0)
            {
                strres = strres.Substring(1, strres.Length - 1);
            }
            if (Money.Length % 3 == 1)
            {
                var first = Money.Substring(0, 1);
                var temp = strres;
                first += temp;
                strres = first;
            }
            if (Money.Length % 3 == 2)
            {
                var first = Money.Substring(0, 2);
                var temp = strres;
                first += temp;
                strres = first;
            }

            return strres;
        }
    }
}
