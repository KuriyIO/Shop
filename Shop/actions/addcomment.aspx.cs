using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop.actions
{
    public partial class addcomment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(AddComment());
        }
        protected bool CheckBanlist(string ip)
        {
            using(var db = new dbEntities())
            {
                bool banned = false;
                var query = from b in db.banlists
                            where b.Ip==ip
                            select b;
                foreach(var ban in query)
                {
                    banned=true;
                }
                return banned;
            }
        }
        protected string AddComment()
        {
            bool InfoExist = true;
            int prodId=0;
            string Url="";
            string name="";
            string text="";
            string Ip = Request.UserHostAddress;
            DateTime date = new DateTime();
            date = DateTime.Now;
            string time = date.ToUniversalTime().ToString("u");
            time = time.Substring(0, time.Length - 1);
            if(Request.QueryString["productID"]!=null)
            {
                prodId=int.Parse(Request.QueryString["productID"]);
            }
            else
            {
                InfoExist=false;
            }
            if(Request.QueryString["Url"]!=null)
            {
                Url = Request.QueryString["Url"];
            }
            else
            {
                InfoExist=false;
            }
            if(Request.QueryString["name"]!=null)
            {
                name = Request.QueryString["name"];
            }
            else
            {
                InfoExist=false;
            }
            if(Request.QueryString["text"]!=null)
            {
                text = Request.QueryString["text"];
            }
            else
            {
                InfoExist=false;
            }
            if(InfoExist==false)
                return string.Format("~/product.aspx?id={0}&err=1", prodId+1274);
            if(!CheckBanlist(Ip))
            {
                using(var db = new dbEntities())
                {
                    //insert into comments
                    comment insert = new comment();
                    insert.Ip = Ip;
                    insert.Name = name;
                    insert.Text = text;
                    insert.Time = time;
                    db.comments.Add(insert);
                    db.SaveChanges();

                    int CommentId=0;

                    var getcommentid = from b in db.comments
                                       where (b.Time==time && b.Ip==Ip)
                                       select b.Id;
                    foreach(var id in getcommentid)
                    {
                        CommentId = id;
                    }
                    if(CommentId!=0)
                    {
                        var commentslist = from a in db.products
                                           where a.Id==prodId
                                           select a;
                        foreach(var list in commentslist)
                        {
                            string comments = list.Comments;
                            comments += string.Format("{0}a", CommentId);
                            list.Comments = comments;
                            db.SaveChanges();
                        }
                        return string.Format("~/product.aspx?id={0}&err=0", prodId + 1274);
                    }
                    else
                        return string.Format("~/product.aspx?id={0}&err=1", prodId + 1274);
                }
            }
            else
                return string.Format("~/product.aspx?id={0}&err=2", prodId + 1274);
        }
    }
}