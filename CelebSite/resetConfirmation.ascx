<%@ Control Language="C#" AutoEventWireup="true" CodeFile="resetConfirmation.ascx.cs" Inherits="resetConfirmation" Visible="false"%>                                
                                   
<table style="width: 100%;">
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="Label" ForeColor="White" oncopy="return false;" ></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:TextBox ID="tbxPin" runat="server"></asp:TextBox>
            <asp:Button ID="btnOk" runat="server" Text="Confirm" BackColor="Red" OnClick="btnOk_Click" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        </td>
    </tr>
</table>
                               