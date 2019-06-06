<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirmwareScheduling.aspx.cs" Inherits="TIOT_WEB.HardwareBasedScheduling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <h6 class="pageH"><i class="fa fa-cog fa-fw"></i><a href="Configuration.aspx">Configuration </a><i class="fa fa-caret-right  fa-fw" aria-hidden="true"></i>Firmware Scheduling </h6>
        </div>
    </div>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlclient" runat="server" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect" OnSelectedIndexChanged="ddlclient_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlgroup" runat="server" AutoPostBack="true" class="dropdown-toggle form-control ddl ddlSelect" OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlObject" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-2 col-md-3 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSchedule" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-2 col-xs-3">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlhours" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-2 col-xs-3">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlmins" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-2 col-xs-3">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlsecs" runat="server" class="dropdown-toggle form-control ddl ddlSelect">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-1 col-md-1 col-sm-6 col-xs-12">
                    <div class="form-group">
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-xs btn-primary " />
                    </div>
                </div>
             

                <div class="row">
                    <div class="col-sm-4">
                        <h4 class="text-center">Days </h4>
                        <ul class="list-group">
                            <li class="list-group-item"><span>Monday</span>

                                <div class="material-switch pull-right">
                                    <asp:CheckBox ID="ChkMonday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Tuesday</span>

                                <div class="material-switch pull-right">
                                        <asp:CheckBox ID="ChkTuesday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Wednesday</span>

                                <div class="material-switch pull-right">
                               <asp:CheckBox ID="ChkWednesday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Thursday</span>

                                <div class="material-switch pull-right">
                <asp:CheckBox ID="ChkThursday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Friday</span>

                                <div class="material-switch pull-right">
                             <asp:CheckBox ID="ChkFriday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Saturday</span>
                                <div class="material-switch pull-right">
                                        <asp:CheckBox ID="ChkSaturday" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>Sunday</span>

                                <div class="material-switch pull-right">
                                 <asp:CheckBox ID="ChkSunday" runat="server" />
                                </div>
                            </li>


                        </ul>
                        <asp:TextBox ID="txtcommd" runat="server" CssClass=" col-sm- 4 form-control ddl" Placeholder="Command"></asp:TextBox>
                    </div>
                    <div class=" col-sm-4 ">
                        <h4 class="text-center">ON Switches </h4>
                        <ul class="list-group">
                            <li class="list-group-item"><span>ON Switch 01</span>
                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase1" runat="server" id="onS1" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 02</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase2" runat="server"  id="onS2" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 03</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase3" runat="server"  id="onS3" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 04</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase4" runat="server"  id="onS4" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 05</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase5" runat="server"  id="onS5" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 06</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase6" runat="server"  id="onS6" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 07</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase7" runat="server"  id="onS7" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>ON Switch 08</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase8" runat="server"  id="onS8" />
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="col-sm-4">
                        <h4 class="text-center">OFF Switches</h4>
                        <ul class="list-group">

                            <li class="list-group-item"><span>OFF Switch 01</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase1" id="offS1" runat="server"  />
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 02</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase2" id="offS2" runat="server"  />
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 03</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase3" id="offS3" runat="server"  />
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 04</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase4" id="offS4" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 05</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase5" id="offS5" runat="server" />
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 06</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase6" id="offS6"  runat="server"/>
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 07</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase7" id="offS7"  runat="server"/>
                                </div>
                            </li>
                            <li class="list-group-item"><span>OFF Switch 08</span>

                                <div class="material-switch pull-right">
                                    <input type="radio" name="switchcase8" id="offS8"  runat="server"/>
                                </div>
                            </li>

                        </ul>
                    </div>

                </div>

                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="Update1">
                    <ProgressTemplate>
                        <div style="position: fixed; text-align: start; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; opacity: 0.7;">
                            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/Content/images/loading7.gif" AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed; top: 10%; left: 50%;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlclient" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlgroup" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            staticMethod();
        });

        function staticMethod() {
            $('.ddlSelect').select2();
        }

    $("#onS1").change(switchcase1);
        $("#offS1").change(switchcase1);
        function switchcase1() {
            if (document.getElementById("onS1").checked == true) {
                document.getElementById("onS1").value = 1;
                document.getElementById("offS1").value = 0;
            } else {
                document.getElementById("onS1").value = 0;
                document.getElementById("offS1").value = 1;
            }
        }


        $("#onS2").change(switchcase2);
        $("#offS2").change(switchcase2);
        function switchcase2() {
            if (document.getElementById("onS2").checked == true) {
                document.getElementById("onS2").value = 1;
                document.getElementById("offS2").value = 0;
            } else {
                document.getElementById("onS2").value = 0;
                document.getElementById("offS2").value = 1;
            }
        }


        $("#onS3").change(switchcase3);
        $("#offS3").change(switchcase3);
        function switchcase3() {
            if (document.getElementById("onS3").checked == true) {
                document.getElementById("onS3").value = 1;
                document.getElementById("offS3").value = 0;
            } else {
                document.getElementById("onS3").value = 0;
                document.getElementById("offS3").value = 1;
            }
        }


        $("#onS4").change(switchcase4);
        $("#offS4").change(switchcase4);
        function switchcase4() {
            if (document.getElementById("onS4").checked == true) {
                document.getElementById("onS4").value = 1;
                document.getElementById("offS4").value = 0;
            } else {
                document.getElementById("onS4").value = 0;
                document.getElementById("offS4").value = 1;
            }
        }


        $("#onS5").change(switchcase5);
        $("#offS5").change(switchcase5);
        function switchcase5() {
            if (document.getElementById("onS5").checked == true) {
                document.getElementById("onS5").value = 1;
                document.getElementById("offS5").value = 0;
            } else {
                document.getElementById("onS5").value = 0;
                document.getElementById("offS5").value = 1;
            }
        }


        $("#onS6").change(switchcase6);
        $("#offS6").change(switchcase6);
        function switchcase6() {
            if (document.getElementById("onS6").checked == true) {
                document.getElementById("onS6").value = 1;
                document.getElementById("offS6").value = 0;
            } else {
                document.getElementById("onS6").value = 0;
                document.getElementById("offS6").value = 1;
            }
        }


        $("#onS7").change(switchcase7);
        $("#offS7").change(switchcase7);
        function switchcase7() {
            if (document.getElementById("onS7").checked == true) {
                document.getElementById("onS7").value = 1;
                document.getElementById("offS7").value = 0;
            } else {
                document.getElementById("onS7").value = 0;
                document.getElementById("offS7").value = 1;
            }
        }

        $("#onS8").change(switchcase8);
        $("#offS8").change(switchcase8);
        function switchcase8() {
            if (document.getElementById("onS8").checked == true) {
                document.getElementById("onS8").value = 1;
                document.getElementById("offS8").value = 0;
            } else {
                document.getElementById("onS8").value = 0;
                document.getElementById("offS8").value = 1;
            }
        }
    </script>
</asp:Content>
