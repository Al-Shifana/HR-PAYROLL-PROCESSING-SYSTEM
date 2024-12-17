<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="DepartmentMasterListing.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.DepartmentMasterListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="outer-container-master">
    <div class="mb-3 container">
        
        <div class="container">
        <h2 style="text-align: center" >Department Master Listing</h2>
        <br />
         <div class="d-grid gap-2 d-flex justify-content-sm-end">
           <asp:Button ID="btnAdd" ButtonType="Button" runat="server" Text="Add" CommandName="ADD"   class="btn btn-primary me-md-2" OnClick="btnAdd_Click" />
         </div>
        <asp:GridView runat="server"
        ID="grid1"
        DataKeyNames="DEPT_NO"
        AutoGenerateColumns="False"
        CssClass="grid-view" 
        OnRowEditing="grid1_RowEditing"
        OnRowDeleting="grid1_RowDeleting"
        OnSorting="grid1_Sorting" AllowSorting="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" 
        Width="100%" class="grid-style table table-striped table-bordered table-hover" HeaderStyle-ForeColor="White">
      
          <Columns>           
                <asp:TemplateField HeaderText="Department No" SortExpression="DEPT_NO">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDeptNo" Text='<%# Eval("DEPT_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Name" SortExpression="DEPT_NAME">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDeptName" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                             <asp:ImageButton ID="imbEdit" runat="server" CommandName="Edit" ImageUrl="~/Images/icons8-edit-40.png" ToolTip="Edit" Height="20px" Width="20px" />
                             <asp:ImageButton ID="imbDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/icons8-delete-100.png" ToolTip="Delete" Height="20px" Width="20px" OnClientClick='return confirm("Are you sure you want to delete?")' />
                          <%--  <asp:Button ID="btnEdit" ButtonType="Button" runat="server" Text="Edit" CssClass="styled-button" OnClick="btnEdit_Click" />--%>
                        </ItemTemplate>
               </asp:TemplateField>

           <%-- <asp:TemplateField>
                  <ItemTemplate>
                  <asp:Button ID="btnEdit" ButtonType="Button" runat="server" Text="Edit" CommandName="View" CssClass="styled-button" OnClick="btnEdit_Click"/>
                  </ItemTemplate>
            </asp:TemplateField>
           <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowCancelButton="true" />--%>
          </Columns>       

       </asp:GridView>
    </div> 

    </div> 

   </div>

</asp:Content>
