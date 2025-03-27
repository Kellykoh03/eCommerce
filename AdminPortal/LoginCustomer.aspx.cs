using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            this.Title = "Customer Login";

            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }

            if (!IsPostBack)
            {
                // Hide the 2FA section initially
                //TwoFactorSection.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string email = TextBox1.Text.Trim();
            string password = TextBox2.Text.Trim();

            // Validate user input
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter all fields.');", true);
                return;
            }

            if (String.IsNullOrEmpty(Recaptcha1.Response))
            {
                Response.Write("<script>alert('Captcha cannot be empty. Fill it please')</script>");
            }
            else
            {

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    string query = "SELECT customer_password FROM Customer WHERE customer_email = @Email";
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
                                    string dbPassword = reader["customer_password"].ToString();

                                    if (password == dbPassword)
                                    {

                                        // Store the 2FA code in the session (or database)
                                        //Session["2FACode"] = twoFactorCode;
                                        Session["LoggedInEmail"] = email;

                                        // Show the 2FA section and hide the initial login section
                                        //LoginSection.Visible = false;
                                        //TwoFactorSection.Visible = true;
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Login Successfully.');", true);
                                        Response.Redirect("Homepage.aspx");
                                    }
                                    else
                                    {
                                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid password');", true);
                                    }
                                }
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", "alert('Email is not registered. Redirecting to Sign Up...'); window.location='SignUpCustomer.aspx';", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: " + ex.Message + "');", true);
                        }
                    }
                }

            }




        }

        // Method to generate a random 6-digit 2FA code
        private string Generate2FACode()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        // Method to send 2FA code via email
        private void Send2FACode(string email, string twoFactorCode)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("alexlch-wp22@student.tarc.edu.my");
                mail.To.Add(email);
                mail.Subject = "Your 2FA Code";
                mail.Body = "Your 2FA code is: " + twoFactorCode;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("alexlch-wp22@student.tarc.edu.my", "030620141291");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to send 2FA code: " + ex.Message + "');", true);
            }
        }

        protected void Verify2FA_Click(object sender, EventArgs e)
        {
            //string enteredCode = TextBox2FACode.Text.Trim();
            //string session2FACode = Session["2FACode"] as string;

            //if (enteredCode == session2FACode)
            //{
            //    // 2FA successful, proceed to login
            //    Response.Redirect("Homepage.aspx");
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid 2FA code. Please try again.');", true);
            //}
        }

    }
}