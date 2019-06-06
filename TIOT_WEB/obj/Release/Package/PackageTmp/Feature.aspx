<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Feature.aspx.cs" Inherits="TIOT_WEB.Feature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx"> Configuration </a> <i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>  Features </h6>
    </div>
        <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-lg-2 col-md-6 col-sm-6 col-xs-12">
            <div class="form-group">
                <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged" CssClass="form-control ddl"  AutoPostBack="true">
                    <asp:ListItem Text="Select Type"></asp:ListItem>
                    <asp:ListItem Text="Configuration"></asp:ListItem>
                    <asp:ListItem Text="Reports"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

    </div>
    <div class="row">
        
            <asp:GridView ID="gvdFeatures" runat="server" CssClass="table table-bordered table-striped gvdFeatureClass"  AutoGenerateColumns="false" EmptyDataText="No Record Found!" Visible="false">
                <Columns>
                    <asp:BoundField DataField="Link" HeaderText="Link"/>
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                       <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:TextBox ID="txtcssclass" Text='<%# Eval("Class") %>' CssClass="time form-control" runat="server"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <div class="form-group" style="margin: 0">
                                    <asp:CheckBox ID="chkstatus" Checked='<%#bool.Parse(Eval("EnableORDisable").ToString()) %>' runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("FeatureID") %>' OnCommand="linkbtnEdit_Command" CssClass="btn btn-primary btn-xs"><span><i class="fa fa-edit fa-fw"></i></span></asp:LinkButton>
                            <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="RemoveID" CommandArgument='<%# Eval("FeatureID") %>' OnCommand="linkbtnDel_Command" CssClass="btn btn-danger btn-xs" ><span><i class="fa fa-trash fa-fw"></i></span></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        
    </div>
             <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Update1">
                <ProgressTemplate>
                      <div class="loading">
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlType" EventName="SelectedIndexChanged" />
             <asp:AsyncPostBackTrigger ControlID="gvdFeatures" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
