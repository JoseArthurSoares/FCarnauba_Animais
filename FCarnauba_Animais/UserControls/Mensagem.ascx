<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensagem.ascx.cs" Inherits="FCarnauba_Animais.UserControls.Mensagem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Panel ID="pnlMensagem" CssClass="area_mensagem" runat="server">
	<div class="mensagem_titulo">
	<asp:Image ID="imgMensagem" runat="server" ImageAlign="Left" ImageUrl="~/img/aviso.png" 
		EnableViewState="false" /><asp:Label ID="lblTituloMensagem" runat="server" EnableViewState="false"></asp:Label></div>
	<div class="mensagem">
		<asp:Label ID="lblMensagem" runat="server" EnableViewState="false"></asp:Label></div>
	<hr />
	<asp:Button ID="btnFecharMensagem" runat="server" Text="Fechar" CssClass="botao" />
	<asp:Button runat="server" ID="btnTargetControlEscondido" Style="display: none;" />
</asp:Panel>
<cc1:modalpopupextender id="mpeMensagem" runat="server" popupcontrolid="pnlMensagem"
	targetcontrolid="btnTargetControlEscondido" dropshadow="true" cancelcontrolid="btnFecharMensagem"
	backgroundcssclass="modalBackground"></cc1:modalpopupextender>
