<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="PayRollProcessing.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.PayRollProcessing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">
            
            <h2 style="text-align: center">Payroll  Processing</h2>
            <br />
            <div class="d-flex justify-content-center my-2 mb-2 column-gap-2">
                <div class="form-group">
                    <asp:Label ID="lblMonth" runat="server" class="form-label">Month</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-select" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblYear" runat="server" class="form-label">Year</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-select" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>

            </div>
            <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" DataKeyNames="PR_EMP_NO" OnRowEditing="grid1_RowEditing" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" CssClass="grid-view" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found">
                <Columns>
                    <asp:TemplateField HeaderText="Employee Id">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("PR_EMP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpName" Text='<%# Eval("PR_EMP_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="YearMonth">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblYYYYMM" Text='<%# Eval("PR_YYYMM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDesg" Text='<%# Eval("PR_DESIGNATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Present Days" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPresent" Text='<%# Eval("PR_DAYS_PRESENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Absent Days" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAbsent" Text='<%# Eval("PR_DAYS_ABSENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="BASIC" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblBasic" Text='<%# Eval("PR_BASIC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="HRA" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblHra" Text='<%# Eval("PR_HRA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CONV" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblConv" Text='<%# Eval("PR_CONV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DA" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblDa" Text='<%# Eval("PR_DA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="TDS" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTds" Text='<%# Eval("PR_TDS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ESI" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEsi" Text='<%# Eval("PR_ESI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Earnings" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotEarnings" Text='<%# Eval("PR_TOT_EARNINGS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Total Deductions" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTotDedu" Text='<%# Eval("PR_TOT_DEDU") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Net Payable" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblNetPay" Text='<%# Eval("PR_NET_PAYABLE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/Report-PNG-Image.png" ToolTip="Edit" Height="20px" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <br />
            <div class="d-grid gap-2 d-flex justify-content-sm-center">
                <asp:Button ID="btnPayRollProcessing" runat="server" Text="Process Payroll" OnClick="btnPayRollProcessing_Click" class="btn btn-primary me-md-2" />
            </div>
        </div>
    </div>
</asp:Content>
