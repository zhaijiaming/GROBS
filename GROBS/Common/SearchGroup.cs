using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace GROBS.Common
{
    public static class SearchGroup
    {
        /// <summary>
        /// 根据条件分页获得记录
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="ascending">是否升序</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="totalRecord">总记录数</param>
        /// <returns>记录列表</returns>
        //public List<T> GetMany(Expression<Func<T, bool>> where, string orderBy, bool ascending, int pageIndex, int pageSize, out int totalRecord)
        //{
        //    totalRecord = 0;
        //    where = where.And(u => u.Flag != (int)Flags.Delete);
        //    var list = dbset.Where(where);

        //    totalRecord = list.Count();
        //    if (totalRecord <= 0) return new List<T>();

        //    list = list.OrderBy(orderBy, ascending).Skip((pageIndex - 1) * pageSize).Take(pageSize);

        //    return list.ToList();
        //}

        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        //{
        //    Type type = typeof(T);

        //    PropertyInfo property = type.GetProperty(propertyName);
        //    if (property == null)
        //        throw new ArgumentException("propertyName", "Not Exist");

        //    ParameterExpression param = Expression.Parameter(type, "p");
        //    Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
        //    LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

        //    string methodName = ascending ? "OrderBy" : "OrderByDescending";

        //    MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

        //    return source.Provider.CreateQuery<T>(resultExp);
        //}

        ///// <summary>
        ///// 获取对应的字段名
        ///// </summary>
        ///// <typeparam name="TSource"></typeparam>
        ///// <param name="keySelector"></param>
        ///// <returns></returns>
        //public static string GetMemberName<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        //{
        //    string fieldName = null;
        //    var exp = keySelector.Body as UnaryExpression;
        //    if (exp == null)
        //    {
        //        var body = keySelector.Body as MemberExpression;
        //        fieldName = body.Member.Name;
        //    }
        //    else
        //    {
        //        fieldName = (exp.Operand as MemberExpression).Member.Name;
        //    }
        //    return fieldName;
        //}

    }
    /// <summary>
    /// 统一ParameterExpression
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
    public static class PredicateExtensionses
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }

    ///// <summary>
    ///// Queryable扩展
    ///// </summary>
    //public static class QueryableExtensions
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="queryable"></param>
    //    /// <param name="propertyName"></param>
    //    /// <returns></returns>
    //    public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
    //    {
    //        return OrderBy(queryable, propertyName, false);
    //    }
    //    /// <summary>
    //    /// OrderBy
    //    /// </summary>
    //    /// <typeparam name="T">实体</typeparam>
    //    /// <param name="queryable">条件</param>
    //    /// <param name="propertyName">属性名称</param>
    //    /// <param name="desc">是否降序</param>
    //    /// <returns></returns>
    //    public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
    //    {
    //        var param = Expression.Parameter(typeof(T));
    //        var body = Expression.Property(param, propertyName);
    //        dynamic keySelector = Expression.Lambda(body, param);
    //        return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
    //    }
    //}
}