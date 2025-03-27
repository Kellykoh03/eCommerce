<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="CategoryManagement.aspx.cs" Inherits="adminPortal.WebForm19" %>

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
                        columns: [{ width: '10%' }, { width: '40%' }, { width: '50%' }]
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
        <h2 class="page-title">Category Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Product_Category]"></asp:SqlDataSource>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="product_category_id" DataSourceID="SqlDataSource1" class="table table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="product_category_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="product_category_id" />
                            <asp:BoundField DataField="product_category_name" HeaderText="Name" SortExpression="product_category_name" />
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image ID="Image3" runat="server" class="img-fluid p-2" ImageUrl='<%# Eval("product_category_image") %>' Height="200" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
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
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                                <span class="mr-2">ID:</span>
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control mr-2" Width="120px" OnTextChanged="TextBox12_TextChanged"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" />
                                <span class="small-text">(ID will be auto generated when adding new item)</span>

                            </asp:Panel>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Category name(*must unique)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" onkeypress="return event.keyCode != 13;"></asp:TextBox>
                            </div>
                        </div>
                    </div>





                    <div class="row">
                        <div class="col">
                            <label>Category description</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox10" onkeypress="return event.keyCode != 13;" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>


                    <div class="row option-row">

                        <div class="col-md-6">
                            <label>Do you want to show in Homepage?</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList2" runat="server" onkeypress="return event.keyCode != 13;">
                                    <asp:ListItem Value="&quot;&quot;">--EMPTY--</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-6">
                            <label>Do you want to show in nav menu?</label>
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList3" runat="server" onkeypress="return event.keyCode != 13;">
                                    <asp:ListItem Value="&quot;&quot;">--EMPTY--</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Category Image (*must unique)</label>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-2">
                                        File Name:
                                    </div>
                                    <div class="col-10">
                                        <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <asp:FileUpload ID="FileUpload1" runat="server" onchange="readUrl(this)" />
                                <img id="imgview" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image2" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <%--<div class="row">
                        <div class="col-md-12">
                            <label>Specification type 1 name:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <label>Specification type 1 description:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                                        <div class="row">
                        <div class="col-md-12">
                            <label>Specification type 2 name:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <label>Specification type 2 description:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                                                            <div class="row">
                        <div class="col-md-12">
                            <label>Specification type 3 name:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <label>Specification type 3 description:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                                                            <div class="row">
                        <div class="col-md-12">
                            <label>Specification type 4 name:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox9" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col">
                            <label>Specification type 4 description:</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" TextMode="MultiLine" Rows="2"></asp:TextBox>
                            </div>
                        </div>
                    </div>--%>


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
