<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ponderal.aspx.cs" Inherits="FCarnauba_Animais_WebMobile.Ponderal" AspCompat="true" %>

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
    <title>SGP - Ponderal</title>
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
    <form id="form1" runat="server">

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
    <asp:GridView ID="gridViewMensuracoes" runat="server" 
    AutoGenerateColumns="False" CellPadding="4"  Width="100%"
    ForeColor="Black" BackColor="White"
    BorderColor="#156AE9" BorderStyle="Solid" BorderWidth="1px" CssClass="gvclass">
    <RowStyle CssClass="gridlinha01" />
					    <AlternatingRowStyle CssClass="gridlinha02" />
					    <HeaderStyle CssClass="gridtitulo" />
					    <FooterStyle CssClass="gridrodape" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" CssClass="sortasc"/>
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" CssClass="sortdesc"/>
    <Columns>
        <asp:BoundField DataField="NomeAnimal" HeaderText="Animal" HeaderStyle-Width="50%" ></asp:BoundField>
        <asp:BoundField DataField="Peso" HeaderText="Peso" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle" HeaderStyle-Width="25%"></asp:BoundField>
        <asp:TemplateField HeaderText="Ações" HeaderStyle-Width="25%">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaProducaoLeite" 
                    NavigateUrl='<%# "~/EditaMensuracao.aspx?lotePonderalId=" + lotePonderalId + "&p=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
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
