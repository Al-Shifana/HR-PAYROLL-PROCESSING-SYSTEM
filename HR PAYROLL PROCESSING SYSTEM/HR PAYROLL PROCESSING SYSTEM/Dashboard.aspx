<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Master.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HR_PAYROLL_PROCESSING_SYSTEM.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet">
  <style>
      .outer-container-master {
          display: flex;
          justify-content: center;
          align-items: center;
          height: 100vh; 
      }

      .centered-heading {
          text-align: center;
          color: black;
          font-size: 2em; 
          font-family: 'Open Sans', sans-serif; 
      }

      @media (min-width: 768px) {
          .centered-heading {
              font-size: 3em;
          }
      }

      @media (min-width: 1024px) {
          .centered-heading {
              font-size: 4em;
          }
      }
  </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="outer-container-master">
        <div class="mb-3 container">
            <h2 class="centered-heading">Welcome to HRPayroll Processing System</h2>
        </div>
    </div>
</asp:Content>
