﻿#if SCRIPTSHARP
using System;
using System.Html;
using jQueryApi;
#else
using System.Text;
using Bobs.StorageScriptCompatibility;
using Model.Entities;
#endif
using Js.Library;

namespace Js.Controls.EventBox
{

	public class EventPageDetails
	{

		#region Members
		public EventDetails[] Events;
#if SCRIPTSHARP
		public Controller Controller;
#endif
		public string ParentClientID;
		public string ClientID;
		public EventPageStub Data;
		public EventPageHtml Html;
		public bool RequestInProgress;
		public bool HasIncompleteEventData;
		public bool IsLoading;
		public bool IsEmpty;
		#endregion

		#region Selected
		public bool Selected
		{
			get
			{
				return selected;
			}
			set
			{
				if (selected != value)
				{
					selected = value;
					Html.UpdateUI();
				}
			}
		}
		private bool selected;
		#endregion

		#region GetKey
		public string GetKey()
		{
			return GetKeyStatic(Data);
		}
		#endregion

		#region GetKeyStatic
		public static string GetKeyStatic(EventPageStub data)
		{
			return
				((int)data.parentObjectType).ToString() + "_" +
				data.parentObjectK.ToString() + "_" +
				((int)data.tabType).ToString() + "_" +
				data.musicTypeK.ToString() + "_" +
				data.pageIndex.ToString();
		}
		#endregion

		//#region GetKeyStatic
		//public static string GetKeyStatic(
		//    ObjectType parentObjectType,
		//    int parentObjectK,
		//    DateType dateType,
		//    int musicTypeK,
		//    int pageIndex)
		//{
		//    return
		//        ((int)parentObjectType).ToString() + "_" +
		//        parentObjectK.ToString() + "_" +
		//        ((int)dateType).ToString() + "_" +
		//        musicTypeK.ToString() + "_" +
		//        pageIndex.ToString();
		//}
		//#endregion

		#region public EventPageDetails
		public EventPageDetails(
#if SCRIPTSHARP
Controller controller,
#else
			string parentClientID,
			Bobs.EventSet es,
#endif
			EventPageStub data,
			bool isLoading
			)
		{
			Data = data;
			IsLoading = isLoading;

#if SCRIPTSHARP
			Controller = controller;
			ParentClientID = Controller.ClientID;
#else
			ParentClientID = parentClientID;
#endif
			ClientID = ParentClientID + "_" + GetKey();
			
			Events = new EventDetails[8];
#if SCRIPTSHARP
#else
			Data.events = new EventStub[8];
#endif
			bool gotEvent = false;
			bool gotNullEvent = false;
			for (int i = 0; i < 8; i++)
			{
#if SCRIPTSHARP
				Events[i] = new EventDetails(i, this, Data.events != null && Data.events.Length > i && Data.events[i] != null ? Data.events[i] : null, isLoading);
				if (Data.events == null || Data.events.Length <= i || Data.events[i] == null)
					gotNullEvent = true;
				else
					gotEvent = true;
#else
				Events[i] = new EventDetails(i, this, es != null && es.Count > i && es[i] != null ? es[i] : null, isLoading);
				Data.events[i] = Events[i].Data;
				if (es == null || es.Count <= i || es[i] == null)
					gotNullEvent = true;
				else
					gotEvent = true;
#endif
			}
			HasIncompleteEventData = gotNullEvent;
			IsEmpty = !gotEvent;

			Html = new EventPageHtml(this);
		}
		#endregion

#if SCRIPTSHARP
#else
		public string Serialize()
		{
			System.Web.Script.Serialization.JavaScriptSerializer d = new System.Web.Script.Serialization.JavaScriptSerializer();
			return d.Serialize(Data);
		}
#endif



		public static EventPageStub GetStubFromKey(string key)
		{
			string[] keyArr = key.Split('_');
#if SCRIPTSHARP
			return new EventPageStub(
				(ObjectType)int.Parse(keyArr[0]),
				int.Parse(keyArr[1]),
				(TabType)int.Parse(keyArr[2]),
				int.Parse(keyArr[3]),
				int.Parse(keyArr[4]),
				int.Parse(keyArr[4]),
				null);
#else
			
			return new EventPageStub(
				(ObjectType)int.Parse(keyArr[0]),
				int.Parse(keyArr[1]),
				(TabType)int.Parse(keyArr[2]),
				int.Parse(keyArr[3]),
				int.Parse(keyArr[4]),
				int.Parse(keyArr[4]),
				null);
#endif
		}


#if SCRIPTSHARP
		#region ChangeSelectedState(bool state, bool animate)
		public void ChangeSelectedState(bool state, bool animate, string direction)
		{
			if (selected == state)
				return;

			selected = state;

			if (!state)
			{
				if (direction == "left")
					direction = "right";
				else if (direction == "right")
					direction = "left";
				else if (direction == "up")
					direction = "down";
				else if (direction == "down")
					direction = "up";
			}

			//if (animate && Controller.EnableEffects)
			//{
			//    //need to put logic in here to ensure we don't start one animation before another finishes...
			//    if (state)
			//    {
			//        Controller.PerformOrQueueAnimationTask(new Action[] { new Action(delegate() { Html.ShowAnimate(direction); }) }, "EventPage_ShowAnimate");
			//    }
			//    else
			//    {
			//        Controller.PerformOrQueueAnimationTask(new Action[] { new Action(delegate() { Html.HideAnimate(direction); }), new Action(delegate() { Html.HideImmediate(); }) }, "EventPage_HideAnimate");
			//    }
			//}
			//else
			//{
				if (state)
				{
					Html.ShowImmediate();
				}
				else
				{
					Html.HideImmediate();
				}
			//}
		}
		#endregion
#endif		
		public string GetEventsIconsHtml()
		{
			StringBuilderJs sb = new StringBuilderJs();
			for (int i = 0; i < 8; i++)
			{
				Events[i].Html.AppendHtmlIcon(sb);
			}
			return sb.ToString();
		}

	}


