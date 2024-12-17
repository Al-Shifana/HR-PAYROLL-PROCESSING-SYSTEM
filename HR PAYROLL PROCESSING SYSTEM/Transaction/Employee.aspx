<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.Employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $("#ContentPlaceHolder1_txtDOB").datepicker({
                showOn: 'button',
                buttonImage: "../Images/Calendar.png",
                buttonText: "Calendar",
                changeYear: true,
                changeMonth: true,
                minDate: "-65Y",
                maxDate: "-18Y",
                yearRange: "1958:2099",
                dateFormat: "dd/mm/yy",
            });

            var date = new Date();
            var firstDayOfMonth = new Date(date.getFullYear(), date.getMonth(), 1);
            $("#ContentPlaceHolder1_txtJoinDate").datepicker({
                showOn: 'button',
                buttonImage: "../Images/Calendar.png",
                buttonText: "Calendar",
                changeYear: true,
                changeMonth: true,
                minDate: "-1M",
                maxDate: "+1M",
                dateFormat: "dd/mm/yy",
            });

            $('.num-field').on('input', function () {
                $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                var val = $(this).val();
                var decimalCount = val.split('.').length - 1;
                if (decimalCount > 1) {
                    var lastIndex = val.lastIndexOf('.');
                    $(this).val(val.substring(0, lastIndex));
                }
            });


            $(function () {
                var a = $(".commasep");
                $.each(a, function () {
                    var parts = $(this).val().toString().split(".");
                    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    $(this).val(parts.join("."));
                });
            });


        });
        function CheckSalary() {
            var txtBasic = $('#ContentPlaceHolder1_txtBasic').val();
            var txtHRA = $('#ContentPlaceHolder1_txtHRA').val();
            var txtCONV = $('#ContentPlaceHolder1_txtCONV').val();
            var txtDA = $('#ContentPlaceHolder1_txtDA').val();
            var txtTDS = $('#ContentPlaceHolder1_txtTDS').val();
            var txtESI = $('#ContentPlaceHolder1_txtESI').val();

            let msg;

            if (parseFloat(txtHRA) > parseFloat(txtBasic)) {
                msg = 'HRA should be less than Basic Salary.';

                $('#ContentPlaceHolder1_txtHRA').val('');
            }

            if (parseFloat(txtCONV) > parseFloat(txtBasic)) {
                msg = 'CONV should be less than Basic Salary.';
                $('#ContentPlaceHolder1_txtCONV').val('');
            }

            if (parseFloat(txtDA) > parseFloat(txtBasic)) {
                msg = 'DA should be less than Basic Salary.';
                $('#ContentPlaceHolder1_txtDA').val('');
            }

            if (parseFloat(txtTDS) > parseFloat(txtBasic)) {
                msg = 'TDS should be less than Basic Salary.';
                $('#ContentPlaceHolder1_txtTDS').val('');
            }

            if (parseFloat(txtESI) > parseFloat(txtBasic)) {
                msg = 'ESI should be less than Basic Salary.';
                $('#ContentPlaceHolder1_txtESI').val('');
            }

            if (msg !== null && msg !== undefined && msg !== '') {
                ShowAlert(msg);
            }
        }

        function ShowAlert(msg) {
            Swal.fire({
                title: "Warning!",
                text: msg,
                icon: "warning"
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container col-md-8">

            <asp:ScriptManager runat="server"></asp:ScriptManager>
            <div class="container">
                <div class="d-grid gap-2 d-flex justify-content-sm-start">
                    <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back" class="btn btn-primary me-md-2" OnClick="btnBack_Click" />
                </div>
                <h2 style="text-align: center">Employee</h2>
                <br />

                <div class="row" runat="server">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblENo" runat="server" class="form-label">Employee Number<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtENo" runat="server" MaxLength="120" ReadOnly="true" class="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblEmpName" runat="server" class="form-label">Name<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtEmpName" runat="server" MaxLength="24" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv1"
                                ControlToValidate="txtEmpName"
                                ValidationGroup="valgrp"
                                ErrorMessage="Name is required"
                                ForeColor="Red" runat="server"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblDOB" runat="server" class="form-label">DOB</asp:Label>
                            <div class="inner-sec"></div>
                            <div class="inner-sec">
                                <asp:TextBox ID="txtDOB" runat="server" ValidationGroup="valgrp" class="form-control number-field"></asp:TextBox>
                            </div>

                            <asp:RequiredFieldValidator
                                ID="rfv2"
                                ControlToValidate="txtDOB"
                                ErrorMessage="DOB is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="valgrp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblJoinDate" runat="server" class="form-label">Join Date</asp:Label>
                        <div class="inner-sec"></div>
                        <div class="inner-sec">
                            <asp:TextBox ID="txtJoinDate" runat="server" ValidationGroup="valgrp" class="form-control number-field"></asp:TextBox>
                        </div>

                        <asp:RequiredFieldValidator
                            ID="rfv3"
                            ControlToValidate="txtJoinDate"
                            ErrorMessage="JoinDate is required"
                            ForeColor="Red" runat="server"
                            ValidationGroup="valgrp"
                            CssClass="error-message">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblsalary" runat="server" class="form-label">Salary</asp:Label>
                        <asp:TextBox ID="txtSalary" runat="server" ValidationGroup="valgrp" ReadOnly="true" MaxLength="240" class="form-control commasep"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator 
                                   ID="rfv4"
                                   ControlToValidate="txtSalary"
                                   ErrorMessage="Value is required" 
                                   ForeColor="Red" runat="server"
                                   ValidationGroup="valgrp"
                                   CssClass="error-message">
                  </asp:RequiredFieldValidator>--%>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblDept" runat="server" class="form-label">Department</asp:Label>
                        <%--<asp:TextBox ID="txtDept" runat="server" MaxLength="240" class="form-control"></asp:TextBox>--%>
                        <asp:DropDownList runat="server" ID="ddlDept" ValidationGroup="valgrp" CssClass="form-select"></asp:DropDownList>
                        <asp:RequiredFieldValidator
                            ID="rfv5"
                            ControlToValidate="ddlDept"
                            ErrorMessage="Department is required"
                            ForeColor="Red" runat="server"
                            ValidationGroup="valgrp"
                            CssClass="error-message">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblMgr" runat="server" class="form-label">Manager</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlMgr" ValidationGroup="valgrp" CssClass="form-select"></asp:DropDownList>
                        <%--<asp:TextBox ID="txtMgr" runat="server" MaxLength="240" class="form-control"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator
                            ID="rfv6"
                            ControlToValidate="ddlMgr"
                            ErrorMessage="Manager is required"
                            ForeColor="Red" runat="server"
                            ValidationGroup="valgrp"
                            CssClass="error-message">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblStatus" runat="server" class="form-label">Status</asp:Label>
                        <asp:DropDownList runat="server" ID="ddlStatus" ValidationGroup="valgrp" CssClass="form-select"></asp:DropDownList>
                        <%-- <asp:TextBox ID="txtStatus" runat="server" MaxLength="240" class="form-control"></asp:TextBox>--%>
                        <asp:RequiredFieldValidator
                            ID="rfv7"
                            ControlToValidate="ddlStatus"
                            ErrorMessage="Status is required"
                            ForeColor="Red" runat="server"
                            ValidationGroup="valgrp"
                            CssClass="error-message">
                        </asp:RequiredFieldValidator>
                    </div>

                    <%--<div class="col-md-4">
               <asp:Label ID="lblpswd" runat="server" class="form-label">Password</asp:Label>
               <asp:TextBox ID="txtPswd" runat="server" TextMode="Password" class="form-control"></asp:TextBox>
           </div>--%>


                    <div class="">
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
                        <asp:Label ID="lblActive" runat="server" Text="Active" for="cbActive" class="form-label"></asp:Label>
                    </div>

                    <div class="d-grid gap-2 d-flex justify-content-center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" ValidationGroup="valgrp" class="btn btn-primary me-md-2" />
                        <asp:Button ID="btnAllowance" runat="server" Text="Add Allowance" OnClick="btnAllowance_Click" Visible="false" class="btn btn-primary me-md-2" />
                    </div>

                </div>

                <%--<div class="container" id="container2">       --%>

                <div runat="server" id="container2" class="mt-3 border border-secondary rounded p-3">
                    <h2 style="text-align: center">Allowance</h2>
                    <div class="row" runat="server">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblDesignation" runat="server" class="form-label">Designation</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlDesignation" CssClass="form-select"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtDesignation" runat="server" MaxLength="12" class="form-control"></asp:TextBox>--%>
                                <%--<asp:RequiredFieldValidator 
                                   ID="RequiredFieldValidator1"
                                   ControlToValidate="txtEmpName"
                                   ErrorMessage="Value is required" 
                                   ForeColor="Red" runat="server"
                                   CssClass="error-message">
                  </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblGrade" runat="server" class="form-label">Grade</asp:Label>
                                <asp:DropDownList runat="server" ID="ddlGrade" CssClass="form-select"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtGrade"  runat="server" class="form-control number-field"></asp:TextBox>--%>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblBasic" runat="server" class="form-label">Basic</asp:Label>
                            <asp:TextBox ID="txtBasic" runat="server" class="form-control number-field num-field" MaxLength="12" ValidationGroup="vldgp" OnChange="return CheckSalary();"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv8"
                                ControlToValidate="txtBasic"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblHRA" runat="server" class="form-label">HRA</asp:Label>
                            <asp:TextBox ID="txtHRA" runat="server" MaxLength="240" ValidationGroup="vldgp" class="form-control  number-field num-field" OnChange="return CheckSalary();"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv9"
                                ControlToValidate="txtHRA"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblCONV" runat="server" class="form-label">CONV</asp:Label>
                            <asp:TextBox ID="txtCONV" runat="server" MaxLength="240" ValidationGroup="vldgp" OnChange="return CheckSalary();" class="form-control number-field num-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv10"
                                ControlToValidate="txtCONV"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>

                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblDA" runat="server" class="form-label">DA</asp:Label>
                            <asp:TextBox ID="txtDA" runat="server" MaxLength="240" ValidationGroup="vldgp" OnChange="return CheckSalary();" class="form-control number-field num-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv11"
                                ControlToValidate="txtDA"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblTDS" runat="server" class="form-label">TDS</asp:Label>
                            <asp:TextBox ID="txtTDS" runat="server" MaxLength="240" ValidationGroup="vldgp" OnChange="return CheckSalary();" class="form-control  number-field num-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv12"
                                ControlToValidate="txtTDS"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-3">
                            <asp:Label ID="lblESI" runat="server" class="form-label">ESI</asp:Label>
                            <asp:TextBox ID="txtESI" runat="server" MaxLength="240" ValidationGroup="vldgp" OnChange="return CheckSalary();" class="form-control  number-field num-field"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="rfv13"
                                ControlToValidate="txtESI"
                                ErrorMessage="Value is required"
                                ForeColor="Red" runat="server"
                                ValidationGroup="vldgp"
                                CssClass="error-message">
                            </asp:RequiredFieldValidator>
                        </div>

                        <div class="col-md-3">
                        </div>

                        <div class="d-grid gap-2 d-flex justify-content-sm-center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="Submit_Click" ValidationGroup="vldgp" class="btn btn-success me-md-2" />
                        </div>

                    </div>
                </div>

                <div runat="server" id="Div2" class="mt-5">
                    <div class="row" runat="server">
                        <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" CssClass="grid-view" DataKeyNames="EH_EMP_NO" Width="100%">
                            <Columns>
                                <%--<asp:TemplateField HeaderText="Emp NO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label1" Text='<%# Eval("EH_EMP_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label2" Text='<%# Eval("EH_DESIGNATION") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Grade">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label3" Text='<%# Eval("EH_GRADE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Basic">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label4" Text='<%# Eval("EH_BASIC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="HRA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label5" Text='<%# Eval("EH_HRA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CONV">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label6" Text='<%# Eval("EH_CONV") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="DA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label7" Text='<%# Eval("EH_DA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="TDS">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label8" Text='<%# Eval("EH_TDS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ESI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="Label9" Text='<%# Eval("EH_ESI") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnEdit" ButtonType="Button" runat="server" Text="Edit" CssClass="styled-button" OnClick="btnEdit_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />--%>
                            </Columns>

                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--   </div>--%>
        </div>

    </div>

</asp:Content>
