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
    public partial class WebForm8 : System.Web.UI.Page
    {
        private string RamValue
        {
            get
            {
                return ViewState["RamValue"] != null ? ViewState["RamValue"].ToString() : string.Empty;
            }
            set
            {
                ViewState["RamValue"] = value;
            }
        }

        private string RomValue
        {
            get
            {
                return ViewState["RomValue"] != null ? ViewState["RomValue"].ToString() : string.Empty;
            }
            set
            {
                ViewState["RomValue"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Product Details";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }

            if (!IsPostBack)
            {
                string productId = Request.QueryString["ProductID"];
                if (!string.IsNullOrEmpty(productId))
                {
                    int productIdValue;
                    if (int.TryParse(productId, out productIdValue))
                    {
                        // Example: Manually set the parameter for SqlDataSource
                        SqlDataSource1.SelectParameters["ProductID"].DefaultValue = productIdValue.ToString();
                        SqlDataSource2.SelectParameters["ProductID"].DefaultValue = productIdValue.ToString();
                        SqlDataSource3.SelectParameters["ProductID"].DefaultValue = productIdValue.ToString();
                    }

                    // Call a method to retrieve the product details using productId
                    LoadProductDetails(productId);
                }
            }
        }

        private void LoadProductDetails(string productId)
        {
            // Connection string
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            // SQL query to fetch product details based on product ID
            string query = "SELECT product_name, product_description, product_unit_price, product_availability, product_image1, product_image2, product_image3, product_image4, specification_type_name, specification_value " +
                "FROM Product p JOIN Product_Specification ps " +
                "ON p.product_id = ps.product_id JOIN Specification_Type st " +
                "ON ps.specification_type_id = st.specification_type_id " +
                "WHERE p.product_id = @ProductID";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Adding the parameter for product ID
                    cmd.Parameters.AddWithValue("@ProductID", productId);

                    try
                    {
                        // Open the connection
                        con.Open();

                        // Execute the query and get the product details
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // controls to display the product details
                                lblProdName.Text = reader["product_name"].ToString();
                                lblProdName2.Text = reader["product_name"].ToString();
                                //lblProductDescription.Text = reader["product_description"].ToString();
                                imgProduct1.ImageUrl = reader["product_image1"].ToString();
                                imgProduct2.ImageUrl = reader["product_image2"].ToString();
                                imgProduct3.ImageUrl = reader["product_image3"].ToString();
                                imgProduct4.ImageUrl = reader["product_image4"].ToString();
                                imgProductIndicator1.ImageUrl = reader["product_image1"].ToString();
                                imgProductIndicator2.ImageUrl = reader["product_image2"].ToString();
                                imgProductIndicator3.ImageUrl = reader["product_image3"].ToString();
                                imgProductIndicator4.ImageUrl = reader["product_image4"].ToString();
                                lblPrice.Text = "RM " + reader["product_unit_price"].ToString();

                                // Check the product_availability and set the label text accordingly
                                string availability = reader["product_availability"].ToString();
                                lblAvailability.Text = availability == "Yes" ? "Availability: In Stock" : "Availability: Out of Stock";

                                int customerId = Convert.ToInt32(Session["CustomerID"]);
                            }
                        }
                        else
                        {
                            // Handle case where no product is found for the given ID
                            lblProdName.Text = "Product not found!";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur during the database operation
                        lblProdName.Text = "Error retrieving product details: " + ex.Message;
                    }
                }
            }
        }

        protected void btnColor_Click(object sender, EventArgs e)
        {
            // Find the button that was clicked
            Button clickedButton = (Button)sender;

            // Get the value from the CommandArgument
            string specificationValue = clickedButton.CommandArgument;

            // Example: Set the text of a Label control to the clicked value
            lblColor.Text = "Color: " + specificationValue;
        }

        protected void btnMemory1_Click(object sender, EventArgs e)
        {
            // Find the button that was clicked
            Button clickedButton = (Button)sender;

            // Get the value from the CommandArgument
            RamValue = clickedButton.CommandArgument;


            updateMemoryLabel();
        }

        protected void btnMemory2_Click(object sender, EventArgs e)
        {
            // Find the button that was clicked
            Button clickedButton = (Button)sender;

            // Get the value from the CommandArgument
            RomValue = clickedButton.CommandArgument;


            updateMemoryLabel();
        }

        private void updateMemoryLabel()
        {
            lblMemory.Text = "Memory: " + RamValue + "GB RAM + " + RomValue + "GB SSD";
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (Session["CustomerID"] == null || Convert.ToInt32(Session["CustomerID"]) <= 0)
            {
                Response.Redirect("LoginCustomer.aspx");
            }

            string conStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            Button clickedButton = (Button)sender;

            string productId = Request.QueryString["ProductID"];

            string prodName = lblProdName.Text;
            string prodColor = lblColor.Text;
            string getProdMemory = lblMemory.Text.Replace("Memory: ", "").Trim();
            string prodMemory = getProdMemory;
            int productQty = int.Parse(lblQtyProduct.Text);

            string getColor = prodColor.Replace("Color: ", "").Trim();
            string getPrice = lblPrice.Text;

            string deleteExtraWordPrice = getPrice.Replace("RM ", "").Trim();

            int price = int.Parse(deleteExtraWordPrice);

            string imageUrl = imgProduct1.ImageUrl;

            string cartItemID = $"{prodName}, {getColor}, {prodMemory}, {productId}";
            string cartItemName = $"{prodName}, {getColor}, {prodMemory}, {productId}";

            // Prepare the SQL command
            string updateQuery = @"
                                UPDATE Cart 
                                SET item_quantity = item_quantity + @item_quantity 
                                WHERE cart_item_id = @product_id AND customer_id = @customer_id";

            string insertQuery = @"
                            INSERT INTO Cart (cart_item_id, cart_item_name, item_quantity, item_price, customer_id, cart_item_image)
                            VALUES (@product_id, @cart_item_name, @item_quantity, @item_price, @customer_id, @cart_item_image)";

            // Execute the command
            using (SqlConnection conn = new SqlConnection(conStr))
            {
                conn.Open();

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@product_id", cartItemID);
                    updateCmd.Parameters.AddWithValue("@item_quantity", productQty);
                    updateCmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@product_id", cartItemID);
                            insertCmd.Parameters.AddWithValue("@cart_item_name", cartItemName);
                            insertCmd.Parameters.AddWithValue("@item_quantity", productQty);
                            insertCmd.Parameters.AddWithValue("@item_price", price);
                            insertCmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);
                            insertCmd.Parameters.AddWithValue("@cart_item_image", imageUrl);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void btnProductPlus_Click(object sender, EventArgs e)
        {
            int productQty = int.Parse(lblQtyProduct.Text);
            lblQtyProduct.Text = (productQty + 1).ToString();
        }

        protected void btnProductMinus_Click(object sender, EventArgs e)
        {
            int productQty = int.Parse(lblQtyProduct.Text);
            if (productQty > 1)
            {
                lblQtyProduct.Text = (productQty - 1).ToString();
            }
        }
    }
}