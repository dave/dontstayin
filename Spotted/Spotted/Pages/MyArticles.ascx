<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyArticles.ascx.cs" Inherits="Spotted.Pages.MyArticles" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo"  %>
<%@ Register TagPrefix="Controls" TagName="Pic" Src="/Controls/Pic.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Cropper" Src="/Controls/Cropper.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Video" Src="/Controls/Video.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Picker" Src="/Controls/Picker.ascx" %>
<asp:Panel Runat="server" ID="CantEditPanel">
	<dsi:h1 runat="server" ID="H14">You can't edit this article</dsi:h1>
	<div class="ContentBorder">
		<p>
			You can only edit articles when their status is set to <b>New</b>. 
			<a href="/pages/myarticles/mode-list">Back to the My articles page</a>.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="CurrentArticlesPanel">
	<dsi:h1 runat="server">Articles</dsi:h1>
	<div class="ContentBorder">
		<asp:Panel Runat="server" ID="NoArticlesDataGridPanel">
			<p>
				You haven't added any articles yet...
			</p>
			<p align="center" style="font-size:small;">
				<b><a href="/pages/myarticles/mode-add">Add a new article here</a></b>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="ArticlesDataGridPanel">
			<h2>Adding a new article</h2>
			<p>
				<a href="/pages/myarticles/mode-add">Click here to add a new article</a>
			</p>
			<h2>Your current articles</h2>
			<p>
				You have added the articles below. Click <b>Edit</b> to change the article, 
				or <b>Preview</b> to see what it'll look like when published.
			</p>
			<p>
				<asp:DataGrid Runat="server" ID="ArticlesDataGrid" 
					GridLines="None" AutoGenerateColumns="False"
					BorderWidth=0 CellPadding=3 CssClass=dataGrid 
					AlternatingItemStyle-CssClass="dataGridAltItem"
					HeaderStyle-CssClass="dataGridHeader" SelectedItemStyle-CssClass="dataGridSelectedItem" 
					ItemStyle-VerticalAlign="Top" AllowPaging="True" OnPageIndexChanged="ArticlesDataGridChangePage"
					PageSize="20" PagerStyle-Mode="NumericPages">
					<Columns>
						<asp:TemplateColumn HeaderText="Key">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).K%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Title">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).Title%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Status">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).Status%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Owner">
							<ItemTemplate>
								<%#((Bobs.Article)(Container.DataItem)).Owner.Link()%>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Options">
							<ItemTemplate>
								<nobr>
									<a href="/pages/myarticles/mode-edit/k-<%#((Bobs.Article)(Container.DataItem)).K%>">Edit</a> | 
									<a href="<%#((Bobs.Article)(Container.DataItem)).Url()%>">Preview</a>
								</nobr>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</p>
			<p>
				Status level key: <br>
				New <small>- it's still in progress. Only the author may preview or edit it.</small><br>
				Edit <small>- it's at the editorial stage. Only article moderators can make changes.</small><br>
				Enabled <small>- it's already on the site. Everyone can view it, and only admins may edit it.</small>
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="AddArticlePanel">
	<dsi:h1 runat="server" ID="H11" NAME="H11">Add an article</dsi:h1>
	<div class="ContentBorder">
	
		<asp:Panel Runat="server" ID="AddArticleSubjectMatterPanel">
			<a name="AddArticleScope" />
			<h2>
				Subject matter
			</h2>
			<p>
				We need to know where to show your article on the site. Your article will get more 
				exposure if you put it in the right place!
			</p>
			<p>
				<asp:RadioButton Runat="server" ID="AddArticleScopeEvent"   OnCheckedChanged="AddArticleScopeCheckChange" AutoPostBack="true" GroupName="AddArticleScope" Text="An event <b>(goes live immediately)</b><br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a review of a club night or an interview with a DJ or event promoter</small>" TabIndex="122"/><br>
				<asp:RadioButton Runat="server" ID="AddArticleScopeVenue"   OnCheckedChanged="AddArticleScopeCheckChange" AutoPostBack="true" GroupName="AddArticleScope" Text="A venue <b>(goes live immediately)</b><br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a review of a club or an interview with the management of a club</small>" TabIndex="125"/><br>
				<asp:RadioButton Runat="server" ID="AddArticleScopePlace"   OnCheckedChanged="AddArticleScopeCheckChange" AutoPostBack="true" GroupName="AddArticleScope" Text="A town or place (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a rant about how great (or crap) clubbing is in your town</small>" TabIndex="126"/><br>
				<asp:RadioButton Runat="server" ID="AddArticleScopeCountry" OnCheckedChanged="AddArticleScopeCheckChange" AutoPostBack="true" GroupName="AddArticleScope" Text="A country (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. something that's UK wide, or a review of your favourite clubbing holiday destination</small>" TabIndex="127"/><br>
				<asp:RadioButton Runat="server" ID="AddArticleScopeGeneral" OnCheckedChanged="AddArticleScopeCheckChange" AutoPostBack="true" GroupName="AddArticleScope" Text="General (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. an interview with an international DJ, or a completely random rant</small>" TabIndex="128"/>
			</p>
			<asp:Panel runat="server" ID="AddArticleScopeMultiFinderPanel" Visible="false">
				<p>
					<dsi:Picker runat="server" id="AddArticleScopeMultiPicker" />
				</p>
				<asp:RequiredFieldValidator Runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="<p>Please select a subject matter above</p>" ControlToValidate="AddArticleScopeMultiPicker" ID="Customvalidator13" NAME="Customvalidator1" />
			</asp:Panel>

		</asp:Panel>
	
		<h2>Article title</h2>
		<p>
			<asp:TextBox Runat="server" ID="AddArticleTitleTextBox" MaxLength="100" Width="594px" Columns="80"></asp:TextBox>
		</p>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ErrorMessage="<p>Please enter a title</p>" ControlToValidate="AddArticleTitleTextBox"/>
		<asp:RegularExpressionValidator Runat="server" Display="Dynamic" ValidationExpression="^(.|\n){5,100}$" 
			ErrorMessage="<p>Please enter between 5 and 100 characters here</p>" ControlToValidate="AddArticleTitleTextBox" />
		<asp:CustomValidator Visible="false" runat="server" Display="Dynamic" ControlToValidate="AddArticleTitleTextBox" OnServerValidate="AddArticleTitleCapsVal"
			ErrorMessage="<p>You've used lots of capitals. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator8" runat="server" Display="Dynamic" ControlToValidate="AddArticleTitleTextBox" OnServerValidate="AddArticleTitlePunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation. Please don't decorate your text with punctuation characters.</p>" />
		<p>
			<small>A short title - 100 characters or less</small>
		</p>
		
		<h2>Summary</h2>
		<p>
			<asp:TextBox Runat="server" ID="AddArticleSummaryTextBox" TextMode="MultiLine" Width="594px" Rows="5"/>
		</p>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ErrorMessage="<p>Please enter a summary</p>" ControlToValidate="AddArticleSummaryTextBox"/>
		<asp:RegularExpressionValidator Runat="server" Display="Dynamic" ValidationExpression="^(.|\n){10,500}$" 
			ErrorMessage="<p>Please enter between 10 and 500 characters here</p>" ControlToValidate="AddArticleSummaryTextBox" />
		<asp:CustomValidator Visible="false" ID="CustomValidator3" runat="server" Display="Dynamic" ControlToValidate="AddArticleSummaryTextBox" OnServerValidate="AddArticleSummaryCapsVal"
			ErrorMessage="<p>You've used lots of capitals. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator4" runat="server" Display="Dynamic" ControlToValidate="AddArticleSummaryTextBox" OnServerValidate="AddArticleSummaryPunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation. Please don't decorate your text with punctuation characters.</p>" />
		<p>
			<small>A short paragraph that summarises your article - no html tags</small>
		</p>
		
		<h2>Article text</h2>
		<p>
			<dsi:Html runat="server" id="AddArticleBodyHtml" DisableSaveButton="true" DisableContainer="true" PreviewType="Article" TabIndexBase="102" />
		</p>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" 
			ErrorMessage="<p>Please enter the text of your article</p>" ControlToValidate="AddArticleTitleTextBox" />
		<p>
			<small>Please leave blank lines between paragraphs</small>
		</p>
		
		

	</div>
	
	
	
	
	<dsi:h1 runat="server" ID="H13" NAME="H11">Done?</dsi:h1>
	<div class="ContentBorder">
		<p>
			When you've finished, click the Done button below. Click Cancel to return to the article list.
		</p>
		<p>
			<asp:Button ID="Button1" Runat="server" onclick="AddButtonCancel" Text="&lt;- Cancel" CausesValidation="False" TabIndex="130"/> <asp:Button Runat="server" onclick="AddButtonDone" Text="Done -&gt;" ID="Button2" TabIndex="131"/> 
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="EditArticleIndexPanel">
	<dsi:h1 runat="server" ID="H18" NAME="H11">Edit article - <asp:Label Runat="server" ID="EditArticleIndexPanelArticleNameLabel"></asp:Label></dsi:h1>
	<div class="ContentBorder">
		
		<p>
			<small><a runat="server" id="EditArticleIndexPanelPreviewAnchor" target="_blank">Preview this article</a> or <a href="/pages/myarticles/mode-list">back to the articles list</a>.</small>
		</p>
		
		<asp:Panel Runat="server" ID="EditArticleIndexPublishPanel">
			<h2>Finished?</h2>
			<p>
				When you've finished this article, and it's all done, click <b>Publish</b>.
			</p>
			<p>
				If you've selected an Event or a Venue on the "Link" tab, your article will go live straight 
				away. Be carefull - you can't make changes after you publish your article! It's OK to include 
				promotional material in this type of article.
			</p>
			<p>
				If you've selected Place, Country or General on the "Link" tab, your article will be 
				suggested to our Mixmag editorial team. It will have to be good - 90% of these articles 
				don't make the grade. We advise against including too much promotional material!
			</p>
			<p>
				<asp:Button ID="Button3" Runat="server" OnClick="EditArticleIndexPublishClick" Text="Publish"></asp:Button>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="EditArticleIndexToDoPanel">
			<h2>Not quite finished yet...</h2>
			<p>
				To publish your article to the site, you've first got to add an icon. 
				To do this, click the <b>Icon</b> tab below, and follow the instructions.
			</p>
		</asp:Panel>
		
	</div>
	
	<h1 class="TabHolder">
		<a href="/" class="TabbedHeading" runat="server" id="EditArticleContentTab">Content</a>
		<a href="/" class="TabbedHeading" runat="server" id="EditArticleTitleTab">Title</a>
		<a href="/" class="TabbedHeading" runat="server" id="EditArticleLinkTab">Link</a>
		<a href="/" class="TabbedHeading" runat="server" id="EditArticleIconTab">Icon</a>
		<a href="/" class="TabbedHeading" runat="server" id="EditArticlePhotosTab">Photos</a>
		<a href="/" class="TabbedHeading" runat="server" id="EditArticleAdminTab">Admin</a>
	</h1>
	
