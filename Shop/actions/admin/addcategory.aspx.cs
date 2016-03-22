using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class addcategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Add();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Add()
        {
            using(var db  = new dbEntities())
            {
                int id = 1;
                string categoryname = Request.Form.GetValues("name")[0];
                HttpPostedFile image = Request.Files["image"];
                string fullname = image.FileName;
                string[] blocks = fullname.Split('.');            
            
                var q = from b in db.categories
                        select b;
                foreach(var elements in q)
                {
                    id++;
                }

                string name = string.Format("{0}.{1}", id, blocks[blocks.Length - 1]);

                category cat = new category();
                cat.Id = id;
                cat.Image = name;
                cat.CategoryName = categoryname;
                db.categories.Add(cat);
                db.SaveChanges();
            
                
                string loc = Server.MapPath("~/images") + "\\categories\\" + name;
                image.SaveAs(loc);
            }

        }
    }
}