<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Editar_parceiros.aspx.cs" Inherits="DzAnalyzer.View.Parceiro.Editar_parceiros" %>

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

        .editandoFontes {
            font-family: Verdana;
            color: blue;
            font-size: 16px;
        }

        .editandoCxTexto {
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

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <h1 style="font-family: Arial; color: blue">Edite seus dados cadastrais aqui</h1>

            <div style="width: 1200px; height: 900px;">

                <h3 style="font-family: Arial; color: blue">Digite o CNPJ para buscar o cadastro !!</h3>

                <b>CNPJ</b><br />
                <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="cnpj_text" runat="server" TabIndex="1"></asp:TextBox><br />
                <br />

                <asp:Button CssClass="btn btn-primary" ID="Button1" runat="server" Text="CONSULTAR" OnClick="BotaoConsultaEdicao_Click" TabIndex="2" />
                <asp:Button CssClass="btn btn-primary" ID="Button3" runat="server" Text="APAGAR CAMPOS" OnClick="btnApagaCampos_Click" TabIndex="2" />
                


                <br />
                <br />
                <div style="width: 400px; height: 300px; float: left;">

                    <b>NOME FANTASIA</b><br />
                    <asp:TextBox CssClass="form-control" ID="nome_fantasia" runat="server" TabIndex="3" ReadOnly="true"></asp:TextBox><br />

                    <b>EMAIL</b><br />
                    <asp:TextBox CssClass="form-control" ID="email" runat="server" TabIndex="4" ReadOnly="true"></asp:TextBox><br />

                    <b>TELEFONE COMERCIAL</b><br />
                    <asp:TextBox CssClass="form-control" ID="telefone" runat="server" TabIndex="5" ReadOnly="true"></asp:TextBox><br />

                    <b>CEP</b><br />
                    <asp:TextBox CssClass="form-control" ID="cep" runat="server" TabIndex="6" ReadOnly="true"></asp:TextBox><br />

                    <b>ENDEREÇO</b><br />
                    <asp:TextBox CssClass="form-control" ID="rua" runat="server" TabIndex="7" ReadOnly="true"></asp:TextBox><br />

                    <b>NÚMERO</b><br />
                    <asp:TextBox CssClass="form-control" ID="numero" runat="server" TabIndex="8" ReadOnly="true"></asp:TextBox><br />

                    <b>ESTADO</b><br />
                    <asp:TextBox CssClass="form-control" ID="estado_text" runat="server" TabIndex="9" ReadOnly="true"></asp:TextBox><br />

                    <b>CIDADE</b><br />
                    <asp:TextBox CssClass="form-control" ID="cidade_text" runat="server" TabIndex="10" ReadOnly="true"></asp:TextBox><br />

                </div>




                <div style="width: 700px; height: 400px; float: right;">
                    
                    <b>NOME FANTASIA</b><br />
                    <asp:TextBox CssClass="form-control" ID="nome_fantasia_text" runat="server" TabIndex="11"></asp:TextBox>

                    <b>EMAIL</b><br />
                    <asp:TextBox CssClass="form-control" ID="email_text" runat="server" TabIndex="12"></asp:TextBox>

                    <b>DDD + TELEFONE COMERCIAL</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" size="2" ID="ddd_text" runat="server" TabIndex="13"></asp:TextBox>
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="telefone_text" runat="server" TabIndex="14"></asp:TextBox><br />

                    <b>CEP</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="cep_text" runat="server" TabIndex="15"></asp:TextBox><br />

                    <b>ENDEREÇO</b><br />
                    <asp:TextBox CssClass="form-control"  onkeypress="return VerificaString(event);" ID="endereco_text" runat="server" TabIndex="16"></asp:TextBox><br />

                    <b>NÚMERO</b><br />
                    <asp:TextBox CssClass="form-control" onkeypress="return VerificaNumero(event);" ID="numero_text" runat="server" TabIndex="17"></asp:TextBox><br />

                    <b>ESTADO</b><br />
                    <asp:DropDownList CssClass="form-control" ID="ddl_est" runat="server" OnSelectedIndexChanged="ddl_est_SelectedIndexChanged" AutoPostBack="True" TabIndex="18"></asp:DropDownList><br />

                    <b>CIDADE</b><br />
                    <asp:DropDownList CssClass="form-control" ID="ddl_cid" runat="server" AutoPostBack="True" TabIndex="19"></asp:DropDownList><br />

                    <asp:Button CssClass="btn btn-primary" ID="Button2" runat="server" Text="ALTERAR" OnClick="btnAlteraCadastro_Click" />

                </div>

                    <!--Inicio da Modal -->
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
                    <!-- fim da modal -->

                </div>







            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
