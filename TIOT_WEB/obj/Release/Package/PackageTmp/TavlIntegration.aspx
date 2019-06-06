<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TavlIntegration.aspx.cs" Inherits="TIOT_WEB.TavlIntegration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>

    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Tavl Integration  </h6>
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
                        <asp:DropDownList ID="ddlObject" runat="server" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" class="dropdown-toggle form-control ddl ddlSelect" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtTavlClientID" placeholder="Client #" runat="server" CssClass="form-control ddl tavlIntegrationtxt" onkeypress="NumberOnly();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtTavlGroupID" placeholder="Group #" runat="server" CssClass="form-control ddl tavlIntegrationtxt" onkeypress="NumberOnly();"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtTavlIP" placeholder="IP" runat="server" CssClass="form-control ddl tavlIntegrationtxt ip_address"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 checkbxLable">
                    <div class="form-group">
                        <asp:CheckBox ID="chkEnable" runat="server" Text="Enable" Checked="false" data-toggle="toggle" />

                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6 ">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnadd" OnClick="btnadd_Click" Text="Integrated" CssClass="btn bg-primary btn-block btn-xs btnAddtavlIntegration" disabled="true" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-4 col-xs-6">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" OnClick="btnClear_Click" Text="Clear" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
            </div>

            
                <asp:GridView ID="gvdTavlIntegration" runat="server"  CssClass="table table-bordered table-striped table-responsive"  AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="TavlClient" HeaderText="Client #" />
                        <asp:BoundField DataField="TavlGroup" HeaderText="Group #"  />
                        <asp:BoundField DataField="TavlIP" HeaderText="IP" />
                        <asp:TemplateField HeaderText="Enable">
                            <ItemTemplate>
                                <asp:CheckBox ID="gvdcheckEnable" runat="server" Checked='<%# Eval("TavlStatus") %>' Enabled="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ObjectID") %>' OnCommand="linkbtnDel_Command" CssClass="btn btn-danger btn-xs"><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            
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
            <asp:AsyncPostBackTrigger ControlID="btnadd" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvdTavlIntegration" />
        </Triggers>

    </asp:UpdatePanel>

    <script>

        $(function () {
            staticMethod('Disable');
        });
        function staticMethod(_key) {
            if (_key == 'Enable') {
                $('.btnAddtavlIntegration').prop('disabled', false);
            }
            if (_key == 'Disable') {
                $('.btnAddtavlIntegration').prop('disabled', true);
            }
            enabledSubmit('.tavlIntegrationtxt', '.btnAddtavlIntegration');
            $('.ddlSelect').select2();
        }
    </script>
</asp:Content>
