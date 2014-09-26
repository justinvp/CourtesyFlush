using System;
using System.Web.Mvc;

namespace CourtesyFlush
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class FlushHeadAttribute : ActionFilterAttribute
    {
        public string Title { get; set; }
        public bool FlushAntiForgeryToken { get; set; }

        private readonly Func<ActionDescriptor, ViewDataDictionary> _viewDataFunction;

        public FlushHeadAttribute()
        {
        }

        public FlushHeadAttribute(Func<ActionDescriptor, ViewDataDictionary> viewDataFunction)
        {
            _viewDataFunction = viewDataFunction;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool bypassFlushHead = filterContext.ActionDescriptor.IsDefined(typeof(BypassFlushHeadAttribute), inherit: true)
                                   || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(BypassFlushHeadAttribute), inherit: true);

            if (bypassFlushHead)
            {
                return;
            }

            var controller = filterContext.Controller;

            if (_viewDataFunction != null)
                controller.ViewData = _viewDataFunction(filterContext.ActionDescriptor);

            controller.FlushHead(Title, null, FlushAntiForgeryToken);
        }
    }
}