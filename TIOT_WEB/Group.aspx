<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Group.aspx.cs" Inherits="TIOT_WEB.Group" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Branches  </h6>
    </div>
        <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div class="row lineHeight">
        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:DropDownList ID="ddlClient" runat="server" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:TextBox ID="txtGroupName" runat="server" placeholder="Branch *" CssClass="form-control ddl grouptxt"></asp:TextBox>
            </div>
        </div>


        <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:TextBox ID="txtComments" runat="server" placeholder="Comments *" CssClass="form-control ddl grouptxt"></asp:TextBox>

            </div>
        </div>

        <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6  pull-right">
            <div class="form-group">
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
            </div>
        </div>
        <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6  pull-right ">
            <div class="form-group">
                <asp:Button runat="server" ID="btnGroup" Text="Save" OnClick="btnGroup_Click" CssClass="btn bg-primary btn-block btn-xs btngroupuser"/>
            </div>
        </div>
    </div>

    <div class="row">
        <asp:GridView ID="gvdGroup"  runat="server" CssClass="table table-bordered table-striped gvdGroupClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
            <Columns>
                <asp:BoundField DataField="GroupID" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Branch" />
                <asp:BoundField DataField="Comment" HeaderText="Comments" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("GroupID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>                        
                        <asp:LinkButton ID="linkbtnRemoveID" CommandName="RemoveID" CommandArgument='<%# Eval("GroupID") %>' OnCommand="linkbtnRemoveID_Command" runat="server" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                    <h4 class="modal-title custom_align" id="Heading2">Delete Branch</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="alert alert-danger"><span class="fa-fw fa fa-warning"></span>Are you sure you want to delete this Branch?</div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" OnCommand="linkbtnDel_Command" CssClass="btn btn-success btn-sm"><span><i class="fa fa-check fa-fw"></i></span></asp:LinkButton>
                    <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                </div>
            </div>

        </div>
    </div> <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Update1">
                <ProgressTemplate>
                      <div class="loading">
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnGroup" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="gvdGroup" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">

        $(function () {
            staticMethod('Disable');
        });

        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btngroupuser').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btngroupuser').prop('disabled', true);
            }
            enabledSubmit('.grouptxt', '.btngroupuser');
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
