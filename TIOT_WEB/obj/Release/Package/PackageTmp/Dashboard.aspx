<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="TIOT_WEB.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div>
        <asp:Timer ID="Timer1" runat="server" Interval="100000" OnTick="Timer1_Tick" Enabled="False"></asp:Timer>
    </div>

    <div class="row pageH pageHDash pageHdashboard">
        <div class="col-lg-4 col-xs-7" style="margin-top: 1em;">
            <span><i class="fa fa-tachometer fa-fw"></i>Dashboard </span>
            <asp:Label ID="sessionId" ClientIDMode="Static" runat="server" Style="display: none"></asp:Label>
        </div>
        <ul class="nav  navbar-right " id="Notifications">
            <li class="dropdown" id="notif">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false" style="padding: 8px 12px 0px 0px;">
                    <span class="fa-stack has-badge">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-bell  faa-ring animated  fa-stack-1x fa-inverse"></i>

                    </span>
                    <%--<span class="badge badge-notify  faa-pulse animated-hover ">
                        <label id="notifCount" class="LableMargin">5</label>
                    </span>--%>
                </a>
                <ul class="dropdown-menu notify-drop" id="notifyopen">
                    <li class="notify-drop-title">
                        <div class="row">
                            <div class="text-center">NOTIFICATIONS</div>
                        </div>
                    </li>

                    <li class="drop-content">
                        <table id="tblnotify" class="table table-hover nt-tbl" style="margin-bottom: 0px;">
                        </table>
                    </li>
                    <li class="notify-drop-footer text-center">
                        <asp:LinkButton runat="server" ID="lnkbtnSeemoreNotif" ClientIDMode="Static">See More</asp:LinkButton>
                    </li>
                </ul>
            </li>
        </ul>

    </div>

    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="row" runat="server">
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12" id="ddlclientdiv" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 " id="ddlgroupdiv" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="card" id="mainP">
                <ul class="nav nav-tabs" role="tablist">
                    <li role="presentation" class="active"><a href="#iot" aria-controls="home" role="tab" data-toggle="tab">
                        <img src="Images/dashboardIcons/iot.png" class="imgicons" /><span>IOT</span></a></li>
                    <li role="presentation"><a href="#iotGroup" aria-controls="messages" role="tab" data-toggle="tab">
                        <img src="Images/categoryIcons/motion.png" class="imgicons" /><span>IOT Group Details</span></a></li>
                    <li role="presentation"><a href="#vehicletracking" aria-controls="profile" role="tab" data-toggle="tab">
                        <img src="Images/dashboardIcons/vehicletracking.png" class="imgicons" /><span>VEHICLE TRACKING</span></a></li>
                    <li role="presentation"><a href="#attendence" aria-controls="messages" role="tab" data-toggle="tab">
                        <img src="Images/dashboardIcons/attendance.png" class="imgicons" /><span>ATTENDANCE</span></a></li>
                    <li role="presentation"><a href="#persontracking" aria-controls="messages" role="tab" data-toggle="tab">
                        <img src="Images/dashboardIcons/persontracking.png" class="imgicons" /><span>PERSON TRACKING</span></a></li>

                </ul>

                <hr class="hrs" />
                <div class="tab-content tab-content-height ">
                    <div role="tabpanel" class="tab-pane active" id="iot">
                        <div class="col-md-12">
                            <div class="row">

                                <asp:Repeater ID="rptObject" runat="server" OnItemCommand="rptObject_ItemCommand">

                                    <ItemTemplate>
                                        <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                            <div class="pricing-table ">
                                                <div class="pricing-option">
                                                    <p class="text-center">
                                                        <small><span><i runat="server" class='<%# Eval("StatusClass") %>'></i>
                                                            <asp:Label ID="lblDate" runat="server" ClientIDMode="Static" Text='<%# Eval("LastRecordReceived") %>' /></span></small>
                                                    </p>
                                                    <p>
                                                        <strong>
                                                            <img src="Images/dashboardIcons/home.png" class="pricingImg" />
                                                            <asp:Label ID="lblObjName" runat="server" Text='<%# Eval("Name") %>' />
                                                        </strong>
                                                    </p>
                                                    <div class="price">
                                                        <div class="front">
                                                            <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Details</span>
                                                        </div>
                                                        <div class="back">
                                                            <asp:LinkButton ClientIDMode="Static" CssClass="button" Text="View Details" ID="lnkbtnviewObjdt" CommandName="lnkbtnviewObjdt" CommandArgument='<%# Eval("ObjectID") %>' ToolTip="View" runat="server">
                                                            </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divIOT" runat="server" visible="false">
                            <div class=" panel">
                                <div class="text-center">
                                    <h4>No IOT device found!</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="vehicletracking">
                        <div class="col-md-12">
                            <div class="row">
                                <asp:Repeater ID="rptTracker" runat="server">
                                    <ItemTemplate>
                                        <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                            <div class="pricing-table">
                                                <div class="pricing-option">

                                                    <img src="Images/dashboardIcons/vehicletracking.png" style="width: 50px; margin-top: 1px;" />
                                                    <p>
                                                        <strong class="text-center">

                                                            <asp:Label ID="lbltrackername" runat="server" Text='<%# Eval("Name") %>' />
                                                        </strong>
                                                    </p>
                                                    <div class="price">

                                                        <div class="front">
                                                            <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Details</span>
                                                        </div>
                                                        <div class="back">
                                                            <a href="#" class="button" data-toggle="modal" data-target="#myMapModal" style="cursor: pointer">View Details</a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Label ID="lblobjectId" runat="server" Style="visibility: hidden" ClientIDMode="Static" Text='<%# Eval("ObjectId") %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divtracking" runat="server" visible="false">
                            <div class=" panel">
                                <div class="text-center">
                                    <h4>No Tracking device found!</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="attendence">
                        <div class="col-md-12">
                            <div class="row">
                                <asp:Repeater ID="rptAttendance" runat="server" OnItemCommand="rptAttendance_ItemCommand">
                                    <ItemTemplate>
                                        <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                                            <div class="pricing-table">
                                                <div class="pricing-option">
                                                    <img src="Images/dashboardIcons/home.png" style="width: 25px; height: 23px; margin-top: 4px;" />
                                                    <p>
                                                        <strong>
                                                            <asp:Label ID="lblAttendance" runat="server" Text='<%# Eval("Name") %>' />
                                                        </strong>
                                                    </p>

                                                    <div class="price">
                                                        <div class="front">

                                                            <span class="price"><i class="fa fa-chevron-circle-right faCarcolor fa-fw"></i>View Details</span>
                                                        </div>
                                                        <div class="back">
                                                            <asp:LinkButton ClientIDMode="Static" CssClass="button" Text="View Details" ID="lnkbtnAttendancelist" CommandName="lnkbtnviewATDN" CommandArgument='<%# Eval("AttendanceClient") %>' ToolTip="View" runat="server"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divAttendance" runat="server" visible="false">
                            <%--  <div class=" panel">
                             <div class="text-center">
                                    <h4>No attendance device found!</h4>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="persontracking">
                        <div class="col-sm-12">
                            <div class=" panel">
                                <div class="text-center">
                                    <h4>No person tracking device found!</h4>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div role="tabpanel" class="tab-pane" id="iotGroup">
                        <div class="col-sm-12">
                            <asp:Repeater ID="RepeatergroupDetail" runat="server" OnItemDataBound="RepeatergroupDetail_ItemDataBound">
                                <ItemTemplate>
                                    <div class="panel panelCustom">
                                        <div class="panel-body paddingPanel">
                                            <div class="col-sm-1 lblmargin">
                                                <asp:Label runat="server" CssClass="labelCustom" Text='<%# Eval("Device") %>' />
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Repeater ID="rptgroup" runat="server">
                                                    <ItemTemplate>
                                                        <div class="groups">
                                                            <div>
                                                                <img src='<%# Eval("Category") %>' />
                                                                <span>
                                                                    <label class='<%# Eval("StatusIOTClass") %>'></label>
                                                                </span>
                                                                <asp:Label runat="server" CssClass="customH text-center" Text='<%# Eval("Name") %>' />

                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <asp:HiddenField ID="hfObjectId" runat="server" Value='<%# Eval("ObjectID")  +","+ Eval("RelayStatus")%>' />
                                            </div>
                                            <div class="col-sm-1 pull-right lblmargin">
                                                <span class="labelCustom"><i class='<%# Eval("StatusClass") %>'></i>
                                                    <asp:Label runat="server" Text='<%# Eval("DateTimeStamp") %>' /></span>
                                                <%--<label class="labelCustom"><i class="fa fa-plug fa-fw faTopDate-primary"></i>1/17/2018 3:40:40 PM</label>--%>
                                            </div>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>


                        </div>
                        <div class="col-sm-12" id="divIOTGroup" runat="server" visible="false">
                            <div class=" panel">
                                <div class="text-center">
                                    <h4>No IOT tracking device found!</h4>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="loading">
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlclient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlgroup" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>

    <!-- Modal -->
    <div id="myModal" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog dashboardpopup">
            <div class="modal-content">
                <div class="modal-body modalbodyheight">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header" style="padding: 0; border-bottom: 1px solid #337ab7;">
                                <div class="row">
                                    <div class="col-xs-4 col-sm-4 col-md-4">
                                        <img src="Images/sim.png" style="padding-top: 4px;" />
                                        <asp:Label ID="lblsim" runat="server" Text="" CssClass="paneltextdash"></asp:Label>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4" style="padding: 1%;">
                                        <h6 class="modal-title" style="text-align: center; color: #337ab7; font-size: 16px; font-weight: bold">
                                            <asp:Label ID="lblObjId" runat="server" Text="" Style="visibility: hidden"></asp:Label>
                                            <asp:Label ID="lblobj" runat="server" Text=""></asp:Label></h6>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4">
                                        <div id="divtime" runat="server" style="padding-left: 27%; margin-top: 2%">
                                            <div class="btn-group btn-toggle" id="lnkonoff">
                                                <asp:LinkButton ID="lnkBtnAllON" runat="server" OnClick="lnkBtnAllON_Click" OnClientClick="return confirm('Are you sure you want to switch on all Sensors?');" CssClass="btn btn-primary btn-xs">ON</asp:LinkButton>
                                                <asp:LinkButton ID="lnkbtnOFF" runat="server" OnClick="lnkbtnOFF_Click" CssClass="btn btn-danger btn-xs" OnClientClick="return confirm('Are you sure you want to switch off all Sensors?');">OFF</asp:LinkButton>
                                            </div>
                                            <img src="Images/dashboardIcons/Poweroff.png" id="imgpower" runat="server" style="width: 7%" />
                                            <img src="Images/dashboardIcons/Jammer-Barsbb.png" id="imgjamer" runat="server" style="width: 10%" />
                                            <img src="Images/dashboardIcons/Signal-BarsGSMdd.png" id="imgsignal" runat="server" style="width: 7%" />
                                            <asp:Label ID="lblTime" runat="server" Text="" Style="float: right" CssClass="paneltextdash"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <ul class="nav nav-tabs Margin" role="tablist">
                                <li role="presentation" class="active"><a href="#ALL" aria-controls="home" role="tab" data-toggle="tab"><%--<i class="fa fa-home fa-fw"></i>--%><span>ALL</span></a></li>
                                <li role="presentation"><a href="#Appliances" aria-controls="profile" role="tab" data-toggle="tab"><%--<i class="fa fa-car fa-fw"></i>--%><span>Appliances</span></a></li>
                                <li role="presentation"><a href="#Sensors" aria-controls="messages" role="tab" data-toggle="tab"><%--<i class="fa fa-calendar fa-fw">--%></i><span>Sensors</span></a></li>
                                <li role="presentation"><a href="#Fuel" aria-controls="messages" role="tab" data-toggle="tab"><%--<i class="fa fa-calendar fa-fw">--%></i><span>Fuel</span></a></li>
                                <li role="presentation"><a href="#Temperature" aria-controls="messages" role="tab" data-toggle="tab"><%--<i class="fa fa-calendar fa-fw">--%></i><span>Temperature</span></a></li>
                                <%--<li role="presentation"><a href="#Surveillance" aria-controls="messages" role="tab" data-toggle="tab"><%--<i class="fa fa-calendar fa-fw"></i><span>Surveillance</span></a></li>--%>
                            </ul>
                            <hr class="hrs" />
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane active" id="ALL">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-12 col-md-11" id="grdobjdiv">
                                            <div class="table-responsive">
                                                <asp:GridView ID="Gvdobjsensor" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No HVAC Sensors found in this device!" OnRowCommand="Gvdobjsensor_RowCommand">
                                                    <Columns>
                                                        <asp:ImageField HeaderText="Category" DataImageUrlField="Category" HeaderStyle-Width="250px" ItemStyle-Width="20"></asp:ImageField>
                                                        <asp:BoundField DataField="Name" HeaderText="Area" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Voltage" HeaderText="Voltage(Volt)" DataFormatString="{0:n}" HeaderStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Current" HeaderText="Current(Amp)" HeaderStyle-Width="250px" DataFormatString="{0:n}" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Power" HeaderText="Power(KW)" HeaderStyle-Width="250px" DataFormatString="{0:f2}" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:TemplateField HeaderStyle-Width="250px">
                                                            <HeaderTemplate>Status</HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnStatus" runat="server" CommandName="chkstatus" OnClientClick=" return confirm('Are you sure you want to switch status of this Sensor ? ');" Text='<%# Eval("Status").ToString()%>' CommandArgument='<%# Eval("SensorID") +"," + Eval("ObjectID") %>' CssClass='<%# Eval("StatusClass")%>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-6 col-md-6">
                                                    <div id="dvGridAnalog" runat="server">
                                                        <asp:Repeater ID="rptAnalog" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-xs-6 col-sm-3 col-md-3">
                                                                    <div class="panel" style="border: 1px solid #ddd;">
                                                                        <div class="panelimg">
                                                                            <div class="panelboxdashimg">
                                                                                <img src='<%# Eval("Category") %>' runat="server" />
                                                                            </div>
                                                                            <div class="panelboxdashlbl">
                                                                                <asp:Label ID="lblAINTH" runat="server" Text='<%# Eval("Name") %>' CssClass="paneltextdash"></asp:Label>
                                                                            </div>
                                                                            <div class="panelboxdashbtn">
                                                                                <asp:Label ID="lblAIN" runat="server" Text='<%# Eval("Value") %>' CssClass="paneltextdash"></asp:Label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-6 col-md-6">
                                                    <div id="dvGridDin" runat="server">
                                                        <asp:Repeater ID="rptDIN" runat="server">
                                                            <ItemTemplate>
                                                                <div class="col-xs-6 col-sm-3 col-md-3">
                                                                    <div class="panel" style="border: 1px solid #ddd;">
                                                                        <div class="panelimg">
                                                                            <div class="panelboxdashimg">
                                                                                <img src='<%# Eval("Category") %>' runat="server" />
                                                                            </div>
                                                                            <div class="panelboxdashlbl">
                                                                                <asp:Label ID="lblDINTH" runat="server" Text='<%# Eval("Name") %>' CssClass="paneltextdash"></asp:Label>
                                                                            </div>
                                                                            <div class="panelboxdashbtn">
                                                                                <asp:LinkButton ID="btnchkDIN" runat="server" Text='<%# Eval("Value").ToString()%>' CssClass='<%# Eval("CategoryClass").ToString() + " btndashDIN"%>'></asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-xs-12 col-sm-6 col-md-1" id="feuldivparent" runat="server">
                                            <div style="padding-top: 5px;">
                                                <asp:Repeater ID="rptTemp" runat="server" ClientIDMode="Static">
                                                    <ItemTemplate>
                                                        <div class="panel temp" id="bf">
                                                            <img src='<%# Eval("Category") %>' runat="server" class="center-block" />
                                                            <asp:Label ID="lblTempName" runat="server" CssClass="TempText center-block" Text='<%# Eval("Name") %>' />
                                                            <asp:Label ID="lblTempValue" runat="server" CssClass="TempText center-block" Text='<%# Eval("Value") %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Appliances">
                                    <div class="row" style="padding-top: 5px; padding-left: 15px; padding-right: 15px;">
                                        <asp:Repeater ID="rptappliances" runat="server">
                                            <ItemTemplate>
                                                <div class="col-sm-4 col-md-4 col-lg-2">
                                                    <div class=" panel">
                                                        <div class="panel-body">
                                                            <div class="text-center">
                                                                <img src='<%# Eval("Category")%>' runat="server" />
                                                                <br />
                                                                <asp:Label runat="server" CssClass="text-primary" ClientIDMode="Static" Text='<%# Eval("Name").ToString() %>'></asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ClientIDMode="Static" Text='<%# Eval("Voltage","{0:n}") + " V" %>'></asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ClientIDMode="Static" Text='<%# Eval("Current","{0:n}") + " A" %>' DataFormatString="{0:n}"> </asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ClientIDMode="Static" Text='<%# Eval("Power","{0:n}") + " KW" %>' DataFormatString="{0:f2}"></asp:Label>
                                                                <br />
                                                                <label>Status :</label>
                                                                <asp:LinkButton runat="server" Text='<%# Eval("Status").ToString()%>' CssClass='<%# Eval("StatusClass") %>' Enabled="false"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Sensors">
                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="jumbotron jumbotronC">
                                            <div class="row">
                                                <h4 class="text-center fontColor">Digital Sensors</h4>
                                                <hr class="hrs" />
                                                <asp:Repeater ID="rptdigitalsensor" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-sm-6 col-md-6 col-lg-6">
                                                            <div class="panel">
                                                                <div class="panel-body">
                                                                    <div class="col-sm-2">
                                                                        <div class="text-center">
                                                                            <img src='<%# Eval("Category") %>' runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="text-center">
                                                                        <div class="col-sm-7">
                                                                            <asp:Label runat="server" CssClass="contents text-center" Text='<%# Eval("Name") %>'></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:LinkButton ID="lnkbtndinsensors" runat="server" Text='<%# Eval("Value").ToString()%>' CssClass='<%# Eval("CategoryClass").ToString() + " btndashDIN"%>'></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-6 col-md-6 col-sm-12">
                                        <div class="jumbotron jumbotronC">
                                            <div class="row">
                                                <h4 class="text-center fontColor">Analog Sensors</h4>
                                                <hr class="hrs" />
                                                <asp:Repeater ID="rptAnalogsensor" runat="server">
                                                    <ItemTemplate>
                                                        <div class="col-sm-6 col-md-6 col-lg-6">
                                                            <div class=" panel">
                                                                <div class="panel-body">
                                                                    <div class="col-sm-3">
                                                                        <div class="text-center">
                                                                            <img src='<%# Eval("Category") %>' runat="server" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="text-center">
                                                                        <div class="col-sm-5">
                                                                            <asp:Label runat="server" Text='<%# Eval("Name") %>' CssClass="paneltextdash"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-4">
                                                                        <asp:Label runat="server" Text='<%# Eval("Value") %>' CssClass="paneltextdash"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Fuel">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class=" panel">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label>Fuel Level :</label>
                                                                <label>80.44 ltr</label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Status : </label>
                                                                <button class='btn btn-danger btn-xs'>OFF</button>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <div class="form-group">
                                                                <label>Fuel Temperature :</label>
                                                                <label>32 C</label>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Time : </label>
                                                                <label>01-15-2018 12:22:00 </label>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div id="chart" style="height: 300px"></div>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <div id="chart2" style="height: 300px"></div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="Temperature">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="panel-body">
                                                <asp:Repeater ID="rpttempsensor" runat="server" ClientIDMode="Static">
                                                    <ItemTemplate>
                                                        <div class="col-sm-3">
                                                            <div class="panel temp">
                                                                <img src='<%# Eval("Category") %>' runat="server" class="center-block" />
                                                                <asp:Label runat="server" CssClass="TempText center-block" Text='<%# Eval("Name") %>' />
                                                                <asp:Label runat="server" CssClass="TempText center-block" Text='<%# Eval("Value") %>' />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <%--<div role="tabpanel" class="tab-pane" id="Surveillance">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class=" panel">
                                                <div class="panel-body">
                                                    <div class="text-center">
                                                        No Surveillance Found<br />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="myAttendanceModal" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog dashboardpopup">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="container-fluid">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Repeater ID="rptAttendancesuccess" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3 col-sm-4 col-xs-12">
                                            <div class='<%# Eval("cssClass") %>' runat="server">
                                                <div class="panel panel-heading alignATTpanel">
                                                    <div class="col-md-4 col-sm-4 col-xs-8">
                                                        <img src='<%# Eval("Emp_Image") %>' runat="server" style="width: 100%" />
                                                    </div>

                                                    <div class="col-md-8 col-sm-8 col-xs-12">
                                                        <div class="text-centerv">
                                                            <asp:Label runat="server" Text='<%# Eval("EmpName") %>' Style="font-size: 10px" />
                                                        </div>
                                                        <div style="float: left;">
                                                            <asp:Label runat="server" Text='<%# Eval("Designation") %>' Style="font-size: 9px" />
                                                        </div>
                                                        <br />
                                                        <div style="float: left;">
                                                            <asp:Label runat="server" Text='<%# Eval("DeptName") %>' Style="font-size: 9px" />
                                                        </div>
                                                        <br />
                                                        <div style="float: left;">
                                                            <asp:Label runat="server" Text='<%# Eval("ClockTimeStr") %>' Style="font-size: 9px" />
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

                </div>
                <div class="modal-footer">
                     <div class="text-primary pull-left">
                        <a target="_blank" href="http://124.29.205.150/smartattendancesystem/">View more</a>
                    </div>
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myMapModal" class="modal fadeIn" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog dashboardpopup">
            <div class="modal-content">
                <div class="modal-body" style="padding: 0">
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-sm-10" style="padding: 0">
                                <div id="map">
                                </div>
                            </div>
                            <div class="col-sm-2" style="padding: 0">
                                <div class="table-responsive" id="tblexample">
                                    <table id="example" cellspacing="0" width="100%" class="display  table-bordered table-hover table-striped" style="visibility: hidden">
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="text-primary pull-left">
                        <a target="_blank" href="http://124.29.205.149/tavlweb">View more</a>
                    </div>
                    <div class="pull-right">
                        <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%-- <div class="modal fade" id="SeeAllnotifications" tabindex="-1" role="dialog" aria-labelledby="SeeAllnotificationsLabel" aria-hidden="true"  data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">All Notifications</h5>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary btn-xs" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div id="notificationModal" class="notificationmenu">
        <div class="notification-header">
            <h3>Notifications</h3>
        </div>
        <div class="notificationtable">
        </div>
    </div>--%>



    <script type="text/javascript">

        $(function () {
            ddlselect2('.ddlSelect');

        });



        function cg(element) {
            Highcharts.chart(element, {
                chart: {
                    zoomType: 'x'
                },
                title: {
                    text: ''
                },
                credits: {
                    enabled: false
                },

                xAxis: {
                    zoom: 'x',
                    type: 'datetime'
                },
                series: [{
                    name: 'Fuel Level',
                    data: [
                        [moment('2018-01-15 16:14:25.380').valueOf(), 24.44],
                        [moment('2018-01-15 16:16:23.540').valueOf(), 40.55],
                        [moment('2018-01-15 16:14:23.540').valueOf(), 20.55],
                        [moment('2018-01-15 16:17:23.540').valueOf(), 60.55],
                        [moment('2018-01-15 16:19:22.527').valueOf(), 70.66],
                        [moment('2018-01-15 16:20:25.380').valueOf(), 24.44],
                        [moment('2018-01-15 16:21:23.540').valueOf(), 40.55],
                        [moment('2018-01-15 16:22:23.540').valueOf(), 20.55],
                        [moment('2018-01-15 16:23:23.540').valueOf(), 60.55],
                        [moment('2018-01-15 16:24:22.527').valueOf(), 70.66],
                        [moment('2018-01-15 16:25:25.380').valueOf(), 24.44],
                        [moment('2018-01-15 16:26:23.540').valueOf(), 40.55],
                        [moment('2018-01-15 16:27:23.540').valueOf(), 20.55],
                        [moment('2018-01-15 16:28:23.540').valueOf(), 60.55],
                        [moment('2018-01-15 16:29:22.527').valueOf(), 70.66],
                        [moment('2018-01-15 16:30:25.380').valueOf(), 24.44],
                        [moment('2018-01-15 16:31:23.540').valueOf(), 40.55],
                        [moment('2018-01-15 16:32:23.540').valueOf(), 20.55],
                        [moment('2018-01-15 16:33:23.540').valueOf(), 60.55],
                        [moment('2018-01-15 16:34:22.527').valueOf(), 70.66],
                        [moment('2018-01-15 16:35:25.380').valueOf(), 24.44],
                        [moment('2018-01-15 16:36:23.540').valueOf(), 40.55],
                        [moment('2018-01-15 16:37:23.540').valueOf(), 20.55],
                        [moment('2018-01-15 16:38:23.540').valueOf(), 60.55],
                        [moment('2018-01-15 16:39:22.527').valueOf(), 70.66],
                        [moment('2018-01-15 16:40:22.527').valueOf(), 80.66]
                    ]
                }],

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500,
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });
        }
        function csg(element) {
            Highcharts.chart(element, {
                chart: {
                    zoomType: 'x'
                },
                title: {
                    text: ''
                },
                credits: {
                    enabled: false
                },

                xAxis: {
                    type: 'datetime'
                },
                series: [{
                    name: 'Fuel Temperature',
                    data: [
                        [moment('2018-01-15 16:14:25.380').valueOf(), 4.44],
                        [moment('2018-01-15 16:16:23.540').valueOf(), 28.55],
                        [moment('2018-01-15 16:14:23.540').valueOf(), 18.55],
                        [moment('2018-01-15 16:17:23.540').valueOf(), 32.55],
                        [moment('2018-01-15 16:19:22.527').valueOf(), 26.66],
                         [moment('2018-01-15 16:20:25.380').valueOf(), 4.44],
                        [moment('2018-01-15 16:21:23.540').valueOf(), 28.55],
                        [moment('2018-01-15 16:22:23.540').valueOf(), 18.55],
                        [moment('2018-01-15 16:23:23.540').valueOf(), 32.55],
                        [moment('2018-01-15 16:24:22.527').valueOf(), 26.66],
                         [moment('2018-01-15 16:25:25.380').valueOf(), 4.44],
                        [moment('2018-01-15 16:26:23.540').valueOf(), 28.55],
                        [moment('2018-01-15 16:27:23.540').valueOf(), 18.55],
                        [moment('2018-01-15 16:28:23.540').valueOf(), 32.55],
                        [moment('2018-01-15 16:29:22.527').valueOf(), 26.66],
                          [moment('2018-01-15 16:30:25.380').valueOf(), 4.44],
                        [moment('2018-01-15 16:31:23.540').valueOf(), 28.55],
                        [moment('2018-01-15 16:32:23.540').valueOf(), 18.55],
                        [moment('2018-01-15 16:33:23.540').valueOf(), 32.55],
                        [moment('2018-01-15 16:34:22.527').valueOf(), 26.66],
                         [moment('2018-01-15 16:35:25.380').valueOf(), 4.44],
                        [moment('2018-01-15 16:36:23.540').valueOf(), 28.55],
                        [moment('2018-01-15 16:37:23.540').valueOf(), 18.55],
                        [moment('2018-01-15 16:38:23.540').valueOf(), 2.55],
                        [moment('2018-01-15 16:39:22.527').valueOf(), 32.66]
                    ]
                }],

                responsive: {
                    rules: [{
                        condition: {
                            maxWidth: 500,
                        },
                        chartOptions: {
                            legend: {
                                layout: 'horizontal',
                                align: 'center',
                                verticalAlign: 'bottom'
                            }
                        }
                    }]
                }

            });
        }

        $('body').click(function () {
            if (!$(this.target).is('#notifyopen')) {
                $("#notifyopen").fadeOut();
            }
        });

        $("#notif").click(function (e) {
            e.preventDefault();
            $("#notifyopen").fadeIn(300, function () { $(this).focus(); });
            var clientId = $('#MainContent_ddlclient').val();
            if (clientId != undefined) {
                GetNotificationByClient(clientId);
            }
            else {
                var userId = $('#sessionId').text();
                if (userId.includes("GroupID") == true) {
                    var TempId = userId.split("G");
                    var Id = TempId[0];
                    GetNotificationByGroup(Id);
                }
                if (userId.includes('ClientID') == true) {
                    var TempId = userId.split("C");
                    var Id = TempId[0];
                    GetNotificationByClient(Id);
                }
            }
            return false;
        });

        //$('#lnkbtnSeemoreNotif').click(function () {
        //    $('#SeeAllnotifications').modal('show');
        //});
        //$('#tblnotify').click(function () {
        //    $('#SeeAllnotifications').modal('show');
        //});
        $("#myMapModal").on("shown.bs.modal", function () {
            initialize();
            google.maps.event.trigger(map, "resize");

        });

        $('#myMapModal').on('hidden.bs.modal', function () {
            $("#map").empty();
            $('#example').empty();
            $('#example').DataTable().destroy();
            $('#example').css('visibility', 'hidden');
            $("#tblexample").css("background-color", "white");
        });

        function initialize() {
            var objId = $('#lblobjectId').text();
            var res = getObjectbyId(objId);
            var clientId = res['clientId'];
            var groupId = res['groupId'];
            var IP = res['IP'];
            var map = new google.maps.Map(document.getElementById('map'));
            getObjects(clientId, groupId, IP, map);
        }

        function togl() {
            cg(chart); csg(chart2);
            var value = $("#lblLevel").text();
            var level = value / 10;
            $("#lblLevel").text(level + ' Ltr');
            var objId = $('#lblobjectId').text();
            if (objId == '33') {
                $('#lnkonoff').hide();
                $('#MainContent_imgpower').hide();
                $('#MainContent_imgjamer').hide();
                $('#MainContent_imgsignal').hide();
            }

        }


        function chngeDin() {
            if ($('#bf').length == 0) {
                $("#bf").remove();
                $("#grdobjdiv").removeClass('col-xs-11 col-sm-11 col-md-11');
                $("#grdobjdiv").addClass("col-xs-12 col-sm-12 col-md-12");
            }
        }




    </script>
</asp:Content>
