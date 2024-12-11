<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="DetalhesControlePluviometrico.aspx.cs" Inherits="FCarnauba_Animais.DetalhesControlePluviometrico" AspCompat="true" %>
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
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript">
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
    <fieldset class="cadastrar_info">
            <legend>Controle Pluviométrico&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarControlePluviometrico.aspx?act=edit&controlePluviometricoId=<%=ControlePluviometricoId%>" id="EditarCP" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
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
                    <td width="15%" class="rotulo">Pluviômetro:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPluviometro" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Pluviometria:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPluviometria" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
</asp:Content>
