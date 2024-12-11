<%@ Page Title="" Language="C#" MasterPageFile="~/SiteEstruturaPropriedade.Master" AutoEventWireup="true" CodeBehind="CadastrarEstruturaPropriedade.aspx.cs" Inherits="FCarnauba_Animais.CadastrarEstruturaPropriedade" AspCompat="true" %>
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
                if (Request.Params["estruturaPropriedadeId"] != "") {
                    param = Request.Params["estruturaPropriedadeId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "CadastrarEstruturaPropriedade.aspx?estruturaPropriedadelId=" + param
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

        if ($("#txtData").length > 0) $("#txtData").mask("99/99/9999");

        if ($("#txtDataPastagem").length > 0) $("#txtDataPastagem").mask("99/99/9999");

        if ($("#txtDataAgricultura").length > 0) $("#txtDataAgricultura").mask("99/99/9999");

        if ($("#txtDataBenfeitoria").length > 0) $("#txtDataBenfeitoria").mask("99/99/9999");

        if ($("#txtDataArrendamento").length > 0) $("#txtDataArrendamento").mask("99/99/9999");

        if ($("#txtDataOutra").length > 0) $("#txtDataOutra").mask("99/99/9999");

        var numSettings = { aSep: '.', aDec: ',', aSign: '' };

        if ($("#txtArea").length > 0) $("#txtArea").autoNumeric(numSettings);
        if ($("#txtArea").length > 0) $("#txtArea").autoNumericSet(parseFloat($("#txtArea").val().replace(",", ".")));

        if ($("#txtAreaUtilizada").length > 0) $("#txtAreaUtilizada").autoNumeric(numSettings);
        if ($("#txtAreaUtilizada").length > 0) $("#txtAreaUtilizada").autoNumericSet(parseFloat($("#txtAreaUtilizada").val().replace(",", ".")));

        if ($("#txtReserva").length > 0) $("#txtReserva").autoNumeric(numSettings);
        if ($("#txtReserva").length > 0) $("#txtReserva").autoNumericSet(parseFloat($("#txtReserva").val().replace(",", ".")));

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
            <legend>Dados da Propriedade&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
        <tr>
            <td class="rotulo">
                Nome:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtNome" Width="300px" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvNome" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvNome_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Localização:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtLocalizacao" Width="300px" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvLocalizacao" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLocalizacao_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Registro Oficial:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtRegistroOficial" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvRegistroOficial" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvRegistroOficial_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtData" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvData" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvData_ServerValidate"></asp:customvalidator>
	            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Área:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtArea" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvArea" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvArea_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Área Utilizada:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtAreaUtilizada" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvAreaUtilizada" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvAreaUtilizada_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Reserva:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtReserva" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvReserva" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvReserva_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Atividades:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtAtividades" TextMode="MultiLine" Width="300px" Rows="5" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvAtividades" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvAtividades_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                Município:
            </td>
            <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtMunicipio" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvMunicipio" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvMunicipio_ServerValidate"></asp:customvalidator>
                </td>
        </tr>
        <tr>
            <td class="rotulo">
                UF:
            </td>
            <td width="80%">
                    <div class="inputDiv">
                        <asp:DropDownList ID="ddlUf" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem>AC</asp:ListItem>
                                        <asp:ListItem>AL</asp:ListItem>
                                        <asp:ListItem>AP</asp:ListItem>
                                        <asp:ListItem>AM</asp:ListItem>
                                        <asp:ListItem>BA</asp:ListItem>
                                        <asp:ListItem>CE</asp:ListItem>
                                        <asp:ListItem>DF</asp:ListItem>
                                        <asp:ListItem>ES</asp:ListItem>
                                        <asp:ListItem>GO</asp:ListItem>
                                        <asp:ListItem>MA</asp:ListItem>
                                        <asp:ListItem>MT</asp:ListItem>
                                        <asp:ListItem>MS</asp:ListItem>
                                        <asp:ListItem>MG</asp:ListItem>
                                        <asp:ListItem>PA</asp:ListItem>
                                        <asp:ListItem>PB</asp:ListItem>
                                        <asp:ListItem>PR</asp:ListItem>
                                        <asp:ListItem>PE</asp:ListItem>
                                        <asp:ListItem>PI</asp:ListItem>
                                        <asp:ListItem>RJ</asp:ListItem>
                                        <asp:ListItem>RN</asp:ListItem>
                                        <asp:ListItem>RS</asp:ListItem>
                                        <asp:ListItem>RO</asp:ListItem>
                                        <asp:ListItem>RR</asp:ListItem>
                                        <asp:ListItem>SC</asp:ListItem>
                                        <asp:ListItem>SP</asp:ListItem>
                                        <asp:ListItem>SE</asp:ListItem>
                                        <asp:ListItem>TO</asp:ListItem>   
                            </asp:DropDownList>
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
        <div id="pastagem-tab">
             <uc3:UserControlPastagem ID="UserControlPastagem1" runat="server" EditMode="false" EstruturaPropriedadeId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" />
            
        </div>
        <div id="agricultura-tab">
             <uc5:UserControlAgricultura ID="UserControlAgricultura1" runat="server" EditMode="false" EstruturaPropriedadeId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" />
            
        </div>
        <div id="benfeitoria-tab">
             <uc6:UserControlBenfeitoria ID="UserControlBenfeitoria1" runat="server" EditMode="false" EstruturaPropriedadeId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" />
            
        </div>
        <div id="arrendamento-tab">
             <uc7:UserControlArrendamento ID="UserControlArrendamento1" runat="server" EditMode="false" EstruturaPropriedadeId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" />
            
        </div>
        <div id="outra-tab">
             <uc8:UserControlOutra ID="UserControlOutra1" runat="server" EditMode="false" EstruturaPropriedadeId="<%# Convert.ToInt32(EstruturaPropriedadeId) %>" />
            
        </div>
</div>
</asp:Content>
