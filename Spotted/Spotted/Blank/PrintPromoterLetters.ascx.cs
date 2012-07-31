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
	We have set your venue up with a free community page where you can list your events, upload photos, write articles about events & DJ’s, run competitions and even organise your guest list. Most importantly you give your regulars the chance to meet each other online, and grow your community outside of the venue.
</p>
<p>
	Please find enclosed some stickers for your venue - these encourage people to upload pictures from your events. This is a great way to increase your exposure on the site, and generate more interest in your future events.
</p>
<p>
	We have many promotional tools to attract our members to your venue, the most effective being flyer banners. Flyer banners are more effective than normal paper flyers for a variety of reasons, the most important being targeting. You can target your flyer banners by location and music type, so if you’re running a London house music event, we won’t be showing your banner to hip-hop fans in Manchester! You have instant statistics on how many views and clicks your banner is getting, and you can update it daily to ensure that your advertising message stays fresh. Finally flyer banners are much more cost effective than printed flyers - you don’t need to pay for printing or distribution.
</p>
<p>
	As a new venue on our site we have a great offer to help you increase your exposure:
</p>
<p class=""Big"">
	We will double whatever you spend on advertising<span style=""font-family:Times New Roman; font-weight:normal;"">*</span>
</p>
<p>
	To activate this offer and the many free tools we provide, please visit:
</p>
<p class=""Big"">
	dontstayin.com/offer
</p>
<p>
	You’ll be asked to enter an access code:
</p>
<p class=""Big"">
	[ACCESS-CODE]
</p>
<p>
	If you have any questions about how to use the site or how to increase your exposure just give our team a call on 0207 835 5599.
</p>
<p>
	Yours faithfully,
</p>
<img src=""/gfx/fabe-sig.gif"" height=""75"" style=""margin-top:-10px; margin-bottom:-10px; margin-left:50px;"" />
<p>
	Fabe Bustillos<br />
	fabe@dontstayin.com
</p>
<p>
	* Offer expires one month after activation, and is available for event or venue banners only. Not to be combined with any other offer or discount.
</p>
</div>";
			#endregion
			#region Old
			string Old = @"<div class=""PageDiv"" style=""page-break-after:always;"">
<p class=""Address"">
	[ADDRESS]
</p>
<p style=""margin-top:6mm;"">
	Dear [SALUTATION],
</p>
<p>
	You have a promoter account on www.dontstayin.com, but haven’t used it in a while. DontStayIn is the number one clubbing and social events site in the UK. We now have nearly half a million members, with thousands more joining each day. Our website offers endless opportunities to promote your events to the people who really matter.
</p>
<p>
	The most effective promotional tools we offer for events and venues are flyer banners. Flyer banners are more effective than normal paper flyers for a variety of reasons, the most important being targeting. You can target your flyer banners by location and music type, so if you’re running a London house music event, we won’t be showing your banner to hip-hop fans in Manchester! You have instant statistics on how many views and clicks your banner is getting, and you can update it daily to ensure that your advertising message stays fresh. Finally flyer banners are much more cost effective than printed flyers – you don’t need to pay for printing or distribution.
</p>
<p>
	We have a great offer to give your promotion a boost:
</p>
<p class=""Big"">
	We will double whatever you spend on advertising<span style=""font-family:Times New Roman; font-weight:normal;"">*</span>
</p>
<p>
	To activate this offer and the many free tools we provide, please visit:
</p>
<p class=""Big"">
	dontstayin.com/offer
</p>
<p>
	You’ll be asked to enter an access code:
</p>
<p class=""Big"">
	[ACCESS-CODE]
</p>
<p>
	If you have any questions about how to use the site or how to increase your exposure just give our team a call on 0207 835 5599.
</p>
<p>
	Yours faithfully,
</p>
<img src=""/gfx/fabe-sig.gif"" height=""75"" style=""margin-top:-10px; margin-bottom:-10px; margin-left:50px;"" />
<p>
	Fabe Bustillos<br />
	fabe@dontstayin.com
</p>
<p>
	* Offer expires one month after activation, and is available for event or venue banners only. Not to be combined with any other offer or discount.
</p>
</div>";
			#endregion
			#region New
			string New = @"<div class=""PageDiv"" style=""page-break-after:always;"">
