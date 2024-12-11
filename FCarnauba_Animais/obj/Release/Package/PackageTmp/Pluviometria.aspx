<%@ Page Title="" Language="C#" MasterPageFile="~/SitePluviometria.Master" AutoEventWireup="true" CodeBehind="Pluviometria.aspx.cs" Inherits="FCarnauba_Animais.Pluviometria" AspCompat="true" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
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

        $('#Buscar').live("click", function () {
            ShowProgress();
        });

        $('a[href="Relatorios.aspx?rel=1"]').live("click", function () {
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
        <uc4:Mensagem ID="mensagem" runat="server" />
<div class="barra02" align="left">
			Pluviometria</div>

<div style="margin-top: 5px; margin-bottom: 5px;">
				<asp:Button ID="btnCarnauba" runat="server" EnableViewState="false" CssClass="botaop" BackColor="#96AFC6"
					CausesValidation="false"  Text="Fazenda Carnaúba" onclick="btnCarnauba_Click" /><asp:Button
						ID="btnPauLeite" CausesValidation="false" EnableViewState="false"
						runat="server" CssClass="botaop" BackColor="#96AFC6"  Text="Fazenda Pau Leite" onclick="btnPauLeite_Click" />
                        <asp:Button
						ID="btnBonito" CausesValidation="false" EnableViewState="false"
						runat="server" CssClass="botaop" BackColor="#96AFC6"  Text="Fazenda Bonito" onclick="btnBonito_Click" />
                        <asp:DropDownList ID="ddlAno" runat="server" Width="90"></asp:DropDownList></div>

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
<div align="right"><a href="./CadastrarControlePluviometrico.aspx" id="AdicionaP" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewControlesPluviometricos" runat="server" AllowSorting="False" CellPadding="4"
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
                            <asp:BoundField DataField="Data" HeaderText="DATA" SortExpression="Data" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="Pluviometro" HeaderText="PLUVIOMETRO" SortExpression="Pluviometro"></asp:BoundField>
                            <asp:BoundField DataField="Pluviometria" HeaderText="PLUVIOMETRIA" SortExpression="Pluviometria" DataFormatString="{0:N2}"></asp:BoundField>
                            <asp:TemplateField HeaderText="Ações">
                                <ItemTemplate>
                                    <asp:HyperLink title="Detalhes" runat="server" Text="<img src='./img/details_icon.gif'>" ID="HyperLink1"
                                        NavigateUrl='<%# "~/DetalhesControlePluviometrico.aspx?controlePluviometricoId=" + DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaControlePluviometrico" 
                                        NavigateUrl='<%# "~/CadastrarControlePluviometrico.aspx?act=edit&controlePluviometricoId=" + DataBinder.Eval(Container.DataItem,"Id") %>' />
                                        <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteControlePluviometrico" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteControlePluviometrico_Click" OnClientClick="javascript:return pergunta();" />
                                </ItemTemplate>
                                <ItemStyle Wrap="False" />
                            </asp:TemplateField>
                        </Columns>
    </asp:GridView>
</asp:Content>
