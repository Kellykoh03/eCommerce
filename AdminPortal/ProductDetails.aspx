<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="adminPortal.WebForm8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="asset/css/productDetails.css" rel="stylesheet" />
    <style type="text/css">
    .auto-style1 {
        border-style: none;
        border-color: inherit;
        border-width: medium;
        background-image: url('asset/Image/svg/plus.svg');
        background-repeat: no-repeat;
        background-color: transparent;
        position: absolute;
        top: 12px;
        left: 80px;
    }
    .auto-style2{
        background-image: url('asset/Image/svg/dash.svg');
        background-repeat: no-repeat;
        border: none;
        background-color: transparent;
        height: 15px;
        position: absolute;
        top: 12px;
    }
</style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="container mt-4 containerStyle">
        <div class="row my-3">
            <div class="col-md-6">
                <div class="row">
                    <div id="carouselIndicatorProductDetails" class="carousel carousel-dark slide" data-interval="false">
                        <div class="carousel-indicators">
                            <button type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
                            <button type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="1" aria-label="Slide 2"></button>
                            <button type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="2" aria-label="Slide 3"></button>
                            <button type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="3" aria-label="Slide 4"></button>
                        </div>

                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <asp:Image ID="imgProduct1" runat="server" CssClass="d-block w-100" Width="400" Height="400"/>
                            </div>
                            <div class="carousel-item">.
                                <asp:Image ID="imgProduct2" runat="server" CssClass="d-block w-100" Width="400" Height="400"/>
                            </div>
                            <div class="carousel-item">
                                <asp:Image ID="imgProduct3" runat="server" CssClass="d-block w-100" Width="400" Height="400"/>
                            </div>
                            <div class="carousel-item">
                                <asp:Image ID="imgProduct4" runat="server" CssClass="d-block w-100" Width="400" Height="400"/>
                            </div>
                        </div>


                        <button class="carousel-control-prev" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </button>
                        <button class="carousel-control-next" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </button>
                    </div>
                </div>

                <div id="btnProductDetailsIndicatorContainer" class="row my-3">
                    <div class="col-md-3">
                        <button class="btnProductDetailsIndicator btnProductDetailsActive" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="0">
                            <asp:Image ID="imgProductIndicator1" runat="server" Height="100" Width="140"/>
                        </button>
                    </div>
                    <div class="col-md-3">
                        <button class="btnProductDetailsIndicator" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="1">
                            <asp:Image ID="imgProductIndicator2" runat="server" Height="100" Width="140"/>
                        </button>
                    </div>
                    <div class="col-md-3">
                        <button class="btnProductDetailsIndicator" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="2">
                            <asp:Image ID="imgProductIndicator3" runat="server" Height="100" Width="140"/>
                        </button>
                    </div>
                    <div class="col-md-3">
                        <button class="btnProductDetailsIndicator" type="button" data-bs-target="#carouselIndicatorProductDetails" data-bs-slide-to="3">
                            <asp:Image ID="imgProductIndicator4" runat="server" Height="100" Width="140"/>
                        </button>
                    </div>
                </div>
            </div>
            <div class="col-md-6 ml-3">
                <div class="row">
                    <asp:Label ID="lblProdName" runat="server" Text="" CssClass="prodNameStyle"></asp:Label>
                </div>
                <div class="row">
                    <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                </div>
                <div class="row mt-3">
                    <asp:Label ID="lblAvailability" runat="server" Text="" CssClass="lblAvailabilityStyle"></asp:Label>
                </div>
                <div class="row mt-3">
                    <asp:Label ID="lblColor" runat="server" Text="" CssClass="lblColorStyle"></asp:Label>
                </div>
                
                <div class="row mt-3">
                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                        <ItemTemplate>
                            <div class="col-md-2">
                                <asp:Button ID="btnColor" runat="server" Text='<%# Eval("specification_value") %>' CssClass="btnMemoryStyle4" OnClick="btnColor_Click" CommandArgument='<%# Eval("specification_value") %>'/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT p.product_id, p.product_name, p.product_description, p.product_unit_price, p.product_availability, p.product_image1, p.product_image2, p.product_image3, p.product_image4, st.specification_type_name, ps.specification_value FROM Product AS p INNER JOIN Product_Specification AS ps ON p.product_id = ps.product_id INNER JOIN Specification_Type AS st ON ps.specification_type_id = st.specification_type_id WHERE (p.product_id = @ProductID) AND specification_type_name = 'Color'">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ProductID" QueryStringField="product_Id" Type="Int32"/>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>

                <div class="row mt-3">
                    <asp:Label ID="lblMemory" runat="server" Text="" CssClass="lblMemoryStyle"></asp:Label>
                </div>
                <div class="row mt-3">
                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
                        <ItemTemplate>
                            <div class="col-md-4" style="max-width: 180px;">
                                <asp:Button ID="btnMemory1" runat="server" Text='<%# Eval("specification_value") + " GB RAM" %>' CssClass="btnMemoryStyle1" OnClick="btnMemory1_Click" CommandArgument='<%# Eval("specification_value")%>'/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT p.product_id, p.product_name, p.product_description, p.product_unit_price, p.product_availability, p.product_image1, p.product_image2, p.product_image3, p.product_image4, st.specification_type_name, ps.specification_value FROM Product AS p INNER JOIN Product_Specification AS ps ON p.product_id = ps.product_id INNER JOIN Specification_Type AS st ON ps.specification_type_id = st.specification_type_id WHERE (p.product_id = @ProductID) AND specification_type_name = 'RAM'">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ProductID" QueryStringField="product_Id" Type="Int32"/>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
                <div class="row mt-3">
                    <asp:Repeater ID="Repeater3" runat="server" DataSourceID="SqlDataSource3">
                        <ItemTemplate>
                            <div class="col-md-4" style="max-width: 180px;">
                                <asp:Button ID="btnMemory2" runat="server" Text='<%# Eval("specification_value") + " GB SSD" %>' CssClass="btnMemoryStyle1" OnClick="btnMemory2_Click" CommandArgument='<%# Eval("specification_value")%>'/>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT p.product_id, p.product_name, p.product_description, p.product_unit_price, p.product_availability, p.product_image1, p.product_image2, p.product_image3, p.product_image4, st.specification_type_name, ps.specification_value FROM Product AS p INNER JOIN Product_Specification AS ps ON p.product_id = ps.product_id INNER JOIN Specification_Type AS st ON ps.specification_type_id = st.specification_type_id WHERE (p.product_id = @ProductID) AND specification_type_name = 'ROM'">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="ProductID" QueryStringField="product_Id" Type="Int32"/>
                        </SelectParameters>
                    </asp:SqlDataSource>

                </div>
                <div class="row">
                    <div class="col-md-2 mt-4">
                        <div style="border: 1px solid; border-radius: 15px; width: 100px; height: 40px;">
                            <div style="position: relative;">
                                <asp:Button ID="btnProductMinus" runat="server" CssClass="auto-style2" OnClick="btnProductMinus_Click"/>
                                <asp:Label ID="lblQtyProduct" runat="server" Text="1" CssClass="lblQtyProduct"></asp:Label>
                                <asp:Button ID="btnProductPlus" runat="server" CssClass="auto-style1" OnClick="btnProductPlus_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 mt-4">
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add To Cart" Height="40" Width="300" CssClass="btnAddToCartStyle" OnClick="btnAddToCart_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div style="height: 80px;">
    </div>
    <div>
        <h4 style="margin: 0; padding: 0; text-align: center; font-weight: bold;">Specification</h4>
    </div>

    <div class="container containerStyle mt-4 my-4 product-description" style="min-height: 500px;">
        <ul>
            
            <asp:Label ID="lblProdName2" runat="server" Text="" CssClass="prodNameStyle"></asp:Label>

            <asp:Repeater ID="Repeater5" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <li>
                        <asp:Label ID="lblColor" runat="server" Text='<%# Eval("specification_value") %>'></asp:Label>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
            
            <asp:Repeater ID="Repeater4" runat="server" DataSourceID="SqlDataSource2">
                <ItemTemplate>
                    <li>
                        <asp:Label ID="lblRAM" runat="server" Text='<%# Eval("specification_value") + "GB RAM"%>'></asp:Label>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            <asp:Repeater ID="Repeater6" runat="server" DataSourceID="SqlDataSource3">
                <ItemTemplate>
                    <li>
                        <asp:Label ID="lblROM" runat="server" Text='<%# Eval("specification_value") + "GB ROM"%>'></asp:Label>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            
    </div>

    <script src="asset/js/script.js"></script>
</div>
</asp:Content>
