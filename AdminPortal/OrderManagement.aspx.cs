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
using System.Drawing;

namespace adminPortal
{
    public partial class WebForm15 : System.Web.UI.Page
    {

        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }

            this.Title = "Order Management";
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkOrderExistById())
            {
                getOrderById();

                displayOrderInfo();
            }
            else
            {
                Response.Write("<script>alert('Order Id not exist, try other')</script>");
            }
        }

        protected void Button15_Click(object sender, EventArgs e)
        {
            if (isEmpty())
            {
                Response.Write("<script>alert('Error. There is empty fields. Please fill all')</script>");
                return;
            }

            if (checkOrderExistById())
            {
                updateOrder();
                clearForm();
            }
            else
            {
                Response.Write("<script>alert('Order Id not exist, try other')</script>");
            }



        }

        protected void updateOrder()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("UPDATE Order_Form SET order_status=@order_status WHERE order_form_id =@order_form_id", con);

                cmd.Parameters.AddWithValue("@order_status", DropDownList5.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@order_form_id", TextBox12.Text);

                cmd.ExecuteNonQuery();

                con.Close();

                Response.Write("<script>alert('Order Updated Successfully.')</script>");

                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");
            }






        }

        protected bool isEmpty()
        {
            if (DropDownList5.SelectedIndex == 0
           )
            {
                return true;

            }
            else
            {
                return false;
            }


        }

        protected void getOrderById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Order_Form where order_form_id=@order_form_id;", con);

                cmd.Parameters.AddWithValue("@order_form_id", TextBox12.Text.Trim());

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);



                if (dt.Rows.Count >= 1)
                {
                    con.Close();


                    TextBox3.Text = Convert.ToDateTime(dt.Rows[0]["order_date"]).ToString("yyyy-MM-dd");

                    DropDownList5.SelectedValue = dt.Rows[0]["order_status"].ToString();

                    TextBox5.Text = dt.Rows[0]["customer_id"].ToString();
                    TextBox10.Text = dt.Rows[0]["shipping_address"].ToString();
                    TextBox7.Text = dt.Rows[0]["shipping_city"].ToString();
                    TextBox2.Text = dt.Rows[0]["shipping_postal_code"].ToString();
                    TextBox11.Text = dt.Rows[0]["shipping_country"].ToString();
                    TextBox1.Text = dt.Rows[0]["payment_total_amount"].ToString();



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

        protected bool checkOrderExistById()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * from Order_Form where order_form_id=@order_form_id;", con);

                cmd.Parameters.AddWithValue("@order_form_id", TextBox12.Text.Trim());

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
            TextBox5.Text = string.Empty;
            TextBox7.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox11.Text = string.Empty;
            TextBox1.Text = string.Empty;



            if (DropDownList5.Items.Count > 0)
            {
                DropDownList5.SelectedIndex = 0;
            }

        }

        protected void displayOrderInfo()
        {
            try
            {
                Label1.Text = "";

                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT    OFA.order_form_id,     OFA.payment_total_amount,     P.product_id,     P.product_name,     P.product_unit_price,     OI.order_item_id,   OI.order_item_name,          OI.order_item_quantity,    OI.order_item_discount,   OI.order_unit_price       FROM    Order_Form OFA INNER JOIN     Order_Item OI ON OFA.order_form_id = OI.order_form_id INNER JOIN     Product P ON OI.product_id = P.product_id WHERE     OFA.order_form_id = @order_form_id;", con);

                cmd.Parameters.AddWithValue("@order_form_id", TextBox12.Text.Trim());

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    string totalPayment = "";

                    Label1.Text += $@"<div style='width: 80%; margin: 0 auto; text-align: center;'>";

                    while (reader.Read())
                    {
                        // Extract values
                        totalPayment = reader[1].ToString();
                        string productId = reader[2].ToString();
                        string productName = reader[6].ToString();
                        string productUnitPrice = reader[9].ToString();
                        string orderItemQuantity = reader[7].ToString();
                        string orderItemDiscount = reader[8].ToString();

                        // Dynamically generate a label content with the retrieved data
                        Label1.Text += $@"
                    <p class='orderInfo'>Product ID: {productId}</p>
                    <p class='orderInfo'>Product Name: {productName}</p>
                    <p class='orderInfo'>Product Unit Price (RM): {productUnitPrice}</p>
                    <p class='orderInfo'>Quantity: {orderItemQuantity}</p>
                    <p class='orderInfo'>Discount (RM): {orderItemDiscount}</p>
                    <hr />
                ";
                    }

                    Label1.Text += $@"
               <p class='orderInfo' style='font-size: 20px; font-weight: bold; text-align:center !important;'>Total Payment (RM): {totalPayment}</p>";

                    Label1.Text += $@"</div>";
                }
                else
                {
                    Label1.Text = "No order details found.";
                }

                con.Close();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }
        }
    }
}