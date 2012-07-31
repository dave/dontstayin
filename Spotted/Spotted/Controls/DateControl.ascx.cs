using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Spotted.Controls
{
	public partial class DateControl : System.Web.UI.UserControl
	{
		public bool ShowDay
		{
			get { return (ViewState["ShowDay"] != null) ? (bool)ViewState["ShowDay"] : false; }
			set { ViewState["ShowDay"] = value; }
		}
		private bool dateInitilized = false;
		private void InitializeDate()
		{
			if (!dateInitilized)
			{
				ReInitializeDate();
			}
		}
		public void ReInitializeDate()
		{
			if (this.InitialDate != DateTime.MinValue)
			{
				if (ShowDay)
					this.uiDay.SelectedValue = this.InitialDate.Day.ToString();
				uiMonth.SelectedValue = this.InitialDate.Month.ToString();
				uiYear.Text = this.InitialDate.Year.ToString();
			}
			else
			{
				if (ShowDay)
					this.uiDay.SelectedValue = DateTime.Today.Day.ToString();
				uiMonth.SelectedValue = DateTime.Today.Month.ToString();
				uiYear.Text = DateTime.Today.Year.ToString();
			}
			dateInitilized = true;
		}
		public override void DataBind()
		{
			if (!ShowDay)
			{
				this.uiDayTd.Visible = false;
				this.uiSetDaysInMonthScript.Visible = false;
			}
			else
			{
				this.uiDayTd.Visible = true;
				this.uiSetDaysInMonthScript.Visible = true;

				if (uiDay.Items.Count == 0)
				{
					this.uiDay.Visible = true;

					// set up values so at least there's a value to preselect
					for (int i = 1; i < 32; i++)
					{
						uiDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
					}
					this.uiMonth.Attributes["onclick"] = "setDaysInMonth();";
					this.uiYear.Attributes["onchange"] = "setDaysInMonth();";
				}
			}
			if (uiMonth.Items.Count == 0)
			{
				for (DateTime month = new DateTime(1, 1, 1); month < new DateTime(2, 1, 1); month = month.AddMonths(1))
				{
					uiMonth.Items.Add(new ListItem(month.ToString("MMM"), month.Month.ToString()));
				}
			}

			InitializeDate();
		}
		public void Page_PreRender(object o, EventArgs e)
		{
			if (ShowDay)
				ScriptManager.RegisterStartupScript(Page, typeof(Page), this.ClientID + "_initDays", "setDaysInMonth();", true);
		}

		#region SelectedYear
		public int SelectedYear
		{
			get
			{
				if (selectedYear == 0)
				{
					try
					{
						selectedYear = int.Parse(uiYear.Text);
					}
					catch { }
				}
				return selectedYear;
			}
		}
		int selectedYear = 0;
		#endregion
		#region SelectedDay
		public int SelectedDay
		{
			get
			{
				if (!ShowDay)
				{
					throw new ArgumentException("ShowDay", "cannot be false if using SelectedDay");
				}
				if (selectedDay == 0)
				{
					int.TryParse(uiDay.SelectedValue, out selectedDay);
				}
				return selectedDay;
			}
		}
		private int selectedDay = 0;
		#endregion
		#region SelectedMonth
		public int SelectedMonth
		{
			get
			{
				if (selectedMonth == 0)
				{
					try
					{
						selectedMonth = int.Parse(uiMonth.SelectedValue);
						if (selectedMonth > 12)
							selectedMonth = 12;
						if (selectedMonth < 1)
							selectedMonth = 1;
					}
					catch { }
				}
				return selectedMonth;
			}
		}
		int selectedMonth = 0;
		#endregion
		#region SelectedDate
		public DateTime SelectedDate
		{
			get
			{
				if (selectedDate == DateTime.MinValue && this.SelectedYear > 0 && this.SelectedMonth > 0 && (!ShowDay || this.SelectedDay > 0))
				{
					selectedDate = new DateTime(this.SelectedYear, this.SelectedMonth, ShowDay ? this.SelectedDay : 1);
				}
				return selectedDate;
			}
		}
		private DateTime selectedDate;
		#endregion

		#region InitialDate
		private DateTime initialDate;
		public DateTime InitialDate
		{
			get
			{
				if (initialDate == DateTime.MinValue && ViewState["InitialDate"] != null)
				{
					initialDate = (DateTime)ViewState["InitialDate"];
				}
				return initialDate;
			}
			set
			{
				initialDate = value;
				ViewState["InitialDate"] = value;
			}
		}
		#endregion
	}


}
