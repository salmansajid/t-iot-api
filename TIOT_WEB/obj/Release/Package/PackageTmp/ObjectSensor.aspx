<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjectSensor.aspx.cs" Inherits="TIOT_WEB.ObjectSensor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Device Sensors  </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row lineHeight">
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlClient" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" runat="server" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSensor" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlcategory" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtName" placeholder="Sensor Name *" runat="server" CssClass="form-control ddl objectSensortxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtMin" placeholder="MIN *" MaxLength="8" runat="server" CssClass="form-control ddl objectSensortxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtMax" placeholder="MAX *" MaxLength="8" runat="server" CssClass="form-control ddl objectSensortxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
            </div>
            <div class="row lineHeight">
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txta0" placeholder="a 0 *" runat="server" CssClass="form-control ddl objectSensortxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txta1" placeholder="a 1 *" runat="server" CssClass="form-control ddl objectSensortxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtContact" ClientIDMode="Static" runat="server" placeholder="Mobile Alerts Number *" Width="100%" CssClass="form-control ddl objecttxt tags contact"></asp:TextBox>
                    </div>
                </div>

                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 checkbxLable">
                    <div class="form-group">
                        <asp:CheckBox ID="cbSmsAlert" runat="server" Text="SMS Alert" data-toggle="toggle" />
                    </div>
                </div>
                <%--   <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12 checkbxLable">
            <div class="form-group">
                <asp:CheckBox ID="cbEmailAlert" runat="server" Text="Email Alert" Checked data-toggle="toggle" />
            </div>
        </div>--%>

                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12 pull-right ">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddObjectSensor" Text="Save" OnClick="btnAddObjectSensor_Click" CssClass="btn bg-primary btn-block btn-xs btnObjectSensor" />
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:GridView ID="gvdObjectSensor" runat="server" CssClass="table table-bordered table-striped gvdObjectSensorClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                          <asp:BoundField DataField="ObjectSensorID" HeaderText="ID"/>
                        <asp:BoundField DataField="Name" HeaderText="Device Sensor" />
                        <asp:BoundField DataField="SourceID" HeaderText="Sensor" />
                        <asp:BoundField DataField="Max" HeaderText="Max"/>
                        <asp:BoundField DataField="Min" HeaderText="Min" />
                        <asp:TemplateField HeaderText="SMS Alert">
                            <ItemTemplate>
                                <asp:CheckBox Checked='<%#bool.Parse(Eval("SMSAlert").ToString()) %>' runat="server" Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Contact" HeaderText="Alert Contact"/>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("ObjectSensorId") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                <asp:LinkButton ID="linkbtnDisable" runat="server" CommandName="Disable" CommandArgument='<%# Eval("ObjectSensorId") %>' OnCommand="linkbtnDisable_Command" CssClass="btn btn-danger btn-xs"><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvdObjectSensor" />
            <asp:AsyncPostBackTrigger ControlID="btnAddObjectSensor" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">



        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function closeDeleteModal() {
            $('#delete').modal('hide')
        }


        function phonenumber() {
            $('.tags').tagsinput({
                allowDuplicates: true
            });
            $('.tags').on('itemAdded', function (item, tag) {
                $('.items').html('');
                var tags = $('.tags').tagsinput('items');
            });
        }

        $(function () {
            staticMethod('Disable');
        });

        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btnObjectSensor').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnObjectSensor').prop('disabled', true);
            }
            enabledSubmit('.objectSensortxt', '.btnObjectSensor');
            $('.ddlSelect').select2();
            phonenumber();
        }


    </script>
</asp:Content>