</asp:Panel>


<asp:Panel Runat="server" ID="EditArticleTitleSummaryPanel">
	<div class="ContentBorder">
		<h2>Article title</h2>
		<p>
			<asp:TextBox Runat="server" ID="EditArticleTitleTextBox" MaxLength="100" Columns="80"></asp:TextBox>
		</p>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ErrorMessage="<p>Please enter a title</p>" ControlToValidate="EditArticleTitleTextBox" ID="Requiredfieldvalidator4" NAME="Requiredfieldvalidator3"/>
		<asp:RegularExpressionValidator Runat="server" Display="Dynamic" ValidationExpression="^(.|\n){5,100}$" 
			ErrorMessage="<p>Please enter between 5 and 100 characters here</p>" ControlToValidate="EditArticleTitleTextBox" ID="Regularexpressionvalidator3" NAME="Regularexpressionvalidator2"/>
		<asp:CustomValidator Visible="false" ID="CustomValidator9" runat="server" Display="Dynamic" ControlToValidate="EditArticleTitleTextBox" OnServerValidate="EditArticleTitleCapsVal"
			ErrorMessage="<p>You've used lots of capitals. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator10" runat="server" Display="Dynamic" ControlToValidate="EditArticleTitleTextBox" OnServerValidate="EditArticleTitlePunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation. Please don't decorate your text with punctuation characters.</p>" />
		<p>
			<small>A short title - 100 characters or less</small>
		</p>
		
		<h2>Summary</h2>
		<p>
			<asp:TextBox Runat="server" ID="EditArticleSummaryTextBox" TextMode="MultiLine" Columns="80" Rows="5"/>
		</p>
		<asp:RequiredFieldValidator Runat="server" Display="Dynamic" ErrorMessage="<p>Please enter a summary</p>" ControlToValidate="EditArticleSummaryTextBox" ID="Requiredfieldvalidator5" NAME="Requiredfieldvalidator1"/>
		<asp:RegularExpressionValidator Runat="server" Display="Dynamic" ValidationExpression="^(.|\n){10,500}$" 
			ErrorMessage="<p>Please enter between 10 and 500 characters here</p>" ControlToValidate="EditArticleSummaryTextBox" 
			ID="Regularexpressionvalidator4" NAME="Regularexpressionvalidator1"/>
		<asp:CustomValidator Visible="false" ID="CustomValidator11" runat="server" Display="Dynamic" ControlToValidate="EditArticleSummaryTextBox" OnServerValidate="EditArticleSummaryCapsVal"
			ErrorMessage="<p>You've used lots of capitals. Please use normal sentance capitalisation. We've made everything lower-case to help you.</p>" />
		<asp:CustomValidator Visible="false" ID="CustomValidator12" runat="server" Display="Dynamic" ControlToValidate="EditArticleSummaryTextBox" OnServerValidate="EditArticleSummaryPunctuationVal"
			ErrorMessage="<p>You've used lots of punctuation. Please don't decorate your text with punctuation characters.</p>" />
		<p>
			<small>A short paragraph that summarises your article - no html tags</small>
		</p>
		<h2>Done?</h2>
		<p>
			<asp:Button ID="Button4" Runat="server" Text="Save" OnClick="EditArticleTitleSummaryPanelSave"/>
			<asp:Label Runat="server" EnableViewState="False" ForeColor="Blue" id="EditArticleTitleSummaryPanelSavedLabel" Visible="False">Saved</asp:Label>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticleSubjectMatterPanel">
	<div class="ContentBorder">
		<a name="EditArticleScope" />
		<p>
			We need to know where to show your article on the site. Your article will get more 
			exposure if you put it in the right place!
		</p>
		<p>
			<asp:RadioButton Runat="server" ID="EditArticleScopeEvent"   OnCheckedChanged="EditArticleScopeCheckChange" AutoPostBack="true" GroupName="EditArticleScope" Text="An event <b>(goes live immediately)</b><br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a review of a club night or an interview with a DJ or event promoter</small>" TabIndex="122"/><br>
			<asp:RadioButton Runat="server" ID="EditArticleScopeVenue"   OnCheckedChanged="EditArticleScopeCheckChange" AutoPostBack="true" GroupName="EditArticleScope" Text="A venue <b>(goes live immediately)</b><br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a review of a club or an interview with the management of a club</small>" TabIndex="125"/><br>
			<asp:RadioButton Runat="server" ID="EditArticleScopePlace"   OnCheckedChanged="EditArticleScopeCheckChange" AutoPostBack="true" GroupName="EditArticleScope" Text="A town or place (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. a rant about how great (or crap) clubbing is in your town</small>" TabIndex="126"/><br>
			<asp:RadioButton Runat="server" ID="EditArticleScopeCountry" OnCheckedChanged="EditArticleScopeCheckChange" AutoPostBack="true" GroupName="EditArticleScope" Text="A country (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. something that's UK wide, or a review of your favourite clubbing holiday destination</small>" TabIndex="127"/><br>
			<asp:RadioButton Runat="server" ID="EditArticleScopeGeneral" OnCheckedChanged="EditArticleScopeCheckChange" AutoPostBack="true" GroupName="EditArticleScope" Text="General (suggested to our editors)<br><small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;e.g. an interview with an international DJ, or a completely random rant</small>" TabIndex="128"/>
		</p>
		<asp:Panel runat="server" ID="EditArticleScopeMultiFinderPanel" Visible="false">
			<p>
				<dsi:Picker runat="server" id="EditArticleScopeMultiPicker" />
			</p>
			<asp:RequiredFieldValidator Runat="server" Display="Dynamic" EnableClientScript="False" ErrorMessage="<p>Please select a subject matter above</p>" ControlToValidate="EditArticleScopeMultiPicker" ID="RequiredFieldValidator1" NAME="Customvalidator1" />
		</asp:Panel>
		<h2>Done?</h2>
		<p>
			<asp:Button Runat="server" Text="Save" OnClick="EditArticleSubjectMatterPanelSave" ID="Button10"/>
			<asp:Label Runat="server" EnableViewState="False" ForeColor="Blue" id="EditArticleSubjectMatterPanelSavedLabel" Visible="False">Saved</asp:Label>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticlePicturePanel">
	<div class="ContentBorder">
		<p>
			The icon for your article is displayed at the top of the article and also 
			throughout the site the article is listed.
		</p>
		<Controls:Pic Runat="server" ID="EditArticlePicture" 
			OnActionSaved="EditArticlePictureSave" OnActionNoPic="EditArticlePictureSave"/>
		<asp:Panel Runat="server" ID="EditArticlePicturePanelSavedLabel" Visible="False" EnableViewState="False">
			<p>
				<span style="color:blue;">Saved</span>
			</p>
		</asp:Panel>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticleBodyPanel">
	<div class="ContentBorder">
		<p>
			Below are the paragraphs that make up your article. The first paragraph is displayed on the site in 
			the "News" section. The buttons next to each paragraph are explained below:
		</p>
		<p>
			<b>Edit</b> - 
			<small>
				This lets you edit the text of a paragraph, or change the paragraph 
				type between <i>text</i>, <i>photo</i> and <i>title</i>.
			</small>
		</p>
		<p>
			<b>Picture</b> - 
			<small>
				This lets you assign or upload a picture to the paragraph. This can either be a small
				picture to the side of the text, or a full-sized picture with the text as a caption.
			</small>
		</p>
		<p>
			<b>Up / Down</b> - 
			<small>
				This allows you to move a paragraph around. Up moves the paragraph up, or to the previous page. 
				Down moves the paragraph down, or to the next page. To create a new page, just click the <b>Down</b> 
				button on the last paragraph.
			</small>
		</p>
		<p>
			<b>Delete</b> - 
			<small>
				This allows you to delete a paragraph.
			</small>
		</p>
		<p>
			<b>New</b> - 
			<small>
				This inserts a new paragraph below the <b>New</b> button.
			</small>
		</p>
	</div>
	<asp:Repeater Runat="server" ID="EditArticleBodyPageRepeater"/>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticleParaPanel">
	<dsi:h1 runat="server" ID="H110" NAME="H11">Edit paragraph</dsi:h1>
	<div class="ContentBorder">
		<h2>Paragraph type</h2>
		<p>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaType" ID="EditArticleParaTypeTitle" 
				Text="Title <small>- large sub-title</small>" TabIndex="100"/><br>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaType" ID="EditArticleParaTypePara"
				Text="Normal paragraph <small>- normal body text, optionally with a picture on left or right</small>" TabIndex="101"/><br>
		</p>
		<asp:CustomValidator ID="CustomValidator5" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="EditArticleParaTypeVal"
			ErrorMessage="<p>Please choose a paragraph type."/>
		<h2>Text</h2>
		<asp:CustomValidator ID="CustomValidator6" Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="EditArticleParaTitleVal"
			ErrorMessage="<p>A title should be less than 100 characters. Shorten your text or choose the <b>Normal paragraph</b> type.</p>"/>
		<p>
			<dsi:Html runat="server" id="EditArticleParaHtml" PreviewType="Article" DisableContainer="true" DisableSaveButton="true" TabIndexBase="102" />
		</p>
		<h2>Done?</h2>
		<p>
			<asp:Button ID="Button5" Runat="server" OnClick="EditArticleParaSaveClick" Text="Save" TabIndex="122"/>
			<asp:Button Runat="server" OnClick="EditArticleParaAddPhotoClick" Text="Add a photo" ID="Button6" TabIndex="123"/>
		</p>
		<h2>Messed it up?</h2>
		<p>
			<asp:Button Runat="server" OnClick="EditArticleParaCancelClick" CausesValidation=false Text="Cancel" ID="Button7" TabIndex="124"/>
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticleParaPhotoPanel">
	
	<a name="EditArticleParaPhotoPositionAnchor"></a>
	<dsi:h1 runat="server" ID="H114" NAME="H11">Add a photo to a paragraph</dsi:h1>
	<div class="ContentBorder">
		<h2>Photo position</h2>
		<p>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaPhotoPosition" ID="EditArticleParaPhotoPositionTop" Text="&nbsp;Top <small>(before the text)</small>" Checked="True"/><br>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaPhotoPosition" ID="EditArticleParaPhotoPositionLeft" Text="&nbsp;&lt;- Left <small>(at the side of the text)</small>"/><br>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaPhotoPosition" ID="EditArticleParaPhotoPositionRight" Text="&nbsp;Right -&gt; <small>(at the side of the text)</small>"/><br>
			<asp:RadioButton Runat="server" GroupName="EditArticleParaPhotoPosition" ID="EditArticleParaPhotoPositionBottom" Text="&nbsp;Bottom <small>(after the text)</small>"/><br><br />
			<asp:RadioButton Runat="server" GroupName="EditArticleParaPhotoPosition" ID="EditArticleParaPhotoPositionHidden" Text="&nbsp;Hidden <small>(don't show this photo)</small>"/>
		</p>
		<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="EditArticleParaPhotoPositionVal"
			ErrorMessage="<p>Please choose a position.</p>" ID="Customvalidator7" NAME="Customvalidator5"/>
	</div>
	<dsi:h1 runat="server" ID="H113" NAME="H11">Photo source</dsi:h1>
	<div class="ContentBorder">
		<p>
			Choose from the options below to select the photo to use:
		</p>
		<p style="margin-bottom:0px;">
			<asp:RadioButton Runat="server"
				ID="EditArticleParaPhotoSourceUploadedCheck" 
				GroupName="EditArticleParaPhotoSource" 
				Text="&nbsp;A photo you've uploaded for this article..."/>
		</p>
		<p style="margin-left:24px;margin-top:0px;">
			<small>
				Choose this if you want to use a photo that you've uploaded for this article. If you haven't uploaded any photos yet, <a href="" runat="server" id="EditArticleParaPhotoUploadLink">click here</a>.
			</small>
		</p>
		<div class="ContentBorder" style="margin-left:24px;padding:0px;display:none;" id="EditArticleParaPhotoSourceIFrame" runat="server">
			<iframe src="" width="100%" height="118" frameborder="0" runat="server" id="PhotosIFrame"></iframe>
		</div>
		
		<asp:Panel Runat="server" ID="EditArticleParaPhotoSourceEventPanel">
			<p style="margin-bottom:0px;">
				<asp:RadioButton Runat="server"
					ID="EditArticleParaPhotoSourceEventCheck" 
					GroupName="EditArticleParaPhotoSource" 
					Text="&nbsp;A photo that has been uploaded for this event..."/>
			</p>
			<p style="margin-left:24px;margin-top:0px;">
				<small>
					Choose this if you want to use a photo that someone has uploaded for this event. You can then select from a list of galaries and pick the photo.
				</small>
			</p>
			<p runat="server" style="display:none;margin-left:24px;" id="EditArticleParaPhotoSourceEventGalleriesP">
				Choose the gallery here: <asp:DropDownList Runat="server" ID="EditArticleParaPhotoSourceEventGalleryDropDown"></asp:DropDownList>
			</p>
			<div class="ContentBorder" style="margin-left:24px;padding:0px;display:none;" id="EditArticleParaPhotoSourceEventIFrame" runat="server">
				<iframe src="" width="100%" height="118" frameborder="0" runat="server" id="PhotosEventIFrame"></iframe>
			</div>
		</asp:Panel>
		
		<input type="hidden" runat="server" id="EditArticleParaPhotoHidden"/>
		
		<a name="EditArticleParaPhotoUpdatePreviewsButton"></a>
		<p style="margin-bottom:0px;">
			<asp:RadioButton Runat="server" 
				ID="EditArticleParaPhotoSourceMiscCheck" 
				GroupName="EditArticleParaPhotoSource" 
				Text="&nbsp;A photo that's already on the site..."/>
		</p>
		<p style="margin-left:24px;margin-top:0px;">
			<small>
				Choose this if you want to use a photo that's been uploaded to the site in an event gallery. 
				
			</small>
		</p>
		<div style="margin-left:24px;display:none;" runat="server" id="EditArticleParaPhotoSourceMiscRefP">
			
			
			Photo link: <asp:TextBox Runat="server" ID="EditArticleParaPhotoSourceKTextBox"/> <asp:Button Runat="server" OnClick="EditArticleParaPhotoUpdatePreviews" Text="Update" CausesValidation="False" ID="Button8" NAME="Button8"></asp:Button>
			<small>
				Click <b>Update</b> button to show the photo below.
			</small>
			
			<div style="margin-top:5px;margin-bottom:5px;padding:3px;border:1px solid #ff0000;width:337px;">
				<b>Don't get this from the URL bar of your browser!</b><br />
				Go to the photo page, and click "Link / embed code". Copy and paste the link from the "Link to this photo" box.
			</div>
			
			<span style="color:red;" runat="server" id="EditArticleParaPhotoUpdatePreviewsError" Visible="False">
				<br>Error! Photo not found. Please try again.
			</span>
			
		</div>
		
		<script>
			function UpdateSelectedPhoto(k)
			{
				document.forms[0]["<%= EditArticleParaPhotoHidden.ClientID %>"].value = k;
				__doPostBack('Content$EditArticleParaPhotoShowPhotoButton',k);
			}
		</script>
		<asp:LinkButton Runat="server" CommandName="ShowPhoto" CommandArgument="4444" OnCommand="EditArticleParaPhotoShowCommand" ID="EditArticleParaPhotoShowPhotoButton" Visible="False">Show</asp:LinkButton>
		
	</div>
	<asp:Panel runat="server" id="EditArticleParaPhotoCropperPanel" Visible="False">
		<dsi:h1 runat="server" ID="H115" NAME="H11">Photo size</dsi:h1>
		<div class="ContentBorder">
			<p>
				Use the RED triangle to resize your final image. Use the BLUE square to zoom the contents. Drag the image around to 
				get it in the right place.
			</p>
			<p runat="server" id="EditArticleParaPhotoSaveNoResizeP">
				If you'd prefer to just use the image as it was uploaded (and not zoom / crop it), <asp:LinkButton ID="LinkButton1" Runat="server" OnClick="EditArticleParaPhotoSaveNoResize">click here</asp:LinkButton>.
			</p>
			<p>
				<Controls:Cropper Runat="server" ID="EditArticleParaPhotoCropper" ShowTextHelpers="true" ControlHeight="560"
					MaxHeight=450 MaxWidth=565 AllowCustomWidth=true AllowCustomHeight=true  />
			</p>
		</div>
	</asp:Panel>
	<asp:Panel runat="server" id="EditArticleParaPhotoVideoPanel" Visible="False">
		<dsi:h1 runat="server" ID="dfgkjgs" NAME="H12">Video preview</dsi:h1>
		<div class="ContentBorder">
			<p align="center">
				<Controls:Video Runat="server" ID="EditArticleParaPhotoVideo" />
			</p>
		</div>
	</asp:Panel>
	<dsi:h1 runat="server" ID="H117" NAME="H11">Done?</dsi:h1>
	<div class="ContentBorder">
		<p>
			<button id="Button9" runat="server" onserverclick="EditArticleParaPhotoCancelClick" CausesValidation="False">&lt;- Back</button>
			<asp:Button Runat="server" OnClick="EditArticleParaPhotoSaveClick" Text="Save -&gt;" ID="Button11"/>
			
		</p>
	</div>
