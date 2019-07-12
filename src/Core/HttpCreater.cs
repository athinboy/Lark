
using Feign.Core.Attributes;
using Feign.Core.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core
{
    internal class HttpCreater
    {


        private static List<object> empterArgs = new List<object>();

        internal static void Create(MethodWrapContext mwContext, List<Object> args)
        {

            args = args ?? empterArgs;

            if (mwContext == null)
            {
                throw new ArgumentNullException(nameof(mwContext));
            }

            for (int i = 0; i < args.Count; i++)
            {
                if (InternalConfig.LogRequestParameter)
                {
                    System.Console.WriteLine(args[i].ToString());
                }

            }
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
             



        }
    }
}

