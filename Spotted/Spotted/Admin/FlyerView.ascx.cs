using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Admin
{
	public partial class FlyerView : AdminUserControl
	{

		private Flyer flyer;
		protected Flyer Flyer
		{
			get { return flyer ?? (flyer = new Flyer(ContainerPage.Url["k"].ValueInt)); }
		}

		private int? countUsrBase;
		protected int CountUsrBase
		{
			get { return countUsrBase ?? (countUsrBase = Flyer.GetUsrs().Count).Value; }
		}

		protected void Page_PreRender(object source, EventArgs e)
		{
			uiValidationErrors.Visible = Flyer.ValidationErrors.Length > 0;
			uiSendButtons.Visible = Flyer.CanSend;
			if (uiSendButtons.Visible)
			{
				uiSend.Visible = !Flyer.IsReadyToSend && !Flyer.IsSending && !Flyer.HasFinishedSending;
				uiStop.Visible = Flyer.IsReadyToSend && !Flyer.HasFinishedSending; // IsSending could be anything
			}
			uiSentSuccessfully.Visible = Flyer.HasFinishedSending;
			uiRestart.Visible =
				Flyer.IsSending && 
				!Flyer.HasFinishedSending && 
				Flyer.DateTimeLastMessageSent.HasValue && 
				Flyer.DateTimeLastMessageSent < DateTime.Now.AddHours(-1);

			
		}
		protected void Restart(object source, EventArgs e)
		{
			//Flyer.DateTimeLastMessageSent = null;
			Flyer.IsReadyToSend = true;
			Flyer.IsSending = false;
			Flyer.Update();

		}

		protected void Send(object source, EventArgs e)
		{
			Flyer.IsReadyToSend = true;
			Flyer.Update();
			// flyer watcher task will detect this and do the rest
		}
		protected void Stop(object source, EventArgs e)
		{
			Flyer.IsReadyToSend = false;
			Flyer.Update();
			// flyer sender program will detect this and do the rest
		}
	}
}
