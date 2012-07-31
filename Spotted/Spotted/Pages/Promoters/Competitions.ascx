<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Competitions.ascx.cs" Inherits="Spotted.Pages.Promoters.Competitions" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Competitions">
	<asp:Panel Runat="server" ID="InfoPanel">
		<h2>It's FREE to add a competition</h2>
		<p>
			Just click the button below and complete the form.
		</p>
		<p>
			When you've finished editing your competition, and added a picture, you must click the Publish 
			button. We will then check it and make it live on the site. If you don't click Publish, it won't
			be enabled!
		</p>
		<p>
			<a href="<%= CurrentPromoter.UrlApp("competitions","mode","add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">add a competition</a>
		</p>
	</asp:Panel>
</dsi:PromoterIntro>

<asp:Panel Runat="server" ID="PanelEdit">
	<dsi:h1 runat="server" ID="H14">Add a competition</dsi:h1>
	<div class="ContentBorder">
		<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary1" NAME="Validationsummary1"/>
		<h2>Instructions</h2>
		<p>
			You just complete the details below. Enter a question, three answers, the prizes and we do the rest. 
			We'll draw your competition automatically at midday on the date you choose, and we'll invite you to 
			a private message with each of the winners.
		</p>
		<p>
			If you've giving out more than £100 worth of prizes (excluding guestlists or event-based offers), 
			we'll list your competition at the top of the competitions box.
		</p>
		<p>
			Only a couple of restrictions on prizes – if you're giving away guestlists, make sure they are for 
			free entry - <b>not</b> concessions, and please <b>don't</b> put special restrictions on them 
			e.g. "arrive before 10pm for free entry".
		</p>
		<table cellpadding="2" cellspacing="2">
			<tr><td colspan="3"><h2>Prize details</h2></td></tr>
			<tr><td colspan="3"><hr size="1" color="#CBA21E"></td></tr>
			<tr>
				<td rowspan="2"><h2><nobr>1st prize(s):</nobr></h2></td>
				<td>number of winners:</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesFirstNumber" Columns="2" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="None" ControlToValidate="EditPrizesFirstNumber"
						ErrorMessage="Please enter a number of first prizes"/>
					<asp:CompareValidator ID="CompareValidator1" Runat="server" Display="None" ControlToValidate="EditPrizesFirstNumber"
						ErrorMessage="Please enter a number for the number of first prizes" 
						ValueToCompare="0" Operator="GreaterThan" Type="Integer"/>
				</td>
			</tr>
			<tr>
				<td>
					prize description:
				</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesFirstDesc" Columns="50" MaxLength="100" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" Display="None" ControlToValidate="EditPrizesFirstDesc"
						ErrorMessage="Please enter a description of the first prize"/>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator1" Runat="server" Display="None" ControlToValidate="EditPrizesFirstDesc"
						ErrorMessage="First prize description must be between 10 and 100 characters" ValidationExpression="^.{10,100}$"/>
				</td>
			</tr>
			<tr><td colspan="3"><hr size="1" color="#CBA21E"></td></tr>
			<tr>
				<td rowspan="2"><h2><nobr>2nd prize(s):</nobr></h2></td>
				<td>number of winners:</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesSecondNumber" Columns="2" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:CompareValidator ID="CompareValidator2" Runat="server" Display="None" ControlToValidate="EditPrizesSecondNumber"
						ErrorMessage="Please enter a number for the number of second prizes. If there are none, please enter 0." 
						ValueToCompare="0" Operator="GreaterThanEqual" Type="Integer"/>
					<small>leave this blank if you don't have second prizes</small>
				</td>
			</tr>
			<tr>
				<td>
					prize description:
				</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesSecondDesc" Columns="50" MaxLength="100" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator2" Runat="server" Display="None" ControlToValidate="EditPrizesSecondDesc"
						ErrorMessage="Second prize description must be between 10 and 100 characters" ValidationExpression="^.{10,100}$"/>
				</td>
			</tr>
			<tr><td colspan="3"><hr size="1" color="#CBA21E"></td></tr>
			<tr>
				<td rowspan="2"><h2><nobr>Runners<br>up prize(s):</nobr></h2></td>
				<td>number of winners:</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesThirdNumber" Columns="2" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:CompareValidator ID="CompareValidator3" Runat="server" Display="None" ControlToValidate="EditPrizesSecondNumber"
						ErrorMessage="Please enter a number for the number of runners up prizes. If there are none, please enter 0." 
						ValueToCompare="0" Operator="GreaterThanEqual" Type="Integer"/>
					<small>leave this blank if you don't have runners-up prizes</small>
				</td>
			</tr>
			<tr>
				<td>
					prize description:
				</td>
				<td>
					<asp:TextBox Runat="server" ID="EditPrizesThirdDesc" Columns="50" MaxLength="100" style="border:1px solid #A58319;"></asp:TextBox>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator3" Runat="server" Display="None" ControlToValidate="EditPrizesThirdDesc"
						ErrorMessage="Runners up prize description must be between 10 and 100 characters" ValidationExpression="^.{10,100}$"/>
				</td>
			</tr>
			<tr><td colspan="3"><hr size="1" color="#CBA21E"></td></tr>
		</table>
		<p>
			<table>
				<tr>
					<td colspan="2" valign="top">
						<nobr>Total cash value of all non-guestlist prizes:&nbsp;</nobr>
					</td>
					<td>
						<asp:DropDownList Runat="server" ID="EditPrizesValue">
							<asp:ListItem Value="1">£0 - £99</asp:ListItem>
							<asp:ListItem Value="2">£100 - £499</asp:ListItem>
							<asp:ListItem Value="3">£500+</asp:ListItem>
						</asp:DropDownList><br>
						<small>Please exclude all prizes that are a guestlist or offer based on entry to an event</small>
					</td>
				</tr>
			</table>
		</p>
		
		<h2>Sponsor details</h2>
		
		<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Runat="server" Display="None" ControlToValidate="EditSponsorDescriptionHtml"
			ErrorMessage="<p>Please enter a sponsor description</p>" />
		<asp:RegularExpressionValidator ID="RegularExpressionValidator4" Runat="server" Display="None" ControlToValidate="EditSponsorDescriptionHtml"
			ErrorMessage="<p>Sponsor description must be 2000 characters or less</p>" ValidationExpression="^(.|\n){1,2000}$"/>
		<p>
			<dsi:Html runat="server" id="EditSponsorDescriptionHtml" PreviewType="Competition" DisableSaveButton="true" DisableContainer="true" />
		</p>
		<p>
			<small>Enter a couple of paragraphs about the sponsor or promotion -<br>max 2000 characters.</small>
		</p>
		
		<table cellpadding="5" cellspacing="2">
			<tr runat="server" id="EditLinkTr">
				<td valign="top">
					More<br>details<br>link:
				</td>
				<td>
					<p>
						<asp:RadioButton Runat="server" ID="EditLinkNoneRadio" 
							GroupName="EditLinkGroup" Text="Don't include a more details link"/>
					</p>
					<p runat="server" id="EditLinkEventP">
						<asp:RadioButton Runat="server" ID="EditLinkEventRadio" 
							GroupName="EditLinkGroup" Text="An upcoming event:"/>
						<a href="" runat="server" id="EditLinkEventAnchor" target="_blank"></a>
						<asp:DropDownList Runat="server" ID="EditLinkEventDropDown" style="width:350px;"></asp:DropDownList>
					</p>
					<p runat="server" id="EditLinkBrandP">
						<asp:RadioButton Runat="server" ID="EditLinkBrandRadio" 
							GroupName="EditLinkGroup" Text="Your brand page:"/>
						<a href="" runat="server" id="EditLinkBrandAnchor" target="_blank"></a>
						<asp:DropDownList Runat="server" ID="EditLinkBrandDropDown"></asp:DropDownList>
					</p>
					<p>
						<small>
							We can include a 'more details' link at the bottom of your
							sponsor details. This also helps us promote the competition
							on the right pages.
						</small>
					</p>
					<asp:CustomValidator ID="CustomValidator1" Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="LinkRadioVal" 
						ErrorMessage="Choose a more details link by clicking one of the buttons below."/>
				</td>
			</tr>
		</table>
		
		<table cellpadding="5" cellspacing="2">
			<tr>
				<td colspan="3">
					<h2>Competition</h2>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Question:
				</td>
				<td valign="top" colspan="2">
					<asp:TextBox Runat="server" ID="EditQuestion" Columns="70" MaxLength="200" style="border:1px solid #A58319;"></asp:TextBox><br>
					<small>Enter up to 200 characters</small><br>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Runat="server" Display="None" ControlToValidate="EditQuestion"
						ErrorMessage="Please enter a question"/>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator5" Runat="server" Display="None" ControlToValidate="EditQuestion"
						ErrorMessage="Question must be between 10 and 200 characters" ValidationExpression="^.{10,200}$"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Answer 1:
				</td>
				<td valign="top" width="200">
					<asp:TextBox Runat="server" ID="EditAnswer1" Columns="30" MaxLength="50" style="border:1px solid #A58319;"></asp:TextBox><br>
					<small>Enter up to 50 characters</small><br>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Runat="server" Display="None" ControlToValidate="EditAnswer1"
						ErrorMessage="Please enter answer 1"/>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator6" Runat="server" Display="None" ControlToValidate="EditAnswer1"
						ErrorMessage="Answer 1 must not be more than 50 characters" ValidationExpression="^.{1,50}$"/>
				</td>
				<td valign="top">
					<asp:RadioButton Runat="server" ID="EditCorrectRadio1" 
							GroupName="EditCorrectGroup" Text="Answer 1 is the correct answer"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Answer 2:
				</td>
				<td valign="top" width="200">
					<asp:TextBox Runat="server" ID="EditAnswer2" Columns="30" MaxLength="50" style="border:1px solid #A58319;"></asp:TextBox><br>
					<small>Enter up to 50 characters</small><br>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator6" Runat="server" Display="None" ControlToValidate="EditAnswer2"
						ErrorMessage="Please enter answer 2"/>
					<asp:RegularExpressionValidator ID="RegularExpressionValidator7" Runat="server" Display="None" ControlToValidate="EditAnswer2"
						ErrorMessage="Answer 2 must not be more than 50 characters" ValidationExpression="^.{1,50}$"/>
				</td>
				<td valign="top">
					<asp:RadioButton Runat="server" ID="EditCorrectRadio2" 
							GroupName="EditCorrectGroup" Text="Answer 2 is the correct answer"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Answer 3:
				</td>
				<td valign="top" width="200">
					<asp:TextBox Runat="server" ID="EditAnswer3" Columns="30" MaxLength="50" style="border:1px solid #A58319;"></asp:TextBox><br>
					<small>Enter up to 50 characters</small>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator7" Runat="server" Display="None" ControlToValidate="EditAnswer3"
						ErrorMessage="Please enter answer 3" />
					<asp:RegularExpressionValidator ID="RegularExpressionValidator8" Runat="server" Display="None" ControlToValidate="EditAnswer3"
						ErrorMessage="Answer 3 must not be more than 50 characters" ValidationExpression="^.{1,50}$"/>
				</td>
				<td valign="top">
					<asp:RadioButton Runat="server" ID="EditCorrectRadio3" 
							GroupName="EditCorrectGroup" Text="Answer 3 is the correct answer"/>
					<asp:CustomValidator ID="CustomValidator2" Runat="server" Display="None" EnableClientScript="False" 
						OnServerValidate="CorrectVal" 
						ErrorMessage="Choose a correct answer by clicking one of the buttons by the answers"/>
				</td>
			</tr>
		</table>
		
		<table cellpadding="5" cellspacing="2">
			<tr>
				<td colspan="3">
					<h2>Other details</h2>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Prize contact:
				</td>
				<td>
					<asp:DropDownList Runat="server" ID="EditPrizeContact"/>
					<p>
						<small>
							This person will be automatically be invited to a private message with 
							each prize winner when the competition is drawn
						</small>
					</p>
				</td>
			</tr>
			<tr>
				<td valign="top">
					<a name="CompEditCalendars"></a>
					Start date:
				</td>
				<td>
					<asp:Calendar Runat="server" ID="EditDateStart" OnSelectionChanged="Calendar_Change"/>
					<asp:CustomValidator Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="DateStartVal" 
						ErrorMessage="Your start date must be BEFORE your end date!" ID="Customvalidator3" NAME="Customvalidator1"/>
					<asp:CustomValidator Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="DateStartVal1" 
						ErrorMessage="The maximum length of a competition in this prize band (£0-£99) is two weeks. We've adjusted the start date acordingly." ID="Customvalidator4" NAME="Customvalidator2"/>
					<asp:CustomValidator Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="DateStartVal2" 
						ErrorMessage="The maximum length of a competition in this prize band (£100-£499) is four weeks. We've adjusted the start date acordingly." ID="Customvalidator5" NAME="Customvalidator3"/>
					<asp:CustomValidator Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="DateStartVal3" 
						ErrorMessage="The maximum length of a competition in this prize band (£500+) is six weeks. We've adjusted the start date acordingly." ID="Customvalidator6" NAME="Customvalidator4"/>
				</td>
			</tr>
			<tr>
				<td valign="top">
					Closing date:
				</td>
				<td>
					<asp:Calendar Runat="server" ID="EditDateClose" OnSelectionChanged="Calendar_Change"/>
					<p>
						<small>
							Competitions close at midday on this day - if you're giving away 
							tickets, make sure you have enough time to distribute them!
						</small>
					</p>
					<asp:CustomValidator ID="CustomValidator7" Runat="server" Display="None" EnableClientScript="True" 
						OnServerValidate="DateCloseVal" 
						ErrorMessage="Choose a closing date. It must be in the future!"/>
				</td>
			</tr>
			
		</table>
		<p>
			<button Runat="server" onserverclick="PanelEdit_Cancel" causesvalidation="false" ID="Button2">&lt;- Cancel</button>
			<asp:Button Runat="server" OnClick="PanelEdit_Save" Text="Save this competition -&gt;" ID="Button3" NAME="Button3"></asp:Button>
		</p>
		<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary2" NAME="Validationsummary1"/>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="PanelPic">
	<dsi:h1 runat="server" ID="H12">Add a picture</dsi:h1>
	<div class="ContentBorder">
		<asp:Panel Runat="server" ID="PicUploadDefaultPanel">
			<h2>Use a default picture</h2>
			<p>
				If you would like to use one of the pictures below instead 
				of uploading a new one, please click the picture below:
			</p>
			<asp:DataList Runat="server" ID="PicUploadDefaultDataList" RepeatColumns="3" CellPadding="5" OnItemCommand="PicUploadDefaultSelect">
				<ItemTemplate>
					<asp:LinkButton Runat="server" CommandName="Select" 
						CommandArgument='<%#((System.Guid)Container.DataItem).ToString()%>' ID="Linkbutton1">
						<img src="<%#Bobs.Storage.Path((System.Guid)Container.DataItem)%>" width="100" height="100" 
							border="0" class="BorderBlack All">
					</asp:LinkButton>
				</ItemTemplate>
			</asp:DataList>
		</asp:Panel>
		<h2>Upload a picture</h2>
		<p>
			To upload a picture for this event, use the controls below:
		</p>
		<asp:Panel Runat="server" ID="PicUploadPanel">
			<Controls:Pic Runat="server" ID="PicUc" OnActionSaved="PanelPicSaved" OnActionNoPic="PanelPicNoPic"/>
		</asp:Panel>
	</div>
</asp:Panel>
	
<asp:Panel Runat="server" ID="PanelList">
	<dsi:h1 runat="server" ID="H11">Your competitions</dsi:h1>
	<div class="ContentBorder">
		<p>
			Below are listed your competitions:
		</p>
		<p>
			<asp:DataGrid Runat="server" ID="CompDataGrid" 
				GridLines="None" AutoGenerateColumns="False"
				BorderWidth=0 CellPadding=3 CssClass=dataGrid 
				AlternatingItemStyle-CssClass="dataGridAltItem"
				HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
				ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="CompDataGridChangePage"
				PageSize="20" PagerStyle-Mode="NumericPages">
				<Columns>
					<asp:TemplateColumn HeaderText="Pic" ItemStyle-BorderWidth="0">
						<ItemTemplate>
							<%#Bobs.Promoter.PicHtml((Bobs.Comp)(Container.DataItem))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Prize">
						<ItemTemplate>
							<a href="<%#((Bobs.Comp)(Container.DataItem)).Url()%>"><%#((Bobs.Comp)(Container.DataItem)).Name%></a>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Start">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Comp)(Container.DataItem)).DateTimeStart, true)%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="End">
							<ItemTemplate>
								<%#Cambro.Misc.Utility.FriendlyDate(((Bobs.Comp)(Container.DataItem)).DateTimeClose, true)%> @ midday
							</ItemTemplate>
						</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Pub-<br>lished">
						<ItemTemplate>
							<img src="<%#(((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Published) || ((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Enabled))?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ena-<br>bled">
						<ItemTemplate>
							<img src="<%#((Bobs.Comp)(Container.DataItem)).Status.Equals(Bobs.Comp.StatusEnum.Enabled)?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Winners<br>picked">
						<ItemTemplate>
							<img src="<%#((Bobs.Comp)(Container.DataItem)).WinnersPicked?"/gfx/icon-tick.png":"/gfx/icon-cross.png"%>" border="0" height="21" width="26" align="absmiddle">
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Entries">
						<ItemTemplate>
							<%#((Bobs.Comp)(Container.DataItem)).Entries.ToString("#,##0")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Options">
						<ItemTemplate>
							<nobr><%#((Bobs.Comp)Container.DataItem).OptionsHtml%></nobr>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</p>
		<p class="MedCenter">
			<a href="<%= CurrentPromoter.UrlApp("competitions","mode","add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
				align="absmiddle" style="margin-right:3px;">add a competition</a>
		</p>
	</div>
</asp:Panel>