<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Transaction.History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">

            <h2 style="text-align: center" headerstyle-forecolor="White">PayRoll History</h2>
            <br />
            <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="false"
                PageSize="10" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" CssClass="grid-view"
                HeaderStyle-ForeColor="White">

                <Columns>
                    <asp:TemplateField HeaderText="Employee Id" SortExpression="EH_EMP_NO">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmCode" Text='<%# Eval("EH_EMP_NO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Designation">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmType" Text='<%# Eval("EH_DESIGNATION") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Grade">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmDesc" Text='<%# Eval("EH_GRADE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Basic">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCmValue" Text='<%# Eval("EH_BASIC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="HRA">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_HRA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="CONV">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_CONV") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="DA">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_DA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="TDS">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_TDS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="ESI">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_ESI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Action Type">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_ACTION_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <%--<asp:TemplateField HeaderText="SerailNo">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("EH_ACTION_SRL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                     
                </Columns>

            </asp:GridView>
        </div>
    </div>
</asp:Content>
