<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddWrittenVerse.ascx.cs" Inherits="FreestyleOnline.Controls.Verses.AddWrittenVerse" %>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title"><%=this.Text("VERSES", "ADD_WRITTEN")%></h3>
    </div>
    <div class="panel-body">
        <div class="form-group">
            <label for="VerseTitle"><%=this.Text("VERSES", "NAME")%></label>
            <input type="text" class="form-control" id="VerseTitle" runat="server" />
        </div>
        <div class="form-group">
            <label for="VerseContent"><%=this.Text("VERSES", "CONTENT")%></label>
            <textarea class="form-control disableResize" id="VerseContent" runat="server"></textarea>
        </div>
    </div>
    <div class="panel-footer">
        <ext:Button AutoDataBind="true" AutoPostBack="true" ID="WrittenVerseBtn" runat="server" Cls="btn btn-default"
            Text='<%#this.Text("COMMON", "COMMON_SUBMIT")%>' OnClick="WrittenVerse_Click" />
    </div>
</div>
