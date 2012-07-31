#if SCRIPTSHARP
using System;
using System.Html;
using jQueryApi;
using System.Collections.Generic;
#else
using System.Text;
using Bobs.StorageScriptCompatibility;
#endif
using Js.Library;

namespace Js.Controls.EventBox
{
#if SCRIPTSHARP
#else
	[System.Serializable]
#endif
	public class EventDetails
	{
		#region Members
#if SCRIPTSHARP
		public Controller Controller;
#endif
		public EventPageDetails Page;
		public string ParentClientID;
		public EventStub Data;
		public EventHtml Html;
		public int PositionIndex;
		public bool HasData;
		public bool IsLoading;
		#endregion 

		#region public EventDetails(int positionIndex, Controller controller, [EventStub stub | Bobs.Event e])
		public EventDetails(
			int positionIndex, 
			EventPageDetails page,
#if SCRIPTSHARP
			EventStub e,
#else
			Bobs.Event e,
#endif
			bool isLoading
			)
		{
			PositionIndex = positionIndex;
			Page = page;
			ParentClientID = Page.ClientID;
			HasData = e != null;
			IsLoading = isLoading;


#if SCRIPTSHARP
			Controller = Page.Controller;
			Data = e;
#else
			if (e != null)
			{
				Data = new EventStub(
					e.K, 
					e.Name, 
					e.Url(), 
					e.VenueK, 
					e.Venue.Name, 
					e.Venue.Url(), 
					e.Venue.PlaceK, 
					e.Venue.Place.Name, 
					e.Venue.Place.Url(), 
					e.FriendlyDate(false), 
					e.AnyPic == System.Guid.Empty ? "00000000-0000-0000-b916-000000000001" : e.AnyPic.ToString(), 
					e.ShortDetailsHtml, 
					e.MusicTypesString, 
					e.UsrAttendCount,
					e.IsFuture);
			}
#endif

			Html = new EventHtml(this);
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

#if SCRIPTSHARP
		#region ChangeSelectedState(bool state, bool animate)
		public void ChangeSelectedState(bool state, bool animate, string direction)
		{
			if (selected == state)
				return;

			selected = state;

			Html.UpdateIconUI();

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
			//        Controller.PerformOrQueueAnimationTask(new Action[]{new Action(delegate() { Html.ShowInfoAnimate(direction); })}, "ShowInfoAnimate");
			//        if (this.HasData)
			//            Controller.PerformOrQueueAnimationTask(new Action[]{new Action(delegate() { Html.ResizeInfoHolderAnimate(); })}, "ResizeInfoHolderAnimate");
			//    }
			//    else
			//    {
			//        Controller.PerformOrQueueAnimationTask(new Action[] { new Action(delegate() { Html.HideInfoAnimate(direction); }), new Action(delegate() { Html.HideInfoImmediate(); }) }, "HideInfoAnimate");
			//    }
			//}
			//else
			//{
				if (state)
				{
					Html.ShowInfoImmediate();
					Html.ResizeInfoHolderImmediate();
				}
				else
				{
					Html.HideInfoImmediate();
				}
			//}
		}
		#endregion
#endif

	}
	
	#region EventHtml
	public class EventHtml
	{

		#region Members
		EventDetails Details;
		public bool ElementsInitialised;
		public string IconKeylineClientID;
		public string IconImageClientID;
		public string InfoHolderInnerClientID;
		public string InfoTextHolderClientID;
		#endregion

		#region EventHtml(EventDetails details)
		public EventHtml(EventDetails details)
		{
			Details = details;
			IconKeylineClientID = Details.ParentClientID + "_Event_" + Details.PositionIndex + "_Icon_Keyline";
			IconImageClientID = Details.ParentClientID + "_Event_" + Details.PositionIndex + "_Icon_Image";
			InfoHolderInnerClientID = Details.ParentClientID + "_Event_" + Details.PositionIndex + "_Info_Holder";
			InfoTextHolderClientID = Details.ParentClientID + "_Event_" + Details.PositionIndex + "_Info_TextHolder";
		}
		#endregion

