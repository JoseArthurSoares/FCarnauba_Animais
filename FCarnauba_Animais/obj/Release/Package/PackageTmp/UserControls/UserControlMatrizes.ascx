<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlMatrizes.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlMatrizes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<script language=javascript>
    function goBack() {
        window.history.back();
    }
</script>
<uc2:Mensagem ID="mensagem" runat="server" />
<asp:Panel ID="pnlFields" runat=server>
<fieldset class="cadastrar_info">
            <legend>Matrizes&nbsp;&nbsp;</legend>
<table>
    <tr>
        <td class="rotulo">
            Matriz:
        </td>
        <td>
            <div class="inputDiv"><asp:DropDownList ID="ddlMatriz" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" AutoPostBack="true" onselectedindexchanged="ddlMatriz_SelectedIndexChanged">
            </asp:DropDownList></div>
        </td>
    </tr>
    <tr>
        <td class="rotulo">
            Cria:
        </td>
        <td>
            <div class="inputDiv"><asp:DropDownList ID="ddlCria" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" AutoPostBack="true" onselectedindexchanged="ddCria_SelectedIndexChanged">
            </asp:DropDownList></div>
        </td>
    </tr>
    <tr>
        <td width="20%" class="rotulo">
            Em controle leiteiro?
        </td>
        <td width="80%">
            <asp:CheckBox ID="ckControleLeiteiro" runat="server"/>
        </td>
    </tr>
    <tr>
        <td width="20%" class="rotulo">
            Entrada do controle:
        </td>
        <td width="80%">
            <div class="inputDiv"><asp:TextBox ID="txtEntradaControle" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
            <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
            <asp:customvalidator id="cvEntradaControle" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvEntradaControle_ServerValidate"></asp:customvalidator>
        </td>
    </tr>
    <tr>
        <td width="20%" class="rotulo">
            Saída do controle:
        </td>
        <td width="80%">
            <div class="inputDiv"><asp:TextBox ID="txtSaidaControle" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
            <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
            <asp:customvalidator id="cvSaidaControle" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvSaidaControle_ServerValidate"></asp:customvalidator>
        </td>
    </tr>
</table>
</fieldset>
<p align="center">
    <asp:Button ID="btnCadastrar" runat="server" Text="Salvar" 
        OnClick="btnCadastrar_Click" BackColor="#052B5C" ForeColor="White"></asp:Button> &nbsp;&nbsp;
        
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" onclick="btnVoltar_Click" />

</p>
</asp:Panel>
<script type="text/javascript">
    function perguntamat() {
        if (confirm("Realmente deseja remover esta matriz?")) {
            return true;
        } else {
            return false;
        }
    }
</script>
<asp:Label ID="lblMensagemMatrizes" runat="server" CssClass="mensagem" Text="Não existem matrizes cadastradas para este lote!"></asp:Label>
<div align="right"><a href="./CadastrarLote.aspx?loteId=<%=loteId%>&act=new&tabIndex=Matrizes" id="EditarM" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewMatrizes" runat="server" 
    AutoGenerateColumns="false" CellPadding="4" 
    ForeColor="Black" BackColor="White" 
        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CssClass="gvclass"
    onrowdatabound="gridViewMatrizes_RowDataBound" Width="100%">
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
        <asp:BoundField DataField="NomeMatriz" HeaderText="Matriz" />
        <asp:BoundField DataField="EmControleLeiteiroStr" HeaderText="Em Controle Leiteiro" />
        <asp:BoundField DataField="DataEntradaControle" HeaderText="Entrada do Controle" DataFormatString="{0:d}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="DataSaidaControle" HeaderText="Saída do Controle" DataFormatString="{0:d}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <!-- <asp:HyperLink title="Detalhes" runat="server" Text="<img src='./img/details_icon.gif'>" ID="HyperLink1" 
                    NavigateUrl='<%# "~/DetalhesMatriz.aspx?loteId=" + Request.QueryString["loteId"] + "&matrizId=" + DataBinder.Eval(Container.DataItem,"Id") %>' /> -->
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaMatriz" 
                    NavigateUrl='<%# "~/CadastrarLote.aspx?loteId=" + Request.QueryString["loteId"] + "&matrizId=" + DataBinder.Eval(Container.DataItem,"Id") + "&act=edit&tabIndex=Matrizes" %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteMatriz" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"Id") %>' OnClick="btnDeleteMatriz_Click" OnClientClick="javascript:return perguntamat();" />
                
            </ItemTemplate>
            <FooterStyle Wrap="False" />
            <HeaderStyle Wrap="False" />
            <ItemStyle Wrap="False" />
         </asp:TemplateField>       
    </Columns>
</asp:GridView>