using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Update();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Update()
        {
            int i = 0;
            string[] description = new string[] { "", "", "", "", "", "" };
            description[0] = Request.Unvalidated.Form.GetValues("1")[0];
            description[1] = Request.Unvalidated.Form.GetValues("2")[0];
            description[2] = Request.Unvalidated.Form.GetValues("3")[0];
            description[3] = Request.Unvalidated.Form.GetValues("4")[0];
            description[4] = Request.Unvalidated.Form.GetValues("5")[0];
            description[5] = Request.Unvalidated.Form.GetValues("6")[0];
            using (var db = new dbEntities())
            {
                var q = from b in db.contents
                        select b;
                foreach (var cont in q)
                {
                    cont.content1 = description[i];
                    i++;
                }
                db.SaveChanges();
            }

        }
    }
}