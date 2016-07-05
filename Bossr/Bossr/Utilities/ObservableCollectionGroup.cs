using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bossr.Utilities
{
    public class ObservableGroupCollection<TKey, T> : ObservableCollection<T>
    {
        public TKey Key { get; }

        public ObservableGroupCollection(IGrouping<TKey, T> group)
            : base(group)
        {
            Key = group.Key;
        }
    }
}
