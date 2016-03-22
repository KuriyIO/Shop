using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Shop
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.DataBind();
        }
        protected string ContentNavigation()
        {
            string result = "";
            int page_count = 0;
            using(var db = new dbEntities())
            {
                int query;
                if(Request.QueryString["cat"] == null)
                    query = db.products.Count();
                else
                {
                    int category = int.Parse(Request.QueryString["cat"]);
                    query = db.products.Where(b => b.Category == category).Count();
                }
                page_count = (int)decimal.Ceiling((decimal)query / 12);
            }
            int cat=0, page=1;
	        result += "<table><tr><td align=left width=175px>";
            if (Request.QueryString["page"] == null)
                page = 1;
            else
                page = int.Parse(Request.QueryString["page"]);
            if (Request.QueryString["cat"] == null)
                {
                    cat=0;
                }
            else
                {
                    cat=int.Parse(Request.QueryString["cat"]);
                }
            int temp = page;
            if(page>1)
                temp = page - 1;
		    result += string.Format("<div class=navigation-left><a href=/home.aspx?cat={0}&page={1}", cat, temp); 
            result +=">Предыдущая страница</a></div></td><td align=center width=250px><div class=navigation-center>";	
            if(page==page_count-1 && page_count>4)
                temp=page-3;
            else if(page==page_count && page_count>4)
                temp=page-4;
            else if(page==4 && page_count==4)
                temp=page-3;
            else if(page>3)
                temp=page-2;
            else temp=1;

            result += string.Format("<a href=/home.aspx?cat={0}&page={1}", cat, temp);
			if (page==1) result += " class=current";

            result+=string.Format(">[{0}]</a>", temp);

            if (page_count > 1)
            {

                if (page == page_count - 1 && page_count > 4) temp=page - 2;
                else if (page == page_count && page_count > 4) temp=page - 3;
                else if (page == 4 && page_count == 4) temp=page - 2;
                else if (page > 3) temp=page - 1; else temp = 2;
                result += string.Format("<a href=/home.aspx?cat={0}&page={1}", cat, temp);

                if (page == 2) result += " class=current";

                result += string.Format(">[{0}]</a>", temp);
            }

				if(page_count>2)
                {
                    if(page==page_count-1 && page_count>4) temp=page-1; 
				    else if(page==page_count && page_count>4) temp=page-2; 
					     else if(page==4 && page_count==4) temp=page-1;
				 		      else if(page>3) temp=page;
					 	  	       else temp = 3; 
                    result+= string.Format("<a href=home.aspx?cat={0}&page={1}", cat, temp);

					if(page_count==3 && page==3) result+= " class=current";
					else if(page_count==4 && page==3) result+= " class=current";
						 else if(page_count==5 && page==3) result+= " class=current";
						 	  else if(page_count>5 && page<page_count-1 && page>2) result+= " class=current";

                    result+=string.Format(">[{0}]</a>", temp);
                }
				
				if(page_count>3)
                {
                    if(page==page_count-1 && page_count>4) temp=page; 
				    else if(page==page_count && page_count>4) temp=page-1; 
					     else if(page==4 && page_count==4) temp=page; 
				 		      else if(page>3) temp=page+1; 
					 	  	       else temp=4; 
                    result+= string.Format("<a href=home.aspx?cat={0}&page={1}", cat, temp);
				 
					if(page_count<6 && page==4) result+= " class=current";
					else if(page==page_count-1 && page_count!=4) result+= " class=current";
                    
                    result+=string.Format(">[{0}]</a>", temp);
                }
				
				if(page_count>4)
                {
                    if(page==page_count-1 && page_count>4) temp=page+1; else if(page==page_count && page_count>4) temp=page; else if(page>3) temp=page+2; else temp=5;
                    result+= string.Format("<a href=home.aspx?cat={0}&page={1}", cat, temp);
				 
					if(page==page_count) result+= " class=current";

                    result+=string.Format(">[{0}]</a>", temp);
                }
            result+= "</div></td> <td align=right width=175px><div class=navigation-right>";
            if(page==page_count) temp=page; else temp=page+1;
            result+= string.Format("<a href=home.aspx?cat={0}&page={1}",cat,temp);  
            result += ">Следующая страница</a></div></td></tr></table>";
            return result;
	
        }
        protected string CurrentUrl
        {
            get { return Convert.ToString(Request.Url); }
        }
        protected string SearchContent(string SearchText)
        {
            string[] temp = SearchText.Split('+');
            SearchText = "";
            foreach(var word in temp)
            {
                SearchText += word + " ";
            }
            SearchText = SearchText.Substring(0, (SearchText.Length - 1));

            string result = "";
            using(var db = new dbEntities())
            {
                var query = from b in db.products
                            where b.Name.ToLower().Contains(SearchText.ToLower())
                            select b;
                foreach(var product in query)
                {
                    result += string.Format("<div class=product-container><div class=product-image><a href=/product.aspx?id={0}><img src=/images/products/{1} height=180 width=180/></a></div><div class=product-name><a href=/product.aspx?id={2}>{3}</a></div><div class=product-price>{4} грн</div><form class=add-to-cart action=actions/AddToCart.aspx id=ToCartForm ><input type=hidden Value={5} name=Id /><input type=hidden Value=1 name=Count /><input type=hidden Value={6} name=Url /><input type=submit value=" + "'В корзину'" + "class=submit /></form></div>",
                        (product.Id + 1274), product.Image, (product.Id + 1274), product.Name, product.Price, product.Id, CurrentUrl);
                }
            }
            if (result == "")
                result = "<div style='font-size:20px; text-align:center; padding-top:20px;'>По вашему запросу ничего не найдено!</div>";
            return result;
        }
        protected string ProductsContent()
        {
            using (var db = new dbEntities())
            {
                int page, cat;
                string result = "";
                if (Request.QueryString["page"] == null)
                    page = 1;
                else
                    page = int.Parse(Request.QueryString["page"]);
                if (Request.QueryString["cat"] == null || Request.QueryString["cat"]=="0")
                {
                    var query = (from b in db.products
                                 select b).OrderByDescending(b => b.Popularity).Skip((page * 12)-12).Take(12);
                    foreach (var product in query)
                    {
                        result += string.Format("<div class=product-container><div class=product-image><a href=/product.aspx?id={0}><img src=/images/products/{1} height=180 width=180/></a></div><div class=product-name><a href=/product.aspx?id={2}>{3}</a></div><div class=product-price>{4} грн</div><form class=add-to-cart action=actions/AddToCart.aspx id=ToCartForm ><input type=hidden Value={5} name=Id /><input type=hidden Value=1 name=Count /><input type=hidden Value={6} name=Url /><input type=submit value=" + "'В корзину'" + "class=submit /></form></div>",
                        (product.Id + 1274), product.Image, (product.Id + 1274), product.Name, product.Price, product.Id, CurrentUrl);
                    }
                }
                else
                {
                    cat = int.Parse(Request.QueryString["cat"]);
                    var query = (from b in db.products
                                 where b.Category == cat
                                 select b).OrderByDescending(b => b.Popularity).Skip((page * 12) - 12).Take(12);
                    foreach (var product in query)
                    {
                        result += string.Format("<div class=product-container><div class=product-image><a href=/product.aspx?id={0}><img src=/images/products/{1} height=180 width=180/></a></div><div class=product-name><a href=/product.aspx?id={2}>{3}</a></div><div class=product-price>{4} грн</div><form class=add-to-cart action=actions/AddToCart.aspx id=ToCartForm ><input type=hidden Value={5} name=Id /><input type=hidden Value=1 name=Count /><input type=hidden Value={6} name=Url /><input type=submit value=" + "'В корзину'" + "class=submit /></form></div>",
                        (product.Id + 1274), product.Image, (product.Id + 1274), product.Name, product.Price, product.Id, CurrentUrl);
                    }
                }
                return result;
            }      
        }
        protected string Content()
        {
            if (Request.QueryString["search"] != null)
                return SearchContent(Request.QueryString["search"]);
            else
            {
                string res = "<div class=content-navigation>" + ContentNavigation() + "</div>";
                res += ProductsContent();
                res += "<div class=content-navigation-bot>" + ContentNavigation() + "</div>";
                return res;
            }
                
        }
    }
}