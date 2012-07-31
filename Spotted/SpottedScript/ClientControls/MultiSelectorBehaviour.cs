 
using System.DHTML;
using System;
using Sys.UI;
namespace ScriptSharpLibrary
{
	public delegate void ItemChangeDelegate(string key, string value);

	public class MultiSelectorBehaviour 
	{
		public ItemChangeDelegate ItemAdded;
		public ItemChangeDelegate ItemRemoved;
		
		public HtmlAutoCompleteBehaviour HtmlAutoComplete;
		public DOMElement container;
		InputElement hiddenOutput;
		private PairListField selections;
		public MultiSelectorBehaviour(DOMElement container, HtmlAutoCompleteBehaviour htmlAutoComplete, InputElement hiddenOutput)
		{
			this.container = container;
			this.hiddenOutput = hiddenOutput;
			this.HtmlAutoComplete = htmlAutoComplete;
			this.HtmlAutoComplete.ItemChosen = htmlAutoComplete_ItemChosen;
			selections = new PairListField(hiddenOutput);
			InitialiseInitialSelections();
			DomEvent.AddHandler(this.container, "click", this.OnClick);
			
		}

		private void InitialiseInitialSelections()
		{
			selections.Clear();
			for (int i=0; i < container.ChildNodes.Length; i++)
			{
				DOMElement child = container.ChildNodes[i];
				if (child.NodeType == DOMElementType.Element && child.ChildNodes.Length > 1 && child.ChildNodes[0].TagName != "INPUT")
				{
					DomEvent.AddHandler(child.ChildNodes[1], "click", new DomEventHandler(DeleteButtonClick));
					selections.Set(child.ChildNodes[0].NodeValue, child.GetAttributeNode("val").Value);
				}
			}
		}

		private void OnClick(DomEvent e)
		{
			HtmlAutoComplete.Focus();
		}

 
		public void AddItem(string text, string value)
		{
			if (selections.ContainsKey(text))
			{
				return;
			}
			DOMElement item = Document.CreateElement("LI");
			DOMAttribute itemCssAttribute = this.container.GetAttributeNode(MultiSelectorAttributes.MultiSelectorListBoxCss);
			item.ClassName = (itemCssAttribute != null) ? itemCssAttribute.Value : MultiSelectorAttributes.MultiSelectorListBoxCss;
			
			DOMElement deleteButton = Document.CreateElement("SPAN");
			deleteButton.InnerHTML = "&nbsp;&nbsp;";
			DOMAttribute delCssAttribute = this.container.GetAttributeNode(MultiSelectorAttributes.MultiSelectorDelButtonCss);
			deleteButton.ClassName = (delCssAttribute != null) ? delCssAttribute.Value : MultiSelectorAttributes.MultiSelectorDelButtonCss;
			DomEvent.AddHandler(deleteButton, "click", new DomEventHandler(DeleteButtonClick));
			item.AppendChild(deleteButton);

			DOMElement textSpan = Document.CreateElement("SPAN");
			textSpan.InnerHTML = text;
			item.AppendChild(textSpan);
			
			//item.InnerHTML = text;
			
			
			DOMAttribute textAtt = Document.CreateAttribute("text");
			textAtt.Value = text;
			item.SetAttributeNode(textAtt);

			DOMAttribute valueAtt = Document.CreateAttribute("val");
			valueAtt.Value = value;
			item.SetAttributeNode(valueAtt);

			
			
			DOMElement childToInsertBefore = container.LastChild;
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
		 
		void DeleteButtonClick(DomEvent e)
		{
			DomEvent.ClearHandlers(e.Target);
			RemoveItem(e.Target.ParentNode);
			
		}
		public void RemoveItem(DOMElement item)
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
				DOMElement child = container.ChildNodes[i];
				if (child.NodeType == DOMElementType.Element && child.ChildNodes[0].TagName != "INPUT")
				{
					DomEvent.ClearHandlers(child.ChildNodes[1]);
					selections.Set(child.ChildNodes[0].NodeValue, null);
					child.ParentNode.RemoveChild(child);
				}
			}
		}
	}
}
