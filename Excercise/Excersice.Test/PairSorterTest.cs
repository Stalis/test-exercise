using Excercise;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excersice.Test
{
    [TestFixture(new int[] { 1, 2, 3, 4, 5, 6 })]
    [TestFixture(new int[] { 6, 5, 4, 3, 2, 1 })]
    [TestFixture(new int[] { 5, 4, 1, 6, 3, 2 })]
    class PairSorterTest
    {
        public int[] TestArray;

        public PairSorterTest(int[] array)
        {
            TestArray = array;
        }

        [Test]
        public void MainTest()
        {
            Assert.AreEqual(
                new int[] { 1, 2, 3, 4, 5, 6 },
                PairSorter.GetInstance().Sort(TestArray)
                );
        }
    }
}
