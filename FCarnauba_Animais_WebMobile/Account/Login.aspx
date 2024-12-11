<%@ Page Title="Log In" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Account.Login" AspCompat="true" %>
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

<link rel="stylesheet" type="text/css" href="../Styles/SiteMobile.css" />
<link rel="stylesheet" type="text/css" href="../Styles/Site.css" />
<link href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" rel="stylesheet" />    
<head id="Head1" runat="server">
    <title>SGP - Login</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
</head>
<div style="background-color:#052B5C" align="left"><a href="https://fazendacarnauba.com/" title="Portal Fazenda Carnaúba"><img src="../img/sgp.png"></a></div>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="label1" border="0" width="100%">
	<tr>
		<td align="right">&nbsp;</td>
		
	</tr>
    
    
    <tr>
		
		<td><asp:Label ID="lblMensagem" runat="server" Text="Mensagem" Visible="False" ForeColor="red"></asp:Label></td>
        
	</tr>
    
    <tr>
		<td align="right">&nbsp;</td>
		
	</tr>
    <tr>
		
		<td><asp:TextBox ID="txtUsuario" runat="server" placeholder="Usuário"></asp:TextBox></td>
	</tr>
    
    <tr>
		<td align="right">&nbsp;</td>
		
	</tr>
    <tr>
		
		<td><asp:TextBox ID="txtSenha" runat="server" placeholder="Senha" TextMode="Password"></asp:TextBox></td>
	</tr> 

	<tr>
		<td align="right">&nbsp;</td>
		
	</tr>
    <tr>
		<td align="center">
			<table>
				<tr>
					<td align="right">
                        <asp:Button ID="btnEnviar" runat="server" Text="Entrar" 
                            onclick="btnEnviar_Click" /></td>
					<td align="left"><asp:Button ID="btnLimpar" runat="server" Text="Limpar" 
                            onclick="btnLimpar_Click" /></td>
				</tr>
			</table>
		</td>
		
	</tr>
	</table>
    </div>
    </form>
</body>
</html>