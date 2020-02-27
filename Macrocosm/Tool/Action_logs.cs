using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 操作日志
    /// </summary>
    [Table("action_logs")]
    public class Action_logs: BaseModel
    {
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Path")]
        public virtual string Path { get; set; }
        [Column("DateLine")]
        public virtual long DateLine { get; set; }
        [Column("ClientIP")]
        public virtual string ClientIP { get; set; }
        [Column("Status")]
        public virtual int Status { get; set; }
    }
}
