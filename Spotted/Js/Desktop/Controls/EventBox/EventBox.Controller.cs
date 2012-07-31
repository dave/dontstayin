using System.Html;
using System;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using jQueryApi.History;

namespace Js.Controls.EventBox
{
	public class Controller
	{

		#region Members
		public View View;
		public ServerClass Server;
		public ObjectType InitParentObjectType;
		public int InitParentObjectK;
		public int InitMusicTypeK;
		public TabType InitTabType;
		public int InitPageIndex;

		public ObjectType CurrentParentObjectType;
		public int CurrentParentObjectK;
		public int CurrentMusicTypeK;
		public TabType CurrentTabType;
		public int CurrentPageIndex;
		
		public string ClientID;
		public bool EnableEffects;

		Dictionary<object, object> EventPageCache;

		public jQueryObject EventInfoHolderOuterJ;
		public Element EventInfoHolderOuterElement;

		public EventDetails CurrentlySelectedEvent;

		#endregion

		#region public Controller(View view)
		/// <summary>
		/// Don't put anything that affects the DOM in here - it should be in the initialise method
		/// </summary>
		public Controller(View view)
		{
			//Application.EnableHistory = true;
			View = view;
			Server = new ServerClass(this);
			Server.GotEventPage = new EventHandler(gotEventPage);
			Server.GotGenericException = new EventHandler(gotGenericException);

			if (Misc.BrowserIsIE)
				jQuery.OnDocumentReady(new Action(initialise));
			else
				initialise();
		}
		#endregion

		#region void initialise()
		/// <summary>
		/// Anything that affects the DOM goes in here.
		/// </summary>
		void initialise()
		{

			jQuery.Window.Bind("hashchange", Application_Navigate);
			
			//Application.Navigate += new HistoryEventHandler(Application_Navigate);
			
			#region Initialisation variables
			ClientID = View.InitClientID.Value;
			EnableEffects = bool.Parse(View.InitEnableEffects.Value);
			#endregion

			#region JQuery handles
			EventInfoHolderOuterJ = View.EventInfoHolderOuterJ;
			#endregion 

			View.EventIconsNavigationForwardHolderJ.Click(pageChangeForwardClick);
			View.EventIconsNavigationBackHolderJ.Click(pageChangeBackClick);
			View.MusicDropDownControl.View.DropDownJ.Change(musicChangeClick);
			View.PastEventsTabJ.Click(tabClickPast);
			View.FutureEventsTabJ.Click(tabClickFuture);
			View.TicketsTabJ.Click(tabClickTickets);
			
			#region Event data cache, and initialise first page from json data in the page
			EventPageCache = new Dictionary<object, object>();

			EventPageStub firstPageData = ((EventPageStub[])Script.Eval(" [ " + View.InitFirstPage.Value + " ] "))[0];
			EventPageDetails firstPage = new EventPageDetails(this, firstPageData, false);

			firstPage.Selected = true;
			firstPage.Html.InitialiseElements(true, false, false, true, true, false, true);
			for (int i = 0; i < firstPage.Events.Length; i++)
			{
				firstPage.Events[i].ChangeSelectedState(i == 0, false, "");
			}
			CurrentlySelectedEvent = firstPage.Events[0];
			#endregion
			
			EventPageCache[firstPage.GetKey()] = firstPage;
			
			#region Set initial page spec
			InitParentObjectType = firstPage.Data.parentObjectType;
			InitParentObjectK = firstPage.Data.parentObjectK;
			InitTabType = firstPage.Data.tabType;
			InitMusicTypeK = firstPage.Data.musicTypeK;
			InitPageIndex = firstPage.Data.pageIndex;
			#endregion

			#region Set initial and current page spec - this should be done by looking at the page hash state data...
			CurrentParentObjectType = InitParentObjectType;
			CurrentParentObjectK = InitParentObjectK;
			CurrentTabType = InitTabType;
			CurrentMusicTypeK = InitMusicTypeK;
			CurrentPageIndex = InitPageIndex;
			#endregion

			if (Misc.BrowserIsIE)
			{
				jQueryHistory.PushState(new Dictionary<object, object>());
				//Application.AddHistoryPoint(new Dictionary<object, object>());
			}

		}

		void Application_Navigate(jQueryEvent e)
		{
			object keyOb = jQueryHistory.GetState("EventBox_PageKey", false);
			string key = keyOb == null ? "" : keyOb.ToString();

			if (key.Length > 0)
				restorePageState(key);
			else
				restorePageState(null);
		}
		#endregion

		void restorePageState(string key)
		{
			if (key == null)
			{
				EventPageDetails d = new EventPageDetails(this, new EventPageStub(InitParentObjectType, InitParentObjectK, InitTabType, InitMusicTypeK, InitPageIndex, InitPageIndex, null), false);
				key = d.GetKey();
			}

			#region Get the event page to the right place
			if (CurrentEventPage.GetKey() == key)
				return;

			CurrentEventPage.ChangeSelectedState(false, false, "");

			EventPageStub s = EventPageDetails.GetStubFromKey(key);

			CurrentParentObjectType = s.parentObjectType;
			CurrentParentObjectK = s.parentObjectK;
			CurrentMusicTypeK = s.musicTypeK;
			CurrentTabType = s.tabType;
			CurrentPageIndex = s.pageIndex;

			CurrentEventPage.ChangeSelectedState(true, false, "");

			ChangeEventNow(CurrentEventPage.Events[0], false, "");
			#endregion

			#region Set the music-type drop-down
			View.MusicDropDownControl.View.DropDown.Value = CurrentMusicTypeK.ToString();
			#endregion

			#region Set the future / past events tabs
			updateTabsUI();
			#endregion
		}

		#region pageChangeClick
		void pageChangeForwardClick(jQueryEvent e)
		{
			pageChange(true);
		}
		void pageChangeBackClick(jQueryEvent e)
		{
			pageChange(false);
		}
		void pageChange(bool forward)
		{
			int newPageIndex = CurrentPageIndex + (forward ? 1 : (CurrentPageIndex == 0 ? 0 : -1));

			if (CurrentPageIndex == newPageIndex)
				return;

			string movementDirection = forward ? "right" : "left";

			CurrentEventPage.ChangeSelectedState(false, true, movementDirection);

			CurrentPageIndex = newPageIndex;

			CurrentEventPage.ChangeSelectedState(true, true, movementDirection);

			ChangeEventNow(CurrentEventPage.Events[0], true, movementDirection);

			Dictionary<object, object> d = new Dictionary<object, object>();
			d["EventBox_PageKey"] = CurrentEventPage.GetKey();
			jQueryHistory.PushState(d);
			//Application.AddHistoryPoint(d, "Event box - page " + (newPageIndex + 1).ToString());

		}


		void tabClickPast(jQueryEvent e)
		{
			changeTabType(TabType.Past);
		}
		void tabClickFuture(jQueryEvent e)
		{
			changeTabType(TabType.Future);
		}
		void tabClickTickets(jQueryEvent e)
		{
			changeTabType(TabType.Tickets);
		}
		int getTabLocation(TabType tab)
		{
			return tab == TabType.Future ? 1 : tab == TabType.Past ? 2 : 3;
		}
		void changeTabType(TabType tabType)
		{
			if (CurrentTabType == tabType)
				return;

			int currentTab = getTabLocation(CurrentTabType);
			int newTab = getTabLocation(tabType);

			string movementDirection = "left";
			if (currentTab < newTab)
				movementDirection = "right";

			CurrentEventPage.ChangeSelectedState(false, true, movementDirection);

			CurrentPageIndex = 0;
			CurrentTabType = tabType;

			CurrentEventPage.ChangeSelectedState(true, true, movementDirection);

			ChangeEventNow(CurrentEventPage.Events[0], true, movementDirection);

			updateTabsUI();

			Dictionary<object, object> d = new Dictionary<object, object>();
			d["EventBox_PageKey"] = CurrentEventPage.GetKey();
			jQueryHistory.PushState(d);
			//Application.AddHistoryPoint(d, "Event box - " + (CurrentTabType == TabType.Future ? "future events" : CurrentTabType == TabType.Future ? "past events" : "tickets"));
		}
		void updateTabsUI()
		{
			View.FutureEventsTab.ClassName = CurrentTabType == TabType.Future ? "TabbedHeading Selected" : "TabbedHeading";
			View.PastEventsTab.ClassName = CurrentTabType == TabType.Past ? "TabbedHeading Selected" : "TabbedHeading";
			View.TicketsTab.ClassName = CurrentTabType == TabType.Tickets ? "TabbedHeading Selected" : "TabbedHeading";
		}
		
