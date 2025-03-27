using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Homepage";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }

            if (!IsPostBack)
            {
                if (Session["LoggedInEmail"] != null)
                {
                    string email = Session["LoggedInEmail"].ToString();
                    int customerId = GetCustomerIdByEmail(email);

                    if (customerId > 0)
                    {
                        // Store the customer ID in the session or use it as needed
                        Session["CustomerID"] = customerId;
                        Session["role"] = "customer";
                        Session["CustomerEmail"] = email;
                    }
                    else
                    {
                        // Handle the case where customer ID is not found
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Customer ID not found.');", true);
                    }
                }

                LoadCategories();
                LoadProducts();
                LoadBanner();
            }
        }

        private int GetCustomerIdByEmail(string email)
        {
            int customerId = -1;

            using (SqlConnection con = new SqlConnection(strcon))
            {
                string query = "SELECT customer_id FROM Customer WHERE customer_email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Assuming customer_id is of type int
                                customerId = Convert.ToInt32(reader["customer_id"]);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: " + ex.Message + "');", true);
                    }
                }
            }

            return customerId;
        }

        private void LoadBanner()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = "SELECT * FROM Promotional_Campaign";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    StringBuilder bannerBuilder = new StringBuilder();

                    bool isActiveSlide = true;

                    while (reader.Read())
                    {
                        string campaignID = reader["promotional_campaign_id"].ToString();
                        string promotionalCampaignImage = reader["promotion_image"].ToString();
                        promotionalCampaignImage = promotionalCampaignImage.Replace("~", "");
                        string campaignName = reader["promotional_campaign_name"].ToString();

                        if (isActiveSlide)
                        {
                            bannerBuilder.Append("<div class='carousel-item active' data-bs-interval='5000'>");
                            isActiveSlide = false;
                        }
                        else
                        {
                            bannerBuilder.Append("<div class='carousel-item' data-bs-interval='5000'>");
                        }
                        bannerBuilder.Append($"<a href='PromotionalCampaign.aspx?CampaignID={campaignID}'>");
                        bannerBuilder.Append($"<img src='{promotionalCampaignImage}' class='d-block w-100' alt='{campaignName}'></a>");
                        bannerBuilder.Append("</div>");
                    }
                    bannerCarousel.InnerHtml = bannerBuilder.ToString();
                }
            }
        }
        private void LoadCategories()
        {
            // Example connection string (update with your own)
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // Query to get the categories
            string query = "SELECT product_category_id, product_category_name, product_category_image FROM Product_Category";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Initialize a string builder to build the rows dynamically
                    StringBuilder categoryBuilder = new StringBuilder();
                    int itemCount = 0; // To track the number of items in a row

                    categoryBuilder.Append("<div class='row'>"); // Start first row

                    while (reader.Read())
                    {
                        string categoryID = reader["product_category_id"].ToString();
                        string categoryName = reader["product_category_name"].ToString();
                        string categoryImage = reader["product_category_image"].ToString().Replace("~","");

                        // Add each category (col-md-3 for 4 items per row)
                        categoryBuilder.Append("<div class='col-md-3'>");
                        categoryBuilder.Append("<div class='categoriesLinkDiv'>");
                        categoryBuilder.Append($"<a href='InCategoriesProduct.aspx?CategoryID={categoryID}'>");
                        categoryBuilder.Append($"<img src='{categoryImage}' class='d-block categoriesLink' alt='{categoryName}' />");
                        categoryBuilder.Append("</a>");
                        categoryBuilder.Append("</div>");
                        categoryBuilder.Append($"<h6 style='margin-top: 10px;'>{categoryName}</h6>");
                        categoryBuilder.Append("</div>");

                        itemCount++;

                        // After 4 items, close the current row and start a new one
                        if (itemCount % 4 == 0)
                        {
                            categoryBuilder.Append("</div>"); // Close the row
                            categoryBuilder.Append("<div class='row'>"); // Start a new row
                        }
                    }

                    // Close the last row
                    if (itemCount % 4 != 0)
                    {
                        categoryBuilder.Append("</div>");
                    }

                    // Add the generated HTML to a container on the page
                    categories.InnerHtml = categoryBuilder.ToString();
                }
            }
        }
        private void LoadProducts()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // Query to get the products (adjust based on your actual database schema)
            string query = "SELECT product_id, product_name, product_unit_price, product_image1 FROM Product";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Initialize a StringBuilder to build the carousel items dynamically
                    StringBuilder carouselBuilder = new StringBuilder();
                    int itemCount = 0; // To track the number of items
                    bool isActiveSlide = true; // To make the first carousel-item active

                    carouselBuilder.Append("<div class='carousel-inner'>");

                    // Loop through the database records and create carousel items
                    while (reader.Read())
                    {
                        string productID = reader["product_id"].ToString();
                        string productName = reader["product_name"].ToString();
                        string productPrice = reader["product_unit_price"].ToString();
                        string productImage = reader["product_image1"].ToString().Replace("~","");

                        // Open a new carousel-item if this is the first item in a new slide
                        if (itemCount % 4 == 0)
                        {
                            if (itemCount > 0) // Close previous slide
                            {
                                carouselBuilder.Append("</div>"); // Close row
                                carouselBuilder.Append("</div>"); // Close carousel-item
                            }

                            // Open a new carousel-item, mark the first one as active
                            if (isActiveSlide)
                            {
                                carouselBuilder.Append("<div class='carousel-item active' data-bs-interval='5000'>");
                                isActiveSlide = false; // Only the first item gets the "active" class
                            }
                            else
                            {
                                carouselBuilder.Append("<div class='carousel-item' data-bs-interval='5000'>");
                            }

                            carouselBuilder.Append("<div class='row'>"); // Start a new row
                        }

                        // Add each product to the carousel (col-md-3 for 4 items per row)
                        carouselBuilder.Append("<div class='col-md-2 mb-3'>");
                        carouselBuilder.Append("<div class='card'>");
                        carouselBuilder.Append($"<a href='ProductDetails.aspx?ProductID={productID}'>");
                        carouselBuilder.Append($"<img src='{productImage}' class='d-block img-fluid' alt='{productName}' />");
                        carouselBuilder.Append("</a>");
                        carouselBuilder.Append("<div class='card-body'>");
                        carouselBuilder.Append($"<h4 class='card-title'>{productName}</h4>");
                        carouselBuilder.Append($"<p class='card-text'>RM {productPrice}</p>");
                        carouselBuilder.Append("</div>");
                        carouselBuilder.Append("</div>");
                        carouselBuilder.Append("</div>");

                        itemCount++;
                    }

                    // Close the last carousel-item and row if there were any products
                    if (itemCount > 0)
                    {
                        carouselBuilder.Append("</div>"); // Close row
                        carouselBuilder.Append("</div>"); // Close carousel-item
                    }

                    carouselBuilder.Append("</div>"); // Close carousel-inner

                    carouselContainer.InnerHtml = carouselBuilder.ToString();
                }
            }
        }
    }
}