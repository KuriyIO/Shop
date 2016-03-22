using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class updateproduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Update();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void Update()
        {
            var inputs = Request.Form;
            int id = int.Parse(inputs.GetValues("id")[0]);
            int popularity = int.Parse(inputs.GetValues("popularity")[0]);
            int price = int.Parse(inputs.GetValues("price")[0]);
            string name = inputs.GetValues("name")[0];
            int category = int.Parse(inputs.GetValues("category")[0]);
            string description = Request.Unvalidated.Form.GetValues("description")[0];
            string image = inputs.GetValues("image")[0];
            using(var db = new dbEntities())
            {
                var q = from b in db.products
                        where b.Id==id
                        select b;
                foreach (var product in q)
                {
                    product.Popularity = popularity;
                    product.Price = price;
                    product.Name = name;
                    product.Category = category;
                    product.Description = description;
                    product.Image = image;
                    db.SaveChanges();
                }

            }

        }
    }
}