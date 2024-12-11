<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="CadastrarFluxoCaixa.aspx.cs" Inherits="FCarnauba_Animais.CadastrarFluxoCaixa" AspCompat="true" %>
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
<script type ="text/javascript">
    $(document).ready(function () {
        //alert('EditaObra ready');
        if ($("#txtData").length > 0) $("#txtData").mask("99/99/9999");
        var numSettings = { aSep: '', aDec: ',', aSign: '' };
        if ($("#txtValor").length > 0) $("#txtValor").autoNumeric(numSettings);

        if ($("#txtValor").length > 0) $("#txtValor").autoNumericSet(parseFloat($("#txtValor").val().replace(",", ".")));

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
            <legend>Dados do Fluxo de Caixa&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
        <tr>
            <td class="rotulo">
                Propriedade:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlPropriedade" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtData" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvData" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvData_ServerValidate"></asp:customvalidator>
	            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Tipo:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:DropDownList ID="ddlTipo" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>Entrada</asp:ListItem>
                    <asp:ListItem>Saída</asp:ListItem>
                </asp:DropDownList></div>
	        </td>
        </tr>
        <tr>
	                <td width="20%" class="rotulo">
	                   Descrição:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtDescricao" runat="server" Width="400px" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        <asp:customvalidator id="cvDescricao" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDescricao_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
        <tr>
            <td class="rotulo">
                Centro de Custo:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlCentroCusto" runat="server" Height="18px" Width="200" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Valor:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtValor" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvValor" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvValor_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        </table>
        </fieldset>
        <p style="text-align: center">
	            <asp:button id="btnProximo" runat="server" onclick="btnProximo_Click" text="Salvar" BackColor="#052B5C" ForeColor="White"></asp:button>
                <asp:Button id="btnCancelar" onclientclick="goBack();return false;" runat="server" Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
            </p>
</asp:Content>
