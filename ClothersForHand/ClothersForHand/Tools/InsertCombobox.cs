using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothersForHand.Tools
{
	class InsertCombobox
	{
		public static List<T> CreatedCombobox<T> (List<T> source, T item)
		{
			var result = source.ToList();
			result.Insert(0, item);

			return result;
		}
	}
}
