using System;
using Sys;

namespace SpottedScript.Controls.PagedData
{
	internal interface IParameterSource
	{
		Dictionary Parameters { get; }
		EventHandler ParametersUpdated { get; set; }
	}
}
