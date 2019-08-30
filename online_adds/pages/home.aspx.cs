using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
namespace online_adds.pages
{
    public partial class home : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        clssecurity encryptype = new clssecurity();
        public void set()
        {
            carousel();
            //site name
            sitename Sitnme = db.sitenames.First();
            lbl_sitename.Text =  Sitnme.title.ToString() ;//call sitename
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
                ListView1.DataSource = mnu;
                ListView1.DataBind();



                var mnuvisible = from p in db.menus
                          where p.status == 15
                          orderby p.menu_id ascending
                          select p;
                ListView1.DataSource = mnuvisible;
                ListView1.DataBind();

                lnklogin.Visible = true;
                lblSignUp.Visible = true;

            
            }
            else
            {
                //Image1.Visible = true;
                cookie.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Add(cookie);
                user User = db.users.First(aa=>aa.id == Convert.ToInt16(cookie["userid"]));
                lbluser.Text = "<li class='dropdown'><a href='#' class='dropdown-toggle' data-toggle='dropdown'>"
                     + "Hi " + User.username
                     + "<b class='caret'></b></a>"
                     + "<ul class='dropdown-menu'>"
                     + "<li><a href='account.aspx'>My Account</a></li>"
                     + "<li><a href='logout.aspx'>Logout</a></li>"
                     + "</ul></li>";
               // Image1.ImageUrl = ("../profilepic/thumb/" + cookie["profilepic"]);
               // lnk_logout.Visible = true;
                lnklogin.Visible = false;
                lblSignUp.Visible = false;
               // lnkregister.Visible = false;
                //acount type "guest"
                if (cookie["usertype"] == "4")
                {

                 

                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    ListView1.DataSource = mnuvisible;
                    ListView1.DataBind();


                }
                //acount type "admin"
                else if (cookie["perms"] == "3")
                {

                   

                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    ListView1.DataSource = mnuvisible;
                    ListView1.DataBind();
                   lbladmin.Visible = true;
                   soption optiontab = db.soptions.First(use => use.id == 3);

                    lbladmin.Text = "<li><a href='admin.aspx'>"+optiontab.name+"</a></li>";
                }
                else if (cookie["perms"] == "5")
                {
                  

                    var mnuvisible = from p in db.menus
                                     where p.status == 15
                                     orderby p.menu_id ascending
                                     select p;
                    ListView1.DataSource = mnuvisible;
                    ListView1.DataBind();
                    lbladmin.Visible = true;
                    soption optiontab = db.soptions.First(use => use.id == 5);

                    lbladmin.Text = "<li><a href='admin.aspx'>" + optiontab.name + "</a></li>";
                }



            }

        }
        /// <summary>
        /// banner
        /// </summary>
        public void carousel()
        {
            var carousellist = from p in db.homebanners
                               where p.status == 1
                               select p;
            ListcarouselInd.DataSource = carousellist;
            ListcarouselInd.DataBind();

            ListWrapper.DataSource = carousellist;
            ListWrapper.DataBind();

            Imagebannerdef.ImageUrl = "../../images/banner/P1110148.jpg";

        
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                lblyr.Text = DateTime.Now.ToString("yyyy");
                set();

                var pst = from p in db.posts
                          where p.status == "Publish"

                          select p;
                Listthumbs.DataSource = pst;
                Listthumbs.DataBind();

            }
        }

        //protected void lnklogin_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("req.aspx?pg=login");
        //}
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
        protected void LoginBtn_Click(object sender, EventArgs e)
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

        protected void lblSignUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("req.aspx?pg=reguser");
        }

      
    }
}
