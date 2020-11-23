using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CrawlerTask
{
    /// <summary>
    /// String 帮助类
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// 根据电影属性获取相应值 DY2018
        /// </summary>
        /// <param name="str"></param>
        /// <param name="field"></param>
        /// <param name="moral"></param>
        /// <param name="isEnd"></param>
        /// <returns></returns>
        public static string GetValueByName(this string str, string field,string moral = "", bool isEnd = true)
        {
            try
            {
                if (!string.IsNullOrEmpty(field))
                {
                    #region 过滤
                    str = Regex.Replace(str, @"\s", "");
                    int index = str.IndexOf("【下载地址】", StringComparison.Ordinal);
                    if (index >= 0)
                    {
                        str = str.Substring(0, index).Trim();
                    }
                    str = str.Replace("'", " ").Replace("：", "");
                    string filter = "◎";
                    char ch = '◎';
                    if ((str.Length - str.Replace("【","").Length) > (str.Length - str.Replace(filter,"").Length))
                    {
                        filter = "【";
                        ch = '【';
                    }
                    #endregion
                    
                    index = str.IndexOf(filter + field, StringComparison.Ordinal);
                    if (index <= 0)
                    {
                        if (!string.IsNullOrEmpty(moral))
                        {
                            field = moral;
                            index = str.IndexOf(filter + field, StringComparison.Ordinal);
                        }
                        if(index<0)
                        {
                            return "-";
                        }
                    }
                    string val = str.Substring(index).Replace("】", "").TrimStart(ch);
                    index = val.IndexOf(field, StringComparison.Ordinal);
                    if (index >= 0)
                    {
                        if (isEnd)
                        {
                            int lastIndex = val.IndexOf(filter, StringComparison.Ordinal);
                            if ( lastIndex < 0)
                            {
                                lastIndex = val.IndexOf("简介", StringComparison.Ordinal);
                            }
                            val = val.Substring(index, lastIndex < 0 ? index:lastIndex).Replace(field, "");
                        }
                        else
                        {
                            val = val.Substring(index).Replace(field, "");
                        }
                    }
                    val = val.Replace(":", "");
                    val = string.IsNullOrEmpty(val) ? "-" : val;
                    return val;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return "-";
        }
    }
}
