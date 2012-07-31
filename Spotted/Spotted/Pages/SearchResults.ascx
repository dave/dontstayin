<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchResults.ascx.cs" Inherits="Spotted.Pages.SearchResults" %>
<dsi:H1 runat="server">Search Results</dsi:H1>

<div id="cse-search-results" class="ContentBorder">
<script type="text/javascript">
	var googleSearchIframeName = "cse-search-results";
	var googleSearchFormName = "cse-search-box";
	var googleSearchFrameWidth = 890;
	var googleSearchDomain = "www.google.co.uk";
	var googleSearchPath = "/cse";
	
</script>
<script type="text/javascript" src="http://www.google.com/afsonline/show_afs_search.js"></script>
<script>
	$(document).ready(function() {
		<!--%= this.ContainerPage.Menu.uiSiteSearchBox.uiAuto.ClientID %-->Behaviour.focus();
	});
</script>
<p>
	If you can't see any results here, check that you don't have any ad-blocking software enabled. If you do, please make an exclusion for this page.
</p>
</div>