<p class=""Address"">
	[ADDRESS]
</p>
<p style=""margin-top:6mm;"">
	Dear [SALUTATION],
</p>
<p>
	You recently created a promoter account on www.dontstayin.com. DontStayIn is the number one clubbing and social events site in the UK. We now have nearly half a million members, with thousands more joining each day. Our website offers endless opportunities to promote your events to the people who really matter. 
</p>
<p>
	The most effective promotional tools we offer for events and venues are flyer banners. Flyer banners are more effective than normal paper flyers for a variety of reasons, the most important being targeting. You can target your flyer banners by location and music type, so if you’re running a London house music event, we won’t be showing your banner to hip-hop fans in Manchester! You have instant statistics on how many views and clicks your banner is getting, and you can update it daily to ensure that your advertising message stays fresh. Finally flyer banners are much more cost effective than printed flyers – you don’t need to pay for printing or distribution.
</p>
<p>
	As a new promoter we have a great offer to help you increase your exposure:
</p>
<p class=""Big"">
	We will double whatever you spend on advertising<span style=""font-family:Times New Roman; font-weight:normal;"">*</span>
</p>
<p>
	To activate this offer and the many free tools we provide, please visit:
</p>
<p class=""Big"">
	dontstayin.com/offer
</p>
<p>
	You’ll be asked to enter an access code:
</p>
<p class=""Big"">
	[ACCESS-CODE]
</p>
<p>
	If you have any questions about how to use the site or how to increase your exposure just give our team a call on 0207 835 5599.
</p>
<p>
	Yours faithfully,
</p>
<img src=""/gfx/fabe-sig.gif"" height=""75"" style=""margin-top:-10px; margin-bottom:-10px; margin-left:50px;"" />
<p>
	Fabe Bustillos<br />
	fabe@dontstayin.com
</p>
<p>
	* Offer expires one month after activation, and is available for event or venue banners only. Not to be combined with any other offer or discount.
</p>
</div>";
			#endregion
			#region Active
			string Active = @"<div class=""PageDiv"" style=""page-break-after:always;"">
<p class=""Address"">
	[ADDRESS]
</p>
<p style=""margin-top:6mm;"">
	Dear [SALUTATION],
</p>
<p>
	You are a valued member of the DontStayIn promoter community. This is a quick letter to catch up and let you know we’re the number one clubbing and social events site in the UK. We now have nearly half a million members, with thousands more joining each day. Our website is becoming an even more important tool to promote your events to the people who really matter.
</p>
<p>
	The most effective promotional tools we offer for events and venues are flyer banners. Flyer banners are more effective than normal paper flyers for a variety of reasons, the most important being targeting. You can target your flyer banners by location and music type, so if you’re running a London house music event, we won’t be showing your banner to hip-hop fans in Manchester! You have instant statistics on how many views and clicks your banner is getting, and you can update it daily to ensure that your advertising message stays fresh. Finally flyer banners are much more cost effective than printed flyers – you don’t need to pay for printing or distribution.
</p>
<p>
	As a valued promoter we have a great offer to help you increase your exposure:
</p>
<p class=""Big"">
	We will double whatever you spend on advertising<span style=""font-family:Times New Roman; font-weight:normal;"">*</span>
</p>
<p>
	To activate this offer and the many free tools we provide, please visit:
</p>
<p class=""Big"">
	dontstayin.com/offer
</p>
<p>
	You’ll be asked to enter an access code:
</p>
<p class=""Big"">
	[ACCESS-CODE]
</p>
<p>
	If you have any questions about how to use the site or how to increase your exposure just give our team a call on 0207 835 5599.
</p>
<p>
	Yours faithfully,
</p>
<img src=""/gfx/fabe-sig.gif"" height=""75"" style=""margin-top:-10px; margin-bottom:-10px; margin-left:50px;"" />
<p>
	Fabe Bustillos<br />
	fabe@dontstayin.com
</p>
<p>
	* Offer expires one month after activation, and is available for event or venue banners only. Not to be combined with any other offer or discount.
</p>
</div>";
			#endregion
			#region TicketsDomain
			string TicketsDomain = @"<div class=""PageDiv"" style=""page-break-after:always;"">
<p class=""Address"">
	[ADDRESS]
</p>
<p style=""margin-top:6mm;"">
	Dear [SALUTATION],
