using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;
using Feign.Core.Enum;

namespace Feign.Core.Context
{
    public abstract class ContextBase
    {

     


        /// <summary>
        /// Clear cache、status etc.
        /// </summary>
        internal abstract void Clear();


        internal abstract void CreateBind();





    }
}