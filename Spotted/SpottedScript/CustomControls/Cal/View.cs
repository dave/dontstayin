using System.DHTML;
namespace SpottedScript.CustomControls.Cal
{
	public partial class View
	{
		public InputElement TextBox { get { return (InputElement)Document.GetElementById(clientId + "_inner"); } }
	}
}
