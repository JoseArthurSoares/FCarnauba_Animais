<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlMensuracao.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlMensuracao" %>
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
                    Animal:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlAnimais" runat="server">
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    &nbsp;
                </td>
                <td width="80%">
                    <div class="inputDiv">
                        <cc1:TextBoxWatermarkExtender ID="twePesquisaDdlAnimal" WatermarkCssClass="textfield_vazio01"
									TargetControlID="txtPesquisaDdlAnimal" WatermarkText="RGD ou Nome do animal" runat="server">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:TextBox CssClass="textfield01" ID="txtPesquisaDdlAnimal" runat="server" Width="200px"></asp:TextBox>
                                    <asp:Button ID="btnPesquisarDDlAnimal" runat="server" Text="Pesquisar" OnClick="btnPesquisarDDlAnimal_Click" />
                    </div>

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
                    <asp:ListItem>MAMANDO SEM ORDENHA</asp:ListItem>
                    <asp:ListItem>MAMANDO COM ORDENHA</asp:ListItem>
                    <asp:ListItem>PASTO</asp:ListItem>
                    <asp:ListItem>PASTO COM CONCENTRADO</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Condição de Criação:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ddlCondicoesCriacao" runat="server" autopostback="true" OnSelectedIndexChanged="ddlCondicoesCriacao_SelectedIndexChanged">
                    <asp:ListItem>MAMANDO</asp:ListItem>
                    <asp:ListItem>DESMAMADO</asp:ListItem>
                    <asp:ListItem>DIAGNÓSTICO DE PRENHEZ</asp:ListItem>
                    <asp:ListItem>PARTO</asp:ListItem>
                    <asp:ListItem>ENTRADA EM CONTROLE LEITEIRO</asp:ListItem>
                    <asp:ListItem>ENCERRAMENTO DA LACTAÇÃO</asp:ListItem>
                </asp:DropDownList>
                </td>
            </tr>
            <asp:Panel ID="pnlDesmame" runat="server" Visible=false>
            <tr>
                <td width="20%" class="rotulo">
                    Data do Desmame:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataDesmame" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataDataDesmame" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataDataDesmame_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            </asp:Panel>
            <asp:Panel ID="pnlDiagnosticoPrenhez" runat="server" Visible=false>
            <tr>
                <td width="20%" class="rotulo">
                    Data do Diagnóstico:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataDiagnosticoPrenhez" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataDiagnosticoPrenhez" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataDiagnosticoPrenhez_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            </asp:Panel>
            <asp:Panel ID="pnlParto" runat="server" Visible=false>
            <tr>
                <td width="20%" class="rotulo">
                    Data do Parto:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataParto" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataParto" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataParto_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            </asp:Panel>
            <asp:Panel ID="pnlEntradaControleLeiteiro" runat="server" Visible=false>
            <tr>
                <td width="20%" class="rotulo">
                    Data da Entrada do Controle Leiteiro:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataEntradaControleLeiteiro" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataEntradaControleLeiteiro" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataEntradaControleLeiteiro_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            </asp:Panel>
            <asp:Panel ID="pnlEncerramentoLactacao" runat="server" Visible=false>
            <tr>
                <td width="20%" class="rotulo">
                    Data do Encerramento da Lactação:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataEncerramentoLactacao" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataEncerramentoLactacao" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataEncerramentoLactacao_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            </asp:Panel>
            <tr>
                <td width="20%" class="rotulo">
                    Remover do Controle?
                </td>
                <td width="80%">
                    <asp:CheckBox ID="ckSairControle" runat="server"/>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Data da Remoção do Controle:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtDataSaidaControle" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                    <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                    <asp:customvalidator id="cvDataSaidaControle" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataSaidaControle_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Motivo:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ddlMotivo" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>ABORTO APÓS 9 MESES COM INÍCIO DE OUTRA</asp:ListItem>
                    <asp:ListItem>BAIXA PRODUÇÃO</asp:ListItem>
                    <asp:ListItem>CAUSAS QUE INVALIDAM A PRODUÇÃO</asp:ListItem>
                    <asp:ListItem>DOENÇA DA VACA</asp:ListItem>
                    <asp:ListItem>MORTE OU SEPARAÇÃO DO BEZERRO</asp:ListItem>
                    <asp:ListItem>NATURAL</asp:ListItem>
                    <asp:ListItem>PARTO SEM CONTROLE DE LACTAÇÃO</asp:ListItem>
                    <asp:ListItem>PARTO SUBSEQUENTE SEM O PERÍODO SECO</asp:ListItem>
                    <asp:ListItem>PEITO PERDIDO POR MASTITE</asp:ListItem>
                    <asp:ListItem>PESAGEM ANTERIOR MAIOR QUE 75 DIAS</asp:ListItem>
                    <asp:ListItem>RETIRADA DO CONTROLE LEITEIRO</asp:ListItem>
                    <asp:ListItem>SECAGEM PRÓXIMA AO PARTO</asp:ListItem>
                    <asp:ListItem>TRANSFERÊNCIA PARA OUTRO REBANHO DO PRÓPRIO CRIADOR</asp:ListItem>
                    <asp:ListItem>VENDIDA</asp:ListItem>
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
<div align="right"><a href="./CadastrarLotePonderal.aspx?lotePonderalId=<%=LotePonderalId%>&tabIndex=Mensurações" id="EditarM" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewMensuracao" runat="server" 
    AutoGenerateColumns="False" CellPadding="4"  Width="100%"
    ForeColor="Black" BackColor="White" OnRowDataBound="gridViewMensuracao_RowDataBound"
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
        <asp:BoundField DataField="NomeAnimal" HeaderText="Animal" ></asp:BoundField>
        <asp:BoundField DataField="Peso" HeaderText="Peso" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CEscrotal" HeaderText="C.E.(C. Escrotal)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="AAnterior" HeaderText="A.A.(A. Anterior)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="APosterior" HeaderText="A.P.(A. Posterior)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="LGarupa" HeaderText="L.G.(L. Garupa)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CGarupa" HeaderText="C.G.(C. Garupa)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="CCorporal" HeaderText="C.C.(C. Corporal)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="PToracico" HeaderText="P.T.(P. Torácico)" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="SairControleSr" HeaderText="Removido do Controle?" />
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaMensuracao" 
                    NavigateUrl='<%# "~/EditaMensuracao.aspx?lotePonderalId=" + Request.QueryString["LotePonderalId"] + "&m=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteControlePonderal" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteControlePonderal_Click" OnClientClick="javascript:return perguntam();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>