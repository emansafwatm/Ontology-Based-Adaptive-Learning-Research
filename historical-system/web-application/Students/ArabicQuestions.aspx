<%@ Page Title="" Language="C#" MasterPageFile="~/ArabicMaster.master" AutoEventWireup="true" CodeFile="ArabicQuestions.aspx.cs" Inherits="Students_ArabicQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        مرحبا
    </h2>
    <asp:Panel ID="Panel1" runat="server" Height="96px" Width="867px">
        هل انت مستعد للبدأ بالامتحان<br />
        <br />
        <asp:LinkButton ID="CmdStart" runat="server" onclick="LinkButton1_Click"> ابدأ </asp:LinkButton>

</asp:Panel>
</asp:Content>