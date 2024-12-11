<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserControlCadastraControleLeiteiro.ascx.cs" Inherits="FCarnauba_Animais.UserControls.UserControlCadastraControleLeiteiro" %>
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
<script type="text/javascript">

    $(document).ready(function () {
        //alert('UserControl ready');
        if ($("#txtPOrdenha").length > 0) $("#txtPOrdenha").mask("99:99");
        if ($("#txtSOrdenha").length > 0) $("#txtSOrdenha").mask("99:99");
        if ($("#txtTOrdenha").length > 0) $("#txtTOrdenha").mask("99:99");
        if ($("#txtDataControle").length > 0) $("#txtDataControle").mask("99/99/9999");
        if ($("#txtDataProximaVisita").length > 0) $("#txtDataProximaVisita").mask("99/99/9999");
        if ($("#ddlFisicaOuJuridica").length > 0) $("#ddlFisicaOuJuridica").change(updateCpfCnpj);
        var numSettings = { aSep: '.', aDec: ',', aSign: 'R$ ', vMax: '999999999999999.99', vMin: '0.00' };
        //$("#txtValorObra").autoNumeric(numSettings);
        //$("#txtValorObra").autoNumericSet(parseFloat($("#txtValorObra").val().replace(",", ".")));
        //updateKmlInput();
       // updateCpfCnpj();
        $("input").each(function (k, v) { $(v).blur(); });
    });

    function perguntacontroleleiteiro() {
        if (confirm("Realmente deseja remover este controle leiteiro?")) {
            return true;
        } else {
            return false;
        }
    }
</script>
<uc2:Mensagem ID="mensagem" runat="server" />
<asp:Panel ID="Panel1" runat="server">
    <fieldset class="cadastrar_info">
        <legend>Controle Leiteiro&nbsp;&nbsp;</legend> 
            <table id="cadr_controleleiteiro_tbl">
                <tr>
	                <td width="20%" class="rotulo">
	                    Categoria:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtCategoria" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        <asp:customvalidator id="cvCategoria" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvCategoria_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
                    <td width="20%" class="rotulo">
                        Data do Controle:
                    </td>
                    <td width="80%">
                        <div class="inputDiv"><asp:TextBox ID="txtDataControle" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                        <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvDataControle" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataControle_ServerValidate"></asp:customvalidator>
                    </td>
                </tr>
                <tr>
                    <td width="20%" class="rotulo">
                        Data Próxima Visita:
                    </td>
                    <td width="80%">
                        <div class="inputDiv"><asp:TextBox ID="txtDataProximaVisita" runat="server" onfocus="Javascript:ValidateForm(this,false,RetTrue);" onblur="Javascript:ValidateForm(this,true,RetTrue);" ClientIDMode="Static" placeholder="__/__/____"></asp:TextBox>
                        <span class="style1">(Informar só os números, no formato DD/MM/AAAA.)</span></div>
                        <asp:customvalidator id="cvDataProximaVisita" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvDataProximaVisita_ServerValidate"></asp:customvalidator>
                    </td>
                </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                    Hora 1ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtPOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
                        <asp:customvalidator id="cvPOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvPOrdenha_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                    Hora 2ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtSOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
                        <asp:customvalidator id="cvSOrdenha" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvSOrdenha_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                    Hora 3ª Ordenha:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtTOrdenha" runat="server" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" ClientIDMode="Static" placeholder="__:__"></asp:TextBox></div>
	                </td>
	            </tr>
                <tr>
	                <td width="20%" class="rotulo">
	                   Controlador:</td>
	                <td width="80%">
	       
	                    <div class="inputDiv"><asp:TextBox ID="txtControlador" runat="server" Width="200px" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);"></asp:TextBox></div>
                        <asp:customvalidator id="cvControlador" runat="server" forecolor="Red" errormessage="*" onservervalidate="cvControlador_ServerValidate"></asp:customvalidator>
                        <span class="style1">*</span>
	                </td>
	            </tr>
        </table>
    </fieldset>
    <p style="text-align: center">
                        <asp:Button ID="btnAdicionaControleLeiteiro" onclick="btnAdicionaControleLeiteiro_Click" runat="server" 
                            Text="Salvar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" /> &nbsp;&nbsp;
                            <asp:Button ID="btnCancelar" onclientclick="goBack();return false;" runat="server" 
                            Text="Cancelar" ClientIDMode="Static" CausesValidation="False" BackColor="#052B5C" ForeColor="White" />
                    </p>
</asp:Panel>
<br /><br />
<asp:GridView ID="gridControles" runat="server" CellPadding="4" 
            GridLines="None" ClientIDMode="Static" 
    AutoGenerateColumns="False" onrowdatabound="gridControles_RowDataBound" BackColor="White" 
        BorderColor="#156AE9" ForeColor="Black" BorderStyle="Solid" 
    BorderWidth="1px" CssClass="gvclass" Width="100%">
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
        <asp:BoundField DataField="Categoria" HeaderText="Categoria" />
        <asp:BoundField DataField="DataControle" HeaderText="Data do Controle" DataFormatString="{0:d}" ></asp:BoundField>
        <asp:BoundField DataField="POrdenha" HeaderText="Hora 1ª Ordenha" />
        <asp:BoundField DataField="SOrdenha" HeaderText="Hora 2ª Ordenha" />
        <asp:BoundField DataField="TOrdenha" HeaderText="Hora 3ª Ordenha" />
        <asp:BoundField DataField="Controlador" HeaderText="Controlador" />
        <asp:TemplateField HeaderText="Ações">
            <ItemTemplate>
                <asp:HyperLink title="Editar" runat="server" Text="<img src='./img/edit_icon.gif'>" ID="btnEditaObra" 
                    NavigateUrl='<%# "~/EditaControleLeiteiro.aspx?act=edit&controleLeiteiroId=" + DataBinder.Eval(Container.DataItem,"Id") + "&tabIndex=Controle%20Leiteiro" %>' />
                <asp:LinkButton title="Remover" runat="server" Text="<img src='./img/delete_icon.png'>" ID="btnDeleteControleLeiteiro" CommandArgument='<%#Bind("Id") %>' OnClick="btnDeleteControleLeiteiro_Click" OnClientClick="javascript:return perguntacontroleleiteiro();" />
            </ItemTemplate>
            <FooterStyle Wrap="False" />
            <HeaderStyle Wrap="False" />
            <ItemStyle Wrap="False" />
         </asp:TemplateField>      
    </Columns>
</asp:GridView>