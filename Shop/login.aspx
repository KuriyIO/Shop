<%@ Page Title="" Language="C#" MasterPageFile="~/Shop.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Shop.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MenuContent" runat="server">

    <li><a href="/home.aspx"><span>Главная</span></a></li>
	<li><a href="/catalog.aspx"><span>Каталог товаров</span></a></li>
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
    <div style="margin-top:25px; margin-left: 225px;">
        <form action="actions/log.aspx">
            <table>
                <tr>
                    <td>Логин: </td><td><input type="text" name="login" required="required" /></td>
                </tr>
                <tr>
                    <td>Пароль: </td><td><input type="password" name="pass" required="required" /></td>
                </tr>
                <tr align="center">
                    <td colspan="2" align="center">
                        <input type="submit" value="Войти"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <%
        int err;
        if(Request.QueryString["err"]==null)
            err=0;
        else
	  	    err = int.Parse(Request.QueryString["err"]);
		switch(err)
		{
			case 1:
			{
				Response.Write("<div style='font-size:20px; width:600px; text-align:center; color:red; padding-top:20px;'>Неправильный логин или пароль!</div>");
				break;
			}
			case 2:
			{
                Response.Write("<div style='font-size:20px; width:600px; text-align:center; color:red; padding-top:20px;'>Чтото пошло не так.</div>");
				break;
			}
			default:
			{
				break;	
			}
		}
    %>
</asp:Content>
