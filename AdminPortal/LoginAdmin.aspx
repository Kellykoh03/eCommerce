<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="LoginAdmin.aspx.cs" Inherits="adminPortal.WebForm11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="row top-banner">
        <h4>Login As Admin<span style="color: #F02757">.</span></h4>
    </div>

    <!-- login section-->
    <div class="login">
        <div class="container">
            <div class="login-container">
                <div class="left-container">
                    <h3>WELCOME</h3>
                    <p>Login into your account.</p>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Enter Email"  onkeypress="return event.keyCode != 13;"  OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Enter Password"  onkeypress="return event.keyCode != 13;" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button class="signupbtn" ID="Button1" runat="server" Text="LOGIN" OnClick="Button1_Click" />
                    </div>
                </div>
                <div class="right-container">
                    <div class="textContent">
                        <img src="asset/Image/verticallogo.png" />
                        <p>Best electronic store in Malaysia</p>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
