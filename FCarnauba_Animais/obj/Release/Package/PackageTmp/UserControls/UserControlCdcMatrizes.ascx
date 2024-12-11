<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlCdcMatrizes.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlCdcMatrizes" %>
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
        <legend><font face="Verdana,Arial,sans-serif" color="black"><b>Matrizes&nbsp;&nbsp;</b></font></legend>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Matriz:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlMatrizes" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    &nbsp;
                </td>
                <td width="80%">
                    <div class="inputDiv">
                        <cc1:TextBoxWatermarkExtender ID="twePesquisaDdlMatriz" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaDdlMatriz" WatermarkText="RGD ou Nome da Matriz" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:TextBox CssClass="textfield01" ID="txtPesquisaDdlMatriz" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisarDDlMatriz" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlMatriz_Click" />
                    </div>

                </td>
            </tr>
            <tr>
        <td width="20%" class="rotulo">
            Cobertura Efetiva?
        </td>
        <td width="80%">
            <asp:CheckBox ID="ckCoberturaEfetiva" runat="server"/>
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
    function perguntam() {
        if (confirm("Realmente deseja remover?")) {
            return true;
        } else {
            return false;
        }
    }
</script>
<div align="right"><a href="./CadastrarCdc.aspx?cdcId=<%=CdcId%>&tabIndex=Matrizes" id="EditarM" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewCdcMatrizes" runat="server" 
    AutoGenerateColumns="False" CellPadding="4"  Width="100%"
    ForeColor="Black" BackColor="White" OnRowDataBound="gridViewCdcMatrizes_RowDataBound"
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
        <asp:BoundField DataField="NomeMatriz" HeaderText="Matriz" ></asp:BoundField>
        <asp:BoundField DataField="CioRepeticao" HeaderText="Cio" />
        <asp:BoundField DataField="CdcEfetivaStr" HeaderText="Cobertura Efetiva?" />
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaCdcMatriz" 
                    NavigateUrl='<%# "~/EditaCdcMatriz.aspx?cdcId=" + Request.QueryString["CdcId"] + "&m=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteCdcMatriz" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteCdcMatriz_Click" OnClientClick="javascript:return perguntam();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
