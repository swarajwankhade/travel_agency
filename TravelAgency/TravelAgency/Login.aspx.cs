using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BL;

namespace TravelAgency
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoginButton_OnClick(object sender, EventArgs e)
        {
            //if(
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            try
            {
                System.Data.DataTable dt = new UserBL().AuthenticateUser(Password.Text, UserName.Text);
                if (dt.Rows.Count > 0)
                {
                    Session["LoggedInUser"] = dt;
                    FailureText.Text = "";
                    if (this.Master.FindControl("hlLogin") != null)
                    {
                        HyperLink hlLogin = (HyperLink)this.Master.FindControl("hlLogin");
                        hlLogin.Text = "Log Out";
                    }
                    
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session["LoggedInUser"] = null;
                    FailureText.Text = "Invalid User.";
                }
            }
            catch (Exception ex)
            { }
            
        }
    }
}
