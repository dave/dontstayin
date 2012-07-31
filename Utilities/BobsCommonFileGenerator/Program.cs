using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Automation.Sql;
using System.Linq;
namespace BobsCommonFileGenerator
{


    static class Program
    {
		const string DatabaseChangeCounterBobs = "DatabaseChangeCounter(Bobs)";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
			Console.WriteLine("Test 1");

			if (args.Length == 0) {
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new Form1());
			} else {
				string connectionString = Common.Properties.ConnectionString;
				Database database = new Database(connectionString);
				database.CreateDatabaseIfDoesNotExist();
				ClassGenerator cg = new ClassGenerator(connectionString);
				if (GetArgValue<string>("/common:", args, "-1") != "-1")
				{
					if (args.Contains(@"/f") || database.ExtendedProperties["GenerateCommonVersion"] != database.ExtendedProperties[DatabaseChangeCounterBobs])
					{
						WriteFile(cg.GetPartialClassesFromDatabase(), GetArgValue<string>("/common:", args, null));
						database.ExtendedProperties["GenerateCommonVersion"] = database.ExtendedProperties[DatabaseChangeCounterBobs];
					}
				}
				if (GetArgValue<string>("/triggers:", args, "-1") != "-1")
				{
					if (args.Contains(@"/f") || database.ExtendedProperties["GenerateTriggersVersion"] != database.ExtendedProperties[DatabaseChangeCounterBobs])
					{
						Predicate<Table> tableFilter = t => t.Columns.Find(c => c.Name == "K") != null && t.HasSinglePrimaryKey;
						WriteFile(cg.GetTemplateOutput("Triggers", tableFilter), GetArgValue<string>("/triggers:", args, null));
						database.ExtendedProperties["GenerateTriggersVersion"] = database.ExtendedProperties[DatabaseChangeCounterBobs];
					}
				}
				if (GetArgValue<string>("/template:", args, "-1") != "-1")
				{
					string templateName = GetArgValue<string>("/template:", args, null);
					if (args.Contains(@"/f") || database.ExtendedProperties["GenerateTemplate_" + templateName + "_Version"] != database.ExtendedProperties[DatabaseChangeCounterBobs])
					{
						string outputPath = GetArgValue<string>("/output:", args, templateName + ".cs");
						bool hasColumnKRequirement = GetArgValue<bool>("/HasKColumn:", args, false);
						bool hasSinglePrimaryKRequirement = GetArgValue<bool>("/HasSinglePrimaryKey:", args, false);
						Predicate<Table> tableFilter = t => 
							(!hasColumnKRequirement || t.Columns.Find(c => c.Name == "K") != null )&&
							(!hasSinglePrimaryKRequirement || t.HasSinglePrimaryKey);
						WriteFile(cg.GetTemplateOutput(templateName, tableFilter), outputPath);
						database.ExtendedProperties["GenerateTemplate_" + templateName + "_Version"] = database.ExtendedProperties[DatabaseChangeCounterBobs];
					}
				}
			}
        }
		static void WriteFile(string text, string path)
		{
			if (System.IO.File.Exists(path))
			{
				System.IO.File.SetAttributes(path, System.IO.FileAttributes.Normal);
			}
			System.IO.File.WriteAllText(path, text);
		}
		static T GetArgValue<T>(string prefix, string[] args, T defaultValue) where T: IConvertible{
			foreach(string arg in args){
				if (arg.Length > prefix.Length && arg.Substring(0, prefix.Length).ToLower() == prefix.ToLower()){
					return (T) Convert.ChangeType(arg.Substring(prefix.Length), typeof(T));
				}
			}
			return defaultValue;
		}
    }
}
