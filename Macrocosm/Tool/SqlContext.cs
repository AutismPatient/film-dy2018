using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class SqlContext:DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<System_Bulletin> System_Bulletins { get; set; }
        public DbSet<System_Config> System_Configs { get; set; }
        public DbSet<System_User> System_Users { get; set; }
        public DbSet<Action_logs> Action_Logs { get; set; }
    }
}
