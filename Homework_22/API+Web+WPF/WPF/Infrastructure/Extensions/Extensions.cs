using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homework_22_WPF.Models;

namespace Homework_22_WPF.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static ObservableCollection<Note> ToObservableCollection(this IEnumerable<Note> collection)
        {
            var oCollection = new ObservableCollection<Note>();

            foreach (var item in collection)
            {
                oCollection.Add(item);
            }

            return oCollection;
        }
    }
}
