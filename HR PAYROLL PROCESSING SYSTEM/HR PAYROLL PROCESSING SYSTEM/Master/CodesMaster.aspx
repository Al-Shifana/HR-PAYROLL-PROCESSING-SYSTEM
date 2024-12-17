<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="CodesMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.CodesMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.num-field').on('input', function () {
                $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                var val = $(this).val();
                var decimalCount = val.split('.').length - 1;
                if (decimalCount > 1) {
                    var lastIndex = val.lastIndexOf('.');
                    $(this).val(val.substring(0, lastIndex));
                }
            });
        });

    </script>
    <div class="container">
        <div class="outer-container-master">
            <div class="mb-3 container">
                <div class="d-grid gap-2 d-flex justify-content-sm-start">
                    <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back" class="btn btn-primary me-md-2" OnClick="btnBack_Click" />
                </div>
                <h2 style="text-align: center" headerstyle-forecolor="White">Codes Master</h2>
                <br />
                <div class="row" runat="server">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblCmCode" runat="server" class="form-label">Code<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtCmCode" runat="server" MaxLength="12" CssClass="form-control" class="form-control number-field num-field" ValidationGroup="valgrp"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv1"
                                ControlToValidate="txtCmCode"
                                ErrorMessage="Code is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblCmType" runat="server" class="form-label">Type<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtCmType" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv2"
                                ControlToValidate="txtCmType"
                                ErrorMessage="Type is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblCmValue" runat="server" class="form-label">Value</asp:Label>
                            <asp:TextBox ID="txtCmValue" MaxLength="9" runat="server" class="form-control"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-12">
                        <asp:Label ID="lblCmDesc" runat="server" class="form-label">Description</asp:Label>
                        <asp:TextBox ID="txtCmDesc" runat="server" MaxLength="240" ValidationGroup="valgrp" TextMode="MultiLine" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="rfv4"
                            ControlToValidate="txtCmDesc"
                            ErrorMessage="Description is required"
                            ForeColor="Red" runat="server"
                            ValidationGroup="valgrp"
                            CssClass="error-message">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="">
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
                        <asp:Label ID="lblActive" runat="server" Text="Active" for="cbActive" class="form-label"></asp:Label>
                        <%-- <asp:RequiredFieldValidator 
                                   ID="rfv5"
                                   ControlToValidate="chkActive"
                                   ErrorMessage="Value is required" 
                                   ForeColor="Red" runat="server"
                                   CssClass="error-message">
                  </asp:RequiredFieldValidator>--%>
                    </div>

                    <div class="d-grid gap-2 d-flex justify-content-sm-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="valgrp" class="btn btn-primary me-md-2" />
                        <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger" />--%>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
