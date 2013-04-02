using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Training.MinerCore;

namespace MinerCoreTests
{
    public class RangeTest
    {
        private const int TestFrom = 10;
        private const int TestTo = 100;
        private const int TestCount = 10;

        [Test]
        public void TestInterval()
        {
            var result = Range.GetCombination(TestFrom, TestTo, TestCount).ToList();
            Assert.AreEqual(TestCount, result.Count);
            Assert.That(result.All(el => TestFrom <= el && el < TestTo));
        }

        [Test]
        [ExpectedException(typeof(RangeException))]
        public void TestEmptyInterval()
        {
            Range.GetCombination(new List<int>(), TestCount);
        }

        [Test]
        public void TestNegativeInterval()
        {
            var result = Range.GetCombination(-TestTo, -TestFrom, 10);
            // TODO: Add test code
        }

        [Test]
        public void TestCollection()
        {
            var coll = new int[100];
            for (var i = 0; i < coll.Length; ++i)
            {
                coll[i] = i;
            }
            var result = Range.GetCombination(coll, 10);
            // TODO: Add test code
        }

        [Test]
        public void TestCollectionWithSameElements()
        {
            var coll = new int[100];
            for (var i = 0; i < coll.Length; ++i)
            {
                coll[i] = 1;
            }
            var result = Range.GetCombination(coll, 10);
            // TODO: Add test code
        }

        [Test]
        public void TestZeroRange()
        {
            var result = Range.GetCombination(TestFrom, TestTo, 0);
            // TODO: Add test code
        }
    }
}
