<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="adminPortal.PasswordRecovery" %>

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
                    <h3>FORGOT YOUR PASSWORD?</h3>
                    <p>Please enter your email, answer the security question, and enter a new password to reset it.</p>

                    <!-- Password Recovery Form -->
                    <div id="PasswordRecoverySection" runat="server">
                        <!-- Enter Email -->
                        <asp:Label ID="LabelEmail" runat="server" Text="Enter your email:" CssClass="recovery-label"></asp:Label><br />
                        <asp:TextBox ID="TextBoxRecoveryEmail" runat="server" CssClass="recovery-input"></asp:TextBox><br />

                        <!-- Security Question -->
                        <asp:Label ID="LabelSecurityQuestion" runat="server" Text="Security Question:" CssClass="recovery-label"></asp:Label><br />
                        <asp:DropDownList CssClass="recovery-input" ID="DropDownListSecurityQuestion" runat="server">
                            <asp:ListItem Value="What was your first pet's name?">What was your first pet's name?</asp:ListItem>
                            <asp:ListItem Value="What is your mother's name?">What is your mother's name?</asp:ListItem>
                            <asp:ListItem Value="What was the name of your school?">What was the name of your school?</asp:ListItem>
                        </asp:DropDownList><br />

                        <asp:Label ID="Label1" runat="server" Text="Security Answer:" CssClass="recovery-label"></asp:Label><br />
                        <asp:TextBox ID="TextBoxSecurityAnswer" runat="server"  CssClass="recovery-input"></asp:TextBox><br />

                        <!-- New Password -->
                        <asp:Label ID="LabelNewPassword" runat="server" Text="Enter New Password:" CssClass="recovery-label" ></asp:Label><br />
                        <asp:TextBox ID="TextBoxNewPassword" runat="server" TextMode="Password" CssClass="recovery-input" ></asp:TextBox><br />

                        <!-- Reset Password Button -->
                        <div class="form-group">
                            <asp:Button class="signupbtn" ID="ButtonResetPassword" runat="server" Text="Reset Password" OnClick="ButtonResetPassword_Click" />
                        </div>
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
