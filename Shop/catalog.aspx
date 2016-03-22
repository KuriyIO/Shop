<%@ Page Title="" Language="C#" MasterPageFile="~/Shop.Master" AutoEventWireup="true" CodeBehind="catalog.aspx.cs" Inherits="Shop.catalog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MenuContent" runat="server">

    <li><a href="/home.aspx"><span>Главная</span></a></li>
	<li><a href="/catalog.aspx" class="current"><span>Каталог товаров</span></a></li>
	<li><a href="/about.aspx"><span>О Нас</span></a></li>
    <li><a href="/payment.aspx"><span>Оплата и доставка</span></a></li>
    <li><a href="/contacts.aspx"><span>Контакты</span></a></li>
    <%
        HttpCookie UserCookie = Request.Cookies["user"];
        if (UserCookie != null)
        {
            switch (int.Parse(UserCookie["user"]))
            {
                case 1:
                    {
                        Response.Write("<li><a href=/admin.aspx><span>Управление</span></a></li>");
                        break;
                    }
                case 2:
                    {
                        Response.Write("<li><a href=/admin.aspx><span>Управление</span></a></li>");
                        break;
                    }
                default: break;
            }
        }
    %>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <%# CatalogContent() %>
</asp:Content>
