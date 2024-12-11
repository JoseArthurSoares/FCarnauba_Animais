<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="EditaMatriz.aspx.cs" Inherits="FCarnauba_Animais.EditaMatriz" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlMatrizes.ascx" TagName="UserControlMatrizes"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type ="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
<script type ="text/javascript" src="Scripts/jquery.maskedinput.js"></script>
<script type ="text/javascript" src="Scripts/autonumeric.js"></script>
    <uc3:UserControlMatrizes ID="UserControlMatrizes" runat="server" EditMode="true" />
</asp:Content>
