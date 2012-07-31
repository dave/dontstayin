<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddOnlyTextBox.ascx.cs" Inherits="Spotted.Controls.AddOnlyTextBox" %>
<asp:TextBox ID="readOnlyTextBox" runat="server" ReadOnly="True" TextMode="MultiLine"></asp:TextBox><br />
<asp:TextBox ID="addTextBox" runat="server"></asp:TextBox><asp:Button ID="AddButton" runat="server" Text="Add" CausesValidation="False" OnClick="AddButton_Click" />
