using System.Html;
using System;
using Js.Library;
using jQueryApi;

namespace Js.ClientControls
{
	public delegate void ItemChangeDelegate(string key, string value);

	public class MultiSelectorBehaviour 
	{
		public ItemChangeDelegate ItemAdded;
		public ItemChangeDelegate ItemRemoved;
		
		public HtmlAutoCompleteBehaviour HtmlAutoComplete;
		public Element container;
		InputElement hiddenOutput;
		private PairListField selections;
		public MultiSelectorBehaviour(Element container, HtmlAutoCompleteBehaviour htmlAutoComplete, InputElement hiddenOutput)
		{
			this.container = container;
			this.hiddenOutput = hiddenOutput;
			this.HtmlAutoComplete = htmlAutoComplete;
			this.HtmlAutoComplete.ItemChosen = htmlAutoComplete_ItemChosen;
			selections = new PairListField(hiddenOutput);
			InitialiseInitialSelections();
			jQuery.FromElement(this.container).Click(this.OnClick);
			
		}

		private void InitialiseInitialSelections()
		{
			selections.Clear();
			for (int i=0; i < container.ChildNodes.Length; i++)
			{
				Element child = container.ChildNodes[i];
				if (child.NodeType == ElementType.Element && child.ChildNodes.Length > 1 && child.ChildNodes[0].TagName != "INPUT")
				{
					jQuery.FromElement(child.ChildNodes[1]).Click(DeleteButtonClick);
					selections.Set(child.ChildNodes[0].NodeValue, child.GetAttributeNode("val").Value);
				}
			}
		}

		private void OnClick(jQueryEvent e)
		{
			HtmlAutoComplete.Focus();
		}

 
		public void AddItem(string text, string value)
		{
			if (selections.ContainsKey(text))
			{
				return;
			}
			Element item = Document.CreateElement("LI");
			ElementAttribute itemCssAttribute = this.container.GetAttributeNode(MultiSelectorAttributes.MultiSelectorListBoxCss);
			item.ClassName = (itemCssAttribute != null) ? itemCssAttribute.Value : MultiSelectorAttributes.MultiSelectorListBoxCss;
			
			Element deleteButton = Document.CreateElement("SPAN");
			deleteButton.InnerHTML = "&nbsp;&nbsp;";
			ElementAttribute delCssAttribute = this.container.GetAttributeNode(MultiSelectorAttributes.MultiSelectorDelButtonCss);
			deleteButton.ClassName = (delCssAttribute != null) ? delCssAttribute.Value : MultiSelectorAttributes.MultiSelectorDelButtonCss;
			jQuery.FromElement(deleteButton).Click(DeleteButtonClick);
			item.AppendChild(deleteButton);

			Element textSpan = Document.CreateElement("SPAN");
			textSpan.InnerHTML = text;
			item.AppendChild(textSpan);
			
			//item.InnerHTML = text;


			ElementAttribute textAtt = Document.CreateAttribute("text");
			textAtt.Value = text;
			item.SetAttributeNode(textAtt);

			ElementAttribute valueAtt = Document.CreateAttribute("val");
			valueAtt.Value = value;
			item.SetAttributeNode(valueAtt);

			
			
			Element childToInsertBefore = container.LastChild;
			while (childToInsertBefore.ChildNodes.Length == 0 || childToInsertBefore.ChildNodes[0].TagName != "INPUT")
			{
				childToInsertBefore = childToInsertBefore.PreviousSibling;
			}
			container.InsertBefore(item, childToInsertBefore);

			selections.Set(text, value);
			if (ItemAdded != null) { ItemAdded(text, value); }
		}

		void htmlAutoComplete_ItemChosen(KeyStringPair item)
		{
			AddItem(item.Key, item.Value);
			this.HtmlAutoComplete.Text = "";
			this.HtmlAutoComplete.Value = "";
			this.HtmlAutoComplete.Focus();
		}
		 
		void DeleteButtonClick(jQueryEvent e)
		{
			//e.Target.Clear
			jQuery.FromElement(e.Target).Unbind("click").Unbind("mouseover");
			//DomEvent.ClearHandlers(e.Target);
			RemoveItem(e.Target.ParentNode);
			
		}
		public void RemoveItem(Element item)
		{
			string text = item.GetAttributeNode("text").Value;
			string value = item.GetAttributeNode("val").Value;
			selections.Set(text, null);
			
			item.ParentNode.RemoveChild(item);
			HtmlAutoComplete.Focus();
			if (ItemRemoved != null) { ItemRemoved(text, value); }
		}
		public PairListField GetSelections()
		{
			return selections;
		}
		public void Clear()
		{
			for (int i = container.ChildNodes.Length - 1; i >= 0 ; i--)
			{
				Element child = container.ChildNodes[i];
				if (child.NodeType == ElementType.Element && child.ChildNodes[0].TagName != "INPUT")
				{
					jQuery.FromElement(child.ChildNodes[1]).Unbind("click").Unbind("mouseover");
					//DomEvent.ClearHandlers(child.ChildNodes[1]);
					selections.Set(child.ChildNodes[0].NodeValue, null);
					child.ParentNode.RemoveChild(child);
				}
			}
		}
	}
}
