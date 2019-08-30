using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace online_adds.pages
{
    public partial class admin : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();


        public void tabview()
        {
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
            if (cookie == null)
            {
                Response.Redirect("home.aspx");
            }
            else if (cookie["perms"] == "3")
            {
                var mnu = from p in db.menus
                          where p.menu_type == 5
                          orderby p.menu_id ascending
                          select p;
                listadmintab.DataSource = mnu;
                listadmintab.DataBind();

                listidtab.DataSource = mnu;
                listidtab.DataBind();
                soption optiontab = db.soptions.First(use => use.id == 3);
                lbltab.Text = optiontab.name.ToString()+ " tab";
                lbltitle.Text = optiontab.name.ToString();
            }
            else if (cookie["perms"] == "5")
            {
                var mnu = from p in db.menus
                          where p.menu_perms == 1
                          orderby p.menu_id ascending
                          select p;
                listadmintab.DataSource = mnu;
                listadmintab.DataBind();

                listidtab.DataSource = mnu;
                listidtab.DataBind();

                soption optiontab = db.soptions.First(use => use.id == 5);
                lbltab.Text = optiontab.name.ToString() + " tab";
                lbltitle.Text = optiontab.name.ToString();
            }

           
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            tabview();

           
        }
    }
}