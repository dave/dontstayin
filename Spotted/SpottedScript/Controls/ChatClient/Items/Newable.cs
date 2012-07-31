using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public abstract class Newable : Html
	{
		#region IsInNewSection
		public bool IsInNewSection
		{
			get
			{
				return isInNewSection;
			}
			set
			{
				isInNewSection = value;
				updateUI();
			}
		}
		protected bool isInNewSection;
		#endregion
		#region IsTopOfSection
		public bool IsTopOfSection
		{
			get
			{
				return isTopOfSection;
			}
			set
			{
				isTopOfSection = value;
				updateUI();
			}
		}
		protected bool isTopOfSection;
		#endregion
		#region IsBottomOfSection
		public bool IsBottomOfSection
		{
			get
			{
				return isBottomOfSection;
			}
			set
			{
				isBottomOfSection = value;
				updateUI();
			}
		}
		protected bool isBottomOfSection;
		#endregion

		public Newable(ItemStub itemStub, Controller parent, int serverRequestIndex)
			: base(itemStub, parent, serverRequestIndex)
		{
		}

		abstract protected void updateUI();

		public void UpdateClassModifiersAllAtOnce(
			bool isTopOfSectionValue,
			bool isBottomOfSectionValue,
			bool isInNewSectionValue)
		{
			if (isTopOfSectionValue != IsTopOfSection ||
				isBottomOfSectionValue != IsBottomOfSection ||
				isInNewSectionValue != IsInNewSection)
			{
				isTopOfSection = isTopOfSectionValue;
				isBottomOfSection = isBottomOfSectionValue;
				isInNewSection = isInNewSectionValue;
				updateUI();
			}
		}

	}
}
