using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Models
{
    /// <summary>
    /// redis 相关model
    /// </summary>
    public class RedisKeyModel
    {
        public string Id { get; set; }
        public string Value { get; set; }
    }
    public class CountModel
    {
        public int DataCount { get; set; }
        public string DateTime { get; set; }
    }
    public class RedisListModel
    {
        //public virtual int DbNum { get; set; }
        //public virtual long Expires { get; set; }
        //public virtual string Size { get; set; }
        //public virtual string Type { get; set; }
        public virtual List<RedisKeyModel> RedisKeys { get; set; }
        public virtual int Count { get; set; }
    }
}
