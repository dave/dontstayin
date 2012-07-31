<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GoogleMap.ascx.cs" Inherits="Spotted.Pages.Venues.GoogleMap" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml">
  <head>
		<title>DontStayIn Venue Google Map Page</title>    
		<script src="http://www.google.co.uk/jsapi?key=<%=GoogleKey %>" type="text/javascript"></script>
		<script src="http://maps.google.co.uk/maps?file=api&amp;v=2&amp;key=<%=GoogleKey %>" type="text/javascript"></script>
  </head>
  <body onunload="GUnload()">
	
    <!-- you can use tables or divs for the overall layout -->
    <a href="#" onclick="ShowVenue();">Show Venue</a>&nbsp;&nbsp;&nbsp;<a href="#" onclick="ShowDirections();">Show Directions</a>
    
    <table id="MapTable" style="display:none;" border=0 cellpadding=0 cellspacing=0>
      <tr>        
        <td valign="top" >
           <div id="directions" style="width:180px; overflow:auto;height:500px"></div>
        </td>
        <td>
           <div id="map" style="width: 420px; height: 500px"></div>
        </td>
      </tr>
    </table>
    <br /><br />
    <noscript><b>JavaScript must be enabled in order for you to use Google Maps.</b> 
      However, it seems JavaScript is either disabled or not supported by your browser. 
      To view Google Maps, enable JavaScript by changing your browser options, and then 
      try again.
    </noscript>


    <script type="text/javascript">
    //<![CDATA[

    if (GBrowserIsCompatible()) 
    {
      var side_bar_html = "";
      var gmarkers = [];
      var htmls = [];
      var i = 0;
      // arrays to hold variants of the info window html with get direction forms open
      var to_htmls = [];
      var from_htmls = [];
      
      var map;
	  var mapTable;
	  var mapDiv;
	  var searchControl;
	  var localSearch;
	  var icon;
 	  var venuePostcode = '';
	  var userPostcode = '';
	  var venueGLatLng;
	  var userGLatLng;
	  var userSelectionGLatLng;
	  var directions = document.getElementById("directions");
	  var isShowVenue = false;
	  var gdir;
	  var venueCountry = 'UK';
	  var userCountry = 'UK';
	  var waitingTask = '';


      // A function to create the marker and set up the event window
      function createMarker(point,name,html) 
      {
        var marker = new GMarker(point);

        // The info window version with the "to here" form open
        to_htmls[i] = html + '<br>Directions: <b>To here</b> - <a href="javascript:fromhere(' + i + ')">From here</a>' +
           '<br>Start address:<form action="javascript:getDirections()">' +
           '<input type="text" SIZE=40 MAXLENGTH=40 name="saddr" id="saddr" value="" /><br>' +
           '<INPUT value="Get Directions" TYPE="SUBMIT">' +
           '<input type="hidden" id="daddr" value="'+point.lat() + ',' + point.lng() + 
           '"/>';
        // The info window version with the "to here" form open
        from_htmls[i] = html + '<br>Directions: <a href="javascript:tohere(' + i + ')">To here</a> - <b>From here</b>' +
           '<br>End address:<form action="javascript:getDirections()">' +
           '<input type="text" SIZE=40 MAXLENGTH=40 name="daddr" id="daddr" value="" /><br>' +
           '<INPUT value="Get Directions" TYPE="SUBMIT">' +
           '<input type="hidden" id="saddr" value="'+point.lat() + ',' + point.lng() +
           '"/>';
        // The inactive version of the direction info
        html = html + '<br>Directions: <a href="javascript:tohere('+i+')">To here</a> - <a href="javascript:fromhere('+i+')">From here</a>';

        GEvent.addListener(marker, "click", function() {
          marker.openInfoWindowHtml(html);
        });
        // save the info we need to use later for the side_bar
        gmarkers[i] = marker;
        htmls[i] = html;
        // add a line to the side_bar html
        side_bar_html += '<a href="javascript:myclick(' + i + ')">' + name + '</a><br>';
        i++;
        return marker;
      }

      // ===== request the directions =====
      function getDirections() 
      {
        var saddr = document.getElementById("saddr").value
        var daddr = document.getElementById("daddr").value
        
        if(saddr != '' && daddr != '')
        {
			mapDiv.style.width = '420';
			directions.style.width = '180';
			GenerateMap();
			if(saddr == venueGLatLng.lat() + "," + venueGLatLng.lng())
			{
				setUserSelectionLatitudeAndLongitudeFromAddress(daddr);
				waitingTask = 'UserSelectedFromVenue';
			}
			else if(daddr == venueGLatLng.lat() + "," + venueGLatLng.lng())
			{
				setUserSelectionLatitudeAndLongitudeFromAddress(saddr);
				waitingTask = 'UserSelectedToVenue';
			}
		}
      }
      
      function ProcessWaitingTask()
      {
		if(waitingTask == 'UserSelectedFromVenue')
			OverlayDirections(venueGLatLng, userSelectionGLatLng);
		else if(waitingTask == 'UserSelectedToVenue')
			OverlayDirections(userSelectionGLatLng, venueGLatLng);
		else if(waitingTask == 'UserHomeToVenue')
			OverlayDirections(userGLatLng, venueGLatLng);
		else if(waitingTask == 'ShowVenue')
			ShowVenueMarker();
      }


	  function GenerateMap()
	  {
			// Generate new map
			map = new GMap2(document.getElementById("map"));
			map.addControl(new GLargeMapControl());
			map.addControl(new GMapTypeControl());
			if(venueGLatLng != null)
				map.setCenter(venueGLatLng, 14);
			else
				map.setCenter(new GLatLng(51.494502,-0.139553), 7);
	
			// create a GDirections Object
			gdir=new GDirections(map, directions);
			directions.innerHTML = '';
	  }

	  function ShowVenue()
	  {
		if(mapTable.style.display == 'none')
		{
			waitingTask = 'ShowVenue';
			mapTable.style.display = 'block';
			mapDiv.style.width = '600';
			directions.style.width = '0';
			GenerateMap();
			setVenueLatitudeAndLongitudeFromAddress(venuePostcode);			
		}		
		else
		{
			mapTable.style.display = 'none';
		}
	  }

	  function ShowDirections()
	  {
		if(mapTable.style.display == 'none')
		{
			mapTable.style.display = 'block';
			mapDiv.style.width = '420';
			directions.style.width = '180';
			
			GenerateMap();
			
			waitingTask = 'UserHomeToVenue';
		    
			setVenueLatitudeAndLongitudeFromAddress(venuePostcode);
			setUserLatitudeAndLongitudeFromAddress(userPostcode);
		}
		else
		{
			mapTable.style.display = 'none';
		}
	  }
	  
	  function ShowVenueMarker()
	  {
		if(venueGLatLng != null && map != null)
		{
			map.setCenter(venueGLatLng, 14);
			var marker = createMarker(venueGLatLng,'End point','<big><b><%= CurrentVenue.Name %></b></big> <nobr>(<%= CurrentVenue.Postcode %>)</nobr>');
			map.addOverlay(marker);
		}
	  }
	  
	  function OverlayDirections(startPoint, endPoint)
	  {
		if(startPoint != null && endPoint != null && map != null && gdir != null)
		{		
			mapDiv.style.width = '420';
			directions.style.width = '180';
			gdir.load("from: "+startPoint.lat() + "," + startPoint.lng()+" to: "+endPoint.lat() + "," + endPoint.lng());
		}
	  }


      // This function picks up the click and opens the corresponding info window
      function myclick(i) 
      {
        gmarkers[i].openInfoWindowHtml(htmls[i]);
      }

      // functions that open the directions forms
      function tohere(i) 
      {
        gmarkers[i].openInfoWindowHtml(to_htmls[i]);
      }
      function fromhere(i) 
      {
        gmarkers[i].openInfoWindowHtml(from_htmls[i]);
      }
      
		



		google.load("search", "1");  

		function GoogleMapOnLoad()
		{
			//alert('googlemaponload');
			searchControl = new google.search.SearchControl();
			// Add in a full set of searchers      
			localSearch = new google.search.LocalSearch();   
			localSearch2 = new google.search.LocalSearch();   
			localSearch3 = new google.search.LocalSearch();   
			
			mapTable = document.getElementById("MapTable");
			mapDiv = document.getElementById("map");
			
			venuePostcode = '<%=CurrentVenue.Postcode %>';
			userPostcode = '<%=Usr.Current.AddressPostcode%>';
			
			venueCountry = '<%=CurrentVenue.Place.Country.Name %>';
			//userCountry =  '<%=Usr.Current.AddressCountry.Name %>';
			
			
			waitingTask = '';
			setVenueLatitudeAndLongitudeFromAddress(venuePostcode);
			setUserLatitudeAndLongitudeFromAddress(userPostcode);		
		}
		      
      function setVenueLatitudeAndLongitudeFromAddress(address) 
		{
			localSearch2.setSearchCompleteCallback(null, 
				function() 
				{					
					if (localSearch2.results[0])
					{		
						venueGLatLng = new GLatLng(localSearch2.results[0].lat,localSearch2.results[0].lng);
						ProcessWaitingTask();
					}
					else
					{
						alert("Address not found! Please try again.");
					}
				});	
			if(venueGLatLng == null)
			{
				localSearch2.execute(address + ", " + venueCountry);
			}
			else
			{
				ProcessWaitingTask();
			}			
		}
		
		function setUserLatitudeAndLongitudeFromAddress(address) 
		{
			localSearch.setSearchCompleteCallback(null, 
				function() 
				{					
					if (localSearch.results[0])
					{		
						userGLatLng = new GLatLng(localSearch.results[0].lat,localSearch.results[0].lng);
						ProcessWaitingTask();
					}
					else
					{
						alert("Address not found! Please try again.");
					}
				});	
			if(userGLatLng == null)
			{
				localSearch.execute(address + ", " + venueCountry);
			}
			else
			{
				ProcessWaitingTask();
			}
		}
		
		function setUserSelectionLatitudeAndLongitudeFromAddress(address) 
		{
			localSearch3.setSearchCompleteCallback(null, 
				function() 
				{					
					if (localSearch3.results[0])
					{		
						userSelectionGLatLng = new GLatLng(localSearch3.results[0].lat,localSearch3.results[0].lng);
						ProcessWaitingTask();
					}
					else
					{
						alert("Address not found! Please try again.");
					}
				});	
			localSearch3.execute(address + ", " + venueCountry);
		}
		
		google.setOnLoadCallback(GoogleMapOnLoad); 
  
  
      // === Array for decoding the failure codes ===
      var reasons=[];
      reasons[G_GEO_SUCCESS]            = "Success";
      reasons[G_GEO_MISSING_ADDRESS]    = "Missing Address: The address was either missing or had no value.";
      reasons[G_GEO_UNKNOWN_ADDRESS]    = "Unknown Address:  No corresponding geographic location could be found for the specified address.";
      reasons[G_GEO_UNAVAILABLE_ADDRESS]= "Unavailable Address:  The geocode for the given address cannot be returned due to legal or contractual reasons.";
      reasons[G_GEO_BAD_KEY]            = "Bad Key: The API key is either invalid or does not match the domain for which it was given";
      reasons[G_GEO_TOO_MANY_QUERIES]   = "Too Many Queries: The daily geocoding quota for this site has been exceeded.";
      reasons[G_GEO_SERVER_ERROR]       = "Server error: The geocoding request could not be successfully processed.";
      reasons[G_GEO_BAD_REQUEST]        = "A directions request could not be successfully parsed.";
      reasons[G_GEO_MISSING_QUERY]      = "No query was specified in the input.";
      reasons[G_GEO_UNKNOWN_DIRECTIONS] = "The GDirections object could not compute directions between the points.";

      // === catch Directions errors ===
      GEvent.addListener(gdir, "error", function() 
      {
        var code = gdir.getStatus().code;
        var reason="Code "+code;
        if (reasons[code]) 
        {
          reason = reasons[code]
        } 

        alert("Failed to obtain directions, "+reason);
      });
    }

    else 
    {
      alert("Sorry, the Google Maps API is not compatible with this browser");
    }

    // This Javascript is based on code provided by the
    // Blackpool Community Church Javascript Team
    // http://www.commchurch.freeserve.co.uk/   
    // http://econym.googlepages.com/index.htm

    //]]>
    </script>
  </body>

</html>
