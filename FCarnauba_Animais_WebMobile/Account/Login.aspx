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
<head id="Head1" runat="server">
    <title>SGP - Login</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
    <style>
        
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&display=swap');
        @import url('https://fonts.googleapis.com/css2?family=Delicious+Handrawn&family=Delius&display=swap');
        
        html, body {
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
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
        
         footer {
            background-color: #002855;
            color: #fff;
            text-align: center;
            padding: 35px;
            position: fixed;
            width: 100%;
            height: 111px;
            bottom: 0;
        }

        footer img {
            height: 30px;
            margin-left: 10px;
        }
        
        footer span
        {
            font-family: "Delius", cursive;
            font-weight: 400;
            font-style: normal;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
        <a href="https://fazendacarnauba.com/">
            <img src="../img/sgp2.png" alt="Logo da Fazenda">
        </a>
    </div>
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
					<td align="right" class="textUser">
                        <asp:Button ID="btnEnviar" runat="server" Text="Entrar" 
                            onclick="btnEnviar_Click" /></td>
					<td align="left" class="textPass"><asp:Button ID="btnLimpar" runat="server" Text="Limpar" 
                            onclick="btnLimpar_Click" /></td>
				</tr>
			</table>
		</td>
		
	</tr>
	</table>
    </div>

    <footer>
        <span>&copy; 2024 - Todos os direitos reservados.</span>
        <img src="../img/logo_lightbase.png" alt="Logo da Lightbase">
    </footer>

    </form>
</body>
</html>