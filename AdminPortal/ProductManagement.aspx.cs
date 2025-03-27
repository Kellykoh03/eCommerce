using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection.Emit;
using System.IO;
using System.Drawing;

namespace adminPortal
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        static string global_filepath1 = "";
        static string global_filepath2 = "";
        static string global_filepath3 = "";
        static string global_filepath4 = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Product Management";
            GridView1.DataBind();

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkProductExistById())
            {
                getProductById();
            }
            else
            {
                Response.Write("<script>alert('Product Id not exist, try other')</script>");
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            if (checkProductExistById())
            {
                deleteProduct();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Product Id not exist, try other')</script>");
            }
        }

        protected void Button14_Click(object sender, EventArgs e)
        {
            if (isEmptyWithEmptyFile())
            {
                Response.Write("<script>alert('Error. There is empty fields. Please fill all')</script>");
                return;
            }

            if (!isNumber())
            {
                Response.Write("<script>alert('Error. Invalid number input. Please change it')</script>");
                return;
            }

            if (checkImageExist())
            {
                Response.Write("<script>alert('Error. Similar image name existed already. Please change it')</script>");
                return;
            }

            if (checkSpecificationTypeExist())
            {
                Response.Write("<script>alert('Error. Similar specification type input existed. Please change it')</script>");
                return;
            }

            if (checkProductNameExist())
            {
                Response.Write("<script>alert('Product Name already exist, try other category name')</script>");
                return;
            }
            else
            {

                addNewProduct();
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

            if (!isNumber())
            {
                Response.Write("<script>alert('Error. Invalid number input. Please change it')</script>");
                return;
            }

            if (checkSpecificationTypeExist())
            {
                Response.Write("<script>alert('Error. Similar specification type input existed. Please change it')</script>");
                return;
            }

            if (checkProductExistForUpdate())
            {

            }
            else if (checkProductNameExist())
            {
                Response.Write("<script>alert('Error. Similar product name existed already. Please change it')</script>");
                return;
            }


            if (!checkProductExistById())
            {
                Response.Write("<script>alert('Product Id not exist, try other')</script>");
                return;
            }


            updateProductDirect();
            clearForm();
            return;


            //string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
            //string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
            //string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);
            //string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);

            //if (filename1 == "")sdasdasd
            //{
            //    //updateWithoutFile
            //    updateProductWithoutFile();
            //    clearForm();
            //    return;
            //}
            //else
            //{
            //    //updateWithFile
            //    //checkImageExist -- if yes, show error message got duplicate image, else proceed update image
            //    if (checkImageExist())
            //    {
            //        Response.Write("<script>alert('Error. Similar image name already exist. Please fill it again')</script>");
            //        return;
            //    }
            //    else
            //    {
            //        updateProduct();
            //        clearForm();
            //        return;

            //    }

            //}








        }

        protected void addNewProduct()
        {
            try
            {
                //Image 1 save
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string relativeName1 = "~/asset/Image/ProductUpload/" + filename1;

                global_filepath1 = relativeName1;

                string serverPath1 = Server.MapPath("~/asset/Image/ProductUpload/" + filename1);

                FileUpload1.SaveAs(serverPath1);

                //Image 2 save

                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);

                string relativeName2 = "~/asset/Image/ProductUpload/" + filename2;

                global_filepath2 = relativeName2;

                string serverPath2 = Server.MapPath("~/asset/Image/ProductUpload/" + filename2);

                FileUpload2.SaveAs(serverPath2);


                //Image 3 save
                string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);

                string relativeName3 = "~/asset/Image/ProductUpload/" + filename3;

                global_filepath3 = relativeName3;

                string serverPath3 = Server.MapPath("~/asset/Image/ProductUpload/" + filename3);

                FileUpload3.SaveAs(serverPath3);


                //Image4 save
                string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);

                string relativeName4 = "~/asset/Image/ProductUpload/" + filename4;

                global_filepath4 = relativeName4;

                string serverPath4 = Server.MapPath("~/asset/Image/ProductUpload/" + filename4);

                FileUpload4.SaveAs(serverPath4);


                SqlConnection con = new SqlConnection(strcon);



                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //insert to product table
                SqlCommand cmd = new SqlCommand("INSERT INTO Product(product_name, product_description, product_unit_price, product_quantity_stock, product_availability, brand_id, product_category_id, product_image1, product_image2, product_image3, product_image4) values(@product_name, @product_description, @product_unit_price, @product_quantity_stock, @product_availability, @brand_id, @product_category_id, @product_image1, @product_image2, @product_image3, @product_image4);", con);



                cmd.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@product_unit_price", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@product_quantity_stock", TextBox6.Text.Trim());

                cmd.Parameters.AddWithValue("@brand_id", DropDownList4.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@product_availability", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@product_category_id", DropDownList3.SelectedItem.Value);

                cmd.Parameters.AddWithValue("@product_image1", relativeName1);
                cmd.Parameters.AddWithValue("@product_image2", relativeName2);
                cmd.Parameters.AddWithValue("@product_image3", relativeName3);
                cmd.Parameters.AddWithValue("@product_image4", relativeName4);

                cmd.ExecuteNonQuery();



                //insert to product specification table
                //get new product ID then do insert

                SqlCommand cmd1 = new SqlCommand("SELECT * from Product where product_name=@product_name;", con);

                cmd1.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                DataTable dt = new DataTable();

                adapter1.Fill(dt);

                string newProductId = "";

                if (dt.Rows.Count >= 1)
                {
                    newProductId = dt.Rows[0]["product_id"].ToString();
                }

                string[] specValuesArray1 = TextBox4.Text.Trim().Split(',');
                string[] specValuesArray2 = TextBox5.Text.Trim().Split(',');
                string[] specValuesArray3 = TextBox7.Text.Trim().Split(',');
                string[] specValuesArray4 = TextBox8.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd2.Parameters.AddWithValue("@specification_type_id", DropDownList5.SelectedValue);  // Replace with actual specification_type_id value
                    cmd2.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd2.Parameters.AddWithValue("@product_id", newProductId);  // Replace with actual product_id value

                    // Execute the command
                    cmd2.ExecuteNonQuery();
                }

                //1
                foreach (string specValue in specValuesArray2)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd3.Parameters.AddWithValue("@specification_type_id", DropDownList1.SelectedValue);  // Replace with actual specification_type_id value
                    cmd3.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd3.Parameters.AddWithValue("@product_id", newProductId);  // Replace with actual product_id value

                    // Execute the command
                    cmd3.ExecuteNonQuery();
                }


                //6
                foreach (string specValue in specValuesArray3)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd4 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd4.Parameters.AddWithValue("@specification_type_id", DropDownList6.SelectedValue);  // Replace with actual specification_type_id value
                    cmd4.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd4.Parameters.AddWithValue("@product_id", newProductId);  // Replace with actual product_id value

                    // Execute the command
                    cmd4.ExecuteNonQuery();
                }

                //7
                foreach (string specValue in specValuesArray4)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd5 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd5.Parameters.AddWithValue("@specification_type_id", DropDownList7.SelectedValue);  // Replace with actual specification_type_id value
                    cmd5.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd5.Parameters.AddWithValue("@product_id", newProductId);  // Replace with actual product_id value

                    // Execute the command
                    cmd5.ExecuteNonQuery();
                }




                con.Close();

                Response.Write("<script>alert('Added New Product Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void deleteProduct()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //Image 1 deletion
                string imagePath1 = "";
                using (SqlCommand cmd2 = new SqlCommand("SELECT product_image1 FROM Product WHERE product_id = @product_id;", con))
                {
                    cmd2.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath1 = reader["product_image1"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath1))
                {
                    string serverPath = Server.MapPath(imagePath1);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }

                //Image 2 deletion
                string imagePath2 = "";
                using (SqlCommand cmd3 = new SqlCommand("SELECT product_image2 FROM Product WHERE product_id = @product_id;", con))
                {
                    cmd3.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());
                    SqlDataReader reader = cmd3.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath2 = reader["product_image2"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath2))
                {
                    string serverPath = Server.MapPath(imagePath2);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }

                //Image 3 deletion
                string imagePath3 = "";
                using (SqlCommand cmd4 = new SqlCommand("SELECT product_image3 FROM Product WHERE product_id = @product_id;", con))
                {
                    cmd4.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());
                    SqlDataReader reader = cmd4.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath3 = reader["product_image3"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath3))
                {
                    string serverPath = Server.MapPath(imagePath3);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }

                //Image 4 deletion
                string imagePath4 = "";
                using (SqlCommand cmd5 = new SqlCommand("SELECT product_image4 FROM Product WHERE product_id = @product_id;", con))
                {
                    cmd5.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());
                    SqlDataReader reader = cmd5.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath4 = reader["product_image4"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath4))
                {
                    string serverPath = Server.MapPath(imagePath4);
                    if (System.IO.File.Exists(serverPath))
                    {
                        System.IO.File.Delete(serverPath);
                    }
                }

                SqlCommand cmd = new SqlCommand("DELETE FROM Product WHERE product_id = @product_id;", con);

                cmd.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());

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


        protected void getProductById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product where product_id=@product_id;", con);

                cmd.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);



                if (dt.Rows.Count >= 1)
                {

                    SqlCommand cmd1 = new SqlCommand("SELECT ps.*, st.specification_type_name FROM Product_Specification ps INNER JOIN Specification_Type st ON ps.specification_type_id = st.specification_type_id WHERE ps.product_id =@product_id;", con);

                    cmd1.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);




                    con.Close();


                    TextBox3.Text = dt.Rows[0]["product_name"].ToString();
                    TextBox10.Text = dt.Rows[0]["product_description"].ToString();
                    TextBox2.Text = dt.Rows[0]["product_unit_price"].ToString();
                    TextBox6.Text = dt.Rows[0]["product_quantity_stock"].ToString();

                    DropDownList2.SelectedValue = dt.Rows[0]["product_availability"].ToString();
                    DropDownList4.SelectedValue = dt.Rows[0]["brand_id"].ToString();
                    DropDownList3.SelectedValue = dt.Rows[0]["product_category_id"].ToString();



                    global_filepath1 = dt.Rows[0]["product_image1"].ToString();
                    Label12.Text = global_filepath1;

                    global_filepath2 = dt.Rows[0]["product_image2"].ToString();
                    Label1.Text = global_filepath2;

                    global_filepath3 = dt.Rows[0]["product_image3"].ToString();
                    Label2.Text = global_filepath3;

                    global_filepath4 = dt.Rows[0]["product_image4"].ToString();
                    Label3.Text = global_filepath4;


                    Dictionary<string, List<string>> specData = new Dictionary<string, List<string>>();



                    foreach (DataRow row in dt1.Rows)
                    {


                        string key = row["specification_type_id"].ToString();

                        string value = row["specification_value"].ToString();



                        if (!string.IsNullOrEmpty(key))
                        {
                            // Check if the key exists in the dictionary
                            if (!specData.ContainsKey(key))
                            {
                                // If not, initialize the list for this key
                                specData[key] = new List<string>();
                            }

                            // Add the value to the list for this key
                            specData[key].Add(value);
                        }



                    }




                    var specTypeNames = specData.Keys.ToList();
                    var specTypeValues = specData.Values.ToList();

                    // Assign specification type IDs to dropdown lists
                    if (specTypeNames.Count > 0)
                    {
                        DropDownList5.SelectedValue = specTypeNames[0]; // Assign first specification type ID
                        TextBox4.Text = string.Join(", ", specTypeValues[0]); // Populate values
                    }

                    if (specTypeNames.Count > 1)
                    {
                        DropDownList1.SelectedValue = specTypeNames[1]; // Assign second specification type ID
                        TextBox5.Text = string.Join(", ", specTypeValues[1]); // Populate values
                    }

                    // Optionally handle additional dropdowns if there are more than 2 specification types
                    if (specTypeNames.Count > 2)
                    {
                        DropDownList6.SelectedValue = specTypeNames[2]; // Assign third specification type ID
                        TextBox7.Text = string.Join(", ", specTypeValues[2]); // Populate values
                    }

                    if (specTypeNames.Count > 3)
                    {
                        DropDownList7.SelectedValue = specTypeNames[3]; // Assign fourth specification type ID
                        TextBox8.Text = string.Join(", ", specTypeValues[3]); // Populate values
                    }





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


        protected bool checkProductExistById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product where product_id=@product_id;", con);

                cmd.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());

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

        protected bool checkProductNameExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product where product_name=@product_name;", con);

                cmd.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());

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

        protected bool checkSpecificationTypeExist()
        {


            string id1 = DropDownList5.SelectedValue;
            string id2 = DropDownList1.SelectedValue;
            string id3 = DropDownList6.SelectedValue;
            string id4 = DropDownList7.SelectedValue;

            // Check if all selected values are unique
            if (id1 != id2 && id1 != id3 && id1 != id4 &&
                id2 != id3 && id2 != id4 && id3 != id4)
            {
                return false;
            }
            else
            {
                return true;
            }


        }

        protected bool checkImageExist()
        {
            try
            {
                //checked in database - image 1
                string filepath1 = "";
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);



                filepath1 = "~/asset/Image/ProductUpload/" + filename1;



                SqlConnection con1 = new SqlConnection(strcon);

                if (con1.State == System.Data.ConnectionState.Closed)
                {
                    con1.Open();
                }

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  product_image1=@product_image1 OR" +
                    " product_image2=@product_image1 OR" +
                    " product_image3=@product_image1 OR" +
                    " product_image4=@product_image1 ;", con1);



                cmd1.Parameters.AddWithValue("@product_image1", filepath1);

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                DataTable dt1 = new DataTable();

                adapter1.Fill(dt1);

                if (dt1.Rows.Count >= 1)
                {
                    con1.Close();


                    return true;
                }


                //checked in database - image 2
                string filepath2 = "";
                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);



                filepath2 = "~/asset/Image/ProductUpload/" + filename2;



                SqlConnection con2 = new SqlConnection(strcon);

                if (con2.State == System.Data.ConnectionState.Closed)
                {
                    con2.Open();
                }

                SqlCommand cmd2 = new SqlCommand("SELECT * from Product WHERE  product_image1=@product_image2 OR product_image2=@product_image2 OR product_image3=@product_image2 OR product_image4=@product_image2;", con2);

                cmd2.Parameters.AddWithValue("@product_image2", filepath2);

                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);

                DataTable dt2 = new DataTable();

                adapter2.Fill(dt2);

                if (dt2.Rows.Count >= 1)
                {
                    con2.Close();


                    return true;
                }


                //checked in database - image 3
                string filepath3 = "";
                string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);



                filepath3 = "~/asset/Image/ProductUpload/" + filename3;



                SqlConnection con3 = new SqlConnection(strcon);

                if (con3.State == System.Data.ConnectionState.Closed)
                {
                    con3.Open();
                }

                SqlCommand cmd3 = new SqlCommand("SELECT * from Product WHERE  product_image1=@product_image3 OR product_image2=@product_image3 OR product_image3=@product_image3 OR product_image4=@product_image3;", con3);

                cmd3.Parameters.AddWithValue("@product_image3", filepath3);

                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);

                DataTable dt3 = new DataTable();

                adapter3.Fill(dt3);

                if (dt3.Rows.Count >= 1)
                {
                    con3.Close();


                    return true;
                }


                //checked in database - image 4
                string filepath4 = "";
                string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);



                filepath4 = "~/asset/Image/ProductUpload/" + filename4;



                SqlConnection con4 = new SqlConnection(strcon);

                if (con4.State == System.Data.ConnectionState.Closed)
                {
                    con4.Open();
                }

                SqlCommand cmd4 = new SqlCommand("SELECT * from Product WHERE  product_image1=@product_image4 OR product_image2=@product_image4 OR product_image3=@product_image4 OR product_image4=@product_image4;", con4);

                cmd4.Parameters.AddWithValue("@product_image4", filepath4);

                SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);

                DataTable dt4 = new DataTable();

                adapter4.Fill(dt4);

                if (dt4.Rows.Count >= 1)
                {
                    con4.Close();


                    return true;
                }

                string fileName1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileName2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string fileName3 = Path.GetFileName(FileUpload3.PostedFile.FileName);
                string fileName4 = Path.GetFileName(FileUpload4.PostedFile.FileName);

                //string message = $"File 1: {fileName1}\nFile 2: {fileName2}\nFile 3: {fileName3}\nFile 4: {fileName4}";

                //// Display the alert with filenames
                //Response.Write($"<script>alert('{message}');</script>");

                // Check uniqueness using direct comparison
                if (fileName1 != fileName2 && fileName1 != fileName3 && fileName1 != fileName4 &&
                    fileName2 != fileName3 && fileName2 != fileName4 && fileName3 != fileName4)
                {
                    return false;
                }

                return true;


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return false;
            }
        }

        protected bool isEmptyWithEmptyFile()
        {
            if (!string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox5.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox7.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox8.Text.Trim()) &&
           DropDownList3.SelectedIndex != 0 &&
            DropDownList4.SelectedIndex != 0 &&
            DropDownList7.SelectedIndex != 0 &&
            DropDownList5.SelectedIndex != 0 &&
            DropDownList1.SelectedIndex != 0 &&
            DropDownList6.SelectedIndex != 0 &&
           DropDownList2.SelectedIndex != 0
           )
            {
                if (!FileUpload1.HasFile || !FileUpload4.HasFile || !FileUpload3.HasFile || !FileUpload2.HasFile)
                {
                    return true;
                }
                else
                {
                    return false;
                }


                return false;

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
           !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox4.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox5.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox7.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox8.Text.Trim()) &&
           DropDownList3.SelectedIndex != 0 &&
            DropDownList4.SelectedIndex != 0 &&
            DropDownList7.SelectedIndex != 0 &&
            DropDownList5.SelectedIndex != 0 &&
            DropDownList1.SelectedIndex != 0 &&
            DropDownList6.SelectedIndex != 0 &&
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

        protected bool checkProductExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Product where product_name=@product_name AND product_id=@product_id;", con);

                cmd.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());

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
                //checked in database - image 1
                string filepath1 = "";
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);



                filepath1 = "~/asset/Image/ProductUpload/" + filename1;



                SqlConnection con1 = new SqlConnection(strcon);

                if (con1.State == System.Data.ConnectionState.Closed)
                {
                    con1.Open();
                }

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  (product_image1=@product_image1 OR" +
                    " product_image2=@product_image1 OR" +
                    " product_image3=@product_image1 OR" +
                    " product_image4=@product_image1) AND product_id=@product_id ;", con1);



                cmd1.Parameters.AddWithValue("@product_image1", filepath1);
                cmd1.Parameters.AddWithValue("@product_id", TextBox1.Text);

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                DataTable dt1 = new DataTable();

                adapter1.Fill(dt1);

                if (dt1.Rows.Count >= 1)
                {
                    con1.Close();


                    return true;
                }


                //checked in database - image 2
                string filepath2 = "";
                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);



                filepath2 = "~/asset/Image/ProductUpload/" + filename2;



                SqlConnection con2 = new SqlConnection(strcon);

                if (con2.State == System.Data.ConnectionState.Closed)
                {
                    con2.Open();
                }

                SqlCommand cmd2 = new SqlCommand("SELECT * from Product WHERE  (product_image1=@product_image2 OR product_image2=@product_image2 OR product_image3=@product_image2 OR product_image4=@product_image2) AND product_id=@product_id ;", con2);

                cmd2.Parameters.AddWithValue("@product_image2", filepath2);
                cmd2.Parameters.AddWithValue("@product_id", TextBox1.Text);

                SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);

                DataTable dt2 = new DataTable();

                adapter2.Fill(dt2);

                if (dt2.Rows.Count >= 1)
                {
                    con2.Close();


                    return true;
                }


                //checked in database - image 3
                string filepath3 = "";
                string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);



                filepath3 = "~/asset/Image/ProductUpload/" + filename3;



                SqlConnection con3 = new SqlConnection(strcon);

                if (con3.State == System.Data.ConnectionState.Closed)
                {
                    con3.Open();
                }

                SqlCommand cmd3 = new SqlCommand("SELECT * from Product WHERE  (product_image1=@product_image3 OR product_image2=@product_image3 OR product_image3=@product_image3 OR product_image4=@product_image3) AND product_id=@product_id ;", con3);

                cmd3.Parameters.AddWithValue("@product_image3", filepath3);
                cmd3.Parameters.AddWithValue("@product_id", TextBox1.Text);

                SqlDataAdapter adapter3 = new SqlDataAdapter(cmd3);

                DataTable dt3 = new DataTable();

                adapter3.Fill(dt3);

                if (dt3.Rows.Count >= 1)
                {
                    con3.Close();


                    return true;
                }


                //checked in database - image 4
                string filepath4 = "";
                string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);



                filepath4 = "~/asset/Image/ProductUpload/" + filename4;



                SqlConnection con4 = new SqlConnection(strcon);

                if (con4.State == System.Data.ConnectionState.Closed)
                {
                    con4.Open();
                }

                SqlCommand cmd4 = new SqlCommand("SELECT * from Product WHERE  (product_image1=@product_image4 OR product_image2=@product_image4 OR product_image3=@product_image4 OR product_image4=@product_image4) AND product_id=@product_id;", con4);

                cmd4.Parameters.AddWithValue("@product_image4", filepath4);
                cmd4.Parameters.AddWithValue("@product_id", TextBox1.Text);

                SqlDataAdapter adapter4 = new SqlDataAdapter(cmd4);

                DataTable dt4 = new DataTable();

                adapter4.Fill(dt4);

                if (dt4.Rows.Count >= 1)
                {
                    con4.Close();


                    return true;
                }

                string fileName1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string fileName2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string fileName3 = Path.GetFileName(FileUpload3.PostedFile.FileName);
                string fileName4 = Path.GetFileName(FileUpload4.PostedFile.FileName);

                //string message = $"File 1: {fileName1}\nFile 2: {fileName2}\nFile 3: {fileName3}\nFile 4: {fileName4}";

                //// Display the alert with filenames
                //Response.Write($"<script>alert('{message}');</script>");

                // Check uniqueness using direct comparison
                if (fileName1 != fileName2 && fileName1 != fileName3 && fileName1 != fileName4 &&
                    fileName2 != fileName3 && fileName2 != fileName4 && fileName3 != fileName4)
                {
                    return false;
                }

                return true;


            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return false;
            }
        }

        protected void updateProductDirect()
        {
            try
            {
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string filepath1 = "~/asset/Image/ProductUpload/" + filename1;

                // if image 1 not empty, check with database got exist similar or not
                if (filename1 != "")
                {
                    SqlConnection con1 = new SqlConnection(strcon);

                    if (con1.State == System.Data.ConnectionState.Closed)
                    {
                        con1.Open();
                    }

                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  product_image1=@product_image1 OR" +
                        " product_image2=@product_image1 OR" +
                        " product_image3=@product_image1 OR" +
                        " product_image4=@product_image1 ;", con1);



                    cmd1.Parameters.AddWithValue("@product_image1", filepath1);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);

                    if (dt1.Rows.Count >= 1)
                    {
                        con1.Close();

                        Response.Write("<script>alert('Error. Similar image existed. Please change it')</script>");
                        return;
                    }
                }
                else
                {
                    filepath1 = global_filepath1;
                }

                // if image 2 not empty, check with database got exist similar or not
                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);
                string filepath2 = "~/asset/Image/ProductUpload/" + filename2;


                if (filename2 != "")
                {
                    SqlConnection con1 = new SqlConnection(strcon);

                    if (con1.State == System.Data.ConnectionState.Closed)
                    {
                        con1.Open();
                    }

                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  product_image1=@product_image1 OR" +
                        " product_image2=@product_image1 OR" +
                        " product_image3=@product_image1 OR" +
                        " product_image4=@product_image1 ;", con1);



                    cmd1.Parameters.AddWithValue("@product_image1", filepath2);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);

                    if (dt1.Rows.Count >= 1)
                    {
                        con1.Close();

                        Response.Write("<script>alert('Error. Similar image existed. Please change it')</script>");
                        return;
                    }
                }
                else
                {
                    filepath2 = global_filepath2;
                }

                // if image 3 not empty, check with database got exist similar or not
                string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);
                string filepath3 = "~/asset/Image/ProductUpload/" + filename3;


                if (filename3 != "")
                {
                    SqlConnection con1 = new SqlConnection(strcon);

                    if (con1.State == System.Data.ConnectionState.Closed)
                    {
                        con1.Open();
                    }

                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  product_image1=@product_image1 OR" +
                        " product_image2=@product_image1 OR" +
                        " product_image3=@product_image1 OR" +
                        " product_image4=@product_image1 ;", con1);



                    cmd1.Parameters.AddWithValue("@product_image1", filepath3);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);

                    if (dt1.Rows.Count >= 1)
                    {
                        con1.Close();

                        Response.Write("<script>alert('Error. Similar image existed. Please change it')</script>");
                        return;
                    }
                }
                else
                {
                    filepath3 = global_filepath3;
                }

                // if image 4 not empty, check with database got exist similar or not
                string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);
                string filepath4 = "~/asset/Image/ProductUpload/" + filename4;


                if (filename4 != "")
                {
                    SqlConnection con1 = new SqlConnection(strcon);

                    if (con1.State == System.Data.ConnectionState.Closed)
                    {
                        con1.Open();
                    }

                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE  product_image1=@product_image1 OR" +
                        " product_image2=@product_image1 OR" +
                        " product_image3=@product_image1 OR" +
                        " product_image4=@product_image1 ;", con1);



                    cmd1.Parameters.AddWithValue("@product_image1", filepath4);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);

                    if (dt1.Rows.Count >= 1)
                    {
                        con1.Close();

                        Response.Write("<script>alert('Error. Similar image existed. Please change it')</script>");
                        return;
                    }
                }
                else
                {
                    filepath4 = global_filepath4;
                }



                // Check uniqueness using direct comparison
                if (filepath1 != filepath2 && filepath1 != filepath3 && filepath1 != filepath4 &&
                    filepath2 != filepath3 && filepath2 != filepath4 && filepath3 != filepath4)
                {
                    // if unique then can update direct (delete ori, then add new)
                    if (global_filepath1 != filepath1)
                    {
                        string serverPath1 = Server.MapPath(global_filepath1);
                        if (System.IO.File.Exists(serverPath1))
                        {
                            System.IO.File.Delete(serverPath1);
                        }

                        string serverPathNew1 = Server.MapPath(filepath1);

                        FileUpload1.SaveAs(serverPathNew1);
                    }

                    if (global_filepath2 != filepath2)
                    {
                        string serverPath2 = Server.MapPath(global_filepath2);
                        if (System.IO.File.Exists(serverPath2))
                        {
                            System.IO.File.Delete(serverPath2);
                        }

                        string serverPathNew2 = Server.MapPath(filepath2);
                        FileUpload2.SaveAs(serverPathNew2);
                    }

                    if (global_filepath3 != filepath3)
                    {
                        string serverPath3 = Server.MapPath(global_filepath3);
                        if (System.IO.File.Exists(serverPath3))
                        {
                            System.IO.File.Delete(serverPath3);
                        }

                        string serverPathNew3 = Server.MapPath(filepath3);
                        FileUpload3.SaveAs(serverPathNew3);
                    }

                    if (global_filepath4 != filepath4)
                    {
                        string serverPath4 = Server.MapPath(global_filepath4);
                        if (System.IO.File.Exists(serverPath4))
                        {
                            System.IO.File.Delete(serverPath4);
                        }

                        string serverPathNew4 = Server.MapPath(filepath4);

                        FileUpload4.SaveAs(serverPathNew4);
                    }













                    //Update Product table
                    SqlConnection con = new SqlConnection(strcon);

                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    SqlCommand cmd = new SqlCommand("UPDATE Product SET product_name = @product_name, product_description = @product_description, product_unit_price = @product_unit_price, product_quantity_stock = @product_quantity_stock, product_availability = @product_availability, brand_id=@brand_id, product_category_id=@product_category_id, product_image1=@product_image1, product_image2=@product_image2, product_image3=@product_image3, product_image4=@product_image4" +
                    "" +
                        " WHERE product_id = @product_id;", con);

                    cmd.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@product_description", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@product_unit_price", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@product_quantity_stock", TextBox6.Text.Trim());



                    cmd.Parameters.AddWithValue("@product_availability", DropDownList2.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@brand_id", DropDownList4.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@product_category_id", DropDownList3.SelectedItem.Value);


                    cmd.Parameters.AddWithValue("@product_image1", filepath1);
                    cmd.Parameters.AddWithValue("@product_image2", filepath2);
                    cmd.Parameters.AddWithValue("@product_image3", filepath3);
                    cmd.Parameters.AddWithValue("@product_image4", filepath4);

                    cmd.ExecuteNonQuery();


                    //Update Specification Type table (delete ori, then add new value)
                    //-deleting
                    SqlCommand cmd6 = new SqlCommand("DELETE FROM Product_Specification WHERE product_id=@product_id;", con);

                    cmd6.Parameters.AddWithValue("@product_id", TextBox1.Text);

                    cmd6.ExecuteNonQuery();


                    //-adding
                    string[] specValuesArray1 = TextBox4.Text.Trim().Split(',');
                    string[] specValuesArray2 = TextBox5.Text.Trim().Split(',');
                    string[] specValuesArray3 = TextBox7.Text.Trim().Split(',');
                    string[] specValuesArray4 = TextBox8.Text.Trim().Split(',');

                    //5
                    foreach (string specValue in specValuesArray1)
                    {
                        // Trim any extra spaces from the value
                        string trimmedSpecValue = specValue.Trim();

                        // Create SQL command for each value
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                        // Add parameters
                        cmd2.Parameters.AddWithValue("@specification_type_id", DropDownList5.SelectedValue);  // Replace with actual specification_type_id value
                        cmd2.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                        cmd2.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                        // Execute the command
                        cmd2.ExecuteNonQuery();
                    }

                    //1
                    foreach (string specValue in specValuesArray2)
                    {
                        // Trim any extra spaces from the value
                        string trimmedSpecValue = specValue.Trim();

                        // Create SQL command for each value
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                        // Add parameters
                        cmd3.Parameters.AddWithValue("@specification_type_id", DropDownList1.SelectedValue);  // Replace with actual specification_type_id value
                        cmd3.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                        cmd3.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                        // Execute the command
                        cmd3.ExecuteNonQuery();
                    }


                    //6
                    foreach (string specValue in specValuesArray3)
                    {
                        // Trim any extra spaces from the value
                        string trimmedSpecValue = specValue.Trim();

                        // Create SQL command for each value
                        SqlCommand cmd4 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                        // Add parameters
                        cmd4.Parameters.AddWithValue("@specification_type_id", DropDownList6.SelectedValue);  // Replace with actual specification_type_id value
                        cmd4.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                        cmd4.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                        // Execute the command
                        cmd4.ExecuteNonQuery();
                    }

                    //7
                    foreach (string specValue in specValuesArray4)
                    {
                        // Trim any extra spaces from the value
                        string trimmedSpecValue = specValue.Trim();

                        // Create SQL command for each value
                        SqlCommand cmd5 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                        // Add parameters
                        cmd5.Parameters.AddWithValue("@specification_type_id", DropDownList7.SelectedValue);  // Replace with actual specification_type_id value
                        cmd5.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                        cmd5.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                        // Execute the command
                        cmd5.ExecuteNonQuery();
                    }




                    con.Close();

                    Response.Write("<script>alert('Updated Product Successfully.')</script>");

                    GridView1.DataBind();

                }
                else
                {
                    Response.Write("<script>alert('Error. Similar uploaded images. Please change it')</script>");

                    return;
                }



            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void updateProduct()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //image1

                string imagePath11 = "";
                string imagePath12 = "";
                string imagePath13 = "";
                string imagePath14 = "";

                using (SqlCommand cmd1 = new SqlCommand("SELECT * FROM Product WHERE productId = @productId", con))
                {
                    cmd1.Parameters.AddWithValue("@productId", TextBox1.Text.Trim());


                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath11 = reader["product_image1"].ToString();
                        imagePath12 = reader["product_image2"].ToString();
                        imagePath13 = reader["product_image3"].ToString();
                        imagePath14 = reader["product_image4"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath11) || !string.IsNullOrEmpty(imagePath12) || !string.IsNullOrEmpty(imagePath13) || !string.IsNullOrEmpty(imagePath14))
                {
                    string serverPath1 = Server.MapPath(imagePath11);
                    if (System.IO.File.Exists(serverPath1))
                    {
                        System.IO.File.Delete(serverPath1);
                    }

                    string serverPath2 = Server.MapPath(imagePath12);
                    if (System.IO.File.Exists(serverPath2))
                    {
                        System.IO.File.Delete(serverPath2);
                    }

                    string serverPath3 = Server.MapPath(imagePath13);
                    if (System.IO.File.Exists(serverPath3))
                    {
                        System.IO.File.Delete(serverPath3);
                    }

                    string serverPath4 = Server.MapPath(imagePath14);
                    if (System.IO.File.Exists(serverPath4))
                    {
                        System.IO.File.Delete(serverPath4);
                    }
                }



                string filepath1 = "";
                string filename1 = Path.GetFileName(FileUpload1.PostedFile.FileName);

                if (filename1 == "" || filename1 == null)
                {
                    filepath1 = global_filepath1;
                }
                else
                {
                    string serverPath = Server.MapPath("asset/Image/ProductUpload/" + filename1);
                    FileUpload1.SaveAs(serverPath);
                    filepath1 = "~/asset/Image/ProductUpload/" + filename1;
                }


                //image2
                string imagePath21 = "";
                string imagePath22 = "";
                string imagePath23 = "";
                string imagePath24 = "";

                using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM Product WHERE productId = @productId", con))
                {
                    cmd2.Parameters.AddWithValue("@productId", TextBox1.Text.Trim());


                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath21 = reader["product_image1"].ToString();
                        imagePath22 = reader["product_image2"].ToString();
                        imagePath23 = reader["product_image3"].ToString();
                        imagePath24 = reader["product_image4"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath21) || !string.IsNullOrEmpty(imagePath22) || !string.IsNullOrEmpty(imagePath23) || !string.IsNullOrEmpty(imagePath24))
                {
                    string serverPath1 = Server.MapPath(imagePath21);
                    if (System.IO.File.Exists(serverPath1))
                    {
                        System.IO.File.Delete(serverPath1);
                    }

                    string serverPath2 = Server.MapPath(imagePath22);
                    if (System.IO.File.Exists(serverPath2))
                    {
                        System.IO.File.Delete(serverPath2);
                    }

                    string serverPath3 = Server.MapPath(imagePath23);
                    if (System.IO.File.Exists(serverPath3))
                    {
                        System.IO.File.Delete(serverPath3);
                    }

                    string serverPath4 = Server.MapPath(imagePath24);
                    if (System.IO.File.Exists(serverPath4))
                    {
                        System.IO.File.Delete(serverPath4);
                    }
                }



                string filepath2 = "";
                string filename2 = Path.GetFileName(FileUpload2.PostedFile.FileName);

                if (filename2 == "" || filename2 == null)
                {
                    filepath2 = global_filepath2;
                }
                else
                {
                    string serverPath = Server.MapPath("asset/Image/ProductUpload/" + filename2);
                    FileUpload2.SaveAs(serverPath);
                    filepath2 = "~/asset/Image/ProductUpload/" + filename2;
                }




                //Image3
                string imagePath31 = "";
                string imagePath32 = "";
                string imagePath33 = "";
                string imagePath34 = "";

                using (SqlCommand cmd3 = new SqlCommand("SELECT * FROM Product WHERE productId = @productId", con))
                {
                    cmd3.Parameters.AddWithValue("@productId", TextBox1.Text.Trim());


                    SqlDataReader reader = cmd3.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath31 = reader["product_image1"].ToString();
                        imagePath32 = reader["product_image2"].ToString();
                        imagePath33 = reader["product_image3"].ToString();
                        imagePath34 = reader["product_image4"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath31) || !string.IsNullOrEmpty(imagePath32) || !string.IsNullOrEmpty(imagePath33) || !string.IsNullOrEmpty(imagePath34))
                {
                    string serverPath1 = Server.MapPath(imagePath31);
                    if (System.IO.File.Exists(serverPath1))
                    {
                        System.IO.File.Delete(serverPath1);
                    }

                    string serverPath2 = Server.MapPath(imagePath32);
                    if (System.IO.File.Exists(serverPath2))
                    {
                        System.IO.File.Delete(serverPath2);
                    }

                    string serverPath3 = Server.MapPath(imagePath33);
                    if (System.IO.File.Exists(serverPath3))
                    {
                        System.IO.File.Delete(serverPath3);
                    }

                    string serverPath4 = Server.MapPath(imagePath34);
                    if (System.IO.File.Exists(serverPath4))
                    {
                        System.IO.File.Delete(serverPath4);
                    }
                }



                string filepath3 = "";
                string filename3 = Path.GetFileName(FileUpload3.PostedFile.FileName);

                if (filename3 == "" || filename3 == null)
                {
                    filepath3 = global_filepath3;
                }
                else
                {
                    string serverPath = Server.MapPath("asset/Image/ProductUpload/" + filename3);
                    FileUpload3.SaveAs(serverPath);
                    filepath3 = "~/asset/Image/ProductUpload/" + filename3;
                }




                //image4
                string imagePath41 = "";
                string imagePath42 = "";
                string imagePath43 = "";
                string imagePath44 = "";

                using (SqlCommand cmd4 = new SqlCommand("SELECT * FROM Product WHERE productId = @productId", con))
                {
                    cmd4.Parameters.AddWithValue("@productId", TextBox1.Text.Trim());


                    SqlDataReader reader = cmd4.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath41 = reader["product_image1"].ToString();
                        imagePath42 = reader["product_image2"].ToString();
                        imagePath43 = reader["product_image3"].ToString();
                        imagePath44 = reader["product_image4"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath41) || !string.IsNullOrEmpty(imagePath42) || !string.IsNullOrEmpty(imagePath43) || !string.IsNullOrEmpty(imagePath44))
                {
                    string serverPath1 = Server.MapPath(imagePath41);
                    if (System.IO.File.Exists(serverPath1))
                    {
                        System.IO.File.Delete(serverPath1);
                    }

                    string serverPath2 = Server.MapPath(imagePath42);
                    if (System.IO.File.Exists(serverPath2))
                    {
                        System.IO.File.Delete(serverPath2);
                    }

                    string serverPath3 = Server.MapPath(imagePath43);
                    if (System.IO.File.Exists(serverPath3))
                    {
                        System.IO.File.Delete(serverPath3);
                    }

                    string serverPath4 = Server.MapPath(imagePath44);
                    if (System.IO.File.Exists(serverPath4))
                    {
                        System.IO.File.Delete(serverPath4);
                    }
                }



                string filepath4 = "";
                string filename4 = Path.GetFileName(FileUpload4.PostedFile.FileName);

                if (filename4 == "" || filename4 == null)
                {
                    filepath4 = global_filepath4;
                }
                else
                {
                    string serverPath = Server.MapPath("asset/Image/ProductUpload/" + filename4);
                    FileUpload4.SaveAs(serverPath);
                    filepath4 = "~/asset/Image/ProductUpload/" + filename4;
                }



                //Update Product table

                SqlCommand cmd = new SqlCommand("UPDATE Product SET product_name = @product_name, product_description = @product_description, product_unit_price = @product_unit_price, product_quantity_stock = @product_quantity_stock, product_availability = @product_availability, brand_id=@brand_id, product_category_id=@product_category_id, product_image1=@product_image1, product_image2=@product_image2, product_image3=@product_image3, product_image4=@product_image4" +
                    "" +
                    " WHERE product_id = @product_id;", con);


                cmd.Parameters.AddWithValue("@product_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@product_description", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@product_unit_price", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@product_quantity_stock", TextBox6.Text.Trim());



                cmd.Parameters.AddWithValue("@product_availability", DropDownList2.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@brand_id", DropDownList4.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@product_category_id", DropDownList3.SelectedItem.Value);


                cmd.Parameters.AddWithValue("@product_image1", filepath1);
                cmd.Parameters.AddWithValue("@product_image2", filepath2);
                cmd.Parameters.AddWithValue("@product_image3", filepath3);
                cmd.Parameters.AddWithValue("@product_image4", filepath4);

                cmd.ExecuteNonQuery();


                //Update Specification Type table (delete ori, then add new value)
                //-deleting
                SqlCommand cmd6 = new SqlCommand("DELETE FROM Product_Specification WHERE product_id=@product_id;", con);

                cmd6.Parameters.AddWithValue("@product_id", TextBox1.Text);

                cmd6.ExecuteNonQuery();


                //-adding
                string[] specValuesArray1 = TextBox4.Text.Trim().Split(',');
                string[] specValuesArray2 = TextBox5.Text.Trim().Split(',');
                string[] specValuesArray3 = TextBox7.Text.Trim().Split(',');
                string[] specValuesArray4 = TextBox8.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd2.Parameters.AddWithValue("@specification_type_id", DropDownList5.SelectedValue);  // Replace with actual specification_type_id value
                    cmd2.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd2.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                    // Execute the command
                    cmd2.ExecuteNonQuery();
                }

                //1
                foreach (string specValue in specValuesArray2)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd3.Parameters.AddWithValue("@specification_type_id", DropDownList1.SelectedValue);  // Replace with actual specification_type_id value
                    cmd3.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd3.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                    // Execute the command
                    cmd3.ExecuteNonQuery();
                }


                //6
                foreach (string specValue in specValuesArray3)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd4 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd4.Parameters.AddWithValue("@specification_type_id", DropDownList6.SelectedValue);  // Replace with actual specification_type_id value
                    cmd4.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd4.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                    // Execute the command
                    cmd4.ExecuteNonQuery();
                }

                //7
                foreach (string specValue in specValuesArray4)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd5 = new SqlCommand("INSERT INTO Product_Specification (specification_type_id, specification_value, product_id) VALUES (@specification_type_id, @specification_value, @product_id);", con);

                    // Add parameters
                    cmd5.Parameters.AddWithValue("@specification_type_id", DropDownList7.SelectedValue);  // Replace with actual specification_type_id value
                    cmd5.Parameters.AddWithValue("@specification_value", trimmedSpecValue);
                    cmd5.Parameters.AddWithValue("@product_id", TextBox1.Text.Trim());  // Replace with actual product_id value

                    // Execute the command
                    cmd5.ExecuteNonQuery();
                }











                con.Close();

                Response.Write("<script>alert('Updated Product Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void clearForm()
        {
            // Clear TextBoxes
            TextBox1.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox10.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox4.Text = string.Empty;
            TextBox5.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox8.Text = string.Empty;

            // Reset DropDownList to default (usually first item)
            if (DropDownList4.Items.Count > 0)
            {
                DropDownList4.SelectedIndex = 0;
            }

            if (DropDownList3.Items.Count > 0)
            {
                DropDownList3.SelectedIndex = 0;
            }

            if (DropDownList2.Items.Count > 0)
            {
                DropDownList2.SelectedIndex = 0;
            }

            if (DropDownList5.Items.Count > 0)
            {
                DropDownList5.SelectedIndex = 0;
            }

            if (DropDownList1.Items.Count > 0)
            {
                DropDownList1.SelectedIndex = 0;
            }

            if (DropDownList6.Items.Count > 0)
            {
                DropDownList6.SelectedIndex = 0;
            }

            if (DropDownList7.Items.Count > 0)
            {
                DropDownList7.SelectedIndex = 0;
            }


            // Reset FileUpload control (no way to clear file selection, but you can reset the control)
            FileUpload1.Attributes.Clear();
            FileUpload2.Attributes.Clear();
            FileUpload3.Attributes.Clear();
            FileUpload4.Attributes.Clear();

            // Clear Image display
            Image2.ImageUrl = string.Empty;
            Image1.ImageUrl = string.Empty;
            Image3.ImageUrl = string.Empty;
            Image4.ImageUrl = string.Empty;
            global_filepath1 = "";
            global_filepath2 = "";
            global_filepath3 = "";
            global_filepath4 = "";


            Label12.Text = "";
            Label1.Text = "";
            Label2.Text = "";
            Label3.Text = "";

        }

        protected bool isNumber()
        {

            if (double.TryParse(TextBox2.Text.Trim(), out double result))
            {

            }
            else
            {
                return false;
            }

            if (double.TryParse(TextBox6.Text.Trim(), out double result1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}