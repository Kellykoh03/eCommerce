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
    public partial class WebForm14 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] == "admin")
            {

                Response.Redirect("Homepage.aspx");

            }
            this.Title = "User Management";
            GridView1.DataBind();
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkUserIdExist())
            {
                getUserById();
            }
            else
            {
                Response.Write("<script>alert('Id does not exist, try another Id')</script>");
            }
        }

        protected bool checkUserIdExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Customer where customer_id=@customer_id;", con);

                cmd.Parameters.AddWithValue("@customer_id", TextBox1.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    con.Close();


                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return false;
            }


        }


        protected void getUserById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Customer where customer_id=@customer_id;", con);

                cmd.Parameters.AddWithValue("@customer_id", TextBox1.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    con.Close();

                    TextBox2.Text = dt.Rows[0][2].ToString();
                    TextBox3.Text = "**********";
                    TextBox4.Text = dt.Rows[0][1].ToString();
                    TextBox6.Text = dt.Rows[0][3].ToString();
                    TextBox5.Text = Convert.ToDateTime(dt.Rows[0][4]).ToString("yyyy-MM-dd");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Id')</script>");
                }



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }
        }
    }
}