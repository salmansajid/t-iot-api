<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="TIOT_WEB.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row pageH pageHDash">
    </div>

    <div class="jumbotron" style="height: calc(100vh - 155px)">
        <div style="margin-top:12%">
            <div class="text-center"><i class="fa fa-5x fa-frown-o text-primary"></i></div>
            <h2 class="text-center">404 Not Found
            </h2>
            <p class="text-center">Try pressing the back button or clicking on button below.</p>
            <p class="text-center"><a class="btn btn-primary" href="Dashboard.aspx"><i class="fa fa-home"></i>Take Me Home</a></p>
        </div>
    </div>
</asp:Content>
