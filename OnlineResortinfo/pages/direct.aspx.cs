using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo.pages
{
    public partial class direct : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
        clssecurity encryptype = new clssecurity();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {


                    sitename Ste = db.sitenames.First();
                    lbltitlesite.Text = Ste.title;
                    Page.Title = string.Format(Ste.title.ToString());

                    string direct = Request.QueryString["req"].ToString();
                    switch (direct)
                    {
                        case "login":
                            MultiView1.ActiveViewIndex = 0;
                            btnsubmit.Text = "Login";
                            break;

                        case "register":
                            MultiView1.ActiveViewIndex = 1;
                            btnsubmit.Text = "Register";
                            break;
                        case "errorlog":
                            string error = Request.QueryString["error"].ToString();
                            promt.Text = "<div class='last_message'>" + encryptype.psDescrypt(error) + "</div>";
                            MultiView1.ActiveViewIndex = 0;
                            btnsubmit.Text = "Login";
                            break;
                        case "success":
                            string success = Request.QueryString["ok"].ToString();
                            MultiView1.ActiveViewIndex = 0;
                            btnsubmit.Text = "Login";
                            txtuser.Text = success.ToString();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MultiView1.ActiveViewIndex = 5;
                }
            }
        }
        /// <summary>
        /// login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        /// <summary>
        /// login user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        public void setcookie()
        {
            if (db.f_usernameExist(txtuser.Text) == true)
            {


                HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
                HttpCookie pinding = Request.Cookies["onlineresortpinding"];
                user User = db.users.First(use => use.username == txtuser.Text);//select cookie in db.user
                if (cookie == null)//if cookie empty
                {
                    cookie = new HttpCookie("onlineresort");

                }
                if (txtuser.Text == "" || txtpass.Text == "")
                {
                    promt.Text = "<div class='last_message'>Username and Password must not be empty</div>";
                }
                else if (txtuser.Text == "")
                {
                    promt.Text = "<div class='last_message'>Username must not be empty</div>";
                }
                else if (txtpass.Text == "")
                {
                    promt.Text = "<div class='last_message'>Password must not be empty</div>";
                }
                else
                {




                    cookie["userid"] = Convert.ToString(User.id);//permission
                    cookie["actype"] = Convert.ToString(User.actype);//account type
                    cookie["perms"] = Convert.ToString(User.perms);//permision

                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    Response.Cookies.Add(cookie);

                }
                if (User.actype == 3)
                {

                    Response.Redirect("admin.aspx");
                }
                else if (User.actype == 4)
                {

                    Response.Redirect("home.aspx");

                }

            }
            else
            {
                promt.Text = "<div class='last_message'>Invalide Username!!!</div>";
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

                promt.Text = "<div class='last_message'>Invalide Username!!!</div>";
            }


        }
        /// <summary>
        /// button register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (btnsubmit.Text == "Login")//login
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

                        promt.Text = "<div class='last_message'>Invalide Username!!!</div>";
                    }

                    if (db.f_passwordExist(encryptype.psEncrypt(txtpass.Text)) == true)
                    {
                        setcookie();

                        Response.Cookies["userid"].Expires = DateTime.Now.AddDays(-1);
                    }
                    else
                    {
                        Response.Redirect("direct.aspx?req=errorlog&error=Uxi3Dgwwcyz7dbL0syoBAN/fyTNJBTJplMOm6rgkbAs=");
                    }
                }
            }
            else if (btnsubmit.Text == "Register")//register
            {
                if (db.f_usernameExist(txtregusername.Text) == true)
                {
                    promt.Text = "<div class='last_message'>Username Already Exist</div>";
                }
                else if (db.f_emailExist(txtemail.Text) == true)
                {
                    promt.Text = "<div class='last_message'>Email Already Exist</div>";
                }
                else
                {
                    if (txtregusername.Text == "" || txtregpassword.Text == "" || txtregccpassword.Text == "")
                    {
                        promt.Text = "<div class='last_message'>Empty Field not allowed </div>";
                    }
                    else
                    {
                        db.users.InsertOnSubmit(new user
                        {
                            username = txtregusername.Text,
                            password = encryptype.psEncrypt(txtregccpassword.Text),
                            email = txtemail.Text,
                            actype = 3,
                            perms = 5,
                            gender = 1,
                            image = "default.png"
                        });
                        db.SubmitChanges();
                        Response.Redirect("direct.aspx?req=success&ok=" + txtregusername.Text);
                    }

                }
          

            }
            else
            {
            }
        }
    }
}