</p>
<p>
	Merry Christmas from everyone at DontStayIn! 
</p>
<p>
	You are one of the top promoters on our site, and to say thank you for your continued support we would like to give you possibly the best present you’ll ever receive!
</p>
<p>
	We’ve registered your very own domain name for selling tickets:
</p>
<p class=""Big"">
	[DOMAINS]
</p>
<p>
	If you go to it now, it’ll take you to your new “hot tickets” page on DontStayIn. You can start putting it on your flyers straight away, and get your customers straight to your tickets.
</p>
<p>
	What’s more, we’re currently working on creating you a customised micro-site! This will sell your tickets without looking like DontStayIn – it will have your logo and colour-scheme. We’ll phone you when this bit is ready to make sure you’re happy with it!
</p>
<p>
	If you’re not already selling tickets on DontStayIn, it literally takes three clicks to start. Click the Promoters button at the top of the site to begin.
</p>
<p>
	If there’s anything you’re unsure about, or you’d like us to show you how to sell tickets, just give me a quick ring on 0207 835 5599.
</p>
<img src=""/gfx/sig-[SALESUSRSIG].gif"" width=""120"" style=""margin-top:-10px; margin-bottom:-10px; margin-left:50px;"" />
<p>
	[SALESUSRNAME] ([SALESUSREMAIL])
</p>
<p>
	Your account manager.
</p>
</div>";
			#endregion

			Query q = new Query();
			q.QueryCondition = new Q(Domain.Columns.RedirectApp, "hottickets");
			q.TableElement = new Join(Promoter.Columns.K, Domain.Columns.PromoterK);
			q.Distinct = true;
			q.DistinctColumn = Promoter.Columns.K;
			PromoterSet ps = new PromoterSet(q);

			foreach (Promoter p in ps)
			{
				string s = TicketsDomain;

				//address
				string Address = "";
				if (p.ContactName.Length > 0)
					Address += p.ContactName + "<br />";
				Address += p.Name + "<br />";
				if (p.AddressStreet.Length > 0)
					Address += p.AddressStreet + "<br />";
				if (p.AddressArea.Length > 0)
					Address += p.AddressArea + "<br />";
				if (p.AddressTown.Length > 0)
					Address += p.AddressTown + "<br />";
				Address += p.AddressCounty.ToUpper() + " " + p.AddressPostcode;

				s = s.Replace("[ADDRESS]", Address);

				string Salutation = "Sir or Madam";
				if (p.ContactName.Length > 0 && !p.ContactName.Equals("Events manager"))
				{
					if (p.ContactName.Contains(" "))
						Salutation = p.ContactName.Split(' ')[0];
					else
						Salutation = p.ContactName;
				}
				s = s.Replace("[SALUTATION]", Salutation);

				Query q1 = new Query();
				q1.QueryCondition = new And(new Q(Domain.Columns.RedirectApp, "hottickets"), new Q(Domain.Columns.PromoterK, p.K));
				DomainSet ds = new DomainSet(q1);
				string domains = "";
				foreach (Domain d in ds)
				{
					domains += domains.Length == 0 ? "" : "<br>";
					domains += "www." + d.DomainName;
				}
				s = s.Replace("[DOMAINS]", domains);

				try
				{
					s = s.Replace("[SALESUSRSIG]", p.SalesUsr.NickName.ToLower());
					s = s.Replace("[SALESUSRNAME]", p.SalesUsr.FullName);
					if (p.SalesUsrK == 1)
						s = s.Replace("[SALESUSREMAIL]", "john@dontstayin.com");
					else
						s = s.Replace("[SALESUSREMAIL]", p.SalesUsr.Email);
				}
				catch { }



				//s = s.Replace("[ACCESS-CODE]", p.K.ToString("0000") + " - " + p.AccessCodeRandom.Substring(0, 4) + " - " + p.AccessCodeRandom.Substring(4, 4));
				p.LetterStatus = Promoter.LetterStatusEnum.Printing;
				p.Update();
				sb.Append(s);
			}
			#endregion

			#region Bottom HTML
			sb.Append(@"
	</body>
</html>");
			#endregion
			Response.Clear();
			Response.Write(sb.ToString());
			Response.Flush();
			Response.End();
		}
	}
}