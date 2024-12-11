<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leiteiro.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Leiteiro" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
    function ismaxlength(obj) {
        var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
        if (obj.getAttribute && obj.value.length > mlength) {
            obj.value = obj.value.substring(0, mlength)
        }
    }
</script>

<link rel="stylesheet" type="text/css" href="./Styles/SiteMobile.css" />
<link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
<link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" /> 
<head id="Head1">
    <title>SGP - Pesagens Leite</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
</head>
<div style="background-color:#052B5C" align="left"><a href="https://fazendacarnauba.com/" title="Portal Fazenda Carnaúba"><img src="./img/sgp.png"></a></div>
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
<div class="barra02" align="center">
			Leiteiro</div>
<script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtEditPOrdenha").length > 0) $("#txtEditPOrdenha").autoNumeric(numSettings);
            if ($("#txtEditPOrdenha").length > 0) $("#txtEditPOrdenha").autoNumericSet(parseFloat($("#txtEditPOrdenha").val().replace(",", ".")));

            if ($("#txtEditSOrdenha").length > 0) $("#txtEditSOrdenha").autoNumeric(numSettings);
            if ($("#txtEditSOrdenha").length > 0) $("#txtEditSOrdenha").autoNumericSet(parseFloat($("#txtEditSOrdenha").val().replace(",", ".")));
        });
    </script>    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true"></asp:ScriptManager>
     <div>
        &nbsp;
    </div>
    <div class="icon">
        <asp:DropDownList ID="ddlLoteData" Name="ddlLoteData" runat="server" Width="300" autopostback="true" OnSelectedIndexChanged="ddlLoteData_SelectedIndexChanged">
            </asp:DropDownList>
    </div>
    <div>
        &nbsp;
    </div>
    
    <asp:GridView ID="gvPesagensLeite" runat="server" AutoGenerateColumns="False" DataSourceID="pesagensLeiteDataSource" Width="100%"
					DataKeyNames="PesagemLoteId" AllowPaging="False" PageSize="10" ShowFooter="False" OnRowCommand="gvPesagensLeite_RowCommand"
					OnRowDataBound="gvPesagensLeite_RowDataBound" OnRowUpdating="gvPesagensLeite_RowUpdating" OnRowUpdated="gvPesagensLeite_RowUpdated">
					<RowStyle CssClass="gridlinha01" />
					<AlternatingRowStyle CssClass="gridlinha02" />
					<HeaderStyle CssClass="gridtitulo" />
					<FooterStyle CssClass="gridrodape" />
                    
                    <Columns>
                    <asp:TemplateField HeaderText="Matriz" SortExpression="NomeMatriz" HeaderStyle-Width="25%">
							<ItemTemplate>
								<asp:Label Font-Size="Small" ID="lblNomeMatriz" runat="server" Text='<%# Bind("NomeMatriz") %>'></asp:Label>
							</ItemTemplate>
							
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="1ª Ord." SortExpression="POrdenha" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox ID="txtEditPOrdenha" runat="server" Text='<%# Bind("POrdenha") %>'  Width="50px" ClientIDMode="Static"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditPOrdenha" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditPOrdenha"
									runat="server" ValidationGroup="EditGroupPesagemLeite"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditPOrdenha" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditPOrdenha">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblPOrdenha" runat="server" Text='<%# string.Format("{0:N2}", Eval("POrdenha")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddPOrdenha" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddPOrdenha" WatermarkText="1ª Ord." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddPOrdenha" runat="server" Text='<%# Bind("POrdenha") %>'  Width="25px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddPOrdenha" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddPOrdenha"
									runat="server" ValidationGroup="AddGroupPesagemLeite"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddPOrdenha" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddPOrdenha">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="2ª Ord." SortExpression="SOrdenha" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:TextBox ID="txtEditSOrdenha" runat="server" Text='<%# Bind("SOrdenha") %>'  Width="50px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtEditSOrdenha" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtEditSOrdenha"
									runat="server" ValidationGroup="EditGroupPesagemLeite"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceEditSOrdenha" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtEditSOrdenha">
									</cc1:ValidatorCalloutExtender>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblSOrdenha" runat="server" Text='<%# string.Format("{0:N2}", Eval("SOrdenha")) %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<cc1:TextBoxWatermarkExtender ID="tweAddSOrdenha" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtAddSOrdenha" WatermarkText="2ª Ord." runat="server">
								</cc1:TextBoxWatermarkExtender>
								<asp:TextBox CssClass="textfield01" ID="txtAddSOrdenha" runat="server" Text='<%# Bind("SOrdenha") %>'  Width="25px"></asp:TextBox>
								<asp:RequiredFieldValidator ID="vldtAddSOrdenha" ErrorMessage="Campo Obrigatório" Display="None" ControlToValidate="txtAddSOrdenha"
									runat="server" ValidationGroup="AddGroupPesagemLeite"></asp:RequiredFieldValidator>
								<cc1:ValidatorCalloutExtender ID="vceAddSOrdenha" runat="server" HighlightCssClass="validacao_invalido" WarningIconImageUrl="~/img/aviso.png" CloseImageUrl="~/img/fechar.png" TargetControlID="vldtAddSOrdenha">
									</cc1:ValidatorCalloutExtender>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="BP" SortExpression="BezerrosPe" HeaderStyle-Width="2%">
							<EditItemTemplate>
                                <asp:CheckBox ID="ckEditBezerrosPe" runat="server" ValidationGroup="EditGroupPesagemLeite" Checked='<%# Bind("BezerrosPe") %>'  />
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblBezerrosPe" runat="server" Width="20px" Text='<%# Bind("BezerrosPe") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
                                <asp:CheckBox ID="ckAddBezerrosPe" runat="server" ValidationGroup="AddGroupPesagemLeite" Checked='<%# Bind("BezerrosPe") %>' />
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField HeaderText="TF" SortExpression="TetosFuncionais" HeaderStyle-Width="5%">
							<EditItemTemplate>
								<asp:DropDownList ID="ddlEditTetosFuncionais"
									ValidationGroup="EditGroupPesagemLeite" runat="server" Width="45px">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem Selected="True">4</asp:ListItem>
								</asp:DropDownList>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="lblTetosFuncionais" runat="server" Width="30px" Text='<%# Bind("TetosFuncionais") %>'></asp:Label>
							</ItemTemplate>
							<FooterTemplate>
								<asp:DropDownList ID="ddlAddTetosFuncionais" ValidationGroup="AddGroupPesagemLeite"
									runat="server" Width="45px">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem Selected="True">4</asp:ListItem>
								</asp:DropDownList>
							</FooterTemplate>
						</asp:TemplateField>
                        <asp:TemplateField ShowHeader="False" HeaderStyle-Width="15%">
							<EditItemTemplate>
								<asp:ImageButton ID="ibtUpdatePesagemLeite" runat="server" ImageUrl="~/img/bt_confirma_azul.gif"
									CausesValidation="True" CommandName="Update" AlternateText="Confirmar Alteração"
									ValidationGroup="EditGroupPesagemLeite" />&nbsp;<asp:ImageButton ID="ibtCancel" ImageUrl="~/img/bt_cancela_azul.gif"
										runat="server" CausesValidation="False" CommandName="Cancel" AlternateText="Cancelar" />
							</EditItemTemplate>
							<ItemTemplate>    
								<asp:ImageButton ID="ibtEditPesagemLeite" runat="server" CausesValidation="False" CommandName="Edit"
									ImageUrl="~/img/edit_icon.gif" AlternateText="Editar" />
							</ItemTemplate>
							<FooterTemplate>
								<asp:ImageButton ID="ibtNewPesagemLeite" runat="server" CausesValidation="True" CommandName="New"
									ImageUrl="~/img/adicionar.png" AlternateText="Nova Pesagem" ValidationGroup="AddGroupPesagemLeite" />
							</FooterTemplate>
						</asp:TemplateField>
                    </Columns>
				</asp:GridView>
                <asp:ObjectDataSource ID="pesagensLeiteDataSource" runat="server" DataObjectTypeName="FCarnauba_Animais.DataAccess.ProducaoLeite"
					SelectMethod="ObtemPesagensLeite" TypeName="FCarnauba_Animais_WebMobile.DataSources.DataSourcePesagensLeite"
					UpdateMethod="Salve" OldValuesParameterFormatString="original_{0}"
					OnUpdated="pesagensLeiteDataSource_Updated" OnSelecting="pesagensLeiteDataSource_Selecting">
				</asp:ObjectDataSource>
    <div>
        &nbsp;
    </div>
    <div class="icon">
        <table width="100%">
        <tr>
		<td align="center">
			<table>
				<tr>
					<td align="center"><asp:Button ID="btnInicio" runat="server" Text="Início" Width="165px" OnClick="btnInicio_Click"/></td>
					<td align="center"><asp:Button ID="btnEncerrar" runat="server" Text="Encerrar" Width="165px" OnClick="btnEncerrar_Click" Visible="false"/></td>
                    
				</tr>
			</table>
		</td>
		
	    </tr>
	</table>
    </div>
    </form>
</body>
</html>
