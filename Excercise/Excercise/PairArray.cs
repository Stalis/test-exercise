using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    /// <summary>
    /// Представляет массив, в котором можно управлять только парами соседних элементов
    /// </summary>
    /// <typeparam name="T">Тип элементов</typeparam>
    public class PairArray<T>
    {
        /// <summary>
        /// Получает массив с текущим положением значений
        /// </summary>
        public T[] Values { get; private set; }
        /// <summary>
        /// Получает массив с текущим положением значений
        /// </summary>
        public List<T> ValuesList => Values.ToList();

        /// <summary>
        /// Создает экземпляр <see cref="PairArray{T}"/>
        /// </summary>
        /// <param name="array">Изначальный массив</param>
        public PairArray(T[] array) {
            Values = array;
        }

        /// <summary>
        /// Возвращает пару чисел из массива
        /// </summary>
        /// <param name="index">индекс первого элемента первой пары</param>
        /// <returns>Пара чисел из массива</returns>
        public Tuple<T, T> GetPair(int index)
        {
            if (!CheckPair(index)) throw new PairNotValidException();
            return new Tuple<T, T>(Values[index], Values[index + 1]);
        }

        /// <summary>
        /// Записывает значения в указанную пару
        /// </summary>
        /// <param name="pair">пара значений</param>
        /// <param name="index">индекс первого элемента пары</param>
        public void SetPair(Tuple<T, T> pair, int index)
        {
            if (!CheckPair(index)) throw new PairNotValidException();
            Values[index] = pair.Item1;
            Values[index + 1] = pair.Item2;
        }


        /// <summary>
        /// Меняет указанные пары местами
        /// </summary>
        /// <param name="index1">индекс первого элемента первой пары</param>
        /// <param name="index2">индекс первого элемента второй пары</param>
        public void SwapPairs(int index1, int index2)
        {
            if (CheckIntersect(index1, index2)) throw new PairsIntersectException();

            var buf = GetPair(index1);
            SetPair(GetPair(index2), index1);
            SetPair(buf, index2);
        }

        /// <summary>
        /// Возвращает наличие пересечения указанных пар
        /// </summary>
        /// <param name="index1">индекс первого элемента первой пары</param>
        /// <param name="index2">индекс первого элемента второй пары</param>
        /// <returns>Наличие пересечения указанных пар</returns>
        public bool CheckIntersect(int index1, int index2) =>
            Math.Abs((index1 - index2)) <= 1;

        /// <summary>
        /// Возвращает валидность пары
        /// </summary>
        /// <param name="index">индекс первого элемента пары</param>
        /// <param name="offset">смещение пары</param>
        /// <returns></returns>
        public bool CheckPair(int index) =>
            (index < Values.Count() - 1) && (index >= 0);

        public override string ToString() =>
            string.Join(",", from item in Values
                             select item.ToString());
    }

    class PairsIntersectException : Exception
    {

    }

    class PairNotValidException : Exception {
    }
}
