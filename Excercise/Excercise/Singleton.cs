using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excercise
{
    public class Singleton<T>
        where T : class, new()
    {
        private static readonly Lazy<T> m_Lazy =
            new Lazy<T>(() => new T());

        public static T GetInstance() => m_Lazy.Value;
    }
}
