using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MapBrowserSprocsLoadTester.Logging
{
	class CsvLogStore : IBufferedLogStore
	{
		private readonly string path;
		bool headersWritten = false;
		public CsvLogStore(string path)
		{
			this.path = path;
			this.fi = new FileInfo(path);
			if (!fi.Directory.Exists) fi.Directory.Create();
			if (this.fi.Exists) this.fi.Delete();
			using (this.fi.Create()){};
			
			this.sw = this.fi.AppendText();
		}

		private object writeLock = new object();
		private FileInfo fi;
		private List<KeyValuePair<string, List<object>>> data = new List<KeyValuePair<string, List<object>>>();
		private StreamWriter sw;

		public void Write(params KeyValuePair<string, string>[] values)
		{
			lock (this.writeLock)
			{
				if (!this.headersWritten)
				{
					this.sw.WriteLine(String.Join(",", values.Select(v => v.Key).ToArray()));
					this.headersWritten = true;
				}
				this.sw.WriteLine(String.Join(",", values.Select(v => v.Value).ToArray()));
			 
			}
		}

		public void Flush()
		{
			this.sw.Flush();
		}

		public void Dispose()
		{
			this.sw.Dispose();
		}
	}
}
