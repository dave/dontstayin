using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Common.CommandLine;
using System.Web.UI;
using System.IO;
using Common;

namespace ViewScriptGenerator
{
	static class Ext
	{
		internal static bool InheritsFrom(this Type a, string fullName)
		{
			Type current = a.BaseType;
			while (current != null && current != typeof(object))
			{
				if (current.FullName == fullName)
				{
					return true;
				}
				current = current.BaseType;
			}
			return false;
		}
	}
	class Program
	{
		//C:\source\XXX\Utilities\Script\ViewScriptGenerator\bin\ViewScriptGenerator.exe /a:C:\source\XXX\Spotted\Spotted\bin\Spotted.dll /o:c:\source\XXX\Spotted\SpottedScript\Views\Generated.cs
		static FilePathArgument assemblyPath = new FilePathArgument(new string[] { "assembly", "a" }, null, "Enter the path to the assembly containing the web services");
		//static FilePathArgument desktopPath = new FilePathArgument(new string[] { "output", "o" }, null, "Enter the path to the file you would like generated e.g. generated.cs");
		//static FilePathArgument mobilePath = new FilePathArgument(new string[] { "mobile", "m" }, null, "Enter the path to the file you would like mobile script generated e.g. generated.cs");

		static void Main(string[] args)
		{
			go(false);
			go(true);
		}
		static void go(bool mobile)
		{
			try
			{
				string[] enhancedTypes;
				if (mobile)
				{
					enhancedTypes = new string[] { "Spotted.MobileEnhancedUserControl", "Spotted.MobileEnhancedHtmlControl", "Spotted.MobileEnhancedControl" };
				}
				else
				{
					enhancedTypes = new string[] { "Spotted.EnhancedUserControl", "Spotted.EnhancedHtmlControl", "Spotted.EnhancedControl", "Spotted.MobileEnhancedUserControl", "Spotted.MobileEnhancedHtmlControl", "Spotted.MobileEnhancedControl" };
				}
				string folderName = mobile ? "Mobile" : "Desktop";
				
				Assembly assembly = Assembly.LoadFrom(assemblyPath.File.FullName);
		
				Dictionary<string, string> mappings = new Dictionary<string, string>();
				AddMappings(mappings);
				
				foreach (var type in assembly.GetTypes())
				{
					if (enhancedTypes.Any(s => type.InheritsFrom(s)))
					{
						#region Enhanced and Mobile UserControl / HtmlControl / Control types
						Dictionary<string, string> missingMappings = new Dictionary<string, string>();

						StringBuilder sb = new StringBuilder();
						sb.AppendLine("using System;");
						sb.AppendLine("using System.Collections.Generic;");
						sb.AppendLine("using System.Html;");
						sb.AppendLine("using jQueryApi;");
						sb.AppendLine("using Js.Library;");
						sb.AppendLine();

						string fullNamespace = "Js" + type.FullName.Substring(7);
						sb.AppendLine("namespace " + fullNamespace);
						sb.AppendLine("{");

						#region Server
						bool hasServer = false;
						StringBuilder sbServer = new StringBuilder();

						foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
							if (method.GetCustomAttributes(typeof(ClientAttribute), true).Length > 0)
								registerClientMethod(method, sbServer);

						Type baseType = type.BaseType;
						if (baseType != null && (baseType.Namespace == null || !baseType.Namespace.StartsWith("System")))
							foreach (MethodInfo method in baseType.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod))
								if (method.GetCustomAttributes(typeof(ClientAttribute), true).Length > 0)
									registerClientMethod(method, sbServer);

						hasServer = sbServer.Length > 0;
						if (hasServer)
						{
							sb.AppendLine("\tpublic class Server {");
							sb.AppendLine("\t\tpublic Server(){}");
							sb.Append(sbServer.ToString());
							sb.AppendLine("\t}");
						}
						#endregion

						#region View
						sb.AppendFormat("\tpublic partial class View\r\n");
						if (!enhancedTypes.Any(s => type.BaseType.FullName == s))
						{
							sb.AppendLine("\t\t : Js" + type.BaseType.FullName.Substring(7) + ".View");
						}
						
						sb.AppendLine("\t{");
						sb.AppendLine("\t\tpublic string clientId;");
						if (hasServer)
							sb.AppendLine("\t\tpublic Server server;");
						sb.AppendFormat("\t\tpublic View(string clientId)\r\n", type.Name);
						if (!enhancedTypes.Any(s => type.BaseType.FullName == s))
						{
							sb.AppendLine("\t\t\t : base(clientId)");
						}
						sb.AppendLine("\t\t{");
						sb.AppendLine("\t\t\tthis.clientId = clientId;");
						if (hasServer)
							sb.AppendLine("\t\t\tthis.server = new Server();");
						sb.AppendLine("\t\t}");
						foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
						{
							if (typeof(Control).IsAssignableFrom(field.FieldType))
							{
								if (mappings.ContainsKey(field.FieldType.FullName) && mappings[field.FieldType.FullName] == null)
								{
									continue;
								}
								string spottedScriptControlName = field.FieldType.FullName.Substring(7);


								if (field.FieldType.FullName.StartsWith(("Spotted.")) && File.Exists(@"C:\Source\" + Common.Properties.CurrentBranchName + @"\Spotted\Js\" + (folderName + spottedScriptControlName).Replace(".", @"\") + @"\"+field.FieldType.Name+".Controller.cs"))
								{
									sb.AppendFormat(ControllerGetter("Js" + spottedScriptControlName) + "\r\n", field.Name);
								}
								else if (mappings.ContainsKey(field.FieldType.FullName))
								{
									sb.AppendFormat(mappings[field.FieldType.FullName] + "\r\n", field.Name);
								}
								else
								{
									missingMappings[field.FieldType.FullName] = field.FieldType.Name;
									if (field.Name.Contains("<"))
									{
										System.Diagnostics.Debugger.Break();
									}
									sb.AppendFormat(
										ElementGetter("Element") + "{1}\r\n",
										field.Name,
										MissingMappingStart + field.FieldType.FullName + MissingMappingEnd);
								}
							}
						}
						sb.AppendLine("\t}");
						sb.AppendLine("}");
						#endregion

						string dir = @"C:\Source\" + Common.Properties.CurrentBranchName + @"\Spotted\Js\" + folderName + type.FullName.Substring(7).Replace(".", @"\");
						string filename = dir + @"\"+type.Name+".View.cs";
						if (!Directory.Exists(dir))
						{
							Directory.CreateDirectory(dir);
						}
						if (!File.Exists(filename))
						{
							FileStream f = File.Create(filename);
							f.Close();
						}
						FileInfo output = new FileInfo(filename);

						StringBuilder fileSb = new StringBuilder();
						foreach (var pair in missingMappings)
						{
							fileSb.AppendLine(MissingMappingStart + pair.Key + MissingMappingEnd);
						}
						fileSb.Append(sb.ToString());
						var atts = output.Attributes;
						output.Attributes = FileAttributes.Normal;
						System.IO.File.WriteAllText(output.FullName, fileSb.ToString());
						output.Attributes = atts;
						#endregion
					}
				}



				
			}
			catch (Exception e)
			{
				WriteMessage("ViewScriptGenerator", "", Category.error, "1", e.ToString());
			}
		}
		static void registerClientMethod(MethodInfo method, StringBuilder sb)
		{
			string paramsWithTypes = "";
			string paramsWithoutTypes = "";

			foreach (var par in method.GetParameters())
			{
				string type = par.ParameterType.ToString();
				if (par.ParameterType == typeof(System.Collections.Generic.Dictionary<string, object>) || par.ParameterType == typeof(System.Collections.Hashtable))
					type = "Dictionary<object, object>";

				paramsWithTypes += type + " " + par.Name + ", ";

				paramsWithoutTypes += (paramsWithoutTypes.Length > 0 ? ", " : "") + par.Name;
			}

			sb.AppendLine("\t\tpublic void " + method.Name + "(" + paramsWithTypes + "Response response) { object[] paramArr = { " + paramsWithoutTypes + " }; ServerRequest req = (ServerRequest)Script.Eval(\"PageMethods.ClientRequest\"); if (req != null) { try { req(\"" + method.DeclaringType.AssemblyQualifiedName + "\", \"" + method.Name + "\", paramArr, response, response); } catch (Exception e) { Dictionary<object, object> d = new Dictionary<object, object>(); d[\"Exception\"] = true; d[\"ExceptionType\"] = \"ClientException\"; d[\"Message\"] = e.Message; d[\"StackTrace\"] = \"\"; response(d); } } }");
			//sb.AppendLine("\t\tpublic void " + method.Name + "(" + paramsWithTypes + "Response response) { object[] paramArr = { " + paramsWithoutTypes + " }; ServerRequest req = (ServerRequest)Script.Eval(\"PageMethods.ClientRequest\"); if (req != null) { req(\"" + method.DeclaringType.AssemblyQualifiedName + "\", \"" + method.Name + "\", paramArr, response, response); } }");
		}

		//private void RegisterClientMethod(MethodInfo method)
		//{
		//    string blockName = string.Concat(method.Name, "_webMethod_uc");

		//    StringBuilder funcBuilder = new StringBuilder();
		//    funcBuilder.Append("function ");
		//    funcBuilder.Append(method.Name);
		//    funcBuilder.Append("(");
		//    foreach (var par in method.GetParameters())
		//        funcBuilder.AppendFormat("{0},", par.Name);
		//    funcBuilder.Append("successCallback,failureCallback){if(PageMethods.ClientRequest){try{var parms=[];for(var i=0;i<arguments.length-2;i++){parms.push(arguments[i]);}PageMethods.ClientRequest(");
		//    funcBuilder.AppendFormat("'{0}','{1}'", method.DeclaringType.AssemblyQualifiedName, method.Name);
		//    funcBuilder.Append(",parms,successCallback,failureCallback);}catch(e){failureCallback.call(null, e.toString());}}}");

		//    ScriptManager.RegisterClientScriptBlock(this, GetType(), blockName, funcBuilder.ToString(), true);
		//}
 
		enum Category { error, warning }
		static void WriteMessage(string origin, string subcategory, Category category, string code, string text)
		{
			System.Console.WriteLine("{0} : {1} {2} {3} : {4}", origin, subcategory, category.ToString(), code, text);
		}
		const string MissingMappingStart = @"//mappings.Add(""";
		readonly static string MissingMappingEnd = @""", ElementGetter(""Element""));";
		static string ElementGetter(string name)
		{
			return
@"		public " + name + @" {0} {{get {{if (_{0} == null) {{_{0} = (" + name + @")Document.GetElementById(clientId + ""_{0}"");}}; return _{0};}}}} private " + name + @" _{0};
		public jQueryObject {0}J {{get {{if (_{0}J == null) {{_{0}J = jQuery.Select(""#"" + clientId + ""_{0}"");}}; return _{0}J;}}}} private jQueryObject _{0}J;";
		}
		static string BehaviourGetter(string name)
		{
			return "\t\tpublic Js.ClientControls." + name + " {0} {{get {{return (Js.ClientControls." + name + ") Script.Eval(clientId + \"_{0}Behaviour\");}}}}";
		}
		static string ControllerGetter(string name)
		{
			name += ".Controller";
			return "\t\tpublic " + name + " {0} {{get {{return (" + name + ") Script.Eval(clientId + \"_{0}Controller\");}}}}";
		}

		private static void AddMappings(Dictionary<string, string> mappings)
		{
			mappings.Add("System.Web.UI.Page", null);
			mappings.Add("System.Web.UI.HtmlControls.HtmlImage", ElementGetter("ImageElement"));
			mappings.Add("JsWebControls.MultiSelector", BehaviourGetter("MultiSelectorBehaviour"));
			mappings.Add("JsWebControls.HtmlAutoComplete", BehaviourGetter("HtmlAutoCompleteBehaviour"));
			mappings.Add("JsWebControls.HtmlAutoSuggest", BehaviourGetter("HtmlAutoCompleteBehaviour"));
			mappings.Add("System.Web.UI.WebControls.TextBox", ElementGetter("InputElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlInputHidden", ElementGetter("InputElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlInputText", ElementGetter("InputElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlInputPassword", ElementGetter("InputElement"));
			mappings.Add("System.Web.UI.WebControls.HiddenField", ElementGetter("InputElement"));
			mappings.Add("System.Web.UI.WebControls.DropDownList", ElementGetter("SelectElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlSelect", ElementGetter("SelectElement"));
			mappings.Add("System.Web.UI.WebControls.CheckBox", ElementGetter("CheckBoxElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlInputCheckBox", ElementGetter("CheckBoxElement"));
			mappings.Add("System.Web.UI.WebControls.RadioButton", ElementGetter("CheckBoxElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlInputRadioButton", ElementGetter("CheckBoxElement")); 
			mappings.Add("System.Web.UI.WebControls.ImageButton", ElementGetter("ImageElement"));
			mappings.Add("System.Web.UI.WebControls.HyperLink", ElementGetter("AnchorElement"));
			mappings.Add("System.Web.UI.WebControls.PlaceHolder", null);
			mappings.Add("System.Web.UI.HtmlControls.HtmlAnchor", ElementGetter("AnchorElement"));
			mappings.Add("System.Web.UI.WebControls.Panel", ElementGetter("DivElement"));
			mappings.Add("System.Web.UI.WebControls.ListBox", ElementGetter("SelectElement"));
			mappings.Add("System.Web.UI.HtmlControls.HtmlButton", ElementGetter("InputElement"));

		}
	}
}

