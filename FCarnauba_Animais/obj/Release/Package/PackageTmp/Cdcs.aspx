<%@ Page Title="" Language="C#" MasterPageFile="~/SiteCDC.Master" AutoEventWireup="true" CodeBehind="Cdcs.aspx.cs" Inherits="FCarnauba_Animais.Cdcs" AspCompat="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
    <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 2px solid black;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        function pergunta() {
            if (confirm("Realmente deseja remover?")) {
                return true;
            } else {
                return false;
            }
        }

        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        $('button').live("click", function () {
            //ShowProgress();
        });

        $('img[src="./img/adicionar.png"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/edit_icon.gif"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/details_icon.gif"]').live("click", function () {
            ShowProgress();
        });

        function LimparBuscaSimples() {

            document.getElementById('<%=txtBusca.ClientID%>').value = "";
        }

</script>
<div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div class="barra02" align="left">
			Cruzamentos</div>
            <div style="margin-top: 5px; margin-bottom: 5px;">
				<asp:Button ID="btnGuzera" runat="server" EnableViewState="false" CssClass="botaop" BackColor="#96AFC6"
					CausesValidation="false"  Text="Guzerá" onclick="btnGuzera_Click" /><asp:Button
						ID="btnSindi" CausesValidation="false" EnableViewState="false"
						runat="server" CssClass="botaop" BackColor="#96AFC6"  Text="Sindi" onclick="btnSindi_Click" />
                        <asp:Button
						ID="btnCpd" CausesValidation="false" EnableViewState="false"
						runat="server" CssClass="botaop" BackColor="#96AFC6"  Text="CPD" onclick="btnCpd_Click" />
                        <asp:DropDownList ID="ddlPropriedade" runat="server" Height="18px" Width="215" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                        </asp:DropDownList></div>

<div id="controles">
<table width="100%" >
    <tr>
        <td style="width: 50px">Busca:</td>
        <td style="width: 300px">
            <asp:TextBox ID="txtBusca" runat="server" Width="300"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" 
                onclick="btnBuscar_Click" BackColor="#052B5C" ForeColor="White" />

                <input type="button" id="btnLimpar" name="btnLimpar" value="Limpar" style="background-color: #052B5C;color: white;" onclick="LimparBuscaSimples();"></input>

            
        </td>
        
        <td>
            <!--<input type="button" id="btnBuscaAvancada" name="btnBuscaAvancada" value="Busca Avançada" style="background-color: #052B5C;color: white;"  onclick="openWinNavigateUrl()"></input>-->
            
        </td>
    </tr>
</table>
</div>
<br />
<input id="hidSortDirection" type="hidden" name="hidSortDirection" runat="server" />
<div align="right"><a href="./CadastrarCdc.aspx" id="EditarM" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
    <asp:GridView ID="gridViewCdcs" runat="server" AllowSorting="False" CellPadding="4"
        ForeColor="#333333"  GridLines="None" BackColor="White"
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                AutoGenerateColumns="False" Width="100%">
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
                            <asp:BoundField DataField="NCdc" HeaderText="CDC" SortExpression="NCdc" ></asp:BoundField>
                            <asp:BoundField DataField="DataCobertura" HeaderText="DATA DA COBERTURA" SortExpression="DataCobertura" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="NomeTouro" HeaderText="NOME DO TOURO" SortExpression="NomeTouro"></asp:BoundField>
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:HyperLink title="Detalhes" runat="server" Text="<img src='./img/details_icon.gif'>" ID="HyperLink1"
                                        NavigateUrl='<%# "~/DetalhesCdc.aspx?cdcId=" + DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaLote" 
                                        NavigateUrl='<%# "~/CadastrarCdc.aspx?act=edit&cdcId=" + DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteCdc" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteCdc_Click" OnClientClick="javascript:return pergunta();" />
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
    </asp:GridView>
</asp:Content>
