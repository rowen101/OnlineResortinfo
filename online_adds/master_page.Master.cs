using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace online_adds
{
    public partial class master_page : System.Web.UI.MasterPage
    {
        databaseDataContext db = new databaseDataContext();
        clssecurity encryptype = new clssecurity();
        public void show()
        {
            int ID = 4;
            soption options = db.soptions.First(use => use.id == ID);//call soption db
            var mnu = from p in db.menus
                      where p.menu_type == ID
                      orderby p.menu_id ascending
                      select p;
            listmenu.DataSource = mnu;
            listmenu.DataBind();
        }

        /// <summary>
        /// caregory
        /// </summary>
        public void category()//show category
        {
            var query = from ctg in db.soptions
                        where ctg.disc == "category"
                        orderby ctg.name ascending
                        select ctg;
            //listcategory.DataSource = query;
            //listcategory.DataBind();
        }
        public void set()
        {

            //site name
            sitename Sitnme = db.sitenames.First();
            lbl_sitename.Text =  Sitnme.title.ToString();//call sitename
            lblfootertitle.Text = Sitnme.title.ToString();
            Page.Title = string.Format(Sitnme.title.ToString());
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie





            if (cookie == null)//if cookie null
            {
                int ID = 4;
                soption options = db.soptions.First(use => use.id == ID);//call soption db
                var mnu = from p in db.menus
                          where p.menu_type == ID
                          orderby p.menu_id ascending
                          select p;
                listmenu.DataSource = mnu;
                listmenu.DataBind();

                var mnuvisible = from p in db.menus
                                 where p.status == 15
                                 orderby p.menu_id ascending
                                 select p;
                listmenu.DataSource = mnuvisible;
                listmenu.DataBind();
                lnklogin.Visible = true;
                lblSignUp.Visible = true;
            }
            else
            {
            
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);

                user User = db.users.First(aa => aa.id == Convert.ToInt16(cookie["userid"]));
                lbluser.Text = "<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>"
                     + "Hi " + User.username
                    + "<b class='caret'></b></a>"
                    + "<ul class='dropdown-menu'>"
                    + "<li><a href='account.aspx'>My Account</a></li>"
                    + "<li><a href='logout.aspx'>Logout</a></li>"
                    + "</ul></li>";
             
                lnklogin.Visible = false;
                lblSignUp.Visible = false;
                if (cookie["usertype"] == "4")
                {

                  

                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listmenu.DataSource = mnuvisible;
                    listmenu.DataBind();

                }
                //acount type "admin"
                else if (cookie["perms"] == "3")
                {



                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listmenu.DataSource = mnuvisible;
                    listmenu.DataBind();
                    lbladmin.Visible = true;
                    soption optiontab = db.soptions.First(use => use.id == 3);

                    lbladmin.Text = "<li><a href='admin.aspx'>" + optiontab.name + "</a></li>";
                }
                else if (cookie["perms"] == "5")
                {


                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    listmenu.DataSource = mnuvisible;
                    listmenu.DataBind();
                    lbladmin.Visible = true;
                    soption optiontab = db.soptions.First(use => use.id == 5);

                    lbladmin.Text = "<li><a href='admin.aspx'>" + optiontab.name + "</a></li>";
                }



            }

        }
        /// <summary>
        /// Backgroud themes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void themes()
        {
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
            themesm Themes_master = db.themesms.First(use => use.id == 1);
            string themes_details = Themes_master.thms_id.ToString();

            thmesd thd = db.thmesds.First(use => use.thms_id == Convert.ToInt16(themes_details));

          

        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                lblyr.Text = DateTime.Now.ToString("yyyy");
                this.set();
            }
            this.themes();
        }

        protected void lnk_logout_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("rowenref");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Response.Redirect("home.aspx");
        }

      

        protected void lnkregister_Click(object sender, EventArgs e)
        {
            Response.Redirect("register.aspx");
        }

        protected void lnklogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("req.aspx?pg=login");
        }

       
        /// <summary>
        /// login user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void setcookie()
        {
            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            HttpCookie pinding = Request.Cookies["rowenpinding"];
            if (cookie == null)//if cookie empty
            {
                cookie = new HttpCookie("rowenref");

            }
            if (txtuser.Text == "" || txtpass.Text == "")
            {
                promt.Text = "<div class='error'>Username and Password must not be empty</div>";
            }
            else if (txtuser.Text == "")
            {
                promt.Text = "<div class='error'>Username must not be empty</div>";
            }
            else if (txtpass.Text == "")
            {
                promt.Text = "<div class='error'>Password must not be empty</div>";
            }
            else
            {

                if (db.f_usernameExist(txtuser.Text) == true)
                {
                    user User = db.users.First(use => use.username == txtuser.Text);//select cookie in db.user
                    string name = User.username;

                    cookie["userid"] = Convert.ToString(User.id);//userind
                    cookie["username"] = txtuser.Text;//username 
                    cookie["fullname"] = User.fname;//fname
                    cookie["usertype"] = User.actype.ToString();//acount type
                    cookie["userpassword"] = User.password;//password
                    cookie["perms"] = User.perms.ToString();//permesion

                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    Response.Redirect("req.aspx?pg=errorlog&error=Uxi3Dgwwcyz7dbL0syoBAN/fyTNJBTJplMOm6rgkbAs=");
                }

            }
            if (cookie["usertype"] == "3")
            {
                Response.Redirect("admin.aspx");
            }
            else if (cookie["usertype"] == "4")
            {
                if (pinding == null)
                {
                    Response.Redirect("home.aspx");
                }
                else
                {
                    string blogid = pinding["Jc07M5Ieg78"].ToString();
                    Response.Redirect("Destination.aspx?frm=read&id=" + blogid);

                }
            }



        }

        //login
        public void logins()
        {

            if (db.f_login(txtuser.Text, encryptype.psEncrypt(txtpass.Text)) == true)
            {
                setcookie();
            }
            else
            {

                promt.Text = "<div class='error'>Invalide Username!!!</div>";
            }


        }
       

        protected void lblSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("req.aspx?pg=reguser");
        }

     

        protected void Button1_Click(object sender, EventArgs e)
        {
            //logins();
            if (chkRememberMe.Checked)
            {
                logins();
            }
            else
            {
                if (db.f_login(txtuser.Text, encryptype.psEncrypt(txtpass.Text)) == true)
                {
                    setcookie();
                }
                else
                {

                    promt.Text = "<div class='error'>Invalide Username!!!</div>";
                }

                if (db.f_passwordExist(encryptype.psEncrypt(txtpass.Text)) == true)
                {
                    setcookie();

                    Response.Cookies["username"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["userpassword"].Expires = DateTime.Now.AddDays(-1);
                }
                else
                {
                    Response.Redirect("req.aspx?pg=errorlog&error=Uxi3Dgwwcyz7dbL0syoBAN/fyTNJBTJplMOm6rgkbAs=");
                }
            }
        }
      
       
    }
}
