<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInicio.Master" AutoEventWireup="true" CodeBehind="CadastrarDispositivo.aspx.cs" Inherits="FCarnauba_Animais.CadastrarDispositivo" AspCompat="true" %>
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
<script type ="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
<script type ="text/javascript" src="./Scripts/validator.js"></script>
<script type ="text/javascript" src="./Scripts/autonumeric.js"></script>
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
                if (Request.Params["lotePonderalId"] != "") {
                    param = Request.Params["lotePonderalId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "CadastrarLotePonderal.aspx?lotePonderalId=" + param
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
        //if ($("#txtEntradaControle").length > 0) $("#txtEntradaControle").mask("99/99/9999");
        //if ($("#txtSaidaControle").length > 0) $("#txtSaidaControle").mask("99/99/9999");
        //if ($("#txtDataLote").length > 0) $("#txtDataLote").mask("99/99/9999");
        //if ($("#txtDataSaidaControle").length > 0) $("#txtDataSaidaControle").mask("99/99/9999");
        //var numSettings = { aSep: '.', aDec: ',', aSign: 'R$ ' };
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
<uc4:Mensagem ID="mensagem" runat="server" />

        <fieldset class="cadastrar_info">
            <legend>Cadastrar Dispositivo&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
        <tr>
            <td class="rotulo">
                Tipo de Equipamento:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTipoEquipamento" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>Balanças Eletrônicas</asp:ListItem>
                    <asp:ListItem>Bastões de Leitura</asp:ListItem>
                    <asp:ListItem>Leitores de RFID</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Equipamento:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlRaca" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>AnimallTAG TB55</asp:ListItem>
                    <asp:ListItem>Coimma KM 3 PLUS V2</asp:ListItem>
                    <asp:ListItem>WBeck – 0323 PÉ DURO</asp:ListItem>
                    <asp:ListItem>Açores ACR Heavy Duty</asp:ListItem>
                    <asp:ListItem>Toledo MGR Campo</asp:ListItem>
                    <asp:ListItem>TruTest EWZI 7</asp:ListItem>
                    <asp:ListItem>Animal Tag AT 05</asp:ListItem>
                    <asp:ListItem>TruTest XRS2</asp:ListItem>
                    <asp:ListItem>Baqueano</asp:ListItem>
                    <asp:ListItem>RFID Honeywell IF1</asp:ListItem>
                    <asp:ListItem>AllTags</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        API/Aplicação:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtAPIAplicacao" runat="server" width="300px"></asp:textbox>
		        </div>
		        
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        USB:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtUSB" runat="server" width="300px"></asp:textbox>
		        </div>
		        
	        </td>
        </tr>
        <tr>
	                <td width="20%" class="rotulo">
	                   Usuário:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtUsuario" runat="server" Width="200px" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        
	                </td>
	            </tr>
        <tr>
	        <td class="rotulo">
		        Diretório/Arquivo:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtDiretorioArquivo" runat="server" width="300px"></asp:textbox>
		        </div>
		        
	        </td>
        </tr>
        <tr>
	                <td width="20%" class="rotulo">
	                   Formato:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtFormato" runat="server" Width="200px" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        
	                </td>
	            </tr>
        <tr>
        </table>
        </fieldset>
            <p style="text-align: center">
	            <asp:button id="btnProximo" runat="server" text="Salvar" BackColor="#052B5C" ForeColor="White"></asp:button>
                <asp:Button id="btnCancelar" onclientclick="goBack();return false;" runat="server" Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
            </p>
</asp:Content>
