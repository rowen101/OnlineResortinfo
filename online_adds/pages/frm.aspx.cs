using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace online_adds.pages
{
    
    public partial class frm1 : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        public void blogss()
        {
            var category = from pst in db.soptions
                           where pst.disc == "category"
                           orderby pst.name ascending
                           select pst;
            Listcategory.DataSource = category;
            Listcategory.DataBind();

            lblblog.Text = "Category";
              string frm = Request.QueryString["frm"].ToString();
              switch (frm)
              {
                  case "req":
                      string dis = Request.QueryString["retrive"].ToString();
                     
                      var viewcategory = db.sp_search_categorypost(dis.ToString());

                      ListView_blog.DataSource = viewcategory;
                      ListView_blog.DataBind();
                      break;
              }
           
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (txtsearchblg.Text == "")
            {
            }
            else
            {
                Response.Redirect("Destination.aspx?frm=srh&cn=" + txtsearchblg.Text);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            blogss();


        }

        protected void lnkhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

       
      

       
    }
}