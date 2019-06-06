<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TIOT_WEB.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>TELTONIKA | Log in</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="shortcut icon" type="image/png" href="Images/teltonika2.png" />
    <link href="Content/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/css/AdminLTE.min.css" rel="stylesheet" />
    <link href="Content/css/blue.css" rel="stylesheet" />
<%--    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />--%>
      <link href="Content/css-plugins/jquery-ui.css" rel="stylesheet" />
        <link href="Content/static-plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
<%--    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />--%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/1.12.4/jquery.js"></script>--%>
        <script src="Content/js/jquery.min.js"></script>
        <script src="Content/js/jqueryui.js"></script>
<%--    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

</head>
<body class="hold-transition login-page" style="background-image: url('Images/iot3.jpg'); background-size: 100% 100%">
   <form id="Form2" runat="server">
       <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div class="login-box">

                <div class="login-box-body">
                    <div class="login-logo">
                        <img src="Images/logoM.png" style="width: 100%" />
                    </div>
                    <p class="login-box-msg" style="font-weight: bold">AUTOMATION</p>
                  
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                        <div class="form-group has-feedback">
                            <asp:TextBox runat="server" ID="txtemail" placeholder="&#xf2bd; Username" class="form-control ddl logintxt logintxtfeild" ></asp:TextBox>

                        </div>
                        <div class="form-group has-feedback">

                            <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" placeholder="&#xf084; Password" class="form-control ddl logintxt logintxtfeild"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:TextBox runat="server" ID="txtclientcode" placeholder="&#xf02a; Client Code" class="form-control ddl logintxtfeild"></asp:TextBox>
                        </div>

                        <div class="row">
                            <div class="col-xs-8">
                                <div class="alert alert-danger lblalertlogin" id="alertlbl">
                                </div>
                            </div>
                            <div class="col-xs-4 pull-right">
                                <asp:Button Text="Sign In" ID="btnLogin" class="btn btn-default btn-block btn-flat btnLogin" runat="server" OnClientClick="saveloginedTime()" OnClick="btnLogin_Click" disabled="true"></asp:Button>
                            </div>
                        </div>
                  
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnLogin" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
      </form>
    <script type="text/javascript">

        $(function () {
            enabledSubmit('.logintxt', '.btnLogin');
        });
        function saveloginedTime() {
            var logtime = new Date().toLocaleString();
            localStorage.setItem("LoginedTime", logtime);
            return false;
        }

        function alertshow(str) {
            $('#alertlbl').html(str);
            $('#alertlbl').show();
        }

        function enabledSubmit(element, button) {
            $(element).on('keyup', function () {
                var empty = true;
                $(element).each(function (i) {
                    if ($(this).val() == '') {
                        empty = true;
                        $(button).prop('disabled', true);
                        return false;

                    }
                    else {
                        empty = false;
                    }
                });
                if (!empty) $(button).prop('disabled', false);
            });
        }


    </script>
</body>
</html>
