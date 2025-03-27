<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="SpecificationTypeManagement.aspx.cs" Inherits="adminPortal.WebForm20" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
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
                        columns: [{ width: '10%' }, { width: '50%' }, { width: '40%' }]
                    }
                );
            });
    </script>
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
            left: 0px;
            top: 0px;
            background-color: var(--bs-body-bg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <h2 class="page-title">Specification Type Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="specification_type_id" DataSourceID="SqlDataSource1" class="table table-striped table-bordered">

                        <Columns>
                            <asp:BoundField DataField="specification_type_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="specification_type_id" />
                            <asp:BoundField DataField="specification_type_name" HeaderText="Specification Type Name" SortExpression="specification_type_name" />
                            <asp:BoundField DataField="specification_type_description" HeaderText="Specification Type Description" SortExpression="specification_type_description" />



                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Specification_Type]"></asp:SqlDataSource>


                </div>
            </div>
        </div>
    </div>

    <%-- entry detail --%>
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
                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control mr-2" Width="120px"></asp:TextBox>
                                    <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                    <span class="small-text">(ID will be auto generated when adding new item)</span>

                                </asp:Panel>
                            </div>
                        </div>
                    </div>


                    <asp:Panel ID="Panel2" runat="server">

                        <div class="row">
                            <div class="col-md-12">
                                <label>Specification Type name</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" onkeypress="return event.keyCode != 13;"></asp:TextBox>
                                </div>
                            </div>
                        </div>





                        <div class="row">
                            <div class="col">
                                <label>Specification Type description</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="auto-style1" ID="TextBox10" runat="server" TextMode="MultiLine" Rows="2" onkeypress="return event.keyCode != 13;"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>





                    <div class="row">
                        <div class="actions-column">
                            <div class="button-group">
                                <asp:Button ID="Button13" runat="server" Text="Delete" CssClass="btn signupbtn" OnClick="Button13_Click" />
                                <asp:Button ID="Button14" runat="server" Text="Add" CssClass="btn signupbtn" OnClick="Button14_Click" />
                                <asp:Button ID="Button15" runat="server" Text="Update" CssClass="btn signupbtn" OnClick="Button15_Click" />
                            </div>
                        </div>



                    </div>

                </div>


            </div>

        </div>
    </div>
</asp:Content>
