//using System.Web;
//using System;
//using System.Threading;
//namespace Spotted.Support.Comet
//{
//    public abstract class AsyncHandler : IHttpAsyncHandler
//    {
//        static ThreadPool _threadPool;

//        static AsyncHandler()
//        {
//            _threadPool =
//              new ThreadPool(2, 25, "AsyncPool");
//            _threadPool.PropogateCallContext = true;
//            _threadPool.PropogateThreadPrincipal = true;
//            _threadPool.PropogateHttpContext = true;
//            _threadPool.Start();
//        }

//        public void ProcessRequest(HttpContext ctx)
//        {
//            // not used
//        }

//        public bool IsReusable
//        {
//            get { return false; }
//        }

//        public IAsyncResult BeginProcessRequest(HttpContext ctx,
//                         AsyncCallback cb, object obj)
//        {
//            BeginProcessRequest();
//            AsyncRequestState reqState =
//                           new AsyncRequestState(ctx, cb, obj);
//            _threadPool.PostRequest(
//                           new WorkRequestDelegate(ProcessRequest),
//                           reqState);

//            return reqState;
//        }

		

//        public void EndProcessRequest(IAsyncResult ar)
//        {
//            EndProcessRequest();
//        }

		

//        void ProcessRequest(object state, DateTime requestTime)
//        {
//            AsyncRequestState reqState = state as AsyncRequestState;

//            LongRunningProcess();
			
//            reqState.CompleteRequest();
//        }
//        protected abstract void BeginProcessRequest();
//        protected abstract void LongRunningProcess();
//        protected abstract void EndProcessRequest();
		
		

//    }
//}
