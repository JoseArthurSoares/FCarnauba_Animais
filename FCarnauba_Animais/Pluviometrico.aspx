<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="Pluviometrico.aspx.cs" Inherits="FCarnauba_Animais.Pluviometrico" AspCompat="true" %>
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

            if ($("#txtData").length > 0) $("#txtData").mask("99/99/9999");

            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtEditPluviometria").length > 0) $("#txtEditPluviometria").autoNumeric(numSettings);
            if ($("#txtEditPluviometria").length > 0) $("#txtEditPluviometria").autoNumericSet(parseFloat($("#txtEditPluviometria").val().replace(",", ".")));

            if ($("#txtAddPluviometria").length > 0) $("#txtAddPluviometria").autoNumeric(numSettings);
            if ($("#txtAddPluviometria").length > 0) $("#txtAddPluviometria").autoNumericSet(parseFloat($("#txtAddPluviometria").val().replace(",", ".")));

        });
    </script>    

	<asp:ScriptManager ID="scriptManager" runat="server" EnableScriptGlobalization="true">
	</asp:ScriptManager>

    <div>
		<div class="barra02" align="left">
			Pluviometria</div>
		<asp:UpdatePanel ID="updatePanel" runat="server">
			<ContentTemplate>
				<uc2:Mensagem ID="mensagem" runat="server" />
				
			
				<table>
					<tr>
						<td>
							Propriedade:</td>
						<td>
                            <asp:DropDownList ID="ddlPropriedade" runat="server" autopostback="true" OnSelectedIndexChanged="ddlPropriedade_SelectedIndexChanged">
                            </asp:DropDownList>
						</td>

                        <td>
							Ano:</td>
						<td>
                            <asp:DropDownList ID="ddlAno" runat="server" Height="18px" Width="150" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                            </asp:DropDownList>
                            <asp:Button ID="btnConsultar" runat="server" CssClass="botao" Text="Consultar" OnClick="btnConsultar_Click" />
						</td>
                        
					</tr>
					
				</table>
				<hr class="busca_barra_rodape" />
                <asp:GridView ID="gvPluviometrias" runat="server" AutoGenerateColumns="False" DataSourceID="pluviometriasDataSource" Width="100%"
					DataKeyNames="Id" AllowPaging="False" ShowFooter="True" OnRowCommand="gvPluviometrias_RowCommand"
					OnRowDataBound="gvPluviometrias_RowDataBound" OnRowUpdating="gvPluviometrias_RowUpdating" PagerSettings-Mode="NumericFirstLast">
					<RowStyle CssClass="gridlinha01" />
					<AlternatingRowStyle CssClass="gridlinha02" />
					<HeaderStyle CssClass="gridtitulo" />
					<FooterStyle CssClass="gridrodape" />
					<EmptyDataTemplate>
						<table>
							<tr class="gridtitulo">
                                <th>
									Data
								</th>
								<th>
									Pluviômetro
								</th>
                                <th>
									Pluviometria
								</th>
								<th>
								</th>
							</tr>
							<tr class="gridlinha01">
                                <td>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAddData"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
									<cc1:TextBoxWatermarkExtender ID="tweAddData" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddData" WatermarkText="Data" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("DataStr") %>' Width="70px" MaxLength="10"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>
                                    
								</td>
                                <td>
									<asp:DropDownList ID="ddlAddPluviometro" ValidationGroup="AddGroup" runat="server" Width="200px">
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
									</asp:DropDownList>
									</td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddPluviometria" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddPluviometria" WatermarkText="Pluviometria" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddPluviometria" runat="server" Text='<%# Bind("Pluviometria") %>'
										ValidationGroup="AddGroup" Width="80px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddPluviometria" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddPluviometria"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddPluviometria" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddPluviometria">
									</cc1:ValidatorCalloutExtender>
									</td>        
								<td>
									<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" ImageUrl="~/img/adicionar.png"
										AlternateText="Nova Pluviometria" ValidationGroup="AddGroup" OnClick="ibtNew_Click" /></td>
							</tr>
						</table>
					</EmptyDataTemplate>
					<Columns>
                        <asp:TemplateField HeaderText="Data" SortExpression="DataStr" HeaderStyle-Width="5%">
							<EditItemTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditData"
                                        Format="dd/MM/yyyy" CssClass="cal_Theme">
                                    </cc1:CalendarExtender>
								<asp:TextBox CssClass="textfield01" ID="txtEditData" runat="server" Text='<%# Bind("DataStr") %>' Width="70px" MaxLength="10"></asp:TextBox>
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
								<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("DataStr") %>' Width="70px" MaxLength="10"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>
                                   
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Pluviômetro" SortExpression="Pluviometro" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditPluviometro"
									ValidationGroup="EditGroup" runat="server" Width="100px">
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
								</asp:DropDownList>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblPluviometro" runat="server" Width="100px" Text='<%# string.Format("{0:N2}", Eval("Pluviometro")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddPluviometro" ValidationGroup="AddGroup"
									runat="server" Width="100px">
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
								</asp:DropDownList>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Pluviometria" SortExpression="Pluviometria" HeaderStyle-Width="7%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditPluviometria" runat="server" Text='<%# Bind("Pluviometria") %>'  Width="80px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditPluviometria" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditPluviometria"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditPluviometria" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditPluviometria">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblPluviometria" runat="server" Text='<%# string.Format("{0:N2}", Eval("Pluviometria")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddPluviometria" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddPluviometria" WatermarkText="Pluviometria" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddPluviometria" runat="server" Text='<%# Bind("Pluviometria") %>'  Width="80px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddPluviometria" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddPluviometria"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddPluviometria" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddPluviometria">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        
						<asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
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
									ImageUrl="~/img/adicionar.png" AlternateText="Novo Produto" ValidationGroup="AddGroup" />
							</FooterTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>
				<asp:ObjectDataSource ID="pluviometriasDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.ControlePluviometrico"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemPluviometrias" TypeName="FCarnauba_Animais.DataSources.DataSourcePluviometria"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="pluviometriasDataSource_Deleted"
					OnUpdated="pluviometriasDataSource_Updated" OnSelecting="pluviometriasDataSource_Selecting">
				</asp:ObjectDataSource>
			</ContentTemplate>
		</asp:UpdatePanel>
	</div>
</asp:Content>
