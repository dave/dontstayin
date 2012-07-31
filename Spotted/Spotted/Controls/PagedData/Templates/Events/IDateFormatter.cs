using System;

namespace Spotted.Controls.PagedData.Templates.Events
{
	public interface IDateFormatter
	{
		string FormatDate(DateTime dt);
	}

	class CambroFriendlyDateFormatter : IDateFormatter
	{
		public string FormatDate(DateTime dt)
		{
			return dt.ToShortDateString();
		}
	}
}
