using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace JsWebControls
{
	interface IHasParameters : IControl
	{
		Dictionary<string, string> Parameters { get; }
		HiddenField ParametersHiddenField { get; }
	}
}
