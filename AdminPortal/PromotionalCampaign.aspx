<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="PromotionalCampaign.aspx.cs" Inherits="adminPortal.WebForm24" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="row top-banner">
        <h4>Promotional Campaign <span style="color: #F02757">.</span></h4>
    </div>

    <div class="page-container">
        <div style="text-align: center;">
            <asp:Image ID="imgPromo" runat="server" />
        </div>
        <h3 style="text-align: center;">
            <asp:Label ID="lblPromoTitle" runat="server" Text="" Font-Size="22"></asp:Label>
        </h3>
       
         <div class="description">
             <p><strong>Product Included:</strong><asp:Label ID="lblProduct" runat="server" Text=""></asp:Label> </p>        
             <p><strong>Flash Sales:</strong><asp:Label ID="lblDiscountPercent" runat="server" Text=""></asp:Label> </p>
            <p><strong>Start Date:</strong><asp:Label ID="lblStartDate" runat="server" Text=""></asp:Label> </p>
            <p><strong>End Date:</strong><asp:Label ID="lblEndDate" runat="server" Text=""></asp:Label></p>
         </div>
    </div>




   
</asp:Content>
