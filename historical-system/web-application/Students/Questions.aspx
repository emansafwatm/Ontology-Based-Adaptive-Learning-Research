<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Questions.aspx.cs" Inherits="Students_Questions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Welcome
    </h2>
    <asp:Panel ID="Panel1" runat="server" Height="96px" Width="867px">
        Are You ready To start The Exam<br />
        <br />
        <asp:Button ID="StartCmd" runat="server" Text="Start" 
            onclick="StartCmd_Click" />
        <br />
       <!-- <a href="../Students/QustionContent.aspx">Start</a><p class="submitButton">-->
                    
        

    </asp:Panel>
</asp:Content>

