<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEstruturaPropriedade.Master" AutoEventWireup="true" CodeBehind="EditaAgricultura.aspx.cs" Inherits="FCarnauba_Animais.EditaAgricultura" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlAgricultura.ascx" TagName="UserControlAgricultura"
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
            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtAreaAgricultura").length > 0) $("#txtAreaAgricultura").autoNumeric(numSettings);
            if ($("#txtAreaAgricultura").length > 0) $("#txtAreaAgricultura").autoNumericSet(parseFloat($("#txtAreaAgricultura").val().replace(",", ".")));
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
    <uc3:UserControlAgricultura ID="UserControlAgricultura1" runat="server" EstruturaPropriedadeId="<%# id %>" AgriculturaInd="<%# a_ind %>" EditMode="true" />
</asp:Content>
