using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.TagCloud
{
	public class TagCloudController
	{
		ITagCloudView view;
		TagCloudService service;
		public TagCloudController(ITagCloudView view)
		{
			this.view = view;
			this.service = new TagCloudService();
			this.view.Load += new EventHandler(view_Load);
		}

		void view_Load(object sender, EventArgs e)
		{
			this.view.LinkCloud.Items = service.GetTags(view.NumberOfItems);
		}
	}
}
