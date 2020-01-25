<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" Inherits="CelebsSite._Default" EnableEventValidation="false" Codebehind="Default.aspx.cs" %>
<%@ Import Namespace="CelebsAPI.Models" %>
<%@ Register TagPrefix="uc" TagName="CelebSheet" Src="~/CelebSheet.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    hello
    <asp:ListView ID="CelebsList" runat="server">
        <ItemTemplate>
            <uc:CelebSheet Celeb="<%# (Celebrity)Container.DataItem %>" runat="server" />
        </ItemTemplate>
    </asp:ListView>
</asp:Content>  