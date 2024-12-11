<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditaMensuracao.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.EditaMensuracao" AspCompat="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<link rel="stylesheet" type="text/css" href="./Styles/SiteMobile.css" />
<link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
<link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" /> 
<head id="Head1">
    <title>SGP - Edita Mensuração</title>
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
			Ponderal</div>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-1.9.1.min.js"></script>
<script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
<script type ="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
<script type ="text/javascript" src="./Scripts/validator.js"></script>
<script type ="text/javascript" src="./Scripts/autonumeric.js"></script>
<script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
<script type="text/javascript">
    function ismaxlength(obj) {
        var mlength = obj.getAttribute ? parseInt(obj.getAttribute("maxlength")) : ""
        if (obj.getAttribute && obj.value.length > mlength) {
            obj.value = obj.value.substring(0, mlength)
        }
    }

    $(document).ready(function () {
        var numSettings = { aSep: '.', aDec: ',', aSign: '', vMax: '999999999999999.99', vMin: '0.00' };
        //if ($("#txtPeso").val() != " ") {
            if ($("#txtPeso").length > 0) $("#txtPeso").autoNumeric(numSettings);
            if ($("#txtPeso").length > 0) $("#txtPeso").autoNumericSet(parseFloat($("#txtPeso").val().replace(",", ".")));
        //}

        //if ($("#txtCEscrotal").val == "0,00") {
        if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumeric(numSettings);
        if ($("#txtCEscrotal").length > 0) $("#txtCEscrotal").autoNumericSet(parseFloat($("#txtCEscrotal").val().replace(",", ".")));
        //}

        //if ($("#txtAAnterior").val == "0,00") {
        if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumeric(numSettings);
        if ($("#txtAAnterior").length > 0) $("#txtAAnterior").autoNumericSet(parseFloat($("#txtAAnterior").val().replace(",", ".")));
        //}

        //if ($("#txtAPosterior").val == "0,00") {
        if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumeric(numSettings);
        if ($("#txtAPosterior").length > 0) $("#txtAPosterior").autoNumericSet(parseFloat($("#txtAPosterior").val().replace(",", ".")));
        //}

        //if ($("#txtLGarupa").val == "0,00") {
        if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumeric(numSettings);
        if ($("#txtLGarupa").length > 0) $("#txtLGarupa").autoNumericSet(parseFloat($("#txtLGarupa").val().replace(",", ".")));
        //}

        //if ($("#txtCGarupa").val == "0,00") {
        if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumeric(numSettings);
        if ($("#txtCGarupa").length > 0) $("#txtCGarupa").autoNumericSet(parseFloat($("#txtCGarupa").val().replace(",", ".")));
        //}

        //if ($("#txtCCorporal").val == "0,00") {
        if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumeric(numSettings);
        if ($("#txtCCorporal").length > 0) $("#txtCCorporal").autoNumericSet(parseFloat($("#txtCCorporal").val().replace(",", ".")));
        //}

        //if ($("#txtPToracico").val == "0,00") {
        if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumeric(numSettings);
        if ($("#txtPToracico").length > 0) $("#txtPToracico").autoNumericSet(parseFloat($("#txtPToracico").val().replace(",", ".")));
        //}

    });

