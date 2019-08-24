using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace OnlineResortinfo
{
    public class galleryphotohelper
    {
        public static Image GetThumbnail(int photoid)
        {
            string sql = "SELECT IMAGE,THUMBNAILHEIGHT,THUMBNAILWIDTH FROM tbl_gallery WHERE gallery_ID=@id";
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@id", photoid);
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, p);
            byte[] imagedata = null;
            int height = 0, width = 0;
            while (reader.Read())
            {
                imagedata = (byte[])reader.GetValue(0);
                height = reader.GetInt32(1);
                width = reader.GetInt32(2);
            }
            reader.Close();
            MemoryStream ms = new MemoryStream(imagedata);
            Image bigImage = Image.FromStream(ms);
            Image smallImage = bigImage.GetThumbnailImage(width, height, null, IntPtr.Zero);
            return smallImage;
        }

        public static Image GetThumbnailForAlbum(int albumid)
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@albumid", albumid);
            int photoid = int.Parse(SqlHelper.ExecuteScalar("SELECT TOP 1 ID FROM tbl_gallerylist WHERE GALLERY_ID=@albumid ORDER BY DATE DESC", p).ToString());
            return GetThumbnail(photoid);
        }

        public static Image GetPhoto(int photoid)
        {
            string sql = "SELECT IMAGE FROM tbl_gallery WHERE gallery_id=@id";
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@id", photoid);
            SqlDataReader reader = SqlHelper.ExecuteReader(sql, p);
            byte[] imagedata = null;
            while (reader.Read())
            {
                imagedata = (byte[])reader.GetValue(0);
            }
            reader.Close();
            MemoryStream ms = new MemoryStream(imagedata);
            Image bigImage = Image.FromStream(ms);
            return bigImage;
        }
    }
}