using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlerTask
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Table("system_config")]
    public class SystemConfig
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("Desc")]
        public string Desc { get; set; }
        [Column("Key")]
        public string Key { get; private set; }
        [Column("Value")]
        public string Value { get; private set; }
        [Column("DateLine")]
        public int DateLine { get; set; }
        [Column("DataType")]
        public string DataType { get; set; }
        [NotMapped]
        public string DateLineText { get; set; }
    }

}
