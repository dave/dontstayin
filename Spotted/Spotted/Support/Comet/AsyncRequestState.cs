//using System;
//using System.Data;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
//using System.Xml.Linq;
//using System.Threading;

//namespace Spotted.Support.Comet
//{
//    public class AsyncRequestState  : IAsyncResult
//    {
//        public AsyncRequestState(HttpContext ctx,
//                                 AsyncCallback cb,
//                                 object extraData)
//        {
//            HttpContext = ctx;
//            AsyncCallback = cb;
//            ExtraData = extraData;
//        }

//        internal HttpContext HttpContext { get; private set; }
//        internal AsyncCallback AsyncCallback { get; private set; }
//        internal object ExtraData { get; private set; }
//        private bool isCompleted = false;
//        private ManualResetEvent _callCompleteEvent = null;

//        internal void CompleteRequest()
//        {
//            isCompleted = true;
//            lock (this)
//            {
//                if (_callCompleteEvent != null)
//                    _callCompleteEvent.Set();
//            }
//            // if a callback was registered, invoke it now
//            if (AsyncCallback != null)
//                AsyncCallback(this);
//        }

//        // IAsyncResult interface property implementations
//        public object AsyncState
//        { get { return (ExtraData); } }
//        public bool CompletedSynchronously
//        { get { return (false); } }
//        public bool IsCompleted
//        { get { return (isCompleted); } }
//        public WaitHandle AsyncWaitHandle
//        {
//            get
//            {
//                lock (this)
//                {
//                    if (_callCompleteEvent == null)
//                        _callCompleteEvent = new ManualResetEvent(false);

//                    return _callCompleteEvent;
//                }
//            }

//        }
//    }

//}
