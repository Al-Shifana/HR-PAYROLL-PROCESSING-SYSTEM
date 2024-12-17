<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="ErrorCodeMaster.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Master.ErrorCodeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="/Stylesheet/grid.css">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h2 style="text-align:center">Error Code Master</h2>
            <br />
            <br />
       <div class="row" runat="server">
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblErrCode" runat="server" class="form-label">Error Code<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtErrCode" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>

           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblErrType" runat="server" class="form-label">Type<span><sup> &#42;</sup></span></asp:Label>
                   <asp:TextBox ID="txtErrType" runat="server" MaxLength="12" class="form-control"></asp:TextBox>
               </div>
           </div>
         
           <div class="col-md-4">
               <div class="form-group">
                   <asp:Label ID="lblErrDesc" runat="server" class="form-label">Description</asp:Label>
                   <asp:TextBox ID="txtErrDesc" MaxLength="9" runat="server" class="form-control number-field"></asp:TextBox>
               </div>
           </div>

           <div class="">
              
           </div>

           <br />
           <div class="d-grid gap-2 d-flex justify-content-sm-center">
               <asp:Button ID="Button1" runat="server" Text="Save" OnClick="btnSave_Click" class="btn btn-primary me-md-2" />
               <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" class="btn btn-danger"/>
           </div>
       </div>

    <asp:GridView runat="server" ID="grid1" AutoGenerateColumns="False" CssClass="grid-view" Width="100%">
        <%--OnRowEditing="grid1_RowEditing" OnRowUpdating="grid1_RowUpdating" OnRowCancelingEdit="grid1_RowCancelingEdit" --%>
          <Columns>           
                <asp:TemplateField HeaderText="ERR_CODE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrCode" Text='<%# Eval("ERR_CODE") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="ERR_TYPE">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrType" Text='<%# Eval("ERR_TYPE") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtErrType"  Text='<%# Bind("ERR_TYPE") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              <asp:TemplateField HeaderText="ERR_DESC">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblErrDesc" Text='<%# Eval("ERR_DESC") %>'></asp:Label>
                    </ItemTemplate>

                    <EditItemTemplate>
                        <asp:TextBox runat="server" ID="txtErrDesc"  Text='<%# Bind("ERR_DESC") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

              
             <%--   <asp:CommandField ButtonType="Button" ShowEditButton="True" ShowCancelButton="true" />   
               <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowCancelButton="true" />--%>
          </Columns>       

       </asp:GridView>
</asp:Content>
