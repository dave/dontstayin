using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Model.Entities;
using LinqToSql.Classes;

namespace LinqToSql.Repositories
{
	public class Themes : ModelToLinqTable<Model.Entities.Theme, Classes.Theme>, Model.Repositories.IThemes
	{
		public Themes(Table<Classes.Theme> table) : base(table){}
	}
	public class Events : ModelToLinqTable<Model.Entities.Event, Classes.Event>, Model.Repositories.IEvents
	{
		public Events(Table<Classes.Event> table) : base(table){}
	}
	public class Venues : ModelToLinqTable<Model.Entities.Venue, Classes.Venue>, Model.Repositories.IVenues
	{
		public Venues(Table<Classes.Venue> table): base(table){}
	}

}
