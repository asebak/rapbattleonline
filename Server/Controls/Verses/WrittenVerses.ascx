<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WrittenVerses.ascx.cs" Inherits="FreestyleOnline.Controls.Verses.WrittenVerses" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>
<asp:GridView runat="server" ID="WrittenVersesGV" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" Width="100%" ShowFooter="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-10 column">
                            <ext:Label ID="Label1" Cls="form-control" Text='<%# Eval("Title") %>' AutoDataBind="true" runat="server" />
                        </div>
                        <div class="col-md-1 column">
                            <asp:ImageButton Visible='<%#this.UserId == this.PageContext.PageUserID %>' ImageUrl="/icons/delete_16.png"
                                ID="DeleteVerseWritten" CommandArgument='<%# Eval("VerseId") %>' OnCommand="DeleteVerseWritten_Command" runat="server" />
                        </div>
                    </div>
                    <script type="text/javascript">
                             <% if (this.PageContext.PageUserID == this.UserId)
                                {%>
                        createEditableVerse('<%#Eval("VerseId")%>');
                        <%}%>
                    </script>
                    <a href="#" class="formattedLabel form-control" id='<%#string.Format("writtenverse{0}", Eval("VerseId"))%>' data-type="textarea"><%#Eval("Content")%></a>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="WrittenVersesPager" runat="server" />
<asp:PlaceHolder ID="NoWrittenVerses" runat="server"/>