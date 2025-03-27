using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Analytic Dashboard";

            if (!IsPostBack)
            {
                BindChart();
                //BindStats();
            }

            if (Session["role"] != "admin")
            {

                Response.Redirect("Homepage.aspx");
            }

        }


        protected void BindChart()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);

                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT               FORMAT(order_date, 'yyyy-MM') AS OrderMonth,                 COUNT(order_form_id) AS TotalOrders                      FROM                 Order_Form         WHERE                 YEAR(order_date) = YEAR(GETDATE())                     GROUP BY               FORMAT(order_date, 'yyyy-MM')                           ORDER BY              OrderMonth DESC;", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                Chart1.Series["Orders"].XValueMember = "OrderMonth";
                Chart1.Series["Orders"].YValueMembers = "TotalOrders";
                Chart1.DataSource = dt;
                Chart1.DataBind();

            }
            catch (Exception ex)
            {
                Response.Write("<script>alert(" + ex.Message + ")</script>");

            }

        }



        //protected void BindStats()
        //{
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(strcon);

        //        if (con.State == System.Data.ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }

        //        SqlCommand cmd = new SqlCommand("SELECT     COUNT(order_form_id) AS TotalOrders FROM    Order_Form WHERE     YEAR(order_date) = YEAR(GETDATE())     AND MONTH(order_date) = MONTH(GETDATE());", con);
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        DataTable dt = new DataTable();
        //        adapter.Fill(dt);

        //        if (dt.Rows.Count >= 1)
        //        {
        //            //order for current month
        //            Label1.Text = dt.Rows[0]["TotalOrders"].ToString(); ;

        //        }


        //        SqlCommand cmd2 = new SqlCommand("SELECT    SUM(payment_total_amount) AS TotalRevenue FROM     Order_Form WHERE    YEAR(order_date) = YEAR(GETDATE())    AND MONTH(order_date) = MONTH(GETDATE());", con);
        //        //SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
        //        //DataTable dt2 = new DataTable();
        //        //adapter2.Fill(dt2
        //        //
        //        string totalRevenue = "0";

        //        float result = cmd2.ExecuteScalar();

        //        // Check if the result is not null
        //        if (result != DBNull.Value && result != null)
        //        {
        //            totalRevenue = Convert.ToString(result);

        //            Label2.Text = totalRevenue;
        //            con.Close();
        //        }


        //        Label2.Text = "0";
        //        con.Close();
        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<script>alert(" + ex.Message + ")</script>");

        //    }

        //}
    }
}