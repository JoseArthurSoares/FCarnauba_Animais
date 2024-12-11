<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlHistorico.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlHistorico" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Mensagem.ascx" TagName="Mensagem" TagPrefix="uc2" %>
<style type="text/css">
    .style1
    {
        color: #FF0000;
    }
</style>

<script>
    function goBack() {
        window.history.back();
    }
</script>
<uc2:Mensagem ID="mensagem" runat="server" />
<asp:Panel ID="pnlFields" runat="server">
    <fieldset class="cadastrar_info">
        <legend><font face="Verdana,Arial,sans-serif" color="black"><b>Histórico&nbsp;&nbsp;</b></font></legend>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Movimento:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlMovimentos" runat="server">
                        <asp:ListItem>Abatido</asp:ListItem>
                        <asp:ListItem>Aborto</asp:ListItem>
                        <asp:ListItem>Acidente</asp:ListItem>
                        <asp:ListItem>Adquirido</asp:ListItem>
                        <asp:ListItem>Ativo</asp:ListItem>
                        <asp:ListItem>Composição Genealógica</asp:ListItem>
                        <asp:ListItem>Descartado</asp:ListItem>
                        <asp:ListItem>Inativo</asp:ListItem>
                        <asp:ListItem>Morto</asp:ListItem>
                        <asp:ListItem>Receptora</asp:ListItem>
                        <asp:ListItem>Vendido</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Data:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataHistorico" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataHistorico" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataHistorico_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" valign="top" class="rotulo">
                    Observações:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtObservacoes" runat="server" Height="62px" TextMode="MultiLine"
                        Width="277px" onfocus="Javascript:ValidateForm(this,false,RetFalse);" onblur="Javascript:ValidateForm(this,true,RetFalse);"></asp:TextBox></div>
                </td>
            </tr>
        </table>
    </fieldset>
    <p align="center">

    <asp:Button ID="btnCadastrar" runat="server" Text="Salvar" 
        OnClick="btnCadastrar_Click" BackColor="#052B5C" ForeColor="White"></asp:Button> &nbsp;&nbsp;
        <asp:Button ID="btnCancelar" onclientclick="goBack();return false;" runat="server" 
                            Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
</p>
</asp:Panel>
<script type="text/javascript">
    function perguntah() {
        if (confirm("Realmente deseja remover?")) {
            return true;
        } else {
            return false;
        }
    }
</script>
<div align="right"><a href="./CadastrarAnimal.aspx?animalId=<%=AnimalId%>&tabIndex=Histórico" id="EditarH" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewHistorico" runat="server" 
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
        <asp:BoundField DataField="Movimento" HeaderText="Movimento" ></asp:BoundField>
        <asp:BoundField DataField="DataManejo" HeaderText="Data" SortExpression="DataManejo" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
        <asp:BoundField DataField="Observacao" HeaderText="Observação" ></asp:BoundField>
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaHistorico" 
                    NavigateUrl='<%# "~/EditaHistorico.aspx?animalId=" + Request.QueryString["AnimalId"] + "&h=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteHistorico" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteHistorico_Click" OnClientClick="javascript:return perguntah();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
