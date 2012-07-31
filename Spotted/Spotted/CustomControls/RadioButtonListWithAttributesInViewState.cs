using System.Web.UI.WebControls;

namespace Spotted.CustomControls
{
	public class RadioButtonListWithAttributesInViewState : RadioButtonList
	{
		protected override object SaveViewState()
		{
			// Create an object array with one element for the RadioButtonList's
			// ViewState contents, and one element for each ListItem in RadioButtonListWithAttributesInViewState
			object[] state = new object[this.Items.Count + 1];

			object baseState = base.SaveViewState();
			state[0] = baseState;

			// Now, see if we even need to save the view state
			bool itemHasAttributes = false;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Attributes.Count > 0)
				{
					itemHasAttributes = true;

					// Create an array of the item's Attribute's keys and values
					object[] attribKV = new object[this.Items[i].Attributes.Count * 2];
					int k = 0;
					foreach (string key in this.Items[i].Attributes.Keys)
					{
						attribKV[k++] = key;
						attribKV[k++] = this.Items[i].Attributes[key];
					}

					state[i + 1] = attribKV;
				}
			}

			// return either baseState or state, depending on whether or not
			// any ListItems had attributes
			if (itemHasAttributes)
				return state;
			else
				return baseState;
		}

		protected override void LoadViewState(object savedState)
		{
			if (savedState == null) return;

			// see if savedState is an object or object array
			if (savedState is object[])
			{
				// we have an array of items with attributes
				object[] state = (object[])savedState;
				base.LoadViewState(state[0]);   // load the base state

				for (int i = 1; i < state.Length; i++)
				{
					if (state[i] != null)
					{
						// Load back in the attributes
						object[] attribKV = (object[])state[i];
						for (int k = 0; k < attribKV.Length; k += 2)
							this.Items[i - 1].Attributes.Add(attribKV[k].ToString(),
														   attribKV[k + 1].ToString());
					}
				}
			}
			else
				// we have just the base state
				base.LoadViewState(savedState);
		}
	}
}
