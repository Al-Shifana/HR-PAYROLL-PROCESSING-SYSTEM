<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.EmployeeAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">
            <div class="d-grid gap-2 d-flex justify-content-sm-start">
                <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back" class="btn btn-primary me-md-2" OnClick="btnBack_Click" CausesValidation="false" />
            </div>
            <h2 style="text-align: center">Attendence Details</h2>
            <br />

            <div class="d-flex justify-content-center my-2 mb-2 column-gap-2">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" class="form-label">Employee Id</asp:Label>
                    <br />
                    <asp:TextBox ID="txtEmpId" runat="server" MaxLength="12" ReadOnly="true" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblTotal" runat="server" class="form-label">Total Days</asp:Label>
                    <asp:TextBox ID="txtTotal" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPresent" runat="server" class="form-label">Present Days</asp:Label>
                    <asp:TextBox ID="txtPresentDays" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAbsentDays" runat="server" class="form-label">Absent Days</asp:Label>
                    <asp:TextBox ID="txtAbsentDays" runat="server" MaxLength="12" class="form-control" OnTextChanged="txtAbsentDays_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAbsentDays" runat="server" ErrorMessage="Absent Days is required" ControlToValidate="txtAbsentDays" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>


                <br />
            </div>
            <div class="d-grid gap-2 d-flex justify-content-sm-center">
                <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" class="btn btn-success me-md-2" />
            </div>
        </div>
    </div>
</asp:Content>
