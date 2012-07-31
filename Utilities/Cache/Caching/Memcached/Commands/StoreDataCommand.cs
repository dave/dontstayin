using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Globalization;

namespace Caching.Memcached.Commands
{
	internal abstract class StoreDataCommand : KeyedCommand
	{
		

		public StoreDataCommand(Key key, object objectToBeStored, DateTime expiry)
			: base(key)
		{
			this.expiry = expiry;
			if (objectToBeStored is int)
			{
				this.objType = StoredObjectType.IntCounter;
				dataToBeStored = Encoding.UTF8.GetBytes(objectToBeStored.ToString());
			}
			else if (objectToBeStored is uint)
			{
				this.objType = StoredObjectType.UIntCounter;
				dataToBeStored = Encoding.UTF8.GetBytes(objectToBeStored.ToString());
			}
			else if (objectToBeStored is long)
			{
				this.objType = StoredObjectType.LongCounter;
				dataToBeStored = Encoding.UTF8.GetBytes(objectToBeStored.ToString());
			}
			else
			{
				this.objType = StoredObjectType.SerializableObject;
				using (MemoryStream ms = new MemoryStream())
				{
					new BinaryFormatter().Serialize(ms, objectToBeStored);
					dataToBeStored = new byte[ms.Length];
					Array.Copy(ms.GetBuffer(), dataToBeStored, dataToBeStored.Length);
				}
			}
		}
		protected byte[] dataToBeStored;
		protected DateTime expiry;
		protected StoredObjectType objType;
		protected string SendCommandAndReadResponse(MemcachedSocket memcachedSocket, string command, params string[] expectedResponses)
		{
			WriteCommandToStream(memcachedSocket, command);
			memcachedSocket.Flush();
			string response = ReadResponseFromStream(memcachedSocket);
			CheckResponse(response, expectedResponses);
			return response;
		}

		internal string ReadResponseFromStream(MemcachedSocket memcachedSocket)
		{
			return memcachedSocket.ReadLine();
		}

		internal void WriteCommandToStream(MemcachedSocket memcachedSocket, string command)
		{
			string firstLine = String.Format("{0} {1} {2} {3} {4}", command, this.Key.Hash, (int)this.objType, Convert.ToString(GetExpiration(expiry), CultureInfo.InvariantCulture), this.dataToBeStored.Length);
			memcachedSocket.WriteLine(firstLine);
			memcachedSocket.WriteLine(dataToBeStored);
		}
		protected static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
		protected static long GetExpiration(DateTime expiresAt)
		{
			if (expiresAt == DateTime.MaxValue)
			{
				expiresAt = DateTime.Now.AddMonths(3);
			}
			return (long)(expiresAt.ToUniversalTime() - UnixEpoch).TotalSeconds;
		}
	}
}
