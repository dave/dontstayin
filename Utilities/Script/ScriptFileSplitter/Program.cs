using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.CommandLine;
using System.IO;

namespace ScriptFileSplitter
{
	class Program
	{
		static FilePathArgument inputFilePath = new FilePathArgument(new string[] { "input", "i" }, null, "Enter the path to the input script file");
		static DirectoryPathArgument outputDirPath = new DirectoryPathArgument(new string[] { "output", "o" }, ".");
		static void Main(string[] args)
		{
			try
			{
				if (!outputDirPath.Directory.Exists) outputDirPath.Directory.Create();
				Generator generator = new Generator(inputFilePath.File, outputDirPath.Directory);
				generator.Execute();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
			


		}
	}
}
