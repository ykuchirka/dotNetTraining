using System;
using System.Collections.Generic;

namespace Training.MinerCore
{
    public static class Range
    {
        // TODO: Write documentation for methods and fields

        private static readonly Random Generator = new Random();

        public static IEnumerable<T> GetCombination<T>(IEnumerable<T> collection, int count)
        {
            var index = 0;
            var result = new T[count];
            foreach (var el in collection)
            {
                if (index < count)
                {
                    result[index] = el;
                }
                else
                {
                    // Element is added
                    if (count > Generator.Next(index))
                    {
                        // Choose random element to be thrown out
                        var replaceIdx = Generator.Next(count);
                        result[replaceIdx] = el;
                    }
                }
                ++index;
            }
            if (index < count)
            {
                throw new RangeException("Not enough elements to create combination of desired size");
            }
            return result;
        }

        public static IEnumerable<int> GetCombination(int from, int to, int count)
        {
            return GetCombination(Iterate(from, to), count);
        }

        private static IEnumerable<int> Iterate(int from, int to)
        {
            for (var i = from; i < to; ++i)
            {
                yield return i;
            }
        } 
    }
}
