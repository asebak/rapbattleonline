<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pager.ascx.cs" Inherits="FreestyleOnline.Controls.Generic.Pager" %>
<div id="PagingPlaceHolder" runat="server">
    <table>
        <tr>
            <td class="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">
                <asp:PlaceHolder ID="PagingLocation" runat="server" />
            </td>
        </tr>
    </table>
</div>