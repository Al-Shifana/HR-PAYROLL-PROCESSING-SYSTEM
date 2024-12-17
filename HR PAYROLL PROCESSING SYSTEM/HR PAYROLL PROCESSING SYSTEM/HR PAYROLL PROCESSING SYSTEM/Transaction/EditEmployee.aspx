<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="EditEmployee.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.EditEmployee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
        <h2 style="text-align:center">Add Employee</h2>
            <br />
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblEmpNo" runat="server" class="form-label" >Emp No<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtEmpNo" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblEmpName" runat="server" class="form-label">Name<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtEmpName" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>
         
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblDOB" runat="server" class="form-label">DOB</asp:Label>
                   <asp:TextBox ID="txtDOB" MaxLength="9" runat="server" TextMode="Date" class="form-control number-field"></asp:TextBox>
               </div>
           </div>
           <div class="col-md-4">
               <asp:Label ID="lblJoinDate" runat="server" class="form-label">Join Date</asp:Label>
               <asp:TextBox ID="txtJoinDate" runat="server" MaxLength="240" TextMode="Date" class="form-control"></asp:TextBox>
           </div>

           <div class="col-md-4">
               <asp:Label ID="lblsalary" runat="server" class="form-label" >Salary</asp:Label>
               <asp:TextBox ID="txtSalary" runat="server" MaxLength="240" class="form-control"></asp:TextBox>
           </div>

           <div class="col-md-4">
               <asp:Label ID="lblDept" runat="server" class="form-label">Department</asp:Label>
               <asp:TextBox ID="txtDept" runat="server" MaxLength="240" class="form-control"></asp:TextBox>
           </div>

           <div class="col-md-4">
               <asp:Label ID="lblMgr" runat="server" class="form-label">Manager</asp:Label>
               <asp:TextBox ID="txtMgr" runat="server" MaxLength="240" class="form-control"></asp:TextBox>
           </div>

           <div class="col-md-4">
               <asp:Label ID="lblStatus" runat="server" class="form-label">Status</asp:Label>
               <asp:TextBox ID="txtStatus" runat="server" MaxLength="240" class="form-control"></asp:TextBox>
           </div> 

           <div class="">
               <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
               <asp:Label ID="lblActive" runat="server" Text="Active" for="cbActive" class="form-label"></asp:Label>
           </div>

           <div class="d-grid gap-2 d-flex justify-content-sm-center">
               <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" class="btn btn-primary me-md-2" />
               <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger"/>
           </div>

       </div>
   </div>

</asp:Content>
