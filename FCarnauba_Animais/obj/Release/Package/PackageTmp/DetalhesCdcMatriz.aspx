<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCDC.Master" AutoEventWireup="true" CodeBehind="DetalhesCdcMatriz.aspx.cs" Inherits="FCarnauba_Animais.DetalhesCdcMatriz" AspCompat="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="./Styles/lightbox.css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 212px;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<fieldset class="cadastrar_info">
        <legend> <font face="Verdana,Arial,sans-serif" color="black"><b>Matriz&nbsp;&nbsp;</b></font></legend>
        <div><asp:ImageButton ID="EditImageButton" runat="server" ImageAlign="Right" 
                    ImageUrl="./img/edit_icon.gif" onclick="EditImageButton_Click" /></div><br>
        <table  width="100%">
        <tr>
            <td width="20%" class="rotulo">
                Matriz:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblMatriz" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Cio:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCio" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Cobertura Efetiva?
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCoberturaEfetiva" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    </fieldset>
</asp:Content>
