<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.UserMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
         <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 style="text-align:center">User Master</h2>
            <br />
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblUserId" runat="server" class="form-label">User Id<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtUserId" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblUsername" runat="server" class="form-label">Username<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtUsername" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>
         
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblPswd" runat="server" class="form-label">Password</asp:Label>
                   <asp:TextBox ID="txtPswd" MaxLength="9" runat="server" class="form-control number-field"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <asp:Label ID="lblType" runat="server" class="form-label">Type</asp:Label>
               <asp:TextBox ID="txtType" runat="server" MaxLength="240"  class="form-control"></asp:TextBox>
           </div>

           <div class="">
               <asp:CheckBox ID="chkActive" runat="server" Checked="true" class="chk-style" />
               <asp:Label ID="lblActive" runat="server" Text="Active" for="cbActive" class="form-label"></asp:Label>
           </div>

           <div class="d-grid gap-2 d-flex justify-content-sm-center">
               <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary me-md-2" />
               <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger"/>
           </div>
       </div>
   </div>
   

    <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" CssClass="grid-view" OnRowEditing="grid1_RowEditing" OnRowUpdating="grid1_RowUpdating" OnRowCancelingEdit="grid1_RowCancelingEdit" OnRowDeleting="grid1_RowDeleting" Width="100%">

          <Columns>           
                <asp:TemplateField HeaderText="USER_ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUserId" Text='<%# Eval("USER_ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="USER_NAME">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblUsername" Text='<%# Eval("USER_NAME") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtUsername"  Text='<%# Bind("USER_NAME") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="USER_PASSWORD">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblPswd"  TextMode="Password" Text='<%# Eval("USER_PASSWORD") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtPswd"  Text='<%# Bind("USER_PASSWORD") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="USER_TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblType" Text='<%# Eval("USER_TYPE") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtType"  Text='<%# Bind("USER_TYPE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="USER_ACTIVE_YN">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblActive" Text='<%# Eval("USER_ACTIVE_YN") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="chkActive"  Text='<%# Bind("USER_ACTIVE_YN") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
              
                 <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />   
                <asp:CommandField ShowDeleteButton="True" ShowCancelButton="true" ButtonType="Button" />
          </Columns>       

       </asp:GridView>
</asp:Content>
