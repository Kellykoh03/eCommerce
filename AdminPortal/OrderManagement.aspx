<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="OrderManagement.aspx.cs" Inherits="adminPortal.WebForm15" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".table1").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable(
                    {
                        layout: {

                            topEnd: {
                                search: {
                                    placeholder: 'Type search here'
                                }
                            }
                        }
                        //columns: [{ width: '10%' }, { width: '40%' }, { width: '50%' }]
                    }
                );
            });
</script>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            height: 26px;
        }

        .auto-style2 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <h2 class="page-title">Order Management<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Order_Form]"></asp:SqlDataSource>
        </h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="order_form_id" DataSourceID="SqlDataSource1" class="table table1 table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="order_form_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="order_form_id" />
                            <asp:BoundField DataField="order_date" HeaderText="Date" SortExpression="order_date" />
                            <asp:BoundField DataField="order_status" HeaderText="Status" SortExpression="order_status" />
                            <asp:BoundField DataField="payment_total_amount" HeaderText="Total" SortExpression="payment_total_amount" />
                        </Columns>
                    </asp:GridView>



                </div>
            </div>
        </div>
    </div>


    <%-- order details section --%>
    <div class="entryDetail">
        <div class="container">
            <div class="entryDetail-container">
                <div class="left-container">
                    <div class="row">
                        <h2 class="page-title">Entry Details</h2>
                        <hr style="padding-bottom: 20px;">
                    </div>
                    <div class="row" style="padding-bottom: 20px; font-weight: 700;">

                        <div class="col-md-12">
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                                <span class="mr-2">ID:</span>
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control mr-2" Width="120px" Style="left: 0px; top: 0px"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <span class="small-text">(ID will be auto generated when adding new item)</span>

                            </asp:Panel>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-6">
                            <label>Order Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" TextMode="Date" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label style="padding-bottom: 10px;">Order Status</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList5" runat="server" AppendDataBoundItems="true">
                                    <Items>
                                        <asp:ListItem Text="--Empty--"></asp:ListItem>
                                        <asp:ListItem>Completed</asp:ListItem>
                                        <asp:ListItem>Received</asp:ListItem>
                                        <asp:ListItem>Delivering</asp:ListItem>
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">


                        <div class="col-md-12">
                            <label>Customer Id</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col">
                            <label>Shipping Address</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" TextMode="MultiLine" Rows="2" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Shipping City</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Shipping Postal</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Number" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Shipping Country</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Total Payment Amount</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>

                    </div>


                    <div>
                        <div class="col-md-12 mt-4">
                            <h2>Order Item List</h2>
                        </div>
                    </div>
                    <div class="row">
                        <hr/>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </div>


                    <%-- <div class="row">
                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <th class="auto-style1">ID</th>
                                    <th class="auto-style2">Product</th>
                                    <th class="auto-style1">Qty</th>
                                    <th class="auto-style1">Price</th>
                                    <th class="auto-style1">Discount</th>
                                    <th class="auto-style1">Total</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td class="col-md-1 text-center">1000</td>
                                    <td class="col-md-7"><em>Baked Rodopa Sheep Feta</em></h4></td>
                                    <td class="col-md-1" style="text-align: center">2 </td>
                                    <td class="col-md-1 text-center">$13</td>
                                    <td class="col-md-1 text-center">$1</td>
                                    <td class="col-md-1 text-center">$26</td>
                                </tr>
                                <tr>
                                    <td class="col-md-1 text-center">1001</td>
                                    <td class="col-md-7"><em>Lebanese Cabbage Salad</em></h4></td>
                                    <td class="col-md-1" style="text-align: center">1 </td>
                                    <td class="col-md-1 text-center">$8</td>
                                    <td class="col-md-1 text-center">$1</td>
                                    <td class="col-md-1 text-center">$8</td>
                                </tr>
                                <tr>
                                    <td class="col-md-1 text-center">1002</td>
                                    <td class="col-md-7"><em>Baked Tart with Thyme and Garlic</em></h4></td>
                                    <td class="col-md-1" style="text-align: center">3 </td>
                                    <td class="col-md-1 text-center">$16</td>
                                    <td class="col-md-1 text-center">$1</td>
                                    <td class="col-md-1 text-center">$48</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="text-right">
                                        <span>
                                            <strong>Subtotal: </strong>
                                        </span>
                                        <span>
                                            <strong>Tax 2: </strong>
                                        </span>
                                        <span>
                                            <strong>Tax 1: </strong>
                                        </span></td>
                                    <td class="text-center"><span>
                                        <strong>$6.94</strong>
                                    </span>
                                        <span>
                                            <strong>$6.94</strong>
                                        </span>
                                        <span>
                                            <strong>$6.94</strong>
                                        </span></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="text-right">
                                        <h4><strong>Total: </strong></h4>
                                    </td>
                                    <td class="text-center text-danger">
                                        <h4><strong>$31.53</strong></h4>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>



                    <div class="row">
                        <div class="actions-column">
                            <div class="button-group">
                                <asp:Button ID="Button15" runat="server" Text="Update" CssClass="btn signupbtn" OnClick="Button15_Click" />
                            </div>
                        </div>



                    </div>

                </div>


            </div>

        </div>
    </div>

</asp:Content>
