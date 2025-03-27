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
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Password Recovery";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }

        protected void ButtonResetPassword_Click(object sender, EventArgs e)
        {
            string email = TextBoxRecoveryEmail.Text;
            string selectedQuestion = DropDownListSecurityQuestion.SelectedItem.Text;
            string securityAnswer = TextBoxSecurityAnswer.Text;
            string newPassword = TextBoxNewPassword.Text;

            // Validate email exists and check security answer
            if (IsValidEmail(email) && IsValidSecurityAnswer(email, selectedQuestion, securityAnswer))
            {
                // Update password if validation passes
                UpdatePassword(email, newPassword);
                Response.Write("<script>alert('Password reset successful!'); window.location='Homepage.aspx';</script>");
            }
            else
            {
                // Display error message for invalid email or incorrect security answer
                Response.Write("<script>alert('Invalid email or security answer.');</script>");
            }
        }

        private bool IsValidEmail(string email)
        {
            // Check if the email exists in the database
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(1) FROM Customer WHERE customer_email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                object result = cmd.ExecuteScalar();

                // Check if the result is null or can be converted to an integer
                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    return count == 1;
                }
                return false;
            }
        }

        private bool IsValidSecurityAnswer(string email, string question, string answer)
        {
            // Retrieve customer_id first using email
            int customerId = GetCustomerIdByEmail(email);
            if (customerId == -1) return false; // If email is not found

            // Check if the security answer matches in the database
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT COUNT(1) FROM Security_Question WHERE customer_id = @CustomerID AND question_text = @Question AND security_answer = @Answer";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CustomerID", customerId);
                cmd.Parameters.AddWithValue("@Question", question);
                cmd.Parameters.AddWithValue("@Answer", answer);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int count))
                {
                    return count == 1;
                }
                return false;
            }
        }

        // Helper method to get customer_id using email
        private int GetCustomerIdByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "SELECT customer_id FROM Customer WHERE customer_email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int customerId))
                {
                    return customerId;
                }
                return -1; // Email not found
            }
        }

        private void UpdatePassword(string email, string newPassword)
        {
            // Update password in the database
            using (SqlConnection conn = new SqlConnection(strcon))
            {
                string query = "UPDATE Customer SET customer_password = @Password WHERE customer_email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", newPassword);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}