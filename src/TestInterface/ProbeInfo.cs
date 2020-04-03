using System;
using System.Collections.Generic;
using System.Text;

namespace TestInterface
{
    public class ProbeInfo
    {

        public string Url { get; set; }
        public string Method { get; set; }

        public List<KeyValuePair<string, string>> Headers { get; set; } = new List<KeyValuePair<string, string>>();

        public string Body { get; set; }

    }
}
