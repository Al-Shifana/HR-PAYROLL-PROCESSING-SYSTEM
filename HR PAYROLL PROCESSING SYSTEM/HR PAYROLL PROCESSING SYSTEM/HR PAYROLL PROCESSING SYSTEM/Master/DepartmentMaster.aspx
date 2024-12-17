<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="DepartmentMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.DepartmentMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 style="text-align:center">Department Master</h2>
            <br />
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblDepatNo" runat="server" class="form-label">Department No<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtDeptNo" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblDeptName" runat="server" class="form-label">Department Name<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtDeptName" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="">
             
           </div>

            <br />
           <div class="d-grid gap-2 d-flex justify-content-sm-center">
               <asp:Button ID="Save" runat="server" Text="Save" OnClick="Save_Click" class="btn btn-primary me-md-2" />
               <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger"/>
           </div>
           <div class="">
             
           </div>
       </div>
   </div>
    <asp:GridView runat="server"
        ID="grid1"
        AutoGenerateColumns="False"
        CssClass="grid-view" 
        OnRowEditing="grid1_RowEditing"
        OnRowUpdating="grid1_RowUpdating"
        OnRowCancelingEdit="grid1_RowCancelingEdit"
        OnRowDeleting="grid1_RowDeleting"
        Width="100%">
      
          <Columns>           
                <asp:TemplateField HeaderText="DEPT_NO">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDeptNo" Text='<%# Eval("DEPT_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="DEPT_NAME">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblDeptName" Text='<%# Eval("DEPT_NAME") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtDeptName"  Text='<%# Bind("DEPT_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
          
                <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />   
               <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowCancelButton="true" />
          </Columns>       

       </asp:GridView>
</asp:Content>
