<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControlePonderal.Master" AutoEventWireup="true" CodeBehind="DetalhesLotePonderal.aspx.cs" Inherits="FCarnauba_Animais.DetalhesLotePonderal" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlMensuracao.ascx" TagName="UserControlMensuracao"
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
                     if (newId == "mensuracao-tab" || newId == "loteponderal-tab") {
                         //jq191("#EditImageButton").hide();
                     } else {
                         //var targUrl = "CadastrarLotePonderal.aspx?act=edit&" +
                         //"lotePonderalId=" + "<%= LotePonderalId %>" +
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
            <li><a href="#loteponderal-tab">Cadastro</a></li>
            <li><a href="#mensuracao-tab">Mensurações</a></li>
        </ul>
    <div id="loteponderal-tab">
        <fieldset class="cadastrar_info">
            <legend>Lote Ponderal&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarLotePonderal.aspx?act=edit&lotePonderalId=<%=LotePonderalId%>" id="EditarM" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
            <table width="100%">
                <tr>
                    <td width="15%" class="rotulo">Lote:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblLotePonderal" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data do Controle:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataLotePonderal" runat="server"></asp:Label>
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
                    Controlador:
                </td>
                <td width="80%" class="dado">
                    <asp:Label ID="lblControlador" runat="server"></asp:Label>
                </td>   
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Lista Ponderal:
                </td>
                <td width="80%" class="dado">
                    <input type="image" src="./img/pdf_icon.png"  value="Lista Ponderal" title="Lista Ponderal"  onclick="javascript:window.open('RelatoriosControlePonderal.aspx?lotePonderalId=<%= LotePonderalId %>','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=300,width=1050,height=750');return false;" >
                </td>   
            </tr>
            <tr>
                    <td width="15%" class="rotulo">Liberar Lote para Mensuração:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblLiberarLotePonderalMensuracao" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
    </div>
    <div id="mensuracao-tab">
           <uc3:UserControlMensuracao ID="UserControlMensuracao1" ReadOnly="true" LotePonderalId="<%# Convert.ToInt32(LotePonderalId) %>" runat="server" />
    </div>

</asp:Content>
