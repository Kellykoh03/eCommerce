<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="InCategoriesProduct.aspx.cs" Inherits="adminPortal.WebForm25" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="asset/css/style.css" rel="stylesheet" />
    <style type="text/css">
    .auto-style1 {
        margin-left: 120px;
        margin-right: 120px;
        float: left;
        height: 622px;
    }
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row top-banner">
    <h4>Search Result <span style="color: #F02757">.</span></h4>
</div>

<div id="data-container" style="min-height: 700px;">
    <div class="container my-3">
        <div class="row">
            <div class="col">
            </div>
            <div class="col" style="text-align: end;">
                <asp:DropDownList ID="ddlFilterByPresetOption" runat="server" CssClass="ddlFilterByPresetOptionStyle" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterByPresetOption_SelectedIndexChanged">
                    <asp:ListItem>Sort By</asp:ListItem>
                    <asp:ListItem Value="ASC">Ascending Alphabetical A-Z</asp:ListItem>
                    <asp:ListItem Value="DESC">Descending Alphabetical A-Z</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>

    <aside class="auto-style1">
        <p>
            <img src="asset/Image/Svg/funnel.svg" />Filter
        </p>
        <p style="margin: 0; padding: 0; font-weight: bold; font-family: Arial;">
            Price
        </p>
        <div style="width: 55px; border: 1px solid black; margin-bottom: 10px;"></div>

        <asp:TextBox ID="txtStartPrice" runat="server" placeholder="Min Price" OnTextChanged="txtStartPrice_TextChanged"></asp:TextBox>
        <br />
        <br />

        <asp:TextBox ID="txtEndPrice" runat="server" placeholder="Max Price" OnTextChanged="txtEndPrice_TextChanged"></asp:TextBox>
        <br />

        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btnFilterStyle" />
        <p style="margin: 0; padding: 0; font-weight: bold; font-family: Arial;">
            Brand
        </p>
        <div style="width: 55px; border: 1px solid black; margin-bottom: 10px;"></div>
        <asp:CheckBoxList ID="cblFilterBrand" runat="server" DataSourceID="SqlDataSource3" DataTextField="company_name" DataValueField="brand_id" OnSelectedIndexChanged="cblFilterBrand_SelectedIndexChanged">
            
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Brand]"></asp:SqlDataSource>
        <br />
        <p style="margin: 0; padding: 0; font-weight: bold; font-family: Arial;">
            Categories
        </p>
        <div style="width: 95px; border: 1px solid black; margin-bottom: 10px;"></div>
        <asp:CheckBoxList ID="cblFilterCategories" runat="server" DataSourceID="SqlDataSource2" DataTextField="product_category_name" DataValueField="product_category_id" OnSelectedIndexChanged="cblFilterCategories_SelectedIndexChanged">
            
        </asp:CheckBoxList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product_Category]"></asp:SqlDataSource>
        <br />
    </aside>
    <div class="card-container">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate>
            <div class="card">
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl='<%# Eval("product_id", "~/ProductDetails.aspx?ProductID={0}") %>'>
                    <img src="<%# Eval("product_image1").ToString().Replace("~","") %>" alt="<%# Eval("product_name") %>">
                </asp:HyperLink>
                <h3><%#Eval("product_name") %></h3>
                <p>RM <%#Eval("product_unit_price") %></p>
            </div>
        </ItemTemplate>
    </asp:Repeater>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product]">
        </asp:SqlDataSource>
    </div>
</div>

<div class="pagination" id="pagination">
    <a class="prevNextBtn" href="#" id="prev">Previous</a>
    <a href="#" class="page-link" data-page="1">1</a>
    <a href="#" class="page-link" data-page="2">2</a>
    <a class="prevNextBtn" href="#" id="next">Next</a>
</div>

<p style="text-align: center;" id="page-numbers"></p>

<script src="asset/js/script.js"> </script>
</asp:Content>
