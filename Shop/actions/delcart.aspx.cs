using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions
{
    public partial class delcart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DelItems();
            Response.Redirect("~/cart.aspx");
        }
        protected void DelItems()
        {
            string result ="";
            HttpCookie CartCookie = Request.Cookies["cart"];
            if (Request.QueryString["Flags[]"] != null)
            {
                if(CartCookie!=null)
                {
                    string value = CartCookie["cart"];
                    string[] blocks = value.Split('a');
                    foreach(var products in blocks)
                    {
                        if(products!="")
                        {
                            string[] NeedToDel = Request.QueryString["Flags[]"].Split(',');
                            bool temp = true;
                            foreach(var check in NeedToDel)
                            {
                                string[] info = products.Split('c');
                                if((info[0].Equals(check)))
                                {
                                    temp = false;
                                    break;
                                }
                            }
                            if(temp)
                                result += products + "a";
                        }
                    }
                    CartCookie["cart"] = result;
                    Response.Cookies.Add(CartCookie);
                }
            }
        }
    }
}