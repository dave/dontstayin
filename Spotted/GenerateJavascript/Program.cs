using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Reflection;
namespace GenerateJavascript
{
	class Program
	{
		static void Main(string[] args)
		{
			bool replaceNames = true;
			bool removeBreaks = true;
			bool removeComments = true;
			bool removeInit = false;
			string globalFile = "";
			string replacementsHints = "";
			Hashtable globalReplacements = new Hashtable();
			int globalVariableNumber = 0;
            
			Regex localReg = new Regex(@"[^\$](\$\w+)");
			Regex globalReg = new Regex(@"[^\^](\^\w+)");
			Regex lineCommentReg = new Regex(@"\/\/.*$", RegexOptions.Multiline);
			Regex blockCommentReg = new Regex(@"\/\*.*\*\/", RegexOptions.Singleline);
			Regex initReg = new Regex(@"(^.*\^InitFunc\(.*,).*\);.*$", RegexOptions.Multiline);

			DirectoryInfo d = new DirectoryInfo(@"..\JavaScript");
			foreach (FileInfo f in d.GetFiles("*.js"))
			{
				replacementsHints += "\n";
				replacementsHints += "=== " + f.Name + " ===\n";

				StreamReader sr = f.OpenText();
				string txt = sr.ReadToEnd();

				if (removeInit)
				{
					if (initReg.IsMatch(txt))
					{
						//int a = 1;
						txt = initReg.Replace(txt, "$1\"\");");
					}
				}

				Hashtable localReplacements = new Hashtable();
				int localVariableNumber = 0;

				MatchCollection localMatches = localReg.Matches(txt);
				foreach (Match m in localMatches)
				{
					string varName = m.Groups[1].ToString();
					if (!localReplacements.ContainsKey(varName))
					{
						localReplacements.Add(varName, "l" + localVariableNumber.ToString());
						localVariableNumber++;
					}
				}
				foreach (string key in localReplacements.Keys)
				{
					replacementsHints += localReplacements[key].ToString() + " -> " + key + "\n";
					string repl = "$1l" + key.Substring(1) + "$2";
					if (replaceNames)
						repl = "$1" + localReplacements[key].ToString() + "$2";
					txt = Regex.Replace(
						txt,
						"([^\\$])" + key.Replace("$", "\\$") + "(\\W)",
						repl);
				}
				if (removeComments)
				{
					txt = lineCommentReg.Replace(txt, "");
					//txt = blockCommentReg.Replace(txt,"");
				}

				globalFile += txt + "\n";
			}

			replacementsHints += "\n";
			replacementsHints += "=== GLOBAL ===\n";

			MatchCollection globalMatches = globalReg.Matches(globalFile);
			foreach (Match m in globalMatches)
			{
				string varName = m.Groups[1].ToString();
				if (!globalReplacements.ContainsKey(varName))
				{
					globalReplacements.Add(varName, "dbc" + globalVariableNumber.ToString());
					globalVariableNumber++;
				}
			}
			foreach (string key in globalReplacements.Keys)
			{
				replacementsHints += globalReplacements[key].ToString() + " -> " + key + "\n";
				string repl = "$1dbc" + key.Substring(1) + "$2";
				if (replaceNames)
					repl = "$1" + globalReplacements[key].ToString() + "$2";
				globalFile = Regex.Replace(
					globalFile,
					"([^\\^])" + key.Replace("^", "\\^") + "(\\W)",
					repl);
			}


			if (removeBreaks)
				globalFile = new Regex("[\t\r\n]+").Replace(globalFile, " ");
			globalFile += (replaceNames ? "" : "\n") + "DbChatSupportFileBuildTime=\"" + DateTime.Now.ToLongTimeString() + "\";var DbChatServerExists = true;";

            StreamWriter fOut = null;
			string DbChatJsPath = @"..\..\Spotted\Misc\DbChat.js";
			FileInfo fi = new FileInfo(DbChatJsPath);
			bool wasReadOnly = true;
			if (fi.Exists && ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly))
			{
				fi.Attributes -= FileAttributes.ReadOnly;
			}
			fOut = File.CreateText(DbChatJsPath);

			fOut.Write(globalFile);
			fOut.Close();
			 
			StreamWriter fOutRep = File.CreateText(@"..\DbChatReplacements.js");
			fOutRep.Write(replacementsHints);
			fOutRep.Close();
			if (wasReadOnly)
			{
				fi.Attributes = FileAttributes.ReadOnly;
			}
			return;
		}
	}
}
