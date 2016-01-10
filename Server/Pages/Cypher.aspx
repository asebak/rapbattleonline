<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cypher.aspx.cs" Inherits="FreestyleOnline.Pages.Cypher"  %>
<%@ Register TagPrefix="RAP" TagName="CypherRoom" Src="~/Controls/RealTime/Cypher.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <meta charset="utf-8">
        <title>Cypher</title>
    </head>
    <body>
        <ext:ResourceManager runat="server" Theme="Gray" />
        <RAP:CypherRoom runat="server" />
    </body>
</html>