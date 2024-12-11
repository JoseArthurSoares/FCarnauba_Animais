<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="DetalhesProducaoLeite.aspx.cs" Inherits="FCarnauba_Animais.DetalhesProducaoLeite" AspCompat="true" %>
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
        <legend> <font face="Verdana,Arial,sans-serif" color="black"><b>Produção de Leite&nbsp;&nbsp;</b></font></legend>
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
                Cria:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblCria" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Lactacao:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblLactacao" runat="server"></asp:Label>
            </td>
        </tr>
         <tr>
            <td width="20%" class="rotulo">
                1ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblPOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                2ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblSOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                3ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblTOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Total:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblTotal" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Gordura 1ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblGordPOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Gordura 2ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblGordSOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Gordura 3ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblGordTOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Proteína 1ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblProtPOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Proteína 2ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblProtSOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Proteína 3ª Ordenha:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblProtTOrdenha" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Bezerros ao Pé:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblBezerrosPe" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Tetos Funcionais:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblTetosFuncionais" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Observações:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblObservacoes" runat="server"></asp:Label>
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
                Receptora:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblReceptora" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Removido do Controle?
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblSairControle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Data da Remoção do Controle:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblDataSaidaControle" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="20%" class="rotulo">
                Motivo:
            </td>
            <td width="80%" class="dado">
                <asp:Label ID="lblMotivo" runat="server"></asp:Label>
            </td>
        </tr>
        </table>
    </fieldset>
    
</asp:Content>
