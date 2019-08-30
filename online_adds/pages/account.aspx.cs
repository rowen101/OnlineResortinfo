using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
namespace online_adds.pages
{
    public partial class account : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        clssecurity clssEncryptsecurity = new clssecurity();
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
        public void view()
        {

            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            if (cookie == null)
            {
                Response.Redirect("home.aspx");
            }
            else
            {


                user User = db.users.First(u => u.username == cookie["username"]);
                txtusername.Text = User.username.ToString();
                txtfname.Text = User.fname;
                txtmname.Text = User.mname;
                txtlname.Text = User.lname;
                txtaddress.Text = User.address;
                txtemail.Text = User.email;
                imageitem.ImageUrl = "../profilepic/oreginal/" + User.Image;
                var sption = from s in db.soptions
                             where s.disc == "gender"
                             select s.name;

                if (sption != null)
                {
                    dropgender.DataSource = sption.Distinct().ToList();
                    dropgender.DataBind();
                }

                dropgender.Text = User.gender;
            }
            if (cookie["username"] == "admin")
            {
                Button1.Enabled = false;
            }
            else
            {
                Button1.Enabled = true;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                view();
                MultiView1.ActiveViewIndex = 0;
                MultiView2.ActiveViewIndex = 0;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {

            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            user Usrs = db.users.First(u => u.username == cookie["username"]);//retrieve uer id
            string password = clssEncryptsecurity.psDescrypt(Usrs.password);//show password


            string Id = Request.QueryString["id"];
            string Profilepic = Usrs.Image.ToString();

            if (fileuserimage.PostedFile == null)
            {


                if (Profilepic == null)
                {
                    db.sp_UPDATE_tbl_userInfo(Convert.ToInt16(Usrs.id), cookie["username"], clssEncryptsecurity.psEncrypt(password), txtfname.Text, txtlname.Text, txtemail.Text, dropgender.Text, txtmname.Text, txtaddress.Text, null);
                    Label1.Text = "<div class='ok'>Update user info Successful</div>";
                    MultiView2.ActiveViewIndex = 0;
                }
                else
                {
                    db.sp_UPDATE_tbl_userInfo(Convert.ToInt16(Usrs.id), cookie["username"], clssEncryptsecurity.psEncrypt(password), txtfname.Text, txtlname.Text, txtemail.Text, dropgender.Text, txtmname.Text, txtaddress.Text, Usrs.Image);
                    Label1.Text = "<div class='ok'>Update user info Successful</div>";
                    MultiView2.ActiveViewIndex = 0;
                }


            }
            else
            {
                if (fileuserimage.PostedFile != null)
                {

                    string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
                    // Check that there is a file
                    if (fileuserimage.PostedFile != null)
                    {

                        //Path to store uploaded files on server - make sure your paths are unique


                        string filePath = "../profilepic/oreginal/" + txtusername.Text + ".jpg";
                        string thumbPath = "../profilepic/thumb/" + txtusername.Text + ".jpg";

                        // Check file size (mustn’t be 0)
                        HttpPostedFile myFile = fileuserimage.PostedFile;
                        int nFileLen = myFile.ContentLength;
                        if ((nFileLen > 0) && (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpg"))
                        {
                            // Read file into a data stream
                            byte[] myData = new Byte[nFileLen];
                            myFile.InputStream.Read(myData, 0, nFileLen);
                            myFile.InputStream.Dispose();

                            // Save the stream to disk as temporary file. make sure the path is unique!
                            System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(filePath + "_temp.jpg"),
                                                               System.IO.FileMode.Create);

                            newFile.Write(myData, 0, myData.Length);

                            // run ALL the image optimisations you want here..... make sure your paths are unique
                            // you can use these booleans later if you need the results for your own labels or so.
                            // dont call the function after the file has been closed.
                            bool success = ResizeImageAndUpload(newFile, thumbPath, 300, 300);
                            success = ResizeImageAndUpload(newFile, filePath, 1200, 900);

                            // tidy up and delete the temp file.
                            newFile.Close();
                            System.IO.File.Delete(Server.MapPath(filePath + "_temp.jpg"));
                        }
                        imgFilename = Path.GetFileName(thumbPath);

                        Stream imgdatastream = fileuserimage.PostedFile.InputStream;
                        int imgdatalen = fileuserimage.PostedFile.ContentLength;
                        byte[] imgdata = new byte[imgdatalen];
                        int n = imgdatastream.Read(imgdata, 0, imgdatalen);

                        db.sp_UPDATE_tbl_userInfo(Convert.ToInt16(Usrs.id), cookie["username"], clssEncryptsecurity.psEncrypt(password), txtfname.Text, txtlname.Text, txtemail.Text, dropgender.Text, txtmname.Text, txtaddress.Text, imgFilename);
                        Label1.Text = "<div class='ok'>Update user info Successful</div>";
                        MultiView2.ActiveViewIndex = 0;
                        Response.Redirect("account.aspx");
                    }
                }

            }

        }

        protected void btnchangeaccount_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            if (cookie == null)
            {
                Response.Redirect("home.aspx");
            }
            else
            {
                try
                {
                    if (txtolduserpass.Text == "")
                    {
                        Label1.Text = "<div class='error'>Old password must not be empty</div>";
                    }
                    else
                    {

                        if (db.f_passwordExist(clssEncryptsecurity.psEncrypt(txtolduserpass.Text)) == true)
                        {
                            db.sp_UPDATE_userAccount(cookie["username"].ToString(), clssEncryptsecurity.psEncrypt(txtpass2.Text));
                            Label1.Text = "<div class='ok'>Change password successful </div>";

                        }
                        else
                        {
                            Label1.Text = "<div class='error'>Incorrect password</div>";
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }


        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            user Usr = db.users.First(u => u.username == cookie["username"]);
            Label2.Text = "Do you want Deactivate your account?";
            sitename Sitnme = db.sitenames.First();
            Page.Title = string.Format(Sitnme.title.ToString());
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            HttpCookie cookie = Request.Cookies["rowenref"];//declaration of cookie
            user usrtrash = db.users.First(use => use.username == cookie["username"]);
         

            post Pstid = db.posts.First(use => use.user_id == Convert.ToInt16(cookie["userid"]));
            
           
            tbl_gallery gll = db.tbl_galleries.First(use => use.pst_id == Pstid.pst_id);

            string file_name = Pstid.Image;
            string Username = usrtrash.Image;
            string userpaths = Server.MapPath("../profilepic/oreginal/" + Username);
            string paths = Server.MapPath("../images/oreginal/" + file_name);

            FileInfo userfiles = new FileInfo(userpaths);
            FileInfo files = new FileInfo(paths);
            if (files.Exists)
            {

                files.Delete();
            }
            else if (userfiles.Exists)
            {
                userfiles.Delete();
            }
            else
            {

            }

            db.sp_DELETE_tbl_commentuser(Convert.ToInt16(cookie["userid"]));//delete user comment
            db.sp_DELETE_tbl_postuser(Convert.ToInt16(cookie["userid"]));//delete user post
          
            db.tbl_galleries.DeleteOnSubmit(gll);
            db.users.DeleteOnSubmit(usrtrash);
            db.SubmitChanges();

            // cookie distroy


            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            Response.Redirect("home.aspx");




        }

        protected void lnkchangeimage_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 1;
        }

        protected void lnkshowimage_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;
        }










    }
}