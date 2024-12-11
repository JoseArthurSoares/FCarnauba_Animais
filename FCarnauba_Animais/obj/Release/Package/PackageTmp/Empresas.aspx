<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="FCarnauba_Animais.Empresas" AspCompat="true" %>
<%@ Register Src="~/UserControls/EscolhaDeGrupo.ascx" TagName="EscolhaDeGrupo" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="conteudo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //if ($("#txtEditData").length > 0) $("#txtEditData").mask("99/99/9999");
            //if ($("#txtAddData").length > 0) $("#txtAddData").mask("99/99/9999");

            //if ($("#txtDataInicio").length > 0) $("#txtDataInicio").mask("99/99/9999");
            //if ($("#txtDataFim").length > 0) $("#txtDataFim").mask("99/99/9999");

            //var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            //if ($("#txtEditValorUnitario").length > 0) $("#txtEditValorUnitario").autoNumeric(numSettings);
            //if ($("#txtEditValorUnitario").length > 0) $("#txtEditValorUnitario").autoNumericSet(parseFloat($("#txtEditValorUnitario").val().replace(",", ".")));

            //if ($("#txtAddValorUnitario").length > 0) $("#txtAddValorUnitario").autoNumeric(numSettings);
            //if ($("#txtAddValorUnitario").length > 0) $("#txtAddValorUnitario").autoNumericSet(parseFloat($("#txtAddValorUnitario").val().replace(",", ".")));

        });
    </script>    

	<asp:ScriptManager ID="scriptManager" runat="server">
	</asp:ScriptManager>
	
	<div>
		<div class="barra02" align="left">
			FORNECEDORES/CLENTES</div>
		<asp:UpdatePanel ID="updatePanel" runat="server">
			<ContentTemplate>
				<uc2:Mensagem ID="mensagem" runat="server" />
				
			
				<table>
					<tr>
						<td>
							Busca:</td>
						<td>
							<asp:TextBox ID="txtDescricao" runat="server"></asp:TextBox>
                        </td>
                        <td>
							UF:</td>
						<td>
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
						</td>
                        <td>
                            <asp:Button ID="btnConsultar" runat="server" CssClass="botao" Text="Consultar" OnClick="btnConsultar_Click" />
						</td>
					</tr>
					
				</table>
				<hr class="busca_barra_rodape" />
			
				
			<asp:GridView ID="gvEmpresas" runat="server" AutoGenerateColumns="False" DataSourceID="empresasDataSource" Width="100%"
					DataKeyNames="IdEmpresa" AllowPaging="True" ShowFooter="True" OnRowCommand="gvEmpresas_RowCommand"
					OnRowDataBound="gvEmpresas_RowDataBound" OnRowUpdating="gvEmpresas_RowUpdating" PagerSettings-Mode="NumericFirstLast">
					<RowStyle CssClass="gridlinha01" />
					<AlternatingRowStyle CssClass="gridlinha02" />
					<HeaderStyle CssClass="gridtitulo" />
					<FooterStyle CssClass="gridrodape" />
					<EmptyDataTemplate>
						<table>
							<tr class="gridtitulo">
                                <th>
									Razão Social/Nome
								</th>
                                <th>
									Tipo
								</th>
								<th>
									CNPJ/CPF
								</th>
                                <th>
									Insc. Estadual/RG
								</th>
                                <th>
									Endereço
								</th>
                                <th>
									UF
								</th>
                                <th>
									Telefone
								</th>
                                <th>
									Email
								</th>
								<th>
								</th>
							</tr>
							<tr class="gridlinha01">
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddRazaoSocial" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddRazaoSocial" WatermarkText="Razão Social/Nome" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddRazaoSocial" runat="server" Text='<%# Bind("RazaoSocial") %>' Width="195px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldtAddRazaoSocial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddRazaoSocial"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddRazaoSocial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddRazaoSocial">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
									<asp:DropDownList ID="ddlAddTipo" ValidationGroup="AddGroup" runat="server" Width="200px">
                                        <asp:ListItem>Cliente</asp:ListItem>
                                        <asp:ListItem>Fornecedor</asp:ListItem>
                                        <asp:ListItem>P. de Serviço</asp:ListItem>
									</asp:DropDownList>
									</td>
								<td>
									<cc1:TextBoxWatermarkExtender ID="tweAddCnpjCpf" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddCnpjCpf" WatermarkText="CNPJ/CPF" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddCnpjCpf" runat="server" Text='<%# Bind("CnpjCpf") %>' Width="100px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="14"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldtAddCnpjCpf" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddCnpjCpf"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddCnpjCpf" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddCnpjCpf">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddInscEstadualRg" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddInscEstadualRg" WatermarkText="Insc. Estadual/RG" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddInscEstadualRg" runat="server" Text='<%# Bind("InscEstadualRg") %>' Width="100px" MaxLength="20"></asp:TextBox>
									</td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddEndereco" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddEndereco" WatermarkText="Endereço" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddEndereco" runat="server" Text='<%# Bind("Endereco") %>' Width="250px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldtAddEndereco" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddEndereco"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddEndereco" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddEndereco">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
									<asp:DropDownList ID="ddlAddUf" ValidationGroup="AddGroup" runat="server" Width="50px">
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
									</td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddTelefones" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddTelefones" WatermarkText="Telefone" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddTelefones" runat="server" Text='<%# Bind("Telefones") %>' Width="100px"></asp:TextBox>
									</td>
								<td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddEmail" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddEmail" WatermarkText="Email" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddEmail" runat="server" Text='<%# Bind("Email") %>' Width="150px"></asp:TextBox>
									</td>
								<td>
									<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" ImageUrl="~/img/bt_novo_azul.gif"
										AlternateText="Nova Empresa" ValidationGroup="AddGroup" OnClick="ibtNew_Click" /></td>
							</tr>
						</table>
					</EmptyDataTemplate>
					<Columns>
                        <asp:TemplateField HeaderText="Razão Social/Nome" SortExpression="RazaoSocial" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditRazaoSocial" runat="server" Text='<%# Bind("RazaoSocial") %>' Width="195px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtEditRazaoSocial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditRazaoSocial"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditRazaoSocial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditRazaoSocial">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblRazaoSocial" runat="server" Text='<%# Bind("RazaoSocial") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddRazaoSocial" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddRazaoSocial" WatermarkText="Razão Social/Nome" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddRazaoSocial" runat="server" Text='<%# Bind("RazaoSocial") %>' Width="195px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtAddRazaoSocial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddRazaoSocial"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddRazaoSocial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddRazaoSocial">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo" SortExpression="Tipo" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditTipo"
									ValidationGroup="EditGroup" runat="server" Width="100px">
                                    <asp:ListItem>Cliente</asp:ListItem>
                                    <asp:ListItem>Fornecedor</asp:ListItem>
                                    <asp:ListItem>P. de Serviço</asp:ListItem>
								</asp:DropDownList>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblTipo" runat="server" Width="100px" Text='<%# Bind("Tipo") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddTipo" ValidationGroup="AddGroup"
									runat="server" Width="100px">
                                    <asp:ListItem>Cliente</asp:ListItem>
                                    <asp:ListItem>Fornecedor</asp:ListItem>
                                    <asp:ListItem>P. de Serviço</asp:ListItem>
								</asp:DropDownList>
							</FooterTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="CNPJ/CPF" SortExpression="CnpjCpf" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditCnpjCpf" runat="server" Text='<%# Bind("CnpjCpf") %>' Width="100px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="14"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditCnpjCpf" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditCnpjCpf"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditCnpjCpf" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditCnpjCpf">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblCnpjCpf" runat="server" Text='<%# Bind("CnpjCpf") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddCnpjCpf" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddCnpjCpf" WatermarkText="CNPJ/CPF" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddCnpjCpf" runat="server" Text='<%# Bind("CnpjCpf") %>' Width="100px" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" MaxLength="14"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtAddCnpjCpf" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddCnpjCpf"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddCnpjCpf" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddCnpjCpf">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Insc. Estadual/RG" SortExpression="InscEstadualRg" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditInscEstadualRg" runat="server" Text='<%# Bind("InscEstadualRg") %>' Width="100px" MaxLength="20"></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblInscEstadualRg" runat="server" Text='<%# Bind("InscEstadualRg") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddInscEstadualRg" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddInscEstadualRg" WatermarkText="I. Estadual/RG" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddInscEstadualRg" runat="server" Text='<%# Bind("InscEstadualRg") %>' Width="100px"  MaxLength="20"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Endereço" SortExpression="Endereco" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditEndereco" runat="server" Text='<%# Bind("Endereco") %>' Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtEditEndereco" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditEndereco"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditEndereco" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditEndereco">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblEndereco" runat="server" Text='<%# Bind("Endereco") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddEndereco" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddEndereco" WatermarkText="Endereço" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddEndereco" runat="server" Text='<%# Bind("Endereco") %>' Width="250px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtAddEndereco" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddEndereco"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddEndereco" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddEndereco">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="UF" SortExpression="Uf" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditUf"
									ValidationGroup="EditGroup" runat="server" Width="50px">
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
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblUf" runat="server" Width="50px" Text='<%# Bind("Uf") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddUf" ValidationGroup="AddGroup"
									runat="server" Width="50px">
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
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Telefone" SortExpression="Telefones" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditTelefones" runat="server" Text='<%# Bind("Telefones") %>' Width="100px"></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblTelefones" runat="server" Text='<%# Bind("Telefones") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddTelefones" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddTelefones" WatermarkText="Telefone" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddTelefones" runat="server" Text='<%# Bind("Telefones") %>' Width="100px"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Email" SortExpression="Email" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditEmail" runat="server" Text='<%# Bind("Email") %>' Width="150px"></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddEmail" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddEmail" WatermarkText="Email" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddEmail" runat="server" Text='<%# Bind("Email") %>' Width="150px"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
						<asp:TemplateField ShowHeader="False">
							<EditItemTemplate>
								<asp:ImageButton ID="ibtUpdate" runat="server" ImageUrl="~/img/bt_confirma_azul.gif"
									CausesValidation="True" CommandName="Update" AlternateText="Confirmar Alteração"
									ValidationGroup="EditGroup" />&nbsp;<asp:ImageButton ID="ibtCancel" ImageUrl="~/img/bt_cancela_azul.gif"
										runat="server" CausesValidation="False" CommandName="Cancel" AlternateText="Cancelar" />
							</EditItemTemplate>
							<ItemTemplate>
								<asp:ImageButton ID="ibtEdit" runat="server" CausesValidation="False" CommandName="Edit"
									ImageUrl="~/img/edit_icon.gif" AlternateText="Editar" />&nbsp;<asp:ImageButton
										ID="ibtDelete" runat="server" ImageUrl="~/img/delete_icon.png" OnClientClick="return confirm('Deseja realmente remover?');"
										CausesValidation="False" CommandName="Delete" AlternateText="Remover" />
							</ItemTemplate>
							<FooterTemplate>
								<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" CommandName="New"
									ImageUrl="~/img/adicionar.png" AlternateText="Nova Empresa" ValidationGroup="AddGroup" />
							</FooterTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				<asp:ObjectDataSource ID="empresasDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Empresa"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemEmpresas" TypeName="FCarnauba_Animais.DataSources.DataSourceEmpresas"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="empresasDataSource_Deleted"
					OnUpdated="empresasDataSource_Updated" OnSelecting="empresasDataSource_Selecting">
				</asp:ObjectDataSource>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
