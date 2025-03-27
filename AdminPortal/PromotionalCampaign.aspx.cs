using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm24 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Promotional Campaign";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
            if (!IsPostBack)
            {
                string campaignID = Request.QueryString["CampaignID"];
                if (!string.IsNullOrEmpty(campaignID))
                {
                    LoadCampaign(campaignID);
                }
                
            }
        }

        private void LoadCampaign(string campaignID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = @"
        SELECT pc.promotional_campaign_name, pc.discount_percentage, pc.start_date, pc.end_date, pc.promotion_image, cp.product_id
        FROM Promotional_Campaign pc 
        JOIN Campaign_Product cp ON pc.promotional_campaign_id = cp.promotional_campaign_id 
        WHERE pc.promotional_campaign_id = @campaign_id";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@campaign_id", campaignID);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            string productIds = string.Empty;
                            while (reader.Read())
                            {
                                productIds += reader["product_id"].ToString() + ", ";

                                lblPromoTitle.Text = reader["promotional_campaign_name"].ToString();
                                lblProduct.Text = productIds;
                                lblDiscountPercent.Text = reader["discount_percentage"].ToString() + "% discount";  // Fixed to "discount_percentage"
                                lblStartDate.Text = Convert.ToDateTime(reader["start_date"]).ToString("dd/MM/yyyy");
                                lblEndDate.Text = Convert.ToDateTime(reader["end_date"]).ToString("dd/MM/yyyy");

                                string imageUrl = reader["promotion_image"].ToString();
                                imgPromo.ImageUrl = imageUrl.Replace("~", "");  // Replace "~" with root path if necessary
                            }
                        }
                        else
                        {
                            lblPromoTitle.Text = "No promo found for this Campaign ID.";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblPromoTitle.Text = "Error loading promo: " + ex.Message;
                    }
                }
            }
        }
    }
}