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
using System.Collections.Generic;
namespace Spotted.Blank
{
	public partial class BannerEditMusic : BlankUserControl
	{
		protected bool SaveWasClicked = false;
		protected void Page_Load(object sender, EventArgs e)
		{
			Page.Title = "Select music types for targetting";
			if (!IsPostBack)
			{
				string musicTypeKs = Request.QueryString["MusicTypeK"];
				if (musicTypeKs.Length > 0)
				{
					if (musicTypeKs.Substring(musicTypeKs.Length - 1) == "/")
					{
						musicTypeKs = musicTypeKs.Substring(0, musicTypeKs.Length - 1);
					}
				}
				else
				{
					musicTypeKs = "1";
				}
				int[] musicTypeKsAsInts = (new List<string>(musicTypeKs.Split(',')).ConvertAll(s => Convert.ToInt32(s))).ToArray();
				Bobs.MusicTypeSet musicTypeSet = new Bobs.MusicTypeSet(new Bobs.Query(new Bobs.Q(Bobs.MusicType.Columns.K, musicTypeKsAsInts)));
				uiMusicTypesControl.InitialMusicTypes = musicTypeSet;

				//bool showMajorMusicTypesOnly = false;
				//bool.TryParse(Request.QueryString["ShowMajorMusicTypesOnly"], out showMajorMusicTypesOnly);
				//uiMusicTypesControl.ShowMajorMusicTypesOnly = showMajorMusicTypesOnly;
				//uiMusicTypesControl.SetState();
				
			}
			
		}
		protected string GetCommaSeparatedStringOfMusicTypeKs()
		{
			return String.Join(",", this.uiMusicTypesControl.SelectedMusicTypes.ConvertAll(i => i.ToString()).ToArray());
		}
		protected void uiSaveButton_Click(Object sender, EventArgs e)
		{
			SaveWasClicked = true;
		}
		
	}
}
