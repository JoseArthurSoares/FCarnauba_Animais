<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInicio.Master" AutoEventWireup="true" CodeBehind="RelLotePonderal.aspx.cs" Inherits="FCarnauba_Animais.RelLotePonderal" AspCompat="true" %>
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

        //        function getParameterByName(name, url = window.location.href) {
        //            name = name.replace(/[\[\]]/g, '\\$&');
        //            var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        //                results = regex.exec(url);
        //                if (!results) return null;
        //                if (!results[2]) return '';
        //                return decodeURIComponent(results[2].replace(/\+/g, ' '));
        //        }

        //        var op = getParameterByName('op');


        //        
        //            $('a[href="Animais.aspx"]').live("click", function () {
        //                jq191("#dialog-modal").dialog("open");
        //                return false;
        //            });


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
        $('a[href="Relatorios.aspx?rel=2"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=3"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=4"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=5"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=6"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=7"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=8"]').live("click", function () {
            ShowProgress();
        });
        $('a[href="Relatorios.aspx?rel=9"]').live("click", function () {
            ShowProgress();
        });

        function LimparBuscaSimples() {

            document.getElementById('<%=txtBusca.ClientID%>').value = "";
            //$("input[name='RIL']").hide();
        }

        function LimparBuscaAvancada() {

            document.getElementById('<%=txtNome.ClientID%>').value = "";
            document.getElementById('<%=txtRgd.ClientID%>').value = "";

            var ddlSexo = document.getElementById('<%=ddlSexo.ClientID%>');
            ddlSexo.selectedIndex = 0;

            var ddlPai = document.getElementById('<%=ddlPai.ClientID%>');
            ddlPai.selectedIndex = 0;

            var ddlMae = document.getElementById('<%=ddlMae.ClientID%>');
            ddlMae.selectedIndex = 0;

            var ddlRaca = document.getElementById('<%=ddlRaca.ClientID%>');
            ddlRaca.selectedIndex = 0;

            $("input[name='calNascimentoInicial']").val('');
            $("input[name='calNascimentoFinal']").val('');

            var ddlPropriedade = document.getElementById('<%=ddlPropriedade.ClientID%>');
            ddlPropriedade.selectedIndex = 0;

            document.getElementById('<%=txtOrdem.ClientID%>').value = "";

            var ddlBetaCaseina = document.getElementById('<%=ddlBetaCaseina.ClientID%>');
            ddlBetaCaseina.selectedIndex = 0;

            var ddlKappaCaseina = document.getElementById('<%=ddlKappaCaseina.ClientID%>');
            ddlKappaCaseina.selectedIndex = 0;

            var ddlFiv = document.getElementById('<%=ddlFiv.ClientID%>');
            ddlFiv.selectedIndex = 0;

            var ddlMovimento = document.getElementById('<%=ddlMovimento.ClientID%>');
            ddlMovimento.selectedIndex = 0;

            document.getElementById('<%=txtObservacao.ClientID%>').value = "";

        }


</script>
    <div class="loading" align="center">
            Carregando... Por favor aguarde.<br />
            <br />
            <img src="./img/loading_swirl.gif" alt="" />
        </div>
<div class="barra02" align="left">
			Relatório Controle Ponderal</div>
<div id="controles">
<table width="100%" border >
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
        <!--<td>
            <span id="busca_avancada_button" onclick="openWinNavigateUrl()">Busca Avançada</span>
        </td>-->
        <td>
            <input type="button" id="btnBuscaAvancada" name="btnBuscaAvancada" value="Busca Avançada" style="background-color: #052B5C;color: white;"  onclick="openWinNavigateUrl()"></input>
            
        </td>
    </tr>
</table>
</div>
<br />
<div id="dialog-modal" title="Busca Avançada" class="jqmodal">
        <script type="text/javascript" src="./Scripts/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="./Scripts/jquery-migrate-1.0.0.js"></script>
        <script type="text/javascript" src="./Scripts/jquery.maskedinput.js"></script>
        
        <script type="text/javascript">
            var jq191 = $.noConflict(true);
            jQuery = jq191;
            $ = jQuery;
        </script>
        <script type="text/javascript" src="./Scripts/jquery-ui-1.10.3.custom.min.js"></script>
            <script type="text/javascript">

                jq191(function () {
                    jq191("#dialog-modal").dialog({
                        width: 640,
                        appendTo: 'form',
                        resizable: false,
                        autoOpen: false,
                        closeText: "Fechar",
                        buttons: [
                                    {
                                        text: "Buscar", id: "Buscar", click: function () {
                                            __doPostBack("btnBuscaAvancadaOK_Click", '');
                                            jq191(this).dialog("close");
                                        }
                                    },
                                    {
                                        text: "Cancelar", click: function () {
                                            jq191(this).dialog("close");
                                        }
                                    },

                                    {
                                        text: "Limpar", click: function () {
                                            LimparBuscaAvancada();
                                        }
                                    }

                                 ],
                        modal: true
                    });
                });
                jq191(function () {
                    var dateOpt = {
                        constrainInput: true,
                        minDate: new Date(2001, 0, 1),
                        maxDate: new Date(),
                        /*            pt-br locale        */
                        dateFormat: "dd/mm/yy",
                        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
                        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
                        nextText: 'Próximo',
                        prevText: 'Anterior'
                    };
                    $("input[name='calNascimentoInicial']").mask("99/99/9999").datepicker(dateOpt);
                    $("input[name='calNascimentoFinal']").datepicker(dateOpt).mask("99/99/9999");
                });
                function close_jq_date() {
                    alert("dsfjdslkfjsdlkfj");
                    //$("input[name='calPagamentosInicial']").hide();
                    //$("input[name='calPagamentosFinal']").hide();
                }
                function openWinNavigateUrl() {
                    jq191("#dialog-modal").dialog("open");
                }
        </script>
        <table>
        <tr>
            <td>Nome:</td>
            <td><asp:TextBox ID="txtNome" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>RGD:</td>
            <td><asp:TextBox ID="txtRgd" runat="server"></asp:TextBox></td>
        </tr>
        
        <tr>
            <td>Sexo:</td>
            <td><asp:DropDownList ID="ddlSexo" runat="server">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem>Macho</asp:ListItem>
                <asp:ListItem>Fêmea</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Raça:</td>
            <td><asp:DropDownList ID="ddlRaca" Name="ddlRaca" runat="server" Width="200" ViewStateMode="Enabled">
             <asp:ListItem>SINDI</asp:ListItem>
             <asp:ListItem>ZEBUÍNAS</asp:ListItem>
             <asp:ListItem>GUZERÁ</asp:ListItem>
             <asp:ListItem>CURRALEIRO PÉ DURO</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Pai:</td>
            <td><asp:DropDownList ID="ddlPai" Name="ddlPai" runat="server" Width="400" ViewStateMode="Enabled">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Mãe:</td>
            <td><asp:DropDownList ID="ddlMae" Name="ddlMae" runat="server" Width="400" ViewStateMode="Enabled">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Nascimento</td>
            <td>
                Período de <input type="text" name="calNascimentoInicial" placeholder="__/__/____" class="date_size" /> a
                            <input type="text" name="calNascimentoFinal" placeholder="__/__/____" class="date_size" /> &nbsp; 
            </td>
        </tr>
        <tr>
            <td>Propriedade:</td>
            <td><asp:DropDownList ID="ddlPropriedade" Name="ddlPropriedade" runat="server" Width="400" ViewStateMode="Enabled">
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Número de Ordem:</td>
            <td><asp:TextBox ID="txtOrdem" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Beta Caseína:</td>
            <td><asp:DropDownList ID="ddlBetaCaseina" runat="server">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem>A1A1</asp:ListItem>
                <asp:ListItem>A1A2</asp:ListItem>
                <asp:ListItem>A2A2</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Kappa Caseína:</td>
            <td><asp:DropDownList ID="ddlKappaCaseina" runat="server">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem>A/A</asp:ListItem>
                <asp:ListItem>A/B</asp:ListItem>
                <asp:ListItem>B/B</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Fiv:</td>
            <td><asp:DropDownList ID="ddlFiv" runat="server">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem>Sim</asp:ListItem>
                <asp:ListItem>Não</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Movimento:</td>
            <td><asp:DropDownList ID="ddlMovimento" runat="server">
                <asp:ListItem Selected="True"></asp:ListItem>
                <asp:ListItem>Acidente</asp:ListItem>
                <asp:ListItem>Adquirido</asp:ListItem>
                <asp:ListItem>Ativo</asp:ListItem>
                <asp:ListItem>Composição Genealógica</asp:ListItem>
                <asp:ListItem>Descartado</asp:ListItem>
                <asp:ListItem>Inativo</asp:ListItem>
                <asp:ListItem>Morto</asp:ListItem>
                <asp:ListItem>Vendido</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
            <td>Observação:</td>
            <td><asp:TextBox ID="txtObservacao" runat="server"></asp:TextBox></td>
        </tr>
        </table>        
    </div>
    <input id="hidSortDirection" type="hidden" name="hidSortDirection" runat="server" />
    <asp:GridView ID="gridViewAnimais" runat="server" AllowSorting="False" CellPadding="4"
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
                            <asp:BoundField DataField="Rgd" HeaderText="RGD" SortExpression="RGD" ></asp:BoundField>
                            <asp:BoundField DataField="Nome" HeaderText="NOME" SortExpression="Nome"></asp:BoundField>
                            <asp:BoundField DataField="Sexo" HeaderText="SEXO" SortExpression="Sexo"></asp:BoundField>
                            <asp:BoundField DataField="DataNascimento" HeaderText="DATA DO NASCIMENTO" SortExpression="DataNascimento" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:BoundField DataField="NomePai" HeaderText="PAI" SortExpression="NomePai"></asp:BoundField>
                            <asp:BoundField DataField="NomeMae" HeaderText="MÃE" SortExpression="NomeMae"></asp:BoundField>
                            <asp:TemplateField HeaderText="Relatório">
                                <ItemTemplate>
                                    <input type="image" src="./img/pdf_icon.png" name="RPI2"  value="RPI" title="RPI"  onclick="javascript:window.open('RelatoriosControlePonderal.aspx?animalId=<%# DataBinder.Eval(Container.DataItem,"Id") %>','_blank', 'toolbar=yes,scrollbars=yes,resizable=yes,top=50,left=300,width=1050,height=750');return false;" >
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
    </asp:GridView>
</asp:Content>
