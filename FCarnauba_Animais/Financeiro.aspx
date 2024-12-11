<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="Financeiro.aspx.cs" Inherits="FCarnauba_Animais.Financeiro" AspCompat="true" %>
<%@ Register Src="~/UserControls/EscolhaDeGrupo.ascx" TagName="EscolhaDeGrupo" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="conteudo" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .cal_Theme .ajax__calendar_container div {
 padding: 0px;
 background-color: white;}
        .cal_Theme .ajax__calendar_container th {
 padding: 0px;
 background-color: white;}
 .cal_Theme .ajax__calendar_container td {
 padding: 0px;
 background-color: white;}
 .cal_Theme .ajax__calendar_month {
  padding-bottom: 3px;
  margin-top: -3.5px;
  background-color: white;} 
  </style>
    <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //if ($("#txtEditData").length > 0) $("#txtEditData").mask("99/99/9999");
            //if ($("#txtAddData").length > 0) $("#txtAddData").mask("99/99/9999");

            if ($("#txtDataInicio").length > 0) $("#txtDataInicio").mask("99/99/9999");
            if ($("#txtDataFim").length > 0) $("#txtDataFim").mask("99/99/9999");
            
            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtEditValorUnitario").length > 0) $("#txtEditValorUnitario").autoNumeric(numSettings);
            if ($("#txtEditValorUnitario").length > 0) $("#txtEditValorUnitario").autoNumericSet(parseFloat($("#txtEditValorUnitario").val().replace(",", ".")));

            if ($("#txtAddValorUnitario").length > 0) $("#txtAddValorUnitario").autoNumeric(numSettings);
            if ($("#txtAddValorUnitario").length > 0) $("#txtAddValorUnitario").autoNumericSet(parseFloat($("#txtAddValorUnitario").val().replace(",", ".")));

        });
    </script>    

	<asp:ScriptManager ID="scriptManager" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>
	
	<div>
		<div class="barra02" align="left">
			RECEITA/CUSTO</div>
		<asp:UpdatePanel ID="updatePanel" runat="server">
			<ContentTemplate>
				<uc2:Mensagem ID="mensagem" runat="server" />
				
			
				<table>
					<tr>
						<td>
							Descrição:</td>
						<td>
							<asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
						</td>
					
						<td>
							Propriedade:</td>
						<td>
                            <asp:DropDownList ID="ddlPropriedade" runat="server">
                            </asp:DropDownList>
						</td>
					
						<td>
							Período:</td>
						<td>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDataInicio"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
                             <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDataFim"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
							<asp:TextBox ID="txtDataInicio" runat="server" ClientIDMode="Static" name="txtDataInicio" placeholder="__/__/____" class="date_size"></asp:TextBox> a <asp:TextBox ID="txtDataFim" runat="server" ClientIDMode="Static" MaxLength="10" name="txtDataFim" placeholder="__/__/____" class="date_size"></asp:TextBox>
                            
						</td>

                        <td>
							Forn.\Cliente:</td>
						<td>
                            <asp:DropDownList ID="ddlFornCliente" runat="server">
                            </asp:DropDownList>
						</td>

                        <td>
							Venda?:</td>
						<td>
                            <asp:CheckBox ID="ckVenda" runat="server" />

                            <asp:Button ID="btnConsultar" runat="server" CssClass="botao" Text="Consultar" OnClick="btnConsultar_Click" />
                            
						</td>
					</tr>
					
				</table>
				<hr class="busca_barra_rodape" />
			
				<asp:GridView ID="gvFinanceiros" runat="server" AutoGenerateColumns="False" DataSourceID="financeirosDataSource" Width="100%"
					DataKeyNames="IdFinanceiro" AllowPaging="True" ShowFooter="True" OnRowCommand="gvFinanceiros_RowCommand"
					OnRowDataBound="gvFinanceiros_RowDataBound" OnRowUpdating="gvFinanceiros_RowUpdating" OnPageIndexChanged="gvFinanceiros_PageIndexChanged" OnRowDeleted="gvFinanceiros_RowDeleted" OnRowUpdated="gvFinanceiros_RowUpdated" PagerSettings-Mode="NumericFirstLast">
					<RowStyle CssClass="gridlinha01" />
					<AlternatingRowStyle CssClass="gridlinha02" />
					<HeaderStyle CssClass="gridtitulo" />
					<FooterStyle CssClass="gridrodape" />
					<EmptyDataTemplate>
						<table>
							<tr class="gridtitulo">
                                
								<th>
									Descrição
								</th>
                                <th>
									Fornecedor/Cliente
								</th>
								<th>
									Propriedade(s)
								</th>
								<th>
									Grupo</th>
                                <th>
									Quant.
								</th>
								<th>
									V. Unitário/Méd.
                                </th>
                                <th>
									Data
								</th>
                                <th>
									Venda?
								</th>
                                <th>
									F. Pag.
								</th>
								<th>
								</th>
							</tr>
							<tr class="gridlinha01">
                                
								<td>
									<cc1:TextBoxWatermarkExtender ID="tweAddDescricao" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddDescricao" WatermarkText="Descrição" runat="server">
									</cc1:TextBoxWatermarkExtender>
                                    <asp:DropDownList ID="ddlAddItemDescricao" ValidationGroup="AddGroup" DataTextField="Descricao"
										DataValueField="Descricao" DataSourceID="itensDescricaoDataSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddItemDescricao_SelectedIndexChanged" Width="200px">
									</asp:DropDownList> 
									<asp:TextBox CssClass="textfield01" ID="txtAddDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="195px"></asp:TextBox>
									</td>
                                <td>
									<asp:DropDownList ID="ddlAddIdEmpresa" ValidationGroup="AddGroup" DataTextField="RazaoSocial"
										DataValueField="IdEmpresa" DataSourceID="empresasDataSource" runat="server" Width="180px">
									</asp:DropDownList>
									<asp:RequiredFieldValidator ID="vldtAddIdEmpresa" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlAddIdEmpresa"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddIdEmpresa" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddIdEmpresa">
									</cc1:ValidatorCalloutExtender>
									</td>
								<td>
									<asp:DropDownList ID="ddlAddIdPropriedade" ValidationGroup="AddGroup" DataTextField="Nome"
										DataValueField="IdsPropriedades" DataSourceID="propriedadesDataSource" runat="server">
									</asp:DropDownList>
									<asp:RequiredFieldValidator ID="vldtAddIdPropriedade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlAddIdPropriedade"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddIdPropriedade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddIdPropriedade">
									</cc1:ValidatorCalloutExtender>
									</td>
								<td>
									<asp:Label ID="lblAddGrupo" runat="server" Text="Grupo..." ToolTip="Clique para escolher o grupo" CssClass="botao_grupos"></asp:Label>
									<cc1:PopupControlExtender ID="pceAddGrupo" Position="Bottom" PopupControlID="pnlAddGrupo"
										TargetControlID="lblAddGrupo" runat="server">
									</cc1:PopupControlExtender>
									<asp:Panel ID="pnlAddGrupo" runat="server" CssClass="panel_grupo">
										<h3>Escolha o grupo</h3>
										<uc1:EscolhaDeGrupo ID="addGrupo" OnGrupoSelecionado="addGrupo_GrupoSelecionado" runat="server"></uc1:EscolhaDeGrupo>
									</asp:Panel>
								</td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddQuantidade" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddQuantidade" WatermarkText="Quant." runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddQuantidade" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" Text='<%# Bind("Quantidade") %>'
										ValidationGroup="AddGroup" Width="40px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddQuantidade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddQuantidade"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddQuantidade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddQuantidade">
									</cc1:ValidatorCalloutExtender>
									</td>

                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddValorUnitario" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddValorUnitario" WatermarkText="V. Unitário/Méd." runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddValorUnitario" runat="server" Text='<%# Bind("ValorUnitario") %>'
										ValidationGroup="AddGroup" Width="70px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddValorUnitario" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValorUnitario"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddValorUnitario" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValorUnitario">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
                                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAddData"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
									<cc1:TextBoxWatermarkExtender ID="tweAddData" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddData" WatermarkText="Data" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("Data") %>' Width="70px" MaxLength="10"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
                                    <asp:CheckBox ID="ckAddVenda" runat="server" ValidationGroup="AddGroup" Checked='<%# Bind("VendaAnimais") %>' />
									
									</td>
                                <td>
									<asp:DropDownList ID="ddlAddFormaPagamento" ValidationGroup="AddGroup" runat="server" Width="170px" DataSourceID="aVistaParcDataSource">
                                        
									</asp:DropDownList>
									</td>
								<td>
									<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" ImageUrl="~/img/adicionar.png"
										AlternateText="Novo Financeiro" ValidationGroup="AddGroup" OnClick="ibtNew_Click" /></td>
							</tr>
						</table>
					</EmptyDataTemplate>
					<Columns>
                        <asp:TemplateField HeaderText="Nº" SortExpression="IdFinanceiro" HeaderStyle-Width="4%">
							
							<ItemTemplate>
								<asp:Label ID="lblIdFinanceiro" runat="server" Text='<%# Bind("IdFinanceiro") %>'></asp:Label>
							</ItemTemplate>
							
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Descri&#231;&#227;o" SortExpression="Descricao" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="195px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditDescricao" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditDescricao"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditDescricao" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditDescricao">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblDescricao" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddDescricao" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddDescricao" WatermarkText="Descrição" runat="server">
								</cc1:TextBoxWatermarkExtender>
                                <asp:DropDownList ID="ddlAddItemDescricao" ValidationGroup="AddGroup" DataTextField="Descricao"
										DataValueField="Descricao" DataSourceID="itensDescricaoDataSource" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAddItemDescricaoFooter_SelectedIndexChanged" Width="200px">
									</asp:DropDownList>
								<asp:TextBox CssClass="textfield01" ID="txtAddDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="195px"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Fornecedor/Cliente" SortExpression="IdEmpresa" HeaderStyle-Width="8%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditIdEmpresa" DataTextField="RazaoSocial" DataValueField="IdEmpresa"
									ValidationGroup="EditGroup" DataSourceID="empresasDataSource" runat="server" Width="180px">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="vldtEditIdEmpresa" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlEditIdEmpresa"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditIdEmpresa" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditIdEmpresa">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblIdEmpresa"  runat="server" Width="180px"></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddIdEmpresa" ValidationGroup="AddGroup" DataTextField="RazaoSocial"
									DataValueField="IdEmpresa" DataSourceID="empresasDataSource" runat="server" Width="180px">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="vldtAddIdEmpresa" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlAddIdEmpresa"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddIdEmpresa" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddIdEmpresa">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Propriedade(s)" SortExpression="PropriedadeComp" HeaderStyle-Width="7%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditIdPropriedade" DataTextField="Nome" DataValueField="IdsPropriedades"
									ValidationGroup="EditGroup" DataSourceID="propriedadesDataSource" runat="server">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="vldtEditIdPropriedade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlEditIdPropriedade"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditIdPropriedade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditIdPropriedade">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblIdPropriedade" runat="server"></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddIdPropriedade" ValidationGroup="AddGroup" DataTextField="Nome"
									DataValueField="IdsPropriedades" DataSourceID="propriedadesDataSource" runat="server">
								</asp:DropDownList>
								<asp:RequiredFieldValidator ID="vldtAddIdPropriedade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="ddlAddIdPropriedade"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddIdPropriedade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddIdPropriedade">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Grupo" SortExpression="IdGrupo" HeaderStyle-Width="13%">
							<EditItemTemplate>
								<asp:Label ID="lblEditGrupo" runat="server" Text="Grupo..." ToolTip="Clique para escolher o grupo" CssClass="botao_grupos"></asp:Label>
								<cc1:PopupControlExtender ID="pceEditGrupo" Position="Top" PopupControlID="pnlEditGrupo"
									TargetControlID="lblEditGrupo" runat="server">
								</cc1:PopupControlExtender>
								<asp:Panel ID="pnlEditGrupo" runat="server" CssClass="panel_grupo">
									<h3>Escolha o grupo</h3>
									<uc1:EscolhaDeGrupo ID="editGrupo" runat="server"></uc1:EscolhaDeGrupo>
								</asp:Panel>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblIdGrupo" runat="server"></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:Label ID="lblAddGrupo" runat="server" Text="Grupo..." CssClass="botao_grupos"></asp:Label>
								<cc1:PopupControlExtender ID="pceAddGrupo" Position="Bottom" PopupControlID="pnlAddGrupo"
									TargetControlID="lblAddGrupo" runat="server">
								</cc1:PopupControlExtender>
								<asp:Panel ID="pnlAddGrupo" runat="server" CssClass="panel_grupo">
									<h3>Escolha o grupo</h3>
									<uc1:EscolhaDeGrupo ID="addGrupo" OnGrupoSelecionado="addGrupo_GrupoSelecionado" runat="server"></uc1:EscolhaDeGrupo>
								</asp:Panel>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Quant." SortExpression="Quantidade" HeaderStyle-Width="4%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditQuantidade" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Text='<%# Bind("Quantidade") %>' Width="40px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditQuantidade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditQuantidade"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditQuantidade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditQuantidade">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblQuantidade" runat="server" Text='<%# Bind("Quantidade") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddQuantidade" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddQuantidade" WatermarkText="Quant." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddQuantidade" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" Text='<%# Bind("Quantidade") %>' Width="40px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddQuantidade" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddQuantidade"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddQuantidade" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddQuantidade">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="V. Unitário/Méd." SortExpression="ValorUnitario" HeaderStyle-Width="7%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditValorUnitario" runat="server" Text='<%# Bind("ValorUnitario") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditValorUnitario" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditValorUnitario"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditValorUnitario" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditValorUnitario">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblValorUnitario" runat="server" Text='<%# string.Format("{0:N2}", Eval("ValorUnitario")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddValorUnitario" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddValorUnitario" WatermarkText="V. Unitário/Méd." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddValorUnitario" runat="server" Text='<%# Bind("ValorUnitario") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddValorUnitario" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValorUnitario"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddValorUnitario" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValorUnitario">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Data" SortExpression="Data" HeaderStyle-Width="5%">
							<EditItemTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditData"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
								<asp:TextBox CssClass="textfield01" ID="txtEditData" runat="server" Text='<%# Bind("Data") %>' Width="70px" MaxLength="10"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditData"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditData">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblData" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("Data")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAddData"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
								<cc1:TextBoxWatermarkExtender ID="tweAddData" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddData" WatermarkText="Data" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("Data") %>' Width="70px" MaxLength="10"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>  
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Venda?" SortExpression="VendaAnimais" HeaderStyle-Width="3%">
							<EditItemTemplate>
                                <asp:CheckBox ID="ckEditVenda" runat="server" ValidationGroup="EditGroup" Checked='<%# Bind("VendaAnimais") %>'  />

							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblVenda" runat="server" Width="60px" Text='<%# Bind("VendaAnimais") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
                                <asp:CheckBox ID="ckAddVenda" runat="server" ValidationGroup="AddGroup" Checked='<%# Bind("VendaAnimais") %>' />
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="À V./Parc." SortExpression="FormaPagamento" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditFormaPagamento"
									ValidationGroup="EditGroup" runat="server" Width="70px" DataSourceID="aVistaParcDataSource">
                                    
								</asp:DropDownList>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblFormaPagamento" runat="server" Width="60px" Text='<%# Bind("FormaPagamento") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddFormaPagamento" ValidationGroup="AddGroup"
									runat="server" Width="70px" DataSourceID="aVistaParcDataSource">
                                    
								</asp:DropDownList>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="V. Total" SortExpression="ValorTotal" HeaderStyle-Width="6%">
							
							<ItemTemplate>
								<asp:Label ID="lblValorTotal" runat="server" Text='<%# string.Format("{0:N2}", Eval("ValorTotal")) %>'></asp:Label>
							</ItemTemplate>
							
						</asp:TemplateField>
						<asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:ImageButton ID="ibtUpdate" runat="server" ImageUrl="~/img/bt_confirma_azul.gif"
									CausesValidation="True" CommandName="Update" AlternateText="Confirmar Alteração"
									ValidationGroup="EditGroup" />&nbsp;<asp:ImageButton ID="ibtCancel" ImageUrl="~/img/bt_cancela_azul.gif"
										runat="server" CausesValidation="False" CommandName="Cancel" AlternateText="Cancelar" />
							</EditItemTemplate>
							<ItemTemplate>
                                <asp:ImageButton ID="ibtDetail" runat="server" CausesValidation="False" CommandName="Detalhes"
									ImageUrl="~/img/details_icon.gif" AlternateText="Detalhes" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IdFinanceiro") %>' OnClick="ibtDetail_Click" />
                                
								<asp:ImageButton ID="ibtEdit" runat="server" CausesValidation="False" CommandName="Edit"
									ImageUrl="~/img/edit_icon.gif" AlternateText="Editar" />&nbsp;<asp:ImageButton
										ID="ibtDelete" runat="server" ImageUrl="~/img/delete_icon.png" OnClientClick="return confirm('Deseja realmente remover?');"
										CausesValidation="False" CommandName="Delete" AlternateText="Remover" />
							</ItemTemplate>
							<FooterTemplate>
								<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" CommandName="New"
									ImageUrl="~/img/adicionar.png" AlternateText="Novo Produto" ValidationGroup="AddGroup" />
							</FooterTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				<asp:ObjectDataSource ID="financeirosDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.ItemFinanceiro"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemFinanceiros" TypeName="FCarnauba_Animais.DataSources.DataSourceFinanceiros"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="financeirosDataSource_Deleted"
					OnUpdated="financeirosDataSource_Updated" OnSelecting="financeirosDataSource_Selecting">
				</asp:ObjectDataSource>
				<asp:ObjectDataSource ID="propriedadesDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Propriedade"
					SelectMethod="ObtemPropriedadesComp" TypeName="FCarnauba_Animais.DataSources.DataSourcePropriedades"
					OldValuesParameterFormatString="original_{0}">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="itensDescricaoDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.ItemDescFinanceiro"
					SelectMethod="ObtemItensDescricao" TypeName="FCarnauba_Animais.DataSources.DataSourceItensDescricao"
					OldValuesParameterFormatString="original_{0}">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="empresasDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Empresa"
					SelectMethod="ObtemEmpresas" TypeName="FCarnauba_Animais.DataSources.DataSourceEmpresas"
					OldValuesParameterFormatString="original_{0}">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="aVistaParcDataSource" runat="server"
					SelectMethod="ObtemParcelas" TypeName="FCarnauba_Animais.DataSources.DataSourceAVistaParc"
					OldValuesParameterFormatString="original_{0}">
				</asp:ObjectDataSource>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
