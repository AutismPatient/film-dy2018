using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerTask
{
    public class Model
    {
        public class RetModel
        {
            public virtual string Message { get; set; }
            public virtual List<MovieInfo> MovieInfos { get; set; }
        }
        public class MovieInfo
        {
            /// <summary>
            /// 标题
            /// </summary>
            public virtual string Title { get; set; }
            /// <summary>
            /// 上映时间
            /// </summary>
            public virtual string ReleaseTime { get; set; }
            /// <summary>
            /// 年代
            /// </summary>
            public virtual string Age { get; set; }
            /// <summary>
            /// 译名
            /// </summary>
            public virtual string AlternateName { get; set; }
            /// <summary>
            /// 封面图片地址
            /// </summary>
            public virtual string SurfaceUrl { get; set; }
            /// <summary>
            /// 产地
            /// </summary>
            public virtual string Origin { get; set; }
            /// <summary>
            /// 类别
            /// </summary>
            public virtual string Category { get; set; }
            /// <summary>
            /// 语言
            /// </summary>
            public virtual string Language { get; set; }
            /// <summary>
            /// 字幕
            /// </summary>
            public virtual string Subtitle { get; set; }
            /// <summary>
            /// 豆瓣评分
            /// </summary>
            public virtual string DouBanGrade { get; set; }
            /// <summary>
            /// 视频尺寸
            /// </summary>
            public virtual string VideoSize { get; set; }
            /// <summary>
            /// 文件大小
            /// </summary>
            public virtual string FileSize { get; set; }
            /// <summary>
            /// 片长
            /// </summary>
            public virtual string FilmLength { get; set; }
            /// <summary>
            /// 导演
            /// </summary>
            public virtual string Director { get; set; }
            /// <summary>
            /// 编剧
            /// </summary>
            public virtual string Scriptwriter { get; set; }
            /// <summary>
            /// 主演
            /// </summary>
            public virtual string Protagonist { get; set; }
            /// <summary>
            /// 标签
            /// </summary>
            public virtual string Tags { get; set; }
            /// <summary>
            /// 简介
            /// </summary>
            public virtual string Synopsis { get; set; }
            /// <summary>
            /// 下载链接
            /// </summary>
            public virtual List<string> DownloadLink { get; set; }
            /// <summary>
            /// 影片截图
            /// </summary>
            public virtual string Screenshot { get; set; }
            /// <summary>
            /// IMDb评分
            /// </summary>
            public virtual string IMDb { get; set; }
            /// <summary>
            /// 获奖情况
            /// </summary>
            public virtual string Awards { get; set; }
            /// <summary>
            /// 集数（电视剧）
            /// </summary>
            public virtual string Episodes { get; set; }
            /// <summary>
            /// 首播
            /// </summary>
            public virtual string Debuted { get; set; }
            /// <summary>
            /// 电视台
            /// </summary>
            public virtual string TV { get; set; }
            public virtual string MovieLink { get; set; }
        }
        
    }
}
