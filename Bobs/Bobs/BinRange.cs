using System;
using System.Collections.Generic;
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
using System.Xml;

namespace Bobs
{

	#region BinRange
	/// <summary>
	/// e.g. Promoter / Event BinRange
	/// </summary>
	[Serializable] 
	public partial class BinRange
	{

		#region simple members
		/// <summary>
		/// The bottom number of the range
		/// </summary>
		public override int Low
		{
			get { return (int)this[BinRange.Columns.Low]; }
			set { this[BinRange.Columns.Low] = value; }
		}
		/// <summary>
		/// The top number of the range
		/// </summary>
		public override int High
		{
			get { return (int)this[BinRange.Columns.High]; }
			set { this[BinRange.Columns.High] = value; }
		}
		/// <summary>
		/// The card type: None=0, Delta=1, Electron=2, VisaPurchasing=3, Visa=4, MasterCard=5, Switch=6, Solo=7, JCB=8, Maestro=9
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[BinRange.Columns.Type]; }
			set { this[BinRange.Columns.Type] = value; }
		}
		/// <summary>
		/// Order they are retreived from the database (for generics)
		/// </summary>
		public override int Order
		{
			get { return (int)this[BinRange.Columns.Order]; }
			set { this[BinRange.Columns.Order] = value; }
		}
		#endregion

		#region Types

		public static string TypeToString(Types cardType)
		{
			return Utilities.CamelCaseToString(cardType.ToString());
		}
		#endregion

		#region Enums To ListItem[]
		public static ListItem[] TypesAsListItemArray()
		{
			List<ListItem> ListItems = new List<ListItem>();

			// TODO: Setup DSI to accept American Express cards
			//ListItems[0] = new ListItem(CardTypes.AmericanExpress.ToString(), Convert.ToInt32(CardTypes.AmericanExpress).ToString());

			ListItems.Add(new ListItem(TypeToString(Types.Delta), Convert.ToInt32(Types.Delta).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.Electron), Convert.ToInt32(Types.Electron).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.Maestro), Convert.ToInt32(Types.Maestro).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.MasterCard), Convert.ToInt32(Types.MasterCard).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.Solo), Convert.ToInt32(Types.Solo).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.Switch), Convert.ToInt32(Types.Switch).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.Visa), Convert.ToInt32(Types.Visa).ToString()));
			ListItems.Add(new ListItem(TypeToString(Types.VisaPurchasing), Convert.ToInt32(Types.VisaPurchasing).ToString()));

			return ListItems.ToArray();
		}
		#endregion
	}
	#endregion

}
