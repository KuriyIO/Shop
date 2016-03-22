using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class updatecategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Update();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Update()
        {
            int id = int.Parse(Request.QueryString["id"]);
            string name = Request.QueryString["name"];
            string image = Request.QueryString["image"];
            using (var db = new dbEntities())
            {
                var q = from b in db.categories
                        where b.Id == id
                        select b;
                foreach (var cat in q)
                {
                    cat.Id = id;
                    cat.CategoryName=name;
                    cat.Image = image;
                    db.SaveChanges();
                }

            }

        }
    }
}