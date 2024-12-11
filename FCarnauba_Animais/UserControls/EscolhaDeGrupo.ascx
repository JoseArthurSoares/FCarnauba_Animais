<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EscolhaDeGrupo.ascx.cs" Inherits="FCarnauba_Animais.UserControls.EscolhaDeGrupo" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
    <asp:TextBox CssClass="textfield01" ID="txtBuscaGrupo" Style="display: inline;" runat="server"></asp:TextBox>
<asp:LinkButton ID="btnBuscar" Style="display: inline;" runat="server" OnClick="lbtBuscar_Click" CausesValidation="false"
	Text="Buscar" CssClass="linkbutton"></asp:LinkButton>
<ignav:UltraWebTree ID="uwtGrupos" runat="server" BorderStyle="None" BorderWidth="0px"
	Editable="False" AutoPostBack="True" Indentation="20" Width="600px" Height="1000px"
	BorderColor="#CCCCCC" DefaultImage="" LoadOnDemand="Manual" OnNodeExpanded="uwtGrupos_NodeExpanded" OnNodeSelectionChanged="uwtGrupos_NodeSelectionChanged">
	<SelectedNodeStyle ForeColor="Black" BackColor="#CCCCCC"></SelectedNodeStyle>
	<NodePaddings Bottom="2px" Left="2px" Top="2px" Right="2px"></NodePaddings>
	<HoverNodeStyle CustomRules="color: blue;" />
	<Padding Top="6px"></Padding>
	<Levels>
		<ignav:Level Index="0"></ignav:Level>
	</Levels>
	<ClientSideEvents />
</ignav:UltraWebTree>
