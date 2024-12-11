<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlPastagem.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlPastagem" %>
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
        <legend><font face="Verdana,Arial,sans-serif" color="black"><b>Pastagem&nbsp;&nbsp;</b></font></legend>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Tipo:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlTipos" runat="server">
                        <asp:ListItem>CAPIM BUFFEL GRASS</asp:ListItem>
                        <asp:ListItem>CAPIM UROCLOA</asp:ListItem>
                        <asp:ListItem>LEGUMINOSAS NATIVAS</asp:ListItem>
                        <asp:ListItem>CAATINGA NATIVA</asp:ListItem>
                        <asp:ListItem>TODOS JUNTOS</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Data:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataPastagem" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataPastagem" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataPastagem_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Área (ha):
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtAreaPastagem" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvAreaPastagem" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvAreaPastagem_ServerValidate"></asp:customvalidator>
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
    function perguntap() {
        if (confirm("Realmente deseja remover?")) {
            return true;
        } else {
            return false;
        }
    }
</script>
<div align="right"><a href="./CadastrarEstruturaPropriedade.aspx?estruturaPropriedadeId=<%=EstruturaPropriedadeId%>&tabIndex=Pastagens" id="EditarP" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewPastagens" runat="server" 
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
        <asp:BoundField DataField="Tipo" HeaderText="Tipo" ></asp:BoundField>
        <asp:BoundField DataField="Data" HeaderText="Data" SortExpression="Data" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
        <asp:BoundField DataField="Area" HeaderText="Área" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="AreaTotalAcumulada" HeaderText="Área Total Acumulada" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaPastagem" 
                    NavigateUrl='<%# "~/EditaPastagem.aspx?estruturaPropriedadeId=" + Request.QueryString["EstruturaPropriedadeId"] + "&p=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteEstruturaPropriedade" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteEstruturaPropriedade_Click" OnClientClick="javascript:return perguntap();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>