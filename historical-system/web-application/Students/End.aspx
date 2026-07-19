<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="End.aspx.cs" Inherits="Students_End" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>
        Your results is :
</h2>
   <asp:Panel ID="Result" runat="server" Height="60px">
        <asp:TextBox ID="TxtQResult" runat="server" BorderStyle="None" Height="25px" 
            Width="841px" ForeColor="#666699" ontextchanged="TxtQResult_TextChanged">Grades</asp:TextBox>
            <br />
            <asp:TextBox ID="TextBox1" runat="server" BorderStyle="None" Height="25px" 
            Width="841px">You Have a chance to improve it</asp:TextBox>
    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server" Height="60px">
        <br />
        &nbsp
        <asp:LinkButton ID="CmdImpove" runat="server" onclick="CmdImprove_Click">Improve</asp:LinkButton>
        &nbsp &nbsp &nbsp &nbsp &nbsp
        <asp:LinkButton ID="CmdCancel" runat="server" onclick="CmdCancel_Click">Cancel</asp:LinkButton>

    </asp:Panel>
</asp:Content>

