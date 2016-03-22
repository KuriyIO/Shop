using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected int Access()
        {
            HttpCookie UserCookie = Request.Cookies["user"];
            if (UserCookie != null)
            {
                switch (int.Parse(UserCookie["user"]))
                {
                    case 1:
                        {
                            return 1;
                        }
                    case 2:
                        {
                            return 2;
                        }
                    default: return 0;
                }
            }
            else
            {
                return 0;
            }
        }
        protected string Logged()
        {
            HttpCookie UserCookie = Request.Cookies["user"];
            if (UserCookie != null)
            {
                return "<a href=" + "actions/logout.aspx" + ">Выход </a>";
            }
            else
            {
                return "<a href=" + "/login.aspx" + ">Вход </a>";
            }
        }
        protected string CartInfo()
        {
            HttpCookie CartCookie = Request.Cookies["cart"];
            if (CartCookie != null && CartCookie["cart"] != "")
            {
                int sum = 0;
                int count = 0;
                string value = CartCookie["cart"];
                string[] blocks = value.Split('a');
                foreach (var products in blocks)
                {
                    if (products != "")
                    {
                        string[] result = products.Split('c');
                        count = int.Parse(result[1]);
                        using (var db = new dbEntities())
                        {
                            var ID = int.Parse(result[0]);
                            var query = from b in db.products
                                        where b.Id == ID
                                        select b.Price;
                            sum += query.First().GetValueOrDefault();

                        }
                    }
                }
                return String.Format("<div id=" + "cartinfo" + ">Добавлено в корзину: {0} <br /> На суму: {1} грн</div> ", (blocks.Length - 1), sum);
            }
            else
            {
                return "<div id=" + "cartinfo" + ">Добавлено в корзину: 0 <br /> На суму: 0 грн</div> ";
            }
        }
        protected string Phone1()
        {
            using (var db = new dbEntities())
            {
                var query = from b in db.contents
                            where b.name.Equals("phone1")
                            select b.content1;
                return query.First();
            }
        }
        protected string Phone2()
        {
            using (var db = new dbEntities())
            {
                var query = from b in db.contents
                            where b.name.Equals("phone2")
                            select b.content1;
                return query.First();
            }
        }
        protected string Footer()
        {
            using (var db = new dbEntities())
            {
                var query = from b in db.contents
                            where b.name.Equals("footer")
                            select b.content1;
                return query.First();
            }
        }
        protected string AdminContent()
        {
            string result = "";
            if (Request.QueryString["type"] == null)
                return "<div style='font-size:20px; text-align:center; padding-top:20px;'>Слева находиться список доступных вам функций</div>";
            else
                if (Request.QueryString["res"] != null)
                {
                    int res = int.Parse(Request.QueryString["res"]);
                    switch (res)
                    {
                        case 1: return "<div style='font-size:20px; text-align:center; padding-top:20px;'>Операция прошла успешно</div>";
                        case 2: return "<div style='font-size:20px; text-align:center; padding-top:20px;'>Ошибка при выполнении операции. Операция не выполнена или выполнена не до конца!</div>";
                        default: return "";
                    }
                }
                else
                {
                    int type = int.Parse(Request.QueryString["type"]);
                    switch (type)
                    {
                        case 1:
                            {
                                if (Access() == 1)
                                    result += "<form enctype=multipart/form-data action=actions/admin/addproduct.aspx method=post >Популярность товара(чем выше тем выше он отображаеться):<br /><input type=text name=popularity /><br />Цена в гривнах:<br /><input type=text name=price /><br />Название товара:<br /><input type=text name=name /><br />Категория товара (Вводить ИД категории! просмотреть ид нужной категории можно во вкладке изменить категорию):<br /><input type=text name=category /><br />Описание товара(можно применять html тэги):<br /><textarea name=description style='width:600px; height:600px;'></textarea><br /><input type=hidden name=MAX_FILE_SIZE value=30000 />Изображение товара(рекомендуется 200х200 пикселей в формате png):<br /><input type=file name=image /><br /><input type=submit value='Добавить товар' /></form>";
                                else
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нету доступа</div>";
                                return result;
                            }
                        case 2:
                            {
                                if (Access() == 1)
                                {
                                    if (Request.QueryString["id"] == null)
                                    {
                                        result += "<table border=1><tr><td>Ид товара</td><td>Код товара</td><td>Цена</td><td>Имя товара</td><td>Катогория</td><td>Изменить</td></tr>";
                                        using (var db = new dbEntities())
                                        {
                                            var query = from b in db.products
                                                        select b;
                                            foreach (var product in query)
                                            {
                                                result += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2} грн</td><td>{3}</td><td>{4}</td><td><a href=admin.aspx?type=2&id={0}>Изменить</a></td></tr>",
                                                                        product.Id, product.Id + 1274, product.Price, product.Name, product.Category);
                                            }
                                            result += "</table>";
                                        }
                                    }
                                    else
                                    {
                                        int id = int.Parse(Request.QueryString["id"]);
                                        using (var db = new dbEntities())
                                        {
                                            bool exist = false;
                                            var query = from b in db.products
                                                        where b.Id == id
                                                        select b;
                                            foreach (var product in query)
                                            {
                                                exist = true;
                                                result += string.Format("<form action=actions/admin/updateproduct.aspx method=post >Популярность товара(чем выше тем выше он отображаеться):<br /><input type=text name=popularity value={0} /><br />Цена в гривнах:<br /><input type=text name=price value={1} /><br />Название товара:<br /><input type=text name=name value={2} /><br />Категория товара (Вводить ИД категории! просмотреть ид нужной категории можно во вкладке изменить категорию):<br /><input type=text name=category value={3} /><br />Описание товара(можно применять html тэги):<br /><textarea name=description style='width:600px; height:600px;'>{4}</textarea><br />Изображение товара:<br /><input type=text name=image value={5} /><br /><input type=hidden value={6} name=id /><input type=submit value=Изменить товар /></form>",
                                                                        product.Popularity, product.Price, product.Name, product.Category, product.Description, product.Image, product.Id);
                                            }
                                            if (exist == false)
                                                result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>Данного товара нету в базе</div>";
                                        }
                                    }
                                }
                                else
                                {
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                }
                                return result;
                            }
                        case 3:
                            {
                                if (Access() != 1)
                                {
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                }
                                else
                                {
                                    result += "<form action=actions/admin/delproducts.aspx><table border=1><tr><td>Ид товара</td><td>Код товара</td><td>Цена</td><td>Имя товара</td><td>Катогория</td><td>Удалить</td></tr>";
                                    using (var db = new dbEntities())
                                    {
                                        var query = from b in db.products
                                                    select b;
                                        foreach (var product in query)
                                        {
                                            result += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2} грн</td><td>{3}</td><td>{4}</td><td><input type=checkbox name=Flags[] value={0} /></td></tr>",
                                                                    product.Id, product.Id + 1274, product.Price, product.Name, product.Category);
                                        }
                                        result += "<tr><td colspan=6 align=center><input type=hidden name=Url value=~/admin.aspx?res=1 /><input type=submit value='Удалить отмеченные' /></td></tr></table></form>";
                                    }
                                }
                                return result;
                            }
                        case 4:
                            {
                                if (Access() != 1)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    result += "<form enctype=multipart/form-data action=actions/admin/addcategory.aspx method=post>Название категории:<br /><input type=text name=name /><br /><input type=hidden name=MAX_FILE_SIZE value=30000 />Изображение категории(рекомендуется 200х200 пикселей в формате png):<br /><input type=file name=image /><br /><input type=submit value='Добавить категорию' /></form>";
                                }
                                return result;
                            }
                        case 5:
                            {
                                if (Access() != 1)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    if (Request.QueryString["id"] == null)
                                    {
                                        result += "<table border=1><tr><td>Ид категории</td><td>Название</td><td>Изменить</td></tr>";
                                        using (var db = new dbEntities())
                                        {
                                            var q = from b in db.categories
                                                    select b;
                                            foreach (var category in q)
                                            {
                                                result += string.Format("<tr><td>{0}</td><td>{1}</td><td><a href=admin.aspx?type=5&id={0}>Изменить</a></td></tr>",
                                                                        category.Id, category.CategoryName);
                                            }
                                        }
                                        result += "</table>";
                                    }
                                    else
                                    {
                                        int id = int.Parse(Request.QueryString["id"]);
                                        using (var db = new dbEntities())
                                        {
                                            var q = from b in db.categories
                                                    where b.Id == id
                                                    select b;
                                            foreach (var cat in q)
                                            {
                                                result += string.Format("<form action=actions/admin/updatecategory.aspx>Название категории:<br /><input type=text name=name value={0} /><br />Изображение категории:<br /><input type=text name=image value={1} /><br /><input type=hidden value={2} name=id /><input type=submit value='Изменить категорию' /></form>",
                                                    cat.CategoryName, cat.Image, cat.Id);
                                            }
                                        }
                                    }
                                }
                                return result;
                            }
                        case 6:
                            {
                                if (Access() != 1)
                                {
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                }
                                else
                                {
                                    result += "<form action=actions/admin/delcategory.aspx><table border=1><tr><td>Ид категории</td><td>Название</td><td>Удалить</td></tr>";
                                    using (var db = new dbEntities())
                                    {
                                        var query = from b in db.categories
                                                    select b;
                                        foreach (var cat in query)
                                        {
                                            result += string.Format("<tr><td>{0}</td><td>{1}</td><td><input type=checkbox name=Flags[] value={0} /></td></tr>",
                                                cat.Id, cat.CategoryName);
                                        }
                                        result += "<tr><td colspan=3 align=center><input type=submit value='Удалить отмеченные' /></td></tr></table></form>";
                                    }
                                }
                                return result;
                            }
                        case 7:
                            {
                                if (Access() != 1 && Access() != 2)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    result += "<form action=actions/admin/delbanlist.aspx><table border=1><tr><td>IP-адрес</td><td>Удалить</td></tr>";
                                    using (var db = new dbEntities())
                                    {
                                        var q = from b in db.banlists
                                                select b;
                                        foreach (var adress in q)
                                        {
                                            result += string.Format("<tr><td>{0}</td><td><input type=checkbox name=Flags[] value={1} /></td></tr>",
                                                                    adress.Ip, adress.Id);
                                        }
                                    }
                                    result += "<tr><td colspan=2 align=center><input type=submit value='Удалить отмеченные' /></td></tr></table></form>";
                                    result += "<form style='margin-top:20px;' action=actions/admin/clearbanlist.aspx>Очистить весь список:<br /><input type=submit value='Очистить банлист' /></form>";
                                    result += "<form style='margin-top:20px;' action=actions/admin/banip.aspx>Добавить адрес в банлист:<br /><input type=text name=userIp /><br /><input type=hidden name=Url value=~/admin.aspx?res=1 /><input type=submit value='Добавить в банлист' /></form>";
                                }
                                return result;
                            }
                        case 8:
                            {
                                if (Access() != 1)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    result += "<form action=actions/admin/adduser.aspx><h3>Добавить пользователя</h3><br /><table><tr><td>Логин: </td><td><input type=text name=login /></td></tr><tr><td>Пароль: </td><td><input type=text name=pass /></td></tr><tr><td>Уровень доступа(1-админ 2-модератор)</td><td><input type=text name=access /></td></tr><tr><td colspan=2><input type=submit value='Добавить пользователя' /></td></tr></form>";

                                    result += "<form action=actions/admin/delusers.aspx><table border=1><tr><td>Логин</td><td>Пароль</td><td>Уровень доступа</td><td>Удалить</td></tr>";
                                    using (var db = new dbEntities())
                                    {
                                        var q = from b in db.users
                                                select b;
                                        foreach (var user in q)
                                        {
                                            result += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td><input type=checkbox name=Flags[] value={3} /></td></tr>",
                                                                    user.Login, user.Password, user.Access, user.Id);
                                        }
                                    }
                                    result += "<tr><td colspan=4 align=center><input type=submit value='Удалить отмеченные' /></td></tr></table></form>";
                                }
                                return result;
                            }
                        case 9:
                            {
                                if (Access() != 1)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    result += "<form action=actions/admin/content.aspx method=post >";
                                    using (var db = new dbEntities())
                                    {
                                        var q = from b in db.contents
                                                select b;
                                        foreach (var page in q)
                                        {
                                            result += string.Format("<h3>{0}</h3><br /><textarea name={1} style='width:600px; height:600px;'>{2}</textarea><br />",
                                                                    page.title, page.id, page.content1);
                                        }
                                    }
                                    result += "<input type=submit value='Применить изменения'></form>";
                                }
                                return result;
                            }
                        case 10:
                            {
                                if (Access() != 1)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    int sum = 0;
                                    using (var db = new dbEntities())
                                    {
                                        var q = from b in db.orders
                                                select b;
                                        foreach (var order in q)
                                        {
                                            result += string.Format("<div style='margin-top:15px; margin-bottom:15px;'><h3>Заказ №{0}</h3><form action=actions/admin/delorder.aspx><input type=hidden name=id value={0} /><input type=hidden name=Url value={1} /><input type=submit value='Удалить заказ' /></form><br /><table border=1><tr><td>Название</td><td>Количиество</td><td>Цена за единицу</td><td>Цена за все</td></tr>",
                                                                    order.Id, Request.Url);
                                            string[] blocks = order.OrderProducts.Split('a');
                                            foreach (var block in blocks)
                                            {
                                                if (block != "")
                                                {
                                                    string[] info = block.Split('c');
                                                    int prodId = int.Parse(info[0]);
                                                    var q2 = from b in db.products
                                                             where b.Id == prodId
                                                             select b;
                                                    foreach (var product in q2)
                                                    {
                                                        result += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td></tr>",
                                                                                product.Name, info[1], product.Price, int.Parse(info[1]) * (int)product.Price);
                                                        sum += int.Parse(info[1]) * (int)product.Price;
                                                    }
                                                }
                                            }
                                            result += string.Format("<tr><td colspan=4 align=right>Итого:{0} грн</td></tr></table><p>Адрес доставки: {1}</p><p>Контакты заказчика: {2}</p><p>Дополнительные пожелания: {3}</p></div>",
                                                                    sum, order.OrderAdress, order.OrderContacts, order.OrderAdditional);

                                        }
                                    }
                                }
                                return result;
                            }
                        case 11:
                            {
                                if (Access() != 1 && Access() != 2)
                                    result += "<div style='font-size:20px; text-align:center; padding-top:20px;'>У вас нет доступа</div>";
                                else
                                {
                                    using (var db = new dbEntities())
                                    {
                                        var q = from b in db.comments
                                                select b;
                                        foreach (var comment in q)
                                        {
                                            bool banned = false;
                                            string ip = comment.Ip;

                                            var q2 = from a in db.banlists
                                                     where a.Ip == ip
                                                     select a;
                                            foreach (var ban in q2)
                                            {
                                                banned = true;
                                            }
                                            if (banned == false)
                                            {
                                                result += string.Format("<div class=comment><div class=comment-name>{0}</div><div class=comment-time>{1}</div><div style='clear:both;'></div><p class=comment-text>{2}</p>",
                                                                        comment.Name, comment.Time, comment.Text);
                                                result += string.Format("<form action=actions/admin/delcomment.aspx><input type=hidden value={0} name=Id /><input type=hidden value={2} name=Url /><input type=submit value='Удалить отзыв'></form><form action=actions/admin/banip.aspx><input type=hidden value={1} name=userIp /><input type=hidden value={2} name=Url /><input type=submit value='Заблокировать пользователя'></form></div>",
                                                                        comment.Id, comment.Ip, Request.Url);
                                            }
                                        }
                                    }
                                }
                                return result;
                            }
                        default: return "<div style='font-size:20px; text-align:center; padding-top:20px;'>Слева находиться список доступных вам функций</div>";
                    }

                }
        }				
    }
}