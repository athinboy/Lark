using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lark.Core
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
            if (false == url.StartsWith("/"))
            {
                url = "/" + url;
            }
            return url;

        }

        public static IEnumerable<string> GetPathParaName(string path)
        {
            Regex pathparaRegex = new Regex(@"\{([a-z|A-Z]{1,})\}");
            MatchCollection matchs = pathparaRegex.Matches(path.ToLower());                
            return matchs.ToList().ConvertAll<string>(x => { return x.Groups[1].Value; }).ToList().Distinct();
        }



    }
}
