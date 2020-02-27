using Macrocosm.Filter;
using Macrocosm.Models;
using Macrocosm.Tool;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Service
{
    [ServiceFilter(typeof(CommonFilter))]
    public class SystemService: ISystemService
    {
        public readonly SqlContext Context;
        private readonly RedisHelper _redis;
        private readonly IRepository<System_User> userRepository;
        private readonly IRepository<System_Config> configRepository;
        private readonly IRepository<Action_logs> logRepository;
        public SystemService(SqlContext context, RedisHelper redis,IRepository<System_User> _userRepository, IRepository<System_Config> _configRepository, IRepository<Action_logs> _logRepository)
        {
            Context = context;
            _redis = redis;
            userRepository = _userRepository;
            configRepository = _configRepository;
            logRepository = _logRepository;
        }
        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="additive"></param>
        /// <returns></returns>
        public async Task<bool> Additive(string adminId,AdditiveAdminModel additive)
        {
            try
            {
                if (!await ExistAsync(adminId))
                {
                    return false;
                }
                var now = DateTime.Now.GetTimeStamp();
                var user = new System_User
                {
                    UserName = additive.UserName,
                    Age = additive.Age,
                    NickName = additive.Nick,
                    PassWord = additive.PassWord.GenerateMD5(),
                    Register = now,
                    UserGrade = 1,
                    LastTime = now,
                    IsPass = 1,
                    IsDelete = 0,
                };
                user = await userRepository.AddAsync(user);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 封禁管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Forbidden(string adminId,string userId)
        {
            try
            {
                if (!await ExistAsync(adminId))
                {
                    return false;
                }
                var user = await Context.System_Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
                if(user == null)
                {
                    return false;
                }
                user.IsPass = 2;
                user = await userRepository.UpdateAsync(user);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 封禁管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> UnForbidden(string adminId, string userId)
        {
            try
            {
                if (!await ExistAsync(adminId))
                {
                    return false;
                }
                var user = await Context.System_Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
                if (user == null)
                {
                    return false;
                }
                user.IsPass = 1;
                user = await userRepository.UpdateAsync(user);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string adminId, string userId)
        {
            try
            {
                if (!await ExistAsync(adminId))
                {
                    return false;
                }
                var user = await Context.System_Users.FirstOrDefaultAsync(x => x.Id == int.Parse(userId));
                if (user == null)
                {
                    return false;
                }
                user.IsDelete = 1;
                user = await userRepository.DeleteAsync(user);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 判断是否超级管理员
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        private async Task<bool> ExistAsync(string adminId)
        {
            var exist = await Context.System_Users.AnyAsync(x => x.Id == int.Parse(adminId) && x.UserGrade == 2);
            return exist;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassWord(string adminId,string password)
        {
            try
            {
                var user = await Context.System_Users.FirstOrDefaultAsync(x => x.Id == int.Parse(adminId));
                if (user == null)
                {
                    return false;
                }
                user.PassWord = password.GenerateMD5();
                user = await userRepository.UpdateAsync(user);
                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 工作台
        /// </summary>
        /// <returns></returns>
        public async Task<WorkbenchInfo> Workbench()
        {
            var info = new WorkbenchInfo() { };
            info.AdminCount = await Context.System_Users.CountAsync();
            info.Video = await Context.Movies.CountAsync();
            info.IPCount = await Context.Action_Logs.CountAsync();
            var offset = DateTime.Now.AddDays(-7).GetTimeStamp();
            info.Day7 = await Context.Movies.CountAsync(x => x.DateLine >= Convert.ToInt32(offset));
            return info;
        }
        /// <summary>
        /// 工作台（近一个月发布）
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<List<Movie>> WorkMovies(int page)
        {
            var offset = Convert.ToInt32(DateTime.Now.AddMonths(-1).GetTimeStamp());
            var list = await Context.Movies.Where(x => x.DateLine >= offset).OrderByDescending(x => x.ReleaseTime).Skip(page).Take(60).ToListAsync();
            return list;
        } 
        /// <summary>
        /// 管理员列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <returns></returns>
        public async Task<List<System_User>> GetUsersAsync(int page,int page_size)
        {
            page_size = 10;
            var list = await Context.System_Users.Where(x => x.IsDelete != 1).Skip((page-1)* page_size).Take(page_size).ToListAsync();
            return list;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetUsersCountAsync()
        {
            var count = await Context.System_Users.CountAsync(x => x.IsDelete != 1);
            return count;
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<System_User> UserDetailAsync(int id)
        {
            var user = await Context.System_Users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }
        /// <summary>
        /// 缓存列表
        /// </summary>
        /// <returns></returns>
        public async Task<RedisListModel> RedisListsAsync(int page = 1, int page_size = 10)
        {
            int offset = (page - 1) * page_size;
            var keys = _redis.GetAllKeys(offset, page_size);
            List<RedisKeyModel> list = new List<RedisKeyModel>();
            foreach(var k in keys)
            {
                try
                {
                    string val = await _redis.Database.StringGetAsync(k);
                    list.Add(new RedisKeyModel()
                    {
                        Id = k,
                        Value = val,
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[{ex.StackTrace}]->{ex.Message}");
                    continue;
                }
            }
            int count = list.Count;
            list = list.Skip(offset).Take(page_size).ToList();
            return new RedisListModel() {
                RedisKeys = list,
                Count = count,
            };
        }
        /// <summary>
        /// 删除redis 键
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> DeleteKeyAsync(string key)
        {
            try
            {
                var ok = await _redis.Database.KeyDeleteAsync(key);
                return ok;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 配置列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<System_Config>> Settings()
        {
            var configs = await Context.System_Configs.ToListAsync();
            return configs;
        }
        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> AddConfigAsync(AddConfigModel model)
        {
            try
            {
                var now = DateTime.Now.GetTimeStamp();
                var config = new System_Config
                {
                    Key = model.Key,
                    Value = model.Value,
                    DateLine = Convert.ToInt32(now),
                    Desc = model.Desc,
                    DataType = model.DataType,
                };
                config = await configRepository.AddAsync(config);
                return config != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 修改配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditConfigAsync(string id,string val)
        {
            try
            {
                var now = DateTime.Now.GetTimeStamp();
                var config = await Context.System_Configs.FirstOrDefaultAsync(x => x.Id == int.Parse(id));
                if(config == null)
                {
                    return false;
                }
                config.Value = val;
                config = await configRepository.UpdateAsync(config);
                return config != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteConfigAsync(string Id)
        {
            try
            {
                var config = await Context.System_Configs.FirstOrDefaultAsync(x => x.Id == int.Parse(Id));
                if(config == null)
                {
                    return false;
                }
                config = await configRepository.DeleteAsync(config);
                return config != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
        /// <summary>
        /// 返回日志列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public async Task<List<Action_logs>> Action_LogsAsync(int page=1,int page_size=10)
        {
            var list = await Context.Action_Logs.OrderByDescending(x => x.DateLine).Skip((page - 1) * page_size).Take(page_size).ToListAsync();
            return list;
        }
        public async Task<int> LogCountAsync()
        {
            var count = await Context.Action_Logs.CountAsync();
            return count;
        }
        public async Task<bool> DeleteLogAsync(string id)
        {
            try
            {
                var model = await Context.Action_Logs.FirstOrDefaultAsync(x => x.Id == int.Parse(id));
                if (model == null)
                {
                    return false;
                }
                model = await logRepository.DeleteAsync(model);
                return model != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now.ToLocalTime().ToString()}]{ex.Message}");
                return false;
            }
        }
    }
}
