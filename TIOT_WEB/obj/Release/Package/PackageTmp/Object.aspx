<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Object.aspx.cs" Inherits="TIOT_WEB.Object" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Devices  </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlClient" runat="server" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtObjectName" placeholder="Device Name *" runat="server" CssClass="form-control ddl objecttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtAddress" runat="server" placeholder="Address *" CssClass="form-control ddl objecttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtLat" runat="server" placeholder="Latitude *" CssClass="form-control ddl objecttxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtLong" runat="server" placeholder="Longtitude *" CssClass="form-control ddl objecttxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtIMEI" runat="server" placeholder="IMEI *" CssClass="form-control ddl objecttxt IMEI"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtSimNumber" runat="server" placeholder="Sim # *" CssClass="form-control ddl objecttxt SIM"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtFirmWareVersion" runat="server" placeholder="Firmware Version *" CssClass="form-control ddl objecttxt"></asp:TextBox>


                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtHardwareVersion" runat="server" placeholder="Hardware Version *" CssClass="form-control ddl objecttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-4 col-md-2 col-sm-6 col-xs-6">
                    <asp:TextBox ID="txtContact" ClientIDMode="Static" runat="server" placeholder="Mobile Alerts Number *" Width="100%" CssClass="form-control ddl objecttxt tags contact" onkeypress="ValidateNumAndDot();"></asp:TextBox>
                </div>

                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList runat="server" CssClass="form-control dropdown dropdown-toggle ddl" ID="ddlDeviceType">
                            <%--<asp:ListItem Value="0">Select Device Type *</asp:ListItem>--%>
                            <asp:ListItem Value="1">Home</asp:ListItem>
                            <asp:ListItem Value="2">Office</asp:ListItem>
                            <asp:ListItem Value="3">Industry</asp:ListItem>
                            <asp:ListItem Value="4">hospital</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6 checkbxLable">
                    <div class="form-group">
                        <asp:CheckBox ID="chkRelaySt" runat="server" Text="Relay Status"></asp:CheckBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6  pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddObject" Text="Save" OnClick="btnAddObject_Click" CssClass="btn bg-primary btn-block btn-xs btnObject" disabled="trulinkbtnDee" />

                    </div>
                </div>
            </div>

            <div class="row">
                <asp:GridView ID="gvdObject" runat="server" CssClass="table table-bordered table-striped gvdObjectClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="ObjectID" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Device" />
                        <asp:BoundField DataField="IMEI" HeaderText="IMEI" />
                        <asp:BoundField DataField="SimNumber" HeaderText="Sim Number" />
                        <asp:BoundField DataField="Contact" HeaderText="Contact" />
                        <asp:BoundField DataField="CreatedDateTime" HeaderText="Created Date" />
                        <asp:TemplateField HeaderText="Relay Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbEnabled" Checked='<%#bool.Parse(Eval("RelayStatus").ToString()) %>' runat="server" Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("ObjectID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                <asp:LinkButton ID="linkbtnRemoveID" runat="server" CommandName="RemoveID" CommandArgument='<%# Eval("ObjectID") %>' OnCommand="linkbtnRemoveID_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                            <h4 class="modal-title custom_align" id="Heading2">Delete Object</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this Object?</div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" OnCommand="linkbtnDel_Command" CssClass="btn btn-success btn-sm"><span><i class="fa fa-check fa-fw"></i></span></asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                        </div>
                    </div>

                </div>
            </div>



            <div class="modal animated fadeIn" id="ContactsModal" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Mobile Alert Numbers</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input_fields_wrap">
                                <input type="text" id="txt1" name="txtContactz" value="+92" maxlength="13" />
                                <button class=" add_field_button btn btn-xs btn-primary"><i class="fa fa-plus"></i></button>


                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary btn-sm" id="btnShow">Save</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlclient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddObject" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvdObject" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
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
                allowDuplicates: false,
                maxTags: 3,
                maxChars: 13
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
                $('.btnObject').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnObject').prop('disabled', true);
            }
            enabledSubmit('.objecttxt', '.btnObject');
            $('.ddlSelect').select2();
            phonenumber();
            $('.IMEI').inputmask('[9999999999999999]');
            $('.SIM').inputmask('[9999999999]');
            $('.contacts').inputmask('+923[999999999],+923[999999999],+923[999999999],');
        }

    </script>
</asp:Content>
