<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlMensuracaoAnimal.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlMensuracaoAnimal" %>
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
        <legend><font face="Verdana,Arial,sans-serif" color="black"><b>Mensuração&nbsp;&nbsp;</b></font></legend>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Data:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataPesagem" class="datepick" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataPesagem" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataPesagem_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Peso Kg:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtPeso" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvPeso" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPeso_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    C.E. (C. Escrotal) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtCEscrotal" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvCEscrotal" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvCEscrotal_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    A.A. (A. Anterior) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtAAnterior" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvAAnterior" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvAAnterior_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    A.P. (A. Posterior) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtAPosterior" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvAPosterior" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvAPosterior_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                     L.G. (L. Garupa) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtLGarupa" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvLGarupa" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvLGarupa_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    C.G. (C. Garupa) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtCGarupa" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvCGarupa" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvCGarupa_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    C.C. (C. Corporal) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtCCorporal" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvCCorporal" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvCCorporal_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                     P.T. (P. Torácico) cm:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtPToracico" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvPToracico" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPToracico_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Caracterização Racial:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlCaracterizacoesRaciais" runat="server">
                        <asp:ListItem>ÓTIMA</asp:ListItem>
                        <asp:ListItem>BOA</asp:ListItem>
                        <asp:ListItem>MÉDIA</asp:ListItem>
                        <asp:ListItem>FRACA</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Classificação de Úbere:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlClassificacoesUbere" runat="server">
                        <asp:ListItem>ÓTIMA</asp:ListItem>
                        <asp:ListItem>BOA</asp:ListItem>
                        <asp:ListItem>MÉDIA</asp:ListItem>
                        <asp:ListItem>FRACA</asp:ListItem>
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Regime Alimentar:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ddlRegimeAlimentar" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                </asp:DropDownList>
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
<div align="right"><a href="./CadastrarAnimal.aspx?animalId=<%=AnimalId%>&tabIndex=Mensurações" id="EditarM" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewMensuracaoAnimal" runat="server" 
    AutoGenerateColumns="False" CellPadding="4"  Width="100%"
    ForeColor="Black" BackColor="White" OnRowDataBound="gridViewMensuracaoAnimal_RowDataBound"
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
        <asp:BoundField DataField="DataPesagem" HeaderText="Data" SortExpression="DataPesagem" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
        <asp:BoundField DataField="Peso" HeaderText="Peso" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CEscrotal" HeaderText="C.E.(C. Escrotal)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="AAnterior" HeaderText="A.A.(A. Anterior)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="APosterior" HeaderText="A.P.(A. Posterior)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="LGarupa" HeaderText="L.G.(L. Garupa)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CGarupa" HeaderText="C.G.(C. Garupa)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CCorporal" HeaderText="C.C.(C. Corporal)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="PToracico" HeaderText="P.T.(P. Torácico)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaMensuracao" 
                    NavigateUrl='<%# "~/EditaMensuracaoAnimal.aspx?animalId=" + Request.QueryString["AnimalId"] + "&m=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteMensuracao" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteMensuracao_Click" OnClientClick="javascript:return perguntam();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
