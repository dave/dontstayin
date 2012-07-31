using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Autofac.Integration.Web;
using Bobs;
using Model.Entities;
using Spotted.Controls.ClientSideRepeater;

namespace Spotted.Controls.PagedData.Templates.Events
{
	[InjectProperties]
	public partial class ItemTemplate : Template
	{


		public enum Columns
		{
			Url,
			Name,
			Venue,
			Time,
			Date,
			PicPath,
			VenueUrl
		}

	}

	public class EventTemplateDataCreator
	{
		readonly IUrlGenerator<IVenue> venueUrlGenerator;
		readonly IUrlGenerator<IEvent> eventUrlGenerator;
		readonly IDateFormatter dateFormatter;
		readonly IPicturePathGenerator picturePathGenerator;
		public EventTemplateDataCreator(IUrlGenerator<IVenue> venueUrlGenerator, IUrlGenerator<IEvent> eventUrlGenerator, IDateFormatter dateFormatter, IPicturePathGenerator picturePathGenerator)
		{
			this.venueUrlGenerator = venueUrlGenerator;
			this.picturePathGenerator = picturePathGenerator;
			this.dateFormatter = dateFormatter;
			this.eventUrlGenerator = eventUrlGenerator;
		}

		public Dictionary<string, string> GetDataItem(IEvent e, IVenue v)
		{
			return new Dictionary<string, string>()
			{
				 {ItemTemplate.Columns.Name.ToString(), e.Name},
				 {ItemTemplate.Columns.Venue.ToString(), v.Name},
				 {ItemTemplate.Columns.VenueUrl.ToString(), this.venueUrlGenerator.GetUrl(v)},
				 {ItemTemplate.Columns.Url.ToString(), this.eventUrlGenerator.GetUrl(e)},
				 {ItemTemplate.Columns.Time.ToString(), e.StartTime.ToString()},
				 {ItemTemplate.Columns.Date.ToString(), this.dateFormatter.FormatDate(e.DateTime)}
				 //{ItemTemplate.Columns.PicPath.ToString(), picturePathGenerator.Path(e.Pic)}
			};

		}
	}

	public interface IUrlGenerator<T>
	{
		string GetUrl(T item);
	}

	public class VenueUrlGenerator : IUrlGenerator<IVenue>
	{
		public string GetUrl(IVenue item)
		{
			return item.Name;
		}
	}
	public class EventUrlGenerator : IUrlGenerator<IEvent>
	{
		public string GetUrl(IEvent item)
		{
			return item.Name;
		}
	}
	public interface IPicturePathGenerator
	{
		string Path(Guid guid);
	}

	public class PicturePathGenerator : IPicturePathGenerator
	{
		public string Path(Guid guid)
		{
			return Storage.Path(guid);
		}
	}
}
