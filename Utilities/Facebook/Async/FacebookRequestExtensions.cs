using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Facebook.Async
{/*
    public static class FacebookRequestExtensions
    {
        public static IAsyncResult BeginExecute(this FacebookRequest fbRequest, AsyncCallback callback, Int32 timeout)
        {
            try
            {
                var request = fbRequest.CreateWebRequest(timeout);
                var state = new FacebookAsyncState(fbRequest, request);

                IAsyncResult result = request.BeginGetResponse(callback, state);
                return result;
            }
            catch (Exception ex) { throw new FacebookException("Error attempting to execute {0} request: {1}", ex, fbRequest.MethodName, ex.Message); }
        }

        public static FacebookResponse<TValue> EndExecute<TValue>(IAsyncResult result)
        {
            FacebookAsyncState state = null;
            try
            {
                state = (FacebookAsyncState)result.AsyncState;
                var response = (HttpWebResponse)state.HttpRequest.EndGetResponse(result);

                return FacebookRequest.ProcessResponse<TValue>(response);
            }
            catch (WebException ex) { return new FacebookResponse<TValue>(ex); }
            catch (Exception ex) { throw new FacebookException("Error attempting to execute {0} request: {1}", ex, state.Request.MethodName, ex.Message); }
        }
    }*/
}
