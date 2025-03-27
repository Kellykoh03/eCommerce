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
using System.Reflection.Emit;

namespace adminPortal
{
    public partial class WebForm16 : System.Web.UI.Page
    {
        static string global_filepath;
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Campaign Management";

            GridView1.DataBind();

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkCampaignExistById())
            {
                getCampaignById();
            }
            else
            {
                Response.Write("<script>alert('Campaign Id not exist, try other')</script>");
            }
        }

        protected void Button13_Click(object sender, EventArgs e)
        {
            if (checkCampaignExistById())
            {
                deleteCampaign();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Campaign Id not exist, try other')</script>");
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

            if (!isDateLegit())
            {
                Response.Write("<script>alert('Error. Start Date and End Date Invalid. Please change it')</script>");
                return;
            }

            if (!isNumber())
            {
                Response.Write("<script>alert('Error. Invalid number input. Please change it')</script>");
                return;
            }

            if (!isProductIdLegit())
            {
                Response.Write("<script>alert('Error. Product ID invalid. Please change it')</script>");
                return;
            }

            if (isProductIdExist())
            {
                Response.Write("<script>alert('Error. Product ID already participate in promotional campaign. Please change it')</script>");
                return;
            }


            if (checkCampaignNameExist())
            {
                Response.Write("<script>alert('Campaign Name already exist, try other campaign name')</script>");
                return;
            }
            else
            {

                addNewCampaign();
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

            if (!isDateLegit())
            {
                Response.Write("<script>alert('Error. Start Date and End Date Invalid. Please change it')</script>");
                return;
            }

            if (!isNumber())
            {
                Response.Write("<script>alert('Error. Invalid number input. Please change it')</script>");
                return;
            }

            if (!isProductIdLegit())
            {
                Response.Write("<script>alert('Error. Product ID invalid. Please change it')</script>");
                return;
            }

            if (isProductIdExistAtOthers()) {
                Response.Write("<script>alert('Error. New Product ID already participate in promotional campaign. Please change it')</script>");
                return;
            }

           
            if (checkCampaignExistForUpdate())
            {
                // Check if name got replace with new or not. If no, then continue check other input field
            }
            else if (checkCampaignNameExist())
            {
                Response.Write("<script>alert('Error. Similar campaign name existed already. Please change it')</script>");
                return;
            }


            if (!checkCampaignExistById())
            {
                Response.Write("<script>alert('Campaign Id not exist, try other')</script>");
                return;
            }


            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

            if (filename == "")
            {
                //updateWithoutFile
                updateCampaignWithoutFile();
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
                    updateCampaign();
                    clearForm();
                    return;

                }

            }
        }

        protected void updateCampaign()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //delete old image
                string imagePath = "";
                using (SqlCommand cmd1 = new SqlCommand("SELECT * FROM Promotional_Campaign WHERE promotional_campaign_id = @promotional_campaign_id", con))
                {
                    cmd1.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd1.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath = reader["promotion_image"].ToString();
                    }
                    reader.Close(); // Close the reader before proceeding
                }

                if (!string.IsNullOrEmpty(imagePath))
                {
                    string serverPath1 = Server.MapPath(imagePath);
                    if (System.IO.File.Exists(serverPath1))
                    {
                        System.IO.File.Delete(serverPath1);
                    }
                }




                //update campaign table

                SqlCommand cmd = new SqlCommand("UPDATE Promotional_Campaign SET promotional_campaign_name = @promotional_campaign_name, start_date = @start_date, end_date = @end_date, discount_percentage = @discount_percentage, promotion_image=@promotion_image WHERE promotional_campaign_id = @promotional_campaign_id;", con);


                cmd.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@start_date", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@end_date", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@discount_percentage", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string relativeName = "~/asset/Image/CampaignUpload/" + filename;

                global_filepath = relativeName;

                string serverPath = Server.MapPath("~/asset/Image/CampaignUpload/" + filename);

                FileUpload1.SaveAs(serverPath);

                cmd.Parameters.AddWithValue("@promotion_image", relativeName);



                cmd.ExecuteNonQuery();


                //update campaign product table
                //Update Specification Type table (delete ori, then add new value)
                //-deleting
                SqlCommand cmd6 = new SqlCommand("DELETE FROM Campaign_Product WHERE promotional_campaign_id=@promotional_campaign_id;", con);

                cmd6.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text);

                cmd6.ExecuteNonQuery();


                //-adding
                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Campaign_Product (promotional_campaign_id, product_id) VALUES (@promotional_campaign_id, @product_id);", con);

                    // Add parameters
                    cmd2.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text);
                    cmd2.Parameters.AddWithValue("@product_id", trimmedSpecValue);


                    // Execute the command
                    cmd2.ExecuteNonQuery();
                }





                con.Close();

                Response.Write("<script>alert('Updated Campaign Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }


        protected void updateCampaignWithoutFile()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                //update campaign table

                SqlCommand cmd = new SqlCommand("UPDATE Promotional_Campaign SET promotional_campaign_name = @promotional_campaign_name, start_date = @start_date, end_date = @end_date, discount_percentage = @discount_percentage WHERE promotional_campaign_id = @promotional_campaign_id;", con);


                cmd.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@start_date", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@end_date", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@discount_percentage", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

                cmd.ExecuteNonQuery();


                //update campaign product table
                //Update Specification Type table (delete ori, then add new value)
                //-deleting
                SqlCommand cmd6 = new SqlCommand("DELETE FROM Campaign_Product WHERE promotional_campaign_id=@promotional_campaign_id;", con);

                cmd6.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text);

                cmd6.ExecuteNonQuery();


                //-adding
                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Campaign_Product (promotional_campaign_id, product_id) VALUES (@promotional_campaign_id, @product_id);", con);

                    // Add parameters
                    cmd2.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text);
                    cmd2.Parameters.AddWithValue("@product_id", trimmedSpecValue);


                    // Execute the command
                    cmd2.ExecuteNonQuery();
                }





                con.Close();

                Response.Write("<script>alert('Updated Campaign Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void deleteCampaign()
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
                using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM Promotional_Campaign WHERE promotional_campaign_id = @promotional_campaign_id;", con))
                {
                    cmd2.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());
                    SqlDataReader reader = cmd2.ExecuteReader();

                    if (reader.Read())
                    {
                        imagePath1 = reader["promotion_image"].ToString();
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


                SqlCommand cmd = new SqlCommand("DELETE FROM Promotional_Campaign WHERE promotional_campaign_id = @promotional_campaign_id;", con);

                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

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

        protected void addNewCampaign()
        {
            try
            {

                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);

                string relativeName = "~/asset/Image/CampaignUpload/" + filename;

                global_filepath = relativeName;

                string serverPath = Server.MapPath("~/asset/Image/CampaignUpload/" + filename);

                FileUpload1.SaveAs(serverPath);


                SqlConnection con = new SqlConnection(strcon);



                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                SqlCommand cmd = new SqlCommand("INSERT INTO Promotional_Campaign(promotional_campaign_name, start_date, end_date, discount_percentage, promotion_image) values(@promotional_campaign_name, @start_date, @end_date, @discount_percentage, @promotion_image);", con);



                cmd.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@start_date", TextBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@end_date", TextBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@discount_percentage", TextBox10.Text.Trim());
                cmd.Parameters.AddWithValue("@promotion_image", relativeName);

                cmd.ExecuteNonQuery();


                //add to campaign product table
                //get new ID then do insert

                SqlCommand cmd1 = new SqlCommand("SELECT * from Promotional_Campaign where promotional_campaign_name=@promotional_campaign_name;", con);

                cmd1.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());

                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                DataTable dt = new DataTable();

                adapter1.Fill(dt);

                string newId = "";

                if (dt.Rows.Count >= 1)
                {
                    newId = dt.Rows[0]["promotional_campaign_id"].ToString();
                }


                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO Campaign_Product (promotional_campaign_id, product_id) VALUES (@promotional_campaign_id, @product_id);", con);

                    // Add parameters
                    cmd2.Parameters.AddWithValue("@promotional_campaign_id", newId);
                    cmd2.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    // Execute the command
                    cmd2.ExecuteNonQuery();
                }


                con.Close();

                Response.Write("<script>alert('Added New Campaign Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }
        }

        protected void getCampaignById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Promotional_Campaign where promotional_campaign_id=@promotional_campaign_id;", con);

                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);



                if (dt.Rows.Count >= 1)
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Campaign_Product WHERE promotional_campaign_id =@promotional_campaign_id;", con);

                    cmd1.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt1 = new DataTable();

                    adapter1.Fill(dt1);


                    con.Close();


                    TextBox3.Text = dt.Rows[0]["promotional_campaign_name"].ToString();
                    TextBox6.Text = Convert.ToDateTime(dt.Rows[0]["start_date"]).ToString("yyyy-MM-dd");
                    TextBox2.Text = Convert.ToDateTime(dt.Rows[0]["end_date"]).ToString("yyyy-MM-dd");
                    TextBox10.Text = dt.Rows[0]["discount_percentage"].ToString();
                    Label12.Text = dt.Rows[0]["promotion_image"].ToString();
                    global_filepath = dt.Rows[0]["promotion_image"].ToString();


                    Dictionary<string, List<string>> specData = new Dictionary<string, List<string>>();



                    foreach (DataRow row in dt1.Rows)
                    {


                        string key = row["promotional_campaign_id"].ToString();

                        string value = row["product_id"].ToString();



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

                    var specTypeValues = specData.Values.ToList();

                    TextBox7.Text = string.Join(", ", specTypeValues[0]); // Populate 



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

        protected bool checkCampaignExistById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Promotional_Campaign where promotional_campaign_id=@promotional_campaign_id;", con);

                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

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

        protected bool isEmptyWithEmptyFile()
        {
            if (
           !string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           !string.IsNullOrWhiteSpace(TextBox7.Text.Trim())
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

        protected bool checkImageExist()
        {
            try
            {
                string filepath = "";
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);



                filepath = "~/asset/Image/CampaignUpload/" + filename;




                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Promotional_Campaign where promotion_image=@promotion_image;", con);

                cmd.Parameters.AddWithValue("@promotion_image", filepath);

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

        protected bool checkCampaignNameExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Promotional_Campaign where promotional_campaign_name=@promotional_campaign_name;", con);

                cmd.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());

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

        protected bool isDateLegit()
        {
            DateTime startDate = DateTime.Parse(TextBox6.Text.Trim());
            DateTime endDate = DateTime.Parse(TextBox2.Text.Trim());

            // Check if end_date is later than start_date
            if (endDate > startDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool isProductIdLegit()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    // Create SQL command for each value
                    SqlCommand cmd = new SqlCommand("SELECT * from Product where product_id=@product_id;", con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    // Execute the command
                    cmd.ExecuteNonQuery();


                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    adapter.Fill(dt);



                    if (dt.Rows.Count >= 1)
                    {

                    }
                    else
                    {
                        return false;
                    }
                }


                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return false;
            }

        }

        protected bool isNumber()
        {
            string text10 = TextBox10.Text.Trim();

            if (double.TryParse(text10, out double result))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool isEmpty()
        {
            if (string.IsNullOrWhiteSpace(TextBox12.Text.Trim()) &&
           string.IsNullOrWhiteSpace(TextBox3.Text.Trim()) &&
           string.IsNullOrWhiteSpace(TextBox6.Text.Trim()) &&
           string.IsNullOrWhiteSpace(TextBox2.Text.Trim()) &&
           string.IsNullOrWhiteSpace(TextBox10.Text.Trim()) &&
           string.IsNullOrWhiteSpace(TextBox7.Text.Trim())

           )
            {
                return true;

            }
            else
            {
                return false;
            }


        }

        protected bool checkCampaignExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Promotional_Campaign where promotional_campaign_name=@promotional_campaign_name AND promotional_campaign_id=@promotional_campaign_id;", con);

                cmd.Parameters.AddWithValue("@promotional_campaign_name", TextBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

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

        protected bool isProductIdExistForUpdate()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    SqlCommand cmd2 = new SqlCommand("SELECT * from Campaign_Product where product_id=@product_id AND ;", con);

                    cmd2.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);

                    DataTable dt2 = new DataTable();

                    adapter2.Fill(dt2);

                    if (dt2.Rows.Count >= 1)
                    {


                        return true;
                    }
                    else
                    {
                        return false;
                    }


                    SqlCommand cmd1 = new SqlCommand("SELECT * from Campaign_Product where product_id=@product_id;", con);

                    cmd1.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt = new DataTable();

                    adapter1.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {


                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

                return false;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return true;
            }
        }


        protected bool isProductIdExist()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    SqlCommand cmd1 = new SqlCommand("SELECT * from Campaign_Product where product_id=@product_id;", con);

                    cmd1.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);

                    DataTable dt = new DataTable();

                    adapter1.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }

                return false;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return true;
            }
        }


        protected bool isProductIdExistAtOthers()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }


                string[] specValuesArray1 = TextBox7.Text.Trim().Split(',');

                //5
                foreach (string specValue in specValuesArray1)
                {
                    // Trim any extra spaces from the value
                    string trimmedSpecValue = specValue.Trim();

                    SqlCommand cmd2 = new SqlCommand("SELECT *               FROM Campaign_Product                WHERE product_id = @product_id               AND promotional_campaign_id != @promotional_campaign_id", con);


                    cmd2.Parameters.AddWithValue("@product_id", trimmedSpecValue);

                    cmd2.Parameters.AddWithValue("@promotional_campaign_id", TextBox12.Text.Trim());

                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);

                    DataTable dt2 = new DataTable();

                    adapter2.Fill(dt2);

                    if (dt2.Rows.Count >= 1)
                    {


                        return true;
                    }
                    else
                    {
                        return false;
                    }



                }

                return false;

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
                return true;
            }
        }

        protected void clearForm()
        {
            // Clear TextBoxes
            TextBox12.Text = string.Empty;
            TextBox3.Text = string.Empty;
            TextBox6.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox10.Text = string.Empty;
            Label12.Text = string.Empty;
            TextBox7.Text = string.Empty;


            // Reset FileUpload control (no way to clear file selection, but you can reset the control)
            FileUpload1.Attributes.Clear(); // This may reset attributes, including the file

            // Clear Image display
            Image2.ImageUrl = string.Empty;
            global_filepath = "";


        }
    }
}