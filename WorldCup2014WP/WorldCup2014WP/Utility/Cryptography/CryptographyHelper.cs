using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WorldCup2014WP.Utility
{
    public class CryptographyHelper
    {
        private const string TOKEN = "QX4BUJWPSU6JTN73";
        private const string PRIVATE_KEY = "07fc99e107434c618e206874f323ed6e";
        private const string SIGN_PARAM_FORMAT = "{0}:{1}:{2}";
        private const string POSTFIX_FORMAT = "&token={0}&sign={1}&t={2}";
        private static DateTime DT_MIN = new DateTime(1970, 1, 1, 0, 0, 0);

        public static string GetApiPostfix()
        {
            return Constants.API_POSTFIX;
            //return string.Empty;

            string timestamp = ((DateTime.Now - DT_MIN).Ticks / TimeSpan.TicksPerMillisecond).ToString();
            string param = string.Format(SIGN_PARAM_FORMAT, TOKEN, PRIVATE_KEY, timestamp);
            string sign = MD5Core.GetHashString(param).ToLower();
            string postfix = string.Format(POSTFIX_FORMAT, TOKEN, sign, timestamp);
            return postfix;
        }

    }
}
