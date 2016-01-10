<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MusicAdd.ascx.cs" Inherits="FreestyleOnline.Controls.MusicAdd" %>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title">
            <%= this.Text("MUSIC", "MUSIC_ADDTITLE") %>
        </h3>
    </div>
    <div class="panel-body">
        <ext:TextField Cls="form-control" AutoDataBind="true" AllowBlank="false" FieldLabel='<%#this.Text("COMMON", "COMMON_TITLE") %>' ID="TitleBoxMusic" runat="server" />
        <ext:FileUploadField Cls="form-control"  AutoDataBind="true" AllowBlank="false" Icon="Picture" FieldLabel='<%#this.Text("COMMON", "COMMON_UPLOADPICTURE") %>' ID="PictureFile" runat="server" />
        <ext:FileUploadField Cls="form-control"  AutoDataBind="true" AllowBlank="false" Icon="MusicNote" FieldLabel='<%#this.Text("COMMON", "COMMON_UPLOADMUSIC") %>' ID="MusicFile" runat="server" />
        <ext:RadioGroup Cls="form-control"  AutoDataBind="true" Layout="ColumnLayout" ColumnsWidths="1" FieldLabel='<%#this.Text("MUSIC", "MUSIC_DOWNLOADS") %>'
            ID="RadioGroupDownloadable" runat="server" Vertical="false">
            <Items>
                <ext:Radio ID="Radio1" Checked="true" BoxLabel='<%#this.Text("COMMON", "COMMON_YES") %>' runat="server" InputValue="1" />
                <ext:Radio ID="Radio2" runat="server" BoxLabel='<%#this.Text("COMMON", "COMMON_NO") %>' InputValue="0" />
            </Items>
        </ext:RadioGroup>

    </div>
    <div class="panel-footer">
               <ext:Button AutoDataBind="true" AutoPostBack="true" runat="server" ID="UploadMusic" Cls="btn btn-primary" 
            Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' OnClick="Upload_Click" /> 
    </div>
</div>