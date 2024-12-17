<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.ErrorCodeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-12 container col-md-8" style="background-color: #f8f9fa; padding: 20px; border-radius: 10px;">
            <div class="d-grid gap-2 d-flex justify-content-sm-start">
                <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back" class="btn btn-primary me-md-2" OnClick="btnBack_Click" />
            </div>
            <div class="container">
                <h2 style="text-align: center">Error Code Master</h2>
                <br />
                <div class="row" runat="server">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblErrCode" runat="server" class="form-label">Error Code<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtErrCode" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv1"
                                ControlToValidate="txtErrCode"
                                ErrorMessage="Code is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblErrType" runat="server" class="form-label">Type<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtErrType" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv2"
                                ControlToValidate="txtErrType"
                                ErrorMessage="Type is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblErrDesc" runat="server" class="form-label">Description</asp:Label>
                            <asp:TextBox ID="txtErrDesc" MaxLength="240" runat="server" ValidationGroup="valgrp" class="form-control number-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv3"
                                ControlToValidate="txtErrDesc"
                                ErrorMessage="Description is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="">
                    </div>

                    <br />
                    <div class="d-grid gap-2 d-flex justify-content-sm-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="valgrp" class="btn btn-primary me-md-2" />
                        <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger"/>--%>
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>
