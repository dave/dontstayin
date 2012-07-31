using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;


namespace ScriptFileSplitter
{
	class Generator
	{
		FileInfo inputFile;
		DirectoryInfo outputDirectory;
		internal Generator(FileInfo inputFile, DirectoryInfo outputDirectory)
		{
			this.inputFile = inputFile;
			this.outputDirectory = outputDirectory;
		}
		internal void Execute()
		{
			ProcessJavascript(ReadJavascriptIn());
		}

		private void ProcessJavascript(List<LineOfJavascript> lines)
		{
			List<LineOfJavascript> nonAspRelatedLines = new List<LineOfJavascript>();
			foreach (var line in lines)
			{
				if (line.Namespace.IsAspControlNamespace)
				{
					line.Namespace.Lines.Add(line);
				}
				else if (line.Namespace.ParentAspControlNamespace != null)
				{
					line.Namespace.ParentAspControlNamespace.Lines.Add(line);
				}
				else
				{
					nonAspRelatedLines.Add(line);
				}
			}

			bool isDebug = inputFile.Name.EndsWith(".debug.js");
			foreach (var file in outputDirectory.GetFiles(isDebug ? "*.debug.js" : "*.js"))
			{
				file.Delete();
			}
			foreach (var ns in Namespace.Namespaces)
			{
				if (ns.Lines.Count > 0)
				{

					File.WriteAllLines(Path.Combine(outputDirectory.FullName, ns.Name + (isDebug ? ".debug" : "" ) + ".js"), ns.Lines.ConvertAll(l => l.Text).ToArray());
				}
			}

			File.WriteAllLines(Path.Combine(outputDirectory.FullName, inputFile.Name), nonAspRelatedLines.ConvertAll(l => l.Text).ToArray());
		}





		private List<LineOfJavascript> ReadJavascriptIn()
		{
			List<LineOfJavascript> lines = new List<LineOfJavascript>();
			Namespace currentNamespace = Namespace.GetNamespace("");
			using (var stream = inputFile.OpenRead())
			{
				using (var reader = new StreamReader(stream))
				{
					while (reader.Peek() > -1)
					{
						string line = reader.ReadLine();
						//if (line.StartsWith("SpottedScript.Controls.ChatClient.Controller.instance")  || line.StartsWith("SpottedScript.Controls.ChatClient.View")) { Debugger.Break(); }
						if (line.Trim() == "") { continue; }
						if (line.Contains(".registerClass("))
						{
							string[] splitLines = line.Split(';');
							foreach (var splitLine in splitLines)
							{
								if (splitLine.Trim().Length == 0) { continue; }
								if (IsStaticVariableSet(splitLine))
								{
									AddLineFromStaticVarSet(lines, splitLine + ";");
								}
								else
								{
									lines.Add(new LineOfJavascript(splitLine + ";", Namespace.GetNamespace(splitLine.Substring(0, splitLine.IndexOf('\'')))));
								}
							}
							continue;
						}
						else if (line.StartsWith("Type.registerNamespace('"))
						{
							int splitPoint = line.IndexOf(';');
							currentNamespace = Namespace.GetNamespace(line.Substring(24, line.IndexOf('\'', 24) - 24));
							lines.Add(new LineOfJavascript(line.Substring(0, splitPoint + 1), currentNamespace));
							lines.Add(new LineOfJavascript(line.Substring(splitPoint + 1), currentNamespace));
						}
						else if (IsStaticVariableSet(line))
						{
							AddLineFromStaticVarSet(lines, line);
							continue;
						}
						else
						{
							lines.Add(new LineOfJavascript(line, currentNamespace));
						}
						
					}
				}
			}
			return lines;
		}


		private static void AddLineFromStaticVarSet(List<LineOfJavascript> lines, string line)
		{
			var ns = Namespace.GetNamespace(line.Substring(0, line.IndexOf("=")).Trim(), false);
			var parentNs = ns.ParentAspControlNamespace;
			if (parentNs == null)
			{
				lines.Add(new LineOfJavascript(line, Namespace.GetNamespace("")));
			}
			else
			{
				lines.Add(new LineOfJavascript(line, parentNs));
			}
		}
		static readonly Regex IsStaticVariableSetRegex1 = new Regex(@"^([$0-9a-zA-Z_])+(\.[$0-9a-zA-Z_]+)+ ?= ?.+$", RegexOptions.Compiled);
		static bool IsStaticVariableSet(string line)
		{
			return IsStaticVariableSetRegex1.IsMatch(line) && !new Regex(@"= ?function").IsMatch(line) && !line.Contains(".prototype") && !line.StartsWith("this.");
		}
	}
}
