using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Redmanmale.ExpressionTreeCopy
{
    public static class DataExtensions
    {
        /// <summary>
        /// Медленное копирование через reflection
        /// </summary>
        public static TTargetClass Cast1<TSourceInterface, TTargetClass>(this TSourceInterface srcObj) where TTargetClass : class, TSourceInterface, new()
        {
            if (srcObj == null)
            {
                throw new ArgumentNullException(nameof(srcObj));
            }

            var resultObj = srcObj as TTargetClass;
            if (resultObj == null)
            {
                resultObj = new TTargetClass();

                var srcObjProps = typeof(TSourceInterface).GetProperties();
                var resultObjProps = typeof(TTargetClass).GetProperties().Where(p => p.CanWrite);

                foreach (var prop in resultObjProps)
                {
                    var srcObjValue = srcObjProps.First(p => p.Name.Equals(prop.Name)).GetValue(srcObj);
                    prop.SetValue(resultObj, srcObjValue);
                }
            }
            return resultObj;
        }

        private static class DelegateHolder<TTargetClass, TSourceInterface> where TTargetClass : class, TSourceInterface, new()
        {
            public static readonly Func<TSourceInterface, TTargetClass> InternalCast = CreateInternalCast();

            private static Func<TSourceInterface, TTargetClass> CreateInternalCast()
            {
                var srcObjProps = typeof(TSourceInterface).GetProperties();
                var resultObjProps = typeof(TTargetClass).GetProperties();

                var sourceParam = Expression.Parameter(typeof(TSourceInterface));
                var resultParam = Expression.Variable(typeof(TTargetClass));

                var body = new List<Expression>(srcObjProps.Length + 2)
                {
                    Expression.Assign(resultParam, Expression.New(resultParam.Type)),
                };

                var propNames = from srcInfo in srcObjProps
                                join res in resultObjProps on srcInfo.Name equals res.Name
                                where srcInfo.CanRead && res.CanWrite
                                select res.Name;

                foreach (var propName in propNames)
                {
                    body.Add(Expression.Assign(Expression.Property(resultParam, propName), Expression.Property(sourceParam, propName)));
                }

                body.Add(resultParam);

                var block = Expression.Block(new[] { resultParam }, body);
                var lambda = Expression.Lambda(block, sourceParam);
                var compiledLambda = (Func<TSourceInterface, TTargetClass>)lambda.Compile();

                return compiledLambda;
            }
        }

        /// <summary>
        /// Быстрое копирование через expression Tree
        /// </summary>
        public static TTargetClass Cast2<TSourceInterface, TTargetClass>(this TSourceInterface srcObj) where TTargetClass : class, TSourceInterface, new()
        {
            if (srcObj == null)
            {
                throw new ArgumentNullException(nameof(srcObj));
            }

            return srcObj as TTargetClass ?? DelegateHolder<TTargetClass, TSourceInterface>.InternalCast(srcObj);
        }
    }
}
