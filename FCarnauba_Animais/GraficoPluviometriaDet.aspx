<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="GraficoPluviometriaDet.aspx.cs" Inherits="FCarnauba_Animais.GraficoPluviometriaDet" AspCompat="true" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="conteudo" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    <script type ="text/javascript">
        $(document).ready(function () {
            //if ($("#txtEditData").length > 0) $("#txtEditData").mask("99/99/9999");
            //if ($("#txtAddData").length > 0) $("#txtAddData").mask("99/99/9999");

            if ($("#txtData").length > 0) $("#txtData").mask("99/99/9999");

        });
    </script>    

	<asp:ScriptManager ID="scriptManager" runat="server">
	</asp:ScriptManager>

    <div>
		<div class="barra02" align="left">
			Pluviometria</div>
		
				<uc2:Mensagem ID="mensagem" runat="server" />
				
			
				<table>
					<tr>
						<td>
							Propriedade:</td>
						<td>
                            <asp:DropDownList ID="ddlPropriedade" runat="server">
                            </asp:DropDownList>
						</td>

                        <td>
							Ano:</td>
						<td>
                            <asp:DropDownList ID="ddlAno" runat="server" Height="18px" Width="150" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                            </asp:DropDownList>
                            <asp:Button ID="btnConsultar" runat="server" CssClass="botao" Text="Consultar" OnClick="btnConsultar_Click" />
						</td>
                        
					</tr>
					
				</table>
				<hr class="busca_barra_rodape" />
	</div>
    <rsweb:reportviewer ID="reportViewer" runat="server" 
        Style="width: 100%; height: 400px;">
</rsweb:reportviewer>
</asp:Content>
