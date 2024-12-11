<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Relatorios.aspx.cs" Inherits="FCarnauba_Animais.Relatorios" AspCompat="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            ShowProgress();
        });

        $('a[href="Relatorios.aspx?rel=1"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=2"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=3"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=4"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=5"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=6"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=7"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=8"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=9"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=10"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=11"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=12"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=13"]').live("click", function () {
            ShowProgress();
        });

</script>
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
	<div class="barra02" align="left">
		Relatórios</div>
	
<rsweb:ReportViewer ID="reportViewer" runat="server" Style="width: 100%; height: 400px;">
</rsweb:ReportViewer>
</asp:Content>
