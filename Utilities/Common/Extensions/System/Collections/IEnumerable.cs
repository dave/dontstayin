using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
	public static class IEnumerableExtensions
	{
	 
		public static IEnumerable<T> Flatten<T>(this IEnumerable<IEnumerable<T>> enumerables)
		{
			foreach (var enumerable in enumerables)
			{
				foreach (var item in enumerable)
				{
					yield return item;
				}
			}
		}
		public static IEnumerable<T> CombineWith<T>(this T main, params T[] ts)
		{
		    yield return main;
		    foreach (var item in ts)
		    {
		        yield return item;
		    }
		}


		public static IEnumerable<T> Distinct<T>(this IEqualityComparer<T> comparer, IEnumerable<T> items)
		{
			List<T> itemsThatHaveAlreadyLeft = new List<T>();
			foreach (var item in items)
			{
				if (!itemsThatHaveAlreadyLeft.Exists(s => comparer.Equals(s, item)))
				{
					itemsThatHaveAlreadyLeft.Add(item);
					yield return item;
				}
			}
		}
		public static IEnumerable<T> Transform<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (T o in enumerable)
			{
				action(o);
				yield return o;
			}
		}
		public static int? FindFirstIndex<U>(this IEnumerable<U> enumerable, Predicate<U> pred)
		{
			int i = 0;
			foreach (U item in enumerable)
			{
				if (pred(item)) { return i ; }
				i++;
			}
			return null;
		}
		public static List<int> FindAllIndexes<U>(this IEnumerable<U> enumerable, Predicate<U> pred)
		{
			List<int> indexes = new List<int>();
			int i = 0;
			foreach (U item in enumerable)
			{
				if (pred(item)) { indexes.Add(i); }
				i++;
			}
			return indexes;
		}
		//public static IEnumerable<T> ConvertAll<T, U>(this IEnumerable<U> enumerable, Converter<U, T> converter)
		//{
		//    foreach (U o in enumerable)
		//    {
		//        yield return converter(o);
		//    }
		//}
		//public static IEnumerable<T> ConvertAll<T>(this IEnumerable enumerable, Converter<object, T> converter)
		//{
		//    foreach (object o in enumerable)
		//    {
		//        yield return converter(o);
		//    }
		//}
		//public static List<T> ToList<T>(this IEnumerable<T> enumerable)
		//{
		//    return new List<T>(enumerable);
		//}
		public static string Join(this IEnumerable<string> enumerable, string separator)
		{
			StringBuilder sb = new StringBuilder();
			using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					sb.Append(enumerator.Current);
				}
				while (enumerator.MoveNext())
				{
					sb.Append(separator);
					sb.Append(enumerator.Current);
				}
			}
			return sb.ToString();
			
			
		}
	}
}
