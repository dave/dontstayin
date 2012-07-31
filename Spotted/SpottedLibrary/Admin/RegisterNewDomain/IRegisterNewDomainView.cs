using System;
using System.Collections.Generic;
using System.Text;

namespace SpottedLibrary.Admin.RegisterNewDomain
{
	public class IRegisterNewDomainView : IView
	{
		#region IView Members

		public event EventHandler Init;

		public event EventHandler Load;

		public bool IsPostBack
		{
			get { throw new NotImplementedException(); }
		}

		public void DataBind()
		{
			throw new NotImplementedException();
		}

		public bool IsValid
		{
			get { throw new NotImplementedException(); }
		}

		#endregion
	}
}
