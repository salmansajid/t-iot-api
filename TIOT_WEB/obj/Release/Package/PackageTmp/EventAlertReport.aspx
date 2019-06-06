<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventAlertReport.aspx.cs" Inherits="TIOT_WEB.EventAlertReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
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
            <div class="col-lg-2 col-md-6" id="ddlclientdiv" runat="server">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Client</asp:Label>
                        <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true"
                            class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-2 col-md-6" id="ddlgroupdiv" runat="server">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Branch</asp:Label>
                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-2 col-md-6">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Device</asp:Label>
                        <asp:DropDownList ID="ddlobject" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlobject_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-2 col-md-6">
                    <div class="form-group">
                         <asp:Label runat="server" CssClass="control-label lbl">Device Sensor</asp:Label>
                        <asp:DropDownList ID="ddlobjectSensor" ClientIDMode="Static" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                 <div class="col-lg-1 col-md-6">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="control-label lbl">Year</asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" class="dropdown-toggle form-control ddl ddlSelect ddlYearClass">
                            <asp:ListItem Text="2018" Value="0"></asp:ListItem>
                            <asp:ListItem Text="2017" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            <div class="col-lg-3 col-md-6">
                 <asp:Label runat="server" CssClass="control-label lbl">Date Range</asp:Label>
                    <div id="reportrange" class="dropdown-toggle form-control ddl dateRangeDiv">
                        <i class="fa fa-calendar"></i>
                        <asp:TextBox ID="txtdtrange" ClientIDMode="Static" runat="server"  CssClass="dateRangetextbox">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-6 pull-right">
                    <div class="form-group">
                        <asp:Button ID="btnGetReport" runat="server" CssClass="btn bg-primary btn-block btn-xs BTNtop" OnClick="btnGetReport_Click" Text="Get Report" />
                    </div>
                </div>
                   <div class="col-lg-1 col-md-6 pull-right">
                    <div class="form-group" id="btngraph" runat="server">
                        <button type="button" class="btn bg-primary btn-block btn-xs BTNtop" data-toggle="modal" data-target="#myModal">Graph</button>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:GridView ID="gvdReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Event" DataField="Value" />
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
        <div id="myModal" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog dashboardpopup">
            <div class="modal-content">
                <div class="modal-body modalbodyheight">
                    <div id='chart' class='chart'></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        $(function () {
            staticMethod();
        });
        function staticMethod() {
            reportdatetimepicker('#txtdtrange', $('.ddlYearClass :selected').text());
            $('.ddlSelect').select2();
            ddlyearSelection();
        }

        function ddlyearSelection() {
            $(".ddlYearClass").change(function () {
                reportdatetimepicker('#txtdtrange', $('.ddlYearClass :selected').text());
            });
        }

        $("#myModal").on("shown.bs.modal", function () {
            temptable = table['data'];
            data = [];
            $(temptable).each(function (i, item) {
                data.push([moment(item[2]).valueOf(), parseFloat(item[1])]);
            });
            if (typeof temptable !== 'undefined' && temptable.length > 0) {
                getDateTimeGraph('chart', data, 'spline', 'Analog Chart', temptable[0][0]);
            }
        });
    </script>
</asp:Content>

