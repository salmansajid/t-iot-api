<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Scheduling.aspx.cs" Inherits="TIOT_WEB.Scheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .ui-timepicker-wrapper {
            margin-left: 0.5%;
            width: 26%;
        }

        #GvdScheduling tbody tr td {
            padding: 5px;
            vertical-align: middle;
            line-height: 0.428571;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Scheduling </h6>
        </div>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12" id="divClient" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlclient" runat="server" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12" id="divGroup" runat="server">
                    <div class="form-group">

                        <asp:DropDownList ID="ddlgroup" runat="server" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" runat="server" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlDays" runat="server" OnSelectedIndexChanged="ddlDays_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect" Visible="false">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSensor" runat="server" class="dropdown-toggle form-control ddl ddlSelect" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlSensor_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn bg-primary btn-block btn-xs" Visible="false" />
                    </div>
                </div>
            </div>

            <div class="row" style="padding: 1%">

                <asp:GridView ID="GvdAllScheduling" runat="server" CssClass="table table-bordered table-striped GvdSchedulingClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!" Visible="false">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Sensor Name" />
                        <asp:BoundField DataField="DaysName" HeaderText="Day"/>
                        <asp:TemplateField HeaderText="Start Time">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:TextBox ID="txtstarttime" Text='<%# Eval("StartTime") %>' CssClass="time form-control" runat="server"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Time">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:TextBox ID="txtendtime" Text='<%# Eval("EndTime") %>' CssClass="time form-control" runat="server"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:CheckBox ID="chkstatus" Checked='<%#bool.Parse(Eval("EnableOrDisable").ToString()) %>' runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkUpdateAll" runat="server" CommandName="Update"
                                    CommandArgument='<%# Eval("SchedulingID") %>' CssClass="btn btn-xs btn-primary" OnCommand="linkUpdateAll_Command"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                <asp:LinkButton ID="linkbtnRemoveID" runat="server" CommandName="RemoveID"
                                    CommandArgument='<%# Eval("SchedulingID") %>' OnCommand="linkbtnRemoveID_Command" CssClass="btn btn-xs btn-danger"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>


            <div class="modal fade" id="delete" role="dialog">
                <div class="modal-dialog modal-md">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header tHeader">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="fa fa-times" aria-hidden="true"></span></button>
                            <h4 class="modal-title custom_align" id="Heading2">Delete Schedule</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this ?</div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="linkbtnDelAll" runat="server" CommandName="Remove" OnCommand="linkbtnDelAll_Command" CssClass="btn btn-success btn-sm"><span><i class="fa fa-check fa-fw"></i></span></asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                        </div>
                    </div>

                </div>
            </div>

            <asp:Panel ID="holidaysPanel" runat="server" Visible="false">
                <div class="row">
                    <asp:GridView ID="GvdSHolidayscheduling" runat="server" CssClass="table table-bordered table-striped " AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                        <Columns>
                            <%-- <asp:BoundField DataField="Holidays" HeaderText="Holiday" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Holiday">
                                <ItemTemplate>
                                    <asp:Label ID="lblHolidays" Text='<%# Eval("Holidays") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblFullDate" Text='<%# Eval("FullDate") %>' DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="FullDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center" />--%>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <div class="form-group" style="margin: 0">
                                        <asp:CheckBox runat="server" ID="chkstatus" Checked='<%#bool.Parse(Eval("Enabled").ToString()) %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <%--  <asp:Button ID="btnsaveHoliday" runat="server" Text="Save Holiday" CssClass="btn btn-xs btn-primary" />
                                CommandName="btnsaveClick" CommandArgument='<%# Eval("HolidaysID") %>'--%>
                                    <asp:LinkButton ID="linkUpdateHoliday" runat="server" CommandName="Update"
                                        CommandArgument='<%# Eval("HolidaysID") %>' CssClass="btn btn-xs btn-primary" OnCommand="linkUpdateHoliday_Command"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                    <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove"
                                        CommandArgument='<%# Eval("HolidaysID") %>' OnCommand="linkbtnDel_Command" CssClass="btn btn-xs btn btn-danger"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row">
                    <div class="col-lg-4 col-md-4">
                        <div class="form-group">
                            <asp:TextBox ID="txtHolidaydt" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Select Holiday"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4">
                        <div class="form-group">
                            <asp:TextBox ID="txtHolidaydesc" runat="server" CssClass="form-control" placeholder="Holiday"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <div class="form-group">
                            <asp:CheckBox runat="server" ID="chkEnabledHoliday" Text="Enabled" />
                        </div>
                    </div>
                    <div class="col-lg-2 col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnAddHoliday" runat="server" OnClick="btnAddHoliday_Click" CssClass="btn btn-primary btn-block" Text="Add Holiday" />
                        </div>
                    </div>
                </div>

            </asp:Panel>


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
            <asp:AsyncPostBackTrigger ControlID="ddlDays" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlSensor" EventName="SelectedIndexChanged" />

            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />

            <asp:AsyncPostBackTrigger ControlID="GvdAllScheduling" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            $('.time').timepicker({
                'minTime': '7:00AM',
                'maxTime': '12:00AM',
                am: 'AM',
                pm: 'PM',

            });

        });
        $('#txtHolidaydt').datepicker({
            changeMonth: true,
        });

        $(function () {
            //applyDatatable(".GvdSchedulingClass");
           staticMethod();
       });
       function staticMethod() {
           $('.ddlSelect').select2();
       }

        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function CloseDeleteModal() {
            $('#delete').modal('hide')
        }
    </script>
</asp:Content>
