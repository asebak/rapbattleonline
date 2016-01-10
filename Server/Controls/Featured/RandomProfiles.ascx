<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RandomProfiles.ascx.cs" Inherits="FreestyleOnline.Controls.Featured.RandomProfiles" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<ul id="rndprofiles" class="jcarousel-skin-tango">
    <asp:Repeater ID="randomProfiles" runat="server">
        <ItemTemplate>
            <li>
                <RAP:ProfileLink 
                    runat="server" UserId='<%#(int) DataBinder.Eval(Container.DataItem, "UserId") %>' />
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>