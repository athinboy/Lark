using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core
{
    public class Util
    {

        /// <summary>
        /// normalize URL
        /// url should begin with "/" ,and would not end with "/"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string NormalizeURL(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }
            while (url.EndsWith("/"))
            {
                url = url.Remove(url.Length - 1);
            }
            if (false==url.StartsWith("/"))
            {
                url = "/" + url;
            }
            return url;


        }

        



    }
}
