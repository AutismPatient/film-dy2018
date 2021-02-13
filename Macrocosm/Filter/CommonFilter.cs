using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Macrocosm.Filter
{
    /// <summary>
    /// MVC通用过滤器
    /// </summary>
    public class CommonFilter: ActionFilterAttribute
    {
        public CommonFilter() { }
        public SqlContext Context;
        private ILogger<CommonFilter> logger;
        public CommonFilter(SqlContext _context, ILogger<CommonFilter> _logger)
        {
            Context = _context;
            logger = _logger;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ignore = context.ActionDescriptor.FilterDescriptors
            .Select(f => f.Filter)
            .OfType<TypeFilterAttribute>()
            .Any(f => f.ImplementationType.Equals(typeof(IgonreGlobalActionFilter)));

            if (ignore)
            {
                await next();
                return;
            }
            string[] str = new string[] {"GET","POST" };
            if(!str.Contains(context.HttpContext.Request.Method))
            {
                context.Result = ResultMsg("request method is error!");
            }
            string host = context.HttpContext.Request.Host.Host;
            string classpath = context.HttpContext.Request.Headers["Host"].ToString();
            if (!classpath.Contains(host) || string.IsNullOrEmpty(classpath))
            {
                context.Result = ResultMsg("Request error,You cannot connect to the server from outside.");
                context.HttpContext.Response.StatusCode = 403;
                await base.OnActionExecutionAsync(context, next);
            }
            var now = DateTime.Now.GetTimeStamp();
            await Context.Action_Logs.AddAsync(new Action_logs {
                Path = context.HttpContext.Request.Path,
                DateLine = now,
                ClientIP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                Status = 1,
            });

            logger?.LogInformation("PATH:{path},DATE:{date},CLIENTIP:{ip},STATUS:ok",context.HttpContext.Request.Path, now,context.HttpContext.Connection.RemoteIpAddress.ToString());
            
            await Context.SaveChangesAsync();

            await base.OnActionExecutionAsync(context, next);
        }
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
        private JsonResult ResultMsg(string msg)
        {
            logger?.LogError(msg);
            return new JsonResult(msg);
        }
    }
}
