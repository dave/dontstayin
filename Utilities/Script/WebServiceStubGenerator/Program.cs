using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.Script.Services;
using Common.CommandLine;
using System.IO;

namespace WebServiceStubGenerator
{
	class Program
	{
		//C:\source\XXX\Utilities\Script\WebServiceStubGenerator\bin\WebServiceStubGenerator.exe /a:C:\source\XXX\Spotted\Spotted\bin\Spotted.dll /o:c:\source\XXX\Spotted\SpottedScript\WebServices\Generated.cs
		static FilePathArgument assemblyPath = new FilePathArgument(new string[] { "assembly", "a" }, null, "Enter the path to the assembly containing the web services");
		static FilePathArgument outputPath = new FilePathArgument(new string[] { "output", "o" }, null, "Enter the path to the file you would like generated e.g. generated.cs");
		static void Main(string[] args)
		{
		//	try
		//	{

				Assembly assembly = Assembly.LoadFrom(assemblyPath.File.FullName);

				Type[] types;
				types = assembly.GetTypes();
				Dictionary<string, object> imports = new Dictionary<string, object>();
				
				
				Dictionary<Type, string> delegates = new Dictionary<Type, string>();

				List<string> classDefs = new List<string>();


				foreach (var type in types)
				{
					Dictionary<string, object> importsLocalFile = new Dictionary<string, object>();
					bool saveToLocalFile = false;
					string localFileName = "";

					bool hasMethods = false;
					StringBuilder sb = new StringBuilder();

					if (type.Namespace != null && type.Namespace.StartsWith("Spotted.WebServices."))
					{
						string name = type.Namespace.Substring(type.Namespace.LastIndexOf(".") + 1);
						string filename = @"C:\Source\" + Common.Properties.CurrentBranchName + @"\Spotted\Js\Desktop\" + type.Namespace.Substring(20).Replace(".", "\\") + @"\" + name + ".Controller.cs";
						localFileName = @"C:\Source\" + Common.Properties.CurrentBranchName + @"\Spotted\Js\Desktop\" + type.Namespace.Substring(20).Replace(".", "\\") + @"\" + name + ".Service.cs";

						if (File.Exists(filename))
						{
							saveToLocalFile = true;
						}
					}

					bool addedHeader = false;
					foreach (var method in type.GetMethods())
					{
						foreach (var att in method.GetCustomAttributes(true))
						{
							if (att is ScriptMethodAttribute)
							{
								ScriptMethodAttribute attribute = (ScriptMethodAttribute)att;

								if (!addedHeader)
								{
									string header = @"
namespace {typeNamespace}
{
	public class {typeName}
	{";
									string namespaceName = type.Namespace;
									if (saveToLocalFile)
										namespaceName = "Js." + namespaceName.Substring(20);
									sb.Append(header.Replace("{typeNamespace}", namespaceName).Replace("{typeName}", type.Name));
									addedHeader = true;
								}
								hasMethods = true;

								string body = @"
		public static void {methodName}({parameters}{typeName}{methodReturnTypeString}WebServiceSuccessCallback success, WebServiceFailureCallback failure, object userContext, int timeout)
		{
			Dictionary<string, object> p = new Dictionary<string, object>();
			{parameterValues}

			jQueryAjaxOptions o = WebServiceHelper.Options(
				""{methodName}"",
				""{path}.asmx"",
				p,
				failure,
				userContext,
				timeout);

			o.Success = 
				delegate(object data, string textStatus, jQueryXmlHttpRequest request)
				{
					success(({methodReturnType})((Dictionary<string, object>)data)[""d""], userContext, ""{methodName}"");
				};
			jQuery.Ajax(o);
		}
";
								body = body.Replace("{methodName}", method.Name);
								#region parameters
								StringBuilder sbParameters = new StringBuilder();
								StringBuilder sbParameterValues = new StringBuilder();
								foreach (var parameter in method.GetParameters())
								{
									if (parameter.ParameterType.Name.StartsWith("Dictionary"))
									{
										sbParameters.AppendFormat("Dictionary<object, object> {0}, ", parameter.Name);
									}
									else
									{
										sbParameters.AppendFormat("{0} {1}, ", parameter.ParameterType.Name, parameter.Name);
										if (saveToLocalFile)
											importsLocalFile[parameter.ParameterType.Namespace] = null;
										else
											imports[parameter.ParameterType.Namespace] = null;
									}
									sbParameterValues.AppendFormat((sbParameterValues.Length == 0 ? "" : "\t\t\t") + "p[\"{0}\"] = {0};\r\n", parameter.Name);
								}
								#endregion
								body = body.Replace("{parameters}", sbParameters.ToString());
								body = body.Replace("{typeName}", type.Name);
								body = body.Replace("{methodReturnTypeString}", GetMethodReturnTypeString(method.ReturnType));
								body = body.Replace("{parameterValues}", sbParameterValues.ToString());
								body = body.Replace("{path}", type.FullName.Substring(type.FullName.IndexOf(".")).Replace('.', '/'));
								body = body.Replace("{methodReturnType}", GetMethodReturnType(method.ReturnType));

								delegates[method.ReturnType] = null;
								if (!method.ReturnType.Name.StartsWith("Dictionary"))
								{
									if (saveToLocalFile)
										importsLocalFile[method.ReturnType.Namespace] = null;
									else
										imports[method.ReturnType.Namespace] = null;
								}

								sb.Append(body);
							}
						}
					}
					sb.Append("\t}\r\n");
					foreach (var pair in delegates)
					{
						if (pair.Key.Name == "Void")
						{
							sb.AppendLine(String.Format("\tpublic delegate void {0}VoidWebServiceSuccessCallback(object nullObject, object userContext, string methodName);", type.Name));
						}
						else
						{
							sb.AppendLine(String.Format("\tpublic delegate void {0}{1}WebServiceSuccessCallback({2} result, object userContext, string methodName);", type.Name, GetMethodReturnTypeString(pair.Key), GetMethodReturnType(pair.Key)));
						}
					}
					delegates.Clear();
					sb.Append("}\r\n");
					if (hasMethods)
					{
						if (saveToLocalFile)
						{
							List<string> localClassDef = new List<string>();
							localClassDef.Add(sb.ToString());
							saveFileNow(localFileName, importsLocalFile, localClassDef);

						}
						else
							classDefs.Add(sb.ToString());
					}
				}

				saveFileNow(outputPath.File.FullName, imports, classDefs);
				
		//	}
		//	catch (Exception e)
		//	{
		//		WriteMessage("WebServiceStubGenerator", "", Category.error, "1", e.ToString());
		//	}
		}

		static void saveFileNow(string fileName, Dictionary<string, object> imports, List<string> classDefs)
		{
			if (!File.Exists(fileName))
			{
				FileStream f = File.Create(fileName);
				f.Close();
			}
			FileInfo file = new FileInfo(fileName);

			StringBuilder fileContents = new StringBuilder();
			#region "using" statements
			fileContents.Append(@"using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
");
			foreach (var key in imports.Keys)
			{
				string name = key.ToString();
				if (name != "System" && name != "ScriptSharpLibrary")
					fileContents.AppendFormat("using {0};\r\n", name.Replace("SpottedScript.", "Js."));
			}
			#endregion
			foreach (var classDef in classDefs)
			{
				fileContents.AppendLine(classDef);
			}

			var atts = file.Attributes;
			file.Attributes = FileAttributes.Normal;
			System.IO.File.WriteAllText(file.FullName, fileContents.ToString());
			file.Attributes = atts;
		}


		enum Category { error, warning }
		static void WriteMessage(string origin, string subcategory, Category category, string code, string text)
		{
			System.Console.WriteLine("{0} : {1} {2} {3} : {4}", origin, subcategory, category.ToString(), code, text);
		}
		private static string GetMethodReturnTypeString(Type method)
		{
			string methodReturnType = method.Name;
			methodReturnType = methodReturnType.Replace("[]", "Array");
			
			if (methodReturnType.StartsWith("Dictionary"))
			{
				methodReturnType = "Dictionary";
			}
			return methodReturnType;
		}
		private static string GetMethodReturnType(Type method)
		{
			string methodReturnType = method.Name;

			if (methodReturnType.StartsWith("Dictionary"))
			{
				methodReturnType = "Dictionary<string, object>";
			}
			
			if (methodReturnType == "Void")
				methodReturnType = "object";

			return methodReturnType;
		}
		static IEnumerable<MethodInfo> FindWebServiceMethods(Assembly assembly)
		{
			foreach (var type in assembly.GetTypes())
			{
				foreach (var method in type.GetMethods())
				{
					foreach (var att in method.GetCustomAttributes(true))
					{
						if (att is ScriptMethodAttribute)
						{
							yield return method;
							break;
						}
					}
				}
			}
		}
	}
}
