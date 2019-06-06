<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjectMaintenance.aspx.cs" Inherits="TIOT_WEB.ObjectMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Device Maintenance  </h6>
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
                <div class="col-lg-2 col-md-4 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" OnSelectedIndexChanged="ddlObject_SelectedIndexChanged" runat="server" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtIssueComment" placeholder="Issue " runat="server" CssClass="form-control ddl objectMnttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtIssuedt" ClientIDMode="Static" runat="server" placeholder="Issue date *" CssClass="form-control ddl objectMnttxt"></asp:TextBox>

                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <asp:TextBox ID="txtIssueAuthor" runat="server" placeholder="Issue Author *" CssClass="form-control ddl objectMnttxt"></asp:TextBox>

                    </div>
                </div>

                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6 pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnClear" Text="Clear" OnClick="btnClear_Click" CssClass="btn bg-primary btn-block btn-xs" />
                    </div>
                </div>
                <div class="col-lg-1 col-md-2 col-sm-3 col-xs-6  pull-right">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnAddObjectMnt" Text="Save" OnClick="btnAddObjectMnt_Click" CssClass="btn bg-primary btn-block btn-xs btnObjectMnt" />
                    </div>
                </div>
            </div>

            <div class="row">
                <asp:GridView ID="gvdObjectMnt" runat="server" CssClass="table table-bordered table-striped gvdObjectMntClass" AutoGenerateColumns="false" EmptyDataText="No Record Found!">
                    <Columns>
                        <asp:BoundField DataField="IssueComments" HeaderText="Issue Comments" />
                        <asp:BoundField DataField="IssueAuthor" HeaderText="Issue Raised By" />
                        <asp:BoundField DataField="IssueDateTime" HeaderText="Issue Date" DataFormatString="{0:D}" HtmlEncode="false" />
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkbtnEdit" runat="server" CommandName="UpdateID" CommandArgument='<%# Eval("MainId") %>' OnCommand="linkbtnEdit_Command" CssClass='<%# Eval("cssClass") %>'><%# Eval("linkbtnText") %></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="modal fade" id="Resolve" role="dialog">
                <div class="modal-dialog modal-md ">

                    <!-- Modal content-->
                    <div class="modal-content modalTop">
                        <div class="modal-header tHeaderReslove">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="fa fa-times" aria-hidden="true"></span></button>
                            <h4 class="modal-title custom_align" id="Heading2">Resolve Issues</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtResolveComments" TextMode="MultiLine" MaxLength="200" Width="100%" Height="100" TabIndex="3" CssClass="form-control ddl objectMntResolvetxt" placeholder="Resolve Comments *"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtResolvePerson" CssClass="form-control ddl objectMntResolvetxt" placeholder="Resolve Person*"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-sm-6">

                                    <div class="form-group">
                                        <asp:TextBox runat="server" ID="txtResolveId" CssClass="form-control ddl objectMntResolvetxt"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                              <asp:LinkButton ID="btnResolved" runat="server" OnClick="btnResolved_Click"  CssClass="btn btn-success btn-sm btnResolvedClass"><span>Resolved</asp:LinkButton>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal fade" id="Resolved" role="dialog">
                <div class="modal-dialog modal-md ">

                    <!-- Modal content-->
                    <div class="modal-content modalTop">
                        <div class="modal-header tHeaderResloved">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true"><span class="fa fa-times" aria-hidden="true"></span></button>
                            <h4 class="modal-title custom_align" id="Heading3">Resolved Issues</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-sm-12">
                                    <p class="">
                                        Resolved DateTime:  
                                            <asp:Label runat="server" ID="lblResolvedDateTime" CssClass="lblbg" Width="100%"></asp:Label>
                                    </p>
                                </div>

                                <div class="col-sm-12">
                                    <p class="">
                                        Resolved Person:          
                                            <asp:Label runat="server" ID="lblResolvedPerson" CssClass="lblbg"  Width="100%"></asp:Label>
                                    </p>

                                </div>

                                <div class="col-sm-12">
                                    <p class="">
                                        Resolved Comments:
                                            <asp:Label runat="server" ID="lblResolvedComments" CssClass="lblbg" Width="100%"></asp:Label>
                                    </p>
                                </div>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--  <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Remove" OnCommand="linkbtnDel_Command" CssClass="btn btn-success btn-sm"><span><i class="fa fa-check fa-fw"></i></span></asp:LinkButton>--%>
                            <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal"><span class="fa-fw fa fa-times"></span></button>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>

        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlObject" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnAddObjectMnt" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvdObjectMnt" />
            <asp:AsyncPostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">

        $(function () {
            staticMethod();
        });

        function staticMethod() {
            $('.btnObjectMnt').prop('disabled', true);
            enabledSubmit('.objectMnttxt', '.btnObjectMnt');
            $('.ddlSelect').select2();
            $("#txtIssuedt").datepicker();
        }



        function openResolveModal() {
            $('#Resolve').modal('show')
        }

        function closeResolveModal() {
            $('#Resolve').modal('hide')
        }
        function openResolvedModal() {
            $('#Resolved').modal('show')
        }

        function closeResolvedModal() {
            $('#Resolved').modal('hide')
        }
    </script>

</asp:Content>
