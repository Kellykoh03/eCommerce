<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="FeaturedBrands.aspx.cs" Inherits="adminPortal.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="asset/css/featuredBrands.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row top-banner">
    <h4>Take A Look On Our Featured Brands <span style="color: #F02757">.</span></h4>
</div>
<div id="data-container">
    <div class="card-container">
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class="card">
                    <asp:HyperLink ID="HyperLink1" runat="server" 
                        NavigateUrl='<%# Eval("brand_id", "~/InCategoriesProduct.aspx?BrandID={0}") %>'>
                        <img src="<%# Eval("image") %>" alt="<%#Eval("company_name") %>" class="brand-image">
                    </asp:HyperLink>
                    <h2><%#Eval("company_name") %></h2>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Brand]"></asp:SqlDataSource>
    </div>
</div>


<div class="pagination" id="pagination">
    <a class="prevNextBtn" href="#" id="prev">Previous        <a href="#" class="page-link" data-page="1">1</a>
    <a href="#" class="page-link" data-page="2">2</a>
    <a href="#" class="page-link" data-page="3">3</a>
    <a class="prevNextBtn" href="#" id="next">Next</a>
</div>

<p style="text-align: center;" id="page-numbers"></p>
<script src="asset/js/featuredBrands.js"> </script>
</asp:Content>
