<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastrarAnimal.aspx.cs" Inherits="FCarnauba_Animais.CadastrarAnimal" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlHistorico.ascx" TagName="UserControlHistorico"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/UserControlMensuracaoAnimal.ascx" TagName="UserControlMensuracaoAnimal"
    TagPrefix="uc5" %>
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
                if (Request.Params["animalId"] != "") {
                    param = Request.Params["animalId"];
                } else {
                    param = Request.Params["id"];
                }

                var targUrl = "CadastrarAnimal.aspx?animalId=" + param
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
        if ($("#txtDataHistorico").length > 0) $("#txtDataHistorico").mask("99/99/9999");
        if ($("#txtDataPesagem").length > 0) $("#txtDataPesagem").mask("99/99/9999");
        var numSettings = { aSep: '.', aDec: ',', aSign: '' };

        if ($("#txtPn").length > 0) $("#txtPn").autoNumeric(numSettings);
        if ($("#txtPn").length > 0) $("#txtPn").autoNumericSet(parseFloat($("#txtPn").val().replace(",", ".")));

        if ($("#txtPeso").length > 0) $("#txtPeso").autoNumeric(numSettings);
        if ($("#txtPeso").length > 0) $("#txtPeso").autoNumericSet(parseFloat($("#txtPeso").val().replace(",", ".")));

        if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumeric(numSettings);
        if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumericSet(parseFloat($("#txtCEscrotal").val().replace(",", ".")));

        if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumeric(numSettings);
        if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumericSet(parseFloat($("#txtAAnterior").val().replace(",", ".")));

        if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumeric(numSettings);
        if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumericSet(parseFloat($("#txtAPosterior").val().replace(",", ".")));

        if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumeric(numSettings);
        if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumericSet(parseFloat($("#txtLGarupa").val().replace(",", ".")));

        if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumeric(numSettings);
        if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumericSet(parseFloat($("#txtCGarupa").val().replace(",", ".")));

        if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumeric(numSettings);
        if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumericSet(parseFloat($("#txtCCorporal").val().replace(",", ".")));

        if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumeric(numSettings);
        if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumericSet(parseFloat($("#txtPToracico").val().replace(",", ".")));

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
            <li><a href="#animal-tab">Cadastro</a></li>
            <li><a href="#historico-tab">Histórico</a></li>
            <li><a href="#mensuracao-tab">Mensurações</a></li>
        </ul>
        <div id="animal-tab">
        <fieldset class="cadastrar_info">
            <legend>Dados do Animal&nbsp;&nbsp;</legend><span class="style1">Preencher os dados obrigatórios em branco (marcados com um *)</span><br><br>
        <table width="100%">
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
		        Número de Ordem:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtNumeroOrdem" runat="server" width="97px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:textbox>
		        </div>
		        <asp:customvalidator id="cvNumeroOrdem" runat="server" forecolor="Red" onservervalidate="cvNumeroOrdem_ServerValidate" validationgroup="animal"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Nome:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtNome" runat="server" width="200px"></asp:textbox>
		        </div>
		        <asp:customvalidator id="cvNome" runat="server" forecolor="Red" onservervalidate="cvNome_ServerValidate" validationgroup="animal"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Sexo:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlSexo" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>Fêmea</asp:ListItem>
                    <asp:ListItem>Macho</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgn Série:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtRgnSerie" runat="server" width="100px"></asp:textbox>
		        </div>
                <asp:customvalidator id="cvRgnSerie" runat="server" forecolor="Red" onservervalidate="cvRgnSerie_ServerValidate" validationgroup="animal"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgn Número:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtRgnNumero" runat="server" width="100px"></asp:textbox>
		        </div>
                <asp:customvalidator id="cvRgnNumero" runat="server" forecolor="Red" onservervalidate="cvRgnNumero_ServerValidate" validationgroup="animal"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgn OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckRgnOK" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgd Série:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtRgdSerie" runat="server" width="100px"></asp:textbox>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgd Número:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtRgdNumero" runat="server" width="100px"></asp:textbox>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Rgd OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckRgdOk" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Data de Nascimento:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtDataNascimento" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvDataNascimento" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataNascimento_ServerValidate"></asp:customvalidator>
	            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Peso ao Nascer Kg:
	        </td>
	        <td>
		        <div class="inputDiv"><asp:TextBox ID="txtPn" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(...)</span></div>
                    <asp:customvalidator id="cvPn" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPn_ServerValidate"></asp:customvalidator>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Pai:
            </td>
            <td>
                <div class="inputDiv">
                    <asp:DropDownList ID="ddlPai" Name="ddlPai" runat="server" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
                <td class="rotulo">
                    &nbsp;
                </td>
                <td>
                    <div class="inputDiv">
                        <cc1:TextBoxWatermarkExtender ID="twePesquisaDdlPai" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaDdlPai" WatermarkText="RGD ou Nome do Pai" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:TextBox CssClass="textfield01" ID="txtPesquisaDdlPai" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisarDDlPai" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlPai_Click"  />
                    </div>

                </td>
            </tr>
            <tr>
            <td class="rotulo">
                Mãe:
            </td>
            <td>
                <div class="inputDiv">
                    <asp:DropDownList ID="ddlMae" Name="ddlMae" runat="server" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
                <td class="rotulo">
                    &nbsp;
                </td>
                <td>
                    <div class="inputDiv">
                        <cc1:TextBoxWatermarkExtender ID="twePesquisaDdlMae" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaDdlMae" WatermarkText="RGD ou Nome da Mãe" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:TextBox CssClass="textfield01" ID="txtPesquisaDdlMae" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisarDDlMae" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlMae_Click"  />
                    </div>

                </td>
            </tr>
        <tr>
	        <td class="rotulo">
		        CDN - Comunicação de Nascimento:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox id="txtCdnOrigem" runat="server" width="97px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);"></asp:textbox>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Foto:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadFoto" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato JPEG (.jpg).)</span>
                    <asp:customvalidator id="cvFoto" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvFoto_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo DNA:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoDna" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoDna" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoDna_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo DNA OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoDnaOk" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Arquivo Permanente:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoDna2" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoDna2" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoDna2_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Arquivo Permanente OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoDna2" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Secundário 1:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoDna3" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoDna3" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoDna3_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Secundário 1 OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoDna3" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Secundário 2:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoDna4" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoDna4" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoDna4_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Secundário 2 OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoDna4" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Beta Caseína:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoBetaCaseina" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoBetaCaseina" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoBetaCaseina_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Tipo Beta Caseína:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTipoBetaCaseina" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>A1A1</asp:ListItem>
                    <asp:ListItem>A1A2</asp:ListItem>
                    <asp:ListItem>A2A2</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Beta Caseína OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoBetaCaseina" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Kappa Caseína:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:FileUpload ID="FileUploadLaudoKappaCaseina" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" />
                    <span class="style1">(Formato PDF (.pdf).)</span>
                    <asp:customvalidator id="cvLaudoKappaCaseina" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLaudoKappaCaseina_ServerValidate"></asp:customvalidator>
		        </div>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Tipo Kappa Caseína:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTipoKappaCaseina" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>A/A</asp:ListItem>
                    <asp:ListItem>A/B</asp:ListItem>
                    <asp:ListItem>B/B</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Laudo Kappa Caseína OK?
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckLaudoKappaCaseina" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Observações:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:textbox onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" id="txtObservacoes" runat="server" width="200px" TextMode="MultiLine"></asp:textbox>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        FIV:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckFiv" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
            <td class="rotulo">
                Receptora:
            </td>
            <td>
                <div class="inputDiv">
                    <asp:DropDownList ID="ddlReceptora" Name="ddlReceptora" runat="server" Width="400" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    </asp:DropDownList>
                </div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Tipo de Parto:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTipoParto" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>NORMAL</asp:ListItem>
                    <asp:ListItem>GEMELAR</asp:ListItem>
                    <asp:ListItem>PUXADO</asp:ListItem>
                    <asp:ListItem>NATIMORTO</asp:ListItem>
                    <asp:ListItem>ABORTO</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Vigor:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlVigorBezerro" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>MUITO BOM</asp:ListItem>
                    <asp:ListItem>BOM</asp:ListItem>
                    <asp:ListItem>MÉDIO</asp:ListItem>
                    <asp:ListItem>FRACO</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Estado Corporal da Mãe:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlEstadoCorporalMae" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>MUITO BOM</asp:ListItem>
                    <asp:ListItem>BOM</asp:ListItem>
                    <asp:ListItem>MÉDIO</asp:ListItem>
                    <asp:ListItem>FRACO</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Tamanho da Teta da Mãe:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlTamanhoTeta" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>P</asp:ListItem>
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>G</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
            <td class="rotulo">
                Produção de Leite da Mãe:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlMaeBoaLeite" runat="server" Height="18px" Width="100" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                    <asp:ListItem>BOM</asp:ListItem>
                    <asp:ListItem>MÉDIA</asp:ListItem>
                    <asp:ListItem>FRACA</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Mãe Ordenhada:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckMaeOrdenhada" runat="server"/>
		        </div>
	        </td>
        </tr>
        <tr>
	        <td class="rotulo">
		        Animal Improdutivo:
	        </td>
	        <td>
		        <div class="inputDiv">
			        <asp:CheckBox ID="ckAnimalImprodutivo" runat="server"/>
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
        <div id="historico-tab">
             <uc3:UserControlHistorico ID="UserControlHistorico1" runat="server" EditMode="false" AnimalId="<%# Convert.ToInt32(AnimalId) %>" />
        </div>
        <div id="mensuracao-tab">
             <uc5:UserControlMensuracaoAnimal ID="UserControlMensuracaoAnimal1" runat="server" EditMode="false" AnimalId="<%# Convert.ToInt32(AnimalId) %>" />
        </div>
</div>
</asp:Content>
