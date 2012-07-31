<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mailing.ascx.cs" Inherits="Spotted.Admin.Mailing" %>
<h2>There are <asp:Label runat="server" ID="TitleLabel"></asp:Label> waiting to print.</h2>
<style>
	li{margin-bottom:10px;}
</style>
<ol>
<!--
<li>
	<a href="/popup/printpromoterletters" target="_blank">Open promoter letters</a>.
</li>
<li>
	Click "File" &gt; "Page Setup", and change options:<br />
	Size: A4<br />
	Source: Letterhead<br />
	Remove any text in the "Header" and "Footer" boxes<br />
	Orientation: Portrait<br />
	Margins: Left 5, Right 5, Top 14, Bottom 14<br />
	(OR for Fabes computer Margins: Left 2, Right 2, Top 4, Bottom 4)
</li>-->
<li>
	<a href="/popup/mailinglabels" target="_blank">Open labels</a>.
</li>
<li>
	Click "File" &gt; "Page Setup", and change options:<br />
	Size: A4<br />
	Source: Labels<br />
	Remove any text in the "Header" and "Footer" boxes<br />
	Orientation: Portrait<br />
	Margins: Left 5, Right 5, Top 14, Bottom 14<br />
	(OR for Fabes computer Margins: Left 2, Right 2, Top 4, Bottom 4)
</li>
<li>
	Stick labels on relevant packets.
</li>

<li>Use Dev Hell OBA account to load details into Royal Mail - talk to Nicola</li>
<li>
	Mark all printed items as sent: <button runat="server" onserverclick="MarkSent">Mark Sent</button> <asp:Label runat="server" ForeColor="Blue" ID="DoneLabel" Visible="false">Done!</asp:Label>
</li>
<!--
<li>
	Put the packets in the relevant mail bags.
</li>
<li>
	Ensure each mail bag doesn't weigh over 10kg. The summary sheet should tell you how many mail bags you should be using. (10kg = 100 envelopes or 20 boxes)
</li>
<li>
	<a href="/popup/mailingupload" target="_blank">Click here</a> to upload the data to Royal Mail. This page will return with the bag tag labels. 
</li>
<li>
	Click "File" &gt; "Page Setup", and change options:<br />
	Size: A4<br />
	Source: Labels<br />
	Remove any text in the "Header" and "Footer" boxes<br />
	Orientation: Portrait<br />
	Margins: Left 5, Right 5, Top 14, Bottom 14<br />
	(OR for Fabes computer Margins: Left 2, Right 2, Top 4, Bottom 4)
</li>
<li>	
	Print the bag tag labels.
</li>

<li>
	<a href="/popup/mailingreport" target="_blank">Click here</a> to generate the Royal Mail report.
</li>
<li>
	Click "File" &gt; "Page Setup", and change options:<br />
	Source: Automatically select<br />
	Orientation: Landscape<br />
</li>
<li>
	Print one copy of the report for each bag, plus one copy for Tim.
</li>

<l1>
	As of 2008-12-11, there should be no small envelopes (symbol A) - they have been replaced with large packs (symbol B).
</l1>
<li>
	Stick the bag tag labels to the relevant Royal Mail bag tags. A / F = Green, B = Red, C / D / E = White
</li>
<li>
	Seal all mail bags with yellow plastic wraps, remembering to atach the relavent tag.
</li>
<li>
	Attach a copy of the summary sheet to each of the bags and take them downstairs for collection.
</li>-->
</ol>
