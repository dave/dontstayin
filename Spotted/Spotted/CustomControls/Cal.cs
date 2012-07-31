using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Spotted.CustomControls
{
	[ValidationProperty("Date")]
	[ClientScript]
	public class Cal : EnhancedHtmlControl, IPostBackDataHandler
	{
		/// <summary>
		/// Use this for getting the value of calendar,
		/// e.g. var date = document.getElementById("<%=myCalendar.InnerClientID%>").value;
		/// This is needed to also work in Firefox browsers.
		/// </summary>
		public Cal()
		{

			this.Load += new EventHandler(this.Cal_Load);
			this.PreRender += new EventHandler(this.Cal_PreRender);
			this.disabled = base.Disabled;
		}
	
		public string InnerClientID
		{
			get
			{
				return base.ClientID + "_inner";
			}
		}

		private bool disabled = false;
		//private bool isValidDate = true;



		public DateTime SelectedDate
		{
			get
			{
				return this.Date;
			}
			set
			{
				this.Date = value;
			}
		}

		#region Year
		public int Year
		{
			get
			{
				return this.Date.Year;
			}
			set
			{
				this.Date = new DateTime(value, this.Date.Month, this.Date.Day);
			}
		}
		#endregion
		#region Month
		public int Month
		{
			get
			{
				return this.Date.Month;
			}
			set
			{
				this.Date = new DateTime(this.Date.Year, value, this.Date.Day);
			}
		}
		#endregion
		#region Day
		public int Day
		{
			get
			{
				return this.Date.Day;
			}
			set
			{
				this.Date = new DateTime(this.Date.Year, this.Date.Month, value);
			}
		}
		#endregion

		#region Date
		public DateTime Date
		{
			get
			{
				if (this.ViewState["Date"] == null)
					this.ViewState["Date"] = DateTime.MinValue;

				return (DateTime)this.ViewState["Date"];
			}
			set
			{
				this.ViewState["Date"] = value;
			}
		}
		#endregion

		#region Disabled
		// Overrides default HTMLControl Disabled
		new public bool Disabled
		{
			get
			{
				return this.disabled;
			}
			set
			{
				base.Disabled = value;
				this.disabled = value;
			}
		}
		#endregion

		#region OnChange
		public string OnChange
		{
			get
			{
				return this.onChange;
			}
			set
			{
				this.onChange = value;
			}
		}
		string onChange = "";
		#endregion

		#region Cal_Load()
		private void Cal_Load(object sender, System.EventArgs e)
		{
			this.Page.RegisterRequiresPostBack(this);
		}
		#endregion
		#region Cal_PreRender()
		public void Cal_PreRender(object o, System.EventArgs e)
		{
			ScriptManager.RegisterClientScriptInclude(this, typeof(Page), "CalendarJs", "/misc/Calendar.js?a=1");

		}
		#endregion
		#region Render()
		protected override void Render(HtmlTextWriter w)
		{
			string dateInit = "";
			if (this.Date > DateTime.MinValue)
				dateInit = @", 'dateInit': new Date(" + this.Date.Year + @"," + ((int)(this.Date.Month - 1)).ToString() + @"," + this.Date.Day + @")";

			string onchange = "";
			if (this.OnChange.Length > 0)
				onchange = ", 'onChange': '" + this.OnChange.Replace("'", "\\'") + @"'";

			w.Write(
				@"<script language=""JavaScript"">

var " + this.ClientID + @"_GC_SET = { 'appearance': GC_APPEARANCE, 'disabled': '" + this.disabled.ToString() + "', 'dataArea': '" + this.InnerClientID + @"'" + dateInit + onchange + @" }
new gCalendar(" + this.ClientID + @"_GC_SET);

</script>");
			// If not valid from IsValidDate(), then show required field message
			if (this.showValidationError)
			{
				w.Write("<p><font color='Red'>* Must select date</font></p>");
			}

			base.Render(w);
		}
		#endregion
		#region RaisePostDataChangedEvent()
		public void RaisePostDataChangedEvent()
		{

		}
		#endregion

		//#region IsValidDate()
		//public bool IsValidDate()
		//{
		//    if (this.Date.Equals(DateTime.MinValue))
		//        this.isValidDate = false;
		//    else
		//        this.isValidDate = true;

		//    return this.isValidDate;
		//}
		//#endregion

		public bool DateValid
		{
			get
			{
				return !this.Date.Equals(DateTime.MinValue);
			}
		}

		public bool ValidateNow()
		{
			if (this.Date.Equals(DateTime.MinValue))
			{
				this.showValidationError = true;
				return false;
			}
			else
			{
				this.showValidationError = false;
				return true;
			}
		}
		bool showValidationError = false;

		#region LoadPostData()
		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			if (this.Visible)
			{
				try
				{
					this.Date = DateTime.Parse(postCollection[this.InnerClientID]);
				}
				catch { }
			}
			return false;
		}
		#endregion
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["Date"] = this.Date;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["Date"] != null) this.Date = (DateTime)this.ViewState["Date"];
		}
		#endregion
	}
}
