<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjectGroup.aspx.cs" Inherits="TIOT_WEB.ObjectGroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx"> Configuration </a> <i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Device Branches  </h6>
    </div>
        <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:DropDownList ID="ddlClient" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" AutoPostBack="true" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:DropDownList ID="ddlObject" runat="server" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" AutoPostBack="true" class="dropdown-toggle form-control ddl objectgroupddl ddlSelect">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:DropDownList ID="ddlGroup" runat="server" class="dropdown-toggle form-control  ddl objectgroupddl ddlSelect">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:Button runat="server" ID="btnAddObjectGroup" Text="Save" OnClick="btnAddObjectGroup_Click" CssClass="btn bg-primary btn-block btn-xs btnobjectgroup" />
            </div>
        </div>
        <div class="col-lg-1 col-md-4 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
            </div>
        </div>
    </div>

    <div class="row">
        
            <asp:GridView ID="gvdObjectGroup"  runat="server" CssClass="table table-responsive table-bordered table-striped gvdObjectGroupClass" align="center"
                 AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                <Columns>
                    <asp:BoundField DataField="ObjectName" HeaderText="Device" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="GroupName" HeaderText="Branch" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("ObjectGroupID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                            <asp:LinkButton ID="linkbtnRemoveID" runat="server" CommandName="RemoveID" CommandArgument='<%# Eval("ObjectGroupID") %>' OnCommand="linkbtnRemoveID_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
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
                    <h4 class="modal-title custom_align" id="Heading2">Delete Object Group</h4>
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
            <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddObjectGroup" EventName="Click" />
            
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="gvdObjectGroup" />
        </Triggers>
    </asp:UpdatePanel>
      <script type="text/javascript">
          $(function () {
              staticMethod('Disable');
          });
          function staticMethod(_key) {
              if (_key == 'Enable') {
                  $('.btnobjectgroup').prop('disabled', false);
              }
              if (_key == 'Disable') {
                  $('.btnobjectgroup').prop('disabled', true);
              }
              enabledSubmit('.objectgroupddl', '.btnobjectgroup');
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
