<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GlobalMessagesManager.ascx.cs" Inherits="FreestyleOnline.Controls.Admin.GlobalMessagesManager" %>
<div class="row clearfix">
    <div class="col-md-12 column">
        <div class="panel panel-success">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%= this.Text("ADMIN", "RT_MSG") %>
                </h3>
            </div>
            <div class="panel-body">
                <ext:Label AutoDataBind="true" runat="server" Text='<%#this.Text("COMMON", "COMMON_TITLE2") %>'></ext:Label>
                <br />
                <input type="text" id="contentTitle" class="form-control" /><br />
                <br />
                <ext:Label runat="server" AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_MESSAGE") %>'></ext:Label>
                <br/>
                <textarea  id="contentMessage" class="form-control disableResize" rows="6"></textarea>
                <br />
            </div>
            <div class="panel-footer">
                <input type="button" id="notifyAll" value='<%= this.Text("COMMON", "COMMON_SUBMIT") %>' class="btn btn-default" />
            </div>
        </div>
    </div>
</div>