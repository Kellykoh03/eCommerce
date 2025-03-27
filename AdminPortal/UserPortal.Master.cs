using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class UserPortal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["role"] == null)
            //{
            //    Session["role"] = "";
            //}

            if (Session["role"] == "" || Session["role"] == null)
            {

                Button1.Visible = true;
            }
            else if (Session["role"] == "customer")
            {

                Button1.Visible = false;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("CustomerAccount.aspx");
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["CustomerID"] != null && Convert.ToInt32(Session["CustomerID"]) > 0)
            {
                // Redirect to CustomerAccount if CustomerID is greater than 0
                Response.Redirect("CustomerAccount.aspx");
            }
            else
            {
                // Otherwise, redirect to LoginCustomer
                Response.Redirect("LoginCustomer.aspx");
            }
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string productName = txtSearch.Text.Trim();

            Response.Redirect("~/InCategoriesProduct.aspx?ProductName=" + Server.UrlEncode(productName));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginAdmin.aspx");
        }
    }
}