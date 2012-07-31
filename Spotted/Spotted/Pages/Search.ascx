<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Search.ascx.cs" Inherits="Spotted.Pages.Search" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>DontStayIn Search Page</title>    
		<script src="http://www.google.com/jsapi?key=ABQIAAAArtnzOH7TK00m4RFuKeW8GBSFN74nUGzixgqmawSLETOEvB7cSxRIewbFpTTq-aLn7DJfm2crUUhaoA" type="text/javascript"></script>
		<script language="Javascript" type="text/javascript">    
			//<![CDATA[    
			google.load("search", "1");    
			function OnLoad() 
			{
				// Create a search control      
				var searchControl = new google.search.SearchControl();
				// Add in a full set of searchers      
//				var localSearch = new google.search.LocalSearch();      
//				searchControl.addSearcher(localSearch);      
//				searchControl.addSearcher(new google.search.WebSearch());      
//				searchControl.addSearcher(new google.search.VideoSearch());      
//				searchControl.addSearcher(new google.search.BlogSearch());      

				var options = new GsearcherOptions();
				options.setExpandMode(google.search.SearchControl.EXPAND_MODE_OPEN);
				
				// all searchers will run in large mode
				searchControl.setResultSetSize(GSearch.LARGE_RESULTSET);
        
				var siteSearch = new google.search.WebSearch();
				siteSearch.setUserDefinedLabel("DontStayIn.com");
				siteSearch.setUserDefinedClassSuffix("siteSearch");
				siteSearch.setSiteRestriction("dontstayin.com");
				searchControl.addSearcher(siteSearch, options);
				
				var siteImageSearch = new GimageSearch();
				siteImageSearch.setUserDefinedLabel("DontStayIn.com photos");
//				siteImageSearch.setUserDefinedClassSuffix("siteSearch");
//				siteImageSearch.setSiteRestriction("dontstayin.com");
//				searchControl.addSearcher(siteImageSearch, options);

				searchControl.setSearchCompleteCallback(document, searchCompleteHandler);
				
				// Set the Local Search center point      
//				localSearch.setCenterPoint("London, UK");      
				// Tell the searcher to draw itself and tell it where to attach      
				searchControl.draw(document.getElementById("searchcontrol"));
				//$("INPUT.gsc-search-button").mousedown( function() { alert("Hello gits"); } );
				//alert($("INPUT.gsc-search-button").trigger);
				// Execute an inital search      searchControl.execute("Google");
				
				$("DIV.gsc-control").css("width", "575");
				var searchParam = getURLParam("searchParam");
				searchControl.execute(searchParam);
			}    
			
			function getURLParam(strParamName)
			{
			  var strReturn = "";
			  var strHref = window.location.href;
			  if ( strHref.indexOf("?") > -1 ){
				var strQueryString = strHref.substr(strHref.indexOf("?")).toLowerCase();
				var aQueryString = strQueryString.split("&");
				for ( var iParam = 0; iParam < aQueryString.length; iParam++ ){
				  if (aQueryString[iParam].indexOf(strParamName.toLowerCase() + "=") > -1 )
				  {
					var aParam = aQueryString[iParam].split("=");
					strReturn = aParam[1];
					break;
				  }
				}
			  }
			  return unescape(strReturn);
			} 

			
			function searchCompleteHandler()
			{
				$("TABLE.gsc-resultsHeader").css("display", "none");
				$("DIV.gs-visibleUrl-short").css("display", "none");
				$("DIV.gsc-ad-box").css("display", "none");
				
				$("A.gs-title").each(function(i)
				{
					this.innerHTML = this.innerHTML.replace(/<b>/gi, "");
					this.innerHTML = this.innerHTML.replace(/ - DontStayIn/gi, "");
				});
				
				$("DIV.gs-snippet").each(function(i)
				{
					this.innerHTML = this.innerHTML.replace(/<b>/gi, "");
				});				
			}
			
			google.setOnLoadCallback(OnLoad);    
		</script>  
	</head>  
	
	<body>
		<dsi:h1 ID="H1" runat="Server">Search</dsi:h1>
		<div class="ContentBorder">
			<p>
				<div id="searchcontrol"></div>
			</p>

		</div>
		


	</body>
</html>
