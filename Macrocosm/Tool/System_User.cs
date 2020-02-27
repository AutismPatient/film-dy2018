using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 管理员
    /// </summary>
    [Table("system_user")]
    public class System_User
    {
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("UserName")]
        public virtual string UserName { get; set; }
        [Column("PassWord")]
        public virtual string PassWord { get; set; }
        [Column("Age")]
        public virtual int Age { get; set; }
        [Column("IsPass")]
        public virtual int IsPass { get; set; }
        [Column("NickName")]
        public virtual string NickName { get; set; }
        [Column("LastTime")]
        public virtual long LastTime { get; set; }
        [Column("Register")]
        public virtual long Register { get; set; }
        [Column("UserGrade")]
        public virtual int UserGrade { get; set; } // 用户等级：1:普通管理员，2:超级管理员
        [Column("IsDelete")]
        public virtual int IsDelete { get; set; }
    }
}
