﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="RelatoriosFluxoCaixa.aspx.cs" Inherits="FCarnauba_Animais.RelatoriosFluxoCaixa" AspCompat="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function pergunta() {
            if (confirm("Realmente deseja remover?")) {
                return true;
            } else {
                return false;
            }
        }

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
			Fluxo de Caixa</div>
<div id="controles">
<table width="100%" >
    <tr>
        <td style="width: 50px">Propriedade:</td>
        <td style="width: 200px">
            <asp:DropDownList ID="ddlPropriedade" Name="ddlPropriedade" runat="server" Width="200" ViewStateMode="Enabled">
            </asp:DropDownList>
        </td>
        <td style="width: 30px">Mês:</td>
        <td style="width: 200px">
            <asp:DropDownList ID="ddlMes" Name="ddlPropriedade" runat="server" Width="200" ViewStateMode="Enabled">
                <asp:ListItem>Janeiro</asp:ListItem>
                <asp:ListItem>Fevereiro</asp:ListItem>
                <asp:ListItem>Março</asp:ListItem>
                <asp:ListItem>Abril</asp:ListItem>
                <asp:ListItem>Maio</asp:ListItem>
                <asp:ListItem>Junho</asp:ListItem>
                <asp:ListItem>Julho</asp:ListItem>
                <asp:ListItem>Agosto</asp:ListItem>
                <asp:ListItem>Setembro</asp:ListItem>
                <asp:ListItem>Outubro</asp:ListItem>
                <asp:ListItem>Novembro</asp:ListItem>
                <asp:ListItem>Dezembro</asp:ListItem>
            </asp:DropDownList>
        </td>
         <td style="width: 30px">Ano:</td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddlAno" Name="ddlAno" runat="server" Width="100" ViewStateMode="Enabled">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnRelatorio" runat="server" Text="Relatório" 
                onclick="btnRelatorio_Click" BackColor="#052B5C" ForeColor="White" />
            
        </td>
    </tr>
</table>
</div>
<rsweb:reportviewer ID="reportViewer" runat="server" 
        Style="width: 100%; height: 400px;">
</rsweb:reportviewer>
</asp:Content>
