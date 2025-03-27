using Microsoft.Ajax.Utilities;
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
    public partial class WebForm12 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Admin Account";

            if (!IsPostBack)
            {
                getAdminInfo();
            }

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                Response.Write("<script>alert('Error. There is empty field. Please fill up all fields')</script>");
                return;
            }

            string newPassword = TextBox11.Text;
            string newRepeatPassword = TextBox8.Text;
            string oriEmail = TextBox1.Text;
            string oriContactNo = TextBox6.Text;
            string patternContactNo = @"^\d{3}-\d{7}$";
            string patternEmail = @"^[^@]+@[^@]{2,}\.[^@]{2,}$";



            if (Regex.IsMatch(oriContactNo, patternContactNo))
            {
            }
            else
            {
                Response.Write("<script>alert('Unable update. Invalid Contact No Format')</script>");
                return;
            }

            if (Regex.IsMatch(oriEmail, patternEmail))
            {
            }
            else
            {
                Response.Write("<script>alert('Unable update. Invalid Email Format')</script>");
                return;
            }

            if (newPassword.IsNullOrWhiteSpace() && newRepeatPassword.IsNullOrWhiteSpace())
            {
                updateAdminInfoWithoutPassword();
                getAdminInfo();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update successfully');", true);
                return;
            }

            if (newPassword == newRepeatPassword)
            {
                updateAdminInfoWithPassword();
                getAdminInfo();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Update successfully');", true);
                return;
            }
            else
            {
                Response.Write("<script>alert('Unable update. new password & repeat new password not equal')</script>");
                return;
            }


        }

        protected void updateAdminInfoWithPassword()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Admin SET admin_name=@admin_name, admin_email=@admin_email,admin_phone_number=@admin_phone_number,admin_password=@admin_password WHERE admin_id ='3';", con);

                cmd.Parameters.AddWithValue("@admin_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@admin_email", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@admin_phone_number", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@admin_password", TextBox8.Text.Trim());



                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void updateAdminInfoWithoutPassword()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Admin SET admin_name=@admin_name, admin_email=@admin_email,admin_phone_number=@admin_phone_number WHERE admin_id ='3';", con);

                cmd.Parameters.AddWithValue("@admin_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@admin_email", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@admin_phone_number", TextBox6.Text.Trim());



                cmd.ExecuteNonQuery();

                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }



        protected void getAdminInfo()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Admin where admin_id='3'", con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    con.Close();

                    TextBox1.Text = dt.Rows[0][2].ToString();
                    TextBox4.Text = dt.Rows[0][1].ToString();
                    TextBox6.Text = dt.Rows[0][3].ToString();

                }
                else
                {
                    Response.Write("<script>alert('Error')</script>");
                }



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }
        }

        protected bool isEmpty()
        {
            // Check if any of the text boxes are null or empty
            if (string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) ||
                string.IsNullOrWhiteSpace(TextBox1.Text.Trim()) ||
                string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) ||
                string.IsNullOrWhiteSpace(TextBox8.Text.Trim()) ||
                string.IsNullOrWhiteSpace(TextBox11.Text.Trim()))
            {
                return true;
            }

            return false;
        }
    }

}