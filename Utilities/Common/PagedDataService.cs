using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
	/// <summary>
	/// Transforms one type of paged data service into another
	/// </summary>
	/// <typeparam name="T">The type of dataservice required</typeparam>
	/// <typeparam name="U">The type of the one you've got</typeparam>
	public class PagedDataService<T, U> : IPagedDataService<T>
	{
		IPagedDataService<U> myPagedDataService;
		Converter<U, T> converter;

		public PagedDataService(IPagedDataService<U> myPagedDataService, Converter<U, T> converter)
		{
			this.myPagedDataService = myPagedDataService;
			this.converter = converter;
		}
		public int Count { get { return myPagedDataService.Count; } }
		public T[] Page(int pageNumber, int pageSize)
		{
			return myPagedDataService.Page(pageNumber, pageSize).ConvertAll(converter);
		}
	}
	public static class IPagedDataServiceExtensions
	{
		public static IPagedDataService<Output> Convert<Input, Output>(this IPagedDataService<Input> input, Converter<Input, Output> converter)
		{
			return new PagedDataService<Output, Input>(input, converter);
		}
	}
}
