<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="DetalhesLote.aspx.cs" Inherits="FCarnauba_Animais.DetalhesLote" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlProducaoLeite.ascx" TagName="UserControlProducaoLeite"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
     <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
    <input type="hidden" name="tabIndex" />
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
                     if (newId == "producaoleite-tab" || newId == "lote-tab") {
                         //jq191("#EditImageButton").hide();
                     } else {
                         //var targUrl = "CadastrarLote.aspx?act=edit&" +
                                            //"loteId=" + "<%= LoteId %>" +
                                            //"&tabIndex=" + ui.newTab.context.innerHTML;
                         //jq191("#EditImageButton").show();
                         //jq191("#EditImageButton").attr('href', targUrl);
                     }
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
    <uc4:Mensagem ID="mensagem" runat="server" />
    <div id="tabs" style="margin-top:20px">
        <ul>
            <li><a href="#lote-tab">Cadastro</a></li>
            <li><a href="#producaoleite-tab">Matrizes e Pesagens</a></li>
        </ul>
    <div id="lote-tab">
        <fieldset class="cadastrar_info">
            <legend>Lote&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarLote.aspx?act=edit&loteId=<%=LoteId%>" id="EditarM" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
            <table width="100%">
                <tr>
                    <td width="15%" class="rotulo">Lote:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblLote" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data do Controle:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataLote" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Propriedade:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPropriedade" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Raça:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRaca" runat="server"></asp:Label>
                    </td>
                </tr>
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
                    <input type="image" src="./img/pdf_icon.png"  value="Relatório Controle Leiteiro" title="Relatório Controle Leiteiro"  onclick="javascript:window.open('RelatoriosControleLeiteiro.aspx?loteId=<%= LoteId %>','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=300,width=1050,height=750');return false;" >
                </td>   
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Lista de Pesagem de Leite:
                </td>
                <td width="80%" class="dado">
                    <input type="image" src="./img/pdf_icon.png"  value="Lista de Pesagem de Leite" title="Relatório Controle Leiteiro"  onclick="javascript:window.open('RelatoriosControleLeiteiro.aspx?loteIdPes=<%= LoteId %>','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=300,width=1050,height=750');return false;" >
                </td>   
            </tr>
            </table>
            </fieldset>
    </div>
    <div id="producaoleite-tab">
           <uc3:UserControlProducaoLeite ID="UserControlProducaoLeite1" ReadOnly="true" LoteId="<%# Convert.ToInt32(LoteId) %>" runat="server" />
    </div>

</asp:Content>
