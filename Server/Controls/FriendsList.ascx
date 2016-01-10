<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FriendsList.ascx.cs" Inherits="FreestyleOnline.Controls.FriendsList" %>
<%@ Register TagPrefix="RAP" TagName="PrivateMsg" Src="~/Controls/Generic/PMWindow.ascx" %>
<ext:Menu ID="OnlineUsersMenu" runat="server">
            <Items>
                <ext:MenuItem ID="EmailMenuItem" runat="server" Text="Send PM" Icon="Email">
                    <DirectEvents>
                        <Click OnEvent="EmailMenuItem_Click">
                              <ExtraParams>
                                 <ext:Parameter Name="userNode" Value="Ext.encode(#{OnlineUsersTree}.getSelectedNodes())" Mode="Raw" />
                             </ExtraParams> 
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
                <ext:MenuItem ID="ProfileMenuItem" runat="server" Text="View Profile'" Icon="Pencil">
                                        <DirectEvents>
                        <Click OnEvent="ProfileMenuItem_Click">
                              <ExtraParams>
                                 <ext:Parameter Name="userNode" Value="Ext.encode(#{OnlineUsersTree}.getSelectedNodes())" Mode="Raw" />
                             </ExtraParams> 
                        </Click>
                    </DirectEvents>
                </ext:MenuItem>
            </Items>
        </ext:Menu>
<RAP:PrivateMsg ID="PrivateMsg" runat="server"/>
<asp:PlaceHolder ID="FriendsAspPlaceHolder" runat="server" />