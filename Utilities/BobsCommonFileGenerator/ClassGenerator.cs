using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Common.Automation.Sql;
using System.IO;
using NVelocityTemplateEngine.Interfaces;
using NVelocityTemplateEngine;
namespace BobsCommonFileGenerator
{


	public class ClassGenerator
	{
		Database database = null;
		DatabaseExecute Dex;
		public delegate void ProgressDelegate();
		public event ProgressDelegate Progress;
		internal string connectionString;
		public ClassGenerator(string connectionString)
		{
			this.connectionString = connectionString;
			Dex = new DatabaseExecute(connectionString);
			database = new Database(new System.Data.SqlClient.SqlConnection(connectionString));
		}

 
		#region GetPartialClassesFromDatabase
		public string GetPartialClassesFromDatabase()
		{
			List<Table> tables = this.database.Tables;
			StringBuilder Code = new StringBuilder();
			#region File top
			Code.Append(@"using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using Bobs.CachedDataAccess;
namespace Bobs
{

");

			#endregion

			//String.Format("0x{0:x4}", ColumnIndex)

			Code.AppendLine(CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.Tables.vm", new Dictionary<string, object>() { { "tables", tables } }));
			Code.Append(@"
	#region TablesEnum
	public enum TablesEnum
	{");
			foreach (Table table in tables)
			{
				Code.Append(@"
		/// <summary>
		/// " + table.Description + @"
		/// </summary>
		" + table.Name + @" = " + String.Format("0x{0:x4}", tables.IndexOf(table) + 1) + @",");
				if (this.Progress != null) this.Progress();
			}
			Code.Append(@"
	}
	#endregion
");

			foreach (Table table in tables)
			{
				#region Bob Generation

				#region Bob top
				Code.Append(@"
	#region " + table.Name + @"
	/// <summary>
	/// " + table.Description + @"
	/// </summary>
	public partial class " + table.Name + @" : Model.Entities." + table.Name + @", IBob, ISerializable
	{
		public Bob Bob {get; private set;}
		public object this[object columnEnum]
		{
			get	{ return Bob[columnEnum]; }
			set	{ Bob[columnEnum] = value; }
		}
		#region IsDirty
		public bool IsDirty(object columnEnum)
		{
			return this.Bob.IsDirty(columnEnum);
		}
		public bool IsDirty()
		{
			return this.Bob.IsDirty();
		}
		#endregion
		#region Update methods
		public int Update(){
			return Update(null);
		}
		public int Update(Transaction t)
		{
			BeforeUpdate(t);
			var result = this.Bob.Update(t);
			AfterUpdate(t);
			return result;
		}
		partial void BeforeUpdate(Transaction t);
		partial void AfterUpdate(Transaction t);
		#endregion

		#region Delete methods
		public void Delete(){
			Delete(null);
		}
		public void Delete(Transaction t)
		{
			BeforeDelete(t);
			this.Bob.Delete(t);
			AfterDelete(t);
		}
		partial void BeforeDelete(Transaction t);
		partial void AfterDelete(Transaction t);
		#endregion


		#region Initialise methods 
		internal void Initialise(BobSet bs, DataRow row)
		{
			this.Bob.Initialise(bs, row);
		}
		internal void Initialise(DataRow row)
		{
			this.Bob.Initialise(row);
			
		}
		#endregion
		#region ExtraSelectElementHolder 
		public Bobs.Bob.ExtraSelectElementHolder ExtraSelectElements
		{
			get { return this.Bob.ExtraSelectElements; }
			set	{ this.Bob.ExtraSelectElements = value; }
		}
		#endregion
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Bob.GetObjectData(info, context);
		}

		");
	#endregion

				Code.AppendLine(CodeWriter.GenerateColumnsEnum(table.Name, table.Columns, tables.IndexOf(table) + 1));

				Code.AppendLine(CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.Bob.GetColumnName.vm", new Dictionary<string, object>() { { "table", table } }));
				Code.AppendLine(CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.Bob.DoesColumnCauseInvalidation.vm", new Dictionary<string, object>() { { "table", table } }));

				Code.AppendLine(table.HasSinglePrimaryKey ? CodeWriter.GenerateCommonCodeForPrimaryKey(table) : CodeWriter.GenerateCommonCodeForMultiKey(table));

				#region Bob end
				Code.Append(@"
	}
	#endregion");
				#endregion
				#endregion

				Code.AppendLine(CodeWriter.GenerateTemplate(table));
				Code.AppendLine(CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.ChildrenDefinitions.vm", new Dictionary<string, object>() { { "table", table } }));
				Code.AppendLine(CodeWriter.RunNVelocityTemplate("Templates.Bobs.Common.DataHolder.vm", new Dictionary<string, object>() { { "table", table } }));
				Code.AppendLine(CodeWriter.GenerateBobSet(table));


				#region TableDef Generation

				#region TableDef top

				Code.Append(@"	#region " + table.Name + @"TableDef
	public class " + table.Name + @"TableDef : TableDef
	{");
				#endregion

				Code.Append(CodeWriter.GenerateTableDef(table));

				Code.Append(CodeWriter.GenerateInitColumn(table.Name, table.Columns));

				Code.Append(CodeWriter.GenerateColumnData(table.Name, table.Columns));
				#region TableDef end
				Code.Append(@"
	}
	#endregion");
				#endregion

				#endregion


				Code.Append(Environment.NewLine);

				if (this.Progress != null) this.Progress();
			}
			#region generate stored procedure code
			Code.Append(Environment.NewLine);
			CodeSections.CodeSection cs = new CodeSections.CodeSection();
			CodeSections.Region storedProcRegion = new BobsCommonFileGenerator.CodeSections.Region("stored procedures");
			cs.Add(storedProcRegion);

			CodeSections.Class storedProcedures = new BobsCommonFileGenerator.CodeSections.Class("StoredProcedures");
			AddStoredProcedureDefinitionsFromDatabase(database, storedProcedures);
			storedProcRegion.Add(storedProcedures);
			foreach (string s in cs.GetCSharpSourceCode())
			{
				Code.Append("\t" + s + Environment.NewLine);
			}

			#endregion

			#region File bottom
			Code.Append(@"
}");
			#endregion


			return Code.ToString();
		}
		#endregion

		private static void AddStoredProcedureDefinitionsFromDatabase(Database database, CodeSections.Class storedProcedures)
		{
			foreach (StoredProcedure sp in database.StoredProcedures)
			{
				string[] storedProcedureNamespaceBreakdown = sp.Name.Split('.');
				CodeSections.Class classToPutMethodIn = storedProcedures;
				for (int i = 0; i < storedProcedureNamespaceBreakdown.Length - 1; i++)
				{
					string name = storedProcedureNamespaceBreakdown[i];
					classToPutMethodIn = GetChildClassFromFromParentClass(classToPutMethodIn, name);
				}
				string className = storedProcedureNamespaceBreakdown[storedProcedureNamespaceBreakdown.Length - 1];
				CodeSections.Class storedProcedureClass = new BobsCommonFileGenerator.CodeSections.Class(className);
				storedProcedureClass.Add(new CodeSections.StoredProcedureVoidCallMethod("ExecuteNonQuery", sp.Name, sp.Parameters));
				storedProcedureClass.Add(new CodeSections.StoredProcedureDataTableCallMethod("ExecuteDataTable", sp.Name, sp.Parameters));

				classToPutMethodIn.Add(storedProcedureClass);
			}
		}

		private static CodeSections.Class GetChildClassFromFromParentClass(CodeSections.Class parentClass, string childClassName)
		{
			CodeSections.Class newClass = parentClass.GetClass(childClassName);
			if (newClass == null)
			{
				newClass = new CodeSections.Class(childClassName);
				parentClass.Add(newClass);
			}
			return newClass;
		}
		internal int NumberOfTables()
		{
			return database.Tables.Count;

		}

		internal string GetTemplateOutput(string templateName, Predicate<Table> tableFilter)
		{
			
			
			


			List<Table> tables = database.Tables.FindAll(tableFilter);

			string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
			INVelocityEngine embeddedEngine = NVelocityEngineFactory.CreateNVelocityAssemblyEngine(assemblyName, true);
			IDictionary context = new Hashtable();
			context["tables"] = tables;
			return embeddedEngine.Process(context, "Templates." + templateName + ".vm");
		}
 
	}
}
