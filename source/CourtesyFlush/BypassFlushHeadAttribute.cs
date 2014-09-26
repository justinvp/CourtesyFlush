using System;

namespace CourtesyFlush
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public sealed class BypassFlushHeadAttribute : Attribute
    {
    }
}
