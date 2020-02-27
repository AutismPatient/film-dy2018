using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Macrocosm.Filter;
using Macrocosm.Models;
using Macrocosm.Service;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Macrocosm.Controllers
{
    [Route("common_api/[controller]")]
    [ServiceFilter(typeof(CommonFilter))]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly RedisHelper redis;
        private readonly SqlContext Context;
        public MovieController(IMovieService movieService, RedisHelper _redis, SqlContext _context)
        {
            _movieService = movieService;
            redis = _redis;
            Context = _context;
        }
        [Route("movies")]
        public async Task<IActionResult> Index(MovieParameter parameter)
        {
            var data = await _movieService.GetMoviesAsync(parameter);
            return Json(new ResultModel
            {
                Data = data,
                StatusCode = 200
            });
        }
        [Route("detail")]
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
            {
                return Json(new ResultModel
                {
                    Data = null,
                    StatusCode = 500
                });
            }
            var data = await _movieService.ViewAsync(id);
            return Json(new ResultModel
            {
                Data = data,
                StatusCode = 200
            });
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password,string terms)
        {
            try
            {
                var user = await _movieService.LoginVerification(username, password);
                var exp = TimeSpan.FromDays(1);
                if (terms == "on") // 记住密码 7天
                {
                    exp = TimeSpan.FromDays(7);
                }
                var opt = new CookieOptions()
                {
                    Path = "/",
                    Expires = DateTime.Now.ToLocalTime().Add(exp),
                    IsEssential = true
                };
                if (user != null)
                {
                    var reload = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultSessionName);
                    if(reload.Key != null)
                    {
                        if(redis.Database.KeyExists(reload.Value))
                        {
                            return Json(new ResultModel
                            {
                                Message = $"您已经登录过，请勿重复登录",
                                StatusCode = 405
                            });
                        }
                    }
                    string sessionVal = Guid.NewGuid().ToString("N");
                    string tokenVal = Guid.NewGuid().ToString("N");
                    HttpContext.Response.Cookies.Append(Const.DefaultTokenName, tokenVal, opt);
                    HttpContext.Response.Cookies.Append(Const.DefaultSessionName, sessionVal, opt);
                    await redis.Database.StringSetAsync(sessionVal, tokenVal, exp);
                    HttpContext.Response.Cookies.Append(Const.DefaultMemberName, user.Id.ToString(), opt);
                    return Json(new ResultModel
                    {
                        Message = $"登录成功,欢迎回来 {username}",
                        StatusCode = 200
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Json(new ResultModel
                {
                    Message = "系统发生异常，登录失败",
                    StatusCode = 403
                });
            }
            return Json(new ResultModel
            {
                Message = "用户名或密码错误！请重试",
                StatusCode = 403
            });
        }
        
        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [TypeFilter(typeof(IgonreGlobalActionFilter))]
        [ServiceFilter(typeof(SystemFiltercs))]
        [Route("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var member = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultMemberName);
            if (string.IsNullOrEmpty(member.Value))
            {
                return Json(new ResultModel
                {
                    Message = "获取用户信息失败",
                    StatusCode = 500
                });
            }
            var user = await Context.System_Users.FirstOrDefaultAsync(x=>x.Id == int.Parse(member.Value));
            if(user == null)
            {
                return Json(new ResultModel
                {
                    Message = "获取用户信息失败",
                    StatusCode = 200
                });
            }
            user.PassWord = "";
            return Json(new ResultModel
            {
                Message = "succeed",
                Data = user,
                StatusCode = 200
            });
        }
    }
}