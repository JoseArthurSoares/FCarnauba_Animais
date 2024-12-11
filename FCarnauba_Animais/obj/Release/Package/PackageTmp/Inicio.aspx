<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInicio.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="FCarnauba_Animais.Inicio" AspCompat="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
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
    .style1
        {
            text-align: center;
            font-size:large;
            font-weight: bold;
        }
    .style2
        {
            height: 87px;
        }
        
    .icon
        {
            height: 150px;
        }    
        
        
</style>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function pergunta() {
            if (confirm("Realmente deseja remover?")) {
                return true;
            } else {
                return false;
            }
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
        $('form').live("submit", function () {
            ShowProgress();
        });

        $('button').live("click", function () {
            //ShowProgress();
        });

        $('img[src="./img/adicionar.png"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/edit_icon.gif"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/details_icon.gif"]').live("click", function () {
            ShowProgress();
        });

</script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>

<table width="100%">
    <tr>
            <td class="style1"><a href="Animais.aspx"><img class="icon" src="img/Animais.png"></a></td>
            <td class="style1"><a href="EstruturasPropriedades.aspx"><img class="icon" src="img/propriedades.png"></a></td>
            <td class="style1"><a href="LotesPonderais.aspx"><img class="icon" src="img/ponderal.png"></a></td>
        </tr>
        <tr>
            <td class="style1"><a href="Lotes.aspx"><img class="icon" src="img/leiteiro.png"></a></td>
            <td class="style1"><a href="Financeiro.aspx"><img class="icon" src="img/financeiro.png"></a></td>
            <td class="style1"><a href="Pluviometrico.aspx"><img class="icon" src="img/pluviometria.png"></a></td>           
        </tr>
        <tr>
            <td class="style1"><a href="SimuladorEndogamia.aspx"><img class="icon" src="img/simulador.png"></a></td>
            <td class="style1"><a href="RTodosIndices.aspx"><img class="icon" src="img/zootecnicos.png"></a></td>
            <td class="style1"><a href="Cdcs.aspx"><img class="icon" src="img/cruzamentos.png"></a></td>           
        </tr>
</table>

</asp:Content>
