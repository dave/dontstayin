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
	public partial class AddOnlyTextBox : System.Web.UI.UserControl, IPostBackDataHandler
	{
		public enum InsertOptions
		{
			AddAtBeginning = 0,
			AddAtEnd = 1,
			Overwrite = 2
		}

		private string timeStampFormat = "";
		private string authorName = "";
		private string delimiter = "\n";
		private InsertOptions insertOption = InsertOptions.AddAtEnd;

		#region Properties
		public string TimeStampFormat
		{
			get { return this.timeStampFormat; }
			set { this.timeStampFormat = value; }
		}
		public string AuthorName
		{
			get { return this.authorName; }
			set { this.authorName = value; }
		}
		public string Delimiter
		{
			get { return this.delimiter; }
			set { this.delimiter = value; }
		}
		public InsertOptions InsertOption
		{
			get { return this.insertOption; }
			set { this.insertOption = value; }
		}
		public TextBox ReadOnlyTextBox
		{
			get { return this.readOnlyTextBox; }
			set { this.readOnlyTextBox = value; }
		}
		public TextBox AddTextBox
		{
			get { return this.addTextBox; }
			set { this.addTextBox = value; }
		}
		#endregion

		protected override void LoadControlState(object savedState)
		{
			base.LoadControlState(savedState);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			Page.RegisterRequiresPostBack(this);
		}
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["AddOnlyTextBox_TimeStampFormat"] = this.TimeStampFormat;
			this.ViewState["AddOnlyTextBox_AuthorName"] = this.AuthorName;
			this.ViewState["AddOnlyTextBox_Delimiter"] = this.Delimiter;
			this.ViewState["AddOnlyTextBox_InsertOption"] = this.InsertOption;

			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["AddOnlyTextBox_TimeStampFormat"] != null) TimeStampFormat = (string)this.ViewState["AddOnlyTextBox_TimeStampFormat"];
			if (this.ViewState["AddOnlyTextBox_AuthorName"] != null) AuthorName = (string)this.ViewState["AddOnlyTextBox_AuthorName"];
			if (this.ViewState["AddOnlyTextBox_Delimiter"] != null) Delimiter = (string)this.ViewState["AddOnlyTextBox_Delimiter"];
			if (this.ViewState["AddOnlyTextBox_InsertOption"] != null) InsertOption = (InsertOptions)this.ViewState["AddOnlyTextBox_InsertOption"];
	
		}
		#endregion

		protected void AddButton_Click(object sender, EventArgs e)
		{

		}
		
		public void AddNote(string note, string authorName, string readOnlyTextBoxText)
		{
			if (note.Length > 0)
			{
				string prefix = "";
				if (TimeStampFormat.Length > 0)
					prefix = "(" + DateTime.Now.ToString(TimeStampFormat) + ") ";

				if (AuthorName.Length > 0)
					prefix += AuthorName + ": ";

				if (this.InsertOption.Equals(InsertOptions.AddAtBeginning))
					this.ReadOnlyTextBox.Text = prefix + note + Delimiter + readOnlyTextBoxText;
				else if (this.InsertOption.Equals(InsertOptions.AddAtEnd))
					this.ReadOnlyTextBox.Text += Delimiter + prefix + note;
				else if (this.InsertOption.Equals(InsertOptions.Overwrite))
					this.ReadOnlyTextBox.Text = prefix + note;

				this.addTextBox.Text = "";
			}
		}


		protected override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("div");
			writer.WriteAttribute("name", UniqueID);
			writer.Write(HtmlTextWriter.TagRightChar);
			base.Render(writer);
			writer.WriteEndTag("div");
		}


		#region IPostBackDataHandler Members

		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection)
		{
			string addTextBoxText = postCollection[this.AddTextBox.UniqueID];
			if (postCollection[this.AddButton.UniqueID] == "Add")
			{
				if (addTextBoxText.Length > 0)
				{
					this.AddNote(addTextBoxText, this.AuthorName, postCollection[this.ReadOnlyTextBox.UniqueID]);
					this.addTextBox.Text = "";
				}
				
			}
			return true;
		}

		public void RaisePostDataChangedEvent()
		{
		
		}

		#endregion
	}
}