</script>
    <form id="form1" runat="server">
    <div>
    <table class="label1" border="0" width="100%">
    <tr>
		
		<td align="center" colspan="2"><asp:Label ID="lblMensagem" runat="server" Text="Mensagem" Visible="False" ForeColor="red"></asp:Label></td>
        
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr> 
    <tr>
		
		<td align="center" colspan="2"><asp:Label ID="lblNomeMatriz" runat="server" Text="Matriz"></asp:Label></td>
        
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>	
		<td align="right"><asp:TextBox ID="txtPeso" runat="server" placeholder="Peso" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
        <td align="left"><asp:TextBox ID="txtCEscrotal" runat="server" placeholder="C.E. (C. Escrotal)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>	
		<td align="right"><asp:TextBox ID="txtAAnterior" runat="server" placeholder="A.A. (A. Anterior)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
        <td align="left"><asp:TextBox ID="txtAPosterior" runat="server" placeholder="A.P. (A. Posterior)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>	
		<td align="right"><asp:TextBox ID="txtLGarupa" runat="server" placeholder="L.G. (L. Garupa)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
        <td align="left"><asp:TextBox ID="txtCGarupa" runat="server" placeholder="C.G. (C. Garupa)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>	
		<td align="right"><asp:TextBox ID="txtCCorporal" runat="server" placeholder="C.C. (C. Corporal)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
        <td align="left"><asp:TextBox ID="txtPToracico" runat="server" placeholder="P.T. (P. Torácico)" Width="150px" ClientIDMode="Static"></asp:TextBox></td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>
		<td colspan="2" align="center">
            <asp:DropDownList ID="ddlCaracterizacoesRaciais" Name="ddlCaracterizacoesRaciais" runat="server" Width="300px">
                <asp:ListItem>Caracterização Racial</asp:ListItem>
                <asp:ListItem>ÓTIMA</asp:ListItem>
                <asp:ListItem>BOA</asp:ListItem>
                <asp:ListItem>MÉDIA</asp:ListItem>
                <asp:ListItem>FRACA</asp:ListItem>
            </asp:DropDownList>
        </td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>
		<td colspan="2" align="center">
            <asp:DropDownList ID="ddlClassificacoesUbere" Name="ddlClassificacoesUbere" runat="server" Width="300px">
                <asp:ListItem>Classificação de Úbere</asp:ListItem>
                <asp:ListItem>ÓTIMA</asp:ListItem>
                <asp:ListItem>BOA</asp:ListItem>
                <asp:ListItem>MÉDIA</asp:ListItem>
                <asp:ListItem>FRACA</asp:ListItem>
            </asp:DropDownList>
        </td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>
		<td colspan="2" align="center">
            <asp:DropDownList ID="ddlRegimeAlimentar" Name="ddlRegimeAlimentar" runat="server" Width="300px">
                <asp:ListItem>Regime Alimentar</asp:ListItem>
                <asp:ListItem>MAMANDO SEM ORDENHA</asp:ListItem>
                <asp:ListItem>MAMANDO COM ORDENHA</asp:ListItem>
                <asp:ListItem>PASTO</asp:ListItem>
                <asp:ListItem>PASTO COM CONCENTRADO</asp:ListItem>
            </asp:DropDownList>
        </td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>
		<td colspan="2" align="center">
            <asp:DropDownList ID="ddlCondicoesCriacao" Name="ddlCondicoesCriacao" runat="server" Width="300px">
                <asp:ListItem>Condição de Criação</asp:ListItem>
                <asp:ListItem>MAMANDO</asp:ListItem>
                <asp:ListItem>DESMAMADO</asp:ListItem>
                <asp:ListItem>DIAGNÓSTICO DE PRENHEZ</asp:ListItem>
                <asp:ListItem>PARTO</asp:ListItem>
                <asp:ListItem>ENTRADA EM CONTROLE LEITEIRO</asp:ListItem>
                <asp:ListItem>ENCERRAMENTO DA LACTAÇÃO</asp:ListItem>
            </asp:DropDownList>
        </td>
	</tr>
    <tr>
		<td colspan="2" align="right">&nbsp;</td>
	</tr>
    <tr>
		<td colspan="2" align="center">
			<table>
				<tr>
					<td align="right">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Width="150px" OnClick="btnSalvar_Click" /></td>
					<td align="left"><asp:Button ID="btnVoltar" runat="server" Text="Voltar" Width="150px" OnClick="btnVoltar_Click" /></td>
				</tr>
			</table>
		</td>
		
	</tr>
    </table>
    </div>
    
    </form>
</body>
</html>
