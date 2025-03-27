using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace adminPortal
{
    public partial class WebForm20 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }

            this.Title = "Specification Type Management";
            GridView1.DataBind();
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                Response.Write("<script>alert('Error. There is empty field. Please fill up all fields')</script>");
                return;
            }

            if (checkTypeNameExist())
            {
                Response.Write("<script>alert('Type name already exist, try another name')</script>");
            }
            else
            {
                addNewType();
                clearForm();
            }
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            if (checkTypeIdExist())
            {
                updateType();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Type name does not exist, try another name')</script>");
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {


            if (checkTypeIdExist())
            {
                deleteType();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Type name does not exist, try another name')</script>");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            getSpecificationTypeById();
        }

        protected void deleteType()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM Specification_Type WHERE specification_type_id LIKE @specification_type_id", con);

                cmd.Parameters.AddWithValue("@specification_type_id", TextBox1.Text.Trim());



                cmd.ExecuteNonQuery();



                con.Close();




                Response.Write("<script>alert('Selected Type Deleted Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void updateType()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Specification_Type SET specification_type_name=@specification_type_name, specification_type_description=@specification_type_description WHERE specification_type_id =@specification_type_id", con);

                cmd.Parameters.AddWithValue("@specification_type_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@specification_type_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@specification_type_id", TextBox1.Text.Trim());

                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Selected Type Updated Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void addNewType()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("INSERT INTO Specification_Type(specification_type_name, specification_type_description) values(@specification_type_name, @specification_type_description)", con);

                cmd.Parameters.AddWithValue("@specification_type_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@specification_type_description", TextBox10.Text.Trim());

                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Added New Type Name Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected bool checkTypeNameExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Specification_Type where specification_type_name=@typeName;", con);

                cmd.Parameters.AddWithValue("@typeName", TextBox3.Text.Trim());

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

        protected bool checkTypeIdExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Specification_Type where specification_type_id=@typeId;", con);

                cmd.Parameters.AddWithValue("@typeId", TextBox1.Text.Trim());

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

        protected void clearForm()
        {
            TextBox1.Text = "";
            TextBox3.Text = "";
            TextBox10.Text = "";
        }


        protected bool isEmpty()
        {
            if (string.IsNullOrEmpty(TextBox3.Text) || string.IsNullOrEmpty(TextBox10.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        protected void getSpecificationTypeById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Specification_Type where specification_type_id=@typeId;", con);

                cmd.Parameters.AddWithValue("@typeId", TextBox1.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    con.Close();

                    TextBox3.Text = dt.Rows[0][1].ToString();
                    TextBox10.Text = dt.Rows[0][2].ToString();

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