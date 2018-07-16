using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excercise;

namespace Excersice.Test
{
    [TestFixture(new int[] { 5, 4, 1, 6, 3, 2 }, TypeArgs = new Type[] { typeof(int) })]
    public class PairArrayTest<T>
    {
        public PairArray<T> TestArray;

        public PairArrayTest(T[] array)
        {
            TestArray = new PairArray<T>(array);
        }

        [Test]
        public void NotIntersectTest()
        {
            Assert.IsFalse(TestArray.CheckIntersect(0, 2));
            Assert.IsFalse(TestArray.CheckIntersect(2, 0));

        }

        [Test]
        public void IntersectTest()
        {
            Assert.IsTrue(TestArray.CheckIntersect(0, 1));
            Assert.IsTrue(TestArray.CheckIntersect(0, 0));
            Assert.IsTrue(TestArray.CheckIntersect(1, 0));
            Assert.IsTrue(TestArray.CheckIntersect(0, 0));
            Assert.IsTrue(TestArray.CheckIntersect(1, 1));
            Assert.IsTrue(TestArray.CheckIntersect(2, 2));
        }

        [Test]
        public void PairCheckOkTest()
        {
            Assert.IsTrue(TestArray.CheckPair(0));
            Assert.IsTrue(TestArray.CheckPair(4));
        }

        [Test]
        public void PairCheckFailTest()
        {
            Assert.IsFalse(TestArray.CheckPair(-1));
            Assert.IsFalse(TestArray.CheckPair(5));
        }
    }
}
