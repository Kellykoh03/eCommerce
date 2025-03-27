<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="adminPortal.WebForm14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(
            function () {
                $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable(
                    {
                        layout: {

                            topEnd: {
                                search: {
                                    placeholder: 'Type search here'
                                }
                            }
                        },
                        columns: [{ width: '30%' }, { width: '70%' }]
                    }
                );
            });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <h2 class="page-title">User Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id" DataSourceID="SqlDataSource1" class="table table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="customer_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="customer_id" />
                            <asp:BoundField DataField="customer_name" HeaderText="Name" SortExpression="customer_name" />
                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Customer]"></asp:SqlDataSource>



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
                            <div class="form-inline">

                                <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                                    <span class="mr-2">ID:</span>
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control mr-2" Width="120px" OnClick="Button1_Click"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />

                                </asp:Panel>
                            </div>
                        </div>
                    </div>



                    <div class="row">

                        <div class="col-md-12">
                            <label>Email</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" TextMode="Email" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Password</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Full Name</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Contact No</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="Phone" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Date of Birth</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" TextMode="Date" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>





                </div>
            </div>
        </div>
    </div>
</asp:Content>
