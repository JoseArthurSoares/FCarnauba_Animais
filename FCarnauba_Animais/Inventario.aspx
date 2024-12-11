<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="FCarnauba_Animais.Inventario" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="./Styles/geral000.css" rel="stylesheet" type="text/css" />
    <link href="./Styles/reset000.css" rel="stylesheet" type="text/css" />
    
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
    <style type="text/css">
        .gridlinha01 {
	        height:25px;
	        background-color:#FFFFFF;
	        font-weight: bold;
	    }
	    .gridlinha02 {
	        height:25px;
	        background-color:#dedede;
	        font-weight: bold;
        }
        .center {
            margin: auto;
            width: 50%;
            padding: 10px;
        }
        
        .modal {
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
        .loading {
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
            ShowProgress();
        });

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
</head>
<body>

<div id="topo">
        <div class="conteudo2">
            
                <img id="logoGov" src="./img/logoFaze.png">
            
            
                <img id="logoSigePb" src="./img/logoLisa.png">
            
        </div>
    </div>
    <div class="loading" align="center">
            Processando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scriptManager" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
    <uc2:Mensagem ID="mensagem" runat="server" />
    <div>
        <table>
    <tr>
        <td>Propriedade:</td>
        <td>
            <asp:DropDownList ID="ddlPropriedade" Name="ddlPropriedade" runat="server" Width="200" ViewStateMode="Enabled">
            </asp:DropDownList>
        </td>
        <td>
							Período:</td>
						<td>
							
                                    <asp:TextBox ID="txtDataInicio" runat="server" ClientIDMode="Static" MaxLength="10" Width=80px name="txtDataInicio"  class="datepick"></asp:TextBox> a <asp:TextBox ID="txtDataFim" runat="server" ClientIDMode="Static" MaxLength="10" Width=80px name="txtDataFim"  class="datepick"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
            OnClick="btnPesquisar_Click" BackColor="#052B5C" ForeColor="White" />
            
        </td>
    </tr>
</table>
        <table width="100%">
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:20%;background-color:#194069;color:#FFFFFF;">INFORMAÇÕES GERAIS</th>
                        </tr>
                        <tr>
                            <td colspan="2" scope="col" class="gridlinha02">LOCALIZAÇÃO: <asp:Label ID="lblLocalizacao" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">ÁREA TOTAL (HA)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblArea" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">ÁREA PASTAGENS (HA)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblPastagens" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">ÁREA DE AGRICULTURA (HA)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblAgricultura" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">PLUVIOMETRIA (mm)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblPluviometria" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:30%;background-color:#194069;color:#FFFFFF;">INDÍCES ZOOTÉCNICOS</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">LOTAÇÃO (UA/HA)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblLotacao" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA FERTILIDADE</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblTaxaFertilidade" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TAXA NATALIDADE</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblTaxaNatalidade" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA MORTALIDADE - BEZERROS (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lbltaxaMortalidadeBezerros" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA MORTALIDADE - ANIMAIS JOVENS (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lbltaxaMortalidadeAnimaisJovens" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA MORTALIDADE - ANIMAIS ADULTOS (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lbltaxaMortalidadeAnimaisAdultos" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">GMD GLOBAL (KG/VAB/DIA)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblGmdGlobal" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE DESMAME (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblTaxaDesmame" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TAXA DE DESFRUTE (%)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblTaxaDesfrute" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE CRESCIMENTO VEGETATIVO SINDI (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblTaxaCrescimentoVegetativoSindi" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE CRESCIMENTO VEGETATIVO GUZERÁ (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblTaxaCrescimentoVegetativoGuzera" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE CRESCIMENTO VEGETATIVO CPD (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblTaxaCrescimentoVegetativoCPD" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">PRODUÇÃO DE LEITE (KG)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblProcucaoLeite" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">RQ MÉDIO (%)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblRQMedio" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:25%;background-color:#194069;color:#FFFFFF;">INFORMAÇÕES FINANCEIRAS (R$)</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">RECEITA</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblEntradas" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">CUSTO</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblDesembolsos" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">CUSTO ALIMENTAR</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblCustoAlimentar" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">CUSTOS FIXOS</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblCustosFixos" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">CUSTOS VARIÁVEIS</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblCustosVariaveis" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">CUSTO ADMINISTRATIVO</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblCustoAdministrativo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">CUSTO TRIBUTÁRIO</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblCustoTributario" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:25%;background-color:#194069;color:#FFFFFF;">ÍNDICES ECONÔMICOS</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">CUSTO/CAB (R$/KG)</td>
                            <td scope="col" class="gridlinha02" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">CALIMCAB/PERÍODO (R$/KG)</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">APLEITE (R$)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblApLeite" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">APLEITE (% TOTAL RECEITA)</td>
                            <td scope="col" class="gridlinha01" align="right"><asp:Label ID="lblApLeitePerReceita" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">APLEITE (% TOTAL CUSTO)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblApLeitePerCusto" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
                <table width="100%">
            <tr>
                
                <td valign="top" align="center" style="width:33.33%">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:20%;background-color:#194069;color:#FFFFFF;">REBANHO GUZERÁ</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TOTAL DE ANIMAIS</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblGuzera" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td valign="top" align="center" style="width:33.33%">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:20%;background-color:#194069;color:#FFFFFF;">REBANHO SINDI</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TOTAL DE ANIMAIS</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblSindi" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                
                <td valign="top" align="center" style="width:33.33%">
                    <table>
                        <tr>
                            <th colspan="2" scope="col" style="width:20%;background-color:#194069;color:#FFFFFF;">REBANHO CPD</th>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TOTAL DE ANIMAIS</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblCpd" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
                </tr>

                </table>
       
                <table width="100%">

                <tr>

                <td valign="top" align="center" style="width:8%">
                        &nbsp;
                </td>
                <td valign="top" align="center" style="width:84%">
                    <rsweb:reportviewer ID="reportViewerPluviometria" runat="server" 
                        Style="width: 100%" ShowToolBar="False">
                        </rsweb:reportviewer>
                </td>
                <td valign="top" align="center" style="width:8%">
                    &nbsp;
                </td>

            </tr>
        </table>

                
                                    
    </div>
    </form>
</body>
</html>
