using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

namespace adminPortal
{
    public partial class WebForm19 : System.Web.UI.Page
    {
        static string global_filepath;
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Category Management";
            GridView1.DataBind();

            if (Session["role"] != "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkCategoryExistById())
            {
                getCategoryById();
            }
            else
            {
                Response.Write("<script>alert('Category Id not exist, try other')</script>");
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            if (checkCategoryExistById())
            {
                deleteCategory();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Category Id not exist, try other')</script>");
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

            if (checkCategoryNameExist())
            {
                Response.Write("<script>alert('Category Name already exist, try other category name')</script>");
                return;
            }
            else
            {

                addNewCategory();
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

            if (checkCategoryExistForUpdate())
            {

            }
            else if (checkCategoryNameExist())
            {
                Response.Write("<script>alert('Error. Similar category name existed already. Please change it')</script>");
                return;
            }

            if (!checkCategoryExistById())
            {
                Response.Write("<script>alert('Category Id not exist, try other')</script>");
                return;
            }


            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

            if (filename == "")
            {
                //updateWithoutFile
                updateCategoryWithoutFile();
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
                    updateCategory();
                    clearForm();
                    return;

                }

            }



        }

        protected void updateCategoryWithoutFile()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("UPDATE Product_Category SET product_category_name = @product_category_name, product_category_description = @product_category_description, isShowHomePage = @isShowHomePage, isShowMenu = @isShowMenu WHERE product_category_id = @product_category_id;", con);


                cmd.Parameters.AddWithValue("@product_category_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_category_description", TextBox10.Text.Trim());


                cmd.Parameters.AddWithValue("@isShowHomePage", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());


                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Updated Category Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void updateCategory()
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
                    string serverPath = Server.MapPath("asset/Image/CategoryUpload/" + filename);
                    FileUpload1.SaveAs(serverPath);
                    filepath = "~/asset/Image/CategoryUpload/" + filename;


                }


                string imagePath = "";
                using (SqlCommand cmd1 = new SqlCommand("SELECT product_category_image FROM Product_Category WHERE product_category_id = @product_category_id", con))
                {
                    cmd1.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath = reader["product_category_image"].ToString();
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




                SqlCommand cmd = new SqlCommand("UPDATE Product_Category SET product_category_name = @product_category_name, product_category_description = @product_category_description, product_category_image = @product_category_image, isShowHomePage = @isShowHomePage, isShowMenu = @isShowMenu WHERE product_category_id = @product_category_id;", con);


                cmd.Parameters.AddWithValue("@product_category_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_category_description", TextBox10.Text.Trim());

                cmd.Parameters.AddWithValue("@product_category_image", filepath);
                cmd.Parameters.AddWithValue("@isShowHomePage", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());


                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Updated Category Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void addNewCategory()
        {
            try
            {

                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string relativeName = "~/asset/Image/CategoryUpload/" + filename;

                global_filepath = relativeName;

                string serverPath = Server.MapPath("~/asset/Image/CategoryUpload/" + filename);

                FileUpload1.SaveAs(serverPath);


                SqlConnection con = new SqlConnection(strcon);



                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("INSERT INTO Product_Category(product_category_name, product_category_description, product_category_image, isShowHomePage, isShowMenu) values(@product_category_name, @product_category_description, @product_category_image, @isShowHomePage, @isShowMenu);", con);



                cmd.Parameters.AddWithValue("@product_category_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_category_description", TextBox10.Text.Trim());

                cmd.Parameters.AddWithValue("@product_category_image", relativeName);
                cmd.Parameters.AddWithValue("@isShowHomePage", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@isShowMenu", DropDownList3.SelectedItem.Value);



                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Added New Category Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void getCategoryById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where product_category_id=@product_category_id;", con);

                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);



                if (dt.Rows.Count >= 1)
                {
                    con.Close();


                    TextBox3.Text = dt.Rows[0]["product_category_name"].ToString();
                    TextBox10.Text = dt.Rows[0]["product_category_description"].ToString();
                    global_filepath = dt.Rows[0]["product_category_image"].ToString();
                    DropDownList2.SelectedValue = dt.Rows[0]["isShowHomePage"].ToString().Trim();
                    DropDownList3.SelectedValue = dt.Rows[0]["isShowMenu"].ToString().Trim();
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

        protected void deleteCategory()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                string imagePath = "";
                using (SqlCommand cmd1 = new SqlCommand("SELECT product_category_image FROM Product_Category WHERE product_category_id = @product_category_id", con))
                {
                    cmd1.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath = reader["product_category_image"].ToString();
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




                SqlCommand cmd = new SqlCommand("DELETE FROM Product_Category WHERE product_category_id= @product_category_id", con);

                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());

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


        protected bool checkImageExist()
        {
            try
            {
                string filepath = "";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);



                filepath = "~/asset/Image/CategoryUpload/" + filename;




                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where product_category_image=@product_category_image;", con);

                cmd.Parameters.AddWithValue("@product_category_image", filepath);

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


        protected bool checkCategoryExistById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where product_category_id=@product_category_id;", con);

                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());

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

        protected bool checkCategoryNameExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where product_category_name=@product_category_name;", con);

                cmd.Parameters.AddWithValue("@product_category_name", TextBox3.Text.Trim());

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

        protected bool checkCategoryExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product_Category where product_category_name=@product_category_name AND product_category_id=@product_category_id;", con);

                cmd.Parameters.AddWithValue("@product_category_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_category_id", TextBox12.Text.Trim());

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
            // Clear TextBoxes
            TextBox12.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox10.Text = string.Empty;

            // Reset DropDownList to default (usually first item)
            if (DropDownList3.Items.Count > 0)
            {
                DropDownList3.SelectedIndex = 0;
            }

            if (DropDownList2.Items.Count > 0)
            {
                DropDownList2.SelectedIndex = 0;
            }


            // Reset FileUpload control (no way to clear file selection, but you can reset the control)
            FileUpload1.Attributes.Clear(); // This may reset attributes, including the file

            // Clear Image display
            Image2.ImageUrl = string.Empty;
            global_filepath = "";

            Label12.Text = "";

        }

        protected bool isEmptyWithEmptyFile()
        {
            if (!string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           DropDownList3.SelectedIndex != 0 &&
           DropDownList2.SelectedIndex != 0
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

        protected bool isEmpty()
        {
            if (!string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
            !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
            DropDownList3.SelectedIndex != 0 &&
            DropDownList2.SelectedIndex != 0
            )
            {

                return false;





            }
            else
            {
                return true;
            }


        }


        protected void TextBox12_TextChanged(object sender, EventArgs e)
        {

        }
    }
}