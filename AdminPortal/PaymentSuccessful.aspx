<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="PaymentSuccessful.aspx.cs" Inherits="adminPortal.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <meta http-equiv="refresh" content="3; URL=Homepage.aspx" />
    <div style="margin-top: 100px; margin-bottom: 100px;">
        <div class="row">
            <img src="asset/Image/green_tick.png" alt="greenTick" style="margin-left: auto; margin-right: auto; margin-bottom:30px; width: 350px; height: 300px;" />
        </div>
        <div class="row" style="text-align: center;">
            <h3>Your payment has been received.</h3>
        </div>
        <div class="row" style="text-align: center;">
            <h4>You will be redirected to home page in 3 seconds.</h4>
        </div>
        <div class="row">
            <asp:Button ID="btnHomePage" runat="server" Text="Go To Homepage" PostBackUrl="~/Homepage.aspx" CssClass="btnHomePageStyle" />
        </div>
    </div>
</asp:Content>
