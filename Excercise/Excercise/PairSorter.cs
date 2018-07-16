using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    public class PairSorter : Singleton<PairSorter>
    {
        public List<string> Log { get; private set; } = new List<string>();

        public PairSorter()
        {
            Debug.Print("PairSorter created");
        }
            
        public int[] Sort(int[] array)
        {
            var pArray = new PairArray<int>(array);

            void swapPairs(int i1, int i2)
            {
                var buf = pArray.ToString();
                var pair1 = pArray.GetPair(i1);
                var pair2 = pArray.GetPair(i2);

                pArray.SwapPairs(i1, i2);
                
                var msg =
                    string.Join(
                        " ",
                        buf,
                        pair1.Item1.ToString() + pair1.Item2.ToString(),
                        "<->",
                        pair2.Item1.ToString() + pair2.Item2.ToString()
                    );
                Debug.Print(msg);
                Log.Add(msg);

            }

            var numbers = pArray.ValuesList;
            numbers.Sort();

            var index = 0;
            while (!pArray.ValuesList.SequenceEqual(numbers))
            {
                if (
                    (pArray.Values[index] > pArray.Values[index + 2]) ||
                    (pArray.Values[index + 1] > pArray.Values[index + 3]) ||
                    (pArray.Values[index + 1] > pArray.Values[index + 2])
                   )
                    swapPairs(index, index + 2);
                if (index < (pArray.ValuesList.Count - 4))
                    index++;
                else
                    index = 0;
            }


            var s = string.Join("; ", from item in pArray.Values
                                      select item.ToString());

            Debug.Print($"{s}");

            Log.Add(pArray.ToString());

            return array;
        }

    }
}
