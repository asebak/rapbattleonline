<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewsManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.EditNewsFeed" %>
<%@ Register TagPrefix="RAP" TagName="PostEditor" Src="~/Controls/Generic/PostEditor.ascx" %>
<br />
<table class="postcomment">
    <tr runat="server">
        <td>
            <label><%= this.Text("ADMIN", "NEWSMANAGER_CONTENT") %></label>
            <input type="text" class="form-control" runat="server" id="NewsTitle"/>
        </td>
    </tr>
    <tr>
        <td>
            <RAP:PostEditor ID="NewsPoster" runat="server"/>
        </td>
    </tr>
</table>
<ext:GridPanel Title='<%#this.Text("ADMIN","MANAGE_OLDNEWS")%>' AutoDataBind="true" ID="NewsGrid" AnimCollapse="true" Collapsible="false" DisableSelection="false" runat="server">
    <Store>
        <ext:Store ID="NewsStore" runat="server">
            <Model>
                <ext:Model runat="server" IDProperty="NewsID" ID="ctl57">
                    <Fields>
                        <ext:ModelField Name="NewsID" />
                        <ext:ModelField Name="News" />
                        <ext:ModelField Name="Title"/>
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
    </Store>
    <ColumnModel runat="server">
        <Columns>
            <ext:Column ID="NewsCol" runat="server" Text='<%#this.Text("ADMIN", "NEWSMANAGER_CONTENT") %>' DataIndex="Title" Flex="1" />
        </Columns>
    </ColumnModel>
    <Plugins>
        <ext:RowExpander runat="server">
            <Template runat="server">
                <Html>
                    <p><b>News :</b> {News}</p>
                </Html>
            </Template>
        </ext:RowExpander>
    </Plugins>
    <SelectionModel>
        <ext:RowSelectionModel runat="server" Mode="Single"/>
    </SelectionModel>
    <Buttons>
        <ext:Button Cls="btn btn-default" AutoDataBind="true" ID="btnDelete" AutoPostBack="true" runat="server" Text='<%#this.Text("COMMON", "COMMON_DELETE") %>' Icon="Delete">
            <Listeners>
                <Click Handler="#{DirectMethods}.HandleDeletion();"/>
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:GridPanel>