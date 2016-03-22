using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions
{
    public partial class clearcart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearCart();
            Response.Redirect("~/cart.aspx");
        }
        protected void ClearCart()
        {
            HttpCookie CartCookie = Request.Cookies["cart"];
            if (CartCookie != null)
                CartCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(CartCookie);
        }
    }
}