using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LinqToDbmlRunner;
using LinqToSqlSchema;
using schemas.microsoft.com.linqtosql.dbml.Item2007;

namespace LinqToSqlDbmlScript
{
	public class DbmlScript : LinqToDbmlScript
	{

		public override void Apply(XRoot root)
		{
			root.Table("ClubDetails").Type.Name = "ClubDetails";
			root.Table("Demographics").Type.Name = "Demographics";
			root.Table("DomainStats").Type.Name = "DomainStats";
			root.Table("Prefs").Type.Name = "Prefs";
			root.Table("OutgoingSms").Type.Name = "OutgoingSms";

			this.SetNotNullColumns(root);
			this.SetNotNullColumnsForIsNotNullProperty(root);
			this.OverrideTypesWithEnumProperties(root);

			//this.SetFunctionResultColumnsToNotNull(root, "dbo.fHtmCoverCircleLatLon");
			this.SetFunctionResultColumnsToNotNull(root, "dbo.fHtmCoverRegion");
			//root.Database.Function.Remove(root.Database.Function.Where(f => f.Name == "dbo.fDistanceLatLon").First());
			Add2WayAssociation(root, "Venue", "Event", null);
			Add2WayAssociation(root, "Theme", "Group", null);


			root.Table("Event").Type.Column("HtmId").CanBeNull = false;

			root.Table("Venue").Type.Column("HtmId").CanBeNull = false;
			root.Table("Place").Type.Column("HtmId").CanBeNull = false;


			
			root.Database.ContextNamespace = "LinqToSql.Classes";
			//root.Database.EntityBase = "LinqToSql.Classes";
			root.Database.EntityNamespace = "LinqToSql.Classes";
			//LinqToSqlClassGenerator.BuildLinqToSqlClasses(root);

			root.Table("Ticket").Type.Column("CustomXml").Type = "System.String";
			root.Table("TicketRun").Type.Column("CustomXml").Type = "System.String";

			root.Table("Log").Type.Column("Item").Name = "ItemType";
			root.Table("Usr").Type.Column("UpdateError").Type = "System.Boolean";

			this.RemoveUnwantedColumns(root);
			this.RemoveUnwantedTables(root);


			//root.Table("Theme").Type.Association.Add(new Association()
			//                                            {
			//                                                Name = "Theme_Groups",
			//                                                Member = "Groups",
			//                                                ThisKey = "K",
			//                                                OtherKey = "ThemeK",
			//                                                Type = "Group",
			//                                                DeleteRule = "NO ACTION"
			//                                            });


			//root.Table("Group").Type.Association.Add(new Association()
			//                                            {
			//                                                Name = "Group_Theme",
			//                                                Member = "Theme",
			//                                                ThisKey = "ThemeK",
			//                                                OtherKey = "K",
			//                                                Type = "Theme",
			//                                                //IsForeignKey = true

			//                                            }
				//);
		}

		private void Add2WayAssociation(XRoot root, string parent, string child, string parentMember)
		{
			root.Table(child).Type.Association.Add(new Association()
			                                       	{
			                                       		Name = parent + "_" + child,
			                                       		Member = parent,
			                                       		ThisKey = parent + "K",
			                                       		Type = parent,
			                                       		IsForeignKey = true
			                                       	});

			root.Table(parent).Type.Association.Add(new Association()
			                                        	{
			                                        		Name = parent + "_" + child,
															Member = parentMember ?? (child + "s"),
			                                        		OtherKey = parent + "K",
			                                        		Type = child
			                                        	});
		}

