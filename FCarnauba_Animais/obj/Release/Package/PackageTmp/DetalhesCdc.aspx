<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCDC.Master" AutoEventWireup="true" CodeBehind="DetalhesCdc.aspx.cs" Inherits="FCarnauba_Animais.DetalhesCdc" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlCdcMatrizes.ascx" TagName="UserControlCDcMatrizes"
    TagPrefix="uc3" %>
    <%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
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
                     if (newId == "cdcmatrizes-tab" || newId == "cdc-tab") {
                         //jq191("#EditImageButton").hide();
                     } else {
                         //var targUrl = "CadastrarLote.aspx?act=edit&" +
                                            //"cdcId=" + "<%= CdcId %>" +
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
            <li><a href="#cdc-tab">Cadastro</a></li>
            <li><a href="#cdcmatrizes-tab">Matrizes</a></li>
        </ul>
        <div id="cdc-tab">
        <fieldset class="cadastrar_info">
            <legend>Cruzamentos&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarCdc.aspx?act=edit&cdcId=<%=CdcId%>" id="EditarM" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
            <table width="100%">
                <tr>
                    <td width="15%" class="rotulo">Cdc:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblCdc" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Propriedade:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPropriedade" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data da Cobertura:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataCobertura" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tipo:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTipo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Raca:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRaca" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Touro:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTouro" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Veterinário:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblVeterinario" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
    </div>
    <div id="cdcmatrizes-tab">
           <uc3:UserControlCdcMatrizes ID="UserControlCdcMatrizes1" ReadOnly="true" LoteId="<%# Convert.ToInt32(CdcId) %>" runat="server" />
    </div>
</asp:Content>
