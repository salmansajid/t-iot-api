<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TIOT_WEB.Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        $(function () {
            var start = moment().subtract(0, 'days');
            var end = moment();
            function cb(start, end) {
                $('#txtdtrange').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
            }
            $('#txtdtrange').daterangepicker({
                startDate: start,
                endDate: end,
                ranges: {
                    'Today': [moment(), moment()],
                    'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                    'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                    'This Month': [moment().startOf('month'), moment().endOf('month')],
                    'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);
            cb(start, end);
        });
        function gvdtempete(gridname) {
            ApplyDatatable(gridname, 13);
        }


    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h5 class="pageH"><i class="fa fa-bar-chart fa-fw"></i>Reports </h5>
        </div>
    </div>
    <div class="row" id="dvddls" runat="server">

        <div class="col-lg-3 col-md-6" id="dvcl" runat="server">
            <div class="form-group">
                <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true"
                    class="dropdown-toggle form-control ddl">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3 col-md-6" id="dvgp" runat="server">
            <div class="form-group">
                <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3 col-md-6">
            <div class="form-group">
                <asp:DropDownList ID="ddlObject" ClientIDMode="Static" runat="server" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" AutoPostBack="true"
                    class="dropdown-toggle form-control ddl">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-lg-3 col-md-6" id="divDate" runat="server" visible="false">
            <div id="reportrange" class="dropdown-toggle form-control ddl dateRangeDiv">
                <i class="fa fa-calendar"></i>
                <asp:TextBox ID="txtdtrange" ClientIDMode="Static" runat="server" Style="border: 0; padding: 0; cursor: pointer;">
                </asp:TextBox>
            </div>
        </div>

    </div>
    <br />
    <div id="div2" runat="server" visible="false">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="euipment" runat="server">
                    <div class="pricing-table">
                        <div class="pricing-option">
                            <i class="fa fa-bar-chart "></i>
                            <h5><strong>Equipment Consumption Report</strong></h5>
                            <div class="price">
                                <div class="front">
                                    <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Report</span>
                                </div>
                                <div class="back">
                                    <asp:LinkButton ID="lnkbtnEQConsumpRpt" CssClass="button" runat="server" OnClick="lnkbtnEQConsumpRpt_Click"> View Report </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="controling" runat="server">
                    <div class="pricing-table">
                        <div class="pricing-option">
                            <i class="fa fa-bar-chart "></i>
                            <h5><strong>Equipment Controlling Report</strong></h5>
                            <div class="price">
                                <div class="front">
                                    <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Report</span>
                                </div>
                                <div class="back">
                                    <asp:LinkButton ID="lnkbtnEQControlingRpt" runat="server" CssClass="button" OnClick="lnkbtnEQControlingRpt_Click">  View Report </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12" id="Div1" runat="server">
                    <div class="pricing-table">
                        <div class="pricing-option">
                            <i class="fa fa-bar-chart "></i>
                            <h5><strong>Temperature Sensor Report</strong></h5>
                            <div class="price">
                                <div class="front">
                                    <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Report</span>
                                </div>
                                <div class="back">
                                    <asp:LinkButton ID="lnkbtnTemperature" runat="server" CssClass="button" OnClick="lnkbtnTemperature_Click">  View Report </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="myModal" class="modal  fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog dashboardpopup">

                    <!-- Modal content-->
                    <div class="modal-content" style="border: 3px solid rgb(51, 122, 183);">
                        <div class="modal-body" style="padding: 0; padding-left: 15px; padding-right: 15px; height: calc(100vh - 120px); overflow-y: auto">
                            <div class="modal-header" style="padding: 0; border-bottom: 1px solid #337ab7;">

                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 style="color: #337ab7; font-size: 10px; font-weight: bold;">Consumption Report </h6>
                                    </div>
                                    <div class="col-sm-6" style="padding: 1%;">
                                        <h6 class="modal-title" style="text-align: center; color: #337ab7; font-size: 16px; font-weight: bold">
                                            <asp:Label ID="lblname" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                    <div class="col-sm-3">
                                        <div style="float: right;">
                                            <h6 style="float: right; font-size: 10px; font-weight: bold;">Date : <%= DateTime.Now.ToString("dd-MM-yyyy") %></h6>
                                            <%--<br />
                                            <a href="#" data-toggle="modal" onclick="ConsumptionTog();"><i class="fa fa-bar-chart pull-right" aria-hidden="true" style="color: #337ab7;"></i></a>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row" style="padding-top: 5px; padding-left: 15px; padding-right: 15px;">


                                <asp:GridView ID="Gvdconsumptionreport" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                                    <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Current" HeaderText="Current(A)" DataFormatString="{0:n}" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Voltage" HeaderText="Voltage(V)" DataFormatString="{0:n}" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Power" HeaderText="Power(KW)" DataFormatString="{0:n}" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Unit" HeaderText="Unit" DataFormatString="{0:n}" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="StartTime" HeaderText="Start Time(H:M:S)" DataFormatString="{0:f}" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="EndTime" HeaderText="End Time(H:M:S)" HeaderStyle-Width="300px" DataFormatString="{0:f}" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TotalTime" HeaderText="Total Time(H:M:S)" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <div class="modal-footer" style="border-top: 1px solid #337ab7;">
                            <div>
                                <asp:Button ID="btnRptEqpConsp" runat="server" OnClick="btnRptEqpConsp_Click" Text="PDF" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnRptEqpConsp" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="myModal2" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog dashboardpopup">
                    <!-- Modal content-->
                    <div class="modal-content" style="border: 3px solid rgb(51, 122, 183);">

                        <div class="modal-body" style="padding: 0; padding-left: 15px; padding-right: 15px;">
                            <div class="modal-header" style="padding: 0; border-bottom: 1px solid #337ab7;">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 style="color: #337ab7; font-size: 10px; font-weight: bold;">Controlling Report </h6>
                                    </div>
                                    <div class="col-sm-6" style="padding: 1%">
                                        <h6 class="modal-title" style="text-align: center; color: #337ab7; font-size: 16px; font-weight: bold">
                                            <asp:Label ID="lblname2" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                    <div class="col-sm-3">
                                        <div style="float: right;">
                                            <h6 style="float: right; font-size: 10px; font-weight: bold;">Date : <%= DateTime.Now.ToString("dd-MM-yyyy") %></h6>
                                            <br />
                                            <a href="#" data-toggle="modal" onclick="loadGraphtog();"><i class="fa fa-bar-chart pull-right" aria-hidden="true" style="color: #337ab7;"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="div_Grid" style="padding-top: 5px; padding-left: 15px; padding-right: 15px;">
                                <div id="chart" style="width: 1121px; padding-left: 21px;"></div>
                                <asp:GridView ID="gv_ControllingReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                                    <Columns>
                                         <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="StartTime" HeaderText="Start Time" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="EndTime" HeaderText="End Time" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="TotalTime" HeaderText="Total Duration(hh:mm:ss)" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer" style="border-top: 1px solid #337ab7;">
                            <div>
                                <asp:Button ID="btnControlingRpt" runat="server" OnClick="btnControlingRpt_Click" Text="PDF" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnControlingRpt" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="myModal3" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog dashboardpopup">
                    <!-- Modal content-->
                    <div class="modal-content" style="border: 3px solid rgb(51, 122, 183);">

                        <div class="modal-body" style="padding: 0; padding-left: 15px; padding-right: 15px">
                            <div class="modal-header" style="padding: 0; border-bottom: 1px solid #337ab7;">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 style="color: #337ab7; font-size: 10px; font-weight: bold;">Temperature Sensor Report </h6>
                                    </div>
                                    <div class="col-sm-6" style="padding: 1%">
                                        <h6 class="modal-title" style="text-align: center; color: #337ab7; font-size: 16px; font-weight: bold">
                                            <asp:Label ID="lblname3" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                    <div class="col-sm-3">
                                        <div style="float: right;">
                                            <h6 style="float: right; font-size: 10px; font-weight: bold;">Date : <%= DateTime.Now.ToString("dd-MM-yyyy") %></h6>
                                            <%--<br />
                                            <a href="#" data-toggle="modal" onclick="loadGraphtog();"><i class="fa fa-bar-chart pull-right" aria-hidden="true" style="color: #337ab7;"></i></a>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="gvdTemp" style="padding-top: 5px; padding-left: 15px; padding-right: 15px;">
                                <%--<div id="chart" style="width: 1121px; padding-left: 21px;"></div>--%>
                                <asp:GridView ID="gvdTemperature" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                                    <Columns>

                                        <asp:BoundField DataField="Device" HeaderText="Device Name" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Sensor" HeaderText="Sensor" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Variatoin_Time" HeaderText="Variation Time" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer" style="border-top: 1px solid #337ab7;">
                            <div>
                                <asp:Button ID="btnTemprature" runat="server" OnClick="btnTemprature_Click" Text="PDF" CssClass="btn btn-primary" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnTemprature" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="myCurrentConsumptionModal" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog dashboardpopup">

                    <!-- Modal content-->
                    <div class="modal-content" style="border: 3px solid rgb(51, 122, 183);">
                        <div class="modal-body" style="padding: 0; padding-left: 15px; padding-right: 15px;">
                            <div class="modal-header" style="padding: 0; border-bottom: 1px solid #337ab7;">
                                <div class="row">
                                    <div class="col-sm-3">
                                        <h6 style="color: #337ab7; font-size: 10px; font-weight: bold;">Consumption Report </h6>
                                    </div>
                                    <div class="col-sm-6" style="padding: 1%;">
                                        <h6 class="modal-title" style="text-align: center; color: #337ab7; font-size: 16px; font-weight: bold">
                                            <asp:Label ID="lblCConsmName" runat="server"></asp:Label>
                                        </h6>
                                    </div>
                                    <div class="col-sm-3">
                                        <div style="float: right;">
                                            <h6 style="float: right; font-size: 10px; font-weight: bold;">Date : <%= DateTime.Now.ToString("dd-MM-yyyy") %></h6>
                                            <br />
                                            <a href="#" data-toggle="modal" onclick="ConsumptionTog();"><i class="fa fa-bar-chart pull-right" aria-hidden="true" style="color: #337ab7;"></i></a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="padding-top: 5px; padding-left: 15px; padding-right: 15px;">

                                <div class="col-sm-12">
                                    <div id="chartConsumptionVoltage" class="col-sm-4" style="width: 33%; padding-left: 21px;"></div>
                                    <div id="chartConsumptionCurrent" class="col-sm-4" style="width: 33%; padding-left: 21px;"></div>
                                    <div id="chartConsumptionPower" class="col-sm-4" style="width: 33%; padding-left: 21px;"></div>
                                </div>

                                <asp:GridView ID="gvdCurrentConsump" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                                    <Columns>
                                        <%--<asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField DataField="Name" HeaderText=" Name" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Voltage(Volt)" HeaderStyle-Width="250px">
                                            <ItemTemplate>
                                                <%# Eval("Voltage","{0:0.00}")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current(Amp)" HeaderStyle-Width="250px">
                                            <ItemTemplate>
                                                <%# Eval("Current","{0:0.00}")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Power(KW)" HeaderStyle-Width="250px">
                                            <ItemTemplate>
                                                <%# Eval("Power","{0:0.00}")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="modal-footer" style="border-top: 1px solid #337ab7;">
                            <div>
                                <%--<asp:Button ID="Button1" runat="server" OnClick="btnRptEqpConsp_Click" Text="PDF" CssClass="btn btn-primary" />--%>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnRptEqpConsp" />
        </Triggers>--%>
    </asp:UpdatePanel>

    <div class="modal fade charts-modal" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel">
        <div class="modal-dialog modal-lg dashboardpopup" style="margin: 5.5%; padding-left: 5px; padding-right: 25px;">
            <div class="modal-content" style="padding: 2%">
                <div class="js-loading text-center">
                    <h3>Loading...</h3>
                </div>
                <div id="chartConsumption">
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">

        google.charts.load("current", { packages: ["timeline"] });
        google.charts.setOnLoadCallback();

        function loadGraph() {
            $('#chart').css({ 'height': '350px', 'margin-bottom': '25px' });

            var doc = new jsPDF('p', 'pt');
            var tbl1_res = doc.autoTableHtmlToJson(document.getElementById('gv_ControllingReport'));
            setTimeout(function () {
                var container = document.getElementById('chart');
                var chart = new google.visualization.Timeline(container);
                var dataTable = new google.visualization.DataTable();
                dataTable.addColumn({ type: 'string', id: 'Route' });

                dataTable.addColumn({ type: 'date', id: 'Start' });
                dataTable.addColumn({ type: 'date', id: 'End' });



                var date1;
                var date2;
                var d = new Date()
                var am_pm = d.getHours() >= 12 ? "PM" : "AM";
                var time = d.getHours() + "," + d.getMinutes() + "," + d.getSeconds();//+ "," + am_pm//.getSeconds();//d.format("hh,mm,ss,tt");


                var dateoff = d.format("MM,dd,yyyy");
                var time1;
                var time2;
                var i;
                var j;

                $.each(tbl1_res.rows, function (key, values) {

                    if (values[1].length > 0) {
                        date1 = values[1].split(' ');

                        var timee = date1[1].split(':');
                        var AMPM = date1[2];
                        if (AMPM == "PM" && timee[0] < 12) timee[0] = parseInt(timee[0]) + 12;
                        if (AMPM == "AM" && timee[0] == 12) timee[0] = parseInt(timee[0]) - 12;



                        time1 = date1[0].replace(/\//g, ",") + "," + timee[0] + "," + timee[1] + "," + timee[2];

                    } else {
                        time1 = dateoff + "," + time.replace(/:/g, ",");
                    }

                    if (values[2].length > 0) {
                        date2 = values[2].split(' ');
                        var timex = date2[1].split(':');
                        var AMPM = date2[2];
                        if (AMPM == "PM" && timex[0] < 12) timex[0] = parseInt(timex[0]) + 12;
                        if (AMPM == "AM" && timex[0] == 12) timex[0] = parseInt(timex[0]) - 12;

                        time2 = date2[0].replace(/\//g, ",") + "," + timex[0] + "," + timex[1] + "," + timex[2];
                    } else {
                        time2 = dateoff + "," + time.replace(/:/g, ",");

                    }

                    time1 = time1.split(',');
                    time2 = time2.split(',');

                    i = time1[0] - 1;
                    j = time2[0] - 1;
                    //   console.log(time2[2] + ":" + j + ":" + time2[1] + ":" + time2[3] + ":" + time2[4] + ":" + time2[5] + "--:--" + time1[2] + ":" + i + ":" + time1[0] + ":" + time1[3] + ":" + time1[4] + ":" + time1[5]);
                    dataTable.addRows([[values[0], new Date(time1[2], j, time1[1], time1[3], time1[4], time1[5]), new Date(time2[2], j, time2[1], time2[3], time2[4], time2[5])]]);//4,3,2017,3,10,41



                });

                var options = {
                    timeline: { singleColor: '#337ab7' },
                    'is3D': true,
                    'width': 1140,
                    'height': 450,
                    'chartArea': { 'width': '70%', 'height': '100%' },
                    hAxis: {
                        minValue: new Date(time1[2], j, time1[1], 00, 00, 00),
                        maxValue: new Date(time1[2], j, time1[1], 23, 59, 59)
                    }
                };

                chart.draw(dataTable, options);

            }, 700);


        }

        function loadGraphtog() {
            $('#chart').toggle();
        }

        function loadGraphConsumption() {

            var doc = new jsPDF('l', 'pt');
            var Data = doc.autoTableHtmlToJson(document.getElementById('gvdCurrentConsump'));
            console.log(Data);
            setTimeout(function () {

                var dataarray_Power = {
                }
                var dataarray_Current = {
                }
                var dataarray_Voltage = {
                }
                var Value_Current; var Value_Power; var Value_Voltage;
                var Value2 = [];
                var Current = []; var Voltage = []; var Power = [];
                var width = $('.charts-modal').width() - 30;

                var chart = c3.generate({
                    bindto: '#chartConsumptionPower',
                    size: {
                        height: 340,
                        width: 350

                    },
                    data: {

                        json: dataarray_Power,
                        labels: true,
                        type: 'area-spline'
                    },
                    color: {
                        pattern: ['#ff7f0e']
                    },

                    tooltip: {
                        show: true
                    },

                    axis: {
                        x: {
                            type: 'category',
                            categories: Power,
                            tick: {
                                rotate: 75,
                                multiline: false
                            }
                        },
                        y: {

                        },

                    }

                });

                var chart_Current = c3.generate({
                    bindto: '#chartConsumptionCurrent',
                    size: {
                        height: 340,
                        width: 350

                    },
                    data: {

                        json: dataarray_Current,
                        labels: true,
                        type: 'area-spline'
                    },

                    color: {
                        pattern: ['#2ca02c']
                    },
                    tooltip: {
                        show: true
                    },

                    axis: {
                        x: {
                            type: 'category',
                            categories: Current,
                            tick: {
                                rotate: 75,
                                multiline: false
                            }
                        },
                        y: {

                        },

                    }

                });
                var chart_Volatge = c3.generate({
                    bindto: '#chartConsumptionVoltage',
                    size: {
                        height: 340,
                        width: 350

                    },
                    data: {

                        json: dataarray_Voltage,
                        labels: true,
                        type: 'area-spline'
                    },


                    tooltip: {
                        show: true
                    },

                    axis: {
                        x: {
                            type: 'category',
                            categories: Voltage,
                            tick: {
                                rotate: 75,
                                multiline: false
                            }
                        },
                        y: {

                        },

                    }

                });



                $.each(Data.columns, function (i, item) {
                    Value2.push(item);

                });
                for (var J = 1; J < 4; J++) {
                    Value_Voltage = Value2[1];
                    Value_Current = Value2[2];
                    Value_Power = Value2[3];

                    dataarray_Voltage[Value_Voltage] = new Array();
                    dataarray_Current[Value_Current] = new Array();
                    dataarray_Power[Value_Power] = new Array();

                    $.each(Data.data, function (i, item) {
                        Current.push(item[0]);
                        Voltage.push(item[0]);
                        Power.push(item[0]);

                        dataarray_Power[Value_Power].push(item[3]);
                        dataarray_Current[Value_Current].push(item[2]);
                        dataarray_Voltage[Value_Voltage].push(item[1]);

                        chart_Volatge.load({
                            json: dataarray_Voltage,
                            categories: Voltage
                        });

                        chart_Current.load({
                            json: dataarray_Current,
                            categories: Current
                        });

                        chart.load({
                            json: dataarray_Power,
                            categories: Power
                        });

                    });
                }






                $('.js-loading').addClass('hidden');
            }, 200);
        };

        function ConsumptionTog() {
            $('#chartConsumptionPower').toggle();
            $('#chartConsumptionCurrent ').toggle();
            $('#chartConsumptionVoltage').toggle();
        }
    </script>
</asp:Content>
