using System;
using System.Collections.Generic;
using System.Text;

namespace Common.CommandLine
{
	public static class Log 
	{
		public enum SeverityLevel
		{
			Error = 1,
			Warning = 2,
			Message = 3
		}
		public static void Error(string message)
		{
			Error(message, System.Reflection.Assembly.GetEntryAssembly().FullName);
		}
		public static void Error(string message, string source)
		{
			Write(message, source, SeverityLevel.Error);
		}
		public static void Write(string message, string source, SeverityLevel level)
		{
			Console.WriteLine(source + " : " + level.ToString().ToLower() + " 1: " + message);
			
		}
	}
}
