using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;

namespace Shop.actions
{
    public partial class addorder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (AddOrder())
                Response.Redirect("~/cart.aspx?err=0");
            else
                Response.Redirect("~/cart.aspx?err=1");
        }
        protected bool AddOrder()
        {
            bool result = true;
            int id = 1;
            HttpCookie CartCookie = Request.Cookies["cart"];
            string value = CartCookie["cart"];
            string contacts = Request.QueryString["contacts"];
            string adress = Request.QueryString["Adress"];
            string additional = Request.QueryString["additional"];
            using (var db = new dbEntities())
            {
                var q = from b in db.orders
                        select b;
                foreach (var a in q)
                {
                    id++;
                }
                order order = new order();
                if (value != null)
                   order.OrderProducts = value;
                else
                    result = false;
                if (contacts != null)
                   order.OrderContacts = contacts;
                else
                    result = false;
                if (adress != null)
                    order.OrderAdress = adress;
                else
                    result = false;
                if (additional != null)
                    order.OrderAdditional = additional;
                order.Id = id;
                    
                db.orders.Add(order);
                db.SaveChanges();
            }
            return result;
        }
    }
}