		void musicChangeClick(jQueryEvent e)
		{
			string movementDirection = "up";
			for (int i = 0; i < View.MusicDropDownControl.View.DropDown.Options.Length; i++)
			{
				OptionElement option = (OptionElement)View.MusicDropDownControl.View.DropDown.Options[i];
				if (option.Value == CurrentMusicTypeK.ToString())
				{
					movementDirection = "down";
					break;
				}
				if (option.Value == View.MusicDropDownControl.View.DropDown.Value)
				{
					break;
				}
			}

			CurrentEventPage.ChangeSelectedState(false, true, movementDirection);

			CurrentPageIndex = 0;
			CurrentMusicTypeK = int.Parse(View.MusicDropDownControl.View.DropDown.Value);

			CurrentEventPage.ChangeSelectedState(true, true, movementDirection);

			ChangeEventNow(CurrentEventPage.Events[0], true, movementDirection);

			Dictionary<object, object> d = new Dictionary<object, object>();
			d["EventBox_PageKey"] = CurrentEventPage.GetKey();
			jQueryHistory.PushState(d);
			//Application.AddHistoryPoint(d, "Event box - " + View.MusicDropDownControl.View.DropDown.Options[View.MusicDropDownControl.View.DropDown.SelectedIndex].InnerHTML);
		}
		#endregion

		#region AnimationTaskQueue
		Dictionary<object, object> animationTaskQueue = new Dictionary<object, object>();
		Dictionary<object, object> animationTasksInProgress = new Dictionary<object, object>();
		#region PerformOrQueueAnimationTask
		/// <summary>
		/// task is a Action[]. [0] is the default animation action. Optional [1] is the action to perform if we don't have time to do the animation.
		/// </summary>
		/// <param name="task"></param>
		/// <param name="taskType"></param>
		public void PerformOrQueueAnimationTask(Action[] task, string taskType)
		{
			if (animationTasksInProgress[taskType] == null || (bool)animationTasksInProgress[taskType] == false)
			{
				animationTasksInProgress[taskType] = true;
				Action action = task[0];
				action();
			}
			else
			{
				if (animationTaskQueue[taskType] != null)
				{
					//we're going to override this one...
					Action[] prevTask = (Action[])animationTaskQueue[taskType];
					if (prevTask.Length == 2)
					{
						Action action = prevTask[1];
						action();
					}
				}
				animationTaskQueue[taskType] = task;
			}
		}
		#endregion
		#region FinishedAnimationTask
		public void FinishedAnimationTask(string taskType)
		{
			if (animationTaskQueue[taskType] != null)
			{
				Action[] task = (Action[])animationTaskQueue[taskType];
				animationTasksInProgress[taskType] = true;
				Action action = task[0];
				action();
				animationTaskQueue[taskType] = null;
			}
			else
				animationTasksInProgress[taskType] = false;
		}
		#endregion
		#endregion

		#region EventIconMouseOver
		int EventIconMouseOverID = 0;
		int EventIconMouseOutID = 0;
		public void EventIconMouseOut(jQueryEvent e)
		{
			EventIconMouseOutID++;
		}
		public void EventIconMouseOver(jQueryEvent e)
		{
			EventIconMouseOverID++;
			int overID = EventIconMouseOverID;
			int outID = EventIconMouseOutID;
			//Window.SetTimeout(
				//new Callback(delegate() { EventIconMouseOverAfterDelay(e, overID, outID); }),
				//100);
			EventIconMouseOverAfterDelay(e, overID, outID);
		}
		public void EventIconMouseOverAfterDelay(jQueryEvent e, int mouseOverID, int mouseOutID)
		{
			if (EventIconMouseOverID != mouseOverID || EventIconMouseOutID != mouseOutID)
				return;

			EventDetails newSelectedEvent = findEventFromMouseOverEvent(e);

			if (newSelectedEvent.HasData)
				ChangeEventNow(newSelectedEvent, true, null);
		}
		public void ChangeEventNow(EventDetails newSelectedEvent, bool animate, string movementDirection)
		{
			if (newSelectedEvent != null && !newSelectedEvent.Selected)
			{
				if (movementDirection == null)
					movementDirection = CurrentlySelectedEvent == null ? "right" : newSelectedEvent.PositionIndex > CurrentlySelectedEvent.PositionIndex ? "right" : "left";

				if (CurrentlySelectedEvent != null)
				{
					CurrentlySelectedEvent.ChangeSelectedState(false, animate, movementDirection);
				}
				if (newSelectedEvent != null)
				{
					newSelectedEvent.ChangeSelectedState(true, animate, movementDirection);
				}

				CurrentlySelectedEvent = newSelectedEvent;
			}
		}
		
