<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="EditaProducaoLeite.aspx.cs" Inherits="FCarnauba_Animais.EditaProducaoLeite" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlProducaoLeite.ascx" TagName="UserControlProducaoLeite"
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

            if ($("#txtPOrdenha").length > 0) $("#txtPOrdenha").autoNumeric(numSettings);
            if ($("#txtPOrdenha").length > 0) $("#txtPOrdenha").autoNumericSet(parseFloat($("#txtPOrdenha").val().replace(",", ".")));

            if ($("#txtSOrdenha").length > 0) $("#txtSOrdenha").autoNumeric(numSettings);
            if ($("#txtSOrdenha").length > 0) $("#txtSOrdenha").autoNumericSet(parseFloat($("#txtSOrdenha").val().replace(",", ".")));

            if ($("#txtTOrdenha").length > 0) $("#txtTOrdenha").autoNumeric(numSettings);
            if ($("#txtTOrdenha").length > 0) $("#txtTOrdenha").autoNumericSet(parseFloat($("#txtTOrdenha").val().replace(",", ".")));

            if ($("#txtGordPOrdenha").length > 0) $("#txtGordPOrdenha").autoNumeric(numSettings);
            if ($("#txtGordPOrdenha").length > 0) $("#txtGordPOrdenha").autoNumericSet(parseFloat($("#txtGordPOrdenha").val().replace(",", ".")));

            if ($("#txtGordSOrdenha").length > 0) $("#txtGordSOrdenha").autoNumeric(numSettings);
            if ($("#txtGordSOrdenha").length > 0) $("#txtGordSOrdenha").autoNumericSet(parseFloat($("#txtGordSOrdenha").val().replace(",", ".")));

            if ($("#txtGordTOrdenha").length > 0) $("#txtGordTOrdenha").autoNumeric(numSettings);
            if ($("#txtGordTOrdenha").length > 0) $("#txtGordTOrdenha").autoNumericSet(parseFloat($("#txtGordTOrdenha").val().replace(",", ".")));

            if ($("#txtProtPOrdenha").length > 0) $("#txtProtPOrdenha").autoNumeric(numSettings);
            if ($("#txtProtPOrdenha").length > 0) $("#txtProtPOrdenha").autoNumericSet(parseFloat($("#txtProtPOrdenha").val().replace(",", ".")));

            if ($("#txtProtSOrdenha").length > 0) $("#txtProtSOrdenha").autoNumeric(numSettings);
            if ($("#txtProtSOrdenha").length > 0) $("#txtProtSOrdenha").autoNumericSet(parseFloat($("#txtProtSOrdenha").val().replace(",", ".")));

            if ($("#txtProtTOrdenha").length > 0) $("#txtProtTOrdenha").autoNumeric(numSettings);
            if ($("#txtProtTOrdenha").length > 0) $("#txtProtTOrdenha").autoNumericSet(parseFloat($("#txtProtTOrdenha").val().replace(",", ".")));

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
    <uc3:UserControlProducaoLeite ID="UserControlProducaoLeite1" runat="server" ControleLeiteiroId="<%# id %>" ProducaoInd="<%# pl_ind %>" EditMode="true" />
</asp:Content>
