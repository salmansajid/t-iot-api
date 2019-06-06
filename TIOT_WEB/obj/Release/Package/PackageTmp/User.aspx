<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="TIOT_WEB.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>User  </h6>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlClient" runat="server" class="dropdown-toggle form-control ddl ddlSelect" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlUserType" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                            <asp:ListItem Value="0" Text="Power User"></asp:ListItem>
                            <asp:ListItem Value="1" Text="User"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtUser" placeholder="Username *" runat="server" CssClass="form-control ddl usertxt "></asp:TextBox>
                    </div>
                </div>

                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtPassword" placeholder="Password *" runat="server" TextMode="Password" CssClass="form-control ddl usertxt P1"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtConfirmPassword" ClientIDMode="Static" placeholder="Confirm Password *" TextMode="Password" runat="server" CssClass="form-control ddl usertxt P2"></asp:TextBox><span><asp:Label runat="server" ID="lblErrorPassword" CssClass="alert-danger"></asp:Label></span>
                        <span>
                            <p class="ValidatePass animated"></p>
                        </span>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtdisplayName" placeholder="Display Name *" runat="server" CssClass="form-control ddl usertxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtComments" placeholder="Comments" runat="server" CssClass="form-control ddl usertxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" href="#" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6  pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddUser" Text="Add" OnClick="btnAddUser_Click" CssClass="btn bg-primary btn-block btn-xs btnadduser" />
                    </div>
                </div>
            </div>


            <asp:GridView ID="GvdUser" runat="server" CssClass="table table-bordered table-striped table-responsive gvduserclass" AutoGenerateColumns="false" EmptyDataText="No user available!">
                <Columns>
                    <asp:BoundField DataField="LoginID" HeaderText="ID" />
                    <asp:BoundField DataField="Role" HeaderText="Role" />
                    <asp:BoundField DataField="User" HeaderText="User"/>
                    <asp:BoundField DataField="Comment" HeaderText="Comments" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("LoginID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                            <asp:LinkButton ID="linkbtnRemoveID" CommandName="RemoveID" CommandArgument='<%# Eval("LoginID") %>' OnCommand="linkbtnRemoveID_Command" runat="server" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


            <div class="modal fade" id="delete" role="dialog">
                <div class="modal-dialog modal-md">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header tHeader">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="fa fa-times" aria-hidden="true"></span></button>
                            <h4 class="modal-title custom_align" id="Heading2">Delete User</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this User?</div>
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
            <asp:AsyncPostBackTrigger ControlID="btnAddUser" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="GvdUser" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(function () {
            staticMethod('Disable');
        });

        function staticMethod(_key)
        {
            if (_key == 'Enable') {
                $('.btnadduser').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnadduser').prop('disabled', true);
            }
            enabledSubmit('.usertxt', '.btnadduser');
            $(".P2").keyup(validatePasswords);
            $('.ddlSelect').select2();
        }

        function openDeleteModal() {
            $('#delete').modal('show')
        }

        function closeDeleteModal() {
            $('#delete').modal('hide')
        }
   
    </script>
</asp:Content>
