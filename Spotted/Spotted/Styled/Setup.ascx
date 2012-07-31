<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Setup.ascx.cs" Inherits="Spotted.Styled.Setup" %>
<script src="/misc/ColourPicker.js" type="text/javascript"></script>
<script type="text/javascript">
	function Switch()
	{
		
		document.getElementById("PreviewIframe").src = document.getElementById("SelectCSS").value + '.htm';
	}
</script>
<div>
	<table>
		<tr>
			<td valign="top" style="padding-right:4px; border-bottom:dotted 1px black;">			
				Step 1 - Choose your colours<br />
				<select id="SelectCSS" onchange="Switch();">
					<option value="default">Default</option>
					<option value="blue">Blue</option>
					<option value="green">Green</option>
					<option value="black">Black</option>
					<option value="pink">Pink</option>
					<option value="default">Upload your own</option>
					<option value="frantic">Frantic</option>
					<option value="tasty">Tasty</option>
					<option value="pacha">Pacha</option>
				</select>
				<br />
				<input type="file" runat="server" id="InputCssFile" size="40" /> 
				<button ID="UploadCssButton" runat="server" onserverclick="UploadCssButton_Click">Upload</button>
				<asp:TextBox ID="CssUrlHiddenTextBox" runat="server" style="display:none;"></asp:TextBox>
			</td>
			<td rowspan="3" valign="top" valign="top" style="padding-left:4px; border-left:dotted 1px black;">	
				<center><strong>Preview</strong></center>
				<iframe id="PreviewIframe" src="default.htm" scrolling="no" style="zoom:65%; width:880px; height:600px;" ></iframe>
			</td>
		</tr>
		<tr>
			<td style="border-bottom:dotted 1px black;" valign="top">Step 2 - Upload images
				<br /><br />
				Logo image<br />
				<input type="file" runat="server" id="InputLogoFile" size="40" /> 
				<button ID="UploadLogoButton" runat="server" onserverclick="UploadLogoButton_Click">Upload</button>
				<asp:TextBox ID="LogoUrlHiddenTextBox" runat="server" style="display:none;"></asp:TextBox>
				<br />Background image<br />
				<input type="file" runat="server" id="InputBackgroundFile" size="40"/> 
				<button ID="UploadBackgroundButton" runat="server" onserverclick="UploadBackgroundButton_Click">Upload</button>
				<asp:TextBox ID="BackgroundUrlHiddenTextBox" runat="server" style="display:none;"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td valign="top">Step 3 - Customise<br />
			
			<div id="colorpicker201" class="colorpicker201"></div>
			<table>
			<tr>
			<td style="border-bottom:solid 1px black;">Tags</td><td style="border-bottom:solid 1px black;">Text color</td><td style="border-bottom:solid 1px black;">Background</td><td style="border-bottom:solid 1px black;">Font family</td><td style="border-bottom:solid 1px black;">Font size</td><td style="border-bottom:solid 1px black;">Font weight</td>
			</tr>

			<tr>
			<td>Body:</td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_1a','sample_1a');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_1a" value=""><input type="text" ID="sample_1a" style="width:10px;" value=""></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_1b','sample_1b');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_1b" value=""><input type="text" ID="sample_1b" style="width:10px;" value=""></td>
			<td>
			<select title='font-family' name='ffamily' onfocus="toset('ffamily',this, '','');" onChange="setvalff(this.value,'body');" class='drop' >
			<option value=''></option>
			<option value='arial'>Arial</option>
			<option value='comic'>Comic Sans MS</option>
			<option value='courier'>Courier New</option>
			<option value='times'>Times New Roman</option>
			<option value='trebuchet'>Trebuchet MS</option>
			<option value='verdana'>Verdana</option>
			</select>
			</td><td>
			<select title='font-size' name='fsize' onfocus="toset('fsize',this,'','');" onChange="setvalsize(this.value,'body');" class='drop' >
			<option value=''></option>
			<option value='xx-small'>XX-Small</option>
			<option value='x-small'>X-Small</option>
			<option value='small'>Small</option>
			<option value='medium' >Medium</option>
			<option value='large'>Large</option>
			<option value='x-large'>X-Large</option>
			<option value='xx-large'>XX-Large</option>
			</select>
			</td>
			<td><select title='font-weight' name='fweight' onfocus="toset('fweight',this,'','');" onChange="setvalweight(this.value,'body');" class='drop' >
			<option value=''></option>
			<option value='bold'>Bold</option>
			<option value='bolder'>Bolder</option>
			<option value='lighter'>Lighter</option>
			<option value='normal'>Normal</option>
			</select></td>
			<td></td>
			</tr>
			<tr><td><font>Headers:</font></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_2a','sample_2a');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_2a" value=""><input type="text" ID="sample_2a" style="width:10px;" value=""></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_2b','sample_2b');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_2b" value=""><input type="text" ID="sample_2b" style="width:10px;" value=""></td>
			<td>
			<select title='font-family' name='fontfamily' onfocus="toset('fontfamily',this,'','');" onChange="setvalff(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='arial'>Arial</option>
			<option value='comic'>Comic Sans MS</option>
			<option value='courier'>Courier New</option>
			<option value='times'>Times New Roman</option>
			<option value='trebuchet'>Trebuchet MS</option>
			<option value='verdana'>Verdana</option>
			</select>
			</td>
			<td><select title='font-size' name='fontsize' onfocus="toset('fontsize',this,'','');" onChange="setvalsize(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='xx-small'>XX-Small</option>
			<option value='x-small'>X-Small</option>
			<option value='small'>Small</option>
			<option value='medium' >Medium</option>
			<option value='large'>Large</option>
			<option value='x-large'>X-Large</option>
			<option value='xx-large'>XX-Large</option>
			</select> </td>
			<td><select title='font-weight' name='fontweight' onfocus="toset('fontweight',this,'','');" onChange="setvalweight(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='bold'>Bold</option>
			<option value='bolder'>Bolder</option>
			<option value='lighter'>Lighter</option>
			<option value='normal'>Normal</option>
			</select></td>
			</tr>
			<tr><td><font>Links:</font></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_3a','sample_3a');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_3a" value=""><input type="text" ID="sample_3a" style="width:10px;" value=""></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_3b','sample_3b');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_3b" value=""><input type="text" ID="sample_3b" style="width:10px;" value=""></td>
			<td>
			<select title='font-family' name='fontfamily' onfocus="toset('fontfamily',this,'','');" onChange="setvalff(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='arial'>Arial</option>
			<option value='comic'>Comic Sans MS</option>
			<option value='courier'>Courier New</option>
			<option value='times'>Times New Roman</option>
			<option value='trebuchet'>Trebuchet MS</option>
			<option value='verdana'>Verdana</option>
			</select>
			</td>
			<td><select title='font-size' name='fontsize' onfocus="toset('fontsize',this,'','');" onChange="setvalsize(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='xx-small'>XX-Small</option>
			<option value='x-small'>X-Small</option>
			<option value='small'>Small</option>
			<option value='medium' >Medium</option>
			<option value='large'>Large</option>
			<option value='x-large'>X-Large</option>
			<option value='xx-large'>XX-Large</option>
			</select> </td>
			<td><select title='font-weight' name='fontweight' onfocus="toset('fontweight',this,'','');" onChange="setvalweight(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='bold'>Bold</option>
			<option value='bolder'>Bolder</option>
			<option value='lighter'>Lighter</option>
			<option value='normal'>Normal</option>
			</select></td>
			</tr>
			<tr><td><font>Links hover:</font></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_4a','sample_4a');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_4a" value=""><input type="text" ID="sample_4a" style="width:10px;" value=""></td>
			<td><img src="/gfx/paint.gif" height="13" width="15" onclick="showColorGrid2('input_field_4b','sample_4b');" border="0" style="cursor:pointer" alt="select color" title="select color"><input type="text" size="6" ID="input_field_4b" value=""><input type="text" ID="sample_4b" style="width:10px;" value=""></td>
			<td>
			<select title='font-family' name='fontfamily' onfocus="toset('fontfamily',this,'','');" onChange="setvalff(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='arial'>Arial</option>
			<option value='comic'>Comic Sans MS</option>
			<option value='courier'>Courier New</option>
			<option value='times'>Times New Roman</option>
			<option value='trebuchet'>Trebuchet MS</option>
			<option value='verdana'>Verdana</option>
			</select>
			</td>
			<td><select title='font-size' name='fontsize' onfocus="toset('fontsize',this,'','');" onChange="setvalsize(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='xx-small'>XX-Small</option>
			<option value='x-small'>X-Small</option>
			<option value='small'>Small</option>
			<option value='medium' >Medium</option>
			<option value='large'>Large</option>
			<option value='x-large'>X-Large</option>
			<option value='xx-large'>XX-Large</option>
			</select> </td>
			<td><select title='font-weight' name='fontweight' onfocus="toset('fontweight',this,'','');" onChange="setvalweight(this.value,'font');" class='drop' >
			<option value=''></option>
			<option value='bold'>Bold</option>
			<option value='bolder'>Bolder</option>
			<option value='lighter'>Lighter</option>
			<option value='normal'>Normal</option>
			</select></td>
			</tr>


			</table>		
			</td>
		</tr>
		
	</table>
</div>
<div style="width:1px; height:420px; z-index:9000; left:250px; top:0px; background-color:#CCCCCC; filter:alpha(opacity=10); position:absolute;"></div>
