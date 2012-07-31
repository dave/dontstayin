<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExploreBox.ascx.cs" Inherits="Spotted.Controls.ExploreBox" %>
<!--%@ OutputCache Duration="6000" VaryByParam="None" VaryByCustom="Browser;Country" %-->


<dsi:h1 runat="server">Where do you want to go out?</dsi:h1>
<div id="ExploreBoxExploreHolder" class="ContentBorder" runat="server">
	<p>
		<select ID="ExploreCountryDropDown" name="ExploreCountryDropDown" onchange="countryChange();" style="height:22px;font-size:14px;">
			<asp:PlaceHolder runat="server" ID="ExploreCountryPh" />
		</select>
		<select ID="ExploreTownDropDown" name="ExploreTownDropDown" style="height:22px;font-size:14px;">
			<asp:PlaceHolder runat="server" ID="ExploreTownPh" />
		</select>
		<button onclick="exploreClick();return false;" style="<%= Vars.IE ? "padding-top:0px; height:23px; font-size:12px; margin-left:4px;" : "height:24px; font-size:14px; margin-left:0px;" %>">
			Explore
		</button>
		<dsi:InlineScript runat="server" ID="TabClientScript">
			<script>
				
				jQuery(document).ready(function() {
					var previouslySelectedTownIndex = document.forms[0].elements["ExploreTownDropDown"].selectedIndex;
					var defaultCountryK = <asp:PlaceHolder runat="server" ID="ExploreDefaultCountryPh" />;
					var selectedCountryK = parseInt(document.forms[0].elements["ExploreCountryDropDown"][document.forms[0].elements["ExploreCountryDropDown"].selectedIndex].value);
				
					if (defaultCountryK != selectedCountryK && selectedCountryK > 0)
					{
						jQuery("#ExploreTownDropDown").ajaxAddOption("/support/getcached.aspx?type=place&countryk=" + selectedCountryK, {}, false, selectTownByIndex, [{"index" : previouslySelectedTownIndex}]);
					}
					
					//Sys.Application.add_navigate(exploreBoxTabNavigate);
				});
				function selectTownByIndex(params)
				{
					document.getElementById("ExploreTownDropDown").selectedIndex = params.index;
				}
				
				function countryChange()
				{
					var countryK = parseInt(document.forms[0].elements["ExploreCountryDropDown"][document.forms[0].elements["ExploreCountryDropDown"].selectedIndex].value);
					if (countryK > 0)
					{
						//jQuery("#ExploreTownDropDown").removeOption(/./);
						jQuery("#ExploreTownDropDown").ajaxAddOption("/support/getcached.aspx?type=place&countryk=" + countryK, {}, false);
					}
					else
					{
						jQuery("#ExploreTownDropDown").removeOption(/./);
						jQuery("#ExploreTownDropDown").addOption({"0" : "Select a country..."}, false);
					}
				}
				function exploreClick()
				{
					var url = document.forms[0].elements["ExploreTownDropDown"][document.forms[0].elements["ExploreTownDropDown"].selectedIndex].value;
					url = url.substring(url.indexOf("$")+1, url.length);
					if (url.length > 1)
						window.location = url;
					else
						alert("Choose a town first...");
				}
				
			</script>
		</dsi:InlineScript>
	</p>
</div>

