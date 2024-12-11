<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Arvore.aspx.cs" Inherits="FCarnauba_Animais.Arvore" AspCompat="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .labelendogamia {
            background-color: #052B5C;
            color: white;
    }
    
    .labelarvore {
        font-weight: normal;
        vertical-align: top;
        text-align: left;
        padding: 2px;
    }
    .gridtitulo {
        height: 25px;
        background-color: #194069;
        padding-top: 5px;
        color: #FFFFFF;
        font-weight: bold;
        text-align:center;
    }
    
    .gridlinha01 {
        height: 25px;
        background-color: #FFFFFF;
        padding-top: 5px;
    }
    
    .gridlinha02 {
        height: 25px;
        background-color: #dedede;
        padding-top: 5px;
    }
    
    .valor {
       text-align:right;
    }
    
    .tabela {
       align-content: center;
       width:100%;
    }
    
    #chart_div{
            
            transform:rotate(-90deg);
        }
    
    .myNodeClass 
    {
        text-align: center;
        vertical-align: middle;
        font-family: arial,helvetica;
        cursor: default;
        border: 2px solid #b5d9ea;
        -moz-border-radius: 5px;
        -webkit-border-radius: 5px;
        -webkit-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
        -moz-box-shadow: rgba(0, 0, 0, 0.5) 3px 3px 3px;
        background-color: #b0d7ee;
        
        writing-mode: vertical-rl;
    }
    </style>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">

        google.charts.load('current', { packages: ["orgchart"] });
        google.charts.setOnLoadCallback(drawChart);

   </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <table>
    <tr>
            <td class="rotulo">
                Nível:
            </td>
            <td>
                <div class="inputDiv"><asp:DropDownList ID="ddlNivel" runat="server" Height="18px" Width="40" onfocus="Javascript:HighlightForm(this,false);" onblur="Javascript:HighlightForm(this,true);" AutoPostBack="true">
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem Selected>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                </asp:DropDownList></div>
            </td>
        </tr>
        </table>

        <div align="left">
    <table>
        <tr class="gridtitulo">
            <td colspan="2" class="gridtitulo">ÁRVORE GENEALÓGICA</td>
        </tr>
        <tr class="gridlinha01">
            <td>ANIMAL</td>
            <td><%=animal.NomeCompleto%></td>
        </tr>
        <tr class="gridlinha02">
            <td>ENDOGAMIA</td>
            <td class="valor"><b><%=Endogamia.ToString("N12")%></b></td>
        </tr>
    </table>
    </div>
    
    <div id="chart_div">
        

    </div>
    
    </form>
</body>
</html>
