using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    public class BaseModel
    {
        [NotMapped]
        public virtual int Count { get; set; }
        [NotMapped]
        public virtual int Page { get; set; }
        [NotMapped]
        public virtual int PageSize { get; set; }
    }
}
