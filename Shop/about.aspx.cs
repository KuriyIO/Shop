using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class about : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected string Content()
        {
            using (var db = new dbEntities())
            {
                var query = from b in db.contents
                            where b.name.Equals("about")
                            select b.content1;
                return query.First();
            }
        }
    }
}