<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlProducaoLeite.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlProducaoLeite" %>
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
        <legend><font face="Verdana,Arial,sans-serif" color="black"><b>Produção de Leite&nbsp;&nbsp;</b></font></legend>
        <table width="100%">
            <tr>
                <td width="20%" class="rotulo">
                    Matriz:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlMatrizes" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" AutoPostBack="true" onselectedindexchanged="ddlMatrizes_SelectedIndexChanged">
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
                    Cria:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:DropDownList ID="ddlCrias" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);">
                        </asp:DropDownList></div>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    1ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtPOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1">(Informar apenas números, separar os decimais por vírgula (não usar pontos como separadores).)</span></div>
                    <asp:customvalidator id="cvPOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    2ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtSOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvSOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvSOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    3ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtTOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvTOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvSOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    % Gordura 1ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtGordPOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvGordPOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvGordPOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                     % Gordura 2ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtGordSOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvGordSOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvGordSOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    % Gordura 3ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtGordTOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvGordTOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvGordTOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    % Proteína 1ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtProtPOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvProtPOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvProtPOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                     % Proteína 2ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtProtSOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvProtSOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvProtSOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    % Proteína 3ª Ordenha:
                </td>
                <td width="80%">
                    <div class="inputDiv"><asp:TextBox ID="txtProtTOrdenha" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static"></asp:TextBox>
                    <span class="style1"></span></div>
                    <asp:customvalidator id="cvProtTOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvProtTOrdenha_ServerValidate"></asp:customvalidator>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Bezerros ao Pé?
                </td>
                <td width="80%">
                    <asp:CheckBox ID="ckBezerrosPe" runat="server"/>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Receptora
                </td>
                <td width="80%">
                    <asp:CheckBox ID="ckReceptora" runat="server"/>
                </td>
            </tr>
            <tr>
                <td width="20%" class="rotulo">
                    Tetos Funcionais:
                </td>
                <td width="80%">
                    <asp:DropDownList ID="ddlTetosFuncionais" runat="server">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
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
    function perguntapl() {
        if (confirm("Realmente deseja remover?")) {
            ShowProgress();
            return true;
        } else {
            return false;
        }
    }

    $('form').live("submit", function () {
        ShowProgress();
    });

</script>
<div align="right"><a href="./CadastrarLote.aspx?loteId=<%=LoteId%>&tabIndex=Matrizes e Pesagens" id="EditarPL" title="Adicionar"><img src="./img/adicionar.png" /></a></div><br>
<asp:GridView ID="gridViewProducaoLeite" runat="server" 
    AutoGenerateColumns="False" CellPadding="4"  Width="100%"
    ForeColor="Black" BackColor="White" OnRowDataBound="gridViewProducaoLeite_RowDataBound"
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
        <asp:BoundField DataField="DiasLactacao" HeaderText="Dias de Lactação" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="POrdenha" HeaderText="1ª Ordenha" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="SOrdenha" HeaderText="2ª Ordenha" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="TOrdenha" HeaderText="3ª Ordenha" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="Acumulado" HeaderText="Acumulado" DataFormatString="{0:N2}" HtmlEncode="false" ItemStyle-CssClass="itemStyle"></asp:BoundField>
        <asp:BoundField DataField="SairControleSr" HeaderText="Removido do Controle?" />
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaProducaoLeite" 
                    NavigateUrl='<%# "~/EditaProducaoLeite.aspx?loteId=" + Request.QueryString["LoteId"] + "&pl=" + DataBinder.Eval(Container.DataItem,"Id")  %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteControleLeiteiro" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteControleLeiteiro_Click" OnClientClick="javascript:return perguntapl();" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>