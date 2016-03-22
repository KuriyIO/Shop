using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected string CartContent()
        {
            string result = "";
            HttpCookie CartCookie = Request.Cookies["cart"];
            if (CartCookie != null && CartCookie["cart"] != "")
            {
                result += "<form action=actions/delcart.aspx class=cart-form><table align=center border=1><tr><td>Наименование</td><td>Количество</td><td>Цена за единицу</td><td>Удалить</td></tr>";
                using (var db = new dbEntities())
                {
                    int sum = 0;
                    int count = 0;
                    string value = CartCookie["cart"];
                    string[] blocks = value.Split('a');
                    foreach (var products in blocks)
                    {
                        if (products != "")
                        {
                            string[] info = products.Split('c');
                            count = int.Parse(info[1]);
                            var ID = int.Parse(info[0]);
                            var query = from b in db.products
                                        where b.Id == ID
                                        select b;
                            sum += query.First().Price.GetValueOrDefault() * count;
                            result += string.Format("<tr><td align=left>{0}</td><td>{1} шт.</td><td>{2} грн</td><td><input type=checkbox name=Flags[] value={3} /></td></tr>",
                                                  query.First().Name, count, query.First().Price.GetValueOrDefault(), query.First().Id);
                        }
                    }
                    result += string.Format("<tr><td></td><td>Итого: </td><td>{0} грн</td><td></td></tr>", sum);
                }
                result += "</table><input type=submit value='Удалить отмеченные' class=submit /></form><form action=actions/clearcart.aspx class=clearcart-form><input type=submit value='Очистить корзину' class=submit></form><form action=actions/addorder.aspx class=addorder-form><table align=center border=0><tr align=left><td>Ваш номер телефона:</td><td><input type=text name=contacts required=required/></td></tr><tr align=left><td>Адрес доставки:</td><td><input type=text name=Adress /></td></tr><tr><td colspan=2>Дополнительные пожелания:</td></tr></table><textarea name=additional class=additional-info></textarea><br /><input type=submit value='Сделать заказ' class=submit /></form>";
            }
            else result = "Ваша корзина пуста!";

            if (Request.QueryString["err"] != null)
            {
                int err = int.Parse(Request.QueryString["err"]);
                if (err == 0)
                {
                    result += "<div style='font-size:20px; text-align:center; color:red; padding-top:20px;'>Ваш заказ принят. Ждите пока с вами свяжется курьер.</div>";
                }
                else
                {
                    result += "<div style='font-size:20px; text-align:center; color:red; padding-top:20px;'>Чтото пошло не так. <br />Если ошибка повториться то вы можете совершить заказ по телефону.</div>";
                }
            }
            return result;
        }
    }
}
	
        
    
