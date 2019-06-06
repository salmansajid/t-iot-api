<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ControllingReport.aspx.cs" Inherits="TIOT_WEB.ControllingReport" %>
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
                        <asp:DropDownList ID="ddlobject" ClientIDMode="Static" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
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
                <div class="col-lg-1 col-md-6">
                    <div class="form-group">
                        <asp:Button ID="btnGetReport" runat="server" CssClass="btn bg-primary btn-block btn-xs BTNtop" OnClick="btnGetReport_Click" Text="Get Report"/>
                    </div>
                </div>
      <%--             <div class="col-lg-1 col-md-6 ">
                    <div class="form-group">
                        <button type="button" class="btn bg-primary btn-block btn-xs" data-toggle="modal" data-target="#myModal">Visual</button>
                    </div>
                </div>--%>
            </div>
            <div class="row">
                <asp:GridView ID="gvdcontrollingReport" ClientIDMode="Static" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="No Record Found!" >
                    <Columns>
                        <asp:BoundField HeaderText="Name" DataField="Name" />
                        <asp:BoundField HeaderText="Start Time" DataField="StartTime" />
                        <asp:BoundField HeaderText="End Time" DataField="EndTime" />
                        <asp:BoundField HeaderText="End Time" DataField="EndTime" />
                        <asp:BoundField HeaderText="Total Time  (hh:mm:ss)" DataField="TotalTime" />
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
                    <div id="chart" style="width: 100%; margin-top:1em" ></div>
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
            datetimepicker('#txtdtrange');
            $('.ddlSelect').select2();
        }

        //$("#myModal").on("shown.bs.modal", function () {
        //    temptable = table;
        //    var element = document.getElementById('chart');
        //    gettimelineGraph(element, temptable);

        //});
    </script>
</asp:Content>