		#region AppendHtmlIcon
		public void AppendHtmlIcon(StringBuilderJs sb)
		{
			sb.Append(@"<div");
			sb.AppendAttribute("class", "EventBoxIconHolder");
			sb.Append(@">");
			{
				sb.Append(@"<div");
				sb.AppendAttribute("id", IconKeylineClientID);
				sb.AppendAttribute("class", Details.Selected ? "EventBoxIconKeyline Selected" : "EventBoxIconKeyline");
				sb.Append(@">&nbsp;");
				sb.Append(@"</div>");

			//	if (Details.HasData)
			//	{
			//		sb.Append(@"<a");
			//		sb.AppendAttribute("href", Details.Data.eventUrl);
			//		sb.Append(@">");
			//	}
				{
					sb.Append(@"<img");
					sb.AppendAttribute("id", IconImageClientID);
					if (Details.HasData)
					{
						sb.AppendAttribute("src", Misc.GetPicUrlFromGuid(Details.Data.eventPicGuid));
						sb.AppendAttribute("class", "BorderBlack All EventBoxIconImage");
					}
					else
					{
						sb.AppendAttribute("src", "/gfx/1pix.gif");
					}
					sb.AppendAttribute("width", "50");
					sb.AppendAttribute("height", "50");
					sb.Append(@"/>");
				}
			//	if (Details.HasData)
			//	{
			//		sb.Append(@"</a>");
			//	}
			}
			sb.Append(@"</div>");

		}
		public string ToHtmlIcon()
		{
			StringBuilderJs sb = new StringBuilderJs();
			AppendHtmlIcon(sb);
			return sb.ToString();
		}
		#endregion
		#region AppendHtmlInfo
		public void AppendHtmlInfo(StringBuilderJs sb)
		{
			sb.Append(@"<div");
			sb.AppendAttribute("id", InfoHolderInnerClientID);
			sb.AppendAttribute("class", "EventBoxInfoHolderInner");
			sb.Append(@">");
			{
				AppendHtmlInfoInner(sb);
			}
			sb.Append(@"</div>");

		}
		public void AppendHtmlInfoInner(StringBuilderJs sb)
		{
			sb.Append(@"<div");
			sb.AppendAttribute("id", InfoTextHolderClientID);
			sb.AppendAttribute("class", "EventBoxInfoTextHolder");
			sb.Append(@">");
			{
				if (Details.HasData)
				{
					sb.Append(@"<a");
					sb.AppendAttribute("href", Details.Data.eventUrl);
					sb.Append(@">");
					{
						sb.Append(@"<img");
						sb.AppendAttribute("src", Misc.GetPicUrlFromGuid(Details.Data.eventPicGuid));
						sb.AppendAttribute("class", "BorderBlack All EventBoxInfoImage");
						sb.AppendAttribute("width", "100");
						sb.AppendAttribute("height", "100");
						sb.AppendAttribute("align", "right");
						sb.Append(@"/>");
					}
					sb.Append(@"</a>");

					sb.Append(@"<b>");
					{
						sb.Append(@"<a");
						sb.AppendAttribute("href", Details.Data.eventUrl);
						sb.Append(@">");
						sb.Append(Details.Data.eventName);
						sb.Append(@"</a>");
					}
					sb.Append(@"</b>");

					sb.Append(@" @ ");

					sb.Append(@"<a");
					sb.AppendAttribute("href", Details.Data.venueUrl);
					sb.Append(@">");
					sb.Append(Details.Data.venueName);
					sb.Append(@"</a>");

					sb.Append(@" in ");

					sb.Append(@"<a");
					sb.AppendAttribute("href", Details.Data.placeUrl);
					sb.Append(@">");
					sb.Append(Details.Data.placeName);
					sb.Append(@"</a>");

					sb.Append(@", ");
					sb.Append(Details.Data.friendlyDateString);
					sb.Append(@".<br />");

					sb.Append(Details.Data.eventShortDescription);
					sb.Append(@"<br />");

					sb.Append(@"(");
					sb.Append(Details.Data.eventMusicText);
					sb.Append(@")");
					sb.Append(@"<br />");

					sb.Append(Details.Data.eventMembersAttending.ToString());
					sb.Append(Details.Data.eventMembersAttending == 1 ? " member" : " members");

					sb.Append(Details.Data.eventInInTheFuture ? @" attending" : @" attended");
				}
				else if (Details.IsLoading)
				{
					sb.Append(@"<center>Loading details...</center>");
				}
				else if (Details.Page.IsEmpty)
				{
					sb.Append(@"<center>Sorry, no events - <a href=""/pages/events/edit"">click here to add one</a>.</center>");
				}
			}
			sb.Append(@"</div>");
		}
		public string ToHtmlInfo()
		{
			StringBuilderJs sb = new StringBuilderJs();
			AppendHtmlInfo(sb);
			return sb.ToString();
		}
		public string ToHtmlInfoInner()
		{
			StringBuilderJs sb = new StringBuilderJs();
			AppendHtmlInfoInner(sb);
			return sb.ToString();
		}
		#endregion

#if SCRIPTSHARP
		#region HideInfo
		public void HideInfoImmediate()
		{
			InfoHolderInnerElement.Style.Display = "none";
		}
		//public void HideInfoAnimate(string direction)
		//{
		//    Dictionary<object, object> options = new Dictionary<object, object>();
		//    options["direction"] = direction;
		//    options["easing"] = "easeOutQuint";
		//    InfoHolderInnerJ.hide("drop", options, 250, new Action(delegate() { Details.Controller.FinishedAnimationTask("HideInfoAnimate"); }));
		//}
		#endregion
		#region ShowInfo
		public void ShowInfoImmediate()
		{
			InfoHolderInnerElement.Style.Display = "block";
		}
		//public void ShowInfoAnimate(string direction)
		//{
		//    InfoHolderInnerElement.Style.Display = "block";

