using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace online_adds
{
    public partial class _Default : System.Web.UI.Page
    {
        public void setcookie()
        {
            HttpCookie cookie = Request.Cookies["rowenref"];//Declaration cookies

            if (cookie == null)//if cookie is emty
            {
                Response.Redirect("pages/home.aspx");//redirect to home
            }
            else if (cookie["usertype"] == "3")//if cookie admin
            {
                Response.Redirect("pages/home.aspx");//redirect to  admin
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
