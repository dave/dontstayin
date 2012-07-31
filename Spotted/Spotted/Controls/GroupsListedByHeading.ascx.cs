using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Entities;

namespace Spotted.Controls
{
	public partial class GroupsListedByHeading : System.Web.UI.UserControl
	{
		protected IEnumerable Groups;
		public IEnumerable<HeadingDTO> Headings { get; set; }
		public class GroupDTO
		{
			public string Title { get; set; }
			public string Description { get; set; }
			public int TotalMembers { get; set; }
			public ThreadDTO LastThread { get; set; }
			public string Url { get; set; }
			
		}
		public class HeadingDTO
		{
			public string Title { get; set; }
			public string Description{ get; set;}
			public IEnumerable<GroupDTO> Groups { get; set; }
			public string Url { get; set; }
		}
		public class ThreadDTO
		{
		 
			public string Subject { get; set; }
			public string PostUsrName { get; set; }
			public DateTime DateTime { get; set; }
			public string Url { get; set; }
		}
	}
	
}
