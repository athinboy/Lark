using System;
using System.Collections.Generic;
using System.Text;

namespace Feign.Core.Attributes
{

    /// <summary>
    /// requset body
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class BodyAttribute : Attribute
    {
    }
}
