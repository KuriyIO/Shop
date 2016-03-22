using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ClearCookie();
            Response.Redirect("~/home.aspx");
        }
        protected void ClearCookie()
        {
            HttpCookie UserCookie = Request.Cookies["user"];
            if(UserCookie!=null)
                UserCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(UserCookie); 
        }
    }
}