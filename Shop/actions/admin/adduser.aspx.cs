using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions.admin
{
    public partial class adduser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AddUser();
            Response.Redirect("~/admin.aspx?res=1");
        }
        protected void AddUser()
        {
            int id = 1;
            string login = Request.QueryString["login"];
            string pass = Request.QueryString["pass"];
            int access = int.Parse(Request.QueryString["access"]);
            using (var db = new dbEntities())
            {
                var q = from b in db.users
                        select b;
                foreach (var a in q)
                {
                    id++;
                }
                user newuser = new user();
                newuser.Id = id;
                newuser.Login = login;
                newuser.Password = pass;
                newuser.Access = access;
                db.users.Add(newuser);
                db.SaveChanges();
            }

        }
    }
}