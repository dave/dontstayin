namespace Js.jQuerySelectBoxesAPI
{
	using System;
	using jQueryApi;
	using System.Runtime.CompilerServices;

	[ScriptName("$"), IgnoreNamespace, Imported]
	public partial class jQuerySelectBoxesObject : jQueryObject
	{
		/// <summary>
		/// Attaches a handler for handling the specified event on the matched set of elements.
		/// </summary>
		/// <param name="eventName">The name of the event.</param>
		/// <param name="eventHandler">The event handler to be invoked.</param>
		/// <returns>The current jQueryObject</returns>
		public jQuerySelectBoxesObject Bind(string eventName, jQuerySelectBoxesEventHandler eventHandler)
		{
			return this;
		}

		/// <summary>
		/// Adds elements to the set of matched elements.
		/// </summary>
		/// <param name="o">The <see cref="jQueryUIObject"/> to add.</param>
		/// <returns>The new <see cref="jQueryUIObject"/> with added elements.</returns>
		public jQuerySelectBoxesObject Add(jQueryObject o)
		{
			return null;
		}

		/// <summary>
		/// Disables text selection.
		/// </summary>
		/// <returns>The new <see cref="jQueryUIObject"/> with added elements.</returns>
		public jQuerySelectBoxesObject DisableSelection()
		{
			return null;
		}

		/// <summary>
		/// Gets a jQueryObject representing the immediate following sibling element
		/// of the matched set of elements filtered by the specified selector.
		/// </summary>
		/// <returns>The new <see cref="jQueryUIObject"/>.</returns>
		public new jQuerySelectBoxesObject Next()
		{
			return null;
		}

        /// <summary>
        /// Get the parent of each element in the current set of matched elements, optionally filtered by a selector.
        /// </summary>
        /// <returns>The new <see cref="jQueryUIObject"/>.</returns>
		public new jQuerySelectBoxesObject Parent()
        {
            return null;
        }

        /// <summary>
        /// Get the parent of each element in the current set of matched elements, optionally filtered by a selector.
        /// </summary>
        /// <param name="selector">A string containing a selector expression to match elements against.</param>
        /// <returns>The new <see cref="jQueryUIObject"/>.</returns>
		public new jQuerySelectBoxesObject Parent(string selector)
        {
            return null;
        }
	}
}
