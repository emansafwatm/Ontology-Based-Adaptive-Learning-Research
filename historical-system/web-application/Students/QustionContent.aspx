<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="QustionContent.aspx.cs" Inherits="Students_QustionContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="QustionBodyPanel" runat="server" Height="60px">
        <asp:TextBox ID="TxtQBody" runat="server" BorderStyle="None" Height="55px" 
            Width="507px">Question Content</asp:TextBox>
        <asp:Label ID="lblSolution" runat="server"></asp:Label>
    </asp:Panel>
    <asp:RadioButtonList ID="ChoicesList" runat="server" Width="805px">
        <asp:ListItem Value="A">First choice</asp:ListItem>
        <asp:ListItem Value="B">Second choice</asp:ListItem>
        <asp:ListItem Value="C">Third Choice</asp:ListItem>
        <asp:ListItem Value="D">Fourth choice</asp:ListItem>
    </asp:RadioButtonList>
    
<asp:Panel ID="Panel1" runat="server">
</asp:Panel>
<asp:LinkButton ID="CmdNext" runat="server" onclick="CmdNext_Click">Next Question</asp:LinkButton>
    
</asp:Content>

