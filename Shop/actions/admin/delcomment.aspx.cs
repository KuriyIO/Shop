using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class delcomment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(Del());
        }
        protected string Del()
        {
            int Id = int.Parse(Request.QueryString["Id"]);
            string Url = Request.QueryString["Url"];
            using(var db = new dbEntities())
            {
                var query = from b in db.comments
                            where b.Id == Id
                            select b;
                foreach(var del in query)
                {
                    db.comments.Remove(del);
                }
                db.SaveChanges();
            }
            return Url;
        }
    }
}