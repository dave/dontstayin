using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SleepTool
{
	class Program
	{
		static void Main(string[] args)
		{
			int pause = int.Parse(args[0]);
			System.Threading.Thread.Sleep(pause * 1000);
		}
	}
}
