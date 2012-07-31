using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class TopPhoto : Item
	{
		public int PhotoK;
		public string PhotoUrl;
		public string PhotoIcon;
		public string PhotoWeb;
		public int PhotoWebWidth;
		public int PhotoWebHeight;
		public string PhotoThumb;
		public int PhotoThumbWidth;
		public int PhotoThumbHeight;

		public TopPhoto(TopPhotoStub topPhotoStub, Controller parent)
			: base(topPhotoStub, parent) 
		{
			PhotoK = topPhotoStub.photoK;
			PhotoUrl = topPhotoStub.photoUrl;
			PhotoIcon = topPhotoStub.photoIcon;
			PhotoWeb = topPhotoStub.photoWeb;
			PhotoWebWidth = topPhotoStub.photoWebWidth;
			PhotoWebHeight = topPhotoStub.photoWebHeight;
			PhotoThumb = topPhotoStub.photoThumb;
			PhotoThumbWidth = topPhotoStub.photoThumbWidth;
			PhotoThumbHeight = topPhotoStub.photoThumbHeight;
		}

	}
}
