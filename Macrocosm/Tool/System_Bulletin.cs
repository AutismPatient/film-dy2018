using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 公告
    /// </summary>
    [Table("system_bulletin")]
    public class System_Bulletin
    {
        [Column("Id")]
        public virtual int Id { get; set; }
        [Column("Title")]
        public virtual string Title { get; set; }
        [Column("Content")]
        public virtual string Content { get; set; }
        [Column("DateLine")]
        public virtual int DateLine { get; set; }
        [Column("Author")]
        public virtual string Author { get; set; }
        [Column("Uid")]
        public virtual int Uid { get; set; }
    }
}
