<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RandomHoods.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.RandomHoods" %>
<%@ Register TagPrefix="RAP"  TagName="HoodBox" Src="~/Controls/HoodBox.ascx"%>
<ul id="rndhoods" class="jcarousel-skin-tango">
    <asp:Repeater ID="randomHoods" runat="server" OnItemDataBound="randomHoods_ItemDataBound">
        <ItemTemplate>
            <li class="sapUiTf sapUiTfBack sapUiTfBrd sapUiTfStd sapUiTxtA">
                <RAP:HoodBox ID="RandomHoodBox"  runat="server" />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>