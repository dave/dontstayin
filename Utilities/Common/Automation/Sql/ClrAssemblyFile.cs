using System;
using System.Collections.Generic;

using System.Text;

namespace Common.Automation.Sql
{
	public class ClrAssemblyFile : ClrAssembly 
	{
		public System.IO.FileInfo FileInfo { get; private set; }
		public ClrAssemblyFile(System.IO.FileInfo fi)
		{
			this.name = System.IO.Path.GetFileNameWithoutExtension(fi.FullName);
			this.FileInfo = fi;
		}
		
		public override string Mvid
		{
			get
			{
				System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFile(FileInfo.FullName);
				System.Reflection.Module module = assembly.ManifestModule;
				return module.ModuleVersionId.ToString();
			}
		}
	}
}
