using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Models
{
    /// <summary>
    /// 通用返回 模型 结构
    /// </summary>
    public class ResultModel
    {
        [JsonProperty("status_code")]
        public virtual int StatusCode { get; set; }
        [JsonProperty("msg")]
        public virtual string Message { get; set; }
        [JsonProperty("data")]
        public virtual object Data { get; set; }
        [JsonProperty("count")]
        public virtual int Count { get; set; }
        [JsonProperty("page_size")]
        public virtual int PageSize { get; set; }
    }
    /// <summary>
    /// 首页视图模型
    /// </summary>
    public class RedisViewModel
    {
        public virtual List<RedisKeyModel> Tags { get; set; }
        public virtual List<RedisKeyModel> Cats { get; set; }
        public virtual int? TopCount { get; set; }
        public virtual string LastTime { get; set; }
    }
}
