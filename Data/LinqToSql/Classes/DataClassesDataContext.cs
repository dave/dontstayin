using System.Linq;
using System.Text;
using DataInterface;
using LinqToSql.Classes;
using Model;
using Model.Entities;
using System;
using LinqToSql.Repositories;
using Model.Repositories;

namespace LinqToSql.Classes
{
	public partial class DbSpottedDataContext : IDsiDataContext
	{


		IThemes IDsiDataContext.Themes
		{
			get { return new Themes(this.Themes); }
		}

		ITable<Model.Entities.Group> IDsiDataContext.Groups
		{
			get { return new ModelToLinqTable<Model.Entities.Group, Classes.Group>(this.Groups); }
		}
		ITable<Model.Entities.Thread> IDsiDataContext.Threads
		{
			get { return new ModelToLinqTable<Model.Entities.Thread, Classes.Thread>(this.Threads); }
		}
		IEvents IDsiDataContext.Events
		{
			get { return new Events(this.Events); }
		}
		ITable<Model.Entities.EventMusicType> IDsiDataContext.EventMusicTypes
		{
			get { return new ModelToLinqTable<Model.Entities.EventMusicType, EventMusicType>(this.EventMusicTypes); }
		}
		IVenues IDsiDataContext.Venues
		{
			get { return new Venues(this.Venues); }
		}

		IQueryable<Model.Entities.IFHtmCoverCircleLatLonResult> IDsiDataContext.FHtmCoverCircleLatLon(double lat, double lon, double radius)
		{
			return this.FHtmCoverCircleLatLon(lat, lon, radius).Cast<IFHtmCoverCircleLatLonResult>();
		}
		IQueryable<Model.Entities.IFHtmCoverCircleLatLonResult> IDsiDataContext.FHtmCoverCircleLatLon(double south, double west, double north, double east)
		{
			double lat = south + (north - south)/2;
			double lon = west + (east - west) / 2;
			double radius = Microsoft.Samples.SqlServer.Sql.fDistanceLatLon(north, west, south, east).Value;
			return this.FHtmCoverCircleLatLon(lat, lon, radius).Cast<IFHtmCoverCircleLatLonResult>();
		}


		IQueryable<Model.Entities.IFHtmCoverRegionResult> IDsiDataContext.FHtmCoverRect(double south, double west, double north, double east)
		{
			return this.FHtmCoverRegion(string.Format("RECT LATLON {0} {1} {2} {3}", south, west, north, east)).Cast<IFHtmCoverRegionResult>();
		}

		void IUnitOfWork.Submit()
		{
			this.SubmitChanges();
		}
		void IDisposable.Dispose()
		{
			this.Dispose();
		}

	}
}
