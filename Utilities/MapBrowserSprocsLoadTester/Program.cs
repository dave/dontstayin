using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Autofac;
using Autofac.Builder;
using MapBrowserSprocsLoadTester.Logging;
using MapBrowserSprocsLoadTester.MapBrowser;
using MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners;
using MapBrowserSprocsLoadTester.Logging;
using MapBrowserSprocsLoadTester.MapBrowser;
using MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners;
using System.IO;

namespace MapBrowserSprocsLoadTester
{
	class Program
	{
		static void Main(string[] args)
		{
		    new Program();
		}


		Program()
		{
			Common.Properties.ConnectionString = "server=anoth;database=db_spotted_testing_2008_08_13;trusted_connection=true";

			string filePath = string.Format(@"c:\Results\log{0}.csv", DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss"));
			int seed = 0;
			
			var builder = new ContainerBuilder();
			builder.Register(c => new Random(seed++)).FactoryScoped();
			builder.Register(c => new CsvLogStore(filePath)).SingletonScoped().As<ILogStore>().As<IBufferedLogStore>();

			builder.Register(typeof(RandomPositionAwayFromLondonViewportBehaviour)).As<IViewportBehaviour>();
			builder.Register(typeof(User)).FactoryScoped();
			builder.Register(typeof(TestRunner));
			var container = builder.Build();

			var tr = container.Resolve<TestRunner>();

			foreach (var tableName in Enum.GetNames(typeof(TableName)))
				for (int zoomLevel = 6; zoomLevel < 18; zoomLevel++)
				foreach (var isFinishedSpecification in IsFinishedSpecifications)
					foreach (var listener in GetListeners(tableName))
					{
						var headers = new List<KeyValuePair<string, string>>()
						{
							new KeyValuePair<string, string>("ZoomLevel", zoomLevel.ToString()),
							new KeyValuePair<string, string>("FinishSpec", isFinishedSpecification.Key),
							new KeyValuePair<string, string>("Table", tableName),
							new KeyValuePair<string, string>("Algorithm", listener.Key),
						};
						var builder2 = new ContainerBuilder();
						builder2.Register(c => new ViewportResultLogger(c.Resolve<ILogStore>(), headers.ToArray())).As
							<IResultLogger<Viewport>>();
						KeyValuePair<string, Func<IContext, IListener<Viewport>>> listener1 = listener;
						builder2.Register(c => listener1.Value(c)).As<IListener<Viewport>>();
						builder2.Register(c => new ViewportFactory(c.Resolve<IListener<Viewport>>(), zoomLevel));
						builder2.Build(container);
						tr.Execute(() => container.Resolve<User>(), isFinishedSpecification.Value, 1, 0);
						container.Resolve<IBufferedLogStore>().Flush();


					}
		
	}


		IEnumerable<KeyValuePair<string, Func<IContext, IListener<Viewport>>>> GetListeners(string tableName)
		{
			yield return new KeyValuePair<string, Func<IContext, IListener<Viewport>>>(tableName,
																						   c =>
																						   new CircleTableLinqToSqlLoggerViewportListener(
																							tableName, 
																							c.Resolve<IResultLogger<Viewport>>(),
																							Common.Properties.ConnectionString));
			//foreach (var sqlScript in SqlScripts)
			//{
			//    string tableName1 = tableName;
			//    KeyValuePair<string, string> sqlScript1 = sqlScript;
			//    yield return new KeyValuePair<string, Func<IContext, IListener<Viewport>>>(tableName + "-" + sqlScript.Key,
			//                                                                               c =>
			//                                                                               new TableSqlLoggerViewportListener(
			//                                                                                tableName1, sqlScript1.Value,
			//                                                                                c.Resolve<IResultLogger<Viewport>>(),
			//                                                                                Common.Properties.ConnectionString));
			//}
		}

		//IEnumerable<KeyValuePair<string, Func<IContext, IListener<Viewport>>>> GetListeners(string tableName)
		//{
		//    foreach (var sqlScript in SqlScripts)
		//    {
		//        string tableName1 = tableName;
		//        KeyValuePair<string, string> sqlScript1 = sqlScript;
		//        yield return new KeyValuePair<string, Func<IContext, IListener<Viewport>>>(tableName + "-" + sqlScript.Key,
		//                                                                                   c =>
		//                                                                                   new TableSqlLoggerViewportListener(
		//                                                                                    tableName1, sqlScript1.Value,
		//                                                                                    c.Resolve<IResultLogger<Viewport>>(),
		//                                                                                    Common.Properties.ConnectionString));
		//    }
		//}

		IEnumerable<KeyValuePair<string, TestRunner.IsFinishedSpecification>> IsFinishedSpecifications
		{
			get
			{
				yield return new KeyValuePair<string, TestRunner.IsFinishedSpecification>("20iterations", (elapsedTime, iterations) => iterations > 20);
				//yield return new KeyValuePair<string, TestRunner.IsFinishedSpecification>("max5seconds", elapsedTime => elapsedTime > 5000);
				//yield return new KeyValuePair<string, TestRunner.IsFinishedSpecification>("max20seconds", elapsedTime => elapsedTime > 20000);
//				yield return new KeyValuePair<string, TestRunner.IsFinishedSpecification>("max20seconds", elapsedTime => elapsedTime < 20000);
			}
		}
	
		IEnumerable<KeyValuePair<string, string>> SqlScripts
		{
			get
			{
				DirectoryInfo di = new DirectoryInfo(@"MapBrowser\SqlViewportListeners\Scripts");
				foreach (var f in di.GetFiles("*.sql"))
				{
					yield return new KeyValuePair<string, string>(f.Name, File.ReadAllText(f.FullName));
				}
			}
		}
	}

	

	class TestRunner
	{
		private Func<User> getUser;
		private IsFinishedSpecification isFinishedSpecification;
		private int numberOfUsers;
		private int timeBetweenRequestsInMs;

		internal delegate bool IsFinishedSpecification(long elapsedTime, int iterations);
		public TestRunner()
		{
			
		}

		public void Execute(Func<User> getUser, IsFinishedSpecification isFinishedSpecification, int numberOfUsers, int timeBetweenRequests)
		{
			this.getUser = getUser;
			this.isFinishedSpecification = isFinishedSpecification;
			this.numberOfUsers = numberOfUsers;
			this.timeBetweenRequestsInMs = timeBetweenRequests;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			var users = new Queue<User>();
			
			for (int i = 0; i < numberOfUsers; i++)
			{
				var user = getUser();
				users.Enqueue(user);
			}
			object usersLock = new object();
				int iterations = 0;
			while (!this.isFinishedSpecification(sw.ElapsedMilliseconds, iterations)|| users.Count < numberOfUsers)
			{
				while (users.Count > 0 && users.Peek().NextUpdateTime < sw.ElapsedMilliseconds && !this.isFinishedSpecification(sw.ElapsedMilliseconds, iterations))
				{
					iterations++;
					User user = null;
					lock (usersLock)
					{
						user = users.Dequeue();
					}
					new Thread(() =>
					{
						user.Update(sw.ElapsedMilliseconds, this.timeBetweenRequestsInMs);
						lock (usersLock)
						{
							users.Enqueue(user);
						}
					}).Start();
				}
				Thread.Sleep(100);
			}
		}
	}

}
