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
    internal abstract class ContextBase
    {

        public SerializeTypes SerializeType=DefaultConfig.DefaultSerilizeType;       


        /// <summary>
        /// Clear cache、status etc.
        /// </summary>
        internal abstract void Clear();

        internal virtual void AddHeader(RequestCreContext requestCreContext)
        {

        }

        internal virtual void AddQueryString(RequestCreContext requestCreContext)
        {

        }


        internal abstract void CreateBind();





    }
}