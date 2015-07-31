<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Cadastro_parceiro.aspx.cs" Inherits="DzAnalyzer.View.Parceiro.Cadastro_parceiro" %>

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

        .form-control {
            font-family: Verdana;
            color: blue;
            font-size: 16px;
            color: black;
        }

        .form-control {
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            font-family: Verdana;
            
        }

        .edicaoDeBotao {
            border-color: blue;
            background-color: blue;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            color: white;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <h1 style="font-family: Arial; color: blue">Cadastro de Parceiros</h1>
            <br />
            <br />
            <br />
            <br />

            <div style="width: 800px; height: 700px;">
                <div style="width: 400px; height: 400px; float: left;">
                    <b>RAZÃO SOCIAL</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaString(event);" ID="nome" runat="server" TabIndex="1"></asp:TextBox><br />

                    <b>CNPJ</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="cnpj" runat="server" TabIndex="3"></asp:TextBox><br />

                    <b>CEP</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="cep" runat="server" TabIndex="5"></asp:TextBox><br />

                    <b>ENDEREÇO</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaString(event);" ID="rua" runat="server" TabIndex="6"></asp:TextBox><br />

                    <b>NÚMERO</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="numero" runat="server" TabIndex="7"></asp:TextBox><br />

                    <b>ESTADO</b><br />
                    <asp:DropDownList CssClass="form-control" ID="ddl_est" runat="server" OnSelectedIndexChanged="ddl_est_SelectedIndexChanged" AutoPostBack="True" TabIndex="8"></asp:DropDownList><br />

                    <b>CIDADE</b><br />
                    <asp:DropDownList CssClass="form-control" ID="ddl_cid" runat="server" AutoPostBack="True" TabIndex="9"></asp:DropDownList><br />


                </div>
                <div style="width: 400px; height: 300px; float: right;">
                    <b>NOME FANTASIA</b><br />
                    <asp:TextBox CssClass="form-control" ID="nome_fantasia" runat="server" TabIndex="2"></asp:TextBox><br />

                    <b>DATA DE FUNDAÇÃO</b><br />
                    <asp:TextBox CssClass="form-control" ID="data" type="date" runat="server" TabIndex="4"></asp:TextBox><br />

                    <b>EMAIL</b><br />
                    <asp:TextBox CssClass="form-control" ID="email" runat="server" TabIndex="10"></asp:TextBox><br />

                    <b>EMAIL RESERVA</b><br />
                    <asp:TextBox CssClass="form-control" ID="emailReserva" runat="server" TabIndex="11"></asp:TextBox><br />

                    <b>DDD + TELEFONE COMERCIAL</b><br />
                    <asp:TextBox CssClass="form-control" ID="ddd1" onkeypress="return VerificaNumero(event);" size="1" runat="server" TabIndex="12"></asp:TextBox>
                    <asp:TextBox CssClass="form-control" ID="telefone" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="13"></asp:TextBox><br />

                    <b>TIPO DE PARCEIRO</b>
                    <asp:DropDownList CssClass="form-control" ID="TipoDeParceiro" runat="server" AutoPostBack="true" TabIndex="14"></asp:DropDownList>

                    <br />
                    <asp:Button CssClass="btn btn-primary" ID="btnCadastrarParceiro" OnClientClick="ValidarCampos();" runat="server" Text="ENVIAR" OnClick="btnInsereDados_Click" TabIndex="16" />
                    <asp:Button CssClass="btn btn-primary" ID="Button2" runat="server" Text="APAGAR CAMPOS" OnClick="btnApagaCampos_Click" />
                </div>
                <!-- Começo da Modal -->
                <asp:Panel ID="ModalAlerta" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
                    <asp:Panel CssClass="modal-box-alerta" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
                        <div class="modal-box-conteudo">
                            <div style="height: 60px">
                                <h1 class="lead" style="width: 100px; margin: 50px auto;">ATENÇÃO !!!!</h1>
                            </div>
                            <asp:TextBox CssClass="form-control" ID="txtAlerta" runat="server" TextMode="MultiLine" Height="140px" Width="500px" Style="margin: 0 auto;"></asp:TextBox>
                        </div>
                        <asp:Panel ID="ModalFecharAlerta" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                            <asp:Button CssClass="btn btn-danger" ID="btnFecharModalAlerta" Text="X" runat="server" OnClick="btnFecharModalAlerta_Click" />
                        </asp:Panel>
                    </asp:Panel>

                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
