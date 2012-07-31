<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemcachedUtils.ascx.cs" Inherits="Spotted.Admin.MemcachedUtils" %>




<h2>
	Set in the cache
</h2>
<p>
	Key: <asp:TextBox runat="server" id="SetKey" Text="test" Columns="50" /> 
</p>
<p>	
	Value: <asp:TextBox runat="server" id="SetValue" Text="test value" Columns="50" /> <asp:Button runat="server" OnClick="Set" Text="Set" />
</p>
<p>
	Response: <asp:TextBox runat="server" id="SetResponse"/>
</p>

<h2>
	Delete from the cache
</h2>
<p>
	Key: <asp:TextBox runat="server" id="DeleteKey" Text="test" Columns="50" /> <asp:Button runat="server" OnClick="Delete" Text="Delete" />
</p>
<p>
	Response: <asp:TextBox runat="server" id="DeleteKeyResponse"/>
</p>

<h2>
	Set counter
</h2>
<p>
	Key: <asp:TextBox runat="server" id="SetCounterKey" Text="" Columns="50" /> <br />
	Value: <asp:TextBox runat="server" id="SetCounterValue" Text="0" Columns="50" /> <br />
	<asp:Button runat="server" OnClick="SetCounter" Text="Set" />
</p>
<p>
	Response: <asp:TextBox runat="server" id="SetCounterKeyResponse"/>
</p>

<h2>
	Get from the cache
</h2>
<p>
	Key: <asp:TextBox runat="server" id="GetKey" Text="test" Columns="50" /> <asp:Button runat="server" OnClick="Get" Text="Get" />
</p>
<p runat="server" id="GetPara">
	
</p>

<h2>
	Get counter from the cache
</h2>
<p>
	Key: <asp:TextBox runat="server" id="GetCounterKey" Text="" Columns="50" /> <asp:Button runat="server" OnClick="GetCounter" Text="Get" />
</p>
<asp:Label ID="GetCounterKeyLabel" runat="server"></asp:Label><asp:Label ID="GetCounterValue" runat="server"></asp:Label>
