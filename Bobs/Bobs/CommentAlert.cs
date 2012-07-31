using System;
using System.Collections.Generic;
using System.Text;
using Bobs.Jobs;

namespace Bobs
{
	#region CommentAlert
	/// <summary>
	/// Links an event to many MusicTypes - the Music types that will be played
	/// </summary>
	[Serializable]
	public partial class CommentAlert 
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[CommentAlert.Columns.UsrK]; }
			set { usr = null; this[CommentAlert.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Key of parent object
		/// </summary>
		public override int ParentObjectK
		{
			get { return (int)this[CommentAlert.Columns.ParentObjectK]; }
			set { parentObject = null; this[CommentAlert.Columns.ParentObjectK] = value; }
		}
		/// <summary>
		/// Photo=1, Event=2, Venue=4, Place=5, Thread=6
		/// </summary>
		public override Model.Entities.ObjectType ParentObjectType
		{
			get { return (Model.Entities.ObjectType)this[CommentAlert.Columns.ParentObjectType]; }
			set { parentObject = null; this[CommentAlert.Columns.ParentObjectType] = value; }
		}
		#endregion

		public CommentAlert(int UsrK, Model.Entities.ObjectType ParentObjectType, int ParentObjectK)
			: this()
		{
			Bob.GetBobFromPrimaryKeyArray(new Q[] { new Q(CommentAlert.Columns.UsrK, UsrK), new Q(CommentAlert.Columns.ParentObjectType, ParentObjectType), new Q(CommentAlert.Columns.ParentObjectK, ParentObjectK) });
		}

		public static void Enable(Usr CurrentUsr, int ParentObjectK, Model.Entities.ObjectType ParentObjectType)
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.Thread))
			{
				Thread t = new Thread(ParentObjectK);
				bool changed = false;
				if (t.CheckPermissionRead(CurrentUsr))
				{
					ThreadUsr tu = t.GetThreadUsr(CurrentUsr);

					if (!tu.IsWatching)
					{
						tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
						changed = true;
					}

					if (changed)
					{
						UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob();
						job.ExecuteAsynchronously();
					}
				}
				return;
			}

			try
			{
				CommentAlert c = new CommentAlert(CurrentUsr.K, ParentObjectType, ParentObjectK);
			}
			catch
			{
				CommentAlert c = new CommentAlert();
				c.UsrK = CurrentUsr.K;
				c.ParentObjectK = ParentObjectK;
				c.ParentObjectType = ParentObjectType;
				c.Update();
			}
		}
		public static void Disable(Usr CurrentUsr, int ParentObjectK, Model.Entities.ObjectType ParentObjectType)
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.Thread))
			{
				Thread t = new Thread(ParentObjectK);
				bool changed = false;
				if (t.CheckPermissionRead(CurrentUsr))
				{
					ThreadUsr tu = t.GetThreadUsr(CurrentUsr);

					tu.ChangeStatus(ThreadUsr.StatusEnum.Ignore, true);
					changed = true;

					if (changed)
					{
						UpdateTotalParticipantsJob job = new UpdateTotalParticipantsJob();
						job.ExecuteAsynchronously();
					}
				}
				return;
			}

			try
			{
				CommentAlert c = new CommentAlert(CurrentUsr.K, ParentObjectType, ParentObjectK);
				c.Delete();
				c.Update();
			}
			catch { }
		}
		public static bool IsEnabled(int UsrK, int ParentObjectK, Model.Entities.ObjectType ParentObjectType)
		{
			if (ParentObjectType.Equals(Model.Entities.ObjectType.Thread))
			{
				try
				{
					ThreadUsr tu = new ThreadUsr(ParentObjectK, UsrK);
					return tu.IsWatching;
				}
				catch
				{
					return false;
				}
			}

			try
			{
				CommentAlert c = new CommentAlert(UsrK, ParentObjectType, ParentObjectK);
				return true;
			}
			catch
			{
				return false;
			}
		}



		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK, this, CommentAlert.Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region ParentObject
		public IBob ParentObject
		{
			get
			{
				if (parentObject == null)
					switch (ParentObjectType)
					{
						case Model.Entities.ObjectType.Photo:
							parentObject = new Photo(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Event:
							parentObject = new Event(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Venue:
							parentObject = new Venue(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Place:
							parentObject = new Place(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Thread:
							parentObject = new Thread(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Article:
							parentObject = new Article(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Brand:
							parentObject = new Brand(ParentObjectK);
							break;
						case Model.Entities.ObjectType.Group:
							parentObject = new Group(ParentObjectK);
							break;
						default:
							break;
					}
				return parentObject;
			}
		}
		IBob parentObject;
		#endregion
		#endregion

		#region Typed Parent Accessors
		public Photo ParentPhoto
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Photo))
					return null;
				else
					return (Photo)ParentObject;
			}
		}
		public Event ParentEvent
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Event))
					return null;
				else
					return (Event)ParentObject;
			}
		}
		public Venue ParentVenue
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
					return null;
				else
					return (Venue)ParentObject;
			}
		}
		public Place ParentPlace
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Place))
					return null;
				else
					return (Place)ParentObject;
			}
		}
		public Thread ParentThread
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Thread))
					return null;
				else
					return (Thread)ParentObject;
			}
		}
		public Brand ParentBrand
		{
			get
			{
				if (!ParentObjectType.Equals(Model.Entities.ObjectType.Brand))
					return null;
				else
					return (Brand)ParentObject;
			}
		}
		#endregion

	}
	#endregion
}
