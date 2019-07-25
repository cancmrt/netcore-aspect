using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace webapi.Aspect.Logging
{
    public class RestApiLog: ActionFilterAttribute, IExceptionFilter
    {
        Stopwatch watch;
        public RestApiLog()
        {
            watch = new Stopwatch();
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            watch.Start();
            base.OnActionExecuting(context);
            Log.Information("Excution start at..."+DateTime.Now.ToString());
        }
        public void OnException(ExceptionContext context)
        {
            Log.Error(context.Exception.ToString());
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            watch.Stop();
            base.OnActionExecuted(context);
            Log.Information("Excution stopped at..." + DateTime.Now.ToString());
            Log.Information("Excution time is..." + watch.Elapsed);
        }
    }
}
