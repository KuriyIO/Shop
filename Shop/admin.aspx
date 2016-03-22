<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="Shop.admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<meta charset="utf-8">
<link rel="stylesheet" type="text/css" href="Styles/Style.css" />
<title>MyShop.wtf</title>
</head>

<body>
<div id="header">
	<div id="site-logo"><a href="http://test1.ru/"><img src="images/logo.png" /></a></div>
	<div id="phone"><h1><%# Phone1() %></h1><h1><%# Phone2() %></h1></div>

	<div id="search-container">
		<form class="form-wrapper" action="~/actions/search.aspx" id="searchform">
        	<input type="search" id="search" name="search" placeholder="Введите товар для поиска" required="required"/>
       		<input type="submit" id="submit"  Value="Поиск" />
		</form>
	</div>

	<div id="login">
        <%# Logged() %>
	</div>
	<div id="cart"><a href="cart.aspx"><img src="images/cart.png" /></a>
        <%# CartInfo() %>
	</div>
</div>

<div id="menu">
	<ul>
        <li><a href="/home.aspx"><span>Главная</span></a></li>
	    <li><a href="/catalog.aspx"><span>Каталог товаров</span></a></li>
	    <li><a href="/about.aspx"><span>О Нас</span></a></li>
        <li><a href="/payment.aspx"><span>Оплата и доставка</span></a></li>
        <li><a href="/contacts.aspx"><span>Контакты</span></a></li>
        <li><a href="/admin.aspx" class="current"><span>Управление</span></a></li>
    </ul>
</div>

<div id="container">
	<div id="category">
		<ul>
            <%
    	    switch(Access())
			{
				case 1: 
                {
                    Response.Write("<li><a href=admin.aspx?type=1 > Добавить товар</a></li><li><a href=admin.aspx?type=2 > Изменить товар</a></li><li><a href=admin.aspx?type=3 > Удалить товар</a></li><li><a href=admin.aspx?type=4 > Добавить категорию</a></li><li><a href=admin.aspx?type=5 > Изменить категорию</a></li><li><a href=admin.aspx?type=6 > Удалить категорию</a></li><li><a href=admin.aspx?type=7 > Банлист</a></li><li><a href=admin.aspx?type=8 > Пользователи</a></li><li><a href=admin.aspx?type=9 > Содержание страниц</a></li><li><a href=admin.aspx?type=10 > Просмотр заказов</a></li><li><a href=admin.aspx?type=11 > Просмотр отзывов</a></li>");
					break;
                }
                case 2:
                {
                    Response.Write("<li><a href=admin.aspx?type=7>Банлист</a></li><li><a href=admin.aspx?type=11>Просмотр отзывов</a></li>");
					break;
                }
                default: break;
			}
            %>
        </ul>
	</div>
	<div id="content">
        <%# AdminContent() %>   
	</div>
</div>

<div style="clear:both;"></div>

<div class="page-buffer"></div>

<div id="footer">
	<%# Footer() %>	<br />
	<a href="http://test1.ru/"><span>Главная</span></a> |
	<a href="http://test1.ru/catalog"><span>Каталог товаров</span></a> |
	<a href="http://test1.ru/about"><span>О Нас</span></a> |
	<a href="http://test1.ru/payment"><span>Оплата и доставка</span></a> |
	<a href="http://test1.ru/contacts"><span>Контакты</span></a>
</div>

</body>
</html>

