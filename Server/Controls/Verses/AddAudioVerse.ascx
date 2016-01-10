<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAudioVerse.ascx.cs" Inherits="FreestyleOnline.Controls.Verses.AddAudioVerse" %>

<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title"><%=this.Text("VERSES", "ADD_AUDIO")%></h3>
    </div>
    <div class="panel-body">
        <div class="form-group">
            <label for="VerseTitleAudio"><%=this.Text("VERSES", "NAME")%></label>
            <input class="form-control" id="VerseTitleAudio" runat="server" />
        </div>
        <div class="form-group">
            <label runat="server"><%=this.Text("VERSES", "CONTENT")%></label>
            <ext:FileUploadField Cls="form-control" AutoPostBack="true" runat="server" ID="VerseUploader" />
        </div>
    </div>
    <div class="panel-footer">
        <ext:Button AutoDataBind="true" AutoPostBack="true" ID="AudioVerseBtn" runat="server" Cls="btn btn-default"
            OnClick="SubmitAudioVerse_Click" Text='<%#this.Text("COMMON", "COMMON_SUBMIT")%>' />
    </div>
</div>