		private void RemoveUnwantedColumns(XRoot root)
		{
			using (var dc = new LinqToSqlSchema.SqlSchemaDataContext(Common.Properties.ConnectionString))
			{
				var columnsJoinedToExtendedProperties =
					from t in dc.tables
					join c in dc.columns on t.object_id equals c.object_id
					join ep in dc.extended_properties on c.object_id equals ep.major_id
					where ep.name == "MS_Description" && ep.minor_id == c.column_id && (string) (ep.value ?? "") != ""
					select new {ColumnName = t.name + "." + c.name};

				var columnsWithoutExtendedProperties =
					from t in dc.tables
					join c in dc.columns on t.object_id equals c.object_id
					where !columnsJoinedToExtendedProperties.Any(x => x.ColumnName == t.name + "." + c.name)
					select new {TableName = t.name, ColumnName = c.name};

				foreach (var column in columnsWithoutExtendedProperties)
				{
					root.Table(column.TableName).Type.Column.Remove(
						root.Table(column.TableName).Type.Column(column.ColumnName));
				}
			}



			//using (var dc = new LinqToSqlSchema.SqlSchemaDataContext(Common.Properties.ConnectionString))
			//{
			//    var columns =
			//        (from t in dc.tables
			//         join c in dc.columns on t.object_id equals c.object_id
			//         join ep in dc.extended_properties on c.object_id equals ep.major_id
			//         where ep.name == "MS_Description" && ep.minor_id == c.column_id && (string)(ep.value ?? "") == ""
			//         select new { TableName = t.name, ColumnName = c.name }).ToArray();

			//    
			//}
		}


		private void SetNotNullColumnsForIsNotNullProperty(XRoot root)
		{
			using (var dc = new LinqToSqlSchema.SqlSchemaDataContext(Common.Properties.ConnectionString))
			{
				var notNullColumns =
					(from t in dc.tables
					 join c in dc.columns on t.object_id equals c.object_id
					 join ep in dc.extended_properties on c.object_id equals ep.major_id
					 where ep.name == "IsNotNull" && ep.minor_id == c.column_id && (string)(ep.value ?? "") == "true"
					 select new { TableName = t.name, ColumnName = c.name }).ToArray();

				foreach (var notNullColumn in notNullColumns)
				{
					root.Table(notNullColumn.TableName).Type.Column(notNullColumn.ColumnName).CanBeNull = false;
				}
			}
		}

		private void RemoveUnwantedTables(XRoot root)
		{
			List<string> tablesWithoutMsExtendedProperties = null;
			using (var dc = new LinqToSqlSchema.SqlSchemaDataContext(Common.Properties.ConnectionString))
			{
					var tablesJoinedToExtendedProperties =
						from t in dc.tables
						join ep in dc.extended_properties on t.object_id equals ep.major_id
						where ep.name == "MS_Description" && ep.minor_id == 0 && (string)(ep.value ?? "") != ""
						select t.name;

					var tablesWithoutExtendedProperties =
						from t in dc.tables
						where !tablesJoinedToExtendedProperties.Contains(t.name)
						select t.name;

				tablesWithoutMsExtendedProperties = tablesWithoutExtendedProperties.ToList();
			}

			foreach (string tableName in tablesWithoutMsExtendedProperties)
			{
				root.Database.Table.Remove(root.Table(tableName));
			}
		}

		private void OverrideTypesWithEnumProperties(XRoot root)
		{
			using (var dc = new LinqToSqlSchema.SqlSchemaDataContext(Common.Properties.ConnectionString))
			{
				var enumProps =
					(from t in dc.tables
					 join c in dc.columns on t.object_id equals c.object_id
					 join ep in dc.extended_properties on c.object_id equals ep.major_id
					 where ep.name == "EnumProperty" && ep.minor_id == c.column_id && (string)(ep.value ?? "") != ""
					 select new { TableName = t.name, ColumnName = c.name, EnumProperty = (string)ep.value }).ToArray();

				foreach (var enumProp in enumProps)
				{
					root.Table(enumProp.TableName).Type.Column(enumProp.ColumnName).Type = enumProp.EnumProperty;
				}
			}
		}

		private void SetFunctionResultColumnsToNotNull(XRoot root, string functionName)
		{
			var function = root.Database.Function.Where(f => f.Name == functionName).First();
			foreach (var et in function.ElementType)
			{
				foreach(var c in et.Column)
				{
					c.CanBeNull = false;
				}
			}
		}

		private void SetNotNullColumns(XRoot root)
		{
			var notNullRecords = File.ReadAllLines("NotNullableColumns.csv").Select(s => s.Split(',')).Select(a => new {Table = a[0], Column = a[1]});
			foreach (var table in root.Database.Table)
			{
				foreach (var column in table.Type.Column)
				{
					if ((from notNullRecord in notNullRecords 
					     where notNullRecord.Table == table.Name.Substring(4) && notNullRecord.Column == column.Name 
					     select notNullRecord).Any())
					{
						column.CanBeNull = false;
					}
				}
			}
		}

    }
}