</asp:Panel>

<asp:Panel Runat="server" ID="EditArticlePhotoUploadPanel">
	<div class="ContentBorder">
		<p style="font-size:12px;font-weight:bold;" align="center">
			<a runat="server" id="EditArticlePhotoUploadLink">Upload more photos</a>
		</p>
	</div>
	<asp:Panel Runat="server" ID="EditArticlePhotoNoPhotosPanel">
		<dsi:h1 runat="server" ID="H112" NAME="H11">Current photos</dsi:h1>
		<div class="ContentBorder">
			<p>
				Below are the photos you have uploaded for this article:
			</p>
			
			<p>
				<asp:DataList Runat="server" ID="EditArticlePhotoGalleryDataList" RepeatLayout="Table" Width="100%" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" CellSpacing="8" RepeatColumns="3" RepeatDirection="Horizontal"></asp:DataList>
			</p>
			<p style="font-size:12px;font-weight:bold;" align="center">
				<a runat="server" id="EditArticlePhotoEditLink">Edit these photos</a>
			</p>
		</div>
	</asp:Panel>
</asp:Panel>
<asp:Panel Runat="server" ID="EditArticleAdminPanel">
	<div class="ContentBorder">
		<h2>
			Status
		</h2>
		<p>
			<asp:DropDownList runat="server" ID="EditArticleAdminStatusDrop">
				<asp:ListItem Value="1" Text="New (author may edit)" />
				<asp:ListItem Value="2" Text="Editorial (only admins may edit)" />
				<asp:ListItem Value="3" Text="Enabled" />
				<asp:ListItem Value="4" Text="Disabled" />
			</asp:DropDownList>
		</p>
		<p runat="server" id="EditArticleStatusDisplay" visible="false" />
		
		<h2>
			Display on...
		</h2>
		<asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">
			<ContentTemplate>
				<p>
					<asp:CheckBox runat="server" ID="EditArticleRelevanceGeneral" Text="Front page<br />" AutoPostBack="true" OnCheckedChanged="EditArticleRelevanceChecked" />
					<asp:CheckBox runat="server" ID="EditArticleRelevanceCountry" Text="Country page<br />" AutoPostBack="true" OnCheckedChanged="EditArticleRelevanceChecked" />
					<asp:CheckBox runat="server" ID="EditArticleRelevancePlace" Text="Town page<br />" AutoPostBack="true" OnCheckedChanged="EditArticleRelevanceChecked" />
					<asp:CheckBox runat="server" ID="EditArticleRelevanceVenue" Text="Venue page<br />" AutoPostBack="true" OnCheckedChanged="EditArticleRelevanceChecked" />
					<asp:CheckBox runat="server" ID="EditArticleRelevanceEvent" Text="Event page<br />" AutoPostBack="true" OnCheckedChanged="EditArticleRelevanceChecked" />
				</p>
			</ContentTemplate>
		</asp:UpdatePanel>
		<p>
			<asp:CheckBox runat="server" ID="EditArticleRelevanceFrontPageAboveFold" Text="Above the fold" />
			<small>- these articles will ONLY display above the fold for 48 hours after the enabled date.</small>
		</p>
		
		<h2>
			Blog style display
		</h2>
		<p>
			<asp:CheckBox runat="server" ID="EditArticleExtended" Text="Display as a blog post" />
		</p>
		<p>
			This displays them on the site in the blog-style. This will render the whole of the 
			first paragraph, instead of the summary. <b>Make sure the style of the first paragraph is 
			consistant before ticking this box!</b>
		</p>
		
		<div runat="server" visible="false">
			<h2>
				Add to the Mixmag feed
			</h2>
			<p>
				<asp:CheckBox runat="server" ID="EditArticleMixmag" Text="Add to Mixmag feed" />
			</p>
		
			<h2>
				Mixmag sections
			</h2>
			<p>
				This determines which Mixmag sections the article appears in. You can choose multiple secations with ctrl+click.
			</p>
			<p>
				<asp:ListBox runat="server" ID="MixmagSectionsListBox" Rows="16" SelectionMode="Multiple">
					<asp:ListItem Value="0" Text="0: Front page"></asp:ListItem>
					<asp:ListItem Value="1" Text="1: House"></asp:ListItem>
					<asp:ListItem Value="2" Text="2: ???"></asp:ListItem>
					<asp:ListItem Value="3" Text="3: ???"></asp:ListItem>
					<asp:ListItem Value="4" Text="4: ???"></asp:ListItem>
					<asp:ListItem Value="5" Text="5: ???"></asp:ListItem>
					<asp:ListItem Value="6" Text="6: ???"></asp:ListItem>
					<asp:ListItem Value="7" Text="7: ???"></asp:ListItem>
					<asp:ListItem Value="8" Text="8: ???"></asp:ListItem>
					<asp:ListItem Value="9" Text="9: ???"></asp:ListItem>
					<asp:ListItem Value="10" Text="10: ???"></asp:ListItem>
					<asp:ListItem Value="11" Text="11: ???"></asp:ListItem>
					<asp:ListItem Value="12" Text="12: ???"></asp:ListItem>
					<asp:ListItem Value="13" Text="13: ???"></asp:ListItem>
					<asp:ListItem Value="14" Text="14: ???"></asp:ListItem>
					<asp:ListItem Value="15" Text="15: ???"></asp:ListItem>
				</asp:ListBox>
			</p>
		</div>

		<p>
			<asp:Button ID="Button12" runat="server" OnClick="EditArticleAdminStatusSaveClick" Text="Save" />
		</p>
		
		
	</div>
</asp:Panel>
