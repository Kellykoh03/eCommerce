<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="Homepage.aspx.cs" Inherits="adminPortal.WebForm5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div id="carouselIntervalIndicator" class="carousel carousel-dark slide" data-bs-ride="carousel">
    <div class="carousel-indicators">
        <button type="button" data-bs-target="#carouselIntervalIndicator" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
        <button type="button" data-bs-target="#carouselIntervalIndicator" data-bs-slide-to="1" aria-label="Slide 2"></button>
        <button type="button" data-bs-target="#carouselIntervalIndicator" data-bs-slide-to="2" aria-label="Slide 3"></button>
    </div>
    <div id="bannerCarousel" class="carousel-inner" runat="server">
        
    </div>
    <button class="carousel-control-prev" type="button" data-bs-target="#carouselIntervalIndicator" data-bs-slide="prev">
        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Previous</span>
    </button>
    <button class="carousel-control-next" type="button" data-bs-target="#carouselIntervalIndicator" data-bs-slide="next">
        <span class="carousel-control-next-icon" aria-hidden="true"></span>
        <span class="visually-hidden">Next</span>
    </button>
</div>

<div class="section">
    <div class="my-4" style="text-align: center; font-weight: bold; font-family: Arial; font-size: 24px;">
        <p class="sub-head">Shop By Categories<span style="color: #F02757">.</span></p>
    </div>

    <div id="categories" class="container text-center" runat="server">

    </div>

</div>


<div class="section">
    <div class="my-4" style="text-align: center; font-weight: bold; font-family: Arial; font-size: 24px;">
        <p class="sub-head">New Arrival<span style="color: #F02757">.</span></p>
    </div>

    <div id="carouselIntervalIndicatorMultiItem" class="carousel carousel-dark slide" data-bs-ride="carousel">
        <div id="carouselContainer" runat="server">

        </div>
        
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselIntervalIndicatorMultiItem" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselIntervalIndicatorMultiItem" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Next</span>
        </button>
    </div>
</div>

</asp:Content>
