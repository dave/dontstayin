using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Globalization;
using System.IO;
using System.Net;

namespace Caching.Memcached
{
	public class MemcachedSocket : IDisposable
	{
		static readonly byte[] NewLineBytes = Encoding.UTF8.GetBytes("\r\n");
		BufferedStream stream;
		Socket socket;
		public System.Net.EndPoint EndPoint { get { return socket.RemoteEndPoint; } }

		internal MemcachedSocket(IPEndPoint endPoint)
		{
#if WEB
			if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Items["MemcachedTimeout"] != null && (bool)System.Web.HttpContext.Current.Items["MemcachedTimeout"])
			{
				int i = (int)System.Web.HttpContext.Current.Items["MemcachedTimeoutQty"];
				throw new Exception("Memcached disabled because of timeout...");
				
			}
#endif

			socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			//socket.ReceiveTimeout = 1;
			//socket.SendTimeout = 1;
			socket.ReceiveTimeout = 50;
			socket.SendTimeout = 50;
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 50);//DAVE - 500
			socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 50);//DAVE - 500
			socket.NoDelay = true;
			
			IAsyncResult result = socket.BeginConnect(endPoint.Address, endPoint.Port, null, null);

			bool success = result.AsyncWaitHandle.WaitOne(50, true);

			if (!success)
			{
				// NOTE, MUST CLOSE THE SOCKET
				socket.Close();

#if WEB
				if (System.Web.HttpContext.Current != null)
				{
					System.Web.HttpContext.Current.Items["MemcachedTimeout"] = true;
					if (System.Web.HttpContext.Current.Items["MemcachedTimeoutQty"] == null)
						System.Web.HttpContext.Current.Items["MemcachedTimeoutQty"] = 1;
					else
						System.Web.HttpContext.Current.Items["MemcachedTimeoutQty"] = ((int)System.Web.HttpContext.Current.Items["MemcachedTimeoutQty"]) + 1;

				}
#endif

				throw new Exception("Failed to connect server.");
			}

			// Success
			stream = new BufferedStream(new NetworkStream(this.socket));



		}
		internal void Write(string value)
		{
			Write(Encoding.UTF8.GetBytes(value));
		}

		internal void Write(byte[] bytes)
		{
			stream.Write(bytes, 0, bytes.Length);
		}
		internal void WriteLine(string value)
		{
			WriteLine(Encoding.UTF8.GetBytes(value));
		}

		internal void WriteLine(byte[] bytes)
		{
			stream.Write(bytes, 0, bytes.Length);
			stream.Write(NewLineBytes, 0, NewLineBytes.Length);
		}

		internal void Flush()
		{
			stream.Flush();
		}
		internal string ReadLine()
		{
			int prev = stream.ReadByte();
			int current = stream.ReadByte();
			StringBuilder sb = new StringBuilder();
			while (!(prev == 13 && current == 10))
			{
				sb.Append((char)prev);
				prev = current;
				current = stream.ReadByte();
			}
			return sb.ToString();
		}

		internal byte[] ReadBytes(int numberOfBytes)
		{
			byte[] bytes = new byte[numberOfBytes];

			int count = 0;
			while (count < numberOfBytes)
			{
				// BufferedStream Read can return less than number of bytes requested
				// http://msdn2.microsoft.com/en-us/library/system.io.bufferedstream.read.aspx
				int cnt = stream.Read(bytes, count, (numberOfBytes - count));
				count += cnt;
			}

			return bytes;
		}

		void IDisposable.Dispose()
		{
			socket.Shutdown(SocketShutdown.Both);
			((IDisposable)socket).Dispose();
		}

		internal void DiscardDataInStream()
		{
			stream.Flush();
			while (socket.Available > 0)
			{
				byte[] discardedBytes = new byte[socket.Available];
				socket.Receive(discardedBytes);
			}
		}
	}
}