		#region findEventFromMouseOverEvent
		EventDetails findEventFromMouseOverEvent(jQueryEvent e)
		{
			try
			{
				Element el = e.Target;
				while (el != null)
				{
					if (el.ID.EndsWith("_Icon_Image"))
					{
						string[] parts = e.Target.ID.Split("_");
						int index = int.Parse(parts[parts.Length - 3]);
						return CurrentEventPage.Events[index];
					}
					else if (el.ParentNode != null)
					{
						el = el.ParentNode;
					}
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		#endregion
		#endregion

		#region CurrentEventPage
		public EventPageDetails CurrentEventPage
		{
			get
			{
				EventPageStub data = new EventPageStub(CurrentParentObjectType, CurrentParentObjectK, CurrentTabType, CurrentMusicTypeK, CurrentPageIndex, CurrentPageIndex, null);
				
				string key = EventPageDetails.GetKeyStatic(data);

				if (EventPageCache[key] == null)// ||
					//(((EventPageDetails)EventPageCache[key]).HasIncompleteEventData && !((EventPageDetails)EventPageCache[key]).RequestInProgress))
				{
					Server.GetEventPage(key);
				}

				if (EventPageCache[key] == null)
				{
					EventPageDetails eventPage = new EventPageDetails(this, data, true);
					eventPage.RequestInProgress = true;
					eventPage.Html.InitialiseElements(true, true, false, true, true, false, true);
					EventPageCache[key] = eventPage;
				}

				return (EventPageDetails)EventPageCache[key];
			}
		}
		#endregion

		#region gotEventPage
		public void gotEventPage(object o, EventArgs e)
		{
			if (o != null)
			{
				EventPageStub stub = (EventPageStub)o;
				{
					EventPageDetails newPage = new EventPageDetails(this, stub, false);

					updatePage(newPage);
				}

				if (stub.requestedPageIndex != stub.pageIndex)
				{
					//find requested page and mark it as empty...
					EventPageStub requestedStub = new EventPageStub(
						stub.parentObjectType,
						stub.parentObjectK,
						stub.tabType,
						stub.musicTypeK,
						stub.requestedPageIndex,
						stub.requestedPageIndex,
						null);
					EventPageDetails requestedPage = new EventPageDetails(this, requestedStub, false);

					updatePage(requestedPage);

				}

				


			}
		}
		void updatePage(EventPageDetails newPage)
		{
			string key = newPage.GetKey();

			if (EventPageCache[key] != null)
			{
				EventPageDetails page = (EventPageDetails)EventPageCache[key];
				if (page.HasIncompleteEventData)
				{
					if (page.Selected)
						newPage.Selected = true;

					bool selectedEventIsOnThisPage = false;
					for (int i = 0; i < 8; i++)
					{
						if (page.Events[i].Selected)
						{
							newPage.Events[i].Selected = true;
							CurrentlySelectedEvent = newPage.Events[i];
							selectedEventIsOnThisPage = true;
						}
					}

					newPage.Html.InitialiseElements(true, false, false, true, false, false, true); // just initialise elements (from old html)
					newPage.Html.InitialiseElements(false, false, true, true, false, true, true);  // now refresh new contents html

					if (selectedEventIsOnThisPage)
					{
						//if (EnableEffects)
						//    PerformOrQueueAnimationTask(new Action[] { new Action(delegate() { CurrentlySelectedEvent.Html.ResizeInfoHolderAnimate(); }) }, "ResizeInfoHolderAnimate");
						//else
							CurrentlySelectedEvent.Html.ResizeInfoHolderImmediate();
					}
				}
				else
					return;
			}
			else
			{
				newPage.Html.InitialiseElements(true, true, false, true, true, false, true);
			}
			EventPageCache[key] = newPage;
		}
		#endregion

		#region gotGenericException
		void gotGenericException(object o, EventArgs e)
		{
			GotExceptionEventArgs a = (GotExceptionEventArgs)e;

		//	if (a.Error.ExceptionType.EndsWith("+LoginPermissionException"))
		//		Script.Alert("Login error from the chat server... Are you logged in?");

			//Document.GetElementById("ErrorBox").InnerHTML = a.Error.Message;
			
		}
		#endregion

	}
	
	


}
