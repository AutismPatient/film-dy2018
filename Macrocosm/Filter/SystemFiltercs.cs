using Macrocosm.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Filter
{
    /// <summary>
    /// 后台管理过滤器
    /// </summary>
    public class SystemFiltercs: ActionFilterAttribute
    {
        private readonly RedisHelper _redisHelper;
        public SystemFiltercs() { }
        public SystemFiltercs(RedisHelper redisHelper)
        {
            this._redisHelper = redisHelper;
        }
        /// <summary>
        /// 登录认证：将cookies存到redis里
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool isPass = false;
            var cookieOpt = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1),
                Path = "/",
                IsEssential = true
            };
            var sessionID = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultSessionName); // 会话ID
            var Token = context.HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultTokenName); // token 秘钥
            if (Token.Value == null || sessionID.Value == null || string.IsNullOrEmpty(sessionID.Value))
            {
                string host = context.HttpContext.Request.Host.Host;
                string classpath = context.HttpContext.Request.Headers["Referer"].ToString();
                if (!classpath.Contains(host) && !string.IsNullOrEmpty(classpath))
                {
                    context.Result = ResultMsg("The token is empty !");
                    context.HttpContext.Response.StatusCode = 403;
                    return;
                }
                context.HttpContext.Response.Redirect("/Login/Login");
                return ;
            }
            if (_redisHelper.Database.KeyExists(sessionID.Value) && _redisHelper.Database.StringGet(sessionID.Value).ToString().Equals(Token.Value))
            {
                var val = _redisHelper.Database.StringGet(sessionID.Value).ToString();
                if (val.Equals(Token.Value))
                {
                    isPass = true;
                    Guid.NewGuid().ToString("N");
                }
            }
            if (!isPass)
            {
                context.HttpContext.Response.Redirect("/Login/Login");
                return;
            }
            base.OnActionExecuting(context);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
        private JsonResult ResultMsg(string msg)
        {
            return new JsonResult(msg);
        }
    }
}