		//    Dictionary<object, object> options = new Dictionary<object, object>();
		//    options["direction"] = direction;
		//    options["easing"] = "easeOutQuint";
		//    InfoHolderInnerJ.show("drop", options, 250, new Action(delegate() { ResizeInfoHolderImmediate(); Details.Controller.FinishedAnimationTask("ShowInfoAnimate"); }));

		//}
		#endregion
		#region ResizeInfoHolder
		public void ResizeInfoHolderImmediate()
		{
			Details.Controller.View.EventInfoHolderOuter.Style.Height = getInfoHeight().ToString() + "px";
		}
		//public void ResizeInfoHolderAnimate()
		//{
		//    Dictionary<object, object> options = new Dictionary<object, object>();
		//    options["height"] = getInfoHeight().ToString() + "px";
		//    Details.Controller.EventInfoHolderOuterJ.Animate(options, 250, "easeOutQuint", new Action(delegate() { Details.Controller.FinishedAnimationTask("ResizeInfoHolderAnimate"); }));
		//}
		public int getInfoHeight()
		{
			int textHeight = InfoTextHolderJQ.GetHeight();
			textHeight = textHeight > 100 ? textHeight : 100;
			return textHeight;
		}
		#endregion
#endif

#if SCRIPTSHARP
		#region Elements
		public Element IconKeylineElement;
		public Element IconImageElement;
		public Element InfoHolderInnerElement;
		public Element InfoTextHolderElement;
		#endregion
		#region JQuery handles
		public jQueryObject InfoHolderInnerJQ;
		public jQueryObject InfoTextHolderJQ;
		#endregion
		#region InitialiseElements()
		public void kill(Element element)
		{
			if (element == null)
				return;

			while (element.ChildNodes.Length > 0)
			{
				kill(element.ChildNodes[element.ChildNodes.Length - 1]);
			}
			if (element.ParentNode != null)
				element.ParentNode.RemoveChild(element);
		}
		public void InitialiseElements(
			bool initialiseIcon, 
			bool createInfoHtml, 
			bool refreshInfoHtml, 
			bool initialiseInfo)
		{
			if (initialiseIcon)
			{
				IconKeylineElement = Document.GetElementById(IconKeylineClientID);
				IconImageElement = Document.GetElementById(IconImageClientID);

				jQuery.FromElement(IconImageElement).MouseOver(Details.Controller.EventIconMouseOver);
				jQuery.FromElement(IconImageElement).MouseOver(Details.Controller.EventIconMouseOut);
			}

			if (createInfoHtml)
			{
				InfoHolderInnerElement = Document.GetElementById(InfoHolderInnerClientID);
				//if (InfoHolderInnerElement != null)
				//	InfoHolderInnerElement.ParentNode.RemoveChild(InfoHolderInnerElement); //remove the info holder if it already exists (for first page only)

				kill(InfoHolderInnerElement);

				InfoHolderInnerElement = Document.CreateElement("div");
				InfoHolderInnerElement.InnerHTML = ToHtmlInfo();
				Details.Controller.View.EventInfoHolderOuter.AppendChild(InfoHolderInnerElement);
			}

			if (refreshInfoHtml)
			{
				InfoHolderInnerElement = Document.GetElementById(InfoHolderInnerClientID);
				InfoHolderInnerElement.InnerHTML = ToHtmlInfoInner();
			}

			if (initialiseInfo)
			{
				InfoHolderInnerElement = Document.GetElementById(InfoHolderInnerClientID);
				InfoHolderInnerJQ = jQuery.FromElement(InfoHolderInnerElement);

				InfoTextHolderElement = Document.GetElementById(InfoTextHolderClientID);
				InfoTextHolderJQ = jQuery.FromElement(InfoTextHolderElement);
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
				UpdateIconUI();
				UpdateInfoHolderUI();
			}
		}
		public void UpdateIconUI()
		{
			IconKeylineElement.ClassName = Details.Selected && !Details.Page.IsEmpty ? "EventBoxIconKeyline Selected" : "EventBoxIconKeyline";
		}
		public void UpdateInfoHolderUI()
		{
			InfoHolderInnerElement.Style.Display = Details.Selected ? "block" : "none";
		}
#else
		public void UpdateUI()
		{
		}
#endif
		#endregion


	}
	#endregion

