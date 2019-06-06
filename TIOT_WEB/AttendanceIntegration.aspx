<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AttendanceIntegration.aspx.cs" Inherits="TIOT_WEB.AttendanceIntegration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Attendance Integration  </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlClient" runat="server" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" class="dropdown-toggle form-control ddl ddlSelect" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" class="dropdown-toggle form-control ddl ddlSelect" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" runat="server" class="dropdown-toggle form-control ddl ddlSelect" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-2 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtAttendanceClientID" placeholder="Attendance Client ID" runat="server" CssClass="form-control ddl attendancetxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-2 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtAttendanceIP" placeholder="Attendance IP" runat="server" CssClass="form-control ddl attendancetxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 checkbxLable">
                    <div class="form-group">
                        <asp:CheckBox ID="chkEnable" runat="server" Text="Enable" Checked="false" data-toggle="toggle" />
                    </div>
                </div>

                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6 ">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnadd" Text="Integrated" OnClick="btnadd_Click" CssClass="btn bg-primary btn-block btn-xs btnAddAttendance" />
                    </div>
                </div>
            </div>

            <div class="col-lg-12 col-sm-12 col-xs-12">


                <asp:GridView ID="gvdAttendanceIntegration" runat="server" CssClass="table table-bordered table-striped table-responsive" align="center"
                    AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="ObjectID" HeaderText="Object ID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttendanceClient" HeaderText="Attendance ClientID" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AttendanceIP" HeaderText="Attendance IP" ItemStyle-HorizontalAlign="Center" />
                        <asp:TemplateField HeaderText="Enable">
                            <ItemTemplate>
                                <asp:CheckBox ID="gvdcheckEnable" runat="server" Checked='<%# Eval("AttendanceStatus") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ObjectID") %>' OnCommand="linkbtnDel_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
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
            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvdAttendanceIntegration" />
        </Triggers>
    </asp:UpdatePanel>
    <script>

        $(function () {
            staticMethod('Disable');
        });
        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btnAddAttendance').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnAddAttendance').prop('disabled', true);
            }
            enabledSubmit('.attendancetxt', '.btnAddAttendance');
            $('.ddlSelect').select2();
        }
    </script>
</asp:Content>
