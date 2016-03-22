using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class addproduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Add();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Add()
        {
            using (var db = new dbEntities())
            {
                int id = 1;
                int countname = 1;
                var inputs = Request.Form;
                int popularity = int.Parse(inputs.GetValues("popularity")[0]);
                int price = int.Parse(inputs.GetValues("price")[0]);
                string productname = inputs.GetValues("name")[0];
                int category = int.Parse(inputs.GetValues("category")[0]);
                string description = Request.Unvalidated.Form.GetValues("description")[0];

                HttpPostedFile image = Request.Files["image"];
                string fullname = image.FileName;
                string[] blocks = fullname.Split('.');

                var q = from b in db.products
                        select b;
                id = q.Count()+1;

                var q2 = from b in db.products
                         where b.Category==category
                         select b;
                countname = q2.Count() + 1;

                string name = string.Format("{0}-{1}.{2}", category, countname, blocks[blocks.Length - 1]);

                product prod = new product();

                prod.Id = id;
                prod.Image = name;
                prod.Name = productname;
                prod.Popularity = popularity;
                prod.Category = category;
                prod.Price = price;
                prod.Description = description;
                prod.Comments = "";

                db.products.Add(prod);
                db.SaveChanges();


                string loc = Server.MapPath("~/images") + "\\products\\" + name;
                image.SaveAs(loc);
            }

        }
    }
}