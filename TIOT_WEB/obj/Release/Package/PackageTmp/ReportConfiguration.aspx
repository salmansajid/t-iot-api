<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportConfiguration.aspx.cs" Inherits="TIOT_WEB.ReportConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h5 class="pageH"><i class="fa fa-bar-chart fa-fw"></i><a href="ReportConfiguration.aspx">Reports </a></h5>
        </div>
    </div>
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
            <ContentTemplate>
                <asp:Repeater ID="rptReports" runat="server" OnItemCommand="rptReports_ItemCommand">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-3 col-sm-4 col-xs-12" id="euipment" runat="server">
                            <div class="pricing-table">
                                <div class="pricing-option">
                                    <i class='<%# Eval("Class") %>' style="font-size: 1em !important"></i>
                                    <p><strong><%# Eval("Name") %></strong></p>
                                    <div class="price">
                                        <div class="front">
                                            <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Details</span>
                                        </div>
                                        <div class="back">
                                            <asp:LinkButton ID="lnkbtnUser" runat="server" CssClass="button" CommandName="lnkbtnviewAddress" CommandArgument='<%# Eval("Link") %>'>View Details</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>


            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
