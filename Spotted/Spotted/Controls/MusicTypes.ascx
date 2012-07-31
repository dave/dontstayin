<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicTypes.ascx.cs" Inherits="Spotted.Controls.MusicTypes" %>

<asp:PlaceHolder Runat="server" ID="Tree"></asp:PlaceHolder>

<script language="javascript">
	function showHide(parent,i)
	{
		var cb = document.getElementById(parent+"_ItemCb"+i);
		var children = document.getElementById(parent+"_Children"+i);
		var lab = document.getElementById(parent+"_LabCb"+i);
		if (!cb.checked)
		{
			children.style.display="";
			lab.style.display="none";
		}
		else
		{
			children.style.display="none";
			lab.style.display="";
		}
		
	}
</script>
