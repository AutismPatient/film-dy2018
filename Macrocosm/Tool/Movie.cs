using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// DY2018电影
    /// </summary>
    [Table("crawler_movie")]
    public class Movie
    {
        [Column("Id")]
        public int ID { get; set; }
        [Column("Title")]
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title { get; set; }
        /// <summary>
        /// 上映时间
        /// </summary>
        [Column("ReleaseTime")]
        public virtual string ReleaseTime { get; set; }
        /// <summary>
        /// 年代
        /// </summary>
        [Column("Age")]
        public virtual string Age { get; set; }
        /// <summary>
        /// 译名
        /// </summary>
        [Column("AlternateName")]
        public virtual string AlternateName { get; set; }
        /// <summary>
        /// 封面图片地址
        /// </summary>
        [Column("SurfaceUrl")]
        public virtual string SurfaceUrl { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        [Column("Origin")]
        public virtual string Origin { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Column("Category")]
        public virtual string Category { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        [Column("Language")]
        public virtual string Language { get; set; }
        /// <summary>
        /// 字幕
        /// </summary>
        [Column("Subtitle")]
        public virtual string Subtitle { get; set; }
        /// <summary>
        /// 豆瓣评分
        /// </summary>
        [Column("DouBanGrade")]
        public virtual string DouBanGrade { get; set; }
        /// <summary>
        /// 视频尺寸
        /// </summary>
        [Column("VideoSize")]
        public virtual string VideoSize { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [Column("FileSize")]
        public virtual string FileSize { get; set; }
        /// <summary>
        /// 片长
        /// </summary>
        [Column("FilmLength")]
        public virtual string FilmLength { get; set; }
        /// <summary>
        /// 导演
        /// </summary>
        [Column("Director")]
        public virtual string Director { get; set; }
        /// <summary>
        /// 编剧
        /// </summary>
        [Column("Scriptwriter")]
        public virtual string Scriptwriter { get; set; }
        /// <summary>
        /// 主演
        /// </summary>
        [Column("Protagonist")]
        public virtual string Protagonist { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [Column("Tags")]
        public virtual string Tags { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [Column("Synopsis")]
        public virtual string Synopsis { get; set; }
        /// <summary>
        /// 下载链接
        /// </summary>
        [Column("DownloadLink")]
        public virtual string DownloadLink { get; set; }
        /// <summary>
        /// 影片截图
        /// </summary>
        [Column("Screenshot")]
        public virtual string Screenshot { get; set; }
        /// <summary>
        /// IMDb评分
        /// </summary>
        [Column("IMDb")]
        public virtual string IMDb { get; set; }
        /// <summary>
        /// 获奖情况
        /// </summary>
        [Column("Awards")]
        public virtual string Awards { get; set; }
        /// <summary>
        /// 集数（电视剧）
        /// </summary>
        [Column("Episodes")]
        public virtual string Episodes { get; set; }
        /// <summary>
        /// 首播
        /// </summary>
        [Column("Debuted")]
        public virtual string Debuted { get; set; }
        /// <summary>
        /// 电视台
        /// </summary>
        [Column("TV")]
        public virtual string TV { get; set; }
        [Column("MovieLink")]
        public virtual string MovieLink { get; set; }
        [Column("Menu")]
        public virtual string Menu { get; set; }
        [Column("DateLine")]
        public virtual int DateLine { get; set; }
    }
}
