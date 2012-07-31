//using System;
//using System.Collections.Generic;
//using System.Html;
//using Net.Comet;
//using Js.AutoCompleteLibrary;

//namespace Js.ClientControls.HtmlAutoComplete
//{
	
//    class CometRemoteSuggestionsGetter : RemoteSuggestionsGetter
//    {
//        string url;
//        CometRequest cometRequest;
//        internal CometRemoteSuggestionsGetter(string url)
//        {
//            this.url = url;
//        }

//        protected override void MakeRequest(string text, Dictionary<object, object> parameters, int maxNumberOfItemsToGet)
//        {
//            string requestUrl = url + "?text=" + text.Escape() + "&maxNumberOfItemsToGet=" + maxNumberOfItemsToGet;

//            foreach (object key in parameters.Keys)
//            {
//                requestUrl += "&" + key.ToString().Escape() + "=" + parameters[key].ToString().Escape();
//            }
//            this.cometRequest = CometProxy.Invoke(
//                requestUrl,
//                delegate(string message)
//                {
//                    if (this.OnSuggestionReceived != null) OnSuggestionReceived(
//                        (Suggestion[])Script.Eval("(" + message + ")")
//                    );
//                },
//                delegate()
//                {
//                    if (this.OnAllSuggestionsReceived != null) OnAllSuggestionsReceived();
//                }
//            );
//        }
//        internal override bool IsMakingRequest
//        {
//            get
//            {
//                return cometRequest != null;
//            }
//        }
//        internal override void DoAbortCurrentRequest()
//        {
//            if (cometRequest != null)
//            {
//                cometRequest.Abort();
//                cometRequest = null;
//            }
//        }
//    }
//}
