using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class clearbanlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Clear();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Clear()
        {
            using(var db = new dbEntities())
            {
                var q = from b in db.banlists
                        select b;
                db.banlists.RemoveRange(q);
                db.SaveChanges();
            }
        }
    }
}