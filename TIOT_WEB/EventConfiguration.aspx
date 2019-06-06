<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventConfiguration.aspx.cs" Inherits="TIOT_WEB.EventConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Event Configuration </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2  col-sm-3 col-xs-12" id="divClient" runat="server">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlClient" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" runat="server" class="dropdown-toggle form-control ddl ddlSelect"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2  col-sm-3 col-xs-12" runat="server" id="divGroup">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" class="dropdown-toggle form-control ddl ddlSelect"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2  col-sm-3 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" runat="server" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObjSensor" runat="server" class="dropdown-toggle form-control ddl ddlSelect"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-lg-1 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtMin" placeholder="Min" runat="server" CssClass="form-control ddl EventConfigtxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtMax" placeholder="Max *" runat="server" CssClass="form-control ddl EventConfigtxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txta0" placeholder="a 0 *" runat="server" CssClass="form-control ddl EventConfigtxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txta1" placeholder="a 1 *" runat="server" CssClass="form-control ddl EventConfigtxt" onkeypress="ValidateNumAndDot();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlCondition" runat="server" class="dropdown-toggle form-control ddl"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtContact" ClientIDMode="Static" runat="server" placeholder="Mobile Alerts Number *" Width="100%" CssClass="form-control ddl objecttxt tags contact" onkeypress="ValidateNumAndDot();"></asp:TextBox>
                    </div>

                </div>

                <div class="col-lg-1   col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtFormat" placeholder="Format *" runat="server" CssClass="form-control ddl EventConfigtxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1  col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:TextBox ID="txtUnit" placeholder="Unit *" runat="server" CssClass="form-control ddl EventConfigtxt"></asp:TextBox>

                    </div>
                </div>

                <div class="col-lg-2 col-sm-6 col-xs-12 checkbxLable">
                    <div class="form-group">
                        <asp:CheckBox ID="cbEnabled" runat="server" Text="Enable" data-toggle="toggle" />
                    </div>
                </div>
                <div class="col-lg-1 col-sm-6 col-xs-12  pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1   col-sm-6 col-xs-12 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" CssClass="btn bg-primary btn-block btn-xs btnSaveEventConfig" disabled="true" />
                    </div>
                </div>
            </div>


            <div class="row">
                <asp:GridView ID="gvdEventConfig" runat="server" CssClass="table table-bordered table-striped gvdEventConfigClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Sensor Name" />
                        <asp:BoundField DataField="Min" HeaderText="Min" />
                        <asp:BoundField DataField="Max" HeaderText="Max" />
                        <asp:BoundField DataField="Contact" HeaderText="Alerts Contact" />
                        <asp:BoundField DataField="Units" HeaderText="Units" />
                        <asp:BoundField DataField="Format" HeaderText="Format" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="gvdcheckEnable" runat="server" Checked='<%# Eval("EnableOrDisable") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <%--<asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("EventConfigID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>--%>
                                <asp:LinkButton ID="linkbtnRemoveID" runat="server" CommandName="RemoveID" CommandArgument='<%# Eval("EventConfigID") %>' OnCommand="linkbtnRemoveID_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                            <h4 class="modal-title custom_align" id="Heading2">Delete</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this?</div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" OnCommand="linkbtnDel_Command" CssClass="btn btn-success btn-sm"><span><i class="fa fa-check fa-fw"></i></span></asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                        </div>
                    </div>

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
            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlGroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvdEventConfig" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
   

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
                $('.btnSaveEventConfig').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnSaveEventConfig').prop('disabled', true);
            }
            $('.ddlSelect').select2();
            enabledSubmit('.EventConfigtxt', '.btnSaveEventConfig');
            phonenumber();            
            //$('.contacts').inputmask('+923[999999999],+923[999999999],+923[999999999],');
        }
        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function CloseDeleteModal() {
            $('#delete').modal('hide')
        }
    </script>
</asp:Content>

