using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bobs;

namespace DbServerTest
{
	class Program
	{
		static void Main(string[] args)
		{


			int length = 1000;
			System.Threading.Thread[] writers = new System.Threading.Thread[length];
			System.Threading.Thread[] readers = new System.Threading.Thread[length];
			for (int i = 0; i < length; i++)
			{
				Guid g = Guid.NewGuid();
				writers[i] = new System.Threading.Thread(() => DbTester(i));
				writers[i].Start();

				Console.Write("[" + i.ToString() + "]");

				System.Threading.Thread.Sleep(5000 / length);
			}


		}

		public static void DbTester(int key)
		{
			//Testing bob cache
			Random r = new Random();
			//Guid key = Guid.NewGuid();

			while (true)
			{
				try
				{
					//System.Threading.Thread.Sleep(50);

					Guid data = Guid.NewGuid();

					
					Usr u;
					try
					{
						u = new Usr(key);
					}
					catch (BobNotFound b)
					{
						u = new Usr(4);
					}
					catch (Exception e)
					{
						throw e;
					}

					//Caching.Instances.Main.Store(key.ToString("N"), data);

					System.Threading.Thread.Sleep(r.Next(500, 1500));
					//Caching.Instances.Main.Delete(key);

					string s = u.LoginString;

					Console.Write(".");
				}
				catch (Exception exception) { Console.Write("!"); } //exception.Message); }
			}
		}

	}
}
