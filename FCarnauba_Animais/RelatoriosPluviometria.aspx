<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="RelatoriosPluviometria.aspx.cs" Inherits="FCarnauba_Animais.RelatoriosPluviometria" AspCompat="true" %>
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
        $('#Form1').live("submit", function () {
            ShowProgress();
        });

        $('submit').live("click", function () {
            ShowProgress();
        });

        $('#btnRelatorio').live("click", function () {
            ShowProgress();
        });

        $('a[href="RelatoriosPluviometria"]').live("click", function () {
            ShowProgress();
       

</script>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
<script type ="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
<script type ="text/javascript" src="./Scripts/validator.js"></script>
<script type ="text/javascript" src="./Scripts/autonumeric.js"></script>
<script type="text/javascript">
    var jq191 = $.noConflict(true);
    jQuery = jq191;
    $ = jQuery;
</script>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //if ($("#txtEditData").length > 0) $("#txtEditData").mask("99/99/9999");
            //if ($("#txtAddData").length > 0) $("#txtAddData").mask("99/99/9999");

            if ($("#txtDataInicio").length > 0) $("#txtDataInicio").mask("99/99/9999");
            if ($("#txtDataFim").length > 0) $("#txtDataFim").mask("99/99/9999");

        });

        $(function () {
            $(".datepick").datepicker({
                changeYear: true,
                changeMonth: true,
                showOn: "focus",
                dateFormat: "dd/mm/yy",
                dayNames: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sábado"],
                dayNamesMin: ["Dom", "Seg", "Ter", "Qua", "Qui", "Sex", "Sab"],
                monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Ou', 'Nov', 'Dez']
            });
        });

    </script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div class="barra02" align="left">
			Pluviometria</div>
<div id="controles">
<table>
    <tr>
        <td>Propriedade:</td>
        <td>
            <asp:DropDownList ID="ddlProprieddade" Name="ddlPropriedade" runat="server" Width="200" ViewStateMode="Enabled">
            </asp:DropDownList>
        </td>
        <td>
							Período:</td>
						<td>
							<asp:TextBox ID="txtDataInicio" runat="server" ClientIDMode="Static" name="txtDataInicio" placeholder="__/__/____" class="datepick"></asp:TextBox> a <asp:TextBox ID="txtDataFim" runat="server" ClientIDMode="Static" MaxLength="10" name="txtDataFim" placeholder="__/__/____" class="datepick"></asp:TextBox>
        </td>
         <td>Ano:</td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddlAno" Name="ddlAno" runat="server" Width="100" ViewStateMode="Enabled" AutoPostBack="true" OnSelectedIndexChanged="ddlAno_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnRelatorio" runat="server" Text="Relatório" 
                onclick="btnRelatorio_Click" BackColor="#052B5C" ForeColor="White" ClientIDMode="Static" />
            
        </td>
    </tr>
</table>
</div>
<rsweb:reportviewer ID="reportViewer" runat="server" 
        Style="width: 100%; height: 400px;">
</rsweb:reportviewer>
</asp:Content>
