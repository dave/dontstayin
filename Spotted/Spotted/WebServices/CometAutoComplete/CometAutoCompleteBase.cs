//using System;
//using System.Collections;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.Services;
//using System.Web.Services.Protocols;
//using System.Xml.Linq;
//using Spotted.Support.Comet;
//using Microsoft.JScript;
//using System.Web.Script.Serialization;
//using Js.ClientControls;
//using System.Collections.Generic;
//using System.Threading;
//using Bobs;
//using GetSuggestionsFunction = System.Func<System.Collections.Generic.IEnumerable<Js.AutoCompleteLibrary.Suggestion>>;
//using Common.Collections;
//using Js.AutoCompleteLibrary;

//namespace Spotted.WebServices.CometAutoComplete
//{
//    public abstract class CometAutoCompleteBase : CometAsyncHttpHandler
//    {
//        PriorityList<Suggestion> suggestions = new PriorityList<Suggestion>();

//        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
//        public override System.Collections.Generic.IEnumerable<Action> GetActions()
//        {
//            string text = "";
//            int maxNumberOfItemsToGet = 5;
//            var parameters = new Dictionary<string,object>();
//            foreach (string key in httpContext.Request.QueryString.Keys){
//                switch (key)
//                {
//                    case "text": text = httpContext.Request.QueryString[key]; break;
//                    case "maxNumberOfItemsToGet": maxNumberOfItemsToGet = int.Parse(httpContext.Request.QueryString[key]); break;
//                    default: parameters[key] = HttpContext.Current.Request.QueryString[key]; break;

//                }
//            }
//            int priority = 100 * text.Length;
//            foreach (GetSuggestionsFunction getSuggestionsFunction in GetGetSuggestionsFunctions(currentUsr, text, maxNumberOfItemsToGet, parameters))
//            {
//                GetSuggestionsFunction currentGetSuggestionsFunction = getSuggestionsFunction;
//                int currentPriority = priority;
//                yield return () => CollateSuggestions(currentGetSuggestionsFunction, maxNumberOfItemsToGet, currentPriority);
//                priority--;
//            }
//        }
//        protected abstract IEnumerable<GetSuggestionsFunction> GetGetSuggestionsFunctions(Usr currentUsr, string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters);
//        void CollateSuggestions(GetSuggestionsFunction func, int maxNumberOfItemsToGet, int priority)
//        {
//            var newSuggestions = func();
//            lock (suggestions)
//            {
//                List<Suggestion> suggestionsToSendDownTheWire = new List<Suggestion>();
//                int counter = 0;
//                foreach (var newSuggestion in newSuggestions)
//                {
//                    newSuggestion.priority = priority * maxNumberOfItemsToGet + counter++;
//                    suggestions.Add(newSuggestion);
//                    suggestionsToSendDownTheWire.Add(newSuggestion);

//                }
//                WriteMessage(javaScriptSerializer.Serialize(suggestionsToSendDownTheWire.ToArray()));
//            }
//        }
		
		

//        public override void HandleActionException(Exception ex, Action action)
//        {
//            SpottedException.TryToSaveExceptionAndChildExceptions(ex);
//        }
//    }
//}
