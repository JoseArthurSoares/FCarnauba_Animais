<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="DetalhesControleLeiteiro.aspx.cs" Inherits="FCarnauba_Animais.DetalhesControleLeiteiro" AspCompat="true" %>
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
        function hideEditButton() {
            jq191("#tabs").ready(function () {
                jq191("#EditImageButton").hide();
                jq191("#tabs").tabs("option", "activate", null)
            });
        }

        jq191(function () {
            jq191("#tabs").tabs(
             {
                 activate: function (event, ui) {
                     var newId = ui.newPanel.attr('id');
                    
                     jq191("#EditImageButton").attr('href', "EditaControleLeiteiro.aspx?controleLeiteiroId=" + <%= Request.Params["controleLeiteiroId"] %> +
                                                                "&tabIndex=" + ui.newTab.context.innerHTML);  
                 }
             });
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

         $('img[src="./img/edit_icon.gif"]').live("click", function () {
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
    <div id="tabs" style="margin-top:20px">
        <ul>
            <li><a href="#gerais-tab">Cadastro</a></li>
            <li><a href="#producaoleite-tab">Produção de Leite</a></li>
        </ul>

        <div id="gerais-tab">
            
        <fieldset class="cadastrar_info">
        <legend>Controle Leiteiro&nbsp;&nbsp;</legend>
        <div align="right">   
            <div align="right"><a href="./EditaControleLeiteiro.aspx?act=edit&controleLeiteiroId=<%= id %>" id="A1" title="Editar"><img src="./img/edit_icon.gif" /></a></div>            
        </div>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Categoria:
                </td>
                <td width="80%" class="dado">
                    <asp:Label ID="lblCategoria" runat="server"></asp:Label>
                </td>   
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Data do Controle:
                </td>
                <td width="80%" class="dado">
                    <asp:Label ID="lblDataControle" runat="server"></asp:Label>
                </td>   
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Data da Próxima Visita:
                </td>
                <td width="80%" class="dado">
                    <asp:Label ID="lblDataproximaVisita" runat="server"></asp:Label>
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
                    Controlador:
                </td>
                <td width="80%" class="dado">
                    <asp:Label ID="lblControlador" runat="server"></asp:Label>
                </td>   
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Relatório Controle Leiteiro:
                </td>
                <td width="80%" class="dado">
                    <input type="image" src="./img/pdf_icon.png"  value="Relatório Controle Leiteiro" title="Relatório Controle Leiteiro"  onclick="javascript:window.open('RelatoriosControleLeiteiro.aspx?loteId=<%= idLote %>','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=300,width=1050,height=750');return false;" >
                </td>   
            </tr>
        </table>
        </fieldset>
        </div>
        <div id="producaoleite-tab">
            <uc3:UserControlProducaoLeite ID="UserControlProducaoLeite1" ReadOnly="true" ControleLeiteiroId="<%# id %>" runat="server" />
        </div>
    </div>
</asp:Content>
