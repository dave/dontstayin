using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.TaggingControl;
using Bobs.Tagging;
using Bobs;
using Common;

namespace SpottedLibrary.Controls.PhotoBrowserControl
{
	//[Serializable]
	//public class PhotoBrowserItem 
	//{
	//    public PhotoBrowserItem() :
	//        this(0, Guid.Empty, "", "") { }

	//    public PhotoBrowserItem(Photo p, Getter<string, Photo> getLink) :
	//        this(p.K, p.Icon, p.RolloverMouseOverText, getLink(p)) { }

	//    private PhotoBrowserItem(int k, Guid iconGuid, string rolloverMouseOverText, string linkUrl)
	//    {
	//        this.IconPath = Storage.Path(iconGuid);
	//        this.K = k;
	//        this.Highlight = false;
	//        this.RolloverMouseOverText = rolloverMouseOverText;
	//        this.Link = linkUrl;
	//    }
	//    public string IconPath { get; private set; }
	//    public int K { get; private set; }
	//    public bool Highlight { get; set; }
	//    public string RolloverMouseOverText { get; private set; }
	//    public string Link { get; private set; }

	//    public static ColumnSet RequiredColumns = new ColumnSet(Photo.Columns.K, Photo.Columns.Icon, Photo.Columns.ContentDisabled);


	//}
}
