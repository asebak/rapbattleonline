<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoodAdd.ascx.cs" Inherits="FreestyleOnline.Controls.HoodAdd" %>
<div class="panel panel-primary">
    <div class="panel-heading">
        <h3 class="panel-title">
            <%= this.Text("HOOD", "ADD") %>
        </h3>
    </div>
    <div class="panel-body">
        <div class="form-group">
            <label for="NameTxtBox"><%=this.Text("COMMON", "COMMON_NAME") %></label>
            <input type="text" class="form-control" id="NameTxtBox" runat="server" />
            <label><%=this.Text("COMMON", "COMMON_UPLOADPICTURE")%></label>
            <ext:FileUploadField Width="2000" Cls="form-control" ID="PictureUpload" Icon="PictureAdd" runat="server" />
            <label for="DescriptionTxtBox"><%=this.Text("COMMON", "COMMON_DESCRIPTION") %></label>
            <textarea class="form-control disableResize" maxlength="200" rows="3" runat="server" id="DescriptionTxtBox"></textarea>
            <label><%=this.Text("COMMON", "COMMON_PRIVACY") %></label>
            <ext:RadioGroup Cls="form-control" AutoDataBind="true" IndicatorIcon="Information" Layout="ColumnLayout"
                IndicatorTip='<%#this.Text("HOOD", "HOOD_ADDHELP") %>'
                ColumnsWidths="1" ID="Privacy" runat="server" Vertical="false">
                <Items>
                    <ext:Radio ID="Radio1" runat="server" Checked="true" BoxLabel='<%#this.Text("COMMON", "COMMON_PUBLIC") %>' InputValue="1" />
                    <ext:Radio ID="Radio2" runat="server" BoxLabel='<%#this.Text("COMMON", "COMMON_PRIVATE") %>' InputValue="0" />
                </Items>
            </ext:RadioGroup>
        </div>
    </div>
    <div class="panel-footer">
        <ext:Button AutoDataBind="true" Cls="btn btn-default" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' AutoPostBack="true" ID="CreateHood" runat="server" OnClick="CreateHood_Click" />
    </div>
</div>
