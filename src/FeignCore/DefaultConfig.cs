using System.Collections.Generic;

namespace Feign.Core
{
    public class DefaultConfig
    {
        public static string DefaultHttpMethod="GET";

        public static List<string> SupportHttpMethod=new List<string>{"GET","POST"};
        internal static bool HeaderUnique=true;
    }
}