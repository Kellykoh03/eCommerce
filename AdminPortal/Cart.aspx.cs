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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Cart";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }

        protected void btnItemMinus2_Click(object sender, EventArgs e)
        {
            // Get the clicked button
            Button btn = (Button)sender;

            string cartItemId = btn.CommandArgument;

            // Increment the quantity in the database for the corresponding Food_ID
            string query = "UPDATE Cart SET item_quantity = item_quantity - 1 WHERE cart_item_id = @cart_item_id AND customer_id = @customer_id";

            // Assuming you have a method to execute SQL commands
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cart_item_id", cartItemId);
                    cmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Rebind the Repeater to reflect the updated quantity
            Repeater1.DataBind();
            Repeater2.DataBind();
        }

        protected void btnItemTrash1_Click(object sender, EventArgs e)
        {
            // Get the clicked button
            Button btn = (Button)sender;

            string cartItemId = btn.CommandArgument;

            // Delete the item from the cart
            string query = "DELETE FROM Cart WHERE cart_item_id = @cart_item_id AND customer_id = @customer_id";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cart_item_id", cartItemId);
                    cmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Rebind the Repeater to reflect the updated cart
            Repeater1.DataBind();
            Repeater2.DataBind();
        }

        protected void btnItemPlus1_Click(object sender, EventArgs e)
        {
            // Get the clicked button
            Button btn = (Button)sender;

            // Get the Food_ID from the CommandArgument
            string cartItemId = btn.CommandArgument;

            // Increment the quantity in the database for the corresponding Food_ID
            string query = "UPDATE Cart SET item_quantity = item_quantity + 1 WHERE cart_item_id = @cart_item_id AND customer_id = @customer_id";

            // Assuming you have a method to execute SQL commands
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cart_item_id", cartItemId);
                    cmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Rebind the Repeater to reflect the updated quantity
            Repeater1.DataBind();
            Repeater2.DataBind();
        }

        protected void btnItemRemove1_Click(object sender, EventArgs e)
        {
            // Get the clicked button
            Button btn = (Button)sender;

            string cartItemId = btn.CommandArgument;

            // Delete the item from the cart
            string query = "DELETE FROM Cart WHERE cart_item_id = @cart_item_id AND customer_id = @customer_id";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cart_item_id", cartItemId);
                    cmd.Parameters.AddWithValue("@customer_id", Session["CustomerID"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }

            // Rebind the Repeater to reflect the updated cart
            Repeater1.DataBind();
            Repeater2.DataBind();
        }

        protected void btnCheckOut_Click(object sender, EventArgs e)
        {

        }
    }
}