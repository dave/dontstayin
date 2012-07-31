<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InsertionOrderScreen.ascx.cs"
	Inherits="Spotted.Admin.InsertionOrderScreen" %>

<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DSIControls" TagName="AddOnlyTextBox" Src="/Controls/AddOnlyTextBox.ascx" %>
<dsi:h1 runat="server" id="H1">Insertion Order</dsi:h1>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
<div class="ContentBorder">
	<asp:Panel ID="MainPanel" runat="server" Width="600px">
		
				<table width="600">
			<tr>
				<td colspan="5">
					<h2>Insertion order details</h2>
				</td>
			</tr>
			<tr>
				<td style="width: 90px;">
					<asp:Label ID="uiInsertionOrderKLabel" runat="server" Text="InsertionOrderK" Visible='<%# InsertionOrder.K > 0 %>'></asp:Label></td>
				<td style="width: 180px;">
					<asp:Label ID="uiInsertionOrderK" runat="server" Text='<%# InsertionOrder.K.ToString() %>' Visible='<%# InsertionOrder.K > 0 %>'></asp:Label>
				</td>
				<td style="width: 30px;">
					&nbsp;</td>
				<td style="width: 136px;">
					<asp:Label ID="uiCreatedDateLabel" runat="server" Text="Created&nbsp;date" Visible='<%# InsertionOrder.K != 0 %>'></asp:Label></td>
				<td style="width: 190px;">
					<asp:Label ID="uiCreatedDate" runat="server" Visible='<%# InsertionOrder.K != 0 %>' Text='<%# InsertionOrder.DateTimeCreated.ToString("dd MMM yyyy") %>'></asp:Label>
				</td>
			</tr>
			<tr><td>
				<asp:Label ID="uiCampaignNameLabel" runat="server" Text="Campaign name"></asp:Label>
				</td><td colspan="4">
					<asp:TextBox ID="uiCampaignName" runat="server" Width="400px" Text='<%# InsertionOrder.CampaignName %>' ></asp:TextBox>
				</td></tr>
			<tr>
				<td style="width: 90px;">
					<asp:Label ID="PromoterLabel" runat="server" Text="Promoter"></asp:Label></td>
				<td>
					<js:HtmlAutoComplete  Width="204px" ID="uiPromoterAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"  
					AutoPostBack="true"/>
				</td>
				<td>
				</td>
				<td style="width: 136px">
					<asp:Label ID="uiCampaignStartLabel" runat="server" Text="Campaign start"></asp:Label>
				</td>
				<td>
					<dsi:Cal ID="uiCampaignStartCal" runat="server" 
					Date="<%# InsertionOrder.CampaignStartDate %>" />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="UserLabel0" runat="server" Text="User"></asp:Label>
				</td>
				<td>
					<asp:DropDownList ID="uiUserDropDownList" runat="server" TabIndex="2" 
					AutoPostBack="True" Height="19px" Width="204px" Visible = '<%# InsertionOrder.PromoterK > 0 %>'>
					</asp:DropDownList>
					<asp:TextBox ID="uiUsrNameOverride" runat="server" Visible='<%# InsertionOrder.UsrK == -1 %>' Text='<%# InsertionOrder.UsrNameOverride%>'></asp:TextBox>
					<asp:CustomValidator ID="uiUsrValidator" runat="server" 
					ErrorMessage="select a user" Display="Dynamic" 
					onservervalidate="uiUsrValidator_ServerValidate" ValidationGroup="OnRaiseValidators"></asp:CustomValidator>
				</td>
				<td />
				<td style="width: 136px">
					<asp:Label ID="uiCampaignEndLabel" runat="server" Text="Campaign end"></asp:Label>
				</td>
				<td>
					<dsi:Cal ID="uiCampaignEndCal" runat="server" 
					Date="<%# InsertionOrder.CampaignEndDate %>" />
					<asp:CustomValidator ID="uiCampaignEndDateAfterStartValidator" runat="server" 
					ErrorMessage="Must be on or after start date" Display="Dynamic" 
					onservervalidate="uiCampaignEndDateAfterStartValidator_ServerValidate" 
					ValidationGroup="OnRaiseValidators"></asp:CustomValidator>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="uiActionUsrLabel" runat="server" Text="Action&nbsp;user"></asp:Label>
				</td>
				<td>
					<js:HtmlAutoComplete ID="uiActionUserAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK" Text='<%# InsertionOrder.ActionUsrK > 0 ? InsertionOrder.ActionUsr.NickName : "" %>' 
					Value="<%# InsertionOrder.ActionUsrK.ToString() %>"  />
					<asp:CustomValidator ID="uiActionUsrValidator" runat="server" 
						ErrorMessage="invalid user" ControlToValidate="uiActionUserAutoComplete" 
						onservervalidate="uiActionUsrValidator_ServerValidate" 
					ValidationGroup="OnRaiseValidators"></asp:CustomValidator>
					
					<td>
					</td>
					<td style="width: 136px">
						<asp:Label ID="uiNextInvoiceDueLabel" runat="server" Text="Next invoice due"></asp:Label>
						
					</td>
					<td>
						<dsi:Cal ID="uiNextInvoiceDue" runat="server" 
						Date="<%# InsertionOrder.NextInvoiceDue %>" />
						
					</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="uiAgencyDiscountLabel" runat="server" Text="Agency discount (%)" Visible = '<%#(this.InsertionOrder.PromoterK != 0 && this.InsertionOrder.Promoter.IsAgency)  %>'></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="uiAgencyDiscount" runat="server"
					AutoPostBack="True" CausesValidation="True" 
					ValidationGroup="uiAgencyDiscountValidationGroup" 
					Visible = '<%#(this.InsertionOrder.PromoterK != 0 && this.InsertionOrder.Promoter.IsAgency)  %>' 
					Text='<%# (this.InsertionOrder.AgencyDiscount * 100d).ToString() %>' 
					ontextchanged="uiAgencyDiscount_TextChanged" />
					
					<asp:RangeValidator ID="uiAgencyDiscountRangeValidator0" runat="server" 
						ErrorMessage="Must be between 0 and 100" ControlToValidate="uiAgencyDiscount" 
						MaximumValue="100" MinimumValue="0"  Type="Double" Display="Dynamic"
						ValidationGroup="uiAgencyDiscountValidationGroup"></asp:RangeValidator>
					<asp:RangeValidator ID="uiAgencyDiscountRangeValidator1" runat="server" 
						ErrorMessage="Must be between 0 and 100" ControlToValidate="uiAgencyDiscount" 
						MaximumValue="100" MinimumValue="0"  Type="Double" Display="Dynamic"
						ValidationGroup="InsertionOrderItemValidationGroup"></asp:RangeValidator>
					<asp:RangeValidator ID="uiAgencyDiscountRangeValidator2" runat="server" 
						ErrorMessage="Must be between 0 and 100" ControlToValidate="uiAgencyDiscount" 
						MaximumValue="100" MinimumValue="0"  Type="Double" Display="Dynamic"
						ValidationGroup="OnRaiseValidators"></asp:RangeValidator>
					
					
				</td>
				<td>
				</td>
				<td style="width: 136px">
					<asp:Label ID="uiCustomerRefLabel0" runat="server" Text="Customer reference"></asp:Label>
				</td>
				<td>
					<asp:TextBox ID="uiCustomerRef" runat="server"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="uiPaymentTermsLabel" runat="server" Text="Payment terms"></asp:Label></td>
				<td>
					<asp:TextBox ID="uiPaymentTerms" runat="server" Text='<%# InsertionOrder.PaymentTerms %>'></asp:TextBox></td>
				<td>
				</td>
				<td style="width: 136px">
					<asp:Label ID="uiInvoicePeriodLabel" runat="server" Text="Invoice period"></asp:Label></td>
				<td>
					<asp:TextBox ID="uiInvoicePeriod" runat="server" Text='<%# InsertionOrder.InvoicePeriod %>'></asp:TextBox></td>
			</tr>
			<tr>
				<td>
					<asp:Label runat="server" ID="uiCampaignCreditsLabel" Text="Campaign credits"></asp:Label></td>
				<td>
					<asp:Label runat="server" ID="uiCampaignCredits" Text=""></asp:Label></td>
				<td>
				</td>
				<td style="width: 136px">
					<asp:CheckBox ID="uiCampaignCreditsOverrideCheckBox" runat="server"
						Checked='<%# InsertionOrder.CampaignCreditsOverriden %>' onclick="setUiCampaignCreditsOverrideDisplay()"
						Height="46px" Text="Override campaign credits" TextAlign="Left" Width="118px" /></td>
				<td>
					<asp:TextBox runat="server" ID="uiCampaignCreditsOverride" Style='display:none;'></asp:TextBox></td>
			</tr>
			<tr>
				<td valign="top">
					<asp:Label ID="NotesLabel" runat="server" Text="Notes"></asp:Label></td>
				<td colspan="4">
					<DSIControls:addonlytextbox id="uiNotesAddOnlyTextBox" runat="server" tabindex="20"
						cssclass="notesAddOnly">
					</DSIControls:addonlytextbox>
				</td>
			</tr>
		</table>
		
		<table>
			<tr>
				<td>
					<h2>
						Item details</h2>
					<asp:GridView ID="uiInsertionOrderItemGridView" runat="server" AutoGenerateColumns="False"
						ShowFooter="True" TabIndex="30" CssClass="dataGrid" AlternatingRowStyle-CssClass="dataGridAltItem"
						GridLines="None" BorderWidth="0" CellPadding="3" HeaderStyle-CssClass="dataGridHeader"
						SelectedRowStyle-CssClass="dataGridSelectedItem" AlternatingRowStyle-VerticalAlign="Top"
						RowStyle-VerticalAlign="Top" OnRowCreated="uiInsertionOrderItemGridView_RowCreated">
						<Columns>
							<asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="left">
								<ItemTemplate>
									<asp:Label runat="server" ID="uiBannerDescriptionLabel" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).Description.ToString() %>' />
								</ItemTemplate>
								<FooterTemplate>
									<asp:TextBox ID="uiDescription" runat="server" />
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField>
								<ItemTemplate>
									<asp:Label ID="uiBannerPositionLabel" runat="server" Text='<%# (Container.DataItem == null || ((Bobs.InsertionOrderItem)Container.DataItem).BannerPosition == 0) ? "" : ((Banner.Positions) ((Bobs.InsertionOrderItem)Container.DataItem).BannerPosition).ToString() %>' />
								</ItemTemplate>
								<FooterTemplate>
									<asp:DropDownList Width="120px" ID="uiBannerPosition" runat="server" onchange="uiBannerPosition_onchange();"/>
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Quantity" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<asp:Label ID="uiImpressionQuantityLabel" runat="server" 
										Visible='<%# Container.DataItem == null || (Container.DataItem as Bobs.InsertionOrderItem).BannerPosition != 0 %>'
										Text    ='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).ImpressionQuantity.ToString() %>' />
								</ItemTemplate>
								<FooterTemplate>
									<asp:TextBox ID="uiImpressionQuantity" runat="server" Width="80px" Text="1"  onchange="recalculateGrossCost(this)"></asp:TextBox>
									<asp:RangeValidator ID="uiImpressionQuantityRangeValidator" Display="Dynamic" runat="server"
										ErrorMessage="Must be at least 1" ControlToValidate="uiImpressionQuantity" MinimumValue="1"
										MaximumValue="99999999" Type="Integer" ValidationGroup="InsertionOrderItemValidationGroup"></asp:RangeValidator>
									<asp:RequiredFieldValidator ID="uiImpressionQuantityRequiredFieldValidator" runat="server"
										ControlToValidate="uiImpressionQuantity" Display="Dynamic" ErrorMessage="Required"
										ValidationGroup="InsertionOrderItemValidationGroup"></asp:RequiredFieldValidator>
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="CPM" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<asp:Label ID="uiCpmLabel" runat="server" 
										Visible='<%# Container.DataItem == null || (Container.DataItem as Bobs.InsertionOrderItem).BannerPosition != 0 %>' 
										Text   ='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).Cpm.ToString("C") %>' /></ItemTemplate>
								<FooterTemplate>
								<asp:TextBox runat="server" Text="" ID="uiCpm" Width="50px" onchange="recalculateGrossCost(this)"/>
								<asp:RangeValidator ID="uiCpmRangeValidator" Display="Dynamic" runat="server" ErrorMessage="Must be greater than 0"
									ControlToValidate="uiCpm" MinimumValue="0.0" MaximumValue="99999999" Type="Double" ValidationGroup="InsertionOrderItemValidationGroup">
								</asp:RangeValidator>
								
								</FooterTemplate> 
							</asp:TemplateField>
							<asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Gross cost" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<asp:Label ID="uiGrossCostLabel" runat="server" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).PriceBeforeDiscount.ToString("C") %>'></asp:Label>
								</ItemTemplate>
								<FooterTemplate>
								<asp:TextBox ID="uiGrossCost" runat="server" />
								<asp:RangeValidator ID="uiGrossCostRangeValidator" Display="Dynamic" runat="server" ErrorMessage="Must be greater than 0"
									ControlToValidate="uiGrossCost" MinimumValue="0.0" MaximumValue="99999999" ValidationGroup="InsertionOrderItemValidationGroup"></asp:RangeValidator>
								<asp:RequiredFieldValidator ID="uiGrossCostRequiredValidator" runat="server"
										ControlToValidate="uiGrossCost" Display="Dynamic" ErrorMessage="Required" ValidationGroup="InsertionOrderItemValidationGroup"></asp:RequiredFieldValidator>
								</FooterTemplate>
							
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Discount" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<nobr><asp:Label ID="uiDiscountLabel" runat="server" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).Discount.ToString("P") %>' /></nobr></ItemTemplate>
								<FooterTemplate>
									<asp:TextBox ID="uiDiscount" runat="server" Text="0.0" Width="50px" />
									<asp:RangeValidator ID="uiDiscountRangeValidator" Display="Dynamic" runat="server"
										ErrorMessage="Must be between 0 and 100" ControlToValidate="uiDiscount" MinimumValue="0.0"
										MaximumValue="100.0" Type="Double" ValidationGroup="InsertionOrderItemValidationGroup"></asp:RangeValidator>
									<asp:RequiredFieldValidator ID="uiDiscountRequiredFieldValidator" runat="server"
										ControlToValidate="uiDiscount" Display="Dynamic" ErrorMessage="Required" ValidationGroup="InsertionOrderItemValidationGroup"></asp:RequiredFieldValidator>
								</FooterTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Discounted cost" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<asp:Label ID="uiDiscountedCostLabel" runat="server" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).PriceBeforeAgencyDiscount.ToString("C") %>' /></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Agency discount" Visible="false" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<nobr><asp:Label ID="uiItemAgencyDiscount" runat="server" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).AgencyDiscount.ToString("P") %>' /></nobr></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Net cost" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Right">
								<ItemTemplate>
									<asp:Label ID="uiNetCost" runat="server" Text='<%# Container.DataItem == null ? "" : ((Bobs.InsertionOrderItem)(Container.DataItem)).Price.ToString("C") %>' /></ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField ShowHeader="False">
								<ItemTemplate>
								</ItemTemplate>
								<FooterTemplate>
									<asp:LinkButton ID="uiAddLinkButton" runat="server" CommandName="Add" OnClick="uiAddLinkButton_Click"
										CausesValidation="true" ValidationGroup="InsertionOrderItemValidationGroup">
										<asp:Image ID="Image3" runat="server" ImageUrl="~/Gfx/icon-add.png" Width="26" Height="21" AlternateText="Add" /></asp:LinkButton>
								</FooterTemplate>
							</asp:TemplateField>
		
						</Columns>
					</asp:GridView>
				</td>
			</tr>
		</table>
		<table>
			<tr>
				<td>
					<asp:Button ID="uiSaveButton" runat="server" Text="Save" Width="100px" CausesValidation="False"
						OnClick="uiSaveButton_Click" />
				</td>
				<td>
				</td>
				<td style="width: 69px">
					<asp:Button ID="uiCancelButton" runat="server" CausesValidation="False" OnClick="uiCancelButton_Click"
						Text="Cancel" Width="100px" /></td>
				<td>
				</td>
				<td>
					<asp:Button ID="uiRaiseButton" runat="server" CausesValidation="False" OnClick="uiRaiseButton_Click"
						Text="Raise" Width="100px" />
				</td>
			</tr>
		</table>
	</asp:Panel>
