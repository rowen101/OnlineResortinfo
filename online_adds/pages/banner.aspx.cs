using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace online_adds.pages
{
    public partial class bannerdel : System.Web.UI.Page
    {
        databaseDataContext db = new databaseDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sitename Sitnme = db.sitenames.First();

                Page.Title = string.Format(Sitnme.title.ToString()) + " Banner";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch(e.CommandName.ToString())
            {
                case "Deleteid":

                    homebanner bann = db.homebanners.First(use => use.ID == Convert.ToInt16(e.CommandArgument.ToString()));
                    string file_name = bann.filename;
                    string path = Server.MapPath("../images/banner/" + file_name);
                    FileInfo file = new FileInfo(path);
         
                    if (file.Exists)
                    {
                        file.Delete();
                       
                    }
                    else if (file.Exists)
                    {
                    }
                    
                    db.homebanners.DeleteOnSubmit(bann);
                    db.SubmitChanges();
                    GridView1.DataBind();
                    break;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            //check to make sure a file is selected
            if (FileUpload1.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    FileUpload1.SaveAs(Server.MapPath("../images/banner/") + filename);

                    db.homebanners.InsertOnSubmit(new homebanner
                    {
                        filename = FileUpload1.FileName,
                        description = TextBox1.Text,
                        status = 1
                    });
                    db.SubmitChanges();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error " + ex.ToString();
                }
            }
            sitename Sitnme = db.sitenames.First();

            Page.Title = string.Format(Sitnme.title.ToString()) + " Banner";
         
        }
    }
}