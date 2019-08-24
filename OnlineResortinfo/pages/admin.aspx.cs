using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.IO;
namespace OnlineResortinfo.pages
{
    public partial class admin : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
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
        public void tabview()
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie

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
                listdesktopnav.DataSource = mnu;
                listdesktopnav.DataBind();

                listmobilenav.DataSource = mnu;
                listmobilenav.DataBind();
            }
            else if (cookie["perms"] == "5")
            {
                var mnu = from p in db.menus
                          where p.menu_perms == 1
                          orderby p.menu_id ascending
                          select p;
                listdesktopnav.DataSource = mnu;
                listdesktopnav.DataBind();

                listmobilenav.DataSource = mnu;
                listmobilenav.DataBind();


            }



        }
        public void cattt()
        {
            var sption = from s in db.soptions
                         where s.disc == "category"
                         select s.name;

            if (sption != null)
            {
                Dropcategorypost.DataSource = sption.Distinct().ToList();
                Dropcategorypost.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                tabview();
                sitename Ste = db.sitenames.First();
                lbltitled.Text = Ste.title;
                lbltitlem.Text = Ste.title;
                Page.Title = string.Format(Ste.title.ToString());
                cattt();
                 HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
             
                try
                {
                    if (Request.QueryString["pg"] != null)
                    {
                        string pg = Request.QueryString["pg"].ToString();
                        switch (pg)
                        {
                            case "post":
                                MultiView1.ActiveViewIndex = 0;
                                MultiView2.ActiveViewIndex = 0;
                                lbladmindes.Text = pg.ToString();
                                   user User = db.users.First(use=>use.id == Convert.ToInt16(cookie["userid"]));
                                if(cookie["perms"] == "3")
                                {
                                    var Viwpost = from p in db.view_posts
                                                  select p;
                                    GridViewpost.DataSource = Viwpost;
                                    GridViewpost.DataBind();
                                }
                                else if(cookie["userid"] == Convert.ToString(User.id))
                                {
                                    var Viwpost = from p in db.view_posts
                                                  where p.user_id == User.id
                                                  select p;
                                    GridViewpost.DataSource = Viwpost;
                                    GridViewpost.DataBind();
                                }
                              

                                break;
                            case "createpost":
                                MultiView1.ActiveViewIndex = 0;
                                MultiView2.ActiveViewIndex = 1;
                                lbladmindes.Text = "Create Post";
                                MultiView4.ActiveViewIndex = 0;
                                 
                                break;
                            case "modpost":
                                string id = Request.QueryString["id"].ToString();
                                MultiView1.ActiveViewIndex = 0;
                                MultiView2.ActiveViewIndex = 1;
                                MultiView4.ActiveViewIndex = 1;
                                lbladmindes.Text = "Modify Post";
                                post Post = db.posts.First(use => use.pst_id == Convert.ToInt16(id));
                                txttitlepost.Text = Post.pst_title;
                               //; desEditor.Content.ToString() = Post.pst_content;
                                desEditor.Content = Post.pst_content;
                                soption optionn = db.soptions.First(use => use.id == Post.category);
                                Dropcategorypost.Text = optionn.name.ToString();
                                photopost.ImageUrl = "../images/postimg/"+ Post.filename;
                                break;
                            case "comment":
                                MultiView1.ActiveViewIndex = 1;
                                lbladmindes.Text = pg.ToString();
                                break;
                            case "user":
                                MultiView1.ActiveViewIndex = 2;
                                lbladmindes.Text = pg.ToString();
                                MultiView3.ActiveViewIndex = 0;
                                break;
                            case "rl":
                                MultiView1.ActiveViewIndex = 2;
                                MultiView3.ActiveViewIndex = 1;
                                string modrole = Request.QueryString["md"].ToString();
                                user();
                                user Userr = db.users.First(use => use.id == Convert.ToInt16(modrole));
                                lbladmindes.Text = "Change role of " + Userr.username;
                                Imguser.ImageUrl = "../images/profilepic/" + Userr.image;
                                soption Soption = db.soptions.First(use => use.id == Userr.perms);
                                droprole.Text = Soption.name;
                                break;
                            case "setting":
                                MultiView1.ActiveViewIndex = 3;
                                lbladmindes.Text = pg.ToString();
                                sitename Sitename = db.sitenames.First();
                                txtsitetitle.Text = Sitename.title;


                                break;
                            case "media":
                                MultiView1.ActiveViewIndex = 4;
                                MultiView5.ActiveViewIndex = 0;
                                lbladmindes.Text = pg.ToString();
                                user Usergall = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                               
                                if (cookie["perms"] == "3")
                                {
                                    var Viwgall = from p in db.galleries
                                                  select p;
                                    Gridgallery_view.DataSource = Viwgall;
                                    Gridgallery_view.DataBind();
                                }
                                else if (cookie["userid"] == Convert.ToString(Usergall.id))
                                {
                                    var Viwgall = from p in db.galleries
                                                  where p.user_id == Usergall.id
                                                  select p;
                                    Gridgallery_view.DataSource = Viwgall;
                                    Gridgallery_view.DataBind();
                                }
                                break;

                            case "sl":
                                MultiView1.ActiveViewIndex = 4;
                                MultiView5.ActiveViewIndex = 1;
                                
                                string slid = Request.QueryString["id"].ToString();
                              
                                lbladmindes.Text = pg.ToString();
                                user Usergallselecte = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                                gallery Gall = db.galleries.First(use => use.gallery_id == Convert.ToInt16(slid));
                                lbladmindes.Text = "Gallery album "+ Gall.title;
                                   var lisviewgallerlist = from p in db.gallerylists
                                                           orderby p.date descending
                                                           where p.gallery_id == Gall.gallery_id
                                                           select p;
                                   ListView1.DataSource = lisviewgallerlist;
                                   ListView1.DataBind();
                                break;
                            case"del":
                                MultiView1.ActiveViewIndex = 4;
                                MultiView5.ActiveViewIndex = 2;
                              
                                lbladmindes.Text = "Do you want to delete this image?<br/>";

                             
                                break;

                        }
                    }
                }

                catch (Exception ex)
                {
                }

            }
        }

        protected void btnnewpost_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?pg=createpost");
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("admin.aspx?pg=post");
        }
        /// <summary>
        /// buton save post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {


            string pg = Request.QueryString["pg"].ToString();
            var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
            user USER = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));






            if (filepostimg.PostedFile == null)
            {
                switch (pg)
                {
                    case "createpost":
                        if (Dropcategorypost.Text == "New and Events")
                        {
                            db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 6, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), null);
                            //gallery
                            post gpstcreateid = db.posts.First(use=> use.pst_title == txttitlepost.Text);
                            db.sp_gallerysave(txttitlepost.Text, null, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), gpstcreateid.pst_id);
                            Response.Redirect("admin.aspx?pg=post");
                        }
                        else if (Dropcategorypost.Text == "Promos")
                        {
                            db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 7, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), null);
                            //gallery
                            post gpstcreateid = db.posts.First(use => use.pst_title == txttitlepost.Text);
                            db.sp_gallerysave(txttitlepost.Text, null, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]),gpstcreateid.pst_id);
                            Response.Redirect("admin.aspx?pg=post");
                        }
                        else if (Dropcategorypost.Text == "Rate")
                        {
                            db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 8, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), null);
                            //gallery
                            post gpstcreateid = db.posts.First(use => use.pst_title == txttitlepost.Text);
                            db.sp_gallerysave(txttitlepost.Text, null, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]),gpstcreateid.pst_id);
                            Response.Redirect("admin.aspx?pg=post");
                        }

                        break;
                    case "modpost":
                        if (Dropcategorypost.Text == "New and Events")
                        {
                            string id = Request.QueryString["id"].ToString();
                            post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));

                           
                            db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 6, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Post.filename);
                            //galler update
                            gallery Gall = db.galleries.First(use => use.pst_id == Post.pst_id);
                            db.sp_UPDATE_tbl_gallery(txttitlepost.Text, Gall.filename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                            Response.Redirect("admin.aspx?pg=post");
                        }
                        else if (Dropcategorypost.Text == "Promos")
                        {
                            string id = Request.QueryString["id"].ToString();
                            post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));
                            db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 7, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Post.filename);

                            //galler update
                            gallery Gall = db.galleries.First(use => use.pst_id == Convert.ToInt16(id));
                            db.sp_UPDATE_tbl_gallery(txttitlepost.Text, Gall.filename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                            Response.Redirect("admin.aspx?pg=post");
                        }
                        else if (Dropcategorypost.Text == "Rate")
                        {
                            string id = Request.QueryString["id"].ToString();
                            post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));
                            db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 8, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Post.filename);
                            //galler update
                            gallery Gall = db.galleries.First(use => use.pst_id == Convert.ToInt16(id));
                            db.sp_UPDATE_tbl_gallery(txttitlepost.Text, Gall.filename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                            
                            Response.Redirect("admin.aspx?pg=post");
                        }


                        break;
                }
            }
            else
            {
              

                    //Path to store uploaded files on server - make sure your paths are unique
                    string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;

                    string Id = Request.QueryString["id"];
                    string filePath = "../images/postimg/" + filepostimg.FileName;
                    // string thumbPath = "../images/thumbs/" + txttitlepost.Text + ".jpg";

                    // Check file size (mustn’t be 0)
                    HttpPostedFile myFile = filepostimg.PostedFile;
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
                        // bool success = ResizeImageAndUpload(newFile, thumbPath, 1200, 700);
                        bool success = ResizeImageAndUpload(newFile, filePath, 1200, 1200);

                        // tidy up and delete the temp file.
                        newFile.Close();
                        System.IO.File.Delete(Server.MapPath(filePath + "_temp.jpg"));
                    }
                    imgFilename = Path.GetFileName(filePath);

                    switch (pg)
                    {
                        case "createpost":
                            if (Dropcategorypost.Text == "New and Events")
                            {
                                db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 6, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler image
                                post gpstcreateid = db.posts.First(use => use.pst_title == txttitlepost.Text);
                                db.sp_gallerysave(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), gpstcreateid.pst_id);
                                Response.Redirect("admin.aspx?pg=post");
                            }
                            else if (Dropcategorypost.Text == "Promos")
                            {
                                db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 7, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler image
                                post gpstcreateid = db.posts.First(use => use.pst_title == txttitlepost.Text);
                                db.sp_gallerysave(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), gpstcreateid.pst_id);
                                Response.Redirect("admin.aspx?pg=post");
                            }
                            else if (Dropcategorypost.Text == "Rate")
                            {
                                db.sp_APPEND_tbl_post(txttitlepost.Text, desEditor.Content.ToString(), "Publish", 8, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler image
                                post gpstcreateid = db.posts.First(use => use.pst_title == txttitlepost.Text);
                                db.sp_gallerysave(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), gpstcreateid.pst_id);
                                Response.Redirect("admin.aspx?pg=post");
                            }



                            break;

                        case "modpost":

                            if (Dropcategorypost.Text == "New and Events")
                            {
                                string id = Request.QueryString["id"].ToString();
                                post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));


                                db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 6, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler update
                                db.sp_UPDATE_tbl_gallery(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                                Response.Redirect("admin.aspx?pg=post");
                            }
                            else if (Dropcategorypost.Text == "Promos")
                            {
                                string id = Request.QueryString["id"].ToString();
                                post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));


                                db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 7, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler update
                                db.sp_UPDATE_tbl_gallery(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                                Response.Redirect("admin.aspx?pg=post");
                            }
                            else if (Dropcategorypost.Text == "Rate")
                            {
                                string id = Request.QueryString["id"].ToString();
                                post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(id));


                                db.sp_UPDATE_tbl_post(Convert.ToInt16(id), txttitlepost.Text, desEditor.Content.ToString(), "Publish", 8, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), imgFilename);
                                //galler update
                                db.sp_UPDATE_tbl_gallery(txttitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Convert.ToInt16(id));
                                Response.Redirect("admin.aspx?pg=post");
                            }


                            break;
                    
                }   
            }

        }

        protected void GridViewpost_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
            switch (e.CommandName.ToString())
            {
                case "post_edit":
                    Response.Redirect("admin.aspx?pg=modpost&id=" + e.CommandArgument.ToString());
                    break;
                case "MyDelete":
                    post Post = db.posts.First(use => use.pst_id == Convert.ToInt16(e.CommandArgument.ToString()));
                    if (cookie["userid"] != null)
                    {



                        string file_name = Post.filename;
                        string path = Server.MapPath("../images/postimg/" + file_name);

                        FileInfo file = new FileInfo(path);

                        if (file.Exists)
                        {
                            file.Delete();

                        }
                        else
                        {

                        }
                        gallery gl = db.galleries.First(aa=>aa.pst_id == Convert.ToInt16(e.CommandArgument.ToString()));
                        db.galleries.DeleteOnSubmit(gl);
                        db.SubmitChanges();
                        db.sp_delete_tbl_comment(Convert.ToInt16(e.CommandArgument.ToString()));
                        db.sp_delete_post(Convert.ToInt16(e.CommandArgument.ToString()));
                        Response.Redirect("admin.aspx?pg=post");
                    }
                    else if (cookie["perms"] == Convert.ToString(3))
                    {



                        string file_name = Post.filename;
                        string path = Server.MapPath("../images/postimg/" + file_name);

                        FileInfo file = new FileInfo(path);

                        if (file.Exists)
                        {
                            file.Delete();

                        }
                        else
                        {

                        }
                        gallery gl = db.galleries.First(aa => aa.pst_id == Convert.ToInt16(e.CommandArgument.ToString()));
                        db.galleries.DeleteOnSubmit(gl);
                        db.SubmitChanges();
                        db.sp_delete_tbl_comment(Convert.ToInt16(e.CommandArgument.ToString()));
                        db.sp_delete_post(Convert.ToInt16(e.CommandArgument.ToString()));
                        Response.Redirect("admin.aspx?pg=post");
                    }
                    else
                    {
                        lblpromted.Text = "<div id='last_message'>You cannot delete these post</div><br/>";
                    }

                    break;
            }
           

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToString())
                {
                    case "deletecomment":
                        comment com = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                        db.comments.DeleteOnSubmit(com);
                        db.SubmitChanges();
                        GridView1.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// user role
        /// </summary>
        public void user()
        {
            var sption = from s in db.soptions
                         where s.disc == "Actype"
                         select s.name;

            if (sption != null)
            {
                droprole.DataSource = sption.Distinct().ToList();
                droprole.DataBind();
            }

            var sptionrole = from s in db.soptions
                             where s.disc == "Actype"
                             select s.name;

            droprole.DataSource = sptionrole.Distinct().ToList();
            droprole.DataBind();
        }
        /// <summary>
        /// select user to modify
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnrolesave_Click(object sender, EventArgs e)
        {

            string modrole = Request.QueryString["md"].ToString();

            var q_test = db.users.Where(aa => aa.id == Convert.ToInt16(modrole)).ToList();

            if (droprole.Text == "Admin")
            {
                foreach (var row in q_test)
                {
                    row.perms = 3;
                    row.actype = 3;
                    db.SubmitChanges();
                }

                lbluserpromted.Text = "<div style='color:#005782;'>Change role successful</div>";
            }
            else if (droprole.Text == "Guest")
            {
                foreach (var row in q_test)
                {
                    row.perms = 4;
                    row.actype = 4;
                    db.SubmitChanges();
                }
                lbluserpromted.Text = "<div style='color:#005782;'>Change role successful</div>";
            }
            else if (droprole.Text == "Editor")
            {
                foreach (var row in q_test)
                {
                    row.perms = 5;
                    row.actype = 3;
                    db.SubmitChanges();
                }
                lbluserpromted.Text = "<div style='color:#005782;'>Change role successful</div>";
            }
        }

        protected void btnusercancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("admin.aspx?pg=user");

        }
        /// <summary>
        /// Change site title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsitesave_Click(object sender, EventArgs e)
        {
            sitename Site = db.sitenames.Where(use => use.id == 1).Single();

            Site.title = txtsitetitle.Text;
            db.SubmitChanges();
            Response.Redirect("admin.aspx?pg=setting");
        }

        protected void btnsitename_Click(object sender, EventArgs e)
        {
            btnsitename.CssClass = "Clicked";
            btnabout.CssClass = "Initial";
            btnfaq.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;

        }

        protected void btnabout_Click(object sender, EventArgs e)
        {
            btnsitename.CssClass = "Initial";
            btnabout.CssClass = "Clicked";
            btnfaq.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
            sitename Sitename = db.sitenames.First(use => use.id == 1);
            txtsitetitle.Text = Sitename.title;
            EditorAbout.Text = Sitename.about;
        }

        protected void btnfaq_Click(object sender, EventArgs e)
        {
            btnsitename.CssClass = "Initial";
            btnabout.CssClass = "Initial";
            btnfaq.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
            sitename Sitename = db.sitenames.First(use => use.id == 1);
            txtsitetitle.Text = Sitename.title;
            EditorFAQ.Text = Sitename.faq;

        }

        protected void btnAboutsave_Click(object sender, EventArgs e)
        {
            var Site = db.sitenames.Where(use => use.id == 1).ToList();
            foreach (var row in Site)
            {
                row.about = EditorAbout.Text;
            }

            db.SubmitChanges();
            Response.Redirect("admin.aspx?pg=setting");
        }

        protected void btnfaqsave_Click(object sender, EventArgs e)
        {
            var Site = db.sitenames.Where(use => use.id == 1).ToList();
            foreach (var row in Site)
            {
                row.faq = EditorFAQ.Text;
            }
            db.SubmitChanges();
            Response.Redirect("admin.aspx?pg=setting");
        }
        /// <summary>
        /// upload image media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Uploadimage_Click(object sender, EventArgs e)
        {
            
             

              if (filegallerimg.HasFile)
              {
                  try
                  {
                      string slid = Request.QueryString["id"].ToString();
                      var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                      gallery gallthum = db.galleries.First(use => use.gallery_id == Convert.ToInt16(slid));
                      HttpCookie cookie = Request.Cookies["onlineresort"];// declaration cookie

                      filegallerimg.SaveAs(Server.MapPath("../images/gallery/") + gallthum.title + "-" + filegallerimg.FileName);

                      db.gallerylists.InsertOnSubmit(new gallerylist
                      {
                          gallery_id = Convert.ToInt16(slid),
                          filename = gallthum.title + "-" + filegallerimg.FileName.Replace(".jpg", "").Replace(".JPG","").ToString(),
                          image = gallthum.title + "-" + filegallerimg.FileName,
                          date = Convert.ToDateTime(d)

                      });
                      db.SubmitChanges();
                      Response.Redirect("admin.aspx?pg=sl&id=" + slid);
                  }





                  catch (Exception ex)
                  {

                  }


              }
              //sitename Ste = db.sitenames.First();
              //Page.Title = string.Format(Ste.title.ToString());

        }
        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];// declaration cookie
            switch (e.CommandName.ToString())
            {
                case "medelete":

                    user User = db.users.First(use => use.id == Convert.ToInt16(e.CommandArgument.ToString()));
                    if (cookie == null)
                    {
                        Response.Redirect("home.aspx");
                    }
                    if (cookie["perms"] == "3")
                    {

                       
                            post Post = db.posts.First(use => use.user_id == User.id);
                            db.sp_delete_usercomment(User.id);
                            db.posts.DeleteOnSubmit(Post);
                            db.users.DeleteOnSubmit(User);

                            string file_name = Post.filename;
                            string path = Server.MapPath("../images/postimg/" + file_name);

                            FileInfo file = new FileInfo(path);

                            if (file.Exists)
                            {
                                file.Delete();

                            }
                            else
                            {
                            }
                            db.SubmitChanges();
                      
                         
                           

                            //db.sp_delete_post(User.id);
                            // comment Cmm = db.comments.First(use => use.pst_id == Post.pst_id);




                         
                            GridView2.DataBind();
                            sitename Ste = db.sitenames.First();
                            Page.Title = string.Format(Ste.title.ToString());
                     
                    }
                    else if (cookie["userid"] == Convert.ToString(User.id))
                    {
                        try
                        {
                            post Post = db.posts.First(use => use.user_id == User.id);

                            //db.sp_delete_post(User.id);
                           //comment Cmm = db.comments.First(use => use.pst_id == Post.pst_id);
                            db.sp_delete_usercomment(Post.user_id);
                            db.posts.DeleteOnSubmit(Post);
                            db.users.DeleteOnSubmit(User);

                            string file_name = Post.filename;
                            string path = Server.MapPath("../images/postimg/" + file_name);

                            FileInfo file = new FileInfo(path);

                            if (file.Exists)
                            {
                                file.Delete();

                            }
                            else
                            {
                            }
                           

                            db.SubmitChanges();
                            GridView2.DataBind();
                            sitename Ste = db.sitenames.First();
                            Page.Title = string.Format(Ste.title.ToString());
                        }
                        catch (Exception ex)
                        {
                         
                        }


                    }
                    else
                    {
                        Label1.Text = "You cannot delete this user";
                    }
                    break;

            }
        }
        /// <summary>
        /// delete gallery image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Gridgallery_view_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "select":
                    Response.Redirect("admin.aspx?pg=sl&id=" + e.CommandArgument.ToString());
                    break;
            }
        }

        protected void lnkupload_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 0;
        }

        protected void lnkview_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 1;
        }
        /// <summary>
        /// delete selected image in gallery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string idimg = Request.QueryString["id"].ToString();
            gallerylist glddel = db.gallerylists.First(aa => aa.id == Convert.ToInt16(idimg));
            string file_name = glddel.image;
            string path = Server.MapPath("../images/gallery/" + file_name);

            FileInfo file = new FileInfo(path);

            if (file.Exists)
            {
                file.Delete();

            }
            else
            {
            }
            db.gallerylists.DeleteOnSubmit(glddel);
            db.SubmitChanges();
         
            Response.Redirect("admin.aspx?pg=media");
        }
        /// <summary>
        /// cancel delete attemp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
  
            Response.Redirect("admin.aspx?pg=media");
        }

      

       

       

    } 
}





