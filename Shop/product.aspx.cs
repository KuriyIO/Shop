using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class product1 : System.Web.UI.Page
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
        protected string ProductContent()
        {
            int id, dbId;
            string result = "";
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
                dbId = id - 1274;
            }
            else
            {
                id = 0;
                dbId = 0;
            }
            using (var db = new dbEntities())
            {
                var query = from b in db.products
                            where b.Id == dbId
                            select b;
                foreach (var prod in query)
                {
                    result += string.Format("<div class=product-header><div id=product-image><img src=/images/products/{0} width=300px/></div><div id=products-name>{1}</div><div id=product-id>Код товара:{2}</div><div id=price>{3} грн</div><div><form class=add-to-cart action=actions/AddToCart.aspx><input type=hidden value={4} name=Id /><input type=hidden value={5} name=Url /><div class=count>Количество:<input type=number value=1 name=Count class=product-count /></div><br /><input type=submit value='В корзину' class=submit></form>",
                                          prod.Image, prod.Name, id, prod.Price, dbId, Request.Url);
                    if (Access() == 1)
                    {
                        result += string.Format("<form action=actions/admin/delproduct.aspx><input type=hidden value={0} name=Id /><input type=hidden value={1} name=Url /><input type=submit value=Удалить товар></form>",
                                              dbId,Request.Url);
                        result += string.Format("<form action=actions/admin/updateredirect.aspx><input type=hidden value={0} name=id /><input type=submit value=Изменить товар></form>",
                                              dbId);

                    }
                    result += string.Format("</div></div><div class=product-description>{0}</div><div class=product-comments>", prod.Description);
                    int printed = 0;
                    if (prod.Comments != null)
                    {
                        string[] allcomments = prod.Comments.Split('a');
                        foreach (string comment in allcomments)
                        {
                            if (comment != "")
                            {
                                int CommentId = int.Parse(comment);
                                var q = from res in db.comments
                                        where res.Id == CommentId
                                        select res;
                                foreach (var comm in q)
                                {
                                    string ip = comm.Ip;
                                    bool banned = false;
                                    var q1 = from a in db.banlists
                                             where a.Ip == ip
                                             select a;
                                    foreach (var bans in q1)
                                    {
                                        banned = true;
                                    }
                                    if (banned == false)
                                    {
                                        result += string.Format("<div class=comment><div class=comment-name>{0}</div><div class=comment-time>{1}</div><div style='clear:both;'></div><p class=comment-text>{2}</p>",
                                                                comm.Name, comm.Time, comm.Text);
                                        if (Access() == 1 || Access() == 2)
                                        {
                                            result += string.Format("<form action=actions/admin/delcomment.aspx><input type=hidden value={0} name=Id /><input type=hidden value={1} name=Url /><input type=submit value='Удалить отзыв'></form><form action=actions/admin/banip.aspx><input type=hidden value={2} name=userIp /><input type=hidden value={1} name=Url /><input type=submit value='Заблокировать пользователя'></form>",
                                                                    comm.Id, Request.Url, comm.Ip);
                                        }

                                        result += "</div>";
                                        printed++;
                                    }
                                }
                            }
                        }
                    }
                    if (printed == 0)
                    {
                        result += "<div style='font-size:16px; text-align:left; padding-left:20px;'>У даного товара нет отзывов.</div>";
                    }
                    result += string.Format("</div><form action=actions/addcomment.aspx id=add-comment><input type=hidden value={0} name=productID /><input type=hidden value={1} name=Url /><div align=left>Введите имя:<br /><input type=text id=add-comment-name name=name required/><br />Ваш отзыв:<br /><textarea id=add-comment-text name=text required></textarea><br /><input type=submit value='Оставить отзыв' id=add-comment-submit/></div></form>",
                                            dbId, Request.Url);
                    int err;
                    if (Request.QueryString["err"] != null)
                    {
                        err = int.Parse(Request.QueryString["err"]);
                        switch (err)
                        {
                            case 0: { break; }
                            case 1:
                                {
                                    result += "<div style='font-size:20px; width:600px; text-align:left; color:red; padding-top:20px;'>Чтото пошло не так. Проверьте корректность введенных данных.<br />Данные не должны содержать кавычек и символов < > = ; <br /></div>";
                                    break;
                                }
                            case 2:
                                {
                                    result += "<div style='font-size:20px; width:600px; text-align:left; color:red; padding-top:20px;'>Вы заблокированы. Вы больше не можете оставлять отзывы на этом сайте</div>";
                                    break;
                                }
                            default:
                                {
                                    result += "<div style='font-size:20px; width:600px; text-align:left; color:red; padding-top:20px;'>Сервис отзывов временно недоступен</div>";
                                    break;
                                }
                        }
                    }
                }

            }
            return result;
        }
    }
}