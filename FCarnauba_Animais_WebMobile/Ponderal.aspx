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

<head id="Head1" runat="server">
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
            height: 115px;
        }

        .header img {
            width: 320px;
            height: 75px;
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
            height: 39px;
            margin-left: 13px;
        }

        .container-table-ponderal{
            margin: 14px;
        }
        
    </style>
    <title></title>
</head>
<body>
<div class="header">
        <a href="https://fazendacarnauba.com/">
            <img src="../img/sgp2.png" alt="Logo da Fazenda">
        </a>
        <div class="header-logout" style="cursor: pointer;" onclick="window.location.href='Account/Login.aspx';">
          <img src="../img/sair.png" alt="Logo">
          <p>Sair</p>
        </div>
    </div>

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
    <div class ="container-table-ponderal">

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
    </div>
    
    <div>
        &nbsp;
    </div>
    <div class="icon">
        <table width="100%">
        <tr>
		<div class="bottom-buttons">
            <asp:Button ID="btnInicio" runat="server" Text="Início" OnClick="btnInicio_Click"/>
            <asp:Button ID="btnEncerrar" runat="server" Text="Encerrar" OnClick="btnEncerrar_Click" Visible="false"/>
        </div>
    <footer>
            <span>&copy; 2024 - Todos os direitos reservados.</span>
            <img src="../img/logo-lightbase2.png" alt="Logo da Lightbase">
        </footer>
		
	    </tr>
	</table>
    </div>
    </form>
</body>
</html>
