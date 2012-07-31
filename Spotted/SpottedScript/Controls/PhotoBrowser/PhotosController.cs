using Sys;
using Sys.UI;
using System;
using System.DHTML;
using PhotoControlController = SpottedScript.Controls.PhotoControl.Controller;
using PhotoBrowserController = SpottedScript.Controls.PhotoBrowser.Controller;
using ThreadControlController = SpottedScript.Controls.ThreadControl.Controller;
using PhotoBrowserPhotoProvider = SpottedScript.Controls.PhotoBrowser.PhotoProvider;
using LatestChatController = SpottedScript.Controls.LatestChat.Controller;


namespace SpottedScript.Controls.PhotoBrowser
{
	public abstract class PhotosController
	{
		protected abstract PhotoControlController PhotoControl { get; }
		protected abstract PhotoBrowserController PhotoBrowser { get; }
		protected abstract ThreadControlController ThreadControl { get; }
		protected abstract PhotoBrowserPhotoProvider PhotoProvider { get; }
		protected abstract LatestChatController LatestChatController { get; }

		protected void setupController()
		{
			PhotoProvider.DoPostLoadPhotoSetActions = PhotoBrowser.DoPostLoadPhotoSetActions;
			PhotoProvider.PhotoSetIsLoadingFromServer = PhotoBrowser.PhotoSetIsLoadingFromServer;
			PhotoProvider.LoadPhotos(PhotoBrowser.PaginationControl.CurrentPage);

			PhotoBrowser.PhotoProvider = PhotoProvider;
			
			PhotoBrowser.OnChangePhotoSet = PhotoControl.PhotoSetChanged;
			PhotoBrowser.OnChangePhoto = PhotoControl.PhotoChanged;
			PhotoControl.OnPhotoNextClick = PhotoBrowser.MoveToNextPhoto;
			PhotoControl.OnPhotoPrevClick = PhotoBrowser.MoveToPreviousPhoto;
			PhotoControl.OnPhotoUpClick = PhotoBrowser.MoveToPhotoAbove;
			PhotoControl.OnPhotoDownClick = PhotoBrowser.MoveToPhotoBelow;
			PhotoControl.OnPhotoChanged = photoChanged;
			PhotoControl.OnPhotoChangedAfterDelay = photoChangedAfterDelay;
			PhotoControl.OnRolloverMouseOverTextChanged = PhotoBrowser.RolloverMouseOverTextChanged;

			ThreadControl.OnThreadCreated = threadCreated;
			ThreadControl.OnCommentPosted = commentPosted;
			ThreadControl.uiCommentsDisplay.OnThreadDeleted = threadDeleted;
		}
		void photoChanged(object o, EventArgs e)
		{
			PhotoEventArgs p = (PhotoEventArgs)e;
			ThreadControl.uiCommentsDisplay.SetCommentsCount(p.Photo.commentsCount);
			LatestChatController.Hide();
			//Script.Eval(@"setTimeout(""try { chatClientChangePhoto('" + p.Photo.chatRoomGuid + @"'); } catch (e) { }"", 250);");
		}
		void photoChangedAfterDelay(object o, EventArgs e)
		{
			PhotoEventArgs p = (PhotoEventArgs)e;
			ThreadControl.uiCommentsDisplay.ShowComments(p.Photo.threadK, 1);
			ThreadControl.CurrentParentObjectK = p.Photo.k;
			LatestChatController.Show(p.Photo.k);
		}
		void threadCreated(object o, EventArgs e)
		{
			if (PhotoControl.CurrentPhoto.threadK == 0)
			{
				PhotoControl.CurrentPhoto.threadK = ((IntEventArgs)e).value;
			}
			PhotoControl.CurrentPhoto.commentsCount = 1;
			LatestChatController.Update(o, e);
		}
		void commentPosted(object o, EventArgs e)
		{
			if (PhotoControl.CurrentPhoto.threadK == ((IntEventArgs)e).value)
				PhotoControl.CurrentPhoto.commentsCount++;
		}

		void threadDeleted(object o, EventArgs e)
		{
			if (PhotoControl.CurrentPhoto.threadK == ((IntEventArgs)e).value)
			{
				PhotoControl.CurrentPhoto.threadK = 0;
				PhotoControl.CurrentPhoto.commentsCount = 0;
			}
		}
	}
}
