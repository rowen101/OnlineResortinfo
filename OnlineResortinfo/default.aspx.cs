using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo
{
    public partial class _default1 : System.Web.UI.Page
    {
        public void setcookie()
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];//Declaration cookies

            if (cookie == null)//if cookie is emty
            {
                Response.Redirect("pages/home.aspx");//redirect to home
            }
            else if (cookie["usertype"] == "3")//if cookie admin
            {
                Response.Redirect("pages/admin.aspx");//redirect to  admin
            }
            else if (cookie["usertype"] == "4")//if cookie guest
            {
                Response.Redirect("pages/home.aspx");//redirect to home
            }

            else
            {
                Response.Redirect("pages/home.aspx");//redirect to home
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                setcookie();
            }
            catch
            {

            }
        }
    }
}