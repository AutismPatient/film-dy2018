using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Filter;
using Macrocosm.Models;
using Macrocosm.Service;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Mvc;

namespace Macrocosm.Controllers
{
    /// <summary>
    /// 后台操作控制器
    /// </summary>
    [ServiceFilter(typeof(SystemFiltercs))]
    [ServiceFilter(typeof(CommonFilter))]
    public class SystemController : Controller
    {
        private readonly ISystemService service;
        private readonly RedisHelper redis;
        public SystemController(ISystemService _service,RedisHelper _redis)
        {
            service = _service;
            redis = _redis;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 工作台 电影
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<IActionResult> Movies(int page)
        {
            var list = await service.WorkMovies(page);
            return Json(new ResultModel
            {
                Data = list,
                StatusCode = 200
            });
        }
        /// <summary>
        /// 工作台统计
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Count()
        {
            var data = await service.Workbench();
            return Json(new ResultModel
            {
                Data = data,
                StatusCode = 200
            });
        }
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<IActionResult> AdminList(int page=1,int page_size=10)
        {
            var data = await service.GetUsersAsync(page, page_size);
            var count = await service.GetUsersCountAsync();
            return View(new ResultModel
            {
                Data = data,
                StatusCode = 200,
                Count = page,
                PageSize = count,
            });
        }
        public async Task<IActionResult> AdminDetail(int id)
        {
            var data = await service.UserDetailAsync(id);
            return Json(new ResultModel
            {
                Data = data,
                StatusCode = 200
            });
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await ClearCaches();
            return RedirectToAction("index", "system");
        }
        private async Task ClearCaches()
        {
            HttpContext.Response.Cookies.Delete(Const.DefaultSessionName);
            HttpContext.Response.Cookies.Delete(Const.DefaultTokenName);
            HttpContext.Response.Cookies.Delete(Const.DefaultMemberName);
            var session = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultSessionName);
            var token = HttpContext.Request.Cookies.FirstOrDefault(x => x.Key == Const.DefaultTokenName);
            await redis.Database.KeyDeleteAsync(session.Value);
            await redis.Database.KeyDeleteAsync(token.Value);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChangePassWord(string id,string password)
        {
            var ok = await service.ChangePassWord(id, password);
            if (ok)
            {
                await ClearCaches();
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "修改密码成功，请重新登录",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        public async Task<IActionResult> AddAdmin(string adminId, AdditiveAdminModel additive)
        {
            var ok = await service.Additive(adminId, additive);
            if (ok)
            {
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "提交成功",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        /// <summary>
        /// 封禁
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Banned(string adminId, string userId)
        {
            var ok = await service.Forbidden(adminId, userId);
            if (ok)
            {
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "提交成功",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        /// <summary>
        /// 解除
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IActionResult> UnBanned(string adminId, string userId)
        {
            var ok = await service.UnForbidden(adminId, userId);
            if (ok)
            {
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "提交成功",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        /// <summary>
        /// 缓存列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<IActionResult> RedisList(int page = 1, int page_size = 10)
        {
            var data = await service.RedisListsAsync(page, page_size);
            return View(new ResultModel
            {
                Data = data.RedisKeys,
                StatusCode = 200,
                Count = page,
                PageSize = data.Count,
            });
        }
        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<IActionResult> RedisDelete(string key, int page = 1, int page_size = 10)
        {
            var ok = await service.DeleteKeyAsync(key);
            return RedirectToAction("RedisList","system",new { page, page_size });
        }
        /// <summary>
        /// 设置
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Settings()
        {
            var data = await service.Settings();
            return View(data);
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddConfig(AddConfigModel model)
        {
            switch (model.DataType)
            {
                case "int":
                    var ret = 0;
                    if (!int.TryParse(model.Value,out ret))
                    {
                        return Json(new ResultModel
                        {
                            StatusCode = 500,
                            Message = "值必须与数据类型对应.",
                        });
                    }
                    break;
                case "bool":
                    var ret1 = false;
                    if (!bool.TryParse(model.Value, out ret1))
                    {
                        return Json(new ResultModel
                        {
                            StatusCode = 500,
                            Message = "值必须与数据类型对应.",
                        });
                    }
                    break;
            }
            var ok = await service.AddConfigAsync(model);
            if (ok)
            {
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "提交成功",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditConfig(string id,string val)
        {
            var ok = await service.EditConfigAsync(id,val);
            if (ok)
            {
                return Json(new ResultModel
                {
                    StatusCode = 200,
                    Message = "提交成功",
                });
            }
            return Json(new ResultModel
            {
                StatusCode = 500,
                Message = "请求发生错误，系统错误！",
            });
        }
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<IActionResult> ActionLogs(int page = 1, int page_size = 10)
        {
            var list = await service.Action_LogsAsync(page, page_size);
            var count = await service.LogCountAsync();
            return View(new ResultModel
            {
                Data = list,
                Count = count,
                PageSize = page,
            });
        }
        public async Task<IActionResult> DeleteLogs(string id, int page = 1, int page_size = 10)
        {
            var ok = await service.DeleteLogAsync(id);
            return RedirectToAction("ActionLogs", "system", new { page, page_size });
        }
    }
}