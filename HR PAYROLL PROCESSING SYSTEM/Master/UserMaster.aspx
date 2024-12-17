<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-12 container col-md-8" style="background-color: #f8f9fa; padding: 20px; border-radius: 10px;">
            <div class="d-grid gap-2 d-flex justify-content-sm-start">
                <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back" class="btn btn-primary me-md-2" OnClick="btnBack_Click1" />
            </div>
            <div class="container">
                <h2 style="text-align: center">User Master</h2>
                <br />
                <div class="row" runat="server">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblUserId" runat="server" class="form-label">User Id<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtUserId" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv1"
                                ControlToValidate="txtUserId"
                                ErrorMessage="User Id is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblUsername" runat="server" class="form-label">Username<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtUsername" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv2"
                                ControlToValidate="txtUsername"
                                ErrorMessage="Username is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblPswd" runat="server" class="form-label">Password</asp:Label>
                            <asp:TextBox ID="txtPswd" MaxLength="9" runat="server" Type="Password" ValidationGroup="valgrp" class="form-control number-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv3"
                                ControlToValidate="txtPswd"
                                ErrorMessage="Pswd is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblType" runat="server" class="form-label">Type</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlUserType" ValidationGroup="valgrp" CssClass="form-select"></asp:DropDownList>
                    </div>

                    <div class="">
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
                        <asp:Label ID="lblActive" runat="server" Text="Active" for="cbActive" class="form-label"></asp:Label>
                    </div>

                    <div class="d-grid gap-2 d-flex justify-content-sm-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="valgrp" class="btn btn-primary me-md-2" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
