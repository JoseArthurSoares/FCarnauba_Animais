<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SimuladorEndogamia.aspx.cs" Inherits="FCarnauba_Animais.SimuladorEndogamia" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
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
        .labelendogamia {
            background-color: #052B5C;
            color: white;
    }
    
    .labelarvore {
        font-weight: normal;
        vertical-align: top;
        text-align: left;
        padding: 2px;
    }
    .gridtitulo {
        height: 25px;
        background-color: #194069;
        padding-top: 5px;
        color: #FFFFFF;
        font-weight: bold;
        text-align:center;
    }
    
    .gridlinha01 {
        height: 25px;
        background-color: #FFFFFF;
        padding-top: 5px;
    }
    
    .gridlinha02 {
        height: 25px;
        background-color: #dedede;
        padding-top: 5px;
    }
    
    .valor {
       text-align:left;
       color:Red;
    }
    
    .tabela {
       align-content: center;
       width:100%;
    }
    
    #chart_div{
            
            
            
            width:100%;
        }
    
    .myNodeClass 
    {
        text-align: center;
        vertical-align: middle;
        font-family: arial,helvetica;
        cursor: default;
        border: 2px solid #b5d9ea;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        -webkit-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
        -moz-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
        background-color: #b0d7ee;
        
        writing-mode: vertical-rl;
    }
</style>
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

        google.charts.load('current', { packages: ["orgchart"] });
        google.charts.setOnLoadCallback(drawChart);

</script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            //ShowProgress();
        });
        </script>
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
    <uc4:Mensagem ID="mensagem" runat="server" />
    <div class="barra02" align="left">
			Simulador de Endogamia</div>
            <table>
					<tr>
						<td>
							Embrião:</td>
						<td>
							<asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
						</td>
                        <td>
							Raça:</td>
						<td>
                            <asp:DropDownList ID="ddlRacas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRacas_SelectedIndexChanged">
                                <asp:ListItem>SINDI</asp:ListItem>
                                <asp:ListItem>ZEBUÍNAS</asp:ListItem>
                                <asp:ListItem>GUZERÁ</asp:ListItem>
                                <asp:ListItem>CURRALEIRO PÉ DURO</asp:ListItem>
                            </asp:DropDownList>
						</td> 
                        <td>
							Pai:</td>
						<td>
                            <cc1:TextBoxWatermarkExtender ID="twePesquisaPai" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaPai" WatermarkText="RGD ou Nome do pai" runat="server">
                            </cc1:TextBoxWatermarkExtender>
                            <asp:DropDownList ID="ddlPais" runat="server" Width="300px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPesquisaPai" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPesquisarDDlPai" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlPai_Click" />
						</td>
                        <td>
							Mãe:</td>
						<td>
                            <cc1:TextBoxWatermarkExtender ID="twePesquisaMae" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaMae" WatermarkText="RGD ou Nome da mãe" runat="server">
                            </cc1:TextBoxWatermarkExtender>
                            <asp:DropDownList ID="ddlMaes" runat="server" Width="300px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtPesquisaMae" runat="server"></asp:TextBox>
                            <asp:Button ID="btnPesquisarDDlMae" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlMae_Click" />
						</td>
                        <td class="rotulo">
                            Nível:
                        </td>
                        <td>
                            <div class="inputDiv"><asp:DropDownList ID="ddlNivel" runat="server" Height="18px" Width="40" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem Selected>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            </asp:DropDownList></div>
                        </td> 
                        <td>
                            <asp:Button ID="btnSimular" runat="server" CssClass="botao" Text="Simular" OnClick="btnSimular_Click" />
						</td> 
                    </tr>
            </table>
            <asp:Panel ID="pnlEndogamia" runat="server" Visible="false">

        <div align="left">
            <%
                        if (animal != null)
                        {
                    %>
            <table width="100%">
                <tr class="gridtitulo">
                    <td colspan="2" class="gridtituloCenter">RESULTADO SIMULAÇÃO DA ENDOGAMIA</td>
                </tr>
                <tr class="gridlinha01">
                    <td>EMBRIÃO</td>
                    <td>
                    
                    <%=animal.NomeCompleto%>
                    
                    </td>

                </tr>
                <tr class="gridlinha01">
                    <td>PAI</td>
                    <td>
                    <%=paiDdl.NomeCompleto%>
                    </td>
                </tr>
                <tr class="gridlinha01">
                    <td>MÃe</td>
                    <td><%=maeDdl.NomeCompleto%></td>
                </tr>
                <tr class="gridlinha02">
                    <td>ENDOGAMIA</td>
                    <td class="valor"><b><%=Endogamia.ToString("N12")%></b></td>
                </tr>
            </table>
            <% } %>
            <div id="chart_div" style = "width: 650px; margin: 0 auto">

            </div>
        </div>
            </asp:Panel>
</asp:Content>
