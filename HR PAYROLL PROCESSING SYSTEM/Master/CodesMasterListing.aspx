<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="CodesMasterListing.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.CodesMasterListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">
            
            <h2 style="text-align: center" HeaderStyle-ForeColor="White">Codes Master Listing</h2>
            <br />
            <div class="d-grid gap-2 d-flex justify-content-sm-end">
                <asp:Button ID="btnAdd" ButtonType="Button" runat="server" Text="Add" CommandName="ADD" class="btn btn-primary me-md-2" OnClick="btnAdd_Click" />
            </div>

            <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="false" DataKeyNames="CM_CODE,CM_TYPE"
                PageSize="10" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" OnRowDataBound="grid1_RowDataBound" CssClass="grid-view"
                OnRowDeleting="grid1_RowDeleting" EmptyDataText="No Records Found" OnSorting="grid1_Sorting" OnRowCommand="grid1_RowCommand"
                OnRowEditing="grid1_RowEditing" class="grid-style table table-striped table-bordered table-hover"
                PagerStyle-HorizontalAlign="Center" AllowSorting="true" HeaderStyle-CssClass="tbl-header" HeaderStyle-ForeColor="White">

                <Columns>
                    <asp:TemplateField HeaderText="Code"  SortExpression="CM_CODE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmCode" Text='<%# Eval("CM_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Type" SortExpression="CM_TYPE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmType" Text='<%# Eval("CM_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" SortExpression="CM_DESC">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmDesc" Text='<%# Eval("CM_DESC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Value" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" SortExpression="CM_VALUE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmValue" Text='<%# Eval("CM_VALUE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Active(Y/N)" SortExpression="CM_ACTIVE_YN" >
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("CM_ACTIVE_YN") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                             <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icons8-edit-40.png" ToolTip="Edit" Height="20px" Width="20px" />
                             <asp:ImageButton ID="imbDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/icons8-delete-100.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick='return confirm("Are you sure you want to delete?")' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
        </div>

    </div>


</asp:Content>
