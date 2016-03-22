using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class AddToCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie CartCookie = Request.Cookies["cart"];
            int Id = int.Parse(Request.QueryString["Id"]);
            int Count = int.Parse(Request.QueryString["Count"]);
            string Url = Request.QueryString["Url"];
            string result = "";
            if (CartCookie != null && CartCookie["cart"]!="")
            {
                result = CartCookie["cart"];
                CartCookie["cart"] = result + string.Format("{0}c{1}a", Id, Count);
                Response.Cookies.Add(CartCookie);
            }
            else
            {
                HttpCookie Cookie = new HttpCookie("cart");
                Cookie["cart"] = string.Format("{0}c{1}a", Id, Count);
                Response.Cookies.Add(Cookie);
            }
                
            Response.Redirect(Url);
        }
    }
}