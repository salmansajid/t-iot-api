<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GSMJamingReport.aspx.cs" Inherits="TIOT_WEB.GSMJamingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(function () {
            datetimepicker('#txtdtrange');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h5 class="pageH"><i class="fa fa-bar-chart fa-fw"></i><a href="ReportConfiguration.aspx">Reports </a></h5>
        </div>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
            <div class="col-lg-2 col-md-6"  id="ddlclientdiv" runat="server">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Client</asp:Label>
                        <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true"
                            class="dropdown-toggle form-control ddl">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-2 col-md-6" id="ddlgroupdiv" runat="server">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Branch</asp:Label>
                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-2 col-md-6">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Device</asp:Label>
                        <asp:DropDownList ID="ddlobject" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlobject_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-1 col-md-6">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Device Sensor</asp:Label>
                        <asp:DropDownList ID="ddlobjectSensor" ClientIDMode="Static" runat="server" class="dropdown-toggle form-control ddl">
                        </asp:DropDownList>
                    </div>
                </div>

            <div class="col-lg-3 col-md-6">
                     <asp:Label runat="server" CssClass="control-label lbl">Date Range</asp:Label>
                    <div id="reportrange" class="dropdown-toggle form-control ddl dateRangeDiv">
                        <i class="fa fa-calendar"></i>
                        <asp:TextBox ID="txtdtrange" ClientIDMode="Static" runat="server" CssClass="dateRangetextbox">
                        </asp:TextBox>
                    </div>
                </div>
              <div class="col-lg-1 col-md-6">
                    <div class="form-group">
                        <asp:Button ID="btnGetReport" runat="server" CssClass="btn bg-primary btn-block btn-xs BTNtop" OnClick="btnGetReport_Click" Text="Get Report" />
                    </div>
                </div>
            </div>

              
            <div class="row">
                <asp:GridView ID="gvdReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Value" DataField="Value" />
                        <asp:BoundField HeaderText="Date Time" DataField="DateTimeStamp" />
                    </Columns>
                </asp:GridView>
            </div>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Update1">
                <ProgressTemplate>
                    <div class="loading">
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlclient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlgroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlobject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnGetReport" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


