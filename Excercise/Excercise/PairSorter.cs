using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    /// <summary>
    /// Представляет клсасс сортировщика с использованием <see cref="PairArray{T}"/>
    /// </summary>
    public class PairSorter : Singleton<PairSorter>
    {
        /// <summary>
        /// Получает список действий, использованных при сортировке
        /// </summary>
        public List<string> Log { get; private set; } = new List<string>();

        /// <summary>
        /// Сортирует заданный массив с использованием <see cref="PairArray{T}"/>
        /// </summary>
        /// <remarks>
        /// При невозможности сортировки возвращает заданный массив.
        /// Во время работы записывает совершенные действия в свойство <see cref="Log"/>.
        /// </remarks>
        /// <param name="array"></param>
        /// <returns></returns>
        public int[] Sort(int[] array)
        {
            Log.Clear();
            Debug.Print("Start sorting");
            var pArray = new PairArray<int>(array);

            // Если массив не сортируется, пишем об этом информацию в лог и возвращаем массив
            if (!IsSortable(array))
            {
                Log.Add($"Нельзя отсортировать массив {pArray}");
                return pArray.Values;
            }

            // Функция-замыкание, для использования PairArray.SwapPairs
            // с последующей записью действия в лог
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
            SortV1(pArray, swapPairs);

            Log.Add(pArray.ToString());
            Debug.Print("Stop sorting");
            return array;
        }

        /// <summary>
        /// Проверяет массив на сортируемость
        /// </summary>
        /// <remarks>
        /// Поскольку мы можем менять местами только пары соседних элементов, за один шаг
        /// мы можем изменить только четное количество инверсий
        /// т.к. в отсортированном массиве количество инверсий равно 0, если в массиве
        /// количество инверсий нечетное, то отсортировать его парами нельзя
        /// </remarks>
        /// <param name="array"></param>
        /// <returns></returns>
        public bool IsSortable(int[] array)
        {
            var inversionsCount = 0;
            for (var i = 0; i < array.Count() - 1; i++)
            {
                for (var j = i + 1; j <= array.Count() - 1; j++)
                    if (array[i] > array[j]) inversionsCount++;
            }
            return inversionsCount % 2 == 0;
        }

        private void SortV1(PairArray<int> pArray, Action<int, int> swapPairs)
        {
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
        }
    }
}
