using System.Linq;
using System.Collections.Generic;
using ImpromptuInterface;
using Shouldly;
using StructureMap.Configuration.DSL.Expressions;
using StructureMap.Pipeline;

namespace StructureMapExample.Tests
{
    public static class Extensions
    {
        public static void ShouldBeSameCollectionAs<T>(
            this IEnumerable<T> enumerable,
            IEnumerable<T> expected)
        {
            var input = enumerable.ToList();
            var compare = expected.ToList();
            for (int i = 0; i < input.Count; i++)
            {
                input[i].ShouldBe(compare[i]);
            }
        }

        public static LambdaInstance<TInterface, TInterface> WrapWithLogger<TInterface, TInstance>(this CreatePluginFamilyExpression<TInterface> expression, TInstance instance = null)
            where TInstance : class, TInterface
            where TInterface : class
        {
            return expression.Use(con => 
                new LogWrapper<TInstance>(instance ?? con.GetInstance<TInstance>())
                .ActLike<TInterface>());
        }
    }
}