using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Macrocosm.Tool
{
    /// <summary>
    /// 用于linq查询
    /// </summary>
    public static class LinqConditionHelper
    {
        public static Expression<Func<T, bool>> Condition<T>() { return x=> true; }
        /// <summary>
        /// 或
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {
            var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.Or(exp.Body, exp_condition),exp.Parameters);
        }
        /// <summary>
        /// 与
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {
            try
            {
                var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>().AsEnumerable());
                return Expression.Lambda<Func<T, bool>>(Expression.Add(exp.Body,exp_condition), exp.Parameters);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 在第一个条件满足时：并且
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {

            var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exp.Body, exp_condition), exp.Parameters);
        }
        /// <summary>
        /// 与 ：赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> AndAssign<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {
            var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.AndAssign(exp.Body, exp_condition), exp.Parameters);
        }
        /// <summary>
        /// 或 ：在第一个条件成立时
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrElse<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {
            var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exp.Body, exp_condition), exp.Parameters);
        }
        /// <summary>
        /// 或 ：赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="exp1"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> OrAssign<T>(this Expression<Func<T, bool>> exp, Expression<Func<T, bool>> exp1)
        {
            var exp_condition = Expression.Invoke(exp1, exp.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>(Expression.OrAssign(exp.Body, exp_condition), exp.Parameters);
        }
        /// <summary>
        /// 排序 linq
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exp"></param>
        /// <param name="Field"></param>
        /// <returns></returns>
        public static Expression<Func<T, T1>> OrderByExp<T,T1>(this Expression<Func<T, T1>> exp,string Field)
        {
            var parameterExp = Expression.Parameter(typeof(T), "x");
            //结果是这样：x=>，x是变量名
            var propertyExp = Expression.Property(parameterExp, Field);

            return Expression.Lambda<Func<T, T1>>(propertyExp, parameterExp);
        }
    }
    public static class LinqOrderBy
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts">集合</param>
        /// <param name="expression">表达式</param>
        /// <param name="isDesc">是否倒序</param>
        /// <returns></returns>
        public static IQueryable<T> Order<T,T1>(this IQueryable<T> ts,Expression<Func<T,T1>> expression,bool isDesc=false)
        {
            if (isDesc)
            {
                return ts.OrderByDescending(expression);
            }
            else
            {
                return ts.OrderBy(expression);
            }
        }
    }
}
