using Bobs;

namespace Spotted.Controls
{
	public partial class NewUserWizardOptions : System.Web.UI.UserControl
	{
		public enum Option
		{
			FreeGuestlist,
			FindYourPhoto,
			UploadPhotos,
			FindEvents,
			FindYourFriends,
			AddEvent,
			None
		}
		//public bool ShowFriendsLink { get { return !(((Master.DsiPage)Page).ContentUserControl is Pages.Home) || !Vars.Creamfields2; } }
		//public bool ShowTicketsLink { get { return !(((Master.DsiPage)Page).ContentUserControl is Pages.Home); } }

		//public bool ShowFriendsLink { get { return true; } }
		//public bool ShowTicketsLink { get { return true; } }

		//public string TdWidth { get { return ShowTicketsLink ? "20%" : ShowFriendsLink ? "25%" : "33%"; } }
		public Option SelectedOption
		{
			get
			{
				if (((Master.DsiPage)Page).ContentUserControl is Pages.FindYourPhoto) return Option.FindYourPhoto;
				if (((Master.DsiPage)Page).ContentUserControl is Pages.UploadPhotos) return Option.UploadPhotos;
				if (((Master.DsiPage)Page).ContentUserControl is Pages.FindEvents) return Option.FindEvents;
				if (((Master.DsiPage)Page).ContentUserControl is Pages.FindYourFriends) return Option.FindYourFriends;
				if (((Master.DsiPage)Page).ContentUserControl is Pages.FreeGuestlist) return Option.FreeGuestlist;
				return Option.None;
			}
		}
	}
}
