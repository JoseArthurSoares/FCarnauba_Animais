﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControlePonderal.Master" AutoEventWireup="true" CodeBehind="EditaMensuracao.aspx.cs" Inherits="FCarnauba_Animais.EditaMensuracao" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlMensuracao.ascx" TagName="UserControlMensuracao"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    <script type ="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
    <script type ="text/javascript" src="Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript" src="Scripts/autonumeric.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            if ($("#txtDataEntradaControle").length > 0) $("#txtDataEntradaControle").mask("99/99/9999");
            if ($("#txtDataSaidaControle").length > 0) $("#txtDataSaidaControle").mask("99/99/9999");

            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtPeso").length > 0) $("#txtPeso").autoNumeric(numSettings);
            if ($("#txtPeso").length > 0) $("#txtPeso").autoNumericSet(parseFloat($("#txtPeso").val().replace(",", ".")));

            if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumeric(numSettings);
            if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumericSet(parseFloat($("#txtCEscrotal").val().replace(",", ".")));

            if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumeric(numSettings);
            if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumericSet(parseFloat($("#txtAAnterior").val().replace(",", ".")));

            if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumeric(numSettings);
            if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumericSet(parseFloat($("#txtAPosterior").val().replace(",", ".")));

            if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumeric(numSettings);
            if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumericSet(parseFloat($("#txtLGarupa").val().replace(",", ".")));

            if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumeric(numSettings);
            if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumericSet(parseFloat($("#txtCGarupa").val().replace(",", ".")));

            if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumeric(numSettings);
            if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumericSet(parseFloat($("#txtCCorporal").val().replace(",", ".")));

            if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumeric(numSettings);
            if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumericSet(parseFloat($("#txtPToracico").val().replace(",", ".")));

        });

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

        $('a').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/adicionar.png"]').live("click", function () {
            ShowProgress();
        });
    </script>
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
    <uc3:UserControlMensuracao ID="UserControlMensuracao1" runat="server" ControlePonderalId="<%# id %>" MensuracaoInd="<%# m_ind %>" EditMode="true" />
</asp:Content>
