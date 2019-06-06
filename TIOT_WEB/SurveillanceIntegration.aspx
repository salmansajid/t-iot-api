<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SurveillanceIntegration.aspx.cs" Inherits="TIOT_WEB.SurveillanceIntegration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                ApplyDatatable("#gvdSurveillanceIntegration", 9);
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx"> Configuration </a> <i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Surveillance Integration  </h6>
    </div>

    <div class="row">
        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:DropDownList ID="ddlClient" runat="server" OnSelectedIndexChanged="ddlClient_SelectedIndexChanged" class="dropdown-toggle form-control ddl" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:DropDownList ID="ddlGroup" runat="server" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" class="dropdown-toggle form-control ddl" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:DropDownList ID="ddlObject" runat="server" class="dropdown-toggle form-control ddl">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:TextBox ID="txtSurveillanceIP" placeholder="Surveillance IP" runat="server" CssClass="form-control ddl"></asp:TextBox>

            </div>
        </div>
        <div class="col-lg-2 col-md-3 col-sm-3 col-xs-6">
            <div class="form-group">
                <asp:TextBox ID="txtTavlGroupID" placeholder="Surveillance Port" runat="server" CssClass="form-control ddl"></asp:TextBox>

            </div>
        </div>
          <div class="col-lg-2 col-md-2 col-sm-3 col-xs-6 checkbxLable">
            <div class="form-group">
                <asp:CheckBox ID="chkEnable" runat="server" Text="Enable" Checked="false" data-toggle="toggle" />

            </div>
        </div>

        <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6 pull-right">
            <div class="form-group">
                <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn bg-primary btn-block btn-xs" />
            </div>
        </div>
        <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6  pull-right">
            <div class="form-group">
                <asp:Button runat="server" ID="btnadd" Text="Add" CssClass="btn bg-primary btn-block btn-xs" />
            </div>
        </div>
    </div>

    <div class="col-lg-12 col-sm-12 col-xs-12">


        <%--<asp:GridView ID="gvdSurveillanceIntegration" ClientIDMode="Static" runat="server" Style="text-align: center;" CssClass="table table-bordered table-striped table-responsive" align="center"
            Width="100%" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
              <Columns>
                <asp:BoundField DataField="" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="" HeaderText="" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="Update" CommandArgument='<%# Eval("LoginID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                        <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" CommandArgument='<%# Eval("LoginID") %>' OnCommand="linkbtnDel_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>--%>
    </div>
</asp:Content>
