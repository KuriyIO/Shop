using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace Shop.actions.admin
{
    public partial class banip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(ban());
        }
        protected string ban()
        {
            string Url = Request.QueryString["Url"];
            string Ip = Request.QueryString["userIp"];
            int Id = 1;
            using(var db = new dbEntities())
            {
                var q = from b in db.banlists
                        select b.Id;
                foreach(var id in q)
                {
                    Id++;
                }
                banlist ban = new banlist();
                ban.Ip = Ip;
                ban.Id = Id;
                ban.Status = 1;
                db.banlists.Add(ban);
                db.SaveChanges();
            }
            return Url;
        }
    }
}