<%@ Page Title="" Language="C#" MasterPageFile="~/SiteFluxoCaixa.Master" AutoEventWireup="true" CodeBehind="DetalhesFinanceiro.aspx.cs" Inherits="FCarnauba_Animais.DetalhesFinanceiro" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
     <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true"></asp:ScriptManager>
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
    <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    
    <script type ="text/javascript" src="./Scripts/validator.js"></script>
    
    <script type="text/javascript" src="./Scripts/autonumeric.js"></script>

    <script type="text/javascript">
        var jq191 = $.noConflict(true);
        jQuery = jq191;
        $ = jQuery;
    </script>
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <input type="hidden" name="tabIndex" />
    <script type="text/javascript">
        function hideEditButton() {
            jq191("#tabs").ready(function () {
                jq191("#EditImageButton").hide();
                jq191("#tabs").tabs("option", "activate", null)
            });
        }

        jq191(function () {
            jq191("#tabs").tabs(
             {
                 activate: function (event, ui) {
                     var newId = ui.newPanel.attr('id');
                     if (newId == "compra-tab" || newId == "financeiro-tab") {
                         
                     } else {
                         
                     }
                 }
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
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
    <uc2:Mensagem ID="mensagem" runat="server" />
    
        <div>

            <table width="100%">
                <tr class="gridtitulo">
				<th scope="col" style="width:7%;">Nº</th><th scope="col" style="width:15%;">Descrição</th><th scope="col" style="width:10%;">Fornecedor/Cliente</th><th scope="col" style="width:7%;">Propriedade(s)</th><th scope="col" style="width:15%;">Grupo</th><th scope="col" style="width:5%;">Quant.</th><th scope="col" style="width:7%;">V. Unitário/Médio</th><th scope="col" style="width:5%;">Data</th><th scope="col" style="width:7%;">V. Total</th><th scope="col" style="width:3%;">&nbsp;</th>
                </tr>
                <tr class="gridlinha01">
				<td>

						    <span id="MainContent_gvFinanceiros_lblDocumento_0"><asp:Label ID="lblIdFinanceiro" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblDescricao_0"><asp:Label ID="lblDescricao" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblIdEmpresa_0" style="display:inline-block;width:200px;"><asp:Label ID="lblFornecedorCliente" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblIdPropriedade_0"><asp:Label ID="lblPropriedade" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblIdGrupo_0"><asp:Label ID="lblGrupo" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblQuantidade_0"><asp:Label ID="lblQuantidade" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblValorUnitario_0"><asp:Label ID="lblValorUnitario" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblData_0"><asp:Label ID="lblData" runat="server"></asp:Label></span>
							</td><td>
								<span id="MainContent_gvFinanceiros_lblValorTotal_0"><asp:Label ID="lblValorTotal" runat="server"></asp:Label></span>
							</td>
                            <td>
								<asp:ImageButton
										ID="ibtValida" runat="server" ImageUrl="~/img/bt_confirma_azul_duplo.gif" OnClientClick="return confirm('Deseja realmente validar?');"
										CausesValidation="False" CommandName="Valida" AlternateText="Validar" OnClick="ibtValida_Click" Title="Validar Financeiro?" />
							</td>

			    </tr>
            </table>

            <div class="barra03" align="left">
			    DOCUMENTOS/COMPROVANTES</div>

                <asp:GridView ID="gvDocumentos" runat="server" AutoGenerateColumns="False" DataSourceID="documentosDataSource" Width="100%"
					DataKeyNames="DocumentoFinanceiroId" AllowPaging="True" PageSize="5" ShowFooter="True" OnRowCommand="gvDocumentos_RowCommand"
					OnRowDataBound="gvDocumentos_RowDataBound" OnRowDeleted="gvDocumentos_RowDeleted" OnRowUpdated="gvDocumentos_RowUpdated">
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
									Documento (PDF)
								</th>
								<th>
								</th>
							</tr>
                            <tr class="gridlinha01">
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddDescricaoDocumento" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddDescricaoDocumento" WatermarkText="Descrição" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddDescricaoDocumento" runat="server" Text='<%# Bind("Descricao") %>' Width="300px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="vldtAddDescricaoDocumento" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddDescricaoDocumento"
										runat="server" ValidationGroup="AddGroupDocumento"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddDescricaoDocumento" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddDescricaoDocumento">
									</cc1:ValidatorCalloutExtender>
						        </td>
                                <td>
                                    
                                    <asp:FileUpload ID="uploadAddDocumento" runat="server" onfocus="Javascript:ValidateForm(this,false,RetFalse);" onblur="Javascript:ValidateForm(this,true,RetFalse);" />
                                    <asp:RequiredFieldValidator ID="vldtAddDocumento" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="uploadAddDocumento"
										runat="server" ValidationGroup="AddGroupDocumento"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddDocumento" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddDocumento">
									</cc1:ValidatorCalloutExtender>
                                </td>

                                <td>
									<asp:ImageButton ID="ibtNewDocumento" runat="server" CausesValidation="True" ImageUrl="~/img/adicionar.png"
										AlternateText="Novo Documento" ValidationGroup="AddGroupDocumento" OnClick="ibtNewDocumento_Click" /></td>
                            </tr>
						</table>
					</EmptyDataTemplate>

                    <Columns>
                        <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao" HeaderStyle-Width="40%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditDescricaoDocumento" runat="server" Text='<%# Bind("Descricao") %>' Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtEditDescricaoDocumento" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditDescricaoDocumento"
									runat="server" ValidationGroup="EditGroupDocumento"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditDescricaoDocumento" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditDescricaoDocumento">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblDescricaoDocumento" runat="server" Text='<%# Bind("Descricao") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddDescricaoDocumento" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddDescricaoDocumento" WatermarkText="Descrição" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddDescricaoDocumento" runat="server" Text='<%# Bind("Descricao") %>' Width="300px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="vldtAddDescricaoDocumento" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddDescricaoDocumento"
									runat="server" ValidationGroup="AddGroupDocumento"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddDescricaoDocumento" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddDescricaoDocumento">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Documento (PDF)" HeaderStyle-Width="40%">
                            <EditItemTemplate>
								<asp:FileUpload ID="uploadEditDocumento" runat="server" onfocus="Javascript:ValidateForm(this,false,RetFalse);" onblur="Javascript:ValidateForm(this,true,RetFalse);" />
							</EditItemTemplate>
                            <ItemTemplate>
								<asp:LinkButton ID="lnkDocumento" runat="server" Text="Download" 
                                    onclick="lnkDocumento_Click"/>
							</ItemTemplate>
                            <FooterTemplate>
                                
                                <asp:FileUpload ID="uploadAddDocumento" runat="server" onfocus="Javascript:ValidateForm(this,false,RetFalse);" onblur="Javascript:ValidateForm(this,true,RetFalse);" />   
                                <asp:RequiredFieldValidator ID="vldtAddDocumento" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="uploadAddDocumento"
									runat="server" ValidationGroup="AddGroupDocumento"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddDocumento" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddDocumento">
									</cc1:ValidatorCalloutExtender>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:ImageButton ID="ibtUpdateDocumento" runat="server" ImageUrl="~/img/bt_confirma_azul.gif"
									CausesValidation="True" CommandName="Update" AlternateText="Confirmar Alteração"
									ValidationGroup="EditGroupDocumento" />&nbsp;<asp:ImageButton ID="ibtCancelDocumento" ImageUrl="~/img/bt_cancela_azul.gif"
										runat="server" CausesValidation="False" CommandName="Cancel" AlternateText="Cancelar" />
							</EditItemTemplate>
							<ItemTemplate>    
								<asp:ImageButton ID="ibtEditDocumento" runat="server" CausesValidation="False" CommandName="Edit"
									ImageUrl="~/img/edit_icon.gif" AlternateText="Editar" />&nbsp;<asp:ImageButton
										ID="ibtDeleteDocumento" runat="server" ImageUrl="~/img/delete_icon.png" OnClientClick="return confirm('Deseja realmente remover?');"
										CausesValidation="False" CommandName="Delete" AlternateText="Remover" />
							</ItemTemplate>
							<FooterTemplate>
								<asp:ImageButton ID="ibtNewDocumento" runat="server" CausesValidation="True" CommandName="New"
									ImageUrl="~/img/adicionar.png" AlternateText="Nova Compra" ValidationGroup="AddGroupDocumento" />
							</FooterTemplate>
						</asp:TemplateField>      
                </Columns>
				</asp:GridView>
          
            <asp:Panel ID="pnlVendas" runat="server">
            
            <div class="barra03" align="left">
			VENDAS DE ANIMAIS</div>

          <asp:GridView ID="gvCompras" runat="server" AutoGenerateColumns="False" DataSourceID="comprasDataSource" Width="100%"
					DataKeyNames="CompraFinanceiroId" AllowPaging="True" PageSize="5" ShowFooter="True" OnRowCommand="gvCompras_RowCommand"
					OnRowDataBound="gvCompras_RowDataBound" OnRowUpdating="gvCompras_RowUpdating" OnRowDeleted="gvCompras_RowDeleted" OnRowUpdated="gvCompras_RowUpdated">
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
									Animal
								</th>
                                <th>
									Valor
                                </th>
								<th>
								</th>
							</tr>

                            <tr class="gridlinha01">
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddDescricao" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddDescricao" WatermarkText="Descrição" runat="server">
									</cc1:TextBoxWatermarkExtender> 
									<asp:TextBox CssClass="textfield01" ID="txtAddDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="300px" TextMode="MultiLine"></asp:TextBox>
						        </td>
                                <td>
                                    <cc1:TextBoxWatermarkExtender ID="tweAddPesquisaDdlAnimal" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddPesquisaDdlAnimal" WatermarkText="RGD ou Nome do Animal" runat="server">
								</cc1:TextBoxWatermarkExtender>
									<asp:DropDownList ID="ddlAddIdAnimal" ValidationGroup="AddGroup" DataTextField="NomeCompleto"
										DataValueField="Id" runat="server" Width="400px">
									</asp:DropDownList>
                                    <asp:TextBox CssClass="textfield01" ID="txtAddPesquisaDdlAnimal" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisarDDlAnimal" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlAnimal_Click" />
					            </td>
                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddValor" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddValor" WatermarkText="Valor" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddValor" runat="server" Text='<%# Bind("Valor") %>'
										ValidationGroup="AddGroup" Width="70px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddValor" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValor"
										runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddValor" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValor">
									</cc1:ValidatorCalloutExtender>
									</td>
                                <td>
									<asp:ImageButton ID="ibtNew" runat="server" CausesValidation="True" ImageUrl="~/img/adicionar.png"
										AlternateText="Nova Compra" ValidationGroup="AddGroup" OnClick="ibtNew_Click" /></td>
                            </tr>
						</table>
					</EmptyDataTemplate>
                    <Columns>
                        <asp:TemplateField HeaderText="Descrição" SortExpression="Descricao" HeaderStyle-Width="30%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="300px" TextMode="MultiLine"></asp:TextBox>
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
								<asp:TextBox CssClass="textfield01" ID="txtAddDescricao" runat="server" Text='<%# Bind("Descricao") %>' Width="300px" TextMode="MultiLine"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Animal" SortExpression="IdAnimal" HeaderStyle-Width="30%">
							<EditItemTemplate>
                                <cc1:TextBoxWatermarkExtender ID="tweEditPesquisaDdlAnimal" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtEditPesquisaDdlAnimal" WatermarkText="RGD ou Nome do Animal" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:DropDownList ID="ddlEditIdAnimal" DataTextField="NomeCompleto" DataValueField="Id"
									ValidationGroup="EditGroup" runat="server" Width="400px">
								</asp:DropDownList>
                                <asp:TextBox CssClass="textfield01" ID="txtEditPesquisaDdlAnimal" runat="server" Width="200px"></asp:TextBox>
                                <asp:Button ID="btnPesquisarDDlAnimal" runat="server" Text="Pesquisar" OnClick="btnEditPesquisarDDlAnimal_Click" />
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblIdAnimal"  runat="server" Width="400px"></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
                                <cc1:TextBoxWatermarkExtender ID="tweAddPesquisaDdlAnimal" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddPesquisaDdlAnimal" WatermarkText="RGD ou Nome do Animal" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:DropDownList ID="ddlAddIdAnimal" ValidationGroup="AddGroup" DataTextField="NomeCompleto"
									DataValueField="Id" runat="server" Width="400px">
								</asp:DropDownList>
                                <asp:TextBox CssClass="textfield01" ID="txtAddPesquisaDdlAnimal" runat="server" Width="200px"></asp:TextBox>
                                <asp:Button ID="btnPesquisarDDlAnimal" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlAnimal_Click" />
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor" SortExpression="Valor" HeaderStyle-Width="10%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditValor" runat="server" Text='<%# Bind("Valor") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditValor" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditValor"
									runat="server" ValidationGroup="EditGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditValor" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditValor">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblValor" runat="server" Text='<%# string.Format("{0:N2}", Eval("Valor")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddValor" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddValor" WatermarkText="Valor" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddValor" runat="server" Text='<%# Bind("Valor") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddValor" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValor"
									runat="server" ValidationGroup="AddGroup"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddValor" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValor">
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
									ImageUrl="~/img/adicionar.png" AlternateText="Nova Compra" ValidationGroup="AddGroup" />
							</FooterTemplate>
						</asp:TemplateField>
                    </Columns>
				</asp:GridView>
                <% if (financeiro.FormaPagamento != "À Vista") { %>
                <div class="barra04" align="left">Total Parcelamento Pago:  <% =String.Format("{0:C}", Convert.ToDouble(financeiro.TotalPago))%>
                </div>
                <asp:GridView ID="gvParcelas" runat="server" AutoGenerateColumns="False" DataSourceID="parcelasDataSource" Width="100%"
					DataKeyNames="ParcelaFinanceiroId" AllowPaging="True" PageSize="10" ShowFooter="True" OnRowCommand="gvParcelas_RowCommand"
					OnRowDataBound="gvParcelas_RowDataBound" OnRowUpdating="gvParcelas_RowUpdating" OnRowDeleted="gvParcelas_RowDeleted" OnRowUpdated="gvParcelas_RowUpdated">
					<RowStyle CssClass="gridlinha01" />
					<AlternatingRowStyle CssClass="gridlinha02" />
					<HeaderStyle CssClass="gridtitulo" />
					<FooterStyle CssClass="gridrodape" />

                    <EmptyDataTemplate>
						<table>
							<tr class="gridtitulo">
                                <th>
									Nº Parcela
								</th>
								<th>
									Data
								</th>
                                <th>
									Valor Inicial
                                </th>
                                <th>
									Valor Pago
                                </th>
                                <th>
									Data do Pagamento
                                </th>
								<th>
								</th>
							</tr>
                            <tr class="gridlinha01">

                            <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddNParcela" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddNParcela" WatermarkText="Nº Parc." runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddNParcela" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" Text='<%# Bind("NParcela") %>'
										ValidationGroup="AddGroupParcela" Width="50px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddNParcela" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddNParcela"
										runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddNParcela" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddNParcela">
									</cc1:ValidatorCalloutExtender>
									</td>

                                <td>
                                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtAddData"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
									<cc1:TextBoxWatermarkExtender ID="tweAddData" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddData" WatermarkText="Data" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("Data") %>' Width="70px" MaxLength="10"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
										runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>
									</td>

                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddValorInicial" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddValorInicial" WatermarkText="Valor Inicial" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddValorInicial" runat="server" Text='<%# Bind("ValorInicial") %>'
										ValidationGroup="AddGroupParcela" Width="70px"></asp:TextBox>
									<asp:RequiredFieldValidator ID="vldtAddValorInicial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValorInicial"
										runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
									<cc1:ValidatorCalloutExtender ID="vceAddValorInicial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValorInicial">
									</cc1:ValidatorCalloutExtender>
									</td>

                                <td>
									<cc1:TextBoxWatermarkExtender ID="tweAddValorPago" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddValorPago" WatermarkText="Valor Pago" runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddValorPago" runat="server" Text='<%# Bind("ValorPago") %>'
										ValidationGroup="AddGroupParcela" Width="70px"></asp:TextBox>
									</td>
                                <td>
                                     <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAddDataPagamento"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
									<cc1:TextBoxWatermarkExtender ID="tweAddDataPagamento" WatermarkCssClass="textfield_vazio01"
										TargetControlID="txtAddDataPagamento" WatermarkText="Dt. Pagto." runat="server">
									</cc1:TextBoxWatermarkExtender>
									<asp:TextBox CssClass="textfield01" ID="txtAddDataPagamento" runat="server" Text='<%# Bind("DataPagamento") %>' Width="70px" MaxLength="10"></asp:TextBox>
									</td>
                                <td>
									<asp:ImageButton ID="ibtNewParcela" runat="server" CausesValidation="True" ImageUrl="~/img/adicionar.png"
										AlternateText="Nova Parcela" ValidationGroup="AddGroupParcela" OnClick="ibtNewParcela_Click" /></td>
                            </tr>
						</table>
					</EmptyDataTemplate>
                    <Columns>
                    <asp:TemplateField HeaderText="Nº Parcela" SortExpression="NParcela" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditNParcela" runat="server" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" Text='<%# Bind("NParcela") %>' Width="40px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditNParcela" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditNParcela"
									runat="server" ValidationGroup="EditGroupParcela"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditNParcela" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditNParcela">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblNParcela" runat="server" Text='<%# Bind("NParcela") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddNParcela" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddNParcela" WatermarkText="Nº Parc." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddNParcela" onkeydown = "return (!(event.keyCode>=65) && event.keyCode!=32);" runat="server" Text='<%# Bind("NParcela") %>' Width="50px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddNParcela" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddNParcela"
									runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddNParcela" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddNParcela">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Data" SortExpression="Data" HeaderStyle-Width="15%">
							<EditItemTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEditData"
                                        Format="dd/MM/yyyy">
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
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
								<cc1:TextBoxWatermarkExtender ID="tweAddData" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddData" WatermarkText="Data" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddData" runat="server" Text='<%# Bind("Data") %>' Width="70px" MaxLength="10"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddData" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddData"
									runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddData" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddData">
									</cc1:ValidatorCalloutExtender>  
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Inicial" SortExpression="ValorInicial" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditValorInicial" runat="server" Text='<%# Bind("ValorInicial") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditValorInicial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditValorInicial"
									runat="server" ValidationGroup="EditGroupParcela"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditValorInicial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditValorInicial">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblValorInicial" runat="server" Text='<%# string.Format("{0:N2}", Eval("ValorInicial")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddValorInicial" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddValorInicial" WatermarkText="Valor Inicial" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddValorInicial" runat="server" Text='<%# Bind("ValorInicial") %>'  Width="70px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddValorInicial" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddValorInicial"
									runat="server" ValidationGroup="AddGroupParcela"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddValorInicial" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddValorInicial">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Valor Pago" SortExpression="ValorPago" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox CssClass="textfield01" ID="txtEditValorPago" runat="server" Text='<%# Bind("ValorPago") %>'  Width="70px"></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblValorPago" runat="server" Text='<%# string.Format("{0:N2}", Eval("ValorPago")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddValorPago" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddValorPago" WatermarkText="Valor Pago" runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddValorPago" runat="server" Text='<%# Bind("ValorPago") %>'  Width="70px"></asp:TextBox>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="Data do Pagamento" SortExpression="DataPagamento" HeaderStyle-Width="15%">
							<EditItemTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEditDataPagamento"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
								<asp:TextBox CssClass="textfield01" ID="txtEditDataPagamento" runat="server" Text='<%# Bind("DataPagamento") %>' Width="70px" MaxLength="10"></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblDataPagamento" runat="server" Text='<%# string.Format("{0:dd/MM/yyyy}", Eval("DataPagamento")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAddDataPagamento"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
								<cc1:TextBoxWatermarkExtender ID="tweAddDataPagamento" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddDataPagamento" WatermarkText="Dt. Pagto." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddDataPagamento" runat="server" Text='<%# Bind("DataPagamento") %>' Width="70px" MaxLength="10"></asp:TextBox>  
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:ImageButton ID="ibtUpdateParcela" runat="server" ImageUrl="~/img/bt_confirma_azul.gif"
									CausesValidation="True" CommandName="Update" AlternateText="Confirmar Alteração"
									ValidationGroup="EditGroupParcela" />&nbsp;<asp:ImageButton ID="ibtCancel" ImageUrl="~/img/bt_cancela_azul.gif"
										runat="server" CausesValidation="False" CommandName="Cancel" AlternateText="Cancelar" />
							</EditItemTemplate>
							<ItemTemplate>    
								<asp:ImageButton ID="ibtEditParcela" runat="server" CausesValidation="False" CommandName="Edit"
									ImageUrl="~/img/edit_icon.gif" AlternateText="Editar" />&nbsp;<asp:ImageButton
										ID="ibtDeleteParcela" runat="server" ImageUrl="~/img/delete_icon.png" OnClientClick="return confirm('Deseja realmente remover?');"
										CausesValidation="False" CommandName="Delete" AlternateText="Remover" />
							</ItemTemplate>
							<FooterTemplate>
								<asp:ImageButton ID="ibtNewParcela" runat="server" CausesValidation="True" CommandName="New"
									ImageUrl="~/img/adicionar.png" AlternateText="Nova Parcela" ValidationGroup="AddGroupParcela" />
							</FooterTemplate>
						</asp:TemplateField>
                    </Columns>
				</asp:GridView>
                <% } %>
                </asp:Panel>

                <asp:ObjectDataSource ID="comprasDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Compra"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemCompras" TypeName="FCarnauba_Animais.DataSources.DataSourceCompras"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="comprasDataSource_Deleted"
					OnUpdated="comprasDataSource_Updated" OnSelecting="comprasDataSource_Selecting">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="documentosDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Documento"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemDocumentos" TypeName="FCarnauba_Animais.DataSources.DataSourceDocumentos"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="documentosDataSource_Deleted"
					OnUpdated="documentosDataSource_Updated" OnSelecting="documentosDataSource_Selecting">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="animaisDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Animal"
					SelectMethod="ObtemAnimais" TypeName="FCarnauba_Animais.DataSources.DataSourceAnimais"
					OldValuesParameterFormatString="original_{0}">
				</asp:ObjectDataSource>
                <asp:ObjectDataSource ID="parcelasDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.Parcela"
					DeleteMethod="Remova" InsertMethod="Insira" SelectMethod="ObtemParcelas" TypeName="FCarnauba_Animais.DataSources.DataSourceParcelas"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}" OnDeleted="parcelasDataSource_Deleted"
					OnUpdated="parcelasDataSource_Updated" OnSelecting="parcelasDataSource_Selecting">
				</asp:ObjectDataSource>     
        </div>
</asp:Content>
