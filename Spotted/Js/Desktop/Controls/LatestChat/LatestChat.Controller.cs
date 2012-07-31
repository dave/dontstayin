using System;
using System.Collections.Generic;
using System.Html;
using Js.Library;

namespace Js.Controls.LatestChat
{
	public class Controller
	{
		private View view;
		private LatestThreadsProvider latestThreadsProvider;
		private int objectK
		{
			get
			{
				try
				{
					return int.Parse(view.uiObjectK.Value);
				}
				catch
				{
					return 0;
				}
			}
			set
			{
				view.uiObjectK.Value = value.ToString();
			}
		}
		public Controller(View view)
		{
			this.view = view;
			latestThreadsProvider = new LatestThreadsProvider(int.Parse(view.uiThreadsCount.Value), bool.Parse(view.uiHasGroupObjectFilter.Value), int.Parse(view.uiObjectType.Value));
			latestThreadsProvider.OnLoaded = new EventHandler(loaded);
		}

		public void Hide()
		{
			view.Holder.Style.Display = "none";
		}

		public void Show(int objectK)
		{
			this.objectK = objectK;
			latestThreadsProvider.LoadThreads(this.objectK);
		}

		public void Update(object o, EventArgs e)
		{
			latestThreadsProvider.ReloadThreads(this.objectK);
		}

		private void loaded(object o, EventArgs e)
		{
			displayThreads();
		}

		private void displayThreads()
		{
			ThreadStub[] threads = latestThreadsProvider.CurrentThreads;
			if (threads.Length > 0)
			{
				RemoveAllChildren(view.ThreadsPanel);
				view.ThreadsPanel.AppendChild(CreateTable(threads));
				for (int i = 0; i < threads.Length; i++)
				{
					Script.Eval(threads[i].watchingScript);
					Script.Eval(threads[i].favouriteScript);
				}

				view.Holder.Style.Display = "";
				view.ThreadsPanel.Style.Display = "";
			}
		}

		private void RemoveAllChildren(Element dOMElement)
		{
			dOMElement.InnerHTML = "";
		}

		private TableElement CreateTable(ThreadStub[] threads)
		{
			TableElement t = (TableElement)Document.CreateElement("table");
			t.Style.Border = "0px";
			t.Style.Width = "100%";
			t.Style.BorderCollapse = "collapse";
			TableSectionElement tb = (TableSectionElement)Document.CreateElement("tbody");
			t.AppendChild(tb);
			tb.AppendChild(CreateHeader());
			for (int i = 0; i < threads.Length; i++)
			{
				tb.AppendChild(CreateRow(threads[i]));
			}
			return t;
		}

		private static TableRowElement CreateRow(ThreadStub thread)
		{
			TableRowElement tr = (TableRowElement)Document.CreateElement("tr");
			tr.Style.VerticalAlign = "top";
			//eval scripts
			tr.AppendChild(CreateTableCell(thread.watchingHtml, "dataGridThreadTitlesTight", null));
			//eval scripts
			tr.AppendChild(CreateTableCell(thread.favouriteHtml, "dataGridThreadTitlesTight", null));
			//tr.AppendChild(CreateTableCell("<a href=\"" + thread.threadUrlSimple + "\"><img src=\"" + thread.simpleIconPath + "\" align=\"top\" border=\"0\" class=\"LatestChatImage\" hspace=\"0\" width=\"30\" height=\"30\"></a>", "dataGridTightImg"));
			tr.AppendChild(CreateTableCell(thread.iconsHtml + thread.commentHtmlStart + "<a href=\"" + thread.threadUrlSimple + "\" " + thread.rollover + ">" + thread.subjectSafe + "</a>" + thread.commentHtmlEnd + thread.pagesHtml, "dataGridThreadTitles", null));
			tr.ChildNodes[2].Style.Width = "100%";
			tr.AppendChild(CreateTableCell("<small>" + thread.authorHtml + "</small>", "dataGridThread", "3px"));
			tr.AppendChild(CreateTableCell("<small>" + thread.repliesHtml + "</small>", "dataGridThread", "3px"));
			return tr;
		}

		private static TableRowElement CreateHeader()
		{
			TableRowElement th = (TableRowElement)Document.CreateElement("tr");
			th.ClassName = "dataGridHeader";
			TableCellElement td = (TableCellElement)Document.CreateElement("td");
			td.ColSpan = 3;
			th.AppendChild(td);
			th.AppendChild(CreateTableCell("Author", null, null));
			th.AppendChild(CreateTableCell("Replies&nbsp;/&nbsp;last", null, null));
			return th;
		}

		private static TableCellElement CreateTableCell(string innerHTML, string className, string cellPadding)
		{
			TableCellElement td = (TableCellElement)Document.CreateElement("td");
			td.InnerHTML = innerHTML;
			if (cellPadding != null)
				td.Style.Padding = cellPadding;
			if (className != null)
				td.ClassName = className;
			return td;
		}
	}
}
