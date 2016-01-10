<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminButton.ascx.cs" Inherits="FreestyleOnline.Controls.SiteAdmin" %>
<%@ Import Namespace="YAF.Core" %>
<%@ Import Namespace="YAF.Core.Services" %>
<%@ Import Namespace="YAF.Types.Constants" %>
<%@ Import Namespace="YAF.Types.Interfaces" %>
<%@ Import Namespace="YAF.Utils" %>
<%@ Import Namespace="YAF.Types.Extensions" %>
<script type="text/javascript">

    var enabled = "<%= this.PageContext.IsAdmin%>";
    if (enabled == "True")
    {
        enabled = true;
    }
    else
    {
        enabled = false;
    }
    var adminlogin = new sap.ui.commons.Button({
        text: "Admin Controls",
        icon: "../forum/resources/icons/gear.png",
        lite: true,
        visible: enabled,
        press: function () { window.location = "../Pages/Admin.aspx"; }
    }).placeAt("AdminControl");

</script>
<div id="AdminControl"></div>