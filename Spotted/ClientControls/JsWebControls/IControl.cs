using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsWebControls
{
	interface IControl
	{
		event EventHandler Init;
		event EventHandler PreRender;
	}
}
