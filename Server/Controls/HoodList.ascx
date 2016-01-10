<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoodList.ascx.cs" Inherits="FreestyleOnline.Controls.HoodList" %>
<%@ Register TagPrefix="RAP" TagName="HoodBox" Src="~/Controls/HoodBox.ascx" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>

<asp:GridView Width="100%" GridLines="None" ShowHeader="false" ID="HoodGridView" AutoGenerateColumns="false" runat="server" OnRowDataBound="HoodGridView_RowDataBound">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <RAP:HoodBox ID="HoodBox" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="HoodListPager" runat="server" />
