<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="CustomerAccount.aspx.cs" Inherits="adminPortal.WebForm6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="asset/css/customerAccount.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="row top-banner">
    <h4>My Account <span style="color: #F02757">.</span></h4>
</div>

<div class="myAccount-mainContainer">

  

<div class="d-flex">
    <div class="nav flex-column nav-pills me-3" id="v-pills-tab" role="tablist" aria-orientation="vertical">
        <button style="border-radius: 0;" class="nav-link active btnSelections" id="v-pills-dashboard-tab" data-bs-toggle="pill" data-bs-target="#v-pills-dashboard" type="button" role="tab" aria-controls="v-pills-dashboard" aria-selected="true">Dashboard</button>
        <button style="border-radius: 0;" class="nav-link btnSelections" id="v-pills-orders-tab" data-bs-toggle="pill" data-bs-target="#v-pills-orders" type="button" role="tab" aria-controls="v-pills-orders" aria-selected="false">Orders</button>
        <a href="Homepage.aspx" style="text-decoration: none; color: inherit;">
            <asp:Button ID="btnLogout" runat="server" Text="Logout" CssClass="nav-link btnSelections" data-bs-toggle="pill" data-bs-target="#v-pills-logout" type="button" role="tab" aria-controls="v-pills-logout" aria-selected="false" OnClick="btnLogout_Click"/>
        </a>
    </div>
    <div class="tab-content" id="v-pills-tabContent">
        <div class="tab-pane fade show active" id="v-pills-dashboard" role="tabpanel" aria-labelledby="v-pills-dashboard-tab">

            <%-- My Account Section --%>
            <div class="myAccount profile">
                <div class="container">
                    <div class="myAccount-container">
                        <div class="left-container">

                            <div class="row">
                                <div class="col-md-12">
                                    <label>Email</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" TextMode="Email" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-4">
                                    <label>Old Password</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Password" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>New Password</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" TextMode="Password"></asp:TextBox>
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
                                <div class="col-md-4">
                                    <label>Country</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlCountry" runat="server">
                                            <asp:ListItem>Select a country</asp:ListItem>
                                            <asp:ListItem>Malaysia</asp:ListItem>
                                            <asp:ListItem>Singapore</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>City</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlCity" runat="server">
                                            <asp:ListItem>Select a city</asp:ListItem>
                                            <asp:ListItem>Kuala Lumpur</asp:ListItem>
                                            <asp:ListItem>Marina Bay</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <label>Post Code</label>
                                    <div class="form-group">
                                        <asp:DropDownList CssClass="form-control" ID="ddlPostCode" runat="server">
                                            <asp:ListItem>Select Post Code</asp:ListItem>
                                            <asp:ListItem>55300</asp:ListItem>
                                            <asp:ListItem>53300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 20px;">
                                <div class="col-md-12">
                                    <label>My Address</label>
                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="txtAddress" runat="server"></asp:TextBox>
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



        </div>
        <div class="tab-pane fade" id="v-pills-orders" role="tabpanel" aria-labelledby="v-pills-orders-tab">
            <div style="background-color: white; border-radius: 20px; border: 1px solid black; min-width: 750px; min-height: 600px;">
                <h3 style="margin-left: 20px; margin-top: 20px; margin-bottom:20px;">Order History</h3>

                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div style="border: 1px solid black; border-radius: 20px; margin: 10px; padding: 10px;">
                                <p><strong>Order Form ID:</strong> <%# Eval("OrderFormId") %></p>
                                <p><strong>Order Date:</strong> <%# Eval("OrderDate") %></p>
                                <p><strong>Status:</strong> <%# Eval("OrderStatus") %></p>
                                <p><strong>Total Paid:</strong> <%# "RM " + Eval("PaymentTotalAmount") %></p>

                                <!-- Nested Repeater for displaying order items -->
                                <asp:Repeater ID="RepeaterItems" runat="server" DataSource='<%# Eval("Items") %>'>
                                    <ItemTemplate>
                                        <div style="padding-left: 20px;">
                                            <p><strong>Product:</strong> <%# Eval("OrderItemName") %> Price: RM <%# Eval("OrderUnitPrice") %> Qty: <%# Eval("OrderItemQuantity") %></p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </div>
        </div>
        <div class="tab-pane fade" id="v-pills-logout" role="tabpanel" aria-labelledby="v-pills-logout-tab">
            You have logged out.
        </div>
    </div>
</div>
      </div>




</asp:Content>
