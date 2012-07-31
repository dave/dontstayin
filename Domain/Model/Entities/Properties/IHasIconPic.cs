using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Search;

namespace Model.Entities.Properties
{
	public interface IHasIconPic
	{
		Guid Pic { get; }
		IEnumerable<IHasIconPic> IconPicParents { get; }
	}
	public static class IHasIconPicExtensions
	{
		public static Guid AnyPic(this IHasIconPic hasIconPic)
		{
			var firstHasIconPicWithAPic = new BreadthFirstSearch<IHasIconPic>(hasIconPic, h => h.IconPicParents, h => h.Pic != Guid.Empty).Execute().FirstOrDefault();
			return firstHasIconPicWithAPic == null ? Guid.Empty : firstHasIconPicWithAPic.Pic;
		}
	}
	public interface IGetIHasIconPicParentsService<T> where T : IHasIconPic
	{
		IEnumerable<IHasIconPic> GetParents(T t);
	}
}
