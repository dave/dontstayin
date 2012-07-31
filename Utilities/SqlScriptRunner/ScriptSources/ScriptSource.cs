using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlScriptRunner.ScriptTypes;
namespace SqlScriptRunner.ScriptSources
{
	abstract class ScriptSource
	{
		internal const string DataScriptDir = "Data Scripts";
		internal const string UpdateScriptDir = "Update Scripts";
		internal const string CreateScriptDir = "Create Scripts";
		internal const string OnSyncScriptDir = "OnSync Scripts";

		internal const string ProcedureSubDir = "prc";
		internal const string FunctionSubDir = "udf";
		internal const string TriggerSubDir = "trg";
		internal const string ViewSubDir = "viw";



		List<ObjectScript> functionScripts = new List<ObjectScript>();
		List<ObjectScript> procedureScripts = new List<ObjectScript>();
		List<ObjectScript> viewScripts = new List<ObjectScript>();
		List<ObjectScript> triggerScripts = new List<ObjectScript>();
		SortedList<int, UpdateScript> updateScripts = new SortedList<int, UpdateScript>();
		List<DataScript> tableDataScripts = new List<DataScript>();
		List<OnSyncScript> onSyncScripts = new List<OnSyncScript>();





		internal List<ObjectScript> FunctionScripts
		{
			get
			{
				return this.functionScripts;
			}
		}

		internal List<ObjectScript> ProcedureScripts
		{
			get
			{
				return this.procedureScripts;
			}
		}

		internal List<DataScript> TableDataScripts
		{
			get
			{
				return this.tableDataScripts;
			}
		}

		internal List<ObjectScript> TriggerScripts
		{
			get
			{
				return this.triggerScripts;
			}
		}

		internal List<UpdateScript> UpdateScripts
		{
			get
			{
				return new List<UpdateScript>(this.updateScripts.Values);
			}
		}

		internal List<ObjectScript> ViewScripts
		{
			get
			{
				return this.viewScripts;
			}
		}
		internal List<OnSyncScript> OnSyncScripts
		{
			get
			{
				return this.onSyncScripts;
			}
		}

		internal void AddScript(string type, string path)
		{

			switch (type)
			{
				case ScriptType.TableData:
					this.tableDataScripts.Add(new DataScript(path)); break;
				case ScriptType.UpdateScript:
					UpdateScript updateScript = new UpdateScript(path);
					this.updateScripts.Add(updateScript.VersionNumber, updateScript); break;
				case ScriptType.Trigger:
					this.triggerScripts.Add(new ObjectScript(path, type)); break;
				case ScriptType.StoredProcedure:
					this.procedureScripts.Add(new ObjectScript(path, type)); break;
				case ScriptType.UserDefinedFunction:
					this.functionScripts.Add(new ObjectScript(path, type)); break;
				case ScriptType.View:
					this.viewScripts.Add(new ObjectScript(path, type)); break;
				case ScriptType.OnSync:
					this.onSyncScripts.Add(new OnSyncScript(path)); break;
				default: break;
			}


		}

	}
}
