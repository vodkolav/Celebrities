<%@ Control Language="C#" AutoEventWireup="True" Codebehind="CelebSheet.ascx.cs"  Inherits="CelebsSite.CelebSheet" %>
<style type="text/css">
    .auto-style2 { width: 308px; }

    .auto-style3 {
        height: 217px;
        width: 74px;
    }

    .auto-style4 {
        height: 115px;
        width: 308px;
    }

    .auto-style5 {
        height: 221px;
        width: 100%;
    }
</style>
<table width="80%">
    <tr>
        <td rowspan="4">
            <asp:Image ID="imgFace" runat="server" Width=" 140" Height="209"/>
        </td>
        <td>
            <h3 class="lister-item-header">
                <%= Celeb.Id %>|<%= Celeb.Name %>
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
            Birthday| <%= Celeb.Birth %>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" autopostback="false"/>
        </td>
    </tr>
</table>