<%@ Page Title="" Language="C#" MasterPageFile="~/UserPortal.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="adminPortal.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
                <div class="row top-banner">
        <h4>Check Out <span style="color: #F02757">.</span></h4>
    </div>

    <div style="background: white; border-radius: 20px;" class="container my-5">
        <div class="row checkOut-container">
            <div class="col-md-6" style="width: 600px; margin: 40px;">
                <div class="row">
    <h5 style="margin-bottom: 20px; padding: 0px; font-weight: bold;">Shipping</h5>
</div>
              
                <div class="row">
                    Address
                </div>
                <div class="row">
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </div>
                <div class="row">
                    <div class="col" style="padding: 0; margin: 0;">
                        Postcode
                    </div>
                    <div class="col" style="padding: 0; margin: 0;">
                        City
                    </div>
                    <div class="col" style="padding: 0; margin: 0;">
                        Country
                    </div>
                </div>
                <div class="row">
                    <div class="col" style="padding: 0; margin: 0;">
                        <asp:TextBox ID="txtPostCode" runat="server"></asp:TextBox>
                    </div>
                    <div class="col" style="padding: 0; margin: 0;">
                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                    </div>
                    <div class="col" style="padding: 0; margin: 0;">
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                <div class="row">
                    <asp:Button ID="btnPay" runat="server" Text="Pay now" Height="50" CssClass="btnPayStyle" OnClick="btnPay_Click" />
                </div>
            </div>

            <div class="col-md-6" style="width: 600px; margin-top: 40px;">
                <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1" OnItemDataBound="Repeater1_ItemDataBound">
                    <ItemTemplate>
                        <div class="row">
                            <div class="col" style="margin-left: 0; padding-left: 0;">
                                <img src='<%# Eval("cart_item_image").ToString().Replace("~","") %>' alt="<%# Eval("cart_item_name") %>" height="100" width="150" />
                            </div>
                            <div class="col" style="margin-left: 0; padding-left: 0;">
                                <asp:Label ID="lblProdTitle" runat="server" Text='<%# Eval("cart_item_name") %>'></asp:Label>
                                <p><asp:Label ID="lblMemory" runat="server" Text="" Font-Size="10"></asp:Label></p>
                                <p><asp:Label ID="lblQty" runat="server" Text='<%# "Qty: " + Eval("item_quantity")%>' Font-Size="10"></asp:Label></p>
                            </div>
                            <div class="col" style="margin-left: 0; padding-left: 0; text-align: end;">
                                <asp:Label ID="lblPrice" runat="server" Text='<%# "RM " + Convert.ToInt32(Eval("item_quantity")) * Convert.ToInt32(Eval("item_price")) %>'></asp:Label>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Cart] WHERE customer_id = @customer_id">
                    <SelectParameters>
                        <asp:SessionParameter DefaultValue="-1" Name="customer_id" SessionField="CustomerID" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <div class="row">
                    <div class="col" style="margin-left: 0; padding-left: 0;">
                        Subtotal
                    </div>
                    <asp:Repeater ID="Repeater2" runat="server" DataSourceID="SqlDataSource2">
                        <ItemTemplate>
                            <div class="col" style="text-align: end; margin-left: 0; padding-left: 0;">
                                <asp:Label ID="lblSubtotal" runat="server" Text='<%# "RM " + Eval("Subtotal") %>'></asp:Label>
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
                </div>
                <div class="row">
                    <div class="col" style="margin-left: 0; padding-left: 0; font-weight: bold; font-size: 20px;">
                        Total
                    </div>
                    <div class="col" style="text-align: end; margin-left: 0; padding-left: 0; font-weight: bold; font-size: 20px;">
                        <asp:Label ID="lblTotal" runat="server" Text='<%# "RM " + Eval("Subtotal") %>'></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
