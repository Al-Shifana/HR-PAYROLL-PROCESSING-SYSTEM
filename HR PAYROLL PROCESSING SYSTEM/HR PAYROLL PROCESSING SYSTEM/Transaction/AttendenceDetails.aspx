<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="AttendenceDetails.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.AttendenceDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">

            <h2 style="text-align: center" headerstyle-forecolor="White">Attendance Details</h2>
            <br />
            <div class="d-flex justify-content-center my-2 mb-2 column-gap-2">
                <div class="form-group">
                    <asp:Label ID="lblMonth" runat="server" class="form-label">Month</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlMonth" CssClass="form-select" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblYear" runat="server" class="form-label">Year</asp:Label>
                    <asp:DropDownList runat="server" ID="ddlYear" CssClass="form-select" OnSelectedIndexChanged="ddlMonthYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>

            </div>

            <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" DataKeyNames="EMP_NO" CssClass="grid-view" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" Width="100%" OnRowEditing="grid1_RowEditing">

                <Columns>
                    <asp:TemplateField HeaderText="Employee Id">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpId" Text='<%# Eval("EMP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblEmpName" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Present days" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPresentdays" Text='<%# Eval("ATT_DAYS_PRESENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Absent days" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAbsentdays" Text='<%# Eval("ATT_DAYS_ABSENT") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <span style='<%# (Eval("STATUS") as string) == "Y" ? "display:none;": "" %>'>
                                <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icons8-edit-40.png" ToolTip="Edit" Height="20px" Width="20px" />
                            </span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <br />
            <div class="d-grid gap-2 d-flex justify-content-sm-center">
                <asp:Button ID="btnPayRollProcessing" runat="server" Text="Process Attendance" OnClick="btnPayRollProcessing_Click" class="btn btn-primary me-md-2" />
            </div>
        </div>
    </div>
</asp:Content>
