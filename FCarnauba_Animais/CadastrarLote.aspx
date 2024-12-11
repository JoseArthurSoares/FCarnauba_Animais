<%@ Page Title="" Language="C#" MasterPageFile="~/SiteControleLeiteiro.Master" AutoEventWireup="true" CodeBehind="CadastrarLote.aspx.cs" Inherits="FCarnauba_Animais.CadastrarLote" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlProducaoLeite.ascx" TagName="UserControlProducaoLeite"
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
                if (Request.Params["loteId"] != "") {
                    param = Request.Params["loteId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "CadastrarLote.aspx?loteId=" + param
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
        if ($("#txtEntradaControle").length > 0) $("#txtEntradaControle").mask("99/99/9999");
        if ($("#txtSaidaControle").length > 0) $("#txtSaidaControle").mask("99/99/9999");
        if ($("#txtDataLote").length > 0) $("#txtDataLote").mask("99/99/9999");
        if ($("#txtPOrdenha").length > 0) $("#txtPOrdenha").mask("99:99");
        if ($("#txtSOrdenha").length > 0) $("#txtSOrdenha").mask("99:99");
        if ($("#txtTOrdenha").length > 0) $("#txtTOrdenha").mask("99:99");
        if ($("#txtDataSaidaControle").length > 0) $("#txtDataSaidaControle").mask("99/99/9999");
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
<div id="tabs" style="margin-top:20px">
        <ul>
            <li><a href="#lote-tab">Cadastro</a></li>
            <li><a href="#producaoleite-tab">Matrizes e Pesagens</a></li>
        </ul>
        <div id="lote-tab">
        <fieldset class="cadastrar_info">
            <legend>Dados do Lote&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
        <tr>
            <td class="rotulo">
                Raça:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlRaca" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>SINDI</asp:ListItem>
                    <asp:ListItem>GUZERÁ</asp:ListItem>
                    <asp:ListItem>CURRALEIRO PÉ DURO</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Lote:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtLote" runat="server" width="97px"></asp:textbox>
		        </div>
		        <asp:customvalidator id="cvLote" runat="server" forecolor="Red" onservervalidate="cvLote_ServerValidate" validationgroup="lote"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data do Controle:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtDataLote" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvDataLote" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataLote_ServerValidate"></asp:customvalidator>
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
	        <td width="20%" class="rotulo">Categoria:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtCategoria" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        <asp:customvalidator id="cvCategoria" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvCategoria_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	        </td>
	    </tr>
        <tr>
	        <td width="20%" class="rotulo">
	                    Hora 1ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtPOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
                        <asp:customvalidator id="cvPOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPOrdenha_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                    Hora 2ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtSOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
                        <asp:customvalidator id="cvSOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvSOrdenha_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                    Hora 3ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtTOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                   Controlador:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtControlador" runat="server" Width="200px" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        <asp:customvalidator id="cvControlador" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvControlador_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
	                <td class="rotulo">
		                Liberar Lote para Pesagem?
	                </td>
	                <td>
		                <div class="inputDiv">
			                <asp:CheckBox ID="ckLiberarLotePesagem" runat="server"/>
		                </div>
	                </td>
                </tr>
        </table>
        </fieldset>
            <p style="text-align: center">
	            <asp:button id="btnProximo" runat="server" onclick="btnProximo_Click" text="Salvar" BackColor="#052B5C" ForeColor="White"></asp:button>
                <asp:Button id="btnCancelar" onclientclick="goBack();return false;" runat="server" Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
            </p>
        </div>
        <div id="producaoleite-tab">
             <uc3:UserControlProducaoLeite ID="UserControlProducaoLeite1" runat="server" EditMode="false" ControleLeiteiroId="<%# LoteId %>" />
            
        </div>
</div>
</asp:Content>
