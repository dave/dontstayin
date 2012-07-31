//using System;
//using System.Collections.Generic;
//using System.Text;
//
//using NUnit.Framework;

//namespace Cache
//{
//    [TestFixture]
//    public class StaticTests
//    {
//        bool passed = true;

//        public void RunSets()
//        {
//            for (int i = 0; i < 10; i++)
//            {
//                try
//                {
//                    Static.Cache.Delete(i.ToString());
//                    Thread.Sleep(10);
//                }
//                catch (Exception up)
//                {
//                    passed = false;
//                    throw up;
//                }
//            }
//        }

//        [Test]
//        public void TestCacheCanHandleMultipleThreads()
//        {
//            int threads = 200;
//            Thread[] t = new Thread[threads];
//            for (int i = 0; i < threads; i++)
//            {
//                t[i] = new Thread(new ThreadStart(RunSets));
//                t[i].Start();
//            }
//            for (int i = 0; i < threads; i++)
//            {
//                t[i].Join();
//            }
//            Assert.IsTrue(passed);
//        }
//    }
//}
