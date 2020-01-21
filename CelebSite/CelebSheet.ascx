<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CelebSheet.ascx.cs" Inherits="CelebSheet"   %>
<style type="text/css">
    .auto-style2 {
        width: 308px;
    }
    .auto-style3 {
        width: 74px;
        height: 217px;
    }
    .auto-style4 {
        width: 308px;
        height: 115px;
    }
    .auto-style5 {
        width: 100%;
        height: 221px;
    }
</style>
<table width="80%">
    <tr>
        <td rowspan="4">
            <asp:Image ID="imgFace" runat="server" Width=" 140" Height="209" />
        </td>
        <td>
            <h3 class="lister-item-header">
                  <%= Celeb.Id  %>|<%= Celeb.Name %>    
            </h3>
        </td>
    </tr>
    <tr>
        <td>
           Occupation| <%= Celeb.Occupation %>
        </td>
    </tr>
    <tr>
        <td>
           Birthday| <%= Celeb.Birth %></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete"  OnClick="btnDelete_Click" autopostback="false" /></td>
    </tr>
</table>
