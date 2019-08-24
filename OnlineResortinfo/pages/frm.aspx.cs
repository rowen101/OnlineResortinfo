using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace OnlineResortinfo.pages
{
    public partial class frm : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
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
        public void viewselectedmenu()
        {
            string request = Request.QueryString["req"].ToString();
            var soptiondf = from p in db.soptions
                            where p.disc == "category"
                            select p;
            listcategory.DataSource = soptiondf;
            listcategory.DataBind();

            listcategory2.DataSource = soptiondf;
            listcategory2.DataBind();

            switch (request)
            {
                case "gallery":
                    MultiView1.ActiveViewIndex = 0;
                    MultiView2.ActiveViewIndex = 0;
                    lblgallerysitemap.Text = "Gallery";
                    var Gallery = from p in db.galleries
                                  orderby p.gallery_id ascending
                                  select p;
                    listgalleryallbum.DataSource = Gallery;
                    listgalleryallbum.DataBind();
                    break;

                case "galleryslct":
                    if (Request.QueryString["id"] != null)
                    {
                        MultiView1.ActiveViewIndex = 0;
                        MultiView2.ActiveViewIndex = 1;
                        int gallery_id = int.Parse(Request.QueryString["id"]);

                        imggallery.ImageUrl = "galleryallbumview.aspx?imgtype=thumbnail&id=" + gallery_id;
                        gallery sgallery = db.galleries.First(use => use.gallery_id == gallery_id);
                        lbltitlegallery.Text = sgallery.title;
                        lblgallerysitemap.Text = "<a href='frm.aspx?req=gallery'>Gallery</a> > " + sgallery.title;
                        var listgalleryview = from p in db.gallerylists
                                              where p.gallery_id == gallery_id
                                              orderby p.id ascending
                                              select p;
                        listgallery.DataSource = listgalleryview;
                        listgallery.DataBind();
                    }


                    break;

                case "contact":
                    MultiView1.ActiveViewIndex = 1;


                    break;

                case "faq":
                    MultiView1.ActiveViewIndex = 2;
                    sitename Sitefaq = db.sitenames.First(use => use.id == 1);
                    lblfaq.Text = Sitefaq.faq.ToString();
                    break;

                case "about":
                    MultiView1.ActiveViewIndex = 3;
                    sitename Siteabout = db.sitenames.First(use => use.id == 1);
                    lblabout.Text = Siteabout.about.ToString();
                    break;





            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            viewselectedmenu();



        }

    }
    
}