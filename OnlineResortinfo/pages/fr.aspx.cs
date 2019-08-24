using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineResortinfo.pages
{
    public partial class fr : System.Web.UI.Page
    {
        databaselinqDataContext db = new databaselinqDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            var soptiondf = from p in db.soptions
                            where p.disc == "category"
                            select p;
            listcategory.DataSource = soptiondf;
            listcategory.DataBind();

            string sr = Request.QueryString["res"].ToString();
            switch (sr)
            {
                case"srh":
                    string con = Request.QueryString["cn"].ToString();
                    MultiView1.ActiveViewIndex = 0;

                    listresort.DataSource = db.sp_searchpost(con);
                    listresort.DataBind();
                    break;
                case"category":
                    string category = Request.QueryString["rd"].ToString();
                    MultiView1.ActiveViewIndex = 1;

                    ListView1.DataSource = db.sp_category_search(Convert.ToInt16(category));
                    ListView1.DataBind();
                    break;
            }
        }
    }
}