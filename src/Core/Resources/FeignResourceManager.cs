using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;



namespace Feign.Core.Resources
{
    public class FeignResourceManager
    {
        private static ResourceManager resourceManager;


        private static System.Globalization.CultureInfo currentCultureInfo;

        static FeignResourceManager()
        {
            resourceManager = new ResourceManager("Resources",
                             typeof(FeignResourceManager).Assembly);

            currentCultureInfo = System.Globalization.CultureInfo.CurrentCulture;


        }

        public static string getStr(string name)
        {
            string r = resourceManager.GetString(name, currentCultureInfo);
            return r ?? name;

        }





    }
}



