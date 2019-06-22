using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sonic.Downloader
{
    public class UrlVerification
    {
        public static bool Verify(string url)
        {
            if(!string.IsNullOrWhiteSpace(url))
            {
                try
                {
                    //is valid url
                    Uri u = new Uri(url);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
