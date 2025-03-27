<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="LoginCustomer.aspx.cs" Inherits="adminPortal.WebForm10" %>

<%@ Register Assembly="Recaptcha.Web" Namespace="Recaptcha.Web.UI.Controls" TagPrefix="cc1" %>

<%@ Register Assembly="GoogleReCaptcha" Namespace="GoogleReCaptcha" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="row top-banner">
        <h4>Login As Customer <span style="color: #F02757">.</span></h4>
    </div>

    <!-- login section-->
    <div class="login">
        <div class="container">
            <div class="login-container">
                <div class="left-container">
                    <h3>WELCOME</h3>
                    <p>Login into your customer account.</p>

                    <!-- Login Section -->
                    <asp:Panel ID="LoginSection" runat="server">
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server"  onkeypress="return event.keyCode != 13;" placeholder="Enter Email"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" onkeypress="return event.keyCode != 13;" placeholder="Enter Password" TextMode="Password"></asp:TextBox>
                        </div>
                        <cc1:RecaptchaWidget ID="Recaptcha1" runat="server" />
                        <div class="form-group">
                            <asp:Button class="signupbtn" ID="Button1" runat="server" Text="LOGIN" OnClick="Button1_Click" />
                        </div>
                    </asp:Panel>

                    <!-- 2FA Section -->
                    <%-- <asp:Panel ID="TwoFactorSection" runat="server" Visible="false">
                        <div class="form-group">
                            <label for="TextBox2FACode">Enter the 2FA Code sent to your email:</label>
                            <asp:TextBox ID="TextBox2FACode" runat="server" CssClass="form-control" placeholder="Enter 2FA Code"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="Verify2FA" runat="server" Text="Verify 2FA Code" CssClass="btn btn-primary" OnClick="Verify2FA_Click" />
                        </div>
                    </asp:Panel>--%>

                    <p class="registration-text">
                        You don't have an account ?
                        <a href="SignUpCustomer.aspx" style="color: dodgerblue">Sign Up Now</a>
                    </p>

                    <!-- Forgot password link -->
                    <p class="passwordrecover-text">
                        Forget Password ?
                    <a href="PasswordRecovery.aspx" style="color: dodgerblue">Reset Here</a>
                    </p>
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
