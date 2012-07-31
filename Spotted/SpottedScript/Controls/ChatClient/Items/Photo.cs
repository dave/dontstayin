using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Photo : Newable
	{
		
		public int Width;
		public int Height;
		public string Url;
		public string Web;
		public string Icon;
		public string Thumb;
		public int ThumbWidth;
		public int ThumbHeight;
		public bool BuddyAlert;

		string ImageID;
		DOMElement ImageElement;

		public Photo(PhotoStub photoStub, Controller parent, int serverRequestIndex)
			: base(photoStub, parent, serverRequestIndex)
		{
			Width = photoStub.width;
			Height = photoStub.height;
			Url = photoStub.url;
			Web = photoStub.web;
			Icon = photoStub.icon;
			Thumb = photoStub.thumb;
			ThumbWidth = photoStub.thumbWidth;
			ThumbHeight = photoStub.thumbHeight;
			BuddyAlert = photoStub.buddyAlert;

			ImageID = ClientID + "_Image";
		}

		public override void InitialiseElements()
		{
			initialiseElementsInternal(true);
		}
		protected override void initialiseElementsInternal(bool setElementsInitialisedFlagOnFinish)
		{
			base.initialiseElementsInternal(false);
			ImageElement = Document.GetElementById(ImageID);

			if (setElementsInitialisedFlagOnFinish)
				elementsInitialised = true;
		}

		override protected void updateUI()
		{
			updateItem();
		}
		void updateItem()
		{
			if (elementsInitialised)
			{
				//ItemElement.Style.Height = Size.ToString() + "px";
			}
		}

		int Size
		{
			get
			{
				return IsInNewSection ? 200 : 50;
			}
		}

		int Top
		{
			get
			{
				//int top = (Height - Size) / 2;
				//if (top + Size + 57 > Height)
				//    top = Height - Size - 57;
				//if (top < 0)
				//    top = 0;
				//return 0 - top;

				return (Size - 300) / 2;
			}
		}

		int Left
		{
			get
			{
				//int left = 0;
				//if (Width < 300)
				//{
				//    left = (300 - Width) / 2;
				//}
				//else
				//{
				//    left = 0 - ((Width - 300) / 2);
				//}
				//return left;

				return 0;
			}
		}

		public override void AppendHtml(StringBuilder sb)
		{
			sb.Append(@"<div class=""ChatMessagePhotoHolder""");
			sb.AppendAttribute("id", ClientID);
			//sb.AppendAttribute("style", "height:" + Size.ToString() + "px");
			sb.Append(@">");

			sb.Append(@"<a");
			sb.AppendAttribute("href", Url);
			sb.AppendAttribute("onclick", "event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } document.location = \"" + Url + "\";return false;");
			sb.Append(@">");

			sb.Append(@"<img");
			sb.AppendAttribute("id", ImageID);
			//sb.AppendAttribute("src", Misc.GetPicUrlFromGuid(Web));
			//sb.AppendAttribute("width", Width.ToString());
			//sb.AppendAttribute("height", Height.ToString());
			sb.AppendAttribute("src", Misc.GetPicUrlFromGuid(Thumb));
			sb.AppendAttribute("width", ThumbWidth.ToString());
			sb.AppendAttribute("height", ThumbHeight.ToString());

			sb.AppendAttribute("class", "ChatClientPhotoImage");
			sb.AppendAttribute("style", "top:" + Top.ToString() + "px; left: " + Left.ToString() + "px;");
			sb.AppendAttribute("border", "0");
			//sb.AppendAttribute("onmouseover", "stm('<img src=" + Misc.GetPicUrlFromGuid(Web) + " width=" + Width.ToString() + " height=" + Height.ToString() + " class=Block />');");
			//sb.AppendAttribute("onmouseout", "htm();");
			sb.Append(@" />");

			sb.Append(@"</a>"); //Anchor

			sb.Append(@"</div>"); //PhotoHolder

		}

		
	}
	
}
