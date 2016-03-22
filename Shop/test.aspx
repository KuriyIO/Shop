<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Shop.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        HttpCookie CartCookie = Request.Cookies["cart"];
        Response.Write(CartCookie["cart"]);
    %>
    </div>
    </form>
</body>
</html>
