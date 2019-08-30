using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;

namespace online_adds.pages
{
    public partial class signup : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        clssecurity encryptype = new clssecurity();
        //for resizeng image
        public bool ResizeImageAndUpload(System.IO.FileStream newFile, string folderPathAndFilenameNoExtension, double maxHeight, double maxWidth)
        {

            try
            {

                // Declare variable for the conversion
                float ratio;

                // Create variable to hold the image
                System.Drawing.Image thisImage = System.Drawing.Image.FromStream(newFile);

                // Get height and width of current image
                int width = (int)thisImage.Width;
                int height = (int)thisImage.Height;

                // Ratio and conversion for new size
                if (width > maxWidth)
                {
                    ratio = (float)width / (float)maxWidth;
                    width = (int)(width / ratio);
                    height = (int)(height / ratio);
                }

                // Ratio and conversion for new size
                if (height > maxHeight)
                {
                    ratio = (float)height / (float)maxHeight;
                    height = (int)(height / ratio);
                    width = (int)(width / ratio);
                }

                // Create "blank" image for drawing new image
                Bitmap outImage = new Bitmap(width, height);
                Graphics outGraphics = Graphics.FromImage(outImage);
                SolidBrush sb = new SolidBrush(System.Drawing.Color.White);


                // Fill "blank" with new sized image
                outGraphics.FillRectangle(sb, 0, 0, outImage.Width, outImage.Height);
                outGraphics.DrawImage(thisImage, 0, 0, outImage.Width, outImage.Height);
                sb.Dispose();
                outGraphics.Dispose();
                thisImage.Dispose();

                // Save new image as jpg
                outImage.Save(Server.MapPath(folderPathAndFilenameNoExtension), System.Drawing.Imaging.ImageFormat.Jpeg);
                outImage.Dispose();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    string reg = Request.QueryString["pg"].ToString();
                    switch (reg)
                    {
                        case "login":
                            MultiView1.ActiveViewIndex = 0;
                            break;
                        case "errorlog":
                            string error = Request.QueryString["error"].ToString();
                            MultiView1.ActiveViewIndex = 0;
                            promt.Text = "<div class='error'>" + encryptype.psDescrypt(error) + "</div><br/>";
                            break;
                        case "reguser":
                            MultiView1.ActiveViewIndex = 1;
                            break;
                        case "K/nGKCEBhtE=":
                            MultiView1.ActiveViewIndex = 0;
                            string nwuser = Request.QueryString["usersave"].ToString();
                            user Usr = db.users.First(u => u.username == nwuser);
                            txtuser.Text = Usr.username;
                            break;
                    }
                    sitename Sitnme = db.sitenames.First();
                    Page.Title = string.Format(Sitnme.title.ToString());
                    menu Mne = db.menus.First(use=>use.menu_name =="home");
                    lbl_sitename.Text = "<a  style='font-size:20px;' class='TextLightI WatermarkedInputContainer' href='" + Mne.alias + "'>" + Sitnme.title.ToString() + "</a>";//call sitename
                    lbltitle2.Text = "<a  style='font-size:20px;' class='TextLightI WatermarkedInputContainer' href='" + Mne.alias + "'>" + Sitnme.title.ToString() + "</a>";//call sitename
                  

                    //var sption = from s in db.soptions
                    //             where s.disc == "gender"
                    //             select s.name;

                    //if (sption != null)
                    //{
                    //    drpgender.DataSource = sption.Distinct().ToList();
                    //    drpgender.DataBind();
                    //}
                }
                catch (Exception ex)
                {

                }
                
            }
        }
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
            else if (txtuser.Text =="")
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
            if (cookie["perms"] == "3")
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

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            //logins();
            try
            {


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
                        promt.Text = "<div class='error'>Incorect Password</div>";
                    }
                }
                sitename Sitnme = db.sitenames.First();
                Page.Title = string.Format(Sitnme.title.ToString());
            }
            catch (Exception ex)
            {

            }
        }

      

        protected void btnreg_Click(object sender, EventArgs e)
        {

            try
            {


                if (db.f_usernameExist(txtusername.Text) == true)
                {
                    promted2.Text = "<div class='error'>Username is Already Exist</div>";
                }
                else if (db.f_emailExist(txtemailuser.Text) == true)
                {
                    promted2.Text = "<div class='error'>Email is Already Exist</div>";
                }
                else if (txtimgcode.Text == "")
                {
                    promted2.Text = "<div class='error'>Please enter Security Code</div>";
                }
                else if (this.txtimgcode.Text == this.Session["CaptchaImageText"].ToString())
                {
                    //string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
                    //// Check that there is a file
                    //if (fileuserimage.PostedFile != null)
                    //{

                    //    //Path to store uploaded files on server - make sure your paths are unique

                    //    string Id = Request.QueryString["id"];
                    //    string filePath = "../profilepic/oreginal/" + txtusername.Text + ".jpg";
                    //    string thumbPath = "../profilepic/thumb/" + txtusername.Text + ".jpg";

                    //    // Check file size (mustn’t be 0)
                    //    HttpPostedFile myFile = fileuserimage.PostedFile;
                    //    int nFileLen = myFile.ContentLength;
                    //    if ((nFileLen > 0) && (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpg"))
                    //    {
                    //        // Read file into a data stream
                    //        byte[] myData = new Byte[nFileLen];
                    //        myFile.InputStream.Read(myData, 0, nFileLen);
                    //        myFile.InputStream.Dispose();

                    //        // Save the stream to disk as temporary file. make sure the path is unique!
                    //        System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(filePath + "_temp.jpg"),
                    //                                           System.IO.FileMode.Create);

                    //        newFile.Write(myData, 0, myData.Length);

                    //        // run ALL the image optimisations you want here..... make sure your paths are unique
                    //        // you can use these booleans later if you need the results for your own labels or so.
                    //        // dont call the function after the file has been closed.
                    //        bool success = ResizeImageAndUpload(newFile, thumbPath, 300, 300);
                    //        success = ResizeImageAndUpload(newFile, filePath, 1200, 900);

                    //        // tidy up and delete the temp file.
                    //        newFile.Close();
                    //        System.IO.File.Delete(Server.MapPath(filePath + "_temp.jpg"));
                    //    }
                    //    imgFilename = Path.GetFileName(thumbPath);


                    //}
                    //if (fileuserimage.PostedFile == null)
                    //{
                    /*Here you Write Code To Insert User details*/
                    db.sp_APPEND_tbl_user(txtusername.Text, encryptype.psEncrypt(txtpass2.Text),null, null, txtemailuser.Text, Convert.ToInt16(3), null, Convert.ToInt16(5), null, null, "default.png");
                    Response.Redirect("req.aspx?pg=K/nGKCEBhtE=&usersave=" + txtusername.Text);
                    txtuser.Text = txtusername.Text;
                    //}
                    //else
                    //{
                    /*Here you Write Code To Insert User details*/
                    //db.sp_APPEND_tbl_user(txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, imgFilename);
                    //Response.Redirect("req.aspx?pg=K/nGKCEBhtE=&usersave=" + txtusername.Text);
                    //txtuser.Text = txtusername.Text;
                    //    }

                    //}
                    //else
                    //{
                    //    promted2.Text = "<div class='error'>Security Code not Match</div>";

                    //}

                }
                sitename Sitnme = db.sitenames.First();
                Page.Title = string.Format(Sitnme.title.ToString());

            }
            catch (Exception ex)
            {

            }
       
        }
      
      
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("req.aspx?pg=reguser");
        }

      
      
       

      
    }
}