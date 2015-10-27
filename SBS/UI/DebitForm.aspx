﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="DebitForm.aspx.cs" Inherits="UI.DebitForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Withdraw Money</h3>

    <br />
    <br />
    <div runat="server" id="FromCustomerDiv" class="input-group">
        <span class="input-group-addon">&nbsp;From Customer</span>
        <asp:DropDownList ID="CustomerDropDown" runat="server" Width="250px" Height="35px" CssClass="form-control"
            OnSelectedIndexChanged="CustomerDropDown_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>

    <br />
    <div class="input-group">
        <span class="input-group-addon">&nbsp;&nbsp;&nbsp;&nbsp;From Account</span>
        <asp:DropDownList ID="FromDropdown" runat="server" Width="250px" Height="35px" CssClass="form-control"></asp:DropDownList>
    </div>

    <br />
    <div class="input-group">
        <span class="input-group-addon">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount ($)</span>
        <asp:TextBox ID="Amount" runat="server" class="form-control" Width="250px" TextMode="Number"></asp:TextBox>
    </div>

    <br />
    <br />

    <asp:Button ID="DebitButton" runat="server" Text="Debit Amount" OnClick="DebitButton_Click" CssClass="btn btn-default" />

    <br />
    <br />
    <div runat="server" id="OTPDiv" visible="false">
        <p>To proceed please enter the OTP that was sent to your registered Email ID</p>
        <div class="input-group" style="width: 300px">
            <span class="input-group-addon" id="basic-addon3">OTP Secret</span>
            <asp:TextBox runat="server" type="text" class="form-control" ID="OTPTextBox" aria-describedby="basic-addon3"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="VerifyButton" runat="server" class="btn btn-default" Text="Verify" OnClick="VerifyButton_Click"></asp:Button>
            </span>
        </div>
        <div style="text-align: left">
            &nbsp;&nbsp;<asp:LinkButton ID="ResendOTPLink" runat="server" Text="Resend OTP" OnClick="ResendOTPLink_Click"></asp:LinkButton>
        </div>
    </div>

</asp:Content>
