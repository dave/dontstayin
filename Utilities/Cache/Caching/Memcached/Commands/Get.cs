using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Caching.Memcached.Commands
{
	class Get : KeyedCommand, ICanBeUsedInMultiCommand
	{
		internal object RetrievedObject { get; private set; }
		internal Get(Key key)
			: base(key)
		{
		}

		public override void Execute(MemcachedSocket memcachedSocket)
		{
			BeforeFlush(memcachedSocket);
			memcachedSocket.Flush();
			AfterFlush(memcachedSocket);
		}


		#region ICanBeUsedInMultiCommand Members


		public void BeforeFlush(MemcachedSocket memcachedSocket)
		{
			memcachedSocket.WriteLine("get " + this.Key.Hash);
		}

		public void AfterFlush(MemcachedSocket memcachedSocket)
		{
			string line = memcachedSocket.ReadLine();
			if (line == "END") return;

			CheckResponseStart(line, "VALUE");

			string[] splitLine = line.Split(' ');
			StoredObjectType objType = (StoredObjectType)int.Parse(splitLine[2]);
			int length = int.Parse(splitLine[3]);

			switch (objType)
			{
				case StoredObjectType.IntCounter:
					RetrievedObject = int.Parse(memcachedSocket.ReadLine().Substring(0, length));
					break;
				case StoredObjectType.UIntCounter:
					RetrievedObject = uint.Parse(memcachedSocket.ReadLine().Substring(0, length));
					break;
				case StoredObjectType.LongCounter:
					RetrievedObject = long.Parse(memcachedSocket.ReadLine().Substring(0, length));
					break;
				case StoredObjectType.SerializableObject:
					byte[] bytes = memcachedSocket.ReadBytes(length);
					using (MemoryStream memoryStream = new MemoryStream(bytes))
					{
						RetrievedObject = new BinaryFormatter().Deserialize(memoryStream);
					}
					CheckResponse(memcachedSocket.ReadLine(), "");
					break;
				default:
					throw new Exception("Unexpected StoredObjectType: " + objType.ToString());
			}
			CheckResponse(memcachedSocket.ReadLine(), "END");
		}

		#endregion
	}
}
