using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class catalog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected string CatalogContent()
        {
            string content = "";
            using (var db = new dbEntities())
            {
                var query = from cat in db.categories
                            select cat;
                foreach (var category in query)
                {			
                    content += string.Format("<div style='margin:15px 15px 15px 15px; float:left;'><div class=product-image><a href=home.aspx?cat={0}><img src=/images/categories/{1} height=180 width=180/></a></div><div class=product-name><a href=home.aspx?cat={0}>{2}</a></div></div>",
                                     category.Id, category.Image, category.CategoryName);
                }
            }
            return content;
        }
    }
}