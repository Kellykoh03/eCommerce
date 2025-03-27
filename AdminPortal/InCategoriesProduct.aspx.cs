using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace adminPortal
{
    public partial class WebForm25 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "Categories Product";


            if (Session["role"] == "admin")
            {

                Response.Redirect("AnalyticDashboard.aspx");
            }

            if (!IsPostBack)
            {
                FilterProducts();
            }
        }

        protected void cblFilterBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        protected void cblFilterCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        protected void FilterProducts()
        {
            decimal startPrice = 0;
            decimal endPrice = 10000;

            if (!txtStartPrice.Text.IsNullOrWhiteSpace())
            {
                startPrice = decimal.Parse(txtStartPrice.Text);
            }
            if (!txtEndPrice.Text.IsNullOrWhiteSpace())
            {
                endPrice = decimal.Parse(txtEndPrice.Text);
            }

            List<string> selectedBrands = new List<string>();
            List<string> selectedCategories = new List<string>();

            string sortDirection = ddlFilterByPresetOption.SelectedValue;

            // Get selected brands
            foreach (ListItem item in cblFilterBrand.Items)
            {
                if (item.Selected)
                {
                    selectedBrands.Add(item.Value);
                }
            }

            // Get selected categories
            foreach (ListItem item in cblFilterCategories.Items)
            {
                if (item.Selected)
                {
                    selectedCategories.Add(item.Value);
                }
            }

            string query = "SELECT * FROM [Product] WHERE 1=1";

            string productName = null;
            if (Request.QueryString["ProductName"] != null)
            {
                productName = Server.UrlDecode(Request.QueryString["ProductName"]);
            }

            if (!string.IsNullOrEmpty(productName))
            {
                query += $" AND product_name LIKE '%' + '{productName}' + '%'";
            }

            // Prioritize query string's id first
            string brandID = Request.QueryString["BrandID"];
            if (!string.IsNullOrEmpty(brandID))
            {
                query += $" AND brand_id = '{brandID}'";
            }

            string categoryID = Request.QueryString["CategoryID"];
            if (!string.IsNullOrEmpty(categoryID))
            {
                query += $" AND product_category_id = '{categoryID}'";
            }

            query += $" AND product_unit_price >= {startPrice}";
            query += $" AND product_unit_price <= {endPrice}";

            //Add brand filter if any brands are selected
            if (selectedBrands.Count > 0)
            {
                string brandFilter = string.Join(",", selectedBrands.Select(b => "'" + b + "'"));
                query += $" AND brand_id IN ({brandFilter})";
            }

            // Add category filter if any categories are selected
            if (selectedCategories.Count > 0)
            {
                string categoryFilter = string.Join(",", selectedCategories.Select(c => "'" + c + "'"));
                query += $" AND product_category_id IN ({categoryFilter})";
            }

            if (sortDirection != "Sort By")
            {
                query += $" ORDER BY product_name {sortDirection}";
            }

            // Apply the updated query to the SqlDataSource
            SqlDataSource1.SelectCommand = query;
        }

        protected void ddlFilterByPresetOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        protected void txtStartPrice_TextChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }

        protected void txtEndPrice_TextChanged(object sender, EventArgs e)
        {
            FilterProducts();
        }
    }
}