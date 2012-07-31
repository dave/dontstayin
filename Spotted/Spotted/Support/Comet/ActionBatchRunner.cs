//using System;
//using System.Collections.Generic;
//using System.Threading;
//namespace Spotted.Support.Comet
//{
//    class ActionBatchRunner
//    {
//        List<Action> actions;
//        Action<Exception, Action> exceptionHandler;
//        Thread currentThread;
//        static ThreadPool _threadPool;

//        static ActionBatchRunner()
//        {
//            _threadPool =
//              new ThreadPool(2, 25, "ActionBatchPool");
//            _threadPool.PropogateCallContext = true;
//            _threadPool.PropogateThreadPrincipal = true;
//            _threadPool.PropogateHttpContext = true;
//            _threadPool.Start();
//        }

//        #region ThreadSafe int CompletedActions
//        int completedActions = 0;
//        object completedActionsLock = new object();
//        public int CompletedActions
//        {
//            get
//            {
//                lock (completedActionsLock) { return completedActions; }
//            }
//            set
//            {
//                lock (completedActionsLock) { completedActions = value; }
//            }
//        }
//        #endregion
//        public ActionBatchRunner(IEnumerable<Action> actions, Action<Exception, Action> exceptionHandler)
//        {
//            this.actions = new List<Action>(actions);
//            this.exceptionHandler = exceptionHandler;
//            currentThread = Thread.CurrentThread;
//        }
//        public void Run()
//        {
//            foreach (var action in actions)
//            {
//                var actionInstance = action;
//                _threadPool.PostRequest((state, requestTime) => RunAction(actionInstance));
//            }
//            currentThread.Suspend();
//        }

//        void RunAction(Action action)
//        {
//            try
//            {
//                action();
//            }
//            catch (Exception ex)
//            {
//                if (exceptionHandler != null)
//                {
//                    exceptionHandler(ex, action);
//                }
//            }
//            finally
//            {
//                completedActions++;
//                if (completedActions == actions.Count) { currentThread.Resume(); }
//            }
//        }


//    }
//}
