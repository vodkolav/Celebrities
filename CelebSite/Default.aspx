<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false"  %>
<%@ Register TagPrefix="uc" TagName="CelebSheet" Src="~/CelebSheet.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="CelebsList" runat="server" OnItemDataBound="CelebsList_ItemDataBound" >
    <ItemTemplate>
        <uc:CelebSheet Celeb ="<%# Container.DataItem %>" runat="server" ite ="ListView_DataBinding" />
    </ItemTemplate>
</asp:ListView> 
</asp:Content>
