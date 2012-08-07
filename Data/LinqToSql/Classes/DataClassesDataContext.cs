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
