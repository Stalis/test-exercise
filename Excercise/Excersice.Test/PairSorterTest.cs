using Excercise;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise.Test
{
    [TestFixture]
    class PairSorterTest
    {

        [Test]
        [Timeout(1000)]
        [TestCaseSource(typeof(PairSorterCasesFactory), "TestCases")]
        public void MainTest(int[] array, bool sortable)
        {
            if (sortable)
                Assert.AreEqual(
                    new int[] { 1, 2, 3, 4, 5, 6 },
                    PairSorter6.GetInstance().Sort(array),
                    string.Join("\n", PairSorter6.GetInstance().Log)
                );
            else
                Assert.AreEqual(
                    array,
                    PairSorter6.GetInstance().Sort(array),
                    string.Join("\n", PairSorter6.GetInstance().Log)
                    );
        }
    }

    public class PairSorterCasesFactory
    {
        public static IEnumerable<TestCaseData> TestCases { 
            get {
                yield return new TestCaseData(new int[] { 6, 5, 4, 3, 2, 1 }, false);
                yield return new TestCaseData(new int[] { 2, 6, 4, 1, 3, 5 }, false);
                yield return new TestCaseData(new int[] { 1, 2, 3, 4, 5, 6 }, true);
                yield return new TestCaseData(new int[] { 5, 4, 1, 6, 3, 2 }, true);
                yield return new TestCaseData(new int[] { 1, 2, 4, 6, 5, 3 }, true);
                yield return new TestCaseData(new int[] { 6, 5, 4, 3, 1, 2 }, true);
            }
        }
    }
}
