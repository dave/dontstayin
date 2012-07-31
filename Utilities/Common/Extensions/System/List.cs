using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public static class ListExtensions
	{
		public static IEnumerable<T> InReverseOrder<T>(this IList<T> list)
		{
			for (int i = list.Count - 1; i >= 0; i--)
			{
				yield return list[i];
			}
		}

		public static void RemoveAt<T>(this List<T> list, List<int> indicesToRemove)
		{
			List<T> listNew = new List<T>(list);
			List<int> indicesNew = new List<int>(indicesToRemove);

			indicesNew.Sort();
			foreach (int index in indicesNew.InReverseOrder())
			{
				try
				{
					listNew.RemoveAt(index);
				}
				catch (IndexOutOfRangeException ex)
				{
					throw new IndexOutOfRangeException(String.Format("LengthOfList={0},Index={1}", listNew.Count, index));
				}
			}

			list.Clear();
			list.AddRange(listNew);
		}
	}
}
