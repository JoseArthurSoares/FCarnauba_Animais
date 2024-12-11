﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="RelatoriosFinanceiro.aspx.cs" Inherits="FCarnauba_Animais.RelatoriosFinanceiro" AspCompat="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {

            if ($("#txtDataInicio").length > 0) $("#txtDataInicio").mask("99/99/9999");
            if ($("#txtDataFim").length > 0) $("#txtDataFim").mask("99/99/9999");

        });
    </script> 
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 2px solid black;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
    <script type="text/javascript">

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        $('button').live("click", function () {
            //ShowProgress();
        });

        $('#Buscar').live("click", function () {
            ShowProgress();
        });

        $('a[href="Relatorios.aspx?rel=1"]').live("click", function () {
            ShowProgress();
       
</script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div class="barra02" align="left">
			Financeiro</div>
<div id="controles">
<table>
<uc2:Mensagem ID="mensagem" runat="server" />
    <tr>
        <td>Propriedade:</td>
        <td>
            <asp:DropDownList ID="ddlPropriedade" Name="ddlPropriedade" runat="server" ViewStateMode="Enabled">
            </asp:DropDownList>
        </td>
        <td>Período:</td>
        <td>
            <asp:TextBox ID="txtDataInicio" runat="server" ClientIDMode="Static" name="txtDataInicio" placeholder="__/__/____" class="date_size"></asp:TextBox> a <asp:TextBox ID="txtDataFim" runat="server" ClientIDMode="Static" MaxLength="10" name="txtDataFim" placeholder="__/__/____" class="date_size"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnRelatorio" runat="server" Text="Relatório" 
            onclick="btnRelatorio_Click" BackColor="#052B5C" ForeColor="White" />
            
        </td>
        <td>Mês (Período):</td>
						<td>
                            <asp:DropDownList ID="ddlMes" runat="server" Height="18px" Width="150" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" AutoPostBack="true" OnSelectedIndexChanged="ddlMes_SelectedIndexChanged">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>JANEIRO</asp:ListItem>
                                <asp:ListItem>FEVEREIRO</asp:ListItem>
                                <asp:ListItem>MARÇO</asp:ListItem>
                                <asp:ListItem>ABRIL</asp:ListItem>
                                <asp:ListItem>MAIO</asp:ListItem>
                                <asp:ListItem>JUNHO</asp:ListItem>
                                <asp:ListItem>JULHO</asp:ListItem>
                                <asp:ListItem>AGOSTO</asp:ListItem>
                                <asp:ListItem>SETEMBRO</asp:ListItem>
                                <asp:ListItem>OUTUBRO</asp:ListItem>
                                <asp:ListItem>NOVEMBRO</asp:ListItem>
                                <asp:ListItem>DEZEMBRO</asp:ListItem>
                            </asp:DropDownList>
						</td>
    </tr>
</table>
</div>
<rsweb:reportviewer ID="reportViewer" runat="server" 
        Style="width: 100%; height: 400px;">
</rsweb:reportviewer>
</asp:Content>
