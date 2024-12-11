<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Principal" AspCompat="true" %>

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
    <title>SGP - Início</title>
    <link rel="icon" type="image/png" href="img/favicon-1.png">
	<meta name="viewport" content="width=device-width, initial-scale=1">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
</head>
<div style="background-color:#052B5C" align="left"><a href="https://fazendacarnauba.com/" title="Portal Fazenda Carnaúba"><img src="./img/sgp.png"></a></div>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

     <div>
        &nbsp;
    </div>
    <div class="icon">
        <a href="Leiteiro.aspx"><img class="img-icon" src="img/leiteiro.png"></a>
    </div>
    <div>
        &nbsp;
    </div>
    <div class="icon">
        <a href="Ponderal.aspx"><img class="img-icon" src="img/ponderal.png"></a>
    </div>
    <div>
        &nbsp;
    </div>
    <div class="icon">
        <table width="100%">
        <tr>
		<td align="center">
			<table>
				<tr>
					
					<td align="center"><asp:Button ID="btnSair" runat="server" Text="Sair" Width="200px" OnClick="btnSair_Click"/></td>
                    
				</tr>
			</table>
		</td>
		
	    </tr>
	</table>
    </div>
    
    </form>
</body>
</html>
