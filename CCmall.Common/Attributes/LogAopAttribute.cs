using System;
using System.Collections.Generic;
using System.Text;

namespace CCmall.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class LogAopAttribute : Attribute
    {
    }
}
