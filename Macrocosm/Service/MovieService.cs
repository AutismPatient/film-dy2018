using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Macrocosm.Models;
using Macrocosm.Tool;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Macrocosm.Filter;
using System.Security.Cryptography;

namespace Macrocosm.Service
{
    [CommonFilter]
    public class MovieService:IMovieService
    {
        public readonly SqlContext Context;
        private readonly RedisHelper _redis;
        private readonly IRepository<System_User> repository;
        public MovieService(SqlContext context, RedisHelper redis, IRepository<System_User> _repository)
        {
            Context = context;
            _redis = redis;
            repository = _repository;
        }
        public async Task<List<Movie>> GetMoviesAsync(MovieParameter parameter)
        {
            var list = new List<Movie>();
            #region 筛选
            bool isDesc = true;
            string order_str = "ReleaseTime";
            if (parameter.sort != 1)
            {
                isDesc = !isDesc;
            }
            int offset = (parameter.page - 1) * parameter.page_size;
            Expression<Func<Movie, string>> expression = s => default;
            switch (parameter.order)
            {
                case 2:// 年代
                    order_str = "Age";
                    break;
                case 3://IMDB评分
                    order_str = "IMDb";
                    break;
            }
            Expression<Func<Movie, bool>> exp = x => !x.ReleaseTime.Contains(":") && ((x.Title.Contains(parameter.title) || x.AlternateName.Contains(parameter.title)) && x.Menu.Contains(parameter.menu) && x.Category.Contains(parameter.category));
            expression = expression.OrderByExp(order_str);
            list = await Context.Movies.Where(exp).Order(expression, isDesc).Skip(offset).Take(parameter.page_size).ToListAsync();
            #endregion
            return list;
        }
        /// <summary>
        /// 获取单行详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Movie> ViewAsync(int id)
        {
            var model = new Movie();
            model = await Context.Movies.FirstOrDefaultAsync(x => x.ID == id);
            return model;
        }
        /// <summary>
        /// 获取redis键值 首页
        /// </summary>
        /// <returns></returns>
        public async Task<RedisViewModel> GetRedisModelAsync()
        {
            var exist = await _redis.Database.KeyExistsAsync("tags");
            var model = new RedisViewModel();
            if (!exist)
            {
                await _redis.ListSetExp("tags", new List<RedisKeyModel>()
                {
                    new RedisKeyModel
                    {
                        Value = "恐怖"
                    },
                    new RedisKeyModel
                    {
                        Value = "惊悚"
                    },
                    new RedisKeyModel
                    {
                        Value = "动作"
                    },
                    new RedisKeyModel
                    {
                        Value = "犯罪"
                    },
                    new RedisKeyModel
                    {
                        Value = "剧情"
                    },
                    new RedisKeyModel
                    {
                        Value = "战争"
                    },
                    new RedisKeyModel
                    {
                        Value = "悬疑"
                    },
                    new RedisKeyModel
                    {
                        Value = "喜剧"
                    },
                    new RedisKeyModel
                    {
                        Value = "奇幻"
                    },
                    new RedisKeyModel
                    {
                        Value = "动画"
                    },
                    new RedisKeyModel
                    {
                        Value = "传记"
                    },
                    new RedisKeyModel
                    {
                        Value = "灾难"
                    },
                    new RedisKeyModel
                    {
                        Value = "科幻"
                    },
                    new RedisKeyModel
                    {
                        Value = "爱情"
                    },
                },TimeSpan.FromDays(6));
            }
            exist = await _redis.Database.KeyExistsAsync("cats");
            if (!exist)
            {
                var keys = Context.Movies.Distinct().Select(s => s.Menu).ToList().GroupBy(x => x).ToList();
                var cats = new List<RedisKeyModel>();
                keys.ForEach(s => {
                    cats.Add(new RedisKeyModel() { Value= s.Key });
                });
                await _redis.ListSetExp("cats", cats, TimeSpan.FromDays(6));
            }
            model.Tags = await _redis.GetStringAsync<RedisKeyModel>("tags");
            model.Cats = await _redis.GetStringAsync<RedisKeyModel>("cats");
            var any = await _redis.Database.KeyExistsAsync("count");
            if (!any)
            {
                var count = await Context.Movies.CountAsync();
                var time = await Context.Movies.OrderBy(x=>x.DateLine).LastOrDefaultAsync();
                string syt = time.DateLine.UnixToLacalTime().ToString();
                await _redis.ListSetExp("count", new List<CountModel> { new CountModel { DataCount = count, DateTime = syt }},TimeSpan.FromDays(6));
            }
            var syt_model = await _redis.GetStringAsync<CountModel>("count");
            model.TopCount = syt_model.First()?.DataCount;
            model.LastTime = syt_model.First()?.DateTime;
            return model;
        }
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<System_User> LoginVerification(string username,string password)
        {
            if(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }
            password = password.GenerateMD5();
            bool ok = await Context.System_Users.AnyAsync(x => x.UserName == username && password == x.PassWord && x.IsPass==1);
            if (ok)
            { // 更新最后登录时间
                var user = await Context.System_Users.FirstOrDefaultAsync(x => x.UserName == username && password == x.PassWord && x.IsPass == 1);
                user.LastTime = DateTime.Now.GetTimeStamp();
                user = await repository.UpdateAsync(user);
                return user;
            }
            return null;
        }
    }
}
