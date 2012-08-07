using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Automation.Sql
{
	class DatabaseExtendedProperties  : ExtendedProperties
	{
		Database database;
		internal DatabaseExtendedProperties(Database database)
		{
			this.database = database;
		}
		public override string this[string key]
		{
			get
			{
				try
				{
					return SqlExtendedPropertyMethods.GetDatabaseLevelExtendedProperty(database.conn, key);
				}
				catch (SqlExtendedPropertyMethods.ObjectDoesNotExist)
				{
					return null;
				}
			}
			set
			{
				SqlExtendedPropertyMethods.UpdateDatabaseLevelProperty(database.conn, key, value);
			}
		}

	}
}
