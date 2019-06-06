<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="TIOT_WEB.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="row">
        <div class="col-lg-12">
            <h5 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx"> Configuration </a> </h5>
        </div>
    </div>
               <div class="row">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>

            <asp:Repeater ID="rptConfiguration" runat="server" OnItemCommand="rptConfiguration_ItemCommand">
                <ItemTemplate>
         
                        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                            <div class="pricing-table">
                                <div class="pricing-option">
                                    <i class="fa fa-cog fa-fw  fa-rotate-45"></i>
                                    <p><strong><i class='<%# Eval("Class") %>' style="font-size:1em !important"></i><%# Eval("Name") %></strong></p>
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
