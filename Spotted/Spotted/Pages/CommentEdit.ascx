<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommentEdit.ascx.cs" Inherits="Spotted.Pages.CommentEdit" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<dsi:h1 runat="server">Edit a comment</dsi:h1>
<div class="ContentBorder">
	<asp:Panel Runat="server" ID="SubjectPanel">
		<p>
			Subject:
		</p>
		<p>
			<asp:TextBox Runat="server" ID="ThreadSubjectEditBox" Columns="56" MaxLength="200" style="width:580px; border:1px solid #A58319;" TabIndex="100"></asp:TextBox>
		</p>
	</asp:Panel>
	<p>
		<dsi:Html runat="server" id="CommentEditHtml" PreviewType="Comment" DisableContainer="true" SaveButtonText="Save" OnSave="CommentEditSaveClick" CausesValidation="false" TabIndexBase="101" />
	</p>
</div>
