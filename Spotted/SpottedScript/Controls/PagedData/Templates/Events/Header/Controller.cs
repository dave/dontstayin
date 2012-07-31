
using System;
using SpottedScript.Controls.PagedData;
using Sys;
using Sys.UI;

namespace SpottedScript.Controls.PagedData.Templates.Events.Header
{
	public class Controller : IParameterSource
	{
		private readonly View view;

		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(view.uiShowFuture, "click", OnParametersUpdated);
			DomEvent.AddHandler(view.uiShowPast, "click", OnParametersUpdated);
		}

		private void OnParametersUpdated(DomEvent e)
		{
			if (parametersUpdated != null) parametersUpdated(this, null);
		}

		public Dictionary Parameters
		{
			get
			{
				Dictionary parameters = new Dictionary();
				parameters["showFuture"] = view.uiShowFuture.Checked;
				parameters["showPast"] = view.uiShowPast.Checked;
				parameters["orderDesc"] = view.uiShowPast.Checked ? true : false;
				parameters["musicTypeK"] = view.uiMusicType.Value;
				return parameters;
			}
		}

		
		private EventHandler parametersUpdated;
		public EventHandler ParametersUpdated
		{
			get { return parametersUpdated; }
			set { parametersUpdated = value; }
		}

	}
}
