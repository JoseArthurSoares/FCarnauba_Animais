<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="CadastrarControlePluviometrico.aspx.cs" Inherits="FCarnauba_Animais.CadastrarControlePluviometrico" AspCompat="true" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
        var numSettings = { aSep: '.', aDec: ',', aSign: '' };
        if ($("#txtPluviometria").length > 0) $("#txtPluviometria").autoNumeric(numSettings);

        if ($("#txtPluviometria").length > 0) $("#txtPluviometria").autoNumericSet(parseFloat($("#txtPluviometria").val().replace(",", ".")));

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
            <!--<legend>Dados do Controle Pluviométrico. Preencher os dados obrigatórios em branco (marcados com um *)</legend>-->
        <table width="100%">
        <tr>
            <td class="rotulo">
                Propriedade:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlPropriedade" runat="server" Height="18px" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" autopostback="true" OnSelectedIndexChanged="ddlPropriedade_SelectedIndexChanged">
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtData" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____" autopostback="true" OnTextChanged="txtData_TextChanged"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvData" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvData_ServerValidate"></asp:customvalidator>
	            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Pluviômetro:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:DropDownList ID="ddlPluviometro" runat="server" Height="18px" Width="50" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
                    <asp:ListItem>10</asp:ListItem>
                </asp:DropDownList></div>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Pluviometria:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtPluviometria" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvPluviometria" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPluviometria_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        </table>
        <p style="text-align: center">
	            <asp:button id="btnProximo" runat="server" onclick="btnProximo_Click" text="Salvar" BackColor="#052B5C" ForeColor="White"></asp:button>
                <asp:Button id="btnCancelar" onclientclick="goBack();return false;" runat="server" Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
            </p>
            <rsweb:reportviewer ID="reportViewer" runat="server" 
                Style="width: 100%; height: 400px;" ShowToolBar="False">
            </rsweb:reportviewer>
        </fieldset>
        
            
</asp:Content>
