<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInicio.Master" AutoEventWireup="true" CodeBehind="SPL.aspx.cs" Inherits="FCarnauba_Animais.SPL" AspCompat="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
</style>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
<script type ="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
<script type ="text/javascript" src="./Scripts/validator.js"></script>
<script type ="text/javascript" src="./Scripts/autonumeric.js"></script>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
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

        $('#Buscar').live("click", function () {
            ShowProgress();
        });

        $('a[href="Relatorios.aspx?rel=1"]').live("click", function () {
            ShowProgress();
       

</script>
<script type ="text/javascript">
    $(document).ready(function () {
        //alert('EditaObra ready');
        //if ($("#txtDataInicio").length > 0) $("#txtDataInicio").mask("99/99/9999");
        //if ($("#txtDataFim").length > 0) $("#txtDataFim").mask("99/99/9999");
    });
</script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div class="barra02" align="left">
			Sumário Teor de Proteína do Leite/Lactação</div>
            <div id="controles">
<table width="100%">
    <tr>
        <td style="width: 80px">Ano:</td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddlAno" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList>
        </td>
        <td style="width: 30px">Raça:</td>
        <td style="width: 30px">
            <asp:DropDownList ID="ddlRaca" Name="ddlRaca" runat="server" Width="100" ViewStateMode="Enabled">
             <asp:ListItem>SINDI</asp:ListItem>
             <asp:ListItem>ZEBUÍNAS</asp:ListItem>
             <asp:ListItem>GUZERÁ</asp:ListItem>
             <asp:ListItem>CURRALEIRO PÉ DURO</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 50px">Propriedade:</td>
        <td style="width: 200px">
            <asp:DropDownList ID="ddlPropriedade" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnRelatorio" runat="server" Text="Relatório" 
                 onclick="btnRelatorio_Click" BackColor="#052B5C" ForeColor="White" />
            
        </td>
    </tr>
</table>
</div>
<rsweb:reportviewer ID="reportViewer" runat="server" 
        Style="width: 100%; height: 800px;">
</rsweb:reportviewer>
</asp:Content>
