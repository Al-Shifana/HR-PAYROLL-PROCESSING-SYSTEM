﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.EmployeeList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <h2 style="text-align:center">Employee List</h2>
            <br />
            <br />
    <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" DataKeyNames="EMP_NO" CssClass="grid-view" Width="100%">
    <%--OnRowEditing="grid1_RowEditing" OnRowUpdating="grid1_RowUpdating" OnRowCancelingEdit="grid1_RowCancelingEdit" OnRowDeleting="grid1_RowDeleting" --%>
          <Columns>           
                <asp:TemplateField HeaderText="EMP ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("EMP_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="NAME">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblEmpName" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="DOB">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDOB" Text='<%# Eval("EMP_DOB") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="JOIN_DATE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblJoinDate" Text='<%# Eval("EMP_JOIN_DATE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="SALARY">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSalary" Text='<%# Eval("EMP_SALARY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="DEPTNO">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDeptNo" Text='<%# Eval("EMP_DEPTNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="MGRNO">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblMgrNo" Text='<%# Eval("EMP_MGRNO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("EMP_STATUS") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="ACTIVE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EMP_ACTIVE_YN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField>
                  <ItemTemplate>
                  <asp:Button ID="btnView" ButtonType="Button" runat="server" Text="View" CommandName="View" CssClass="styled-button" OnClick="btnView_Click" />
                  </ItemTemplate>
                 </asp:TemplateField>
<%--                <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />   
                <asp:CommandField ShowDeleteButton="True" ShowCancelButton="true" ButtonType="Button" />--%>
          </Columns>       

       </asp:GridView>
</asp:Content>