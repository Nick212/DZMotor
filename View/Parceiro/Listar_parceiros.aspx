<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Listar_parceiros.aspx.cs" Inherits="DzAnalyzer.View.Parceiro.Listar_parceiros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .modal-box-alerta {
            width: 30%;
            height: 30%;
            position: absolute;
            background-color: #FFF;
            left: 50%;
            top: 50%;
            margin-left: -15%;
            margin-top: -15%;
            border-radius: 5px;
        }

        .espacotextbox {
            width: 240px;
        }

        .edicaoDeFontes {
            font-family: Verdana;
            color: blue;
            font-size: 16px;
        }

        .edicaoCxTexto {
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            font-family: Verdana;
            color: red;
        }

        .edicaoDeBotao {
            border-color: blue;
            background-color: blue;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            color: white;
        }
    </style>


    <h1>Nesta tela você pode consultar os nossos Parceiros</h1>
    <br />
    <br />
    <br />
    <br />

    <div style="width: 1000px; height: 1000px;">
        <div style="width: 720px; height: 300px;">
            <table>
                <tr>
                    <td class="espacotextbox">
                        <b>CNPJ</b><br />
                        <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="cnpj_text" runat="server"></asp:TextBox><br />
                        <br />
                        <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="CONSULTAR" OnClick=" btnConsulta_Click" />

                    </td>
                    <td class="espacotextbox">
                        <b>RAZÃO SOCIAL</b><br />
                        <asp:TextBox CssClass="form-control" onkeypress="return VerificaString(event);" ID="nome_razao_social" runat="server"></asp:TextBox><br />
                        <br />
                        <asp:Button CssClass="btn btn-primary" ID="Button2" runat="server" Text="CONSULTAR" OnClick="BotaoConsulta_Click" /><br />
                    </td>
                    <td class="espacotextbox">
                        <b>ID DO PARCEIRO</b><br />
                        <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="id_parceiro" runat="server"></asp:TextBox><br />
                        <br />
                        <asp:Button CssClass="btn btn-primary" ID="Button3" runat="server" Text="CONSULTAR" OnClick="ConsultaID_Click" />
                    </td>
                    <td>

                        <asp:Button CssClass="btn btn-primary" ID="btnApagaCampos" runat="server" Text="APAGAR CAMPOS" OnClick="btnApagaCampos_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="teste" style="width: 400px; height: 300px;">
            <!--<b>NOME FANTASIA</b><br />
            <asp:TextBox CssClass="form-control" ID="nome_fantasia_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>RAZÃO SOCIAL</b><br />
            <asp:TextBox CssClass="form-control" ID="razao_social_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>CNPJ</b><br />
            <asp:TextBox CssClass="form-control" ID="documento_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>DATA DE FUNDAÇÃO</b><br />
            <asp:TextBox CssClass="form-control" ID="data_funda_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>EMAIL</b><br />
            <asp:TextBox CssClass="form-control" ID="email_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>CEP</b><br />
            <asp:TextBox CssClass="form-control" ID="cep_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>ENDEREÇO</b><br />
            <asp:TextBox CssClass="form-control" ID="rua_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>NÚMERO</b><br />
            <asp:TextBox CssClass="form-control" ID="numero_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>ESTADO</b><br />
            <asp:TextBox CssClass="form-control" ID="estado_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>CIDADE</b><br />
            <asp:TextBox CssClass="form-control" ID="cidade_text" ReadOnly="true" runat="server"></asp:TextBox><br />

            <b>TELEFONE COMERCIAL</b><br />
            <asp:TextBox CssClass="form-control" ID="telefone_text" ReadOnly="true" runat="server"></asp:TextBox><br /><br/>
            
            <b>TIPO DE PARCEIRO</b><br/>
            <asp:TextBox CssClass="form-control" ID="tipo_parceiro_text" ReadOnly="true" runat="server"></asp:TextBox>
            <br/><br/><br/><br/>

            <asp:GridView ID="GridCampos" runat="server" Height="108px" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="True" Width="1000px" ></asp:GridView>
            <asp:GridView ID="GridCampos1" runat="server" Height="108px" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="True" Width="1000px" ></asp:GridView>-->

            <asp:GridView ID="ApareceGrid" runat="server" Height="358" Width="1000px" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="true"></asp:GridView>
        </div>
    </div>

</asp:Content>
