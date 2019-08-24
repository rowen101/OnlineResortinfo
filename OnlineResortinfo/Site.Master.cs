using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo
{
    public partial class Site : System.Web.UI.MasterPage
    {
        databaselinqDataContext db = new databaselinqDataContext();
        clssecurity encryptype = new clssecurity();

        public void set()
        {
            sitename Sitename = db.sitenames.First();
            Page.Title = string.Format(Sitename.title.ToString());
            lbltitle.Text = Sitename.title;
            lblfooter.Text = DateTime.Now.ToString("yyyy") + " " + Sitename.title;

            HttpCookie cookie = Request.Cookies["onlineresort"];// declaration cookie

            var post = db.sp_select_view_post();
            Listlatestpost.DataSource = post;
            Listlatestpost.DataBind();


            //top post
            var newpost = db.sp_newpost();
            listoppost.DataSource = newpost;
            listoppost.DataBind();

            if (cookie == null)//if cookie null
            {

                var mnuvisible = from p in db.menus
                                 where p.status == 15
                                 orderby p.menu_id ascending
                                 select p;
                listminimenu.DataSource = mnuvisible;
                listminimenu.DataBind();

                listmenunav.DataSource = mnuvisible;
                listmenunav.DataBind();

                Loginlnk.Visible = true;
                lblSignUp.Visible = true;


            }
            else
            {
                //Image1.Visible = true;
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
                user User = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                lbluser.Text = "<a href='profile.aspx'>Hi " + User.username + "</a> <a href='logout.aspx'>Logout</a>";
                // Image1.ImageUrl = ("../profilepic/thumb/" + cookie["profilepic"]);
                // lnk_logout.Visible = true;
                Loginlnk.Visible = false;
                lblSignUp.Visible = false;
                // lnkregister.Visible = false;
                //acount type "guest"
                if (User.actype == 4)
                {



                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listminimenu.DataSource = mnuvisible;
                    listminimenu.DataBind();

                    listmenunav.DataSource = mnuvisible;
                    listmenunav.DataBind();

                }
                //acount type "admin"
                else if (User.actype == 3)
                {

                    sitename Stename = db.sitenames.First();

                    lbladminbar.Text = "<div id='cssmenu'><ul>"
                        + "<li><a href='home.aspx'><span>" + Stename.title + "</span></a></li>"
                        + "<li><a href='admin.aspx'><span>Admin</span></a></li>"
                        + "<li class='has-sub'><a href='#'><span>New</span></a>"
                        + "<ul>"
                        + "<li><a href='admin.aspx?pg=createpost'><span>Post</span></a></li>"
                        + "<li class='last'><a href='admin.aspx?pg=media'><span>Media</span></a></li>"

                        + "</ul>"
                        + "</li>"

                        + "</ul></div>";

                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listminimenu.DataSource = mnuvisible;
                    listminimenu.DataBind();

                    listmenunav.DataSource = mnuvisible;
                    listmenunav.DataBind();
                }
                else
                {


                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listminimenu.DataSource = mnuvisible;
                    listminimenu.DataBind();

                    listmenunav.DataSource = mnuvisible;
                    listmenunav.DataBind();
                }



            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                set();
            }
        }

        protected void Loginlnk_Click(object sender, EventArgs e)
        {
            Response.Redirect("direct.aspx?req=login");
        }

        protected void lblSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("direct.aspx?req=register");
        }
        protected void lnksearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("fr.aspx?res=srh&cn=" + txtsearch.Text);
        }
      
    }
}