	#region EventStub
	public class EventStub
	{
		#region Members
		public int eventK;
		public string eventName;
		public string eventUrl;
		public int venueK;
		public string venueName;
		public string venueUrl;
		public int placeK;
		public string placeName;
		public string placeUrl;
		public string friendlyDateString;
		public string eventPicGuid;
		public string eventShortDescription;
		public string eventMusicText;
		public int eventMembersAttending;
		public bool eventInInTheFuture;
		#endregion
		public EventStub(
			int eventK,
			string eventName,
			string eventUrl,
			int venueK,
			string venueName,
			string venueUrl,
			int placeK,
			string placeName,
			string placeUrl,
			string friendlyDateString,
			string eventPicGuid,
			string eventShortDescription,
			string eventMusicText,
			int eventMembersAttending,
			bool eventInInTheFuture)
		{
			this.eventK = eventK;
			this.eventName = eventName;
			this.eventUrl = eventUrl;
			this.venueK = venueK;
			this.venueName = venueName;
			this.venueUrl = venueUrl;
			this.placeK = placeK;
			this.placeName = placeName;
			this.placeUrl = placeUrl;
			this.friendlyDateString = friendlyDateString;
			this.eventPicGuid = eventPicGuid;
			this.eventShortDescription = eventShortDescription;
			this.eventMusicText = eventMusicText;
			this.eventMembersAttending = eventMembersAttending;
			this.eventInInTheFuture = eventInInTheFuture;
		}
	}
	#endregion

}
