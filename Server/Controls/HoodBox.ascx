<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoodBox.ascx.cs" Inherits="FreestyleOnline.Controls.HoodBox" %>
<%@ Register TagPrefix="RAP" TagName="ProfileLink" Src="~/Controls/ProfileLink.ascx" %>
<div class="panel panel-default">
    <div class="panel-body">
        <div class="row clearfix">
            <div class="col-md-6 column">
                <img width="100" height="100" id="HoodItemImage" class="img-thumbnail" runat="server" />
                <div class="control-group">
                    <ext:HyperLink ID="HoodHyperLink" class="control-label" runat="server" />
                    <div class="controls">
                        <br />
                        <label style="width: 100%; word-break: break-all;" runat="server" id="HoodAbout"></label>
                        <br />
                        <label><%= this.Text("COMMON", "COMMON_DATEADDED2") %></label>
                        <YAF:DisplayDateTime ID="DateCreated" runat="server" />
                        <br />
                    </div>
                </div>
            </div>
            <div class="col-md-6 column">
                <table>
                    <ext:Label ID="HoodTotalMembers" runat="server" />
                    <asp:Repeater ID="HoodUsersRepeater" runat="server">
                        <ItemTemplate>
                            <%#(Container.ItemIndex + 5)%5 == 0 ? "<tr>" : string.Empty %>
                            <td>
                                <RAP:ProfileLink ID="ProfileLink" UserId='<%#DataBinder.Eval(Container.DataItem, "UserId") %>' runat="server" />
                            </td>
                            <%#(Container.ItemIndex + 5)%5 == 4 ? "</tr>" : string.Empty %>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
    <div class="panel-footer">
        <%-- TODO: potentially need white text in leave and success buttons --%>
        <ext:Button ID="JoinButton" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_JOIN") %>' AutoPostBack="true" Cls="btn btn-success whiteText"
            OnCommand="JoinButton_Command" runat="server" />
        <ext:Button ID="LeaveButton" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_LEAVE") %>' AutoPostBack="true" Cls="btn btn-danger"
            runat="server" OnCommand="LeaveButton_Command" />
    </div>
</div>
