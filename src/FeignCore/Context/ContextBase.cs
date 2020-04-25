using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Feign.Core.Attributes;
using Feign.Core.Cache;
using Feign.Core.Exception;

namespace Feign.Core.Context
{
    internal abstract class ContextBase
    {

        /// <summary>
        /// Clear cache、status etc.
        /// </summary>
        internal abstract void Clear();

    }
}