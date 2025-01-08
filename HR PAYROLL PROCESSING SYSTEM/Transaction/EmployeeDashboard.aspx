<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Transaction.Master" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.EmployeeDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div class="container">
        <div class="outer-container-master">
            <div class="mb-3 container">

                <h2 style="text-align: center">Employee Profile</h2>
                <br />
                <br />
                <div class="row" runat="server">
                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblEmpNo" runat="server" class="form-label">Emp No<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtEmpNo" runat="server" MaxLength="12" ReadOnly="true" class="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblEmpName" runat="server" class="form-label">Name<span><sup> &#42;</sup></span></asp:Label>
                            <asp:TextBox ID="txtEmpName" runat="server" MaxLength="12" ReadOnly="true" class="form-control"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblDOB" runat="server" class="form-label">DOB</asp:Label>
                            <asp:TextBox ID="txtDOB" runat="server" ReadOnly="true" class="form-control number-field"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <asp:Label ID="lblJoinDate" runat="server" class="form-label">Join Date</asp:Label>
                        <asp:TextBox ID="txtJoinDate" runat="server" ReadOnly="true" class="form-control"></asp:TextBox>
                    </div>

                    <%-- <div class="col-md-4">
                        <asp:Label ID="lblsalary" runat="server" class="form-label">Salary</asp:Label>
                        <asp:TextBox ID="txtSalary" runat="server" ReadOnly="true" MaxLength="240" class="form-control"></asp:TextBox>
                    </div>--%>

                    <div class="col-md-4">
                        <asp:Label ID="lblDept" runat="server" class="form-label">Department</asp:Label>
                        <asp:TextBox ID="ddlDept" runat="server" ReadOnly="true" MaxLength="24" class="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblMgr" runat="server" class="form-label">Manager</asp:Label>
                        <asp:TextBox ID="ddlMgr" runat="server" ReadOnly="true" MaxLength="24" class="form-control"></asp:TextBox>
                    </div>

                    <div class="col-md-4">
                        <asp:Label ID="lblStatus" runat="server" class="form-label">Status</asp:Label>
                        <asp:TextBox ID="ddlStatus" runat="server" ReadOnly="true" MaxLength="24" class="form-control"></asp:TextBox>
                    </div>

                    <div class="">
                        <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
                        <asp:Label ID="lblActive" runat="server" ReadOnly="true" Text="Active" for="cbActive" class="form-label"></asp:Label>
                    </div>

                    <div class="d-grid gap-2 d-flex justify-content-sm-center">
                        <asp:Button ID="btnChangePswd"
                            runat="server" Text="Change Password"
                            OnClientClick="$('#ChangePswd').modal('show'); return false;" Visible="true"
                            CssClass="btn btn-primary me-md-2" />

                        <div class="modal fade" id="ChangePswd" tabindex="-1" role="dialog" aria-labelledby="lblChangePswd" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="lblChangePswd">Change Password</h5>
                                        <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                                            <label for="lblCurrentPswd">Current Password:</label>
                                            <asp:TextBox ID="txtCurrentPswd" runat="server" Type="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvEstimateCurrency" runat="server" ControlToValidate="txtCurrentPswd"
                                                ErrorMessage="Current Password is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="add" />
                                        </div>
                                        <div class="form-group">
                                            <label for="lblNewPswd">New Password:</label>
                                            <asp:TextBox ID="txtNewPswd" runat="server" Type="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="rfvEstimateFcAmount" runat="server" ControlToValidate="txtNewPswd"
                                                ErrorMessage="New Password is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="add" />
                                            <label id="lblNewPswdError" class="text-danger" style="display: none;"></label>
                                        </div>
                                        <div class="form-group">
                                            <label for="lblConfirmPswd">Confirm Password:</label>
                                            <asp:TextBox ID="txtConfirmPswd" runat="server" Type="Password" CssClass="form-control" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                CompareToValidate="txtConfirmPswd" ControlToValidate="txtConfirmPswd"
                                                ErrorMessage="Confirm Password is required." CssClass="text-danger" Display="Dynamic" ValidationGroup="add" />
                                            <label id="lblConfirmPswd" class="text-danger" style="display: none;"></label>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success"
                                            OnClick="btnSubmit_Click" ValidationGroup="add" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                    </div>
                    <br />
                    <div class="d-grid gap-2 d-flex justify-content-sm-center">
                        <asp:Button ID="btnPyslip" runat="server" Text="Payslip generation" OnClick="btnPyslip_Click" class="btn btn-primary me-md-2" />
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
