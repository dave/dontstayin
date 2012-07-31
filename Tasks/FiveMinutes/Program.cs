using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace FiveMinutes
{
	class Program
	{
		static void Main(string[] args)
		{
			ProcessGalleriesThatHaveFinishedUploading();
		}

		static void ProcessGalleriesThatHaveFinishedUploading()
		{
			Query q = new Query();

			q.QueryCondition = new And(
				new Q(Gallery.Columns.RunFinishedUploadingTask, false),
				new Q(Gallery.Columns.LastLiveDateTime, QueryOperator.LessThan, DateTime.Now.AddMinutes(-20)),
				new Q(Gallery.Columns.LivePhotos, QueryOperator.GreaterThan, 0));

			GallerySet gs = new GallerySet(q);
			
			foreach (Gallery g in gs)
			{
				g.RunFinishedUploadingTask = true;
				g.Update();
				if (g.Owner.FacebookConnected && g.Owner.FacebookStoryUploadPhoto)
				{
					try
					{
						FacebookPost.CreatePhotoUpload(g.Owner, g);
					}
					catch { }
				}
			}
		}
	}
}
