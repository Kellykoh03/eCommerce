<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="adminPortal.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
        <div class="row top-banner">
        <h4>Cart <span style="color: #F02757">.</span></h4>
    </div>



    <div style="background-color: white; border-radius: 20px; margin-top: 80px;" class="container mb-3">
        <div class="row pt-4 ml-3">
            <div class="col-md-3">
                PRODUCT
            </div>
            <div class="col-md-3">
                PRICE
            </div>
            <div class="col-md-3">
                QUANTITY
            </div>
            <div class="col-md-3">
                TOTAL
            </div>
        </div>
        <div style="border: 1px grey solid;">
        </div>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class="row pt-4 ml-3">
                    <div class="col-md-1">
                        <img src="<%# Eval("cart_item_image").ToString().Replace("~","") %>" alt="<%# Eval("cart_item_name") %>" style="width: 100px; height: 60px;" />
                    </div>

                    <div class="col-md-2">
                        <%# Eval("cart_item_name") %>
                    </div>

                    <div class="col-md-3 my-auto">
                        <p><%# "RM " + Eval("item_price") %></p>
                    </div>
                    <div class="col-md-3 my-auto">
                        <div style="border: 1px solid; border-radius: 15px; width: 80px; height: 30px; position: relative; text-align: center;">
                            <asp:Button ID="btnItemTrash1" runat="server" CssClass="btnTrash" OnClick="btnItemTrash1_Click" CommandArgument='<%# Eval("cart_item_id") %>' Visible='<%# Convert.ToInt32(Eval("item_quantity")) == 1 %>' />
                            <asp:Button ID="btnItemMinus2" runat="server" CssClass="btnMinus" OnClick="btnItemMinus2_Click" CommandArgument='<%# Eval("cart_item_id") %>' Visible='<%# Convert.ToInt32(Eval("item_quantity")) > 1 %>' />
                            <asp:Label ID="lblCartItem1" runat="server" Text='<%# Eval("item_quantity") %>'></asp:Label>
                            <asp:Button ID="btnItemPlus1" runat="server" CssClass="btnPlus" OnClick="btnItemPlus1_Click" CommandArgument='<%# Eval("cart_item_id") %>'/>
                        </div>
                    </div>
                    <div class="col-md-2 my-auto">
                        <p>
                            <asp:Label ID="lblItemTotalPrice" runat="server" Text='<%# "RM " + Convert.ToInt32(Eval("item_quantity")) * Convert.ToInt32(Eval("item_price")) %>'></asp:Label>
                        </p>
                    </div>
                    <div class="col-md-1 my-auto">
                        <asp:Button ID="btnItemRemove1" runat="server" CssClass="btnRemove" OnClick="btnItemRemove1_Click" CommandArgument='<%# Eval("cart_item_id") %>'/>
                    </div>
                </div>
                <div style="border: 1px grey solid;" class="mt-4">
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Cart] WHERE customer_id = @customer_id">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="customer_id" SessionField="CustomerID" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
    <div class="container mt-5 checkOutContainer">
        <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
            <ItemTemplate>
                <div class="row">
                    <asp:Label ID="lblSubtotal" runat="server" Text='<%# "Subtotal: RM " + Eval("Subtotal") %>' CssClass="col-md-12 subtotal"></asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT SUM(item_quantity * item_price) AS Subtotal
FROM Cart
WHERE customer_id = @customer_id">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="-1" Name="customer_id" SessionField="CustomerID" />
            </SelectParameters>
        </asp:SqlDataSource>
        

        <div class="row">
            <div class="col-md-12" style="text-align: end; color: grey; font-size: 14px; margin-bottom: 20px;">
                Taxes calculated at checkout
            </div>
        </div>
        <div class="row">
            <div class="col-md-12" align="right">
                <asp:Button ID="btnCheckOut" runat="server" Text="Check Out" CssClass="checkOutBtn" PostBackUrl="~/Checkout.aspx" OnClick="btnCheckOut_Click" />
            </div>
        </div>
    </div>

</asp:Content>
