<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalhesMensuracaoAnimal.aspx.cs" Inherits="FCarnauba_Animais.DetalhesMensuracaoAnimal" AspCompat="true" %>
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
        <legend> <font face="Verdana,Arial,sans-serif" color="black"><b>Mensuração&nbsp;&nbsp;</b></font></legend>
        <div><asp:ImageButton ID="EditImageButton" runat="server" ImageAlign="Right" 
                    ImageUrl="./img/edit_icon.gif" onclick="EditImageButton_Click" /></div><br>
        <table  width="100%">
        <tr>
            <td width="20%" class="rotulo">
                Data:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblDataPesagem" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Regime Alimentar:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblRegimeAlimentar" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td width="20%" class="rotulo">
                Peso Kg:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblPeso" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                C.E. (C. Escrotal) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCEscrotal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                A.A. (A. Anterior) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblAAnterior" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                A.P. (A. Posterior) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblAPosterior" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                L.G. (L. Garupa) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblLGarupa" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                C.G. (C. Garupa) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCGarupa" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                C.C. (C. Corporal) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCCorporal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                P.T. (P. Torácico) cm:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblPToracico" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Caracterização Racial:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCaracterizacaoRacial" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Classificação de Úbere:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblClassificacaoUbere" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    </fieldset>
    
</asp:Content>
