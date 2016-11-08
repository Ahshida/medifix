using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Reflection;

namespace DBO.Data.Managers
{
    public static class MultiLingualManager
    {
        public static IEnumerable<TProperty> SelectInNativeMode<TSource, TProperty>(this IEnumerable<TSource> source, Expression<Func<TSource, TProperty>> exp)
        {
            if (!string.Equals("en", Thread.CurrentThread.CurrentCulture.Name))
                exp = exp.UpdateExpression();
            var func = exp.Compile();
            return source.Select(item => func(item));
        }

        public static TProperty NativeMode<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> exp)
        {
            return source.NativeMode(exp, string.Equals("en", Thread.CurrentThread.CurrentCulture.Name));
        }
        public static TProperty NativeMode<TSource, TProperty>(this TSource source, Expression<Func<TSource, TProperty>> exp, bool isEnglish)
        {
            if (!isEnglish)
                exp = exp.Update(exp.Body.UpdateExpression(), exp.Parameters);
            return exp.Compile()(source);
        }

        private static T UpdateExpression<T>(this T expression) where T : Expression
        {
            Expression result;
            if (expression is MemberExpression)
            {
                var exp = (expression as MemberExpression);
                if (exp.Expression == null)
                    return expression;
                PropertyInfo property;
                if (exp.Member.Name.Contains("English"))
                    property = exp.Expression.Type.GetProperty(exp.Member.Name.Replace("English", "Arabic"));
                else
                    property = exp.Expression.Type.GetProperty(string.Format("{0}_ar", exp.Member.Name));

                if (property == null)
                    result = expression;
                else
                    result = Expression.Property(exp.Expression, property);
            }
            else
            {
                if (expression is BinaryExpression)
                {
                    var exp = expression as BinaryExpression;
                    result = exp.Update(exp.Left.UpdateExpression(), exp.Conversion.UpdateExpression() as LambdaExpression, exp.Right.UpdateExpression());
                }
                else if (expression is BlockExpression)
                {
                    var exp = expression as BlockExpression;
                    result = exp.Update(exp.Variables, exp.Expressions.Select(item => item.UpdateExpression()));
                }
                else if (expression is ConditionalExpression)
                {
                    var exp = expression as ConditionalExpression;
                    result = exp.Update(exp.Test.UpdateExpression(), exp.IfTrue.UpdateExpression(), exp.IfFalse.UpdateExpression());
                }
                else if (expression is DynamicExpression)
                {
                    var exp = expression as DynamicExpression;
                    result = exp.Update(exp.Arguments.Select(item => item.UpdateExpression()));
                }
                else if (expression is GotoExpression)
                {
                    var exp = expression as GotoExpression;
                    result = exp.Update(exp.Target, exp.Value.UpdateExpression());
                }
                else if (expression is IndexExpression)
                {
                    var exp = expression as IndexExpression;
                    result = exp.Update(exp.Object.UpdateExpression(), exp.Arguments.Select(item => item.UpdateExpression()));
                }
                else if (expression is InvocationExpression)
                {
                    var exp = expression as InvocationExpression;
                    result = exp.Update(exp.Expression.UpdateExpression(), exp.Arguments.Select(item => item.UpdateExpression()));
                }
                else if (expression is LabelExpression)
                {
                    var exp = expression as LabelExpression;
                    result = exp.Update(exp.Target, exp.DefaultValue.UpdateExpression());
                }
                else if (expression is LambdaExpression)
                {
                    var exp = expression as LambdaExpression;
                    var types = exp.GetType().GetGenericArguments();
                    result = Expression.Lambda(exp.Body.UpdateExpression(), exp.Parameters);
                }
                else if (expression is ListInitExpression)
                {
                    var exp = expression as ListInitExpression;
                    result = exp.Update(exp.NewExpression.UpdateExpression() as NewExpression, exp.Initializers.Select(item => item.UpdateExpression()));
                }
                else if (expression is LoopExpression)
                {
                    var exp = expression as LoopExpression;
                    result = exp.Update(exp.BreakLabel, exp.ContinueLabel, exp.Body.UpdateExpression());
                }
                else if (expression is MemberExpression)
                {
                    var exp = expression as MemberExpression;
                    result = exp.Update(exp.Expression.UpdateExpression());
                }
                else if (expression is MemberInitExpression)
                {
                    var exp = expression as MemberInitExpression;
                    result = exp.Update(exp.NewExpression.UpdateExpression() as NewExpression, exp.Bindings.Select(item => item.UpdateExpression()));
                }
                else if (expression is MethodCallExpression)
                {
                    var exp = expression as MethodCallExpression;
                    result = exp.Update(exp.Object.UpdateExpression(), exp.Arguments.Select(item => item.UpdateExpression()));
                }
                else if (expression is NewArrayExpression)
                {
                    var exp = expression as NewArrayExpression;
                    result = exp.Update(exp.Expressions.Select(item => item.UpdateExpression()));
                }
                else if (expression is NewExpression)
                {
                    var exp = expression as NewExpression;
                    result = exp.Update(exp.Arguments.Select(item => item.UpdateExpression()));
                }
                else if (expression is RuntimeVariablesExpression)
                {
                    var exp = expression as RuntimeVariablesExpression;
                    result = exp.Update(exp.Variables);
                }
                else if (expression is SwitchExpression)
                {
                    var exp = expression as SwitchExpression;
                    result = exp.Update(exp.SwitchValue.UpdateExpression(), exp.Cases.Select(item => item.UpdateExpression()), exp.DefaultBody.UpdateExpression());
                }
                else if (expression is TryExpression)
                {
                    var exp = expression as TryExpression;
                    result = exp.Update(exp.Body.UpdateExpression(), exp.Handlers.Select(item => item.UpdateExpression()), exp.Finally.UpdateExpression(), exp.Fault.UpdateExpression());
                }
                else if (expression is TypeBinaryExpression)
                {
                    var exp = expression as TypeBinaryExpression;
                    result = exp.Update(exp.Expression.UpdateExpression());
                }
                else if (expression is UnaryExpression)
                {
                    var exp = expression as UnaryExpression;
                    result = exp.Update(exp.Operand.UpdateExpression());
                }
                else
                    result = expression;
            }

            return result as T;
        }
        private static SwitchCase UpdateExpression(this SwitchCase switchCase)
        {
            return switchCase.Update(switchCase.TestValues.Select(item => item.UpdateExpression()), switchCase.Body.UpdateExpression());
        }
        private static CatchBlock UpdateExpression(this CatchBlock catchBlock)
        {
            return catchBlock.Update(catchBlock.Variable, catchBlock.Filter.UpdateExpression(), catchBlock.Body.UpdateExpression());
        }
        private static ElementInit UpdateExpression(this ElementInit elementInit)
        {
            return elementInit.Update(elementInit.Arguments.Select(item => item.UpdateExpression()));
        }
        private static MemberBinding UpdateExpression(this MemberBinding memberBinding)
        {
            var memberAssignment = memberBinding as MemberAssignment;
            return memberAssignment.Update(memberAssignment.Expression.UpdateExpression());
        }
    }
}