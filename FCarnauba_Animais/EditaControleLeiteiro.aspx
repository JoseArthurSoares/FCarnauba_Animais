<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="EditaControleLeiteiro.aspx.cs" Inherits="FCarnauba_Animais.EditaControleLeiteiro" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlCadastraControleLeiteiro.ascx" TagName="CadastraControleLeiteiroUserControl"
    TagPrefix="uc2" %>
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
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
    <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript" src="./Scripts/validator.js"></script> 
    <script type="text/javascript" src="./Scripts/autonumeric.js"></script>
    <script type="text/javascript">
        var jq191 = $.noConflict(true);
        jQuery = jq191;
        $ = jQuery;
    </script>
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript">
        var oldFormAction = document.forms[0].action;

        jq191(function () {
            jq191("#tabs").tabs(
        {
            activate: function (event, ui) {
                var newId = ui.newPanel.attr('id');
                var param;
                if (Request.Params["controleLeiteiroId"] != "") {
                    param = Request.Params["controleLeiteiroId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "EditaControleLeiteiro.aspx?controleLeiteiroId=" + param
                                                           + "&tabIndex=" + ui.newTab.context.innerHTML;
                document.forms[0].action = targUrl;
                
            }
        });
        });

        function setActive(tabIndex) {
            jq191("#tabs").ready(function () {
                var i = 0;
                jq191('#tabs ul li a').each(function () {
                    if ($(this).text() == tabIndex) jq191("#tabs").tabs("option", "active", i);
                    i++;
                });

            });
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

        $('img[src="./img/edit_icon.gif"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/adicionar.png"]').live("click", function () {
            ShowProgress();
        });
</script>
<script type ="text/javascript">
    $(document).ready(function () {
        //alert('EditaObra ready');
        //if ($("#txtDataPercentualFisico").length > 0) $("#txtDataPercentualFisico").mask("99/99/9999");
        var numSettings = { aSep: '.', aDec: ',', aSign: 'R$ ' };
        //if ($("#txtValorMedido").length > 0) $("#txtValorMedido").autoNumeric(numSettings);
        //if ($("#txtValorMedido").length > 0) $("#txtValorMedido").autoNumericSet(parseFloat($("#txtValorMedido").val().replace(",", ".")));
        //if ($("#txtCEP").length > 0) $("#txtCEP").mask("99999-999");
    });
</script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div id="tabs" style="margin-top:20px">
        <ul>
            <li><a href="#gerais-tab">Cadastro</a></li>
            <li><a href="#producaoleite-tab">Produção de Leite</a></li>
            
        </ul>
        <div id="gerais-tab">
            <uc2:CadastraControleLeiteiroUserControl ID="CadastraControleLeiteiroUserControl1" runat="server" EditMode="true"/>
        </div>
        <div id="producaoleite-tab">
             <uc3:UserControlProducaoLeite ID="UserControlProducaoLeite1" runat="server" EditMode="false" ControleLeiteiroId="<%# id %>" />
            
        </div>
</div>
</asp:Content>
