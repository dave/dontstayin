<%@ Control Language="C#" AutoEventWireup="true" Codebehind="BannerTargetting.ascx.cs"
	Inherits="Spotted.Pages.Promoters.BannerTargetting" %>
<dsi:h1 runat="server" ID="BannerListHeader2">Misc options</dsi:h1>
<div class="ContentBorder">
	<p>
		Period to wait before rotation: 
		<asp:RadioButton Checked="true" id="uiUseDefaultBannerRotationRadio" runat="server" GroupName="BannerRotation" /> Default value (<%= Common.Settings.DefaultBannerDurationInSeconds.ToString() %>s)
		<asp:RadioButton id="uiUseCustomBannerRotationRadio" runat="server" GroupName="BannerRotation" /> Custom value
		
		<asp:TextBox ID="uiCustomRotationValue" runat="server"></asp:TextBox>
		<asp:RangeValidator ID="uiCustomRotationValidator" MinimumValue="5" MaximumValue="9000" ErrorMessage="Value must be between 5 and 9000" ControlToValidate="uiCustomRotation" ></asp:RangeValidator>
	</p>
</div>

<dsi:h1 runat="server" ID="BannerListHeader1">Advanced Banner Targetting Options</dsi:h1>
<div class="ContentBorder">
	<p>Tick all those which apply to the intended targets</p>
	<asp:Panel ID="pnlTargettingProperties" runat="server">
		<table border="0" cellpadding="3" cellspacing="0">
			<thead style="font-weight: bold">
				<tr>
					<td>
						Description</td>
					<td style="width: 442px">
						Options</td>
				</tr>
			</thead>
			<tr class="dataGridItem">
				<td>
					Gender
				</td>
				<td style="width: 442px">
					<asp:CheckBoxList ID="cblGender" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="1">Unknown</asp:ListItem>
						<asp:ListItem Value="2">Male</asp:ListItem>
						<asp:ListItem Value="3">Female</asp:ListItem>
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr class="dataGridAltItem">
				<td>
					Age Range
				</td>
				<td style="width: 442px">
					<asp:CheckBoxList ID="cblAgeRange" runat="server" RepeatColumns="5" RepeatDirection="Horizontal">
						<asp:ListItem Value="55"><nobr>Unknown</nobr></asp:ListItem>
						<asp:ListItem Value="56"><nobr>Under 18</nobr></asp:ListItem>
						<asp:ListItem Value="57"><nobr>18 - 24</nobr></asp:ListItem>
						<asp:ListItem Value="58"><nobr>25 - 29</nobr></asp:ListItem>
						<asp:ListItem Value="59"><nobr>30 - 34</nobr></asp:ListItem>
						<asp:ListItem Value="60"><nobr>35 - 39</nobr></asp:ListItem>
						<asp:ListItem Value="61"><nobr>40 - 44</nobr></asp:ListItem>
						<asp:ListItem Value="62"><nobr>45 - 49</nobr></asp:ListItem>
						<asp:ListItem Value="63"><nobr>50 +</nobr></asp:ListItem>
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr class="dataGridItem">
				<td>
					Is a Promoter
				</td>
				<td style="width: 442px">
					<asp:CheckBoxList ID="cblPromoter" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="5">Yes</asp:ListItem>
						<asp:ListItem Value="4">No</asp:ListItem>
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr class="dataGridAltItem">
				<td>
					Employment status
				</td>
				<td style="width: 442px">
					<asp:CheckBoxList ID="cblEmploymentStatus" runat="server" RepeatDirection="Horizontal">
						<asp:ListItem Value="14">Unknown</asp:ListItem>
						<asp:ListItem Value="15">Full-time</asp:ListItem>
						<asp:ListItem Value="16">Part-time</asp:ListItem>
						<asp:ListItem Value="17">Currently unemployed</asp:ListItem>
						<asp:ListItem Value="18">Student</asp:ListItem>
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr class="dataGridItem">
				<td>
					Salary
				</td>
				<td style="width: 442px">
					<asp:CheckBoxList ID="cblSalary" runat="server" RepeatColumns="4" RepeatDirection="Horizontal">
						<asp:ListItem Value="6">Unknown</asp:ListItem>
						<asp:ListItem Value="7">Under &#163;15K</asp:ListItem>
						<asp:ListItem Value="8">&#163;15 - 19K</asp:ListItem>
						<asp:ListItem Value="9">&#163;20 - 24K</asp:ListItem>
						<asp:ListItem Value="10">&#163;25 - 29K</asp:ListItem>
						<asp:ListItem Value="11">&#163;30 - 39K</asp:ListItem>
						<asp:ListItem Value="12">&#163;40 - 49K</asp:ListItem>
						<asp:ListItem Value="13">&#163;50K and over</asp:ListItem>
					</asp:CheckBoxList>
				</td>
			</tr>
			<tr class="dataGridAltItem">
				<td>
					Finance
				</td>
				<td>
					<table>
						<td>
							Credit card
						</td>
						<td style="width: 442px">
							<asp:CheckBoxList ID="cblCreditCard" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="46">Unknown</asp:ListItem>
								<asp:ListItem Value="48">Yes</asp:ListItem>
								<asp:ListItem Value="47">No</asp:ListItem>
							</asp:CheckBoxList>
						</td>
						<tr>
							<td>
								Personal loan
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblLoan" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="49">Unknown</asp:ListItem>
									<asp:ListItem Value="51">Yes</asp:ListItem>
									<asp:ListItem Value="50">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Mortgage
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblMortgage" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="52">Unknown</asp:ListItem>
									<asp:ListItem Value="54">Yes</asp:ListItem>
									<asp:ListItem Value="53">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr class="dataGridItem">
				<td>
					Drinks
				</td>
				<td>
					<table>
						<td>
							Water
						</td>
						<td style="width: 442px">
							<asp:CheckBoxList ID="cblDrinkWater" runat="server" RepeatDirection="Horizontal">
								<asp:ListItem Value="19">Unknown</asp:ListItem>
								<asp:ListItem Value="21">Yes</asp:ListItem>
								<asp:ListItem Value="20">No</asp:ListItem>
							</asp:CheckBoxList>
						</td>
						<tr>
							<td>
								Soft Drinks
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkSoft" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="22">Unknown</asp:ListItem>
									<asp:ListItem Value="24">Yes</asp:ListItem>
									<asp:ListItem Value="23">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Energy Drinks
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkEnergy" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="25">Unknown</asp:ListItem>
									<asp:ListItem Value="27">Yes</asp:ListItem>
									<asp:ListItem Value="26">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Draft Beer
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkDraftBeer" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="28">Unknown</asp:ListItem>
									<asp:ListItem Value="30">Yes</asp:ListItem>
									<asp:ListItem Value="29">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Bottled Beer
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkBottledBeer" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="31">Unknown</asp:ListItem>
									<asp:ListItem Value="33">Yes</asp:ListItem>
									<asp:ListItem Value="32">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Spirits
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkSpirits" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="34">Unknown</asp:ListItem>
									<asp:ListItem Value="36">Yes</asp:ListItem>
									<asp:ListItem Value="35">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Wine
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkWine" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="37">Unknown</asp:ListItem>
									<asp:ListItem Value="39">Yes</asp:ListItem>
									<asp:ListItem Value="38">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Alcopops
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkAlcopops" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="40">Unknown</asp:ListItem>
									<asp:ListItem Value="42">Yes</asp:ListItem>
									<asp:ListItem Value="41">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>
								Cider
							</td>
							<td style="width: 442px">
								<asp:CheckBoxList ID="cblDrinkCider" runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="43">Unknown</asp:ListItem>
									<asp:ListItem Value="45">Yes</asp:ListItem>
									<asp:ListItem Value="44">No</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
					</table>
				</td>
			</tr>
			<tr class="dataGridAltItem">
				<td>
					Spend on<br />music
				</td>
				<td>
					<table>
						<tr>
							<td>CDs</td>
							<td>
								<asp:CheckBoxList runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="64">Unknown</asp:ListItem>
									<asp:ListItem Value="65">&#163;0</asp:ListItem>
									<asp:ListItem Value="66">More than &#163;0</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>Vinyl</td>
							<td>
								<asp:CheckBoxList runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="67">Unknown</asp:ListItem>
									<asp:ListItem Value="68">&#163;0</asp:ListItem>
									<asp:ListItem Value="69">More than &#163;0</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
						<tr>
							<td>Download</td>
							<td>
								<asp:CheckBoxList runat="server" RepeatDirection="Horizontal">
									<asp:ListItem Value="70">Unknown</asp:ListItem>
									<asp:ListItem Value="71">&#163;0</asp:ListItem>
									<asp:ListItem Value="72">More than &#163;0</asp:ListItem>
								</asp:CheckBoxList>
							</td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
	</asp:Panel>
	<br />
	<p><strong>Frequency capping</strong></p>
	<p>Max number of times a banner can be seen by a user in one day:</p>
	<asp:TextBox ID="txtFrequencyCap" runat="server" Columns="4" MaxLength="4"></asp:TextBox>
	<p>(leave blank for uncapped)</p>
	<p><strong>Priority</strong></p>
	<p>0 (normal) - 10</p>
	<asp:TextBox ID="txtPriority" runat="server" Columns="4" MaxLength="4"></asp:TextBox>
	<p><strong>Always show this banner if possible?</strong></p>
	<asp:CheckBox ID="cbAlwaysShow" runat="server"></asp:CheckBox><br />
	<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save changes" />
	<button onclick="window.location='<%= CurrentPromoter.UrlApp("banneroptions", "mode", "edit", "bannerk", BannerK.ToString()) %>';">Cancel changes</button>
</div>
