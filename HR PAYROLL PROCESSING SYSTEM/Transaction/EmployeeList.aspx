<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.EmployeeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">
            
            <h2 style="text-align: center">Employee List</h2>
            <br />
            <div class="d-grid gap-2 d-flex justify-content-sm-end">
                <asp:Button ID="btnAdd" ButtonType="Button" runat="server" Text="Add" CommandName="ADD" OnClick="btnAdd_Click" class="btn btn-primary me-md-2" />
            </div>
            <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" DataKeyNames="EMP_NO" CssClass="grid-view"
                OnSorting="grid1_Sorting" AllowSorting="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging"
                Width="100%" class="grid-style table table-striped table-bordered table-hover" HeaderStyle-ForeColor="White" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found">
                <Columns>
                    <asp:TemplateField HeaderText="EMP ID"  SortExpression="EMP_NO">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("EMP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NAME" SortExpression="EMP_DOB">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpName" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DOB" SortExpression="EMP_DOB">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDOB" Text='<%# Eval("EMP_DOB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JOIN DATE" SortExpression="EMP_JOIN_DATE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblJoinDate" Text='<%# Eval("EMP_JOIN_DATE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SALARY" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" SortExpression="EMP_SALARY">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblSalary" Text='<%# Eval("EMP_SALARY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DEPARTMENT" SortExpression="EMP_DEPTNO">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDeptNo" Text='<%# Eval("EMP_DEPTNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="MANAGER" SortExpression="EMP_MGRNO">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblMgrNo" Text='<%# Eval("EMP_MGRNO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:TemplateField HeaderText="STATUS" SortExpression="EMP_STATUS">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("EMP_STATUS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ACTIVE" SortExpression="EMP_ACTIVE_YN">
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
        </div>
    </div>
</asp:Content>
