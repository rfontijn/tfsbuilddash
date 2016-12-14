using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsBuildMonitor.Core
{

    public static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            // Order by and put into a list.
            List<T> sorted = collection.OrderBy(x => x).ToList();

            // Loop the list and exchange items in the collection.
            for (int i = sorted.Count() - 1; i >= 0; i--)
            {
                collection.Insert(0, sorted[i]);
                collection.RemoveAt(collection.Count - 1);
            }
        }

    }
}
