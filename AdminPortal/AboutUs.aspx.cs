﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm21 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "About Us";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }
    }
}