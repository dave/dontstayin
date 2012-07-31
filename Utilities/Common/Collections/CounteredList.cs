using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Collections
{
	[Serializable]
	public class CounteredList<T>
	{
		List<T> items = new List<T>();
		List<int> counts = new List<int>();
		int total;

		public void Add(T item)
		{
			int foundItemIndex = items.FindIndex(i => i.Equals(item));
			if (foundItemIndex < 0)
			{
				items.Add(item);
				counts.Add(1);
			}
			else
			{
				counts[foundItemIndex]++;

				while (foundItemIndex > 0 && counts[foundItemIndex] < counts[foundItemIndex - 1])
				{
					items.SwapElements(foundItemIndex, foundItemIndex - 1);
					counts.SwapElements(foundItemIndex, foundItemIndex - 1);
					foundItemIndex--;
				}
			}

			total++;
		}

		public List<T> GetTopPercentile(double percentile)
		{
			List<T> results = new List<T>();
			int index = 0;
			int target = (int)Math.Ceiling(percentile * total);

			while (target > 0)
			{
				results.Add(items[index]);
				target -= counts[index];
				index++;
			}
			return results;
		}
	}
}
