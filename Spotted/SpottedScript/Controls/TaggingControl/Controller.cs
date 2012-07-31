using System;
using Sys;
using System.DHTML;
using Sys.UI;
using Sys.Net;
using ScriptSharpLibrary;
using Spotted.WebServices.Controls.TaggingControl;
using Utils;

namespace SpottedScript.Controls.TaggingControl
{
	public class Controller
	{
		private View view;
		private int currentPhotoK;
		internal int CurrentPhotoK
		{
			private get
			{
				return currentPhotoK;
			}
			set
			{
				currentPhotoK = value;
				displayTags();
			}
		}
		private Dictionary tagSets;

		public Controller(View view)
		{
			this.view = view;
			tagSets = new Dictionary();

			if (view.uiAddTagButton != null)// else display isn't rendered - user prob not logged in
			{
				DomEvent.AddHandler(view.uiAddTagButton, "click", addTagButtonClick);
				view.uiTagAutoSuggest.ItemChosen = new KeyStringPairAction(addTagFromAutoSuggest);
			}
		}

		private void addTagButtonClick(DomEvent e)
		{
			e.PreventDefault();

			addTag(view.uiTagAutoSuggest.Text);
			view.uiTagAutoSuggest.Text = "";
		}
		private void addTagFromAutoSuggest(KeyStringPair pair)
		{
			addTag(pair.Value);
			view.uiTagAutoSuggest.Text = "";
		}

		#region Add tag Web request
		private void addTag(string tagText)
		{
			Service.AddTagToPhoto(tagText, currentPhotoK, addTagSuccessCallback, Trace.WebServiceFailure, null, -1);
		}
		private void addTagSuccessCallback(TagStub tag, object context, string methodName)
		{
			if (tag != null)
			{
				((Dictionary)tagSets[CurrentPhotoK.ToString()])[tag.k.ToString()] = tag;
				displayTags();
			}
		}
		#endregion

		internal void LoadTagsForPhotoKs(int[] photoKs)
		{
			Array photoKsNotGot = getPhotoKsNotAlreadyLoaded(photoKs);
			if (photoKsNotGot.Length > 0)
				// this will fire displayTags once done
				Service.GetTagsForPhotoKs((int[])photoKsNotGot, getTagsForPhotoKsSuccessCallback, Trace.WebServiceFailure, null, -1);
			else
				displayTags();
		}
		private void getTagsForPhotoKsSuccessCallback(Dictionary photoTags, object context, string methodName)
		{
			foreach (DictionaryEntry photoTag in photoTags)
			{
				tagSets[photoTag.Key] = photoTag.Value;
			}
			displayTags();
		}

		private void displayTags()
		{
			setTags((Dictionary)tagSets[CurrentPhotoK.ToString()]);
		}
		private Array getPhotoKsNotAlreadyLoaded(int[] photoKs)
		{
			Array notLoaded = new Array();
			int index = 0;

			for (int i = 0; i < photoKs.Length; i++)
			{
				bool found = false;
				foreach (DictionaryEntry d in tagSets)
				{
					if (d.Key == photoKs[i].ToString())
					{
						found = true;
						break;
					}
				}

				if (!found)
				{
					notLoaded[index] = photoKs[i];
					index++;
				}
			}

			return notLoaded;
		}

		private void setTags(Dictionary tags)
		{
			view.uiTagsDiv.Style.Display = "none";
			view.uiTagsDivServerSide.Style.Display = "none";

			if (tags != null)
			{
				view.uiTagsDiv.InnerHTML = ""; // too hacky? RemoveChildNodes instead?
				foreach (DictionaryEntry tag in tags)
				{
					view.uiTagsDiv.Style.Display = "";
					addTagToTagsDiv((TagStub)tag.Value);
				}
			}
		}
		private void addTagToTagsDiv(TagStub tag)
		{
			view.uiTagsDiv.AppendChild(createTagDiv(tag));
		}
		private void removeTagClicked(DomEvent e)
		{
			if (Script.Confirm("Are you sure you want to remove the tag \"" + ((ImageElement)e.Target).GetAttribute("tagText").ToString() + "\" from this photo?"))
			{
				int tagK = (int)e.Target.GetAttribute("tagK");
				Service.RemoveTagFromPhoto(tagK, currentPhotoK,
					removeTagSuccessCallback, Trace.WebServiceFailure, tagK, -1);
			}
		}
		private void removeTagSuccessCallback(object nullObject, object tagK, string methodName)
		{
			((Dictionary)tagSets[currentPhotoK.ToString()]).Remove(tagK.ToString());
			displayTags();
		}

		private DivElement createTagDiv(TagStub tag)
		{
			DivElement span = (DivElement)Document.CreateElement("span");
			AnchorElement a = (AnchorElement)Document.CreateElement("a");
			a.InnerHTML = tag.tagText;
			a.Href = "/tags/" + tag.tagText;
			a.Style.PaddingLeft = "3px";
			a.Style.PaddingRight = "3px";
			span.AppendChild(a);

			ImageElement img = (ImageElement)Document.CreateElement("img");
			img.Src = "/gfx/minus.gif";
			img.Alt = "X";
			img.Title = "Remove this tag";
			img.ClassName = "RemoveTagButton";
			img.Style.BorderWidth = "0px";
			img.Style.Cursor = "hand";
			img.SetAttribute("tagText", tag.tagText);
			img.SetAttribute("tagK", tag.k);
			span.AppendChild(img);

			DomEvent.AddHandler(img, "click", new DomEventHandler(removeTagClicked));

			return span;
		}

	}
}
