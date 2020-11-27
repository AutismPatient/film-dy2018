using AngleSharp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Threading;
using AngleSharp.Io;
using Google.Protobuf.WellKnownTypes;

namespace CrawlerTask
{
    /// <summary>
    /// 爬虫任务执行方法
    /// </summary>
    public class CrawlerTask
    {
        private static ConfigHelper configHelper = new ConfigHelper();
        private static Logger<CrawlerTask> Logger = new Logger<CrawlerTask>();
        private static MySqlConnection MySQL = new MySQLHelper().MySql;
        private IEnumerable<SystemConfig> Config;
        public CrawlerTask()
        {
            Config = MySQL.Query<SystemConfig>($"SELECT * FROM System_Config");
        }
        /// <summary>
        /// 阳光电影采集任务
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="pageSzie"></param>
        /// <param name="isNext">是否检测跳过采集</param>
        /// <returns></returns>
        public async Task<Model.RetModel> Dy2018Task(string menu="",int pageSzie = 0,bool isNext = false)
        {
            //配置AngleSharp库
            DefaultHttpRequester defaultHttp = new DefaultHttpRequester()
            {
                Timeout = TimeSpan.FromSeconds(6),
            };
            var config = Configuration.Default.With(defaultHttp).WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //获取爬取根地址
            var addr = configHelper.GetDefaultConfigurationValue();

            var doc = await context.OpenAsync(addr);
            if(doc?.StatusCode != HttpStatusCode.OK || doc == null)
            {
                Logger.Log.Error($"[{addr}]:http 请求失败，请重试");
                return await Dy2018Task();
            }
            int number = 0;
            var cont = doc?.QuerySelectorAll(".contain > ul > li > a");
            Logger.Log.Debug($"{DateTime.Now.ToLongDateString()}[{doc.Title}]采集任务开始");
            var Models = new Model.RetModel();
            float count = 0;
            int offset = 0;
            menu = Config.FirstOrDefault(x=>x.Key == "menu")?.Value;
            isNext = bool.Parse(Config.FirstOrDefault(x => x.Key == "IsNext")?.Value ?? string.Empty);
            foreach (var k in cont)
            {
                if (offset > 10)
                {
                    offset = 0;
                    continue;
                }
                if (menu != "全部")
                {
                    if(menu!= k.InnerHtml)
                    {
                        continue;
                    }
                }
                var href = addr.Replace("index.html","") + k.GetAttribute("href");
                if (!href.Contains("index"))
                {
                    continue;
                }
                doc = await context.OpenAsync(href);
                if(doc.StatusCode != HttpStatusCode.OK)
                {
                    continue;
                }
                var nodes = doc.QuerySelectorAll("select[name='sldd'] > option");
                if (nodes.Length <= 0)
                {
                    continue;
                }
                //获取分页的url
                List<string> hrefs = new List<string>();
                foreach (var n in nodes)
                {
                    var childrenHref = href.Replace("index.html", n.GetAttribute("value"));
                    hrefs.Add(childrenHref);
                }
                //进入分页列表 获取电影详情链接
                List<string> movieUrls = new List<string>();
                foreach (var h in hrefs)
                {
                    count++;
                    if (offset > 10)
                    {
                        break;
                    }
                    doc = await context.OpenAsync(h);
                    if(doc.StatusCode != HttpStatusCode.OK)
                    {
                        continue;
                    }
                    nodes = doc.QuerySelectorAll(".co_content8 > ul > table > tbody > tr > td > b > a:last-child");
                    if (nodes.Length <= 0)
                    {
                        continue;
                    }
                    foreach (var n in nodes)
                    {
                        if(offset > 10)
                        {
                            break;
                        }
                        var movieUrl = addr.Replace("index.html","") + n?.GetAttribute("href").TrimStart('/');
                        if(Config.FirstOrDefault(x=>x.Key == "console").Value == "true")
                        {
                            Console.WriteLine(movieUrl);
                        }
                        try
                        {
                            //检测重复项
                            var exist = await MySQL.QueryFirstOrDefaultAsync<Model.MovieInfo>($"SELECT MovieLink FROM crawler_movie WHERE MovieLink='{movieUrl}' OR Title='{n?.InnerHtml}'", null);
                            if (exist != null)
                            {
                                if (isNext)
                                {
                                    offset++;
                                }
                                continue;
                            }
                            doc = await context.OpenAsync(movieUrl);

                            #region 过滤
                            if (doc.StatusCode != HttpStatusCode.OK)
                            {
                                continue;
                            }
                            var Textnode = doc.QuerySelector("#Zoom > span");
                            var download = Textnode?.QuerySelectorAll("a");
                            var img = Textnode?.QuerySelectorAll("img");
                            var title = doc.QuerySelector(".title_all > h1 > font");
                            var content = Textnode?.TextContent.Replace("　", "");
                            #endregion
                            if (string.IsNullOrEmpty(content))
                            {
                                continue;
                            }
                            #region 分段获取电影详情

                            string awards = content.GetValueByName("获奖情况", isEnd: false);
                            string synopsis = "";
                            if (string.IsNullOrEmpty(awards))
                            {
                                synopsis = content.GetValueByName("简介", isEnd: false);
                            }
                            else
                            {
                                synopsis = content.GetValueByName("简介");
                            }
                            var link = new List<string>();
                            foreach (var l in download)
                            {
                                string hlt = l?.GetAttribute("href");
                                string[] arry = { "https://www.ygdy8.com/", "", "http://www.ygdy8.com/", " ", "/js/thunder.htm", "https://www.ygdy8.net/" };
                                for (int i = 0; i < arry.Length; i++)
                                {
                                    if (arry[i] == hlt)
                                    {
                                        hlt = string.Empty;
                                    }
                                }
                                if (!string.IsNullOrEmpty(hlt))
                                {
                                    link.Add(hlt);
                                }
                            }
                            var imgs = new List<string>();
                            string firstImg = "";
                            string twoImg = "";

                            for (int i = 0; i < img?.Length; i++)
                            {
                                if (i == 0)
                                {
                                    firstImg = img[0].GetAttribute("src");
                                }
                                if (i == 1)
                                {
                                    twoImg = img[1].GetAttribute("src");
                                }
                            }

                            var movie = new Model.MovieInfo
                            {
                                Title = title?.TextContent,
                                AlternateName = content.GetValueByName("译名"),
                                Age = content.GetValueByName("年代"),
                                Subtitle = content.GetValueByName("字幕"),
                                Origin = content.GetValueByName("产地", "国家"),
                                Category = content.GetValueByName("类别"),
                                DouBanGrade = content.GetValueByName("豆瓣评分"),
                                VideoSize = content.GetValueByName("视频尺寸"),
                                FileSize = content.GetValueByName("文件大小"),
                                FilmLength = content.GetValueByName("片长"),
                                Scriptwriter = content.GetValueByName("编剧"),
                                Language = content.GetValueByName("语言"),
                                ReleaseTime = content.GetValueByName("上映日期"),
                                IMDb = content.GetValueByName("IMDb评分", "IMDB评分"),
                                Episodes = content.GetValueByName("集数"),
                                Director = content.GetValueByName("导演"),
                                Protagonist = content.GetValueByName("主演"),
                                Tags = content.GetValueByName("标签"),
                                TV = content.GetValueByName("电视台"),
                                Debuted = content.GetValueByName("首播"),
                                Synopsis = synopsis,
                                Awards = awards,
                                DownloadLink = link,
                                SurfaceUrl = firstImg,
                                Screenshot = twoImg,
                                MovieLink = movieUrl,
                            };
                            #endregion
                            #region 添加数据到mysql
                            if (movie != null)
                            {
                                //Models.MovieInfos.Add(movie);
                                var down = movie.DownloadLink.ToArray();
                                if (movie.DownloadLink.Count <= 0)
                                {
                                    continue;
                                }
                                string sql = @"INSERT crawler_movie SET" +
                                    $" Title='{movie.Title}',ReleaseTime='{movie.ReleaseTime}',Age='{movie.Age}',AlternateName='{movie.AlternateName}',SurfaceUrl='{movie.SurfaceUrl}',Origin='{movie.Origin}',Category='{movie.Category}',Language='{movie.Language}',Subtitle='{movie.Subtitle}'," +
                                    $"DouBanGrade='{movie.DouBanGrade}',VideoSize='{movie.VideoSize}',FileSize='{movie.FileSize}',FilmLength='{movie.FilmLength}',Director='{movie.Director}',Scriptwriter='{movie.Scriptwriter}',Protagonist='{movie.Protagonist}',Tags='{movie.Tags}',Synopsis='{movie.Synopsis}',DownloadLink='{string.Join(",", down)}'," +
                                    $"IMDb='{movie.IMDb}',Awards='{movie.Awards}',Screenshot='{movie.Screenshot}',DateLine={GetTimeStamp()},Menu='{k.InnerHtml}',Episodes='{movie.Episodes}',Debuted='{movie.Debuted}',TV='{movie.TV}',MovieLink='{movie.MovieLink}'";

                                var rof = await MySQL.ExecuteAsync(sql);
                                if (rof > 0)
                                {
                                    number += rof;
                                }
                                Console.Write($"[{movie.Title}]入库成功！");
                            }
                            #endregion
                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}在{ex.StackTrace}");
                            continue;
                        }
                    }
                }
            }
            if (number >= 0)
            {
                Models.Message = $"任务消息：采集数据 {number} 行入库成功！";
                return Models;
            }
            return new Model.RetModel() {
                Message = "数据集为空,请重新过滤条件",
            };
        }
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        private long GetTimeStamp()
        {
            long ts = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            return ts;
        }
    }
}
