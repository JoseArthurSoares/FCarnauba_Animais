<%@ Page Title="Log In" Language="C#" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="FCarnauba_Animais.Account.Login" AspCompat="true" %>

<html>

<meta http-equiv="Content-Type" content="text/html;charset=UTF-8">
<head>
<title>Sistema de Gestão Pecuária - Fazenda Carnaúba</title>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/geral000.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/reset000.css" rel="stylesheet" type="text/css" />
</head>
<body class="logbdy">
    <div id="topo">
        <div class="conteudo2">
            
                <img id="logoGov" src="../img/logoFaze.png">
            
            
                <img id="logoSigePb" src="../img/logoLisa.png">
            
        </div>
    </div>
    <div class="wrapper">
        <asp:Label ID="Label1" runat="server" 
                    Text="Sistema de Gestão Pecuária" style="display:inline-block;font-size: 30px;" 
                     />
        <div class="login_main">
            <form id="login" runat="server">
                
&nbsp;<label>Usuário:</label><asp:TextBox ID="txtUser" runat="server" />
               <label>Senha:</label><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                <asp:Button ID="btnEntra" runat="server" Text="Entrar" onclick="btnEntra_Click" />
                <asp:Button ID="btnConvidado" runat="server" Text="Convidado" onclick="btnConvidado_Click" Visible="False" />
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </form>
        </div>
        <div id="rodape2">
            <div>
                <div class="conteudo" align="center">
                    
                </div>
            </div>
        </div>
    </div>
</body>
</html>