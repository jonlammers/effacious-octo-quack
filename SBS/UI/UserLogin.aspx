﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="UI.UserLogin" %>

<%@ Register TagPrefix="OTP" TagName="OTPBar" Src="OTPControl.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SBS Login</title>
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="css/Custom.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <style>
        .hashPasswordClass { display: none; }
        .spacerRow {
            height: 10px;
        }

        .spacerRowBig {
            height: 70px;
        }

        #inner {
            width: 35%;
            margin: 0 auto;
        }
    </style>
</head>
<body>

    <div>
        <%--class="container"--%>
        <div id="inner">
            <form runat="server" class="form-signin">
                <div style="text-align: right; margin: auto">

                    <table>
                        <tr class="spacerRowBig"></tr>
                        <tr>
                            <td colspan="2">
                                <h2 style="text-align: center">Welcome to <b>SBS !</b></h2>
                                <h4 style="text-align: center; color: gray">The most secure bank.</h4>
                            </td>
                        </tr>
                        <%--<tr class="spacerRowBig"></tr>--%>
                        <tr>
                            <td style="text-align: center">
                                <img style="margin-top: 10px" src="images/logo.png" />
                            </td>
                        </tr>
                        <tr class="spacerRow"></tr>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon" id="basic-addon">Username</span>
                                    <asp:TextBox runat="server" class="form-control" ID="UserNameTextBox" aria-describedby="basic-addon3" autofocus></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr class="spacerRow"></tr>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <span class="input-group-addon" id="basic-addon3">Password&nbsp;</span>
                                    <asp:TextBox runat="server" class="form-control" ID="PasswordTextBox" aria-describedby="basic-addon3" TextMode="Password"></asp:TextBox>
                                    <asp:HiddenField ID="hashPasswordHiddenField" runat="server" />
                                </div>
                            </td>
                        </tr>
                        <tr class="spacerRow"></tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:LinkButton runat="server" ID="ForgotPasswordLink" Text="Forgot password" OnClick="ForgotPasswordLink_Click"></asp:LinkButton>
                                <asp:Button runat="server" ID="LoginButton" type="submit" class="btn btn-default" Text="Sign In" OnClick="LoginButton_Click"></asp:Button>
                                <br />
                                <br />
                                <p>Don't have an account? <a href="UserRegistration.aspx">Request</a> for one now.</p>
                            </td>
                        </tr>
                    </table>

                </div>
            </form>
        </div>
    </div>

    <script>
        $('#LoginButton').click(function () {
            //alert(hashCode($('#PasswordTextBox').val()));
            $('#hashPasswordHiddenField').val(hashCode($('#PasswordTextBox').val()));
        });

        function hashCode(str) {
            var hash = 0;
            if (str.length == 0) return hash;
            for (i = 0; i < str.length; i++) {
                char = str.charCodeAt(i);
                hash = ((hash << 5) - hash) + char;
                hash = hash & hash; // Convert to 32bit integer
            }
            return hash;
        }
    </script>

</body>
</html>
