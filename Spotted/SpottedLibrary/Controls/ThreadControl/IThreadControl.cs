using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.ThreadControl
{
	public interface IThreadControl 
	{
		int? ThreadK { set; }
		int? ParentObjectK { set; }
		void Initialise();
		Model.Entities.ObjectType? ParentObjectType { set; }
		IDiscussable ParentObject { get; }
		bool Visible { set; }
		int CurrentPage { set; }
	}
}
