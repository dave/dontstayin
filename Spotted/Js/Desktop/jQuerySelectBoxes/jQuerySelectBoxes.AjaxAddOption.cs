using System.Collections;
using System;
using Js.Library;

namespace Js.jQuerySelectBoxesAPI
{

	public partial class jQuerySelectBoxesObject
	{
		/// <summary>
		/// Gets options froma URL and adds to a select box.
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <param name="select"></param>
		/// <param name="function">Param: args</param>
		/// <param name="args"></param>
		/// <returns></returns>
		public jQuerySelectBoxesObject AjaxAddOption(
			string url,
			Dictionary parameters,
			bool select,
			ActionObject function,
			object[] args)
		{
			return null;
		}

		/// <summary>
		/// Doesn't add the options, but returns them to the function
		/// </summary>
		/// <param name="url"></param>
		/// <param name="parameters"></param>
		/// <param name="select"></param>
		/// <param name="function">Firs param: args, second param: data</param>
		/// <param name="args"></param>
		/// <returns></returns>
		public jQuerySelectBoxesObject AjaxAddOption1(
			string url,
			Dictionary parameters,
			bool select,
			ActionObjectObject function,
			object[] args)
		{
			return null;
		}


	}
}
