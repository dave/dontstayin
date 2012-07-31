using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Spotted.Controls
{
	public partial class TimeControl : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.HourTextBox.Style.Add("text-align", "right");
		}

		#region Properties
		public DateTime Time
		{
			get
			{
				int hour = 0;
				int minute = 0;
				try
				{
					hour = Convert.ToInt32(this.HourTextBoxControl.Text);
				}
				catch { }
				try
				{
					minute = Convert.ToInt32(this.MinuteTextBoxControl.Text);
				}
				catch { }

				return new DateTime(1900, 1, 1, hour, minute,0);
			}
			set
			{
				this.HourTextBoxControl.Text = value.Hour.ToString();
				this.MinuteTextBoxControl.Text = value.Minute.ToString("00");
			}
		}

		public int Hour
		{
			get
			{
				int hour = 0;
				try
				{
					hour = Convert.ToInt32(this.HourTextBoxControl.Text);
					if (hour < 0 || hour > 23)
						hour = 0;
				}
				catch { }

				return hour;
			}
			set
			{
				this.HourTextBoxControl.Text = value.ToString();
			}
		}

		public int Minute
		{
			get
			{
				int minute = 0;
				try
				{
					minute = Convert.ToInt32(this.MinuteTextBoxControl.Text);
					if (minute < 0 || minute > 59)
						minute = 0;
				}
				catch { }

				return minute;
			}
			set
			{
				this.MinuteTextBoxControl.Text = value.ToString("00");
			}
		}

		public TextBox HourTextBox
		{
			get
			{
				return this.HourTextBoxControl;
			}
		}

		public TextBox MinuteTextBox
		{
			get
			{
				return this.MinuteTextBoxControl;
			}
		}
		#endregion

		#region Methods
		public bool IsValid(bool allowBlankEntries)
		{
			try
			{
				int hour = allowBlankEntries && HourTextBoxControl.Text.Length == 0 ? 0 : int.Parse(HourTextBoxControl.Text);
				int minute = allowBlankEntries && MinuteTextBoxControl.Text.Length == 0 ? 0 : int.Parse(MinuteTextBoxControl.Text);
				DateTime dt = new DateTime(1900, 1, 1, hour, minute, 0);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
		#endregion
	}
}
