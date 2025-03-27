using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Customer Account";

            if (!IsPostBack)
            {
                LoadCustomerData();
                if (Session["CustomerID"] != null && Convert.ToInt32(Session["CustomerID"]) > 0)
                {
                    BindRepeater();
                }
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
                    TextBox1.Text = reader["customer_email"].ToString();
                    TextBox2.Text = reader["customer_password"].ToString();
                    TextBox4.Text = reader["customer_name"].ToString();
                    TextBox6.Text = reader["customer_phone_number"].ToString();
                    TextBox5.Text = Convert.ToDateTime(reader["customer_birthday"]).ToString("yyyy-MM-dd");
                    txtAddress.Text = reader["customer_address"].ToString();
                    ddlCountry.Text = reader["customer_country"].ToString();
                    ddlPostCode.Text = reader["customer_post_code"].ToString();
                    ddlCity.Text = reader["customer_city"].ToString();
                }
                conn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            int customerId = Convert.ToInt32(Session["CustomerID"]);
            string newPassword = TextBox11.Text.Trim();
            string repeatNewPassword = TextBox8.Text.Trim();
            string customerName = TextBox4.Text.Trim();
            string customerPhone = TextBox6.Text.Trim();
            string customerBirthday = TextBox5.Text.Trim();
            string customerCountry = ddlCountry.Text.Trim();
            string customerCity = ddlCity.Text.Trim();
            string customerPostCode = ddlPostCode.Text.Trim();
            string customerAddress = txtAddress.Text.Trim();

            // Check if passwords match
            if (newPassword != repeatNewPassword)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Passwords do not match!');", true);
                return;
            }

            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connString))
            {
                // Construct the base update query
                string query = "UPDATE Customer SET customer_name = @customer_name, customer_phone_number = @customer_phone_number, customer_birthday = @customer_birthday, customer_country = @customer_country, customer_post_code = @customer_post_code, customer_address = @customer_address, customer_city = @customer_city";

                // Only add password to the update if it's not empty
                if (!string.IsNullOrEmpty(newPassword))
                {
                    query += ", customer_password = @customer_password";  // Hash the password in real production systems
                }

                query += " WHERE customer_id = @customer_id";

                SqlCommand cmd = new SqlCommand(query, con);

                // Assign values to the parameters
                cmd.Parameters.AddWithValue("@customer_name", customerName);
                cmd.Parameters.AddWithValue("@customer_phone_number", customerPhone);
                cmd.Parameters.AddWithValue("@customer_birthday", customerBirthday);
                cmd.Parameters.AddWithValue("@customer_id", customerId);
                cmd.Parameters.AddWithValue("@customer_address", customerAddress);
                cmd.Parameters.AddWithValue("@customer_post_code", customerPostCode);
                cmd.Parameters.AddWithValue("@customer_country", customerCountry);
                cmd.Parameters.AddWithValue("@customer_city", customerCity);

                if (!string.IsNullOrEmpty(newPassword))
                {
                    cmd.Parameters.AddWithValue("@customer_password", newPassword);  // Note: Hash the password in a real system
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            // Show a success message
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Your information has been updated successfully!');", true);
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["CustomerID"] = null;
            Session["LoggedInEmail"] = null;
            Session["role"] = null;
            Session["CustomerEmail"] = null;
            Response.Redirect("Homepage.aspx");
        }

        private void BindRepeater()
        {
            int customerID = Convert.ToInt32(Session["CustomerID"]);

            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = $@"
        SELECT ofr.order_form_id, ofr.order_date, ofr.order_status, ofr.payment_total_amount, 
               oi.order_item_id, oi.order_item_name, oi.order_item_quantity, oi.order_unit_price, p.product_name, p.product_unit_price
        FROM Order_Form AS ofr
        JOIN Order_Item AS oi ON ofr.order_form_id = oi.order_form_id
        JOIN Product AS p ON oi.product_id = p.product_id 
        WHERE ofr.customer_id = {customerID}";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                var groupedData = dt.AsEnumerable()
                    .GroupBy(row => new
                    {
                        order_form_id = row["order_form_id"],
                        order_date = row["order_date"],
                        order_status = row["order_status"],
                        payment_total_amount = row["payment_total_amount"]
                    })
                    .Select(group => new
                    {
                        OrderFormId = group.Key.order_form_id,
                        OrderDate = group.Key.order_date,
                        OrderStatus = group.Key.order_status,
                        PaymentTotalAmount = group.Key.payment_total_amount,
                        Items = group.Select(row => new
                        {
                            OrderUnitPrice = row["order_unit_price"],
                            OrderItemName = row["order_item_name"],
                            OrderItemQuantity = row["order_item_quantity"]
                        }).ToList()
                    }).ToList();

                Repeater1.DataSource = groupedData;
                Repeater1.DataBind();
            }
        }
    }
}