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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Checkout";

            if (!IsPostBack)
            {
                Repeater1.DataBind();
                LoadCustomerData();
                decimal totalPayment = CalculateTotalPayment();
                lblTotal.Text = "RM " + totalPayment.ToString("F2");
            }

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }

        private void LoadCustomerData()
        {
            // Assuming you get the customer_id from session or query string
            int customerId = Convert.ToInt32(Session["CustomerID"]);
            string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT customer_password, customer_email, customer_name, customer_phone_number, customer_birthday, customer_address, customer_post_code, customer_country, customer_city FROM Customer WHERE customer_id = @customer_id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@customer_id", customerId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    // Populate the textboxes with user data
                    txtAddress.Text = reader["customer_address"].ToString();
                    txtCity.Text = reader["customer_city"].ToString();
                    txtPostCode.Text = reader["customer_post_code"].ToString();
                    txtState.Text = reader["customer_country"].ToString();
                }
                conn.Close();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // Get the current data item (row) and the cart_item_name value
                string cartItemName = DataBinder.Eval(e.Item.DataItem, "cart_item_name").ToString();

                // Split the cart_item_name using the comma separator
                string[] cartItemDetails = cartItemName.Split(',');

                // Assuming the memory storage is the third value
                if (cartItemDetails.Length >= 3)
                {
                    string memoryStorage = cartItemDetails[2].Trim();

                    // Find the Label for memory storage and set its text
                    Label lblMemory = (Label)e.Item.FindControl("lblMemory");
                    if (lblMemory != null)
                    {
                        lblMemory.Text = "Memory: " + memoryStorage;
                    }
                }
            }
        }

        private decimal CalculateTotalPayment()
        {
            decimal totalPayment = 0;
            decimal discountPercentage = 0;

            foreach (RepeaterItem item in Repeater1.Items)
            {
                Label lblQty = (Label)item.FindControl("lblQty");
                Label lblProdTitle = (Label)item.FindControl("lblProdTitle");
                Label lblPrice = (Label)item.FindControl("lblPrice");

                if (lblQty != null && lblProdTitle != null && lblPrice != null)
                {
                    // Extract product quantity
                    string getTotalQty = lblQty.Text.Replace("Qty: ", "").Trim();
                    int qty = Convert.ToInt32(getTotalQty);

                    // Extract price
                    string getPrice = lblPrice.Text.Replace("RM ", "").Trim();
                    decimal price = Convert.ToDecimal(getPrice) / qty;

                    // Get the product ID (assuming it's in the product title)
                    string productTitle = lblProdTitle.Text;
                    int productId = Convert.ToInt32(productTitle.Split(',').Last().Trim());

                    // Check for any campaign discount
                    string checkCampaignQuery = @"
            SELECT pc.discount_percentage 
            FROM Campaign_Product cp
            JOIN Promotional_Campaign pc ON cp.promotional_campaign_id = pc.promotional_campaign_id
            WHERE cp.product_id = @ProductID 
            AND pc.start_date <= @CurrentDate AND pc.end_date >= @CurrentDate";

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(checkCampaignQuery, conn);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                        conn.Open();
                        object discount = cmd.ExecuteScalar();
                        if (discount != null)
                        {
                            discountPercentage = Convert.ToDecimal(discount);
                        }
                        conn.Close();
                    }

                    // Calculate discounted price
                    decimal totalPrice = price * qty;
                    if (discountPercentage > 0)
                    {
                        totalPrice = totalPrice - (totalPrice * discountPercentage / 100);
                    }

                    totalPayment += totalPrice;
                }
            }

            return totalPayment;
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            string orderDate = DateTime.Now.ToString();
            string orderStatus = "Pending";
            string shippingAddress = txtAddress.Text;
            string shippingCity = txtCity.Text;
            string shippingPostalCode = txtPostCode.Text;
            string shippingCountry = txtState.Text;

            int customerID = Convert.ToInt32(Session["CustomerID"]);
            int adminID = 3;
            decimal totalPayment = 0;
            List<int> productIDs = new List<int>();
            List<string> productNames = new List<string>();
            List<int> productQuantities = new List<int>();
            List<decimal> discountPercent = new List<decimal>();
            List<decimal> productPrices = new List<decimal>();

            decimal discountPercentage = 0;

            // Retrieve product IDs, names, quantities, and prices
            foreach (RepeaterItem item in Repeater1.Items)
            {
                Label lblQty = (Label)item.FindControl("lblQty");
                Label lblProdTitle = (Label)item.FindControl("lblProdTitle");
                Label lblPrice = (Label)item.FindControl("lblPrice");

                if (lblQty != null && lblProdTitle != null && lblPrice != null)
                {
                    // Extract product quantity
                    string getTotalQty = lblQty.Text;
                    string excludeQtyTotalPayment = getTotalQty.Replace("Qty: ", "").Trim();
                    int qty = Convert.ToInt32(excludeQtyTotalPayment);

                    // Extract product name
                    string productName = lblProdTitle.Text.Trim();

                    // Extract price
                    string getPrice = lblPrice.Text;
                    string excludeRMPrice = getPrice.Replace("RM ", "").Trim();
                    decimal price = Convert.ToDecimal(excludeRMPrice) / qty;

                    string productTitle = lblProdTitle.Text;
                    string[] productDetails = productTitle.Split(',');
                    int productId = Convert.ToInt32(productDetails.Last().Trim());

                    string checkCampaignQuery = @"
                SELECT pc.discount_percentage 
                FROM Campaign_Product cp
                JOIN Promotional_Campaign pc ON cp.promotional_campaign_id = pc.promotional_campaign_id
                WHERE cp.product_id = @ProductID 
                AND pc.start_date <= @CurrentDate AND pc.end_date >= @CurrentDate";

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand(checkCampaignQuery, conn);
                        cmd.Parameters.AddWithValue("@ProductID", productId);
                        cmd.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

                        conn.Open();
                        object discount = cmd.ExecuteScalar();
                        if (discount != null)
                        {
                            discountPercentage = Convert.ToDecimal(discount);
                        }
                        conn.Close();
                    }

                    string totalPriceText = lblPrice.Text.Replace("RM ", "").Trim();
                    decimal totalPrice = Convert.ToDecimal(totalPriceText);

                    if (discountPercentage > 0)
                    {
                        totalPrice = totalPrice - (totalPrice * discountPercentage / 100);
                    }

                    // Add to total payment
                    totalPayment += Convert.ToDecimal(totalPrice);

                    // Add to lists
                    productIDs.Add(Convert.ToInt32(lblProdTitle.Text.Split(',').Last().Trim())); // Assuming product ID is in lblProdTitle text
                    productNames.Add(productName);
                    productQuantities.Add(qty);
                    productPrices.Add(price);
                    discountPercent.Add(discountPercentage);
                }
            }

            string insertOrderFormQuery = @"
    INSERT INTO Order_Form (order_date, customer_id, order_status, shipping_address, shipping_city, shipping_postal_code, shipping_country, admin_id, payment_total_amount)
    VALUES (@OrderDate, @CustomerID, @OrderStatus, @ShippingAddress, @ShippingCity, @ShippingPostalCode, @ShippingCountry, @AdminID, @PaymentTotalAmount)";

            string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(con))
            {
                SqlCommand cmd = new SqlCommand(insertOrderFormQuery, conn);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                cmd.Parameters.AddWithValue("@OrderStatus", orderStatus);
                cmd.Parameters.AddWithValue("@ShippingAddress", shippingAddress);
                cmd.Parameters.AddWithValue("@ShippingCity", shippingCity);
                cmd.Parameters.AddWithValue("@ShippingPostalCode", shippingPostalCode);
                cmd.Parameters.AddWithValue("@ShippingCountry", shippingCountry);
                cmd.Parameters.AddWithValue("@AdminID", adminID);
                cmd.Parameters.AddWithValue("@PaymentTotalAmount", totalPayment);

                conn.Open();
                cmd.ExecuteNonQuery();

                string getLastOrderFormIDQuery = "SELECT MAX(order_form_id) FROM Order_Form";

                SqlCommand getLastOrderCmd = new SqlCommand(getLastOrderFormIDQuery, conn);

                int orderFormID = Convert.ToInt32(getLastOrderCmd.ExecuteScalar());

                string insertOrderItemQuery = @"
                INSERT INTO Order_Item (product_id, order_form_id, order_item_quantity, order_item_discount, order_item_name, order_unit_price)
                VALUES (@ProductID, @OrderFormID, @OrderItemQuantity, @OrderItemDiscount, @OrderItemName, @OrderUnitPrice)";

                // Ensure that both lists have the same number of items
                if (productIDs.Count == productNames.Count)
                {
                    for (int i = 0; i < productIDs.Count; i++)
                    {
                        SqlCommand insertOrderItemCmd = new SqlCommand(insertOrderItemQuery, conn);

                        decimal originalPrice = productPrices[i] * productQuantities[i]; // Total price before discount
                        decimal discountedPrice = (originalPrice * discountPercent[i] / 100); // Discount amount

                        // Add parameters for the current productID and productName
                        insertOrderItemCmd.Parameters.AddWithValue("@ProductID", productIDs[i]);
                        insertOrderItemCmd.Parameters.AddWithValue("@OrderFormID", orderFormID);
                        insertOrderItemCmd.Parameters.AddWithValue("@OrderItemQuantity", productQuantities[i]);  // Adjust if different per item
                        insertOrderItemCmd.Parameters.AddWithValue("@OrderItemDiscount", discountedPrice);  // Adjust if different per item
                        insertOrderItemCmd.Parameters.AddWithValue("@OrderItemName", productNames[i]);  // Insert product name
                        insertOrderItemCmd.Parameters.AddWithValue("@OrderUnitPrice", productPrices[i]);

                        // Execute the query for each product
                        insertOrderItemCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    // Handle the error case where the two lists are not the same size
                    throw new Exception("Product IDs and Product Names count do not match.");
                }

                string deleteCartQuery = "DELETE FROM Cart WHERE customer_id = @CustomerID";
                SqlCommand deleteCartCmd = new SqlCommand(deleteCartQuery, conn);
                deleteCartCmd.Parameters.AddWithValue("@CustomerID", customerID);
                deleteCartCmd.ExecuteNonQuery();

                conn.Close();
            }
            Response.Redirect("~/PaymentSuccessful.aspx");
        }
    }
}