using System.Linq;
using System.Collections.Generic;
using Shouldly;

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
    }
}