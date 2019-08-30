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
using System.IO;
using System.Drawing;
namespace online_adds.pages
{
    public partial class adminsetup : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        clssecurity encryptype = new clssecurity();
        /// <summary>
        /// show ip address
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetUser_IP()
        {
            string VisitorsIPAddr = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                VisitorsIPAddr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                VisitorsIPAddr = HttpContext.Current.Request.UserHostAddress;
            }
            lblip.Text = VisitorsIPAddr;
        }
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
                    HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
                    lblfullname.Text = cookie["fullname"];
                    lblprofilepic.Text = cookie["profilepic"];

                    //show date

                    drpslecategorypost();
                    this.user();

                    string frm = Request.QueryString["pg"].ToString();

                    //ip
                    GetUser_IP();

                    switch (frm)
                    {
                        //post content
                        case "post":
                            MultiView1.ActiveViewIndex = 0;
                           
                            useridpost.Text = cookie["userid"].ToString();
                            lbladview.Text = "1";

                            user Userpost = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"].ToString()));

                            //if(cookie["userid"] == Convert.ToString(Userpost.id))
                            //{
                            //    var viwpost= from pst in db.viewposts
                            //                     where pst.user_id == Userpost.id
                            //                     select pst;
                            //    GridView1.DataSource = viwpost;
                            //    GridView1.DataBind();
                            //}
                           if(cookie["perms"] == "3")
                            {
                                var viwpost = from pst in db.viewposts
                                                 select pst;
                                GridView1.DataSource = viwpost;
                                GridView1.DataBind();
                            }
                           else if(cookie["userid"] == Convert.ToString(Userpost.id))
                           {
                               var viwpost = from pst in db.viewposts
                                             where pst.user_id == Userpost.id
                                             select pst;
                               GridView1.DataSource = viwpost;
                               GridView1.DataBind();
                           }
                            break;
                        case "modifypost":
                            MultiView1.ActiveViewIndex = 0;
                            Paneladdpost.Visible = true;
                            Panelviewpost.Visible = false;
                            string mod = Request.QueryString["postid"].ToString();
                            postid.Text = mod.ToString();

                            post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(mod));
                            postid.Text = mod.ToString();
                            txttitlepost.Text = Post.pst_title.ToString();
                            desEditor.Content = Post.pst_content.ToString();
                            Dropcategorypost.Text = Post.category.ToString();
                            MultiView4.ActiveViewIndex = 1;
                            imageitem.ImageUrl = "../images/oreginal/" + Post.Image;
                            break;
                        case "deletepost":
                            MultiView1.ActiveViewIndex = 0;
                           
                            Paneladdpost.Visible = false;
                            Panelviewpost.Visible = false;
                            string userid = Request.QueryString["postid"].ToString();
                            post Postdel = db.posts.First(p => p.pst_id == Convert.ToInt16(userid));


                            if (cookie["userid"] == Convert.ToString(Postdel.user_id))
                            {
                                Paneldeletepost.Visible = true;
                                lblpostdelpromted.Text = "Do you want to delete " + Postdel.pst_title + " post?";

                            }
                            else if (cookie["perms"] == Convert.ToString(3))
                            {
                                Paneldeletepost.Visible = true;
                                lblpostdelpromted.Text = "Do you want to delete " + Postdel.pst_title + " post?";
                            }
                            else
                            {
                                Paneldeletepost.Visible = true;
                                btndelpost.Visible = false;
                                lblpostdelpromted.Text = Postdel.pst_title + " is not your post delete attempt stop!";
                            }
                           
                            break;
                        //end post
                        case "comment":
                            MultiView1.ActiveViewIndex = 1;
                            MultiView7.ActiveViewIndex = 0;

                            break;
                        case "appearance":
                            MultiView1.ActiveViewIndex = 2;
                            break;
                        //view user
                        case "user":
                            MultiView1.ActiveViewIndex = 3;
                            MultiView3.ActiveViewIndex = 0;


                            break;
                        //add user
                        case "adduser":
                            MultiView1.ActiveViewIndex = 3;
                            MultiView3.ActiveViewIndex = 1;
                            lbluser.Text = "<b>Add User</b>";
                            break;

                        //view select user
                        case "Userview":
                            MultiView1.ActiveViewIndex = 3;
                            MultiView3.ActiveViewIndex = 1;
                            string userview = Request.QueryString["userid"].ToString();
                            int id = Convert.ToInt16(userview);
                            user Usr = db.users.First(u => u.id == id);
                            Label2.Text = Convert.ToString(id);
                            txtusername.Enabled = false;
                            txtusername.Text = Usr.username;
                            txtemailuser.Text = Usr.email;
                            txtfname.Text = Usr.fname;
                            txtmname.Text = Usr.mname;
                            txtlname.Text = Usr.lname;
                            txtaddress.Text = Usr.address;
                            drpgender.Text = Usr.gender;
                            txtpass1.Enabled = false;
                            txtpass2.Enabled = false;
                            soption Soption = db.soptions.First(s => s.id == Usr.actype);
                            droprole.Text = Soption.name;
                            lbluser.Text = "<b>Modify User</b>";

                            userimage.Visible = true;
                            userimage.ImageUrl = ("../profilepic/thumb/" + Usr.Image);
                            break;
                        //view setting
                        case "setting":
                            MultiView1.ActiveViewIndex = 4;
                            sitename Sitnme = db.sitenames.First();
                            txtsitetitle.Text = Sitnme.title;
                            txttagline.Text = Sitnme.tagline;
                            txtemail.Text = Sitnme.email;
                            Editorfaq.Content = Sitnme.faq;
                          

                            break;
                        //media
                        case "media":
                            MultiView1.ActiveViewIndex = 5;
                            MultiView6.ActiveViewIndex = 0;

                            user User = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"].ToString()));

                            if (cookie == null)
                            {
                                Response.Redirect("home.aspx");
                            }
                            else if(cookie["perms"] == "3")
                            {
                                var viwgallery = from pst in db.tbl_galleries
                                                 select pst;
                                ListView1.DataSource = viwgallery;
                                ListView1.DataBind();
                            }
                              else  if(cookie["userid"] == Convert.ToString(User.id))
                            {
                                var viwgallery = from pst in db.tbl_galleries
                                                 where pst.user_id == User.id
                                                 select pst;
                                ListView1.DataSource = viwgallery;
                                ListView1.DataBind();
                            }
                                
                            
                            break;

                        case "addgallery":
                            string mediaid = Request.QueryString["mediaid"].ToString();
                            MultiView1.ActiveViewIndex = 5;
                            MultiView6.ActiveViewIndex = 1;
                            Panel1.Visible = true;
                            Panel2.Visible = false;
                            tbl_gallery Gallery = db.tbl_galleries.First(use => use.gallery_id == Convert.ToInt16(mediaid));
                            mediathumb.ImageUrl = ("../images/oreginal/" + Gallery.filename);
                            Label6.Text = Gallery.filename.Replace(".jpg","")+" Gallery";
                            var galleryselected = from pst in db.gallerylists
                                                  where pst.gallery_id == Convert.ToInt16(mediaid)
                                                  orderby pst.gallery_id ascending
                                                  select pst;
                            listgalleryslcted.DataSource = galleryselected;
                            listgalleryslcted.DataBind();
                            break;
                        case "selcimage":
                            string gallid = Request.QueryString["id"].ToString();
                            gallerylist lisgall = db.gallerylists.First(use=>use.id == Convert.ToInt16(gallid));

                                if(cookie["userid"] == Convert.ToString(lisgall.user_id))
                                {
                                    MultiView1.ActiveViewIndex = 5;
                                    MultiView6.ActiveViewIndex = 1;
                                    Panel1.Visible = false;
                                    Panel2.Visible = true;
                                    lbldeleteimagepromted.Text = "Do you want delete This Image?";
                                }
                                else if (cookie["perms"] == Convert.ToString(3))
                                {
                                    MultiView1.ActiveViewIndex = 5;
                                    MultiView6.ActiveViewIndex = 1;
                                    Panel1.Visible = false;
                                    Panel2.Visible = true;
                                    lbldeleteimagepromted.Text = "Do you want delete This Image?";
                                }
                                else
                                {
                                    MultiView1.ActiveViewIndex = 5;
                                    MultiView6.ActiveViewIndex = 1;
                                    Panel1.Visible = false;
                                    Panel2.Visible = true;
                                    lbldeleteimagepromted.Text = "You cannot delete This Image!";
                                    brtdelteimage.Visible = false;
                                }
                            break;
                        //pages
                        case "pages":
                            MultiView1.ActiveViewIndex = 7;
                            MultiView5.ActiveViewIndex = 0;
                            break;

                        case "deletepage":
                            string pageid = Request.QueryString["req"].ToString();
                            MultiView1.ActiveViewIndex = 7;
                            MultiView5.ActiveViewIndex = 1;

                            menu Mnu = db.menus.First(use => use.menu_id == Convert.ToInt16(pageid));
                            int dis = Convert.ToInt16(Mnu.parent_id);

                            if (dis == Convert.ToInt16(0))
                            {
                                lblpgpromted.Text = "You Do not delete This page";
                                btnpgdelete.Enabled = false;
                            }
                            else
                            {
                                lblpgpromted.Text = "Do you want delete This page ?";
                            }
                            break;
                        //add Pages
                        case "Addpages":
                            MultiView1.ActiveViewIndex = 7;
                            MultiView5.ActiveViewIndex = 2;
                            break;
                        //Edit Pages
                        case "Editpage":
                            string pageidedit = Request.QueryString["req"].ToString();
                            MultiView1.ActiveViewIndex = 7;
                            MultiView5.ActiveViewIndex = 2;
                            menu Mnue = db.menus.First(use => use.menu_id == Convert.ToInt16(pageidedit));
                            otherpage othpag = db.otherpages.First(use => use.Pagetitle == Mnue.menu_name);
                            lblmenuid.Text = Convert.ToString(Mnue.menu_id);
                            lblotherpageid.Text = Convert.ToString(othpag.id);
                            txtpagestitle.Text = othpag.Pagetitle;
                            Editorpages.Content = othpag.body;
                            break;


                    }
                }
                catch
                {
                    MultiView1.ActiveViewIndex = 6;
                }
            }
        }


        #region"Comment"
        /// <summary>
        /// for comment
        /// </summary>


        protected void lnkrefresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=comment");

        }

        protected void lnkallcomment_Click(object sender, EventArgs e)
        {
            GridView2.DataBind();
            MultiView1.ActiveViewIndex = 1;
            MultiView7.ActiveViewIndex = 0;
        }

        protected void lnkapproved_Click(object sender, EventArgs e)
        {
            GridView5.DataBind();
            MultiView1.ActiveViewIndex = 1;
            MultiView7.ActiveViewIndex = 1;
        }

        protected void lnkpinding_Click(object sender, EventArgs e)
        {
            GridView6.DataBind();
            MultiView1.ActiveViewIndex = 1;
            MultiView7.ActiveViewIndex = 2;
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {

                case "Pinding":


                    comment Cmts = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    Cmts.c_status = Convert.ToInt16(13);
                    db.SubmitChanges();
                    GridView6.DataBind();

                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

                case "Approved":
                    comment CmtAp = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    CmtAp.c_status = Convert.ToInt16(12);
                    db.SubmitChanges();
                    GridView6.DataBind();
                    //Response.Redirect("admin.aspx?frm=allcomment");


                    break;
                case "Trash":
                    comment Trsh = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    db.comments.DeleteOnSubmit(Trsh);
                    db.SubmitChanges();
                    GridView6.DataBind();
                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {

                case "Pinding":


                    comment Cmts = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    Cmts.c_status = Convert.ToInt16(13);
                    db.SubmitChanges();
                    GridView5.DataBind();

                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

                case "Approved":
                    comment CmtAp = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    CmtAp.c_status = Convert.ToInt16(12);
                    db.SubmitChanges();
                    GridView5.DataBind();
                    //Response.Redirect("admin.aspx?frm=allcomment");


                    break;
                case "Trash":
                    comment Trsh = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    db.comments.DeleteOnSubmit(Trsh);
                    db.SubmitChanges();
                    GridView5.DataBind();
                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

            }
        }
        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {

                case "Pinding":


                    comment Cmts = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    Cmts.c_status = Convert.ToInt16(13);
                    db.SubmitChanges();
                    GridView2.DataBind();

                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

                case "Approved":
                    comment CmtAp = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    CmtAp.c_status = Convert.ToInt16(12);
                    db.SubmitChanges();
                    GridView2.DataBind();
                    //Response.Redirect("admin.aspx?frm=allcomment");


                    break;
                case "Trash":
                    comment Trsh = db.comments.First(use => use.c_ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    db.comments.DeleteOnSubmit(Trsh);
                    db.SubmitChanges();
                    GridView2.DataBind();
                    // Response.Redirect("admin.aspx?frm=allcomment");
                    break;

            }
        }

        #endregion


        #region"Post"
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// category post
        /// </summary>
        public void viewpostcontent()
        {
            Response.Redirect("adminsetup.aspx?pg=post");
            Paneladdpost.Visible = false;
            Panelviewpost.Visible = true;
            Paneldeletepost.Visible = false;
        }
        //dropdown list
        public void drpslecategorypost()
        {
             
            var sption = from s in db.soptions
                         where s.disc == "category"
                         select s.name;

            if (sption != null)
            {
                Dropcategorypost.DataSource = sption.Distinct().ToList();
                Dropcategorypost.DataBind();
            }
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
            //if (cookie["perms"] == "3")
            //{
            //    var drptitle = from ps in db.posts
            //                   orderby ps.pst_title ascending
            //                   select ps.pst_title;
            //    if (drptitle != null)
            //    {
            //        droptitlepost.DataSource = drptitle.Distinct().ToList();
            //        droptitlepost.DataBind();
            //    }
            //}
            //else
            //{
            //    var drptitle = from ps in db.posts
            //                   where ps.user_id == Convert.ToInt16(cookie["userid"].ToString())
            //                   orderby ps.pst_title ascending
            //                   select ps.pst_title;
            //    if (drptitle != null)
            //    {
            //        droptitlepost.DataSource = drptitle.Distinct().ToList();
            //        droptitlepost.DataBind();
            //    }
            //}
           

        }
        /// <summary>
        /// refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {

            Response.Redirect("adminsetup.aspx?pg=post");
        }
        protected void lnkchangeimage_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 0;
            lnkshow.Visible = true;
        }

        protected void lnkshow_Click(object sender, EventArgs e)
        {
            MultiView4.ActiveViewIndex = 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToString())
                {
                    case "post_edit":
                        Response.Redirect("adminsetup.aspx?pg=modifypost&postid=" + e.CommandArgument.ToString());
                        break;
                    case "MyDelete":
                        Response.Redirect("adminsetup.aspx?pg=deletepost&postid=" + e.CommandArgument.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnsavepost_Click(object sender, EventArgs e)
        {

           
            
                string frm = Request.QueryString["pg"].ToString();
                var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
                user USER = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));

                if (postimgupload.PostedFile == null)
                {

                    switch (frm)
                    {
                        case "post":
                            if (db.f_posttitleExist(txttitlepost.Text) == true)
                            {
                                lblrequiredfiled.Text = "<div class='promtederror'>This Title is already Exist..</div>";

                            }
                            else
                            {
                                db.sp_savepost(txttitlepost.Text, desEditor.Content.ToString(), lblip.Text, "Publish", Dropcategorypost.Text, Convert.ToDateTime(d), null, USER.id, 1);
                                //post Pst = db.posts.First(use => use.pst_title == droptitlepost.Text);

                                post Postsl = db.posts.First(p => p.pst_title == txttitlepost.Text);
                                db.tbl_galleries.InsertOnSubmit(new tbl_gallery
                                {
                                    title = txttitlepost.Text,
                                    filename = null,
                                    date = Convert.ToDateTime(d),
                                    user_id = Convert.ToInt16(cookie["userid"]),
                                    pst_id = Postsl.pst_id
                                });
                                db.SubmitChanges();
                                viewpostcontent();
                            }
                            break;
                        case "modifypost":

                            string mod = Request.QueryString["postid"].ToString();
                            post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(mod));
                            //gallery update


                            db.sp_updatepost(Convert.ToInt16(postid.Text), txttitlepost.Text, desEditor.Content.ToString(), Convert.ToDateTime(d), lblip.Text, "Publish", Dropcategorypost.Text, Post.Image, USER.id);

                            tbl_gallery gl = db.tbl_galleries.First(aa => aa.pst_id == Convert.ToInt16(mod));
                            var glid = db.tbl_galleries.Where(use => use.pst_id == gl.pst_id).ToList();
                            foreach (var row in glid)
                            {
                                row.title = txttitlepost.Text;
                                row.filename = gl.filename;
                                row.date = Convert.ToDateTime(d);
                                row.user_id = Convert.ToInt16(cookie["userid"]);
                                row.pst_id = Convert.ToInt16(mod);
                                db.SubmitChanges();
                            }
                            viewpostcontent();
                            break;
                    }
                }
                else
                {
                    string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
                    // Check that there is a file

                    if (postimgupload.HasFile)
                    {


                        try
                        {
                            //string filename = Path.GetFileName(FileUpload1.FileName);
                            postimgupload.SaveAs(Server.MapPath("../images/oreginal/") + txttitlepost.Text + ".jpg");




                            switch (frm)
                            {
                                case "post":

                                    if (db.f_posttitleExist(txttitlepost.Text) == true)
                                    {
                                        lblrequiredfiled.Text = "<div class='promtederror'>This Title is already Exist..</div>";
                                    }
                                    else
                                    {
                                        db.sp_savepost(txttitlepost.Text, desEditor.Content.ToString(), lblip.Text, "Publish", Dropcategorypost.Text, Convert.ToDateTime(d), txttitlepost.Text + ".jpg", USER.id, 1);

                                        post Postsl = db.posts.First(p => p.pst_title == txttitlepost.Text);
                                        db.tbl_galleries.InsertOnSubmit(new tbl_gallery
                                        {
                                            title = txttitlepost.Text,
                                            filename = txttitlepost.Text + ".jpg",
                                            date = Convert.ToDateTime(d),
                                            user_id = Convert.ToInt16(cookie["userid"]),
                                            pst_id = Postsl.pst_id
                                        });
                                        db.SubmitChanges();
                                        viewpostcontent();
                                    }
                                    break;
                                case "modifypost":
                                    string mod = Request.QueryString["postid"].ToString();
                                    post Post = db.posts.First(p => p.pst_id == Convert.ToInt16(mod));

                                    db.sp_updatepost(Convert.ToInt16(postid.Text), txttitlepost.Text, desEditor.Content.ToString(), Convert.ToDateTime(d), lblip.Text, "Publish", Dropcategorypost.Text, txttitlepost.Text + ".jpg", USER.id);
                                    tbl_gallery gl = db.tbl_galleries.First(aa => aa.pst_id == Convert.ToInt16(mod));
                                    var glid = db.tbl_galleries.Where(use => use.pst_id == gl.pst_id).ToList();
                                    foreach (var row in glid)
                                    {
                                        row.title = txttitlepost.Text;
                                        row.filename = gl.filename;
                                        row.date = Convert.ToDateTime(d);
                                        row.user_id = Convert.ToInt16(cookie["userid"]);
                                        row.pst_id = Convert.ToInt16(mod);
                                        db.SubmitChanges();
                                    }

                                    viewpostcontent();
                                    break;
                            }


                        }
                        catch (Exception ex)
                        {
                            lblrequiredfiled.Text = "<div class='promtederror'>Please Upload Image</div>";
                        }


                    }

                }
     
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        /// <summary>
        /// update post Index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
        /// <summary>
        /// show and new form Post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Paneladdpost.Visible = true;
            Panelviewpost.Visible = false;
            Paneldeletepost.Visible = false;
            MultiView4.ActiveViewIndex = 0;

        }
        /// <summary>
        /// redirect viewpost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancelpost_Click(object sender, EventArgs e)
        {

            Response.Redirect("adminsetup.aspx?pg=post");
            Paneladdpost.Visible = false;
            Panelviewpost.Visible = true;
            GridView1.DataBind();


        }
        /// <summary>
        /// delete post
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelpost_Click(object sender, EventArgs e)
        {
            try
            {
                string frm = Request.QueryString["pg"].ToString();
                switch (frm)
                {
                    case "deletepost":
                        string delpost = Request.QueryString["postid"].ToString();

                         tbl_gallery gl = db.tbl_galleries.First(use => use.pst_id == Convert.ToInt16(delpost));
                        db.tbl_galleries.DeleteOnSubmit(gl);

                        post Postdel = db.posts.First(p => p.pst_id == Convert.ToInt16(delpost));
                        db.posts.DeleteOnSubmit(Postdel);//delete post
                        db.sp_DELETE_tbl_comment(Convert.ToInt16(delpost));//delete comment 
                       
                        db.SubmitChanges();
                        GridView1.DataBind();
                        string file_name = Postdel.Image;
                   
                        string paths = Server.MapPath("../images/oreginal/" + file_name);
                      
                        FileInfo files = new FileInfo(paths);
                        if (files.Exists)
                        {
                          
                            files.Delete();
                        }
                        else if (files.Exists)
                        {
                        }
                        Response.Redirect("adminsetup.aspx?pg=post");
                        break;
                }
            }
            catch (Exception ex)
            {
            }

        }

        protected void btndelpostcancel_Click(object sender, EventArgs e)
        {
            viewpostcontent();
        }
        #endregion



        #region"Setting"
        /// <summary>
        /// hide / show faq
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       

        protected void btnfaq_Click(object sender, EventArgs e)
        {
            if (btnfaq.Text == "view FAQ")
            {
                btnfaq.Text = "hide FAQ";
                MultiView2.ActiveViewIndex = 0;
            }
            else if (btnfaq.Text == "hide FAQ")
            {
                btnfaq.Text = "view FAQ";
                MultiView2.ActiveViewIndex = 1;
            }
        }
        /// <summary>
        /// about us button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnabout_Click(object sender, EventArgs e)
        //{
        //    if (btnabout.Text == "view Aboutus")
        //    {
        //        btnabout.Text = "hide Aboutus";
        //        Button1.Enabled = false;
        //        MultiView2.ActiveViewIndex = 2;
        //    }
        //    else if (btnabout.Text == "hide Aboutus")
        //    {
        //        btnabout.Text = "view Aboutus";
        //        Button1.Enabled = true;
        //        MultiView2.ActiveViewIndex = 1;
        //    }
        //}
        /// <summary>
        /// modify sitename
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button3_Click(object sender, EventArgs e)
        {

            var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
            sitename Sitnme = db.sitenames.First();
            int sitid = Sitnme.id;
            db.sp_modsitename(sitid, txtsitetitle.Text, Editorfaq.Content.ToString(), txtemail.Text, txttagline.Text, Convert.ToDateTime(d), cookie["fullname"], null);
            lblpromted.Text = "<div class='promtedinformation'>" + txtsitetitle.Text + "&nbsp;Successfully save </div>";
        }
        #endregion


        #region"User"
        /// <summary>
        /// user gender
        /// </summary>
        public void user()
        {
            var sption = from s in db.soptions
                         where s.disc == "gender"
                         select s.name;

            if (sption != null)
            {
                drpgender.DataSource = sption.Distinct().ToList();
                drpgender.DataBind();
            }

            var sptionrole = from s in db.soptions
                             where s.disc == "Actype"
                             select s.name;

            droprole.DataSource = sptionrole.Distinct().ToList();
            droprole.DataBind();
        }
        protected void btnuser_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=adduser");

        }

        protected void canceluser_Click(object sender, EventArgs e)
        {
            MultiView3.ActiveViewIndex = 0;
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=user");
        }
        protected void Griduser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName.ToString())
                {
                    case "user_edit":
                        Response.Redirect("adminsetup.aspx?pg=Userview&userid=" + e.CommandArgument.ToString());
                        break;
                    case "MyDelete":
                        Response.Redirect("adminsetup.aspx?pg=deletepost&postid=" + e.CommandArgument.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSaveuser_Click(object sender, EventArgs e)
        {


            if (txtusername.Text == "")
            {
                lblrequiredfiled.Text = "User must be Required ";
            }
            else
            {



                string frm = Request.QueryString["pg"].ToString();
                switch (frm)
                {
                    case "Userview":
                        lblpromteduser.Visible = true;
                        string userview = Request.QueryString["userid"].ToString();
                        int id = Convert.ToInt16(userview);
                        user Usr = db.users.First(u => u.id == id);
                        string idpass = encryptype.psDescrypt(Usr.password.ToString());
                        string profilepic = Usr.Image.ToString();

                        if (txtpass1.Text == "" || txtpass2.Text == "")
                        {
                            if (droprole.Text == "Admin")
                            {
                                if (profilepic == "")
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(3), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {

                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(3), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else if (droprole.Text == "Editor")
                            {

                                if (profilepic == "")
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(5), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(5), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else if (droprole.Text == "Guest")
                            {
                                if (profilepic == "")
                                {

                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(idpass), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else
                            {
                            }

                        }
                        else
                        {
                            if (droprole.Text == "Admin")
                            {
                                if (profilepic == "")
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(3), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(3), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else if (droprole.Text == "Editor")
                            {
                                if (profilepic == "")
                                {

                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(5), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(5), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else if (droprole.Text == "Guest")
                            {
                                if (profilepic == "")
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, null);
                                }
                                else
                                {
                                    db.sp_save_tbl_user(Convert.ToInt16(Label2.Text), txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, Usr.Image);
                                }
                            }
                            else
                            {
                            }
                        }
                        lblpromteduser.Text = "Username " + txtusername.Text + "&nbsp;has&nbsp;Successfully&nbsp;Modify";
                        break;
                    case "adduser":



                        lblpromteduser.Visible = true;
                        if (db.f_usernameExist(txtusername.Text) == true)
                        {
                            lblpromteduser.Text = txtusername.Text + "username already Exist choose other username";
                        }
                        else if (db.f_emailExist(txtemailuser.Text) == true)
                        {
                            lblpromteduser.Text = txtusername.Text + "Email Address already Exist choose other Email Address";
                        }
                        else
                        {



                            if (droprole.Text == "Admin")
                            {
                                db.sp_APPEND_tbl_user(txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(3), txtmname.Text, txtaddress.Text, null);
                            }
                            else if (droprole.Text == "Editor")
                            {
                                db.sp_APPEND_tbl_user(txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(3), drpgender.Text, Convert.ToInt16(5), txtmname.Text, txtaddress.Text, null);
                            }
                            else if (droprole.Text == "Guest")
                            {
                                db.sp_APPEND_tbl_user(txtusername.Text, encryptype.psEncrypt(txtpass2.Text), txtfname.Text, txtlname.Text, txtemailuser.Text, Convert.ToInt16(4), drpgender.Text, Convert.ToInt16(4), txtmname.Text, txtaddress.Text, null);
                            }
                            lblpromteduser.Text = "New username&nbsp;has&nbsp;Successfully&nbsp;Save";
                        }

                        break;
                }

                try
                {

                }
                catch (Exception ex)
                {
                }
            }
        }

        #endregion


        #region"Media"
        /// <summary>
        /// media
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnbckgallery_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=media");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=media");
        }
        /// <summary>
        /// upload gallery list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadAll_Click(object sender, EventArgs e)
        {
            var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string mediaid = Request.QueryString["mediaid"].ToString();
            tbl_gallery gallthum = db.tbl_galleries.First(use => use.gallery_id == Convert.ToInt16(mediaid));

            HttpFileCollection filesColl = Request.Files;
            string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
            // Check that there is a file

            if (FileUpload1.HasFile)
            {
                try
                {
                    string Id = Request.QueryString["id"];


                    FileUpload1.SaveAs(Server.MapPath("../images/goreginal/") + gallthum.filename.Replace(".jpg", "-") + "" + txtdescription.Text + ".jpg");

                    db.gallerylists.InsertOnSubmit(new gallerylist
                     {
                         gallery_id = Convert.ToInt16(mediaid),
                         date = Convert.ToDateTime(d),
                         description = gallthum.filename.Replace(".jpg", "-") + "" + txtdescription.Text,
                         Image = gallthum.filename.Replace(".jpg", "-") + "" + txtdescription.Text+".jpg"

                     });

                    db.SubmitChanges();

                    Response.Redirect("adminsetup.aspx?pg=addgallery&mediaid=" + mediaid);
                }
                catch (Exception ex)
                {

                }
            }
        }
              

        
        //save gallery title
        //protected void Button7_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
             
        //            string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
        //            // Check that there is a file

        //            if (db.f_GallerytitleExist(droptitlepost.Text) == true)
        //            {
        //                Label3.Text = "<div class='promtederror'>This Gallery Title has Already Exist</div>";
        //            }
        //            else
        //            {
        //                if (filegallerimg.PostedFile == null)
        //                {
        //                    Label3.Text = "<div class='promtederror'>Please Select Image</div>";
        //                }
        //                else
        //                {

        //                    //Path to store uploaded files on server - make sure your paths are unique

        //                    string Id = Request.QueryString["id"];

        //                    string filePath = "../images/goreginal/" + droptitlepost.Text + ".jpg";
        //                    string thumbPath = "../images/gthumbs/" + droptitlepost.Text + ".jpg";
        //                    // Check file size (mustn’t be 0)
        //                    HttpPostedFile myFile = filegallerimg.PostedFile;
        //                    int nFileLen = myFile.ContentLength;
        //                    if ((nFileLen > 0) && (System.IO.Path.GetExtension(myFile.FileName).ToLower() == ".jpg"))
        //                    {
        //                        // Read file into a data stream
        //                        byte[] myData = new Byte[nFileLen];
        //                        myFile.InputStream.Read(myData, 0, nFileLen);
        //                        myFile.InputStream.Dispose();

        //                        // Save the stream to disk as temporary file. make sure the path is unique!
        //                        System.IO.FileStream newFile = new System.IO.FileStream(Server.MapPath(filePath + "_temp.jpg"),
        //                                                           System.IO.FileMode.Create);

        //                        newFile.Write(myData, 0, myData.Length);

        //                        // run ALL the image optimisations you want here..... make sure your paths are unique
        //                        // you can use This booleans later if you need the results for your own labels or so.
        //                        // dont call the function after the file has been closed.
        //                        bool success = ResizeImageAndUpload(newFile, thumbPath, 1000, 500);
        //                        success = ResizeImageAndUpload(newFile, filePath, 1200, 900);

        //                        // tidy up and delete the temp file.
        //                        newFile.Close();
        //                        System.IO.File.Delete(Server.MapPath(filePath + "_temp.jpg"));
        //                    }
        //                    imgFilename = Path.GetFileName(thumbPath);
        //                    post Pst = db.posts.First(use => use.pst_title == droptitlepost.Text);
        //                    var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //                    db.sp_savegallery(droptitlepost.Text, imgFilename, Convert.ToDateTime(d), Convert.ToInt16(cookie["userid"]), Pst.pst_id);
        //                    Label3.Text = "";
        //                    Response.Redirect("adminsetup.aspx?pg=media");
        //                }

        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        /// <summary>
        /// refresh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
            user User = db.users.First(aa => aa.id == Convert.ToInt16(cookie["userid"]));
            if (cookie == null)
            {
                Response.Redirect("home.aspx");
            }
            else if (cookie["perms"] == "3")
            {
                var viwgallery = from pst in db.tbl_galleries
                                 select pst;
                ListView1.DataSource = viwgallery;
                ListView1.DataBind();
            }
            else if (cookie["userid"] == Convert.ToString(User.id))
            {
                var viwgallery = from pst in db.tbl_galleries
                                 where pst.user_id == User.id
                                 select pst;
                ListView1.DataSource = viwgallery;
                ListView1.DataBind();
            }
        }
        //protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
        //    try
        //    {

        //        switch (e.CommandName.ToString())
        //        {

        //            case "MyDelete":

        //                tbl_gallery gall = db.tbl_galleries.First(use => use.gallery_id == Convert.ToInt16(e.CommandArgument.ToString()));
        //                if (cookie["userid"] == Convert.ToString(gall.user_id))
        //                {
        //                    db.sp_DELETE_tbl_gallerylist(Convert.ToInt16(e.CommandArgument.ToString()));
        //                    db.sp_DELETE_tbl_gallery(Convert.ToInt16(e.CommandArgument.ToString()));
        //                    GridView3.DataBind();

        //                    string file_name = gall.filename;
        //                    string path = Server.MapPath("../images/gthumbs/" + file_name);
        //                    string paths = Server.MapPath("../images/goreginal/" + file_name);
        //                    FileInfo file = new FileInfo(path);
        //                    FileInfo files = new FileInfo(paths);
        //                    if (files.Exists)
        //                    {
        //                        file.Delete();
        //                        files.Delete();
        //                    }
        //                    else if (files.Exists)
        //                    {
        //                    }
        //                    Response.Redirect("adminsetup.aspx?pg=media");
        //                }
        //                else if (cookie["perms"] == Convert.ToString(3))
        //                {
        //                    db.sp_DELETE_tbl_gallerylist(Convert.ToInt16(e.CommandArgument.ToString()));
        //                    db.sp_DELETE_tbl_gallery(Convert.ToInt16(e.CommandArgument.ToString()));
        //                    GridView3.DataBind();

        //                    string file_name = gall.filename;
        //                    string path = Server.MapPath("../images/gthumbs/" + file_name);
        //                    string paths = Server.MapPath("../images/goreginal/" + file_name);
        //                    FileInfo file = new FileInfo(path);
        //                    FileInfo files = new FileInfo(paths);
        //                    if (files.Exists)
        //                    {
        //                        file.Delete();
        //                        files.Delete();
        //                    }
        //                    else if (files.Exists)
        //                    {
        //                    }
        //                    Response.Redirect("adminsetup.aspx?pg=media");
        //                }
        //                else
        //                {
        //                    mediapromted.Text = "<div class='promtederror'>This gallery is not your own delete attempt stop!</div>";
        //                }




        //                break;

        //            case "Addimages":

        //                tbl_gallery selecgallery = db.tbl_galleries.First(use => use.gallery_id == Convert.ToInt16(e.CommandArgument.ToString()));
        //                if (cookie["userid"] == Convert.ToString(selecgallery.user_id))
        //                {

        //                    Response.Redirect("adminsetup.aspx?pg=addgallery&mediaid=" + e.CommandArgument.ToString());
        //                }
        //                else if (cookie["perms"] == Convert.ToString(3))
        //                {
        //                    Response.Redirect("adminsetup.aspx?pg=addgallery&mediaid=" + e.CommandArgument.ToString());
        //                }
        //                else
        //                {

        //                    mediapromted.Text = "<div class='promtederror'>Oop! you cannot access This gallery because is not your own.</div>";
        //                }


        //                break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        protected void btncanceldelimage_Click(object sender, EventArgs e)
        {

            Response.Redirect("adminsetup.aspx?pg=media");

        }

        protected void brtdelteimage_Click(object sender, EventArgs e)
        {
            string gallid = Request.QueryString["id"].ToString();
            gallerylist gallist = db.gallerylists.First(use => use.id == Convert.ToInt16(gallid));
            db.gallerylists.DeleteOnSubmit(gallist);
            db.SubmitChanges();

            string file_name = gallist.Image;
       
            string paths = Server.MapPath("../images/goreginal/" + file_name);
        
            FileInfo files = new FileInfo(paths);
            if (files.Exists)
            {
              
                files.Delete();
            }
            else if (files.Exists)
            {
            }
            Response.Redirect("adminsetup.aspx?pg=media");
        }


        #endregion

        #region "pages"

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName.ToString())
            {
                case "Show":
                    menu Mneshow = db.menus.First(use => use.menu_id == Convert.ToInt16(e.CommandArgument.ToString()));
                    Mneshow.status = Convert.ToInt16(14);
                    db.SubmitChanges();
                    GridView4.DataBind();
                    break;

                case "Hide":
                    menu Mnehide = db.menus.First(use => use.menu_id == Convert.ToInt16(e.CommandArgument.ToString()));
                    Mnehide.status = Convert.ToInt16(15);
                    db.SubmitChanges();
                    GridView4.DataBind();
                    break;

                case "delete":
                    Response.Redirect("adminsetup.aspx?pg=deletepage&req=" + e.CommandArgument.ToString());

                    break;
                case "Editpages":
                    //  Response.Redirect("adminsetup.aspx?pg=Editpage&req=" + e.CommandArgument.ToString());

                    menu Mnue = db.menus.First(use => use.menu_id == Convert.ToInt16(e.CommandArgument.ToString()));
                    int diss = Convert.ToInt16(Mnue.parent_id);

                    if (diss == Convert.ToInt16(0))
                    {
                        // Response.Redirect("adminsetup.aspx?pg=pages");

                        //MultiView5.ActiveViewIndex = 0;
                        lblpgpromted.Text = "Oops Sorry This page has freeze";
                        btnpgdelete.Visible = false;
                        MultiView1.ActiveViewIndex = 7;
                        MultiView5.ActiveViewIndex = 1;

                    }
                    else
                    {
                        //MultiView1.ActiveViewIndex = 7;
                        //MultiView5.ActiveViewIndex = 2;
                        Response.Redirect("adminsetup.aspx?pg=Editpage&req=" + e.CommandArgument.ToString());

                    }
                    break;
            }
        }
        protected void btnpgdelete_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie

            if (cookie == null)
            {
                Response.Redirect("home.aspx");
            }
            else
            {
                string pageid = Request.QueryString["req"].ToString();
                menu Mnu = db.menus.First(use => use.menu_id == Convert.ToInt16(pageid));
                otherpage Otherpage = db.otherpages.First(use => use.Pagetitle == Mnu.menu_name);

                db.menus.DeleteOnSubmit(Mnu);
                db.otherpages.DeleteOnSubmit(Otherpage);
                db.SubmitChanges();

                Response.Redirect("adminsetup.aspx?pg=pages");

            }

        }

        protected void btnpgcancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=pages");
        }
        //add pages
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=Addpages");
        }


        //cancel
        protected void Button6_Click1(object sender, EventArgs e)
        {
            Response.Redirect("adminsetup.aspx?pg=pages");
        }
        //save pages
        protected void Button8_Click(object sender, EventArgs e)
        {
            try
            {

                string frm = Request.QueryString["pg"].ToString();
                switch (frm)
                {
                    case "Addpages":
                        if (txtpagestitle.Text == "")
                        {
                            lblpromtedpages.Text = "<div class='promtederror'>Required Page title</div>";
                        }
                        else
                        {
                            db.sp_save_tbl_menu(txtpagestitle.Text, "request.aspx?pg=psQKgMQTuHo=&D/OTj7WyY5Y=" + txtpagestitle.Text, Convert.ToInt16(1), Convert.ToInt16(0), Convert.ToInt16(4), Convert.ToInt16(15), Convert.ToInt16(1));
                            db.sp_save_tbl_othpage(txtpagestitle.Text, Editorpages.Content.ToString());
                            lblpromtedpages.Text = "<div class='promtedinformation'>New page successfully save</div>";
                        }
                        break;

                    case "Editpage":
                        if (txtpagestitle.Text == "")
                        {
                            lblpromtedpages.Text = "<div class='promtederror'>Required Page title</div>";
                        }
                        else
                        {
                            db.sp_APPEND_tbl_menu(Convert.ToInt16(lblmenuid.Text), txtpagestitle.Text, "request.aspx?pg=psQKgMQTuHo=&D/OTj7WyY5Y=" + txtpagestitle.Text, Convert.ToInt16(1), Convert.ToInt16(0), Convert.ToInt16(4), Convert.ToInt16(15), Convert.ToInt16(1));
                            db.sp_APPEND_tbl_othpage(Convert.ToInt16(lblotherpageid.Text), txtpagestitle.Text, Editorpages.Content.ToString());
                            lblpromtedpages.Text = "<div class='promtedinformation'>Modify page successfully save</div>";
                        }
                        break;
                }


            }
            catch (Exception ex)
            {

            }

        }
        #endregion

      

       

       

      

    }
}




