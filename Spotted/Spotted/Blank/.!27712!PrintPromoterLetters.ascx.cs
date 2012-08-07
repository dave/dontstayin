using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;
using System.IO;
using System.Text;

namespace Spotted.Blank
{
	public partial class PrintPromoterLetters : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//throw new Exception("paused!");
			Usr.KickUserIfNotAdmin("");

			StringBuilder sb = new StringBuilder();
			#region Top HTML
			sb.Append(@"
<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">
<html>
	<head>
		<style>
			P{
				text-align:justify;
				font-size:10pt;
				margin-top:2mm;
				margin-bottom:2mm;
				margin-left:10mm;
				margin-right:10mm;
			}
			P.Address{
				margin-left:15mm;
			}
			P.Big{
				font-family:Verdana;
				text-align:center; 
				font-size:10pt; 
				font-weight:bold;
			}
			Div.PageDiv{
				page-break-after:always;
				padding-top:3cm;
				padding-left:1cm;
				padding-right:1cm;
				padding-bottom:0cm;
			}
		</style>
	</head>
	<body topmargin=""0"" bottommargin=""0"" leftmargin=""0"" rightmargin=""0"">");
			#endregion

			#region Promoter intro letters (with media packs)

			sb.Append(@"<div class=""PageDiv"" style=""page-break-after:always;"">Promoter letters to be posted with media packs</div>");

			#region Venue
			string Venue = @"<div class=""PageDiv"" style=""page-break-after:always;"">
<p class=""Address"">
	[ADDRESS]
</p>
<p style=""margin-top:6mm;"">
	Dear [SALUTATION],
</p>
<p>
	DontStayIn (www.dontstayin.com) is the number one clubbing and social events site in the UK. We now have nearly half a million members, with thousands more joining each day. Our website offers endless opportunities to promote your events to the people who really matter.
</p>
<p>
