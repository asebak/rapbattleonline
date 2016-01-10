<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AudioVerses.ascx.cs" Inherits="FreestyleOnline.Controls.Verses.AudioVerses" %>
<%@ Register TagPrefix="RAP" TagName="Pager" Src="~/Controls/Generic/Pager.ascx" %>

<asp:GridView runat="server" ID="AudioVersesGV" Width="100%" AutoGenerateColumns="false" ShowHeader="false" GridLines="None" ShowFooter="false">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <div class="form-group">
                    <div class="row">
                        <div class="col-md-6 column">
                            <ext:Label ID="Label1" Cls="form-control" Text='<%# Eval("Title") %>' AutoDataBind="true" runat="server" />
                        </div>
                        <div class="col-md-6 column">
                            <asp:ImageButton Visible='<%#this.UserId == this.PageContext.PageUserID %>' ImageUrl="/icons/delete_16.png" 
                                ID="DeleteVerse" CommandArgument='<%# Eval("VerseId") %>' OnCommand="DeleteVerse_Command" runat="server" />
                        </div>
                    </div>
                    <audio controls class="form-control">
                        <source src='/api/verses/getaudioverse/<%#Eval("VerseId") %>' type="audio/mpeg" />
                    </audio>
                </div>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<RAP:Pager ID="AudioVersesPager" runat="server" />
<asp:PlaceHolder ID="NoAudioVerses" runat="server" />
