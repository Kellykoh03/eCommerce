<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPortal.Master" AutoEventWireup="true" CodeBehind="AnalyticDashboard.aspx.cs" Inherits="adminPortal.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-lg-12 page-header">
            <h2 class="page-title">Analytic Dashboard</h2>
        </div>
    </div>
    <%--    <div class="row dashboard">
        <div class="col-lg-2 mt-3">
        </div>
        <div class="col-lg-4 mt-3">
            <div class="card">
                <div class="content">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="text-center">
                                <img class="big-icon" src="asset/Image/Admin/order.svg" />
                            </div>
                        </div>
                        <div class="col-sm-8">
                            <div class="detail">
                                <p class="detail-subtitle">
                                    New Orders
                                </p>
                                <asp:Label ID="Label1" CssClass="number" runat="server"></asp:Label>

                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats">
                            <img src="asset/Image/Admin/calender.svg">For this Month
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4 mt-3">
            <div class="card">
                <div class="content">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="text-center">
                                <img class="big-icon" src="asset/Image/Admin/money-dollar.svg" />
                            </div>
                        </div>
                        <div class="col-sm-8">
                            <div class="detail">
                                <p class="detail-subtitle">Revenue</p>
                                <asp:Label ID="Label2" CssClass="number" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="footer">
                        <hr />
                        <div class="stats">
                            <img src="asset/Image/Admin/calender.svg">For this Month
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-2 mt-3">
        </div>
    </div>--%>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="content">
                    <div class="head">
                        <h5 class="mb-0"><strong>Number of Order for Current Year</strong></h5>

                    </div>
                    <hr />
                    <div class="chart-section">
                        <asp:Chart ID="Chart1" runat="server" Width="600px" Height="400px">
                            <Series>
                                <asp:Series Name="Orders" ChartType="Column"></asp:Series>
                            </Series>
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                    </div>

                </div>
            </div>
        </div>
    </div>


</asp:Content>
