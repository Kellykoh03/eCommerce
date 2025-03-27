<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="ProductManagement.aspx.cs" Inherits="adminPortal.WebForm13" %>

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
                        columns: [{ width: '10%' }, { width: '30%' }, { width: '10%' }, { width: '50%' }]
                    }
                );
            });

        function readUrl1(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview1').attr('src', e.target.result);
                    $('#imgview1').css('display', 'block');
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

        function readUrl2(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview2').attr('src', e.target.result);
                    $('#imgview2').css('display', 'block');
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

        function readUrl3(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview3').attr('src', e.target.result);
                    $('#imgview3').css('display', 'block');
                };

                reader.readAsDataURL(input.files[0]);
            }
        }

        function readUrl4(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    $('#imgview4').attr('src', e.target.result);
                    $('#imgview4').css('display', 'block');
                };

                reader.readAsDataURL(input.files[0]);
            }
        }
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
            left: 2px;
            top: 1px;
            background-color: var(--bs-body-bg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <h2 class="page-title">Product Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product]"></asp:SqlDataSource>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="product_id" DataSourceID="SqlDataSource4" class="table table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="product_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="product_id" />
                            <asp:BoundField DataField="product_name" HeaderText="Name" SortExpression="product_name" />
                            <asp:BoundField DataField="product_unit_price" HeaderText="Unit Price" SortExpression="product_unit_price" />
                            <asp:TemplateField HeaderText="Image 1">
                                <ItemTemplate>
                                    <asp:Image ID="Image5" runat="server" class="img-fluid p-2" Height="200" ImageUrl='<%# Eval("product_image1") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
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
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control mr-2" Width="120px"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <span class="small-text">(ID will be auto generated when adding new item)</span>

                            </asp:Panel>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Name (*must unique)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="auto-style1" ID="TextBox3" onkeypress="return event.keyCode != 13;" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Description</label>
                            <asp:TextBox CssClass="form-control" ID="TextBox10" onkeypress="return event.keyCode != 13;" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Unit Price</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" onkeypress="return event.keyCode != 13;" ID="TextBox2" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <label>Quantity Stock</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" onkeypress="return event.keyCode != 13;" ID="TextBox6" runat="server" TextMode="Number"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row pb-3">
                        <div class="col-md-4">
                            <label style="padding-bottom: 10px;">Brand</label>

                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList4" runat="server" DataSourceID="SqlDataSource2" DataTextField="brand_name" DataValueField="brand_id" AppendDataBoundItems="true">
                                    <Items>
                                        <asp:ListItem Text="--Empty--" Value="" />
                                    </Items>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [brand_id], [brand_name] FROM [Brand]"></asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label style="padding-bottom: 10px;">Availability</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList2" runat="server">
                                    <asp:ListItem Value="&quot;&quot;">--empty--</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <label style="padding-bottom: 10px;">Product Category</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource3" DataTextField="product_category_name" DataValueField="product_category_id" AppendDataBoundItems="true">
                                    <Items>
                                        <asp:ListItem Text="--Empty--" Value="" />
                                    </Items>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [product_category_id], [product_category_name] FROM [Product_Category]"></asp:SqlDataSource>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Image 1 (*must unique)</label>
                            <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" onchange="readUrl1(this)" />
                                <img id="imgview1" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image2" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Image 2 (*must unique)</label>
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload2" runat="server" onchange="readUrl2(this)" />
                                <img id="imgview2" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image1" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Image 3 (*must unique)</label>
                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload3" runat="server" onchange="readUrl3(this)" />
                                <img id="imgview3" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image3" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Product Image 4 (*must unique)</label>
                            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload4" runat="server" onchange="readUrl4(this)" />
                                <img id="imgview4" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image4" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <div class="row product-type-row">
                        <div class="col-md-12">
                            <div class="header-group">
                                <label>
                                    Product Specification Type 1:&nbsp;
                                    <asp:DropDownList ID="DropDownList5" runat="server" DataSourceID="SqlDataSource5" DataTextField="specification_type_name" DataValueField="specification_type_id" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="--Empty--" Value="" />
                                        </Items>
                                    </asp:DropDownList>
                                    &nbsp;(*must unique)</label>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [specification_type_id], [specification_type_name] FROM [Specification_Type]"></asp:SqlDataSource>
                            </div>
                            <div class="form-group">
                                <div class="option-group">
                                    <label for="option1">Option (*seperate by comma):</label>
                                    <asp:TextBox CssClass="form-control option-input" onkeypress="return event.keyCode != 13;" ID="TextBox4" runat="server"></asp:TextBox>

                                </div>

                            </div>

                        </div>
                    </div>

                    <div class="row product-type-row">
                        <div class="col-md-12">
                            <div class="header-group">
                                <label>
                                    Product Specification Type 2:&nbsp;
                                    <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource5" DataTextField="specification_type_name" DataValueField="specification_type_id" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="--Empty--" Value="" />
                                        </Items>
                                    </asp:DropDownList>
                                    &nbsp;(*must unique)</label>
                            </div>
                            <div class="form-group">
                                <div class="option-group">
                                    <label for="option1">Option (*seperate by comma):</label>
                                    <asp:TextBox CssClass="form-control option-input" onkeypress="return event.keyCode != 13;" ID="TextBox5" runat="server"></asp:TextBox>

                                </div>

                            </div>

                        </div>
                    </div>

                    <div class="row product-type-row">
                        <div class="col-md-12">
                            <div class="header-group">
                                <label>
                                    Product Specification Type 3:&nbsp;
                                    <asp:DropDownList ID="DropDownList6" runat="server" DataSourceID="SqlDataSource5" DataTextField="specification_type_name" DataValueField="specification_type_id" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="--Empty--" Value="" />
                                        </Items>
                                    </asp:DropDownList>
                                    &nbsp;(*must unique)</label>
                            </div>
                            <div class="form-group">
                                <div class="option-group">
                                    <label for="option1">Option (*seperate by comma):</label>
                                    <asp:TextBox CssClass="form-control option-input" onkeypress="return event.keyCode != 13;" ID="TextBox7" runat="server"></asp:TextBox>

                                </div>

                            </div>

                        </div>
                    </div>

                    <div class="row product-type-row">
                        <div class="col-md-12">
                            <div class="header-group">
                                <label>
                                    Product Specification Type 4:&nbsp;
                                    <asp:DropDownList ID="DropDownList7" runat="server" DataSourceID="SqlDataSource5" DataTextField="specification_type_name" DataValueField="specification_type_id" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="--Empty--" Value="" />
                                        </Items>
                                    </asp:DropDownList>
                                    &nbsp;(*must unique)</label>
                            </div>
                            <div class="form-group">
                                <div class="option-group">
                                    <label for="option1">Option (*seperate by comma):</label>
                                    <asp:TextBox CssClass="form-control option-input" onkeypress="return event.keyCode != 13;" ID="TextBox8" runat="server"></asp:TextBox>

                                </div>

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
