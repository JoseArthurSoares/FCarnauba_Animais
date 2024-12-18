<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Leiteiro.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Leiteiro" AspCompat="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebNavigator.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
	Namespace="Infragistics.WebUI.UltraWebNavigator" TagPrefix="ignav" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Fazenda Carnaúba - Controle Leiteiro</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
    <link rel="stylesheet" type="text/css" href="./Styles/SiteMobile.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css">
    <style>
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&display=swap');
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&family=Delius&display=swap');
        
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .header {
            background-color: #002855;
            color: #fff;
            text-align: center;
            padding: 10px 0;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 50px;
        }

        .header img {
            width: 343px;
            height: 96px;
        }

       .header-logout {
            display: flex;
            align-items: center; 
            gap: 3px; 
            color: white; 
            font-size: 16px; 
            line-height: 1; 
        }

        .header-logout img {
            height: 24px; 
            width: auto;
            display: block; 
        }

        .header-logout span {
            display: flex;
            align-items: center;
            line-height: 1;
            
        }
        .header-logout p
        {
            margin-top:13px;
        }

        .container-leiteiro {
            max-width: 600px;
            margin: 20px auto;
            padding: 0 15px;
        }

        .lote-selector {
            width: 100%;
            margin-bottom: 20px;
        }

        .grid-pesagens {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .grid-pesagens th, .grid-pesagens td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .grid-pesagens th {
            background-color: #002855;
            color: white;
        }

        .grid-pesagens tr:nth-child(even) {
            background-color: #f2f2f2;
        }

        .grid-pesagens .edit-buttons {
            display: flex;
            justify-content: center;
            gap: 10px;
        }

        .grid-pesagens .edit-buttons img {
            height: 24px;
            cursor: pointer;
        }

        .grid-pesagens .check-cell {
            text-align: center;
        }

        .bottom-buttons {
            display: flex;
            justify-content: center;
            gap: 20px;
        }

        .bottom-buttons input {
            width: 165px;
            background-color: #ffcc00;
            color: #000;
            border: none;
            padding: 10px;
            border-radius: 8px;
            font-family: "Delicious Handrawn", cursive;
            font-size: 20px;
            cursor: pointer;
        }

        .bottom-buttons input:hover {
            background-color: #e6b800;
        }

        footer {
            background-color: #002855;
            color: #fff;
            text-align: center;
            padding: 35px;
           position: absolute;
            width: 100%;
            height: 111px;
            bottom: 0;
        }

        footer span {
            font-family: "Delius", cursive;
            font-weight: 400;
            font-style: normal;
        }

        footer img {
            height: 30px;
            margin-left: 10px;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
    <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptLocalization="true" EnableScriptGlobalization="true"></asp:ScriptManager>

    <div class="header">
        <a href="https://fazendacarnauba.com/">
            <img src="../img/sgp2.png" alt="Logo da Fazenda">
        </a>
        <div class="header-logout">
          <img src="../img/sair.png" alt="Logo">
          <p>Sair</p>
        </div>
    </div>

    <div class="container-leiteiro">
        <asp:DropDownList ID="ddlLoteData" Name="ddlLoteData" runat="server" CssClass="form-control lote-selector" AutoPostBack="true" OnSelectedIndexChanged="ddlLoteData_SelectedIndexChanged">
        </asp:DropDownList>
        
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

        <div class="bottom-buttons">
            <asp:Button ID="btnInicio" runat="server" Text="Início" OnClick="btnInicio_Click"/>
            <asp:Button ID="btnEncerrar" runat="server" Text="Encerrar" OnClick="btnEncerrar_Click" Visible="false"/>
        </div>
    </div>

    <footer>
        <span>&copy; 2024 - Todos os direitos reservados.</span>
        <img src="../img/logo-lightbase.png" alt="Logo da Lightbase">
    </footer>

    <asp:ObjectDataSource ID="pesagensLeiteDataSource" runat="server" 
        DataObjectTypeName="FCarnauba_Animais.DataAccess.ProducaoLeite"
        SelectMethod="ObtemPesagensLeite" 
        TypeName="FCarnauba_Animais_WebMobile.DataSources.DataSourcePesagensLeite"
        UpdateMethod="Salve" 
        OldValuesParameterFormatString="original_{0}"
        OnUpdated="pesagensLeiteDataSource_Updated" 
        OnSelecting="pesagensLeiteDataSource_Selecting">
    </asp:ObjectDataSource>
    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };

            if ($("#txtEditPOrdenha").length > 0) $("#txtEditPOrdenha").autoNumeric(numSettings);
            if ($("#txtEditPOrdenha").length > 0) $("#txtEditPOrdenha").autoNumericSet(parseFloat($("#txtEditPOrdenha").val().replace(",", ".")));

            if ($("#txtEditSOrdenha").length > 0) $("#txtEditSOrdenha").autoNumeric(numSettings);
            if ($("#txtEditSOrdenha").length > 0) $("#txtEditSOrdenha").autoNumericSet(parseFloat($("#txtEditSOrdenha").val().replace(",", ".")));
        });
    </script>
</body>
</html>