	#region EventPageHtml
	public class EventPageHtml
	{
		#region Members
		EventPageDetails Details;
		public bool ElementsInitialised;
		public string HolderClientID;
		#endregion

		#region EventPageHtml(EventPageDetails details)
		public EventPageHtml(EventPageDetails details)
		{
			Details = details;
			HolderClientID = Details.ParentClientID + "_EventPage_" + Details.GetKey() + "_Holder";
		}
		#endregion

		#region AppendHtml
		public void AppendHtml(StringBuilderJs sb)
		{
			sb.Append(@"<div");
			sb.AppendAttribute("id", HolderClientID);
			sb.AppendAttribute("class", Details.Selected ? "EventBoxPageHolder Selected" : "EventBoxPageHolder");
			sb.Append(@">");
			{
				AppendHtmlInner(sb);
			}
			sb.Append(@"</div>");
		}
		public void AppendHtmlInner(StringBuilderJs sb)
		{
			for (int i = 0; i < 8; i++)
			{
				Details.Events[i].Html.AppendHtmlIcon(sb);
			}
		}
		public string ToHtml()
		{
			StringBuilderJs sb = new StringBuilderJs();
			AppendHtml(sb);
			return sb.ToString();
		}
		public string ToHtmlInner()
		{
			StringBuilderJs sb = new StringBuilderJs();
			AppendHtmlInner(sb);
			return sb.ToString();
		}
		#endregion

#if SCRIPTSHARP
		#region Hide
		public void HideImmediate()
		{
			HolderElement.Style.Display = "none";
		}
		//public void HideAnimate(string direction)
		//{
		//    Dictionary<object, object> options = new Dictionary<object, object>();
		//    options["direction"] = direction;
		//    options["easing"] = "easeOutQuint";
		//    HolderJQ.hide("drop", options, 500, new Action(delegate() { Details.Controller.FinishedAnimationTask("EventPage_HideAnimate"); }));
		//}
		#endregion
		#region Show
		public void ShowImmediate()
		{
			HolderElement.Style.Display = "block";
		}
		//public void ShowAnimate(string direction)
		//{
		//    HolderElement.Style.Display = "block";

		//    Dictionary<object, object> options = new Dictionary<object, object>();
		//    options["direction"] = direction;
		//    options["easing"] = "easeOutQuint";
		//    HolderJQ.show("drop", options, 500, new Action(delegate() { Details.Controller.FinishedAnimationTask("EventPage_ShowAnimate"); }));
		//}
		#endregion
#endif

#if SCRIPTSHARP
		#region Elements
		public Element HolderElement;
		#endregion
		#region JQuery handles
		public jQueryObject HolderJQ;
		#endregion
		#region InitialiseElements()
		public void InitialiseElements(
			bool initialiseHolder,

			bool createIconsHtml, 
			bool refreshIconsHtml, 
			bool initialiseIcons, 

			bool createInfoHtml, 
			bool refreshInfoHtml, 
			bool initialiseInfo)
		{

			if (createIconsHtml)
			{
				Element element = Document.CreateElement("div");
				element.InnerHTML = ToHtml();
				Details.Controller.View.EventIconsHolder.AppendChild(element);
			}

			if (initialiseHolder)
			{
				HolderElement = Document.GetElementById(HolderClientID);
				HolderJQ = jQuery.FromElement(HolderElement);
			}

			if (refreshIconsHtml)
			{
				HolderElement.InnerHTML = Details.GetEventsIconsHtml();
			}

			if (initialiseIcons || createInfoHtml || refreshInfoHtml || initialiseInfo)
			{
				for (int i = 0; i < 8; i++)
					Details.Events[i].Html.InitialiseElements(initialiseIcons, createInfoHtml, refreshInfoHtml, initialiseInfo);
			}

			ElementsInitialised = true;
			UpdateUI();
		}
		#endregion
#endif

		#region UpdateUI()
#if SCRIPTSHARP
		public void UpdateUI()
		{
			if (ElementsInitialised)
			{
				UpdateHolderUI();
			}
		}
		public void UpdateHolderUI()
		{
			HolderElement.ClassName = Details.Selected ? "EventBoxPageHolder Selected" : "EventBoxPageHolder";
		}
#else
		public void UpdateUI()
		{
		}
#endif
		#endregion

	}
	#endregion

	#region EventPageStub
	public class EventPageStub
	{
		#region Members
		public ObjectType parentObjectType;
		public int parentObjectK;
		public TabType tabType;
		public int musicTypeK;
		public int pageIndex;
		public int requestedPageIndex;
		public EventStub[] events;
		#endregion
		public EventPageStub(
			ObjectType parentObjectType,
			int parentObjectK,
			TabType tabType,
			int musicTypeK,
			int pageIndex,
			int requestedPageIndex,
			EventStub[] events)
		{
			this.parentObjectType = parentObjectType;
			this.parentObjectK = parentObjectK;
			this.tabType = tabType;
			this.musicTypeK = musicTypeK;
			this.pageIndex = pageIndex;
			this.requestedPageIndex = requestedPageIndex;
			this.events = events;
		}

		
	}
	#endregion

}
