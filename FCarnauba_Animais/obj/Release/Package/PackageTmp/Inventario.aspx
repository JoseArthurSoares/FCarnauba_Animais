<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inventario.aspx.cs" Inherits="FCarnauba_Animais.Inventario" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    </style>
</head>
<body>
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
							<cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDataInicio"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender><asp:TextBox ID="txtDataInicio" runat="server" ClientIDMode="Static" MaxLength="10" name="txtDataInicio"  class="date_size"></asp:TextBox> a <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDataFim"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender><asp:TextBox ID="txtDataFim" runat="server" ClientIDMode="Static" MaxLength="10" name="txtDataFim"  class="date_size"></asp:TextBox>
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
                            <td scope="col" class="gridlinha02" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA MORTALIDADE</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">GMD GLOBAL (KG/VAB/DIA)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblGmdGlobal" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">ER MÉDIA</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">IPP/IEP MÉDIOS (DIAS)</td>
                            <td scope="col" class="gridlinha02" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE DESMAME (%)</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">TAXA DE DESFRUTE (%)</td>
                            <td scope="col" class="gridlinha02" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">TAXA DE CRESCIMENTO VEGETATIVO (%)</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha02">PRODUÇÃO DE LEITE (KG)</td>
                            <td scope="col" class="gridlinha02" align="right"><asp:Label ID="lblProcucaoLeite" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td scope="col" class="gridlinha01">RQ MÉDIO (%)</td>
                            <td scope="col" class="gridlinha01" align="right"></td>
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
                <td valign="top" align="center" style="width:50%">
                    
                    <rsweb:reportviewer ID="reportViewerPluviometria" runat="server" 
                    Style="width: 100%" ShowToolBar="False">
                    </rsweb:reportviewer>
                </td>
                <td valign="top" align="center" style="width:16.66%">
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
                <td valign="top" align="center" style="width:16.66%">
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
                
                <td valign="top" align="center" style="width:16.66%">
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

                
                                    
    </div>
    </form>
</body>
</html>
