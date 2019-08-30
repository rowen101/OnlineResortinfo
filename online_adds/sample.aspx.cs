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
namespace online_adds
{
    public partial class sample : System.Web.UI.Page
    {
        clssecurity classsecuryty = new clssecurity();
     
       
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

       
        protected void Button1_Click(object sender, EventArgs e)
        {

          Label1.Text = classsecuryty.psEncrypt(TextBox1.Text);
          
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
        protected void Button2_Click(object sender, EventArgs e)
        {
            string sub = string.Empty, imagePath = string.Empty, imgFilename = string.Empty;
            if (FileUpload1.PostedFile != null)
            {

                //Path to store uploaded files on server - make sure your paths are unique
                DateTime uid = new DateTime();
                string Id = Request.QueryString["id"];
                string filePath = "img_item/" + TextBox2.Text;
                string thumbPath = "img_itemthumb/" + TextBox2.Text;

                // Check file size (mustn’t be 0)
                HttpPostedFile myFile = FileUpload1.PostedFile;
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
                    bool success = ResizeImageAndUpload(newFile, thumbPath, 1200, 700);
                    success = ResizeImageAndUpload(newFile, filePath, 1200,900);

                    // tidy up and delete the temp file.
                    newFile.Close();
                    System.IO.File.Delete(Server.MapPath(filePath + "_temp.jpg"));
                }
                imgFilename = Path.GetFileName(thumbPath);
            }
            //if (FileUpload1.PostedFile != null)
            //{
            //    DateTime uid = new DateTime();
            //    sub = "img_item/" + (uid.ToLongTimeString() + "-" + uid.ToLongDateString() + "-" + FileUpload1.PostedFile.FileName).Replace(",", "").Replace("\\", "").Replace("/", "").Replace(":", "");
            //    imagePath = Server.MapPath(sub);
            //    FileUpload1.PostedFile.SaveAs(imagePath);


            //    imgFilename = Path.GetFileName(sub);
            //}
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Label2.Text = classsecuryty.psDescrypt(TextBox3.Text);
        }

       

      
    }
}