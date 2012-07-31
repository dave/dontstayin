using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace JsWebControls
{
	class HasParametersController
	{
		IHasParameters obj;
		public HasParametersController(IHasParameters obj)
		{
			this.obj = obj;

			obj.PreRender += new EventHandler(obj_PreRender);
			obj.ParametersHiddenField.ValueChanged += new EventHandler(ParametersHiddenField_ValueChanged);
		}

		void ParametersHiddenField_ValueChanged(object sender, EventArgs e)
		{
			obj.Parameters.Clear();
			if (obj.ParametersHiddenField.Value.Length > 0)
			{
				string[] parts = obj.ParametersHiddenField.Value.Split(':', ';');
				for (int i = 0; i < parts.Length; i += 2)
				{
					obj.Parameters.Add(parts[i], parts[i + 1]);
				}
			}
		}

		void obj_PreRender(object sender, EventArgs e)
		{

			List<string> keys = new List<string>(obj.Parameters.Keys);
			obj.ParametersHiddenField.Value = String.Join(";", keys.ConvertAll(key => key + ":" + obj.Parameters[key]).ToArray());
		}

		 
	}
}
