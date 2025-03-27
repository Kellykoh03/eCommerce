using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Ajax.Utilities;

namespace adminPortal
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        static string global_filepath;
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Brand Management";
            GridView1.DataBind();

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            if (checkBrandExistById())
            {
                getBrandById();
            }
            else
            {
                Response.Write("<script>alert('Brand Id not exist, try other')</script>");
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {

            if (checkBrandExistById())
            {
                deleteBrand();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Brand Id not exist, try other')</script>");
            }
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            if (isEmptyWithEmptyFile())
            {
                Response.Write("<script>alert('Error. There is empty fields. Please fill all')</script>");
                return;
            }

            if (checkImageExist())
            {
                Response.Write("<script>alert('Error. Similar image name existed already. Please change it')</script>");
                return;
            }

            if (checkBrandExist())
            {
                Response.Write("<script>alert('Brand already exist, try other brand name')</script>");
                return;
            }
            else
            {

                addNewBrand();
                clearForm();
            }
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                Response.Write("<script>alert('Error. There is empty fields. Please fill all')</script>");
                return;
            }

            if (checkBrandExistForUpdate())
            {
                // checkBrandExistForUpdate true mean brand name+brand id exist in database. Then return true to proceed flow normally
                // false mean brand name+brand id is new combination in database. Then need to checkBrandExist ady exist in database or not
            }
            else if (checkBrandExist())
            {
                Response.Write("<script>alert('Error. Similar brand name existed already. Please change it')</script>");
                return;
            }

            if (!checkBrandExistById())
            {
                Response.Write("<script>alert('Brand Id not exist, try other')</script>");
                return;
            }


            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

            if (filename == "")
            {
                //updateWithoutFile
                updateBrandWithoutFile();
                clearForm();
                return;
            }
            else
            {
                //updateWithFile
                //checkImageExist -- if yes, show error message got duplicate image, else proceed update image
                if (checkImageExist())
                {
                    Response.Write("<script>alert('Error. Similar image name already exist. Please fill it again')</script>");
                    return;
                }
                else
                {
                    updateBrand();
                    clearForm();
                    return;

                }

            }
        }

        protected void deleteBrand()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string imagePath = "";
                using (SqlCommand cmd1 = new SqlCommand("SELECT image FROM Brand WHERE brand_id = @brand_id", con))
                {
                    cmd1.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath = reader["image"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string serverPath = Server.MapPath(imagePath);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }




                SqlCommand cmd = new SqlCommand("DELETE FROM Brand WHERE brand_id= @brand_id", con);

                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());

                cmd.ExecuteNonQuery();

                con.Close();




                Response.Write("<script>alert('Deleted Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void updateBrand()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string filepath = "";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                if (filename == "" || filename == null)
                {
                    filepath = global_filepath;
                }
                else
                {
                    string serverPath = Server.MapPath("asset/Image/BrandUpload/" + filename);
                    FileUpload1.SaveAs(serverPath);
                    filepath = "~/asset/Image/BrandUpload/" + filename;


                }

                string imagePath = "";
                using (SqlCommand cmd1 = new SqlCommand("SELECT image FROM Brand WHERE brand_id = @brand_id", con))
                {
                    cmd1.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd1.ExecuteReader();


                    if (reader.Read())
                    {
                        imagePath = reader["image"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string serverPath = Server.MapPath(imagePath);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }



                // SQL command for updating the Brand table
                SqlCommand cmd = new SqlCommand("UPDATE Brand SET company_name = @company_name, contact_name = @contact_name, address = @address, city = @city, postal_code = @postal_code, country = @country, phone_number = @phone_number, fax = @fax, email = @email, image = @image, isShowMenu = @isShowMenu, brand_name = @brand_name WHERE brand_id = @brand_id;", con);

                // Add the parameters
                cmd.Parameters.AddWithValue("@company_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@address", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@postal_code", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@country", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@phone_number", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@fax", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@image", filepath);
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@brand_name", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());


                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Updated Brand Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void updateBrandWithoutFile()
        {
            try
            {

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }



                // SQL command for updating the Brand table
                SqlCommand cmd = new SqlCommand("UPDATE Brand SET company_name = @company_name, contact_name = @contact_name, address = @address, city = @city, postal_code = @postal_code, country = @country, phone_number = @phone_number, fax = @fax, email = @email, isShowMenu = @isShowMenu, brand_name = @brand_name WHERE brand_id = @brand_id;", con);

                // Add the parameters
                cmd.Parameters.AddWithValue("@company_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@address", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@postal_code", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@country", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@phone_number", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@fax", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@brand_name", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());


                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Updated Brand Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void addNewBrand()
        {
            try
            {

                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string relativeName = "~/asset/Image/BrandUpload/" + filename;

                global_filepath = relativeName;

                string serverPath = Server.MapPath("~/asset/Image/BrandUpload/" + filename);

                FileUpload1.SaveAs(serverPath);


                SqlConnection con = new SqlConnection(strcon);



                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("INSERT INTO Brand(company_name, contact_name, address, city, postal_code, country, phone_number, fax, email, image, isShowMenu, brand_name) values(@company_name, @contact_name, @address, @city, @postal_code, @country, @phone_number, @fax, @email, @image, @isShowMenu, @brand_name );", con);



                cmd.Parameters.AddWithValue("@company_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@contact_name", TextBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@address", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@city", TextBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@postal_code", TextBox9.Text.Trim());
                cmd.Parameters.AddWithValue("@country", TextBox11.Text.Trim());
                cmd.Parameters.AddWithValue("@phone_number", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@fax", TextBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@email", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@image", relativeName);
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@brand_name", TextBox8.Text.Trim());



                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Added New Brand Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void fillDropDownValue()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT isShowMenu from Brand", con);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                DropDownList3.DataSource = dt;

                DropDownList3.DataValueField = "isShowMenu";

                DropDownList3.DataBind();




            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }
        }

        protected void getBrandById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Brand where brand_id=@brand_id;", con);

                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);



                if (dt.Rows.Count >= 1)
                {
                    con.Close();


                    TextBox3.Text = dt.Rows[0]["company_name"].ToString();
                    TextBox4.Text = dt.Rows[0]["contact_name"].ToString();
                    TextBox10.Text = dt.Rows[0]["address"].ToString();
                    TextBox7.Text = dt.Rows[0]["city"].ToString();
                    TextBox9.Text = dt.Rows[0]["postal_code"].ToString();
                    TextBox11.Text = dt.Rows[0]["country"].ToString();
                    TextBox6.Text = dt.Rows[0]["phone_number"].ToString();
                    TextBox5.Text = dt.Rows[0]["fax"].ToString();
                    TextBox2.Text = dt.Rows[0]["email"].ToString();
                    global_filepath = dt.Rows[0]["image"].ToString();
                    DropDownList3.SelectedValue = dt.Rows[0]["isShowMenu"].ToString().Trim();
                    TextBox8.Text = dt.Rows[0]["brand_name"].ToString();

                    Label12.Text = global_filepath;

                }
                else
                {
                    Response.Write("<script>alert('Invalid Id, try again')</script>");
                }



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }

        }

        protected bool checkBrandExistById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Brand where brand_id=@brand_id;", con);

                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());

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

        protected bool checkImageExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where image=@image AND brand_id=@brand_id;", con);

                cmd.Parameters.AddWithValue("@image", global_filepath);
                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());

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


        protected bool checkImageExist()
        {
            try
            {
                string filepath = "";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);



                filepath = "~/asset/Image/BrandUpload/" + filename;




                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Brand where image=@image;", con);

                cmd.Parameters.AddWithValue("@image", filepath);

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

        protected bool checkBrandExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Brand where brand_name=@brand_name AND brand_id=@brand_id;", con);

                cmd.Parameters.AddWithValue("@brand_name", TextBox8.Text.Trim());
                cmd.Parameters.AddWithValue("@brand_id", TextBox12.Text.Trim());

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

        protected bool checkBrandExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Brand where brand_name=@brand_name;", con);

                cmd.Parameters.AddWithValue("@brand_name", TextBox8.Text.Trim());

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

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void clearForm()
        {
            // Clear TextBoxes
            TextBox3.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox9.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox8.Text = string.Empty;
            TextBox12.Text = string.Empty;

            // Reset DropDownList to default (usually first item)
            if (DropDownList3.Items.Count > 0)
            {
                DropDownList3.SelectedIndex = 0;
            }

            // Reset FileUpload control (no way to clear file selection, but you can reset the control)
            FileUpload1.Attributes.Clear(); // This may reset attributes, including the file

            // Clear Image display
            Image2.ImageUrl = string.Empty;
            global_filepath = "";

            Label12.Text = "";

        }

        protected bool isEmpty()
        {
            if (!string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox7.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox9.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox11.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox5.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox8.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox12.Text.Trim()) &&
           DropDownList3.SelectedIndex != 0
           )
            {




                return false;
            }
            else
            {
                return true;
            }


        }

        protected bool isEmptyWithEmptyFile()
        {
            if (!string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox7.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox9.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox11.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox5.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox8.Text.Trim()) &&
           DropDownList3.SelectedIndex != 0
           )
            {
                if (!FileUpload1.HasFile)
                {
                    return true;
                }
                else
                {
                    return false;
                }




            }
            else
            {
                return true;
            }


        }



    }
}