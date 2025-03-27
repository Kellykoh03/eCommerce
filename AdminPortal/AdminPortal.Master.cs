using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class AdminPortal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Label1.Text = "Welcome " +Session["fullName"].ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Session["role"] = "";

            Response.Write("<script>alert('Log out sucessfully')</script>");

            Response.Redirect("Homepage.aspx");
        }
    }
}