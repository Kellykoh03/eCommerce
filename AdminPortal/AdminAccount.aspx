<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="AdminAccount.aspx.cs" Inherits="adminPortal.WebForm12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="asset/css/adminAccount.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            display: block;
            width: 100%;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: var(--bs-body-color);
            -webkit-appearance: none;
            -moz-appearance: none;
            appearance: none;
            background-clip: padding-box;
            border-radius: var(--bs-border-radius);
            transition: none;
            left: 2px;
            top: 0px;
            background-color: var(--bs-body-bg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- My Account Section --%>
    <div class="myAccount">
        <div class="container">
            <div class="row">
                <h2 class="page-title">Profile Setting</h2>
            </div>
            <div class="myAccount-container">
                <div class="left-container">

                    <div class="row">
                        <div class="col-md-12">
                            <label>Email (eg:123@gmail.com)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-4">
                            <label>Old Password</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" ReadOnly="true" Text="**************"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>New Password</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="auto-style1" ID="TextBox11" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label>Repeat New Password</label>
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
                        <div class="col-md-12">
                            <label>Contact No (eg: 012-3456789)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="Phone"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Button class="signupbtn" ID="Button1" runat="server" Text="Update" OnClick="Button1_Click" />
                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
