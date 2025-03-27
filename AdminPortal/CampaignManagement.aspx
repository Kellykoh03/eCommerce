<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="CampaignManagement.aspx.cs" Inherits="adminPortal.WebForm16" %>


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
            left: 1px;
            top: 0px;
            background-color: var(--bs-body-bg);
        }

        .auto-style2 {
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
        <h2 class="page-title">Campaign Management</h2>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Promotional_Campaign]"></asp:SqlDataSource>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="promotional_campaign_id" DataSourceID="SqlDataSource1" class="table table-striped table-bordered">
                        <Columns>
                            <asp:BoundField DataField="promotional_campaign_id" HeaderText="Id" InsertVisible="False" ReadOnly="True" SortExpression="promotional_campaign_id" />
                            <asp:BoundField DataField="promotional_campaign_name" HeaderText="Campaign Name" SortExpression="promotional_campaign_name" />
                            <asp:TemplateField HeaderText="Image">

                                <ItemTemplate>
                                    <asp:Image ID="Image3" runat="server" class="img-fluid p-2" Height="200px" ImageUrl='<%# Eval("promotion_image") %>' />
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
                                <asp:TextBox ID="TextBox12" runat="server" CssClass="form-control mr-2" Width="120px" Style="left: 0px; top: 0px"></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" Text="Get" CssClass="btn btn-primary" OnClick="Button1_Click" Style="width: 60px" />
                                <span class="small-text">(ID will be auto generated when adding new item)</span>

                            </asp:Panel>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <label>Campaign Name (*must unique)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="auto-style2" onkeypress="return event.keyCode != 13;" ID="TextBox3" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <label>Start Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" onkeypress="return event.keyCode != 13;" ID="TextBox6" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label>End Date</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="auto-style2" onkeypress="return event.keyCode != 13;" ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col">
                            <label>Discount Percentage (eg: 0-100)</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="auto-style1" onkeypress="return event.keyCode != 13;" ID="TextBox10" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <label>Campaign Image</label>
                            <div class="row">
                                <div class="col-10">
                                    File Name (*must unique):
                                </div>
                                <div class="col-10">
                                    <asp:Label ID="Label12" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <img id="imgview" height="150px" width="100px" src="" style="display: none;" />
                                <asp:Image ID="Image2" runat="server" CssClass="pb-5"></asp:Image>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <label>Product ID that included in this campaign (*unique)</label>
                            <span class="small-text">(seperate each product ID with comma ,)</span>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" onkeypress="return event.keyCode != 13;" ID="TextBox7" runat="server"></asp:TextBox>
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
