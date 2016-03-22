using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions
{
    public partial class log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Log();
        }
        protected void Log()
        {
             string login = Request.QueryString["login"];
             string pass = Request.QueryString["pass"];
             HttpCookie UserCookie = Request.Cookies["user"];
             using (var db = new dbEntities())
             {
                 var query = from b in db.users
                             where (b.Login == login && b.Password == pass)
                             select b.Access;
                 foreach(var res in query)
                 {
                     if (UserCookie != null)
                     {
                         UserCookie["user"] = res.ToString();
                         Response.Cookies.Add(UserCookie);
                     }
                     else
                     {
                         HttpCookie Cookie = new HttpCookie("user");
                         Cookie["user"] = res.ToString();
                         Response.Cookies.Add(Cookie);
                     }
                     Response.Redirect("~/login.aspx");
                 }
                 Response.Redirect("~/login.aspx?err=1");
             }
             Response.Redirect("~/login.aspx?err=2");
            
        }
    }
}