</div>

<script>

	function $(id){
		return document.getElementById(id);
	}
	var uiCpm = $('<%= uiInsertionOrderItemGridView.FooterRow.FindControl("uiCpm").ClientID %>');
	var uiGrossCost = $('<%= uiInsertionOrderItemGridView.FooterRow.FindControl("uiGrossCost").ClientID %>');
	var uiImpressionQuantity = $('<%= uiInsertionOrderItemGridView.FooterRow.FindControl("uiImpressionQuantity").ClientID %>');
	var uiBannerPosition = $('<%= uiInsertionOrderItemGridView.FooterRow.FindControl("uiBannerPosition").ClientID %>');
	
	function uiBannerPosition_onchange(){
		setInsertionOrderItemFieldVisibility();
	}
	
	function setInsertionOrderItemFieldVisibility(){
		uiCpm.style.visibility = (uiBannerPosition.value == 0) ? 'hidden' : '';
		uiImpressionQuantity.style.visibility = (uiBannerPosition.value == 0) ? 'hidden' : '';
		uiImpressionQuantity.style.visibility = (uiBannerPosition.value == 0) ? 'hidden' : '';
		uiGrossCost.disabled = (uiBannerPosition.value != 0);
		if (uiBannerPosition.value > 0) {recalculateGrossCost()};
	}

	function recalculateGrossCost(sender){
		if (uiCpm.value == "" || uiImpressionQuantity.value == ""){
			uiGrossCost.value = "";
		}else{
			uiGrossCost.value = uiCpm.value * uiImpressionQuantity.value / 1000;
		}
	}
	function setUiCampaignCreditsOverrideDisplay(){
		$('<%= uiCampaignCreditsOverride.ClientID %>').style.display = $('<%= uiCampaignCreditsOverrideCheckBox.ClientID %>').checked ? '' : 'none';
	}
	setInsertionOrderItemFieldVisibility();
	setUiCampaignCreditsOverrideDisplay();
</script>
