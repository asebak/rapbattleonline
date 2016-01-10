<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExceptionList.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.ExceptionList" %>
<ext:GridPanel TitleCollapse="false" Title='<%#this.Text("ADMIN", "EXCEPTION_MSG")%>' AutoDataBind="true" ID="ExceptionsGrid" AnimCollapse="true" Collapsible="false" DisableSelection="false" Icon="Error" Height="500" Width="700" AutoScroll="true" runat="server">
    <Store>
        <ext:Store ID="ExceptionsStore" runat="server">
            <Model>
                <ext:Model runat="server" IDProperty="Message" ID="ctl57">
                    <Fields>
                        <ext:ModelField Name="Message" />
                        <ext:ModelField Name="Source" />
                        <ext:ModelField Name="Stack" />
                        <ext:ModelField Name="Date" />
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
    </Store>
    <ColumnModel runat="server">
        <Columns>
            <ext:Column AutoDataBind="true" ID="ExceptionCol" runat="server" Text='<%#this.Text("ADMIN", "EXCEPTION_MSG")%>' DataIndex="Message" Flex="1" />
        </Columns>
    </ColumnModel>
    <Plugins>
        <ext:RowExpander runat="server">
            <Template runat="server">
                <Html>
                    <p><b>Date:</b> {Date}</p>
                    <br />
                    <p><b>Source:</b> {Source}</p>
                    <br />
                    <p><b>Stack:</b> {Stack}</p>
                    <br />
                </Html>
            </Template>
        </ext:RowExpander>
    </Plugins>
    <SelectionModel>
        <ext:RowSelectionModel runat="server" Mode="Single"/>
    </SelectionModel>
    <Buttons>
        <ext:Button AutoDataBind="true" ID="btnDelete" AutoPostBack="true" runat="server" Text='<%#this.Text("COMMON", "COMMON_DELETE")%>' Icon="Delete">
            <Listeners>
                <Click Handler="#{DirectMethods}.HandleDeletion();"/>
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:GridPanel>