<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalhesAnimal.aspx.cs" Inherits="FCarnauba_Animais.DetalhesAnimal" AspCompat="true" %>
<%@ Register Src="~/UserControls/UserControlHistorico.ascx" TagName="UserControlHistorico"
    TagPrefix="uc3" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="Mensagem" TagPrefix="uc4" %>
<%@ Register Src="~/UserControls/UserControlMensuracaoAnimal.ascx" TagName="UserControlMensuracaoAnimal"
    TagPrefix="uc5" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" type="text/css" href="./Styles/smoothness.css" />
     <link rel="stylesheet" type="text/css" href="./Styles/Site.css" />
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
    <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
    
    <script type ="text/javascript" src="./Scripts/validator.js"></script>
    
    <script type="text/javascript" src="./Scripts/autonumeric.js"></script>

    <script type="text/javascript">
        var jq191 = $.noConflict(true);
        jQuery = jq191;
        $ = jQuery;
    </script>
    <script type="text/javascript" src="./Scripts/jquery-ui/jquery-ui-1.10.3.custom.min.js"></script>
    <input type="hidden" name="tabIndex" />
    <script type="text/javascript">
        function hideEditButton() {
            jq191("#tabs").ready(function () {
                jq191("#EditImageButton").hide();
                jq191("#tabs").tabs("option", "activate", null)
            });
        }

        jq191(function () {
            jq191("#tabs").tabs(
             {
                 activate: function (event, ui) {
                     var newId = ui.newPanel.attr('id');
                     if (newId == "historico-tab" || newId == "mensuracao-tab" || newId == "animal-tab") {
                         //jq191("#EditImageButton").hide();
                     } else {
                         //var targUrl = "CadastrarLote.aspx?act=edit&" +
                         //"animalId=" + "<%= AnimalId %>" +
                         //"&tabIndex=" + ui.newTab.context.innerHTML;
                         //jq191("#EditImageButton").show();
                         //jq191("#EditImageButton").attr('href', targUrl);
                     }
                 }
             });
        });



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

        $('img[src="./img/edit_icon.gif"]').live("click", function () {
            ShowProgress();
        });

        $('img[src="./img/adicionar.png"]').live("click", function () {
            ShowProgress();
        });
    </script>
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
    <uc4:Mensagem ID="mensagem" runat="server" />
    <div id="tabs" style="margin-top:20px">
        <ul>
            <li><a href="#animal-tab">Cadastro</a></li>
            <li><a href="#historico-tab">Histórico</a></li>
            <li><a href="#mensuracao-tab">Mensurações</a></li>
        </ul>
    <div id="animal-tab">
        <fieldset class="cadastrar_info">
            <legend>Animal&nbsp;&nbsp;</legend>
            <div align="right"><a href="./CadastrarAnimal.aspx?act=edit&animalId=<%=AnimalId%>" id="EditarA" title="Editar"><img src="./img/edit_icon.gif" /></a></div><br>
            <table width="100%">
                <tr>
                    <td width="15%" class="rotulo">Nome:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblNome" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Nome Completo:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblNomeCompleto" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Número de Ordem:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblNumeroOrdem" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Propriedade:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPropriedade" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Raça:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRaca" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Sexo:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblSexo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgn Série:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgnSerie" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgn Numero:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgnNumero" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgn:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Rgn?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemRgn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgd Série</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgdSerie" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgd Número</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgdNumero" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Rgd:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblRgd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Rgd?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemRgd" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data de Nascimento:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataNascimento" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Peso ao Nascer:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblPn" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Pai:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblNomePai" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Mãe:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblNomeMae" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Cdc Origem:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblCdcOrigem" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data Cdc:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataCdc" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Cdn Origem:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblCdnOrigem" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Foto:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkFoto" runat="server" Text="Download" 
                        onclick="lnkFoto_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo DNA:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoDNA" runat="server" Text="Download" 
                        onclick="lnkLaudoDNA_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo DNA:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoDNA" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo Arquivo Permanente:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoDNA2" runat="server" Text="Download" 
                         onclick="lnkLaudoDNA2_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo Arquivo Permanente?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoArquivoPermanente" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo Secundário 1:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoDNA3" runat="server" Text="Download" 
                         onclick="lnkLaudoDNA3_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo Secundário 1?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoSecundario1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo Secundário 2:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoDNA4" runat="server" Text="Download" 
                         onclick="lnkLaudoDNA4_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo Secundário 2?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoSecundario2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo Beta Caseína:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoBetaCaseina" runat="server" Text="Download" 
                         onclick="lnkLaudoBetaCaseina_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tipo Beta Caseína:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTipoBetaCaseina" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo Beta Caseína?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoBetaCaseina" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Laudo Kappa Caseína:</td>
                    <td width="85%" class="dado">
                        <asp:LinkButton ID="lnkLaudoKappaCaseina" runat="server" Text="Download" 
                         onclick="lnkLaudoKappaCaseina_Click" />
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tipo Kappa Caseína:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTipoKappaCaseina" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tem Laudo kappa Caseína?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTemLaudoKappaCaseina" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Observações:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblObservacoes" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Usuário:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblUsuario" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Data Usuário:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblDataUsuario" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">É Fiv?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblFiv" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Receptora:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblReceptora" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tipo do Parto:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTipoParto" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Vigor do Bezerro:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblVigorBezerro" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Estado Corporal da Mãe:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblEstadoCorporalMae" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Tamanho da Teta:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblTamanhoTeta" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Mãe Boa de Leite:</td>
                    <td width="85%" class="dado"><asp:Label ID="lblMaeBoaLeite" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Mãe Ordenhada?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblMaeOrdenhada" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="15%" class="rotulo">Animal Improdutivo?</td>
                    <td width="85%" class="dado"><asp:Label ID="lblAnimalImprodutivo" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            </fieldset>
    </div>
    <div id="historico-tab">
           <uc3:UserControlHistorico ID="UserControlHistorico1" ReadOnly="true" AnimalId="<%# Convert.ToInt32(AnimalId) %>" runat="server" />
    </div>
    <div id="mensuracao-tab">
            <uc5:UserControlMensuracaoAnimal ID="UserControlMensuracaoAnimal1" ReadOnly="true" AnimalId="<%# Convert.ToInt32(AnimalId) %>" runat="server" />
           
    </div>
    </div>

</asp:Content>
