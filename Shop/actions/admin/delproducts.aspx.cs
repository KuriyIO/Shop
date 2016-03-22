using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class delproducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(Del());
        }
        protected string Del()
        {
            string flags = Request.QueryString["Flags[]"];
            string Url = Request.QueryString["Url"];
            if (flags != null)
            {
                string[] Ids = flags.Split(',');
                using (var db = new dbEntities())
                {
                    foreach (var Id in Ids)
                    {
                        int id = int.Parse(Id);
                        var query = from b in db.products
                                    where b.Id == id
                                    select b;
                        foreach (var del in query)
                        {
                            db.products.Remove(del);
                        }
                        db.SaveChanges();
                    }
                }
            }
            return Url;
        }
    }
}