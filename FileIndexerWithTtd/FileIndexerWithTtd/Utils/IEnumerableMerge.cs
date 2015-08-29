using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileIndexerWithTtd.Utils
{
    static class IEnumerableMerge
    {
        static IEnumerable<T> Merge<T>(this IEnumerable<IEnumerable<T>> collection)
        {
            List<T> result = new List<T>();
            foreach(var element in collection) {
                result.AddRange(element);
            }
            return result.AsEnumerable();
        }
    }
}
