using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "/home.aspx?search=";
            string searchText = Request.QueryString["search"];
            string[] temp = searchText.Split(' ');
            foreach (var word in temp)
            {
                url += word + "+";
            }
            url = url.Substring(0, (url.Length - 1));
            Response.Redirect(url);
        }
    }
}