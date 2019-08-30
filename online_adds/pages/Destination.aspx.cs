using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace online_adds.pages
{
    public partial class Blog : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        string read;


        public void blogss()
        {
            var post = from pst in db.viewposts
                       where pst.status == "Publish"
                       orderby pst.dte ascending
                       select pst;



            ListView_blog.DataSource = post;
            ListView_blog.DataBind();

            string YourText = lblcontent.Text;
            if (YourText.Length > 50)
            {
                YourText = YourText.Substring(0, 50);
                YourText += "<a href=''>... Read more</a>";
                Label1.Text = YourText.ToString();
            }

            var category = from pst in db.soptions
                           where pst.disc == "category"
                           orderby pst.name ascending
                           select pst;
            Listcategory.DataSource = category;
            Listcategory.DataBind();
        }
        public void sitemap()
        {

            lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>Destination</li></ol>";
            MultiView1.ActiveViewIndex = 0;
        }
        protected void Page_Load(object sender, EventArgs e)
        {


            this.blogss();
            this.sitemap();
            try
            {
                string frm = Request.QueryString["frm"].ToString();
                switch (frm)
                {
                    case "read":


                        read = Request.QueryString["id"].ToString();
                        viewpost vPost = db.viewposts.First(use => use.pst_id == Convert.ToInt16(read));
                        lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li><a href='Destination.aspx'>Destination</a></li><li class='active'>" + vPost.pst_title + "</li></ol>";
                        lbldate.Text = Convert.ToString(vPost.dte);
                        lbltitle.Text = vPost.pst_title;
                        lblrestitle.Text = vPost.pst_title;
                        lblcontent.Text = vPost.pst_content;
                        lblauthor.Text  = vPost.Username;
                        lblpostimage.Text = "<a href='../images/oreginal/" + vPost.Image + "'><img class='img-responsive' src='../images/oreginal/" + vPost.Image + "'/></a>";


                        HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
                         if (cookie == null)
                        {
                           // imguser.ImageUrl = "../profilepic/thumb/default.png";
                            Panel1.Visible = true;
                            Panel2.Visible = false;

                        }
                        else
                        {
                            user User = db.users.First(use => use.username == cookie["username"]);
                            imguserlogin.ImageUrl = "../profilepic/thumb/"+User.Image;
                            Panel1.Visible = false;
                            Panel2.Visible = true;

                        }

                        //sider
                       tbl_gallery gall = db.tbl_galleries.First(use=> use.pst_id == Convert.ToInt16(read));
                       lblgallerytitle.Text = gall.title + " Gallery";
                        var slider = from slide in db.gallerylists
                                     where slide.gallery_id == gall.gallery_id
                                     select slide;

                        listslider.DataSource = slider;
                        listslider.DataBind();
                        ////end slider
                        //var con = db.sp_selected_tbl_comment(Convert.ToInt16(read), Convert.ToInt16(13));
                                                var con = from pst in db.view_comments
                                                          where pst.pst_id == Convert.ToInt16(read)
                                                          where pst.c_status == "Approved"
                                                          select pst;
                        //view_comment vComment = db.view_comments.First(use => use.pst_id == Convert.ToInt16(read));
                        ListView1.DataSource = con;
                        ListView1.DataBind();
                        MultiView1.ActiveViewIndex = 1;

                        comment Cmt = db.comments.First(use => use.c_status == Convert.ToInt16(13));

                        if (db.f_countcomment(Convert.ToInt16(read)) != 0)
                        {
                            Label1.Text = Convert.ToString((db.f_countcomment(Convert.ToInt16(read)))) + " Comment";
                            lblres.Text = Convert.ToString((db.f_countcomment(Convert.ToInt16(read))));

                        }
                        else
                        {
                            Label1.Text = "0 Comment";
                            lblres.Text = "No ";
                        }

                        break;


                    case "srh":
                        string srcs = Request.QueryString["cn"].ToString();

                       
                            var viewsearch = db.sp_search_post(srcs.ToString());
                            ListView_blog.DataSource = viewsearch;
                            ListView_blog.DataBind();
                     
                        
                       
                        break;

                   
                }
            }
            catch (Exception ex)
            {

            }




        }

        protected void lnkhome_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }

        protected void lnkblog_Click(object sender, EventArgs e)
        {
            Response.Redirect("Distination.aspx");
        }
        //save comment
        protected void btnpublis_Click(object sender, EventArgs e)
        {
            

                HttpCookie cookie = Request.Cookies["rowenref"];// declaration cookie
                var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (cookie == null)
                {

                }
                else
                {
                    if (txtusercomment.Text == "")
                    {

                    }
                    else
                    {
                        user USER = db.users.First(use => use.username == cookie["Username"]);
                        db.sp_save_tbl_comment(Convert.ToInt16(read), txtusercomment.Text, Convert.ToDateTime(d), null, null, Convert.ToInt16(13), USER.id);
                        txtusercomment.Text = "";
                        Response.Redirect("Destination.aspx?frm=read&id=" + Convert.ToInt16(read) + "#ycomment");
                    }
                }

         }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
        
            if (txtsearchblg.Text == "")
            {
            }
            else
            {
                Response.Redirect("Destination.aspx?frm=srh&cn=" + txtsearchblg.Text);
            }
        }

        protected void lnklogin_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["rowenref"];
            HttpCookie pinding = Request.Cookies["rowenpinding"];

            if (cookie == null)
            {
                pinding = new HttpCookie("rowenpinding");
                pinding["Jc07M5Ieg78"] = Request.QueryString["id"].ToString();
                Response.Cookies.Add(pinding);
                Response.Redirect("req.aspx?pg=login");
            }
            else
            {
            }
        }

       
    }
}
