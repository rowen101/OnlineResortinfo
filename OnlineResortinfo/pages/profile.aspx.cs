using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo.pages
{
    public partial class profile : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
        clssecurity encryptype = new clssecurity();

        public void profileview()
        {
           
                 HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
                 if (cookie == null)
                 {
                     Response.Redirect("home.aspx");
                 }
                 else
                 {
                     user User = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                     imgprofile.ImageUrl = "../images/profilepic/" + User.image;
                     txtusername.Text = User.username;
                     txtfname.Text = User.fname;
                     txtlname.Text = User.lname;
                     txtemail.Text = User.email;
                     txtwebsite.Text = User.website;
                     var sption = from s in db.soptions
                                  where s.disc == "gender"
                                  orderby s.name descending
                                  select s.name;

                     if (sption != null)
                     {
                         dropgender.DataSource = sption.Distinct().ToList();
                         dropgender.DataBind();
                     }
                     
                     soption option = db.soptions.First(use => use.id == User.gender);

                     dropgender.Text = option.name;
                    
                 }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 profileview();
            }
           
        }

        protected void btnupdateprofile_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
            var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//date
            user User = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));

            if (dropgender.Text == "Male")
            {
                if (txtpass1.Text == "" || txtpass2.Text == "")
                {
                    db.sp_UPDATE_tbl_user(Convert.ToInt16(cookie["userid"]), User.password, txtfname.Text, txtlname.Text, txtemail.Text, 1, txtwebsite.Text, Convert.ToDateTime(d));
                    Response.Redirect("profile.aspx");
                    Label1.Text = "<div style='color:blue;'>"+txtusername.Text+" successfully update</div>";
                }
                else
                {
                    db.sp_UPDATE_tbl_user(Convert.ToInt16(cookie["userid"]), encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemail.Text, 1, txtwebsite.Text, Convert.ToDateTime(d));
                    Response.Redirect("profile.aspx");
                    Label1.Text = "<div style='color:blue;'>" + txtusername.Text + " successfully update</div>";
                }
            }
            else
            {
                if (txtpass1.Text == "" || txtpass2.Text == "")
                {
                    db.sp_UPDATE_tbl_user(Convert.ToInt16(cookie["userid"]), User.password, txtfname.Text, txtlname.Text, txtemail.Text, 2, txtwebsite.Text, Convert.ToDateTime(d));
                    Response.Redirect("profile.aspx");
                    Label1.Text = "<div style='color:blue;'>" + txtusername.Text + " successfully update</div>";
                }
                else
                {
                    db.sp_UPDATE_tbl_user(Convert.ToInt16(cookie["userid"]), encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemail.Text, 2, txtwebsite.Text, Convert.ToDateTime(d));
                    Response.Redirect("profile.aspx");
                    Label1.Text = "<div style='color:blue;'>" + txtusername.Text + " successfully update</div>";
                }
            }
                
          


            //db.sp_UPDATE_tbl_user(Convert.ToInt16(Label1.Text), encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemail.Text, 1, txtwebsite.Text, Convert.ToDateTime(d));
            //if (dropgender.Text == "Male")
            //{
            //    db.sp_UPDATE_tbl_user(Convert.ToInt16(usrid), encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemail.Text, 1, txtwebsite.Text, Convert.ToDateTime(d));

            //}
            //else if (dropgender.Text == "Female")
            //{

            //    db.sp_UPDATE_tbl_user(Convert.ToInt16(usrid), encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemail.Text, 2, txtwebsite.Text, Convert.ToDateTime(d));

            //}

        }
       
    }
}