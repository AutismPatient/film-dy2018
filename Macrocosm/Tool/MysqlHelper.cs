using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Macrocosm.Tool
{
    /// <summary>
    /// mysql 仓储类 通用操作方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MysqlHelper<T>: IRepository<T> where T:class
    {
        private readonly SqlContext mysql;
        public MysqlHelper(SqlContext _mysql)
        {
            mysql = _mysql;
        }
        public MysqlHelper() { }
        public async Task<T> AddAsync(T entity)
        {
            try
            {
                var sub = await mysql.AddAsync(entity);
                await Transaction();
                if (sub.Entity is T)
                {
                    return (T)sub.Entity;
                }
                return (T)Convert.ChangeType(sub.Entity, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }
        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                var sub = mysql.Update(entity);
                await Transaction();
                if (sub.Entity is T)
                {
                    return (T)sub.Entity;
                }
                return (T)Convert.ChangeType(sub.Entity, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(T entity)
        {
            try
            {
                var sub = mysql.Remove(entity);
                await Transaction();
                if (sub.Entity is T)
                {
                    return (T)sub.Entity;
                }
                return (T)Convert.ChangeType(sub.Entity, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }
        ///
        public async Task SaveAsync()
        {
            await Transaction();
        }
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public async Task Transaction()
        {
            using (var transaction = await mysql.Database.BeginTransactionAsync())
            {
                try
                {
                   await mysql.SaveChangesAsync();
                   await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                await transaction.DisposeAsync();
            }
        }
    }
}
