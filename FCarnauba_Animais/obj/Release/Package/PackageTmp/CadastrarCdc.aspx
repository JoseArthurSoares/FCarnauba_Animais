<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCDC.Master" AutoEventWireup="true" CodeBehind="CadastrarCdc.aspx.cs" Inherits="FCarnauba_Animais.CadastrarCdc" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlCdcMatrizes.ascx" TagName="UserControlCdcMatrizes"
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
                if (Request.Params["cdcId"] != "") {
                    param = Request.Params["cdcId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "CadastrarCdc.aspx?cdcId=" + param
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
        if ($("#txtDataCobertura").length > 0) $("#txtDataCobertura").mask("99/99/9999");
        if ($("#txtDataImplantacao").length > 0) $("#txtDataImplantacao").mask("99/99/9999");
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
            <legend>Dados dos Cruzamentos&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
        <tr>
	        <td class="rotulo">
		        CDC:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtCdc" runat="server" width="97px"></asp:textbox>
		        </div>
		        <asp:customvalidator id="cvCdc" runat="server" forecolor="Red" onservervalidate="cvCdc_ServerValidate" validationgroup="cdc"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Propriedade:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlPropriedade" runat="server" Height="18px" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data da Cobertura:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtDataCobertura" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvDataCobertura" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataCobertura_ServerValidate"></asp:customvalidator>
	            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Tipo:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTipo" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>Monta Natural</asp:ListItem>
                    <asp:ListItem>FIV</asp:ListItem>
                    <asp:ListItem>IA</asp:ListItem>
                    <asp:ListItem>TE</asp:ListItem>
                    <asp:ListItem>Repetição de Cio</asp:ListItem>
                    <asp:ListItem>Outra</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Raça:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlRaca" Name="ddlRaca" runat="server" Width="200" ViewStateMode="Enabled">
             <asp:ListItem>SINDI</asp:ListItem>
             <asp:ListItem>ZEBUÍNAS</asp:ListItem>
             <asp:ListItem>GUZERÁ</asp:ListItem>
             <asp:ListItem>CURRALEIRO PÉ DURO</asp:ListItem>
            </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Touro:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTouro" runat="server" Height="18px" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Veterinário:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtVeterinario" runat="server" width="200px"></asp:textbox>
		        </div>
		        <asp:customvalidator id="cvVeterinario" runat="server" forecolor="Red" onservervalidate="cvVeterinario_ServerValidate" validationgroup="lote"></asp:customvalidator>
	        </td>
        </tr>
        </table>
        </fieldset>
            <p style="text-align: center">
	            <asp:button id="btnProximo" runat="server" onclick="btnProximo_Click" text="Salvar" BackColor="#052B5C" ForeColor="White"></asp:button>
                <asp:Button id="btnCancelar" onclientclick="goBack();return false;" runat="server" Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
            </p>
        </div>
        <div id="cdcmatrizes-tab">
             <uc3:UserControlCdcMatrizes ID="UserControlCdcMatrizes1" runat="server" EditMode="false" ControleLeiteiroId="<%# CdcId %>" />
            
        </div>
</div>
</asp:Content>
