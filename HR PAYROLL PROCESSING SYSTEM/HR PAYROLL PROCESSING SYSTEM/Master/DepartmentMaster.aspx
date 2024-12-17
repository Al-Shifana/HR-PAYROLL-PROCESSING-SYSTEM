<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="DepartmentMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.DepartmentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="outer-container-master">
      <div class="mb-3 container">
      <div class="d-grid gap-2 d-flex justify-content-sm-start">
           <asp:Button ID="btnBack" ButtonType="Button" runat="server" Text="Back" CommandName="Back"   class="btn btn-primary me-md-2" OnClick="btnBack_Click" />
       </div>
     <div class="container">
        <h2 style="text-align:center">Department Master</h2>
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblDeptNo" runat="server" class="form-label">Department No<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtDeptNo" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator 
                                   ID="rfv1"
                                   ControlToValidate="txtDeptNo"
                                   ErrorMessage="Department No is required" 
                                   ForeColor="Red" runat="server"
                                   ValidationGroup="valgrp"
                                   CssClass="error-message">
                  </asp:RequiredFieldValidator>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblDeptName" runat="server" class="form-label">Department Name<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtDeptName" runat="server" MaxLength="12" ValidationGroup="valgrp" class="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator 
                                   ID="rfv2"
                                   ControlToValidate="txtDeptName"
                                   ErrorMessage="Department Name is required" 
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
               <asp:Button ID="Save" runat="server" Text="Save" OnClick="Save_Click" ValidationGroup="valgrp" class="btn btn-primary me-md-2" />
              
           </div>
           <div class="">
             
           </div>
       </div>
   </div>

   </div>
 </div>
</asp:Content>
