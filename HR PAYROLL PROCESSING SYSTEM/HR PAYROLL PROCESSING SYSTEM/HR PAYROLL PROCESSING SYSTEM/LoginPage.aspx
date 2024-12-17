<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet"/>
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.4/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <style>
        .login-container {
            max-width: 400px;
            margin: 50px auto;
            padding: 20px;
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }
        .form-group {
            margin-bottom: 15px;
        }
        .btn-custom {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px 15px;
            border-radius: 4px;
            cursor: pointer;
        }
        .btn-custom:hover {
            background-color: #45a049;
        }
    </style>
</head> 
<body>
    <form id="form1" runat="server">
        <div class="container">
        <div class="login-container">
            <div class="form-group">
                <asp:Label runat="server" ID="lblUsername" CssClass="form-label">Username</asp:Label>
                <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lblPassword" CssClass="form-label">Password</asp:Label>
                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" CssClass="form-control"></asp:TextBox>
            </div>
            <div>
                <asp:Button runat="server" ID="btnLogin" Text="Login" CssClass="btn btn-custom btn-block" OnClick="btnLogin_Click"/>
            </div>
        </div>
    </div>

    </form>
</body>
</html>
