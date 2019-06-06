<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Client.aspx.cs" Inherits="TIOT_WEB.Client" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Client  </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row lineHeight">
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtClient" placeholder="Client *" runat="server" CssClass="form-control ddl clienttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtAddress" placeholder="Address *" runat="server" CssClass="form-control ddl clienttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtEmail" placeholder="Email *" runat="server" CssClass="form-control ddl clienttxt emails"></asp:TextBox>
                    </div>

                </div>
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtContact" placeholder="Contact *" runat="server" CssClass="form-control ddl clienttxt contacts"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtExpireDate" ClientIDMode="Static" placeholder="Expired" runat="server" CssClass="form-control ddl clienttxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtOperatorId" placeholder="Operator *" runat="server" CssClass="form-control ddl clienttxt OperatorID"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtCode" placeholder="Code *" runat="server" CssClass="form-control ddl clienttxt Codes uppercase"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row lineHeight">
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs " />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddClient" Text="Save" OnClick="btnAddClient_Click" CssClass="btn bg-primary btn-block btn-xs btnaddClient" />
                    </div>
                </div>

            </div>

            <div class="row">
                <asp:GridView ID="GvdClient" runat="server" CssClass="table table-bordered table-striped gvdclientclass"  AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="ClientID" HeaderText="ID" />
                        <asp:BoundField DataField="Name" HeaderText="Client" />
                        <asp:BoundField DataField="Code" HeaderText="Code" />
                        <asp:BoundField DataField="Contact" HeaderText="Contact" />
                        <asp:BoundField DataField="ExpireDate" HeaderText="Expired" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("ClientID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                <asp:LinkButton ID="linkbtnRemoveID" CommandName="RemoveID" CommandArgument='<%# Eval("ClientID") %>' OnCommand="linkbtnRemoveID_Command" runat="server" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                            <h4 class="modal-title custom_align" id="Heading2">Delete Client</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this Client?</div>
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
            <asp:AsyncPostBackTrigger ControlID="btnAddClient" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GvdClient" />
        </Triggers>
    </asp:UpdatePanel>


    <script type="text/javascript">
        $(document).ready(function () {
            applyDatatable('.gvdclientclass');
            staticMethod('Disable');
        });

        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btnaddClient').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnaddClient').prop('disabled', true);
            }
            $("#txtExpireDate").datepicker();
            enabledSubmit('.clienttxt', '.btnaddClient');
            clientMasking();
        }

        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function CloseDeleteModal() {
            $('#delete').modal('hide')
        }
        function clientMasking() {
            $(".emails").inputmask("email");
            $('.contacts').inputmask('+923[999999999]');
            $('.OperatorID').inputmask('[99999]');
            $('.Codes').inputmask('[AAA]');
        }
    </script>

</asp:Content>
