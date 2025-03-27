using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Sign Up Customer";
            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) ||
                string.IsNullOrWhiteSpace(TextBox2.Text) ||
                string.IsNullOrWhiteSpace(TextBox8.Text) ||
                string.IsNullOrWhiteSpace(TextBox4.Text) ||
                string.IsNullOrWhiteSpace(TextBox6.Text) ||
                string.IsNullOrWhiteSpace(TextBox5.Text))
            {
                Response.Write("<script>alert('Please fill out all the fields.')</script>");
                return;
            }

            if (TextBox2.Text != TextBox8.Text)
            {
                Response.Write("<script>alert('Passwords do not match.')</script>");
                return;
            }

            // Validate phone number format (e.g., 012-3456789)
            if (!Regex.IsMatch(TextBox6.Text.Trim(), @"^\d{3}-\d{7,8}$"))
            {
                Response.Write("<script>alert('Invalid phone number format. It should be in the format 012-3456789.')</script>");
                return;
            }

            if (checkEmailExist())
            {
                Response.Write("<script>alert('Email already exists, try another email.')</script>");
            }
            else
            {
                signUpNewCustomer();
            }
        }

        protected bool checkEmailExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Customer where customer_email=@Email;", con);
                cmd.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                return dt.Rows.Count >= 1;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
                return false;
            }
        }


        protected void signUpNewCustomer()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // Begin transaction for atomic operations
                    SqlTransaction transaction = con.BeginTransaction();

                    string insertCustomerQuery = "INSERT INTO Customer (customer_name, customer_email, customer_phone_number, customer_birthday, customer_password) " +
                           "VALUES (@Name, @Email, @Phone, @Birthday, @Password);" +
                           "SELECT SCOPE_IDENTITY();"; // Get the newly inserted customer_id

                    SqlCommand cmdCustomer = new SqlCommand(insertCustomerQuery, con, transaction);
                    cmdCustomer.Parameters.AddWithValue("@Name", TextBox4.Text.Trim());
                    cmdCustomer.Parameters.AddWithValue("@Email", TextBox1.Text.Trim());
                    cmdCustomer.Parameters.AddWithValue("@Phone", TextBox6.Text.Trim());
                    cmdCustomer.Parameters.AddWithValue("@Birthday", TextBox5.Text.Trim());
                    cmdCustomer.Parameters.AddWithValue("@Password", TextBox2.Text.Trim());

                    // Execute the Customer insert command and get the inserted customer_id
                    object result = cmdCustomer.ExecuteScalar();

                    // Handle the case where SCOPE_IDENTITY() returns a decimal
                    int customerId = Convert.ToInt32(result);

                    // Insert security question and answer into Security_Question table
                    SqlCommand cmdSecurity = new SqlCommand("INSERT INTO Security_Question (customer_id, question_text, security_answer) VALUES (@CustomerId, @SecurityQ, @SecurityAns)", con, transaction);
                    cmdSecurity.Parameters.AddWithValue("@CustomerId", customerId);
                    cmdSecurity.Parameters.AddWithValue("@SecurityQ", DropDownListSecurityQuestion.SelectedItem.Text);
                    cmdSecurity.Parameters.AddWithValue("@SecurityAns", TextBoxSecurityAnswer.Text.Trim());

                    // Execute the Security insert command
                    cmdSecurity.ExecuteNonQuery();

                    // Commit transaction after both inserts succeed
                    transaction.Commit();

                    // Redirect to homepage with success alert
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sign-up successful!'); window.location='LoginCustomer.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "')</script>");
            }
        }
    }
}