using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Models
{
    /// <summary>
    /// 电影列表 参数
    /// </summary>
    public class MovieParameter
    {
        private int size = 12;
        private int index = 1;
        private string m = "";
        private string c = "";
        private string t = "";
        /// <summary>
        /// 当前页
        /// </summary>
        public virtual int page
        {
            get { return index; }
            set
            {
                if (value <= 0)
                {
                    index = 1;
                }
                else
                {
                    index = value;
                }
            }
        }
        /// <summary>
        /// 页码
        /// </summary>
        public virtual int page_size
        {
            get { return size; }
            set
            {
                if (value > 30)
                {
                    size = 30;
                }
                else
                {
                    size = value;
                }
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public virtual string title 
        {
            get { return t; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                else
                {
                    t = value;
                }
            }
        } 
        /// <summary>
        /// 排序字段 默认为上映时间
        /// </summary>
        public virtual int order { get; set; } = 1;
        /// <summary>
        /// 正序或倒序 默认为倒序
        /// </summary>
        public virtual int sort { get; set; } = 1;
        /// <summary>
        /// 菜单
        /// </summary>
        public virtual string menu
        {
            get { return m; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                else
                {
                    m = value;
                }
            }
        }
        /// <summary>
        /// 影片类型
        /// </summary>
        public virtual string category 
        {
            get { return c; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                else
                {
                    c = value;
                }
            }
        }
    }
}
