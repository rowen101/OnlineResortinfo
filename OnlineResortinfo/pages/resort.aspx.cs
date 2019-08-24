using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo.pages
{
    public partial class resort : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
        public void viewpost()
        {
            var post = from p in db.view_posts
                       where p.status == "Publish"
                       select p;
            listresort.DataSource = post;
            listresort.DataBind();
            MultiView1.ActiveViewIndex = 0;

            var soptiondf = from p in db.soptions
                            where p.disc == "category"
                            select p;
            listcategory.DataSource = soptiondf;
            listcategory.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                viewpost();

                string frm = Request.QueryString["req"].ToString();
              
                switch (frm)
                {
                    case"rd":
                        string pstid = Request.QueryString["id"].ToString();
                        MultiView1.ActiveViewIndex = 1; 
                        view_post vPost = db.view_posts.First(use => use.pst_id == Convert.ToInt16(pstid));
                        imgpost.ImageUrl = "../images/postimg/" + vPost.filename;
                      
                        hreftitle.Text = vPost.pst_title;
                        lblpstinfo.Text = "<div class='info'>[By " + vPost.Username + " on " + vPost.dte + " with " + vPost.comment_count + " Commnets]</div>";
                        lblcontentslcted.Text ="<p>"+ vPost.pst_content+"</p>";

                        gallery Gll = db.galleries.First(aa => aa.pst_id == Convert.ToInt16(pstid));
                        gallerytitle.Text = "Gallery of "+Gll.title.ToString();
                        var lisgall = from p in db.gallerylists
                                      where p.gallery_id == Gll.gallery_id
                                      select p;
                        listgallery.DataSource = lisgall;
                        listgallery.DataBind();
                         
                        var comview = from p in db.viewcomments
                                      where p.pst_id == Convert.ToInt16(pstid)
                                      orderby p.pst_id ascending
                                      select p;
                        listcomment.DataSource = comview;
                        listcomment.DataBind();

                        HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
                     

                        if (cookie == null)
                        {
                            MultiView2.ActiveViewIndex = 0;
                        }
                        else
                        {
                            MultiView2.ActiveViewIndex = 1;
                        }

                         user Usr = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                        Image1.ImageUrl = "../images/profilepic/" + Usr.image;
                        if (db.f_countcomment(Convert.ToInt16(pstid)) != 0)
                        {
                            //lblcountcomment.Text = Convert.ToString((db.f_countcomment(Convert.ToInt16(pstid))));
                           

                        }

                        break;
                }
            }
            catch (Exception ex)
            {

            }
          
        }

        protected void btncomment_Click(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["onlineresort"];//declaration of cookie
            string pstid = Request.QueryString["id"].ToString();
              var d = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (cookie == null)
            {
                if (txtname.Text == "" || txtemail.Text == "" || txtwebsite.Text == "" || txtcomment.Text == "")
                {

                }
                else
                {
                    db.comments.InsertOnSubmit(new comment
                    {
                        name = txtname.Text,
                        c_email = txtemail.Text,
                        website = txtwebsite.Text,
                        c_content = txtcomment.Text,
                        pst_id = Convert.ToInt16(pstid),
                        c_status = 13,
                        date = Convert.ToDateTime(d),
                        profilepic = "default.png"
                    });
                    db.SubmitChanges();
                    Response.Redirect("resort.aspx?req=rd&id=" + pstid);
                }
              
            }
            else
            {
                user Usr = db.users.First(use => use.id == Convert.ToInt16(cookie["userid"]));
                db.comments.InsertOnSubmit(new comment
                {
                    pst_id = Convert.ToInt16(pstid),
                    c_email = Usr.email,
                    c_content = txtusercomment.Text,
                    c_status = 13,
                    date = Convert.ToDateTime(d),
                    user_id = Usr.id
                });
                db.SubmitChanges();
                Response.Redirect("resort.aspx?req=rd&id=" + pstid);
            }
        }
    }
}