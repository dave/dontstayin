using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region Setting
	/// <summary>
	/// Configuration settings
	/// </summary>
	[Serializable] 
	public partial class Setting
	{

		#region Simple members
		/// <summary>
		/// Name of the Setting
		/// </summary>
		public override string Name
		{
			get { throw new NotImplementedException(); }
			set { this[Setting.Columns.Name] = value; }
		}
		/// <summary>
		/// Value of the Setting
		/// </summary>
		public override object Value
		{
			get { return (object)this[Setting.Columns.Value]; }
			set { this[Setting.Columns.Value] = value; }
		}
		#endregion

	}
	#endregion

}
