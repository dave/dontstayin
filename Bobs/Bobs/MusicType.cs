using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
namespace Bobs
{

	#region MusicType
	/// <summary>
	/// Music types (heirachy)
	/// </summary>
	[Serializable] 
	public partial class MusicType : ISerializable
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[MusicType.Columns.K] as int? ?? 0; }
			set { this[MusicType.Columns.K] = value; }
		}
		/// <summary>
		/// Name - e.g. House, Techno, Drum'n'Bass
		/// </summary>
		public override string Name
		{
			get { return (string)this[MusicType.Columns.Name]; }
			set { this[MusicType.Columns.Name] = value; }
		}
		/// <summary>
		/// If the music type has a parent, this is true
		/// </summary>
		public bool HasParent
		{
			get { return (ParentK > 0); }
		}
		/// <summary>
		/// Link to parent MusicType
		/// </summary>
		public override int ParentK
		{
			get { return (int)this[MusicType.Columns.ParentK]; }
			set { parent = null; this[MusicType.Columns.ParentK] = value; }
		}
		/// <summary>
		/// Generic name - e.g. Name="All Techno" GenericName="Techno"
		/// </summary>
		public override string GenericName
		{
			get { return (string)this[MusicType.Columns.GenericName]; }
			set { this[MusicType.Columns.GenericName] = value; }
		}
		/// <summary>
		/// Order - list order
		/// </summary>
		public override double Order
		{
			get { return (double)this[MusicType.Columns.Order]; }
			set { this[MusicType.Columns.Order] = value; }
		}
		/// <summary>
		/// Music type codesd used in incoming sms's
		/// </summary>
		public override string SmsCode
		{
			get { return (string)this[MusicType.Columns.SmsCode]; }
			set { this[MusicType.Columns.SmsCode] = value; }
		}
		/// <summary>
		/// Sort names used in outgoing sms's
		/// </summary>
		public override string SmsName
		{
			get { return (string)this[MusicType.Columns.SmsName]; }
			set { this[MusicType.Columns.SmsName] = value; }
		}
		/// <summary>
		/// Description for main music types
		/// </summary>
		public override string Description
		{
			get { return (string)this[MusicType.Columns.Description]; }
			set { this[MusicType.Columns.Description] = value; }
		}
		#endregion

		#region RadioButtonText
		public string RadioButtonText
		{
			get
			{
				string desc = this.Description;
				if (desc.Length>0)
					desc = "<p style=\"margin-left:26px;margin-top:0px;margin-bottom:10px;\">e.g. "+desc+"</p>";
				return "<span style=\"font-size:14px;font-weight:bold;\">&nbsp;"+this.GenericName+"</span>"+desc;
			}
		}
		#endregion

		public static Join UsrFavouriteJoin
		{
			get
			{
				return new Join(
					new Join(MusicType.Columns.K,UsrMusicTypeFavourite.Columns.MusicTypeK),
					Usr.Columns.K,
					UsrMusicTypeFavourite.Columns.UsrK);
			}
		}
		public static Join EventMusicTypeJoin
		{
			get
			{
				return new Join(MusicType.Columns.K,EventMusicType.Columns.MusicTypeK);
			}
		}
		public static Join BannerJoin
		{
			get
			{
				return new Join(new Join(MusicType.Columns.K,BannerMusicType.Columns.MusicTypeK),Banner.Columns.K,BannerMusicType.Columns.BannerK);
			}
		}
		public static Join EventJoin
		{
			get
			{
				return new Join(
					EventMusicTypeJoin,
					new TableElement(Bobs.TablesEnum.Event),
					QueryJoinType.Inner,
					new Q(EventMusicType.Columns.EventK,Event.Columns.K,true)
				);
			}
		}
		public static Join EventAllJoin
		{
			get
			{
				return new Join(EventMusicTypeJoin,Event.Columns.K,EventMusicType.Columns.EventK);
			}
		}
	 
		public static Q HasParentQueryCondition
		{
			get
			{
				return new Q(MusicType.Columns.ParentK,QueryOperator.NotEqualTo,0);
			}
		}
		public static OrderBy OrderBy
		{
			get
			{
				return new OrderBy(MusicType.Columns.Order);
			}
		}

		public string DescriptiveText
		{
			get
			{
				string txt = this.GenericName;
				if (this.Description.Length>0)
					txt += " ("+this.Description+")";
				return txt;
			}
		}
		
		#region Links to BobSets
		#region Children
		public MusicTypeSet Children
		{
			get
			{
				if (children==null)
				{
					Query q = new Query();
					q.QueryCondition=new Q(MusicType.Columns.ParentK,K);
					q.OrderBy=MusicType.OrderBy;
					q.CacheDuration = TimeSpan.FromDays(1);
					children = new MusicTypeSet(q);
				}
				return children;
			}
			set{children=value;}
		}
		MusicTypeSet children;
		#endregion
		#region Parent
		public MusicType Parent
		{
			get
			{
				if (!HasParent)
					return null;
				if (parent==null)
					parent = new Bobs.MusicType(ParentK);
				return parent;
			}
			set{parent=value;}
		}

		static IEnumerable<KeyValuePair<string, int>> musicTypes;
		public static IEnumerable<KeyValuePair<string, int>> MusicTypes
		{
			get
			{
				return musicTypes ??
				       (musicTypes = new MusicTypeSet(
				                     	new Query()
				                     	{
				                     		OrderBy = new OrderBy(MusicType.Columns.Order),
				                     		Columns = new ColumnSet(MusicType.Columns.Name, MusicType.Columns.K)
				                     	}).Select(mt => new KeyValuePair<string, int>(mt.Name, mt.K))
				       );

			}
		}

		MusicType parent;
		#endregion
		#endregion


		internal static List<int> GetChildMusicTypeKs(int parentK)
		{
			return Caching.Instances.Main.GetWithLocalCaching("MusicType.GetChildMusicTypeKs(parentK = " + parentK.ToString() + ")",
				() => {
					Query query = new Query();
					query.Columns = new ColumnSet(MusicType.Columns.K);
					query.QueryCondition = new Q(MusicType.Columns.ParentK, parentK);
					MusicTypeSet mts = new MusicTypeSet(query);
					return mts.ToList().ConvertAll(mt => mt.K);
				},
				Common.Time.Minutes(1),
				Common.Time.Minutes(15)
			);
		}
		public static List<int> GetChildMusicTypeKs(IList<int> parentKs)
		{
			List<int> childMusicTypeKs = new List<int>();
			foreach (int pK in parentKs)
			{
				foreach (int cK in GetChildMusicTypeKs(pK))
				{
					if (!childMusicTypeKs.Contains(cK)) childMusicTypeKs.Add(cK);
				}
			}
			return childMusicTypeKs;
		}
		public static List<int> GetTheseAndChildMusicTypeKs(IList<int> parentKs)
		{
			List<int> list = GetChildMusicTypeKs(parentKs);
			foreach (var k in parentKs)
			{
				if (!list.Contains(k)) list.Add(k);
			}
			if (!list.Contains(1)) list.Add(1); //always add All Music
			return list;
		}
		public static List<int> GetAllApplicableMusicTypeKs(IList<int> musicTypeKs)
		{
			List<int> list = GetTheseAndChildMusicTypeKs(musicTypeKs);
			foreach (MusicType mt in musicTypeKs.Select(mK => new MusicType(mK)))
			{
				if (!list.Contains(mt.ParentK ))
				{
					list.Add(mt.ParentK);
				}
			}
			return list;
		}

		#region MusicTypeSetAsQueryList
		internal static List<Q> MusicTypeSetAsQueryList(MusicTypeSet musicTypeSet, object columnToMatch)
		{
			List<Q> qList = new List<Q>();
			foreach (MusicType m in Usr.Current.MusicTypesFavourite)
			{
				qList.Add(new Q(columnToMatch, m.K));
				AddChildren(qList, m);
				AddParents(qList, m);
			}
			return qList;
		}
		static void AddParents(List<Q> qList, MusicType mt)
		{
			if (mt.HasParent)
			{
				qList.Add(new Q(EventMusicType.Columns.MusicTypeK, mt.Parent.K));
				AddParents(qList, mt.Parent);
			}
		}
		static void AddChildren(List<Q> qList, MusicType mt)
		{
			if (mt.Children.Count > 0)
			{
				foreach (MusicType mtChild in mt.Children)
				{
					qList.Add(new Q(EventMusicType.Columns.MusicTypeK, mtChild.K));
					AddChildren(qList, mtChild);
				}
			}
		}
		#endregion

	 
	}
	#endregion

}
