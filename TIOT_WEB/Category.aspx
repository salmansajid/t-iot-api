<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="TIOT_WEB.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Category </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12 ">
                    <div class="form-group">
                        <asp:TextBox ID="txtName" placeholder="Name *" runat="server" CssClass="form-control ddl categorytxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12 ">
                    <div class="form-group">
                        <asp:TextBox ID="txtMin" placeholder="High *" runat="server" CssClass="form-control ddl categorytxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12 ">
                    <div class="form-group">
                        <asp:TextBox ID="txtMax" placeholder="Low *" runat="server" CssClass="form-control ddl categorytxt"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12 ">
                    <div class="form-group">
                        <asp:FileUpload ID="imgCategory" CssClass="form-control ddl" ToolTip="Upload Icon" runat="server" AllowMultiple="true" />
                    </div>
                </div>

                <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:CheckBox ID="chkEnable" runat="server" CssClass="checkbox"></asp:CheckBox>
                        <label>Enabled</label>
                    </div>
                </div>
                <div class="col-lg-1  col-md-3 col-sm-4 col-xs-12 checkbxLable pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-3 col-sm-4 col-xs-12 checkbxLable pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddCategory" Text="Save" OnClick="btnAddCategory_Click" CssClass="btn bg-primary btn-block btn-xs btnaddCategory" />
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:GridView ID="GVDCategory" runat="server" CssClass="table table-bordered table-striped gvdcategoryclass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:ImageField HeaderText="Category" DataImageUrlField="ImgPath"></asp:ImageField>
                        <asp:BoundField DataField="Name" HeaderText="Category" />
                        <asp:BoundField DataField="Min" HeaderText="Low" />
                        <asp:BoundField DataField="Max" HeaderText="High" />
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkEnableORDisable" Checked='<%#bool.Parse(Eval("EnableOrDisable").ToString()) %>' runat="server" Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("CategoryID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                                <asp:LinkButton ID="linkbtnRemoveID" runat="server" CommandName="RemoveID" CommandArgument='<%# Eval("CategoryID") %>' OnCommand="linkbtnRemoveID_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                            <h4 class="modal-title custom_align" id="Heading2">Delete Category</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this Category?</div>
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
            <asp:AsyncPostBackTrigger ControlID="btnAddCategory" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GVDCategory" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            applyDatatable('.gvdcategoryclass');
            staticMethod('Disable');
        });

        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btnaddCategory').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnaddCategory').prop('disabled', true);
            }
            $("#txtExpireDate").datepicker();
            enabledSubmit('.categorytxt', '.btnaddCategory');
        }

        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function CloseDeleteModal() {
            $('#delete').modal('hide')
        }
    </script>
</asp:Content>
