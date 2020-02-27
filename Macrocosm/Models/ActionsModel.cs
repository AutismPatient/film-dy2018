using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 作为对象参数 类
/// </summary>
namespace Macrocosm.Models
{
    public class AdditiveAdminModel
    {
        public virtual string UserName { get; set; }
        public virtual string Nick { get; set; }
        public virtual string PassWord { get; set; }
        public virtual int Age { get; set; }
    }
    public class UserInfo
    {
        public virtual string UserName { get; set; }
        public virtual string Nick { get; set; }
        public virtual int Id { get; set; }
    }
    public class WorkbenchInfo
    {
        public virtual int AdminCount { get; set; }
        public virtual int Video { get; set; }
        public virtual int Day7 { get; set; }
        public virtual int IPCount { get; set; }
    }
    public class AddConfigModel
    {
        public virtual string Desc { get; set; } // 描述
        public virtual string Key { get; set; }
        public virtual string Value { get; set; }
        public virtual string DataType { get; set; }
    }
}
