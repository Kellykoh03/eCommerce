<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="BrandManagement.aspx.cs" Inherits="adminPortal.WebForm17" %>

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
                        columns: [{ width: '10%' }, { width: '90%' }]
                    }
                );
            });

        function readUrl(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview').attr('src', e.target.result);
                    $('#imgview').css('display', 'block');
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <h2 class="page-title">Brand Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">

                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="brand_id" DataSourceID="SqlDataSource1" class="table table-striped table-bordered" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:BoundField DataField="brand_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="brand_id" />



                            <asp:TemplateField>
                                <ItemTemplate>
                                    <div class="container-fluid">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("brand_name") %>' Font-Size="XX-Large" Font-Bold="True"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        Company Name:
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Text='<%# Eval("company_name") %>'></asp:Label>
                                                        &nbsp; |&nbsp; Contact Name:
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Text='<%# Eval("contact_name") %>'></asp:Label>
                                                        &nbsp; |&nbsp; Phone No.:
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Text='<%# Eval("phone_number") %>'></asp:Label>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        Fax:
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Text='<%# Eval("fax") %>'></asp:Label>
                                                        &nbsp; |&nbsp; Email:
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Text='<%# Eval("email") %>'></asp:Label>
                                                        &nbsp; |&nbsp; isShowMenu: <strong>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Eval("isShowMenu") %>'></asp:Label>
                                                        </strong>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        Address: <strong>
                                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("address") %>'></asp:Label>
                                                        </strong>

                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        City: <strong>
                                                            <asp:Label ID="Label9" runat="server" Text='<%# Eval("city") %>'></asp:Label>
                                                            &nbsp; </strong>|<strong>&nbsp; </strong>Postal Code: <strong>
                                                                <asp:Label ID="Label10" runat="server" Text='<%# Eval("postal_code") %>'></asp:Label>
                                                                &nbsp; </strong>|&nbsp; Country: <strong>
                                                                    <asp:Label ID="Label11" runat="server" Text='<%# Eval("country") %>'></asp:Label>
                                                                </strong>

                                                    </div>
                                                </div>

                                            </div>
                                            <div class="col-lg-2">
                                                <asp:Image class="img-fluid p-2" ID="Image1" runat="server" Height="200" ImageUrl='<%# Eval("image") %>' />
                                            </div>
                                        </div>
                                    </div>


                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>


                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Brand]"></asp:SqlDataSource>

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
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control mr-2" Width="120px"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <span class="small-text">(ID will be auto generated when adding new item)</span>

                            </asp:Panel>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Brand Name (*must unique)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox8" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Company Name</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>PIC Name</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox4" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-md-12">
                            <label>Email (eg. lim@gmail.com)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" onkeypress="return event.keyCode != 13;" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Contact No (eg. 012-3456789)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox6" onkeypress="return event.keyCode != 13;" runat="server" TextMode="Phone"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Fax No</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox5" onkeypress="return event.keyCode != 13;" runat="server" TextMode="Phone"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col">
                            <label>Address</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox10" onkeypress="return event.keyCode != 13;" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>City</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox7" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>Postal</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox9" onkeypress="return event.keyCode != 13;" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Country</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox11" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row option-row">



                        <div class="col-md-12">
                            <label>Do you want to show in nav menu?</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList3" runat="server" onkeypress="return event.keyCode != 13;">
                                    <asp:ListItem Value="&quot;&quot;">--empty--</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Brand Image (*must unique)</label>
                            <div class="form-group">
                                <div class="row">
                                    <div class="row">
                                        <div class="col-2">
                                            File Name:
                                        </div>
                                        <div class="col-10">
                                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>


                                    <asp:FileUpload ID="FileUpload1" onchange="readUrl(this)" runat="server" />




                                </div>

                                <img id="imgview" height="150px" width="100px" src="" style="display: none;" />

                                <asp:Image ID="Image2" runat="server" CssClass="pb-5"></asp:Image>

                            </div>
                        </div>
                    </div>


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
