<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEstruturaPropriedade.Master" AutoEventWireup="true" CodeBehind="DetalhesEstruturaPropriedade.aspx.cs" Inherits="FCarnauba_Animais.DetalhesEstruturaPropriedade" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlPastagem.ascx" TagName="UserControlPastagem"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/UserControlAgricultura.ascx" TagName="UserControlAgricultura"
    TagPrefix="uc5" %>
<%@ Register Src="~/UserControls/UserControlBenfeitoria.ascx" TagName="UserControlBenfeitoria"
    TagPrefix="uc6" %>
<%@ Register Src="~/UserControls/UserControlArrendamento.ascx" TagName="UserControlArrendamento"
    TagPrefix="uc7" %>
<%@ Register Src="~/UserControls/UserControlOutra.ascx" TagName="UserControlOutra"
    TagPrefix="uc8" %>
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
                     if (newId == "pastagem-tab" || newId == "agricultura-tab" || newId == "benfeitoria-tab" || newId == "estruturapropriedade-tab") {
                         //jq191("#EditImageButton").hide();
                     } else {
                         //var targUrl = "CadastrarLotePonderal.aspx?act=edit&" +
                         //"estruturaPropriedadeId=" + "<%= EstruturaPropriedadeId %>" +
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
            <li><a href="#estruturapropriedade-tab">Cadastro</a></li>
            <li><a href="#pastagem-tab">Pastagens</a></li>
            <li><a href="#agricultura-tab">Agriculturas</a></li>
            <li><a href="#benfeitoria-tab">Benfeitorias</a></li>
            <li><a href="#arrendamento-tab">Arrendamentos</a></li>
            <li><a href="#outra-tab">Outras</a></li>
        </ul>
        <div id="estruturapropriedade-tab">
        <fieldset class="cadastrar_info">
            <legend>Propriedade&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarEstruturaPropriedade.aspx?act=edit&estruturaPropriedadeId=<%=EstruturaPropriedadeId%>" id="EditarP" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
            <table width="100%">
                <tr>
                    <td width="15%" class="rotulo">Propriedade:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPropriedade" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblData" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Total Pastagens:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTotalPastagens" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Total Agricultura:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTotalAgricultura" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Benfeitorias:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblBenfeitorias" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Arrendamentos:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblArrendamentos" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Reserva:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblReserva" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Outras:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblOutras" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
    </div>
    <div id="pastagem-tab">
           <uc3:UserControlPastagem ID="UserControlPastagem1" ReadOnly="true" EstruturaPropriedadelId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" runat="server" />
    </div>
    <div id="agricultura-tab">
           <uc5:UserControlAgricultura ID="UserControlAgricultura1" ReadOnly="true" EstruturaPropriedadelId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" runat="server" />
    </div>
    <div id="benfeitoria-tab">
           <uc6:UserControlBenfeitoria ID="UserControlBenfeitoria1" ReadOnly="true" EstruturaPropriedadelId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" runat="server" />
    </div>
    <div id="arrendamento-tab">
           <uc7:UserControlArrendamento ID="UserControlArrendamento1" ReadOnly="true" EstruturaPropriedadelId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" runat="server" />
    </div>
    <div id="outra-tab">
           <uc8:UserControlOutra ID="UserControlOutra1" ReadOnly="true" EstruturaPropriedadelId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" runat="server" />
    </div>
</asp:Content>
