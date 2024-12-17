<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="CodeMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.CodeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <div class="container">
        <h2 style="text-align:center">Codes Master</h2>
            <br />
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblCmCode" runat="server" class="form-label">Code<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtCmCode" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblCmType" runat="server" class="form-label">Type<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtCmType" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>
         
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblCmValue" runat="server" class="form-label">Value</asp:Label>
                   <asp:TextBox ID="txtCmValue" MaxLength="9" runat="server" class="form-control number-field"></asp:TextBox>
               </div>
           </div>
           <div class="col-12">
               <asp:Label ID="lblCmDesc" runat="server" class="form-label">Description</asp:Label>
               <asp:TextBox ID="txtCmDesc" runat="server" MaxLength="240" TextMode="MultiLine" class="form-control"></asp:TextBox>
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





    <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" CssClass="grid-view" OnRowEditing="grid1_RowEditing" OnRowUpdating="grid1_RowUpdating" OnRowCancelingEdit="grid1_RowCancelingEdit" OnRowDeleting="grid1_RowDeleting"  Width="100%">

          <Columns>           
                <asp:TemplateField HeaderText="CM_CODE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmCode" Text='<%# Eval("CM_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="CM_TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmType" Text='<%# Eval("CM_TYPE") %>'></asp:Label>
                    </ItemTemplate>

                    <%--<EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCmType"  Text='<%# Bind("CM_TYPE") %>'></asp:TextBox>
                    </EditItemTemplate>--%>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="CM_DESC">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmDesc" Text='<%# Eval("CM_DESC") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCmDesc"  Text='<%# Bind("CM_DESC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="CM_VALUE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblCmValue" Text='<%# Eval("CM_VALUE") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtCmValue"  Text='<%# Bind("CM_VALUE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="CM_ACTIVE_YN">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblActiveYN" Text='<%# Eval("CM_ACTIVE_YN") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="chkActive"  Text='<%# Bind("CM_ACTIVE_YN") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
              
                <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />   
                <asp:CommandField ShowDeleteButton="True" ShowCancelButton="true" ButtonType="Button" />
          </Columns>       

       </asp:GridView>
</asp:Content>



