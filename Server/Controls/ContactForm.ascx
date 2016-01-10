<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactForm.ascx.cs" Inherits="FreestyleOnline.Controls.ContactForm" %>
<div class="row clearfix">
    <div class="col-md-6 column">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <%= this.Text("CONTACT", "CONTACT_TITLE") %>

                </h3>
            </div>
            <div class="panel-body">
                <ext:Label AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_TITLE") %>' runat="server"/>
                <input type="text" class="form-control" runat="server" id="TitleTxtBox" />
                <br />
                <ext:Label AutoDataBind="true" Text='<%#this.Text("COMMON", "COMMON_CONTENT") %>' runat="server"/>
                <textarea  class="form-control" rows="3" runat="server" id="ContentTxtBox"></textarea>
            </div>
            <div class="panel-footer">
                <ext:Button AutoDataBind="true" runat="server" ID="SubmitBtn" Text='<%#this.Text("COMMON", "COMMON_SUBMIT") %>' Icon="Email" 
                            AutoPostBack="true" OnClientClick=" return handlepostback(#;{SubmitBtn;}); " OnClick="SubmitBtn_Click"/>        
            </div>
        </div>
    </div>
</div>