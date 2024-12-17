<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMasterListing.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.ErrorCodeMasterListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
      <div class="mb-3 container">
          
     <div class="container">
        <h2 style="text-align:center">Error Code Listing</h2>
            <br />
         <div class="d-grid gap-2 d-flex justify-content-sm-end">
            <asp:Button ID="btnAdd" ButtonType="Button" runat="server" Text="Add" CommandName="ADD" OnClick="btnAdd_Click" class="btn btn-primary me-md-2" />
        </div>
     <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" DataKeyNames="ERR_CODE" OnRowDeleting="grid1_RowDeleting" OnRowEditing="grid1_RowEditing"
         OnSorting="grid1_Sorting" AllowSorting="true" PageSize="5" AllowPaging="true" OnPageIndexChanging="grid1_PageIndexChanging" 
         CssClass="grid-view" Width="100%" class="grid-style table table-striped table-bordered table-hover" HeaderStyle-ForeColor="White">
         
          <Columns>           
                <asp:TemplateField HeaderText="Code"  SortExpression="ERR_CODE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrCode" Text='<%# Eval("ERR_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Type" SortExpression="ERR_TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrType" Text='<%# Eval("ERR_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="Description" SortExpression="ERR_DESC">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrDesc" Text='<%# Eval("ERR_DESC") %>'></asp:Label>
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
             </div>
</asp:Content>
