using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace online_adds.pages
{
    public partial class frm : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        public void category()
        {
            var categorylist = from pst in db.soptions
                           where pst.disc == "category"
                           orderby pst.name ascending
                           select pst;
            Listcategory.DataSource = categorylist;
            Listcategory.DataBind();
        }
        /// <summary>
        /// view gallery
        /// </summary>
        public void viewgallery()
        {
            var categorylist = from pst in db.tbl_galleries
                               orderby pst.gallery_id ascending
                               select pst;
            listviewgallery.DataSource = categorylist;
            listviewgallery.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = new HttpCookie("rowenref");
            try
            {
                category();
                string frm = Request.QueryString["qZcZSFkBxBw"].ToString();

                switch (frm)
                {
                    case "iQkJHCUtPcA=":
                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = 0;
                        lblslct_title.Text = "Gallery";
                      
                        lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>Gallery</li></ol>";
                        Panelsidebar.Visible = false;
                        viewgallery();
                        break;
                        //selected gallery
                    case "Fa7v03FdlaUM7Tb4YLoWLg=":
                        string id = Request.QueryString["3FE8tKkfmgY"].ToString();
                        tbl_gallery gall = db.tbl_galleries.First(use=>use.gallery_id== Convert.ToInt16(id));
                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = 1;
                        lblslct_title.Text = "Gallery";

                        Label1.Text = "<a target='_blank' href='../images/goreginal/" + gall.filename + "'><img class='img-responsive' src='../images/gthumbs/" + gall.filename + "'</a>";
                       
                        lblsitemap.Text = "<ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li><a href='request.aspx?qZcZSFkBxBw=iQkJHCUtPcA='>Gallery</a></li><li class='active'>" + gall.title + "</li></ol>";
                        lblselectedgallery.Text = id.ToString();
                         var galleryselected = from pst in db.gallerylists
                                               where pst.gallery_id == Convert.ToInt16(id)
                                               orderby pst.gallery_id ascending
                                               select pst;
                         listgalleryslcted.DataSource = galleryselected;
                         listgalleryslcted.DataBind();
                         Panelsidebar.Visible = false;
                        break;
                    case "ZFyBy4NRXDY=":
                        MultiView1.ActiveViewIndex = 1;
                        lblslct_title.Text = "Contact";
                       lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>Contact</li></ol>";
                        user User = db.users.First(use=>use.id == Convert.ToInt16(cookie["userid"]));
          
                        break;

                    case "6V8tKqNxDZg=":
                        MultiView1.ActiveViewIndex = 2;
                        lblslct_title.Text = "FAQ";
                        smltooltip.Text = "<small>Frequently Asked Questions</small>";
                        lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>FAQ</li></ol>";
                        Panelsidebar.Visible = false;
                        sitename Stename = db.sitenames.First();
                        lblfaq.Text = Stename.faq.ToString();
                        break;

                    case "Fcl56ePHIPM=":
                        MultiView1.ActiveViewIndex = 3;
                        lblslct_title.Text = "Account";
                        lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>Account</li></ol>";
                        break;

                    case "M/P0MxbAsE8=":
                        MultiView1.ActiveViewIndex = 4;
                         
                        
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Add(cookie);
                        Response.Redirect("home.aspx");
                        break;

                    case "qIV2NKe/Bco=":
                        MultiView1.ActiveViewIndex = 5;
                         lblslct_title.Text = "About us";
                         lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>About us</li></ol>";
                        Panelsidebar.Visible = false;
                        sitename Stname = db.sitenames.First(p => p.id == 1);
                        lblaboutus.Text = Stname.about.ToString();
                        break;

                    case "psQKgMQTuHo=":
                        MultiView1.ActiveViewIndex = 5;
                        string otherpage = Request.QueryString["D/OTj7WyY5Y"].ToString();
                        otherpage other = db.otherpages.First(use => use.Pagetitle == otherpage);
                        lblslct_title.Text = other.Pagetitle;
                        lblsitemap.Text = " <ol class='breadcrumb'><li><a href='home.aspx'>Home</a></li><li class='active'>dfsf</li></ol>";
                        lblaboutus.Text = other.body;
                        Panelsidebar.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
      
        /// <summary>
        /// Submetted Feedback message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                HttpCookie cookie = new HttpCookie("rowenref");

              
                    sitename Sitnme = db.sitenames.First();

                    SmtpClient client = new SmtpClient("smtp.live.com", 587);
                    client.EnableSsl = true;
                    client.Timeout = 10000;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("growen@live.com", "123gonzales");
                    MailMessage msg = new MailMessage();
                    msg.To.Add("rgrowengonzales66@gmail.com");
                    msg.From = new MailAddress("rgrowengonzales66@gmail.com");
                    msg.Subject = "Online Information";
                    msg.Body = "From " + txtname.Text + " Using " + Sitnme.title.ToString() + " Message: " + txtbody.Text + " " + txtemail.Text;
                    client.Send(msg);
                    lblmsgbox.Text = "<span class='promtedinformation'>Your Messaged has Successfully sent</span>";

                    Page.Title = string.Format(Sitnme.title.ToString());
                    txtname.Text = "";
                    txtemail.Text = "";
                    txtbody.Text = "";
             
                
                

              
            }
            catch (Exception ex)
            {
                lblmsgbox.Text = "<span class='promtedinformation'>Your Messaged has Successfully sent</span>";
                sitename Sitnme = db.sitenames.First();
                Page.Title = string.Format(Sitnme.title.ToString());
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (txtsearchblg.Text == "")
            {
            }
            else
            {
                Response.Redirect("Destanation.aspx?frm=srh&cn=" + txtsearchblg.Text);
            }
        }
       
    }
}

