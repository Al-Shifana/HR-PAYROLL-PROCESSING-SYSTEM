<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="CodesMasterListingTest.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.CodesMasterListingTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container outer-div">
        <div class="mt-2 mx-4 mr-4">
            <asp:GridView runat="server" ID="gvCodesMaster" PageSize="10" AllowPaging="true" OnPageIndexChanging="gvCodesMaster_PageIndexChanging"
                AutoGenerateColumns="false" class="grid-style table table-striped table-bordered table-hover" PagerStyle-HorizontalAlign="Center" HeaderStyle-CssClass="tbl-header" HeaderStyle-ForeColor="White"
                ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Data" OnRowEditing="gvCodesMaster_RowEditing" OnRowDataBound="gvCodesMaster_RowDataBound" OnRowDeleting="gvCodesMaster_RowDeleting" OnRowCommand="gvCodesMaster_RowCommand"
                OnSorting="gvCodesMaster_Sorting" AllowSorting="true" DataKeyNames="CM_CODE, CM_TYPE ">

                <Columns>
                    <asp:TemplateField HeaderText="CODE" SortExpression="CM_CODE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblgvCmcode" Text='<%#Eval("CM_CODE")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TYPE" SortExpression="CM_TYPE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblgvCmtype" Text='<%#Eval("CM_TYPE")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="CM_DESC">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblgvCmdesc" Text='<%#Eval("CM_DESC")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VALUE" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" SortExpression="CM_VALUE">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblgvCmValue" class="num-align" Text='<%#Eval("CM_VALUE")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACTION">
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
