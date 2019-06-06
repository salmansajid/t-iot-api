<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeviceRelaysReport.aspx.cs" Inherits="TIOT_WEB.DeviceRelaysReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
                <div class="col-lg-1 col-md-6 col-sm-12 customCol ddlWidth" id="ddlclientdiv" runat="server">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="control-label lbl">Client</asp:Label>
                        <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true"
                            class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-6 col-sm-12  customCol ddlWidth" id="ddlgroupdiv" runat="server">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="control-label lbl">Branch</asp:Label>
                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-6 col-sm-12  customCol ddlWidth">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="control-label lbl">Device</asp:Label>
                        <asp:DropDownList ID="ddlobject" ClientIDMode="Static" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-6 col-sm-12  customCol ddlWidth">
                    <div class="form-group">
                        <asp:Label runat="server" CssClass="control-label lbl">Year</asp:Label>
                        <asp:DropDownList ID="ddlYear" runat="server" class="dropdown-toggle form-control ddl ddlSelect ddlYearClass">
                            <asp:ListItem Text="2018" Value="0"></asp:ListItem>
                            <asp:ListItem Text="2017" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-3 col-md-6 col-sm-12 customCol">
                    <asp:Label runat="server" CssClass="control-label lbl">Date Range</asp:Label>
                    <div id="reportrange" class="dropdown-toggle form-control ddl dateRangeDiv">
                        <i class="fa fa-calendar"></i>
                        <asp:TextBox ID="txtdtrange" ClientIDMode="Static" runat="server" CssClass="dateRangetextbox">
                        </asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-6 col-sm-12 pull-right customCol">
                    <div class="form-group">
                        <asp:Button ID="btnGetReport" runat="server" CssClass="btn bg-primary btn-block btn-xs" OnClick="btnGetReport_Click" Text="Get Report" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-4">
                    <div id="chartPieCurrent"></div>
                    
                </div>
                <div class="col-sm-8">
                    <div id="chartCurrent"></div>
                </div>
            </div>
             <div class="row">
            <div class="col-sm-12">
                    <div id="chartCurrent2"></div>
                </div>
            </div>
            <div class="row">
            <div class="col-sm-12">
                    <div id="chartVoltage"></div>
                </div>
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
            <asp:AsyncPostBackTrigger ControlID="btnGetReport" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(function () {
            staticMethod();
        });
        var chartCdtt  ,chartVdtt;;
        var chartDTime;

        function chartCRR(result) {
            Rs_CV = [];
            Rs_CV = result.split('*');
            chartVoltage(Rs_CV[1]);
            var resultArray = [];
            resultArray = Rs_CV[0].split('&');
            colors = ['#ff0000', '#0080ff', '#ffbf00', '#40ff00', '#330000', '#996633', '#ff4000', '#669999']
            chartdtt = [];
            piechartdtt = [];
            for (var i = 0; i < resultArray.length; i++) {
                if (resultArray[i] != '[]') {
                    resultt = resultArray[i].toString();
                    var r = JSON.parse(resultt);
                    var chartDt;
                   chartDt = r.map(function (item) {
                       return chartDt = parseFloat(item['Value']);
                   });
                   chartDTime = r.map(function (item) {
                       return chartDTime = moment(item['DateTimeStamp']).format("DD/MMM hh:mm a");
                   });
                   var charteeeDt = r.map(function (item) {
                       return charteeeDt = [moment(item['DateTimeStamp']).valueOf(), parseFloat(item['Value'])];
                   });

                   var temp = {};
                    temp['id'] = r[0]['Name'];
                    temp['name'] = r[0]['Name'];
                    temp['color']= colors[i],
                    temp['data'] = charteeeDt;
                    var sum = chartDt.reduce(function (a, b) { return a + b; });
                    var avg = sum / chartDt.length;
                    var pietemp = {};
                    pietemp['name'] = r[0]['Name'];
                    pietemp['y'] = avg;
                    piechartdtt.push(pietemp);
                    chartdtt.push(temp);
                    drawChartCurrent2(chartdtt);
                }
            }
            chartCdtt = chartdtt;
            piechartdtt[0].sliced = true;
            piechartdtt[0].selected = true;
            drawChartPieCurrent(piechartdtt);
        }

        function chartVoltage(result) {
            var resultArray = [];
            resultArray = result.split('&');
            colors = ['#0080ff', '#ff0000', '#ff4000', '#330000', '#40ff00', '#996633', '#669999', '#ffbf00']
            chartdtt = [];
            for (var i = 0; i < resultArray.length; i++) {
                if (resultArray[i] != '[]') {
                    resultt = resultArray[i].toString();
                    var r = JSON.parse(resultt);
                    var chartDt, chartVDTime;
                    chartDt = r.map(function (item) {
                        return chartDt = parseFloat(item['Value']);
                    });
                    chartVDTime = r.map(function (item) {
                        return chartVDTime = moment(item['DateTimeStamp']).format("DD/MMM hh:mm a");
                    });
                    var charteeeDt = r.map(function (item) {
                        return charteeeDt = [moment(item['DateTimeStamp']).valueOf(), parseFloat(item['Value'])];
                    });

                    var temp = {};
                    temp['id'] = r[0]['Name'];
                    temp['name'] = r[0]['Name'];
                    temp['color'] = colors[i],
                    temp['data'] = charteeeDt;
                    chartdtt.push(temp);
                    drawChartVoltage(chartdtt);
                }
            }
            chartVdtt = chartdtt;
        }

        function drawChartCurrent(name) {
            var ctd = chartCdtt;
            ctd = ctd.filter(function (item) {
                return item.id == name;
            });
            var ctv = chartVdtt;
            ctv = ctv.filter(function (item) {
                return item.id == name;
            });
            ctd[0]['name'] = name + ' Current';
            ctv[0]['name'] = name + ' Voltage';
            var yy = [];
            yy.push(ctd[0]);
            yy.push(ctv[0]);
        

            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            });
            Highcharts.chart('chartCurrent', {
                chart: {
                    zoomType: 'x',
                },
                title: {
                    text: moment(yy[0][0]).format("DD/MMM/YYYY hh:mm a") + ' to ' + moment(yy.slice(-1)[0][0]).format("DD/MMM/YYYY hh:mm a")
                },

                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y:.2f}</b>'
                },
                credits: {
                    enabled: false
                },

                xAxis: {
                    type: 'datetime',
                },
                series: yy,

            });
        }

        function drawChartVoltage(data) {
            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            });
            Highcharts.chart('chartVoltage', {
                chart: {
                    zoomType: 'x',
                },
                title: {
                    text: 'Relays Voltage from  - ' + moment(data[0][0]).format("DD/MMM/YYYY hh:mm a") + ' to ' + moment(data.slice(-1)[0][0]).format("DD/MMM/YYYY hh:mm a")
                },

                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y:.2f} volt</b>'
                },
                credits: {
                    enabled: false
                },

                xAxis: {
                    type: 'datetime',
                },
                series: data,



            });
        }

        function drawChartCurrent2(data) {
            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            });
            Highcharts.chart('chartCurrent2', {
                chart: {
                    zoomType: 'x',
                },
                title: {
                    text: 'Relays Current - '+    moment(data[0][0]).format("DD/MMM/YYYY hh:mm a") + ' to ' + moment(data.slice(-1)[0][0]).format("DD/MMM/YYYY hh:mm a")
                },

                yAxis: {
                    title: {
                        text: ''
                    }
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.y:.2f} amp</b>'
                },
                credits: {
                    enabled: false
                },

                xAxis: {
                    type: 'datetime',
                   },
                series: data,



            });
        }
        function drawChartPieCurrent(cdata) {

            Highcharts.chart('chartPieCurrent', {
            chart: {
                plotBackgroundColor: null,
                plotBorderWidth: null,
                plotShadow: false,
                type: 'pie'
            },
            credits: {
                enabled: false
            },

            title: {
                text: 'Average Current'
            },
            tooltip: {
                pointFormat: '{series.name}: <b>{point.y:.2f} amp</b>'
            },
            plotOptions: {
                series: {
                    turboThreshold: 3000,
                    cursor: 'pointer',
                    point: {
                        events: {
                            click: (function (event) {
                                drawChartCurrent(event.point.name);
                            }).bind(this)
                        }
                    }
                }, pie: {
                    allowPointSelect: true,
                    dataLabels: {
                        enabled: true,
                        format: '<b>{point.name}</b>: {point.y:.2f} amp',
                        style: {
                            color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                        }

                    },
                    size: '35%',
                }             
            },


            series: [{
                name: 'Current',
                colorByPoint: true,
                data: cdata
            }]
            });
            drawChartCurrent(cdata[0]['name']);
        }

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


    </script>
</asp:Content>
