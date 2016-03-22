using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class Shop : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected string Logged()
        {
            HttpCookie UserCookie = Request.Cookies["user"];
            if(UserCookie != null)
            {
                return "<a href="+"actions/logout.aspx"+">Выход </a>";
            }
            else
            {
                return "<a href=" + "/login.aspx" + ">Вход </a>";
            }
        }
        protected string CartInfo()
        {
            HttpCookie CartCookie = Request.Cookies["cart"];
            if (CartCookie != null && CartCookie["cart"]!="")
            {
                int sum =0;
                int count = 0;
                string value = CartCookie["cart"];
                string[] blocks = value.Split('a');
                foreach(var products in blocks)
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
        protected string Categories()
        {
            string allCategories ="";
            using(var db = new dbEntities())
            {
                var query = from cat in db.categories
                            select cat;
                foreach(var category in query)
                {
                    allCategories += "<li><a href=" + string.Format("/home.aspx?cat={0}", category.Id) + string.Format(">{0}</a></li>", category.CategoryName);
                }
            }
            return allCategories;
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
    }
}