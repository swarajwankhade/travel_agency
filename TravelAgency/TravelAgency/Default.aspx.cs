using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TravelAgency
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                DataTable dt = (DataTable)Session["LoggedInUser"];

                lblUser.Text = "Welcome to Mr./Mrs. " + dt.Rows[0]["user_name"].ToString() + "!";
            }
        }
    }
}
