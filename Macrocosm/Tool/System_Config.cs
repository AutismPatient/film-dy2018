using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Table("system_config")]
    public class System_Config
    {
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Desc")]
        public virtual string Desc { get; set; }
        [Column("Key")]
        public virtual string Key { get; set; }
        [Column("Value")]
        public virtual string Value { get; set; }
        [Column("DateLine")]
        public virtual int DateLine { get; set; }
        [Column("DataType")]
        public virtual string DataType { get; set; }
        [NotMapped]
        public virtual string DateLineText { get; set; }
    }
    
}
