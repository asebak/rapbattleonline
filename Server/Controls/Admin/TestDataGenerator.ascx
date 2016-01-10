<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TestDataGenerator.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.TestDataGenerator" %>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TESTDATA_USERTITLE") %>
                </h3>
            </div>
            <div class="panel-body">
                <ext:TextField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("ADMIN", "TESTDATA_BASEUSERNAME") %>' ID="BaseUserName">
                    <ToolTips>
                        <ext:ToolTip runat="server" Html='<%#this.Text("ADMIN", "TESTDATA_BASEUSERNAMEHELP") %>'/>
                    </ToolTips>
                </ext:TextField>
                <ext:TextField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("ADMIN", "TESTDATA_BASEEMAIL") %>' ID="BaseEmail">
                    <ToolTips>
                        <ext:ToolTip runat="server" Html='<%#this.Text("ADMIN", "TESTDATA_BASEEMAILHELP") %>'/>
                    </ToolTips>
                </ext:TextField>
                <ext:TextField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("COMMON", "COMMON_PASSWORD") %>' ID="PasswordTextField" />
                <ext:NumberField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("COMMON", "COMMON_TOTALUSERS") %>' MaxValue="10000" MinValue="1" Number="1000" ID="TotalUsersCreation" />
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" AutoPostBack="true" ID="UserCreator"
                    Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' runat="server" OnClick="UserCreator_Click" />
            </div>
        </div>
    </div>
</div>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TESTDATA_HOODTITLE") %>
                </h3>
            </div>
            <div class="panel-body">
                <ext:TextField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("ADMIN", "TESTDATA_BASEHOODNAME") %>' ID="BaseHood">
                    <ToolTips>
                        <ext:ToolTip runat="server" Html='<%#this.Text("ADMIN", "TESTDATA_BASEHOODNAMEHELP") %>'/>
                    </ToolTips>
                </ext:TextField>
                <ext:FileUploadField Cls="form-control" AutoDataBind="true" FieldLabel='<%#this.Text("COMMON", "COMMON_UPLOADPICTURE") %>' ID="HoodPicture" Icon="PictureAdd" runat="server" />
                <ext:NumberField Cls="form-control" AutoDataBind="true" runat="server" FieldLabel='<%#this.Text("COMMON", "COMMON_TOTALUSERS") %>' MaxValue="10000" MinValue="1" Number="100" ID="TotalHoodsCreation" />
            </div>
            <div class="panel-footer">
                <ext:Button Cls="btn btn-default" AutoDataBind="true" runat="server" ID="HoodCreator" AutoPostBack="true"
                    Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' OnClick="HoodCreator_Click" />
            </div>
        </div>
    </div>
</div>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TESTDATA_WBTITLE") %>
                </h3>
            </div>
            <div class="panel-body">
            </div>
            <div class="panel-footer">
            </div>
        </div>
    </div>
</div>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TESTDATA_ABTITLE") %>
                </h3>
            </div>
            <div class="panel-body">
            </div>
            <div class="panel-footer">
            </div>
        </div>
    </div>
</div>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-danger">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%=this.Text("ADMIN", "TESTDATA_MUSICTITLE") %>
                </h3>
            </div>
            <div class="panel-body">
            </div>
            <div class="panel-footer">
            </div>
        </div>
    </div>
</div>