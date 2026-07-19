<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Results.aspx.cs" Inherits="Teachers_Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Results
    </h2>
    <p>
        <asp:DropDownList ID="ModulesCmb" runat="server" DataSourceID="ModulesDS" 
            DataTextField="Module_Name" DataValueField="Module_ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="ModulesDS" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SyudentsConnectionString %>" 
            ProviderName="<%$ ConnectionStrings:SyudentsConnectionString.ProviderName %>" 
            SelectCommand="SELECT [Module_ID], [Module_Name] FROM [Modules] WHERE ([Language_ID] = ?)">
            <SelectParameters>
                <asp:Parameter DefaultValue="2" Name="Language_ID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </p>
</asp:Content>

