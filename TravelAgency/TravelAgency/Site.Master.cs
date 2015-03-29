using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TravelAgency
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (LoginStatus.InnerText == "Log In")
            //{
            //    if (Session["Authenticated"] != null)
            //    {
            //        if (Session["Authenticated"].ToString() == "True")
            //        { }
            //        LoginStatus.InnerText = "Log Out";
            //    }
            //    //(new UserBL{} ).AuthenticateUser(user
            //}
            //else
            //{
            //    LoginStatus.InnerText = "Log In";
            //}
            if (Session["LoggedInUser"] != null)
            {
                hlLogin.Text = "Log Out";
                Menu menu = NavigationMenu;
                foreach (MenuItem menuItem in menu.Items)
                {
                    menuItem.Selectable = true;
                }
            }
            else
                hlLogin.Text = "Log In";
        }

        protected void LoginStatus_onclick(object sender, EventArgs e)
        {
            if (Session["LoggedInUser"] != null)
            {
                if (Session["LoggedInUser"] == "True")
                {
                    Session["LoggedInUser"] = null;
                    Response.Redirect("Default.aspx");
                    //LoginStatus.InnerText = "Log In";
                }
            }
        }
    }
}
