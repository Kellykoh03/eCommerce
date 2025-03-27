<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="SignUpCustomer.aspx.cs" Inherits="adminPortal.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="row top-banner">
    <h4>Sign Up As Customer <span style="color: #F02757">.</span></h4>
</div>

<!-- SIGN UP section-->
<div class="login">
    <div class="container">
        <div class="login-container">
            <div class="left-container">
                <h3>WELCOME</h3>
                <p>Sign up to get latest update in our community.</p>

                <div class="row">

                    <div class="col-md-12">
                        <label>Email</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                </div>


                <div class="row">
                    <div class="col-md-6">
                        <label>Password</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Repeat Password</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Full Name</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <label>Contact No</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="Phone"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <label>Date of Birth</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Select a Security Question</label>
                        <div class="form-group">
                            <asp:DropDownList CssClass="form-control" ID="DropDownListSecurityQuestion" runat="server">
                                <asp:ListItem Value="Pet">What was your first pet's name?</asp:ListItem>
                                <asp:ListItem Value="Mother">What is your mother's name?</asp:ListItem>
                                <asp:ListItem Value="School">What was the name of your school?</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <label>Security Answer</label>
                        <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBoxSecurityAnswer" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>



                <div class="form-group">
                    <asp:Button class="signupbtn" ID="Button1" runat="server" Text="SIGN UP" OnClick="Button1_Click" />
                </div>
                <p class="registration-text">
                    You have an customer account ?
                    <a href="LoginCustomer.aspx" style="color: dodgerblue">Login Now</a>
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

