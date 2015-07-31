<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Criar.aspx.cs" Inherits="DzAnalyzer.View.Credito.Criar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Criação de Créditos</title>
    <style type="text/css">
        .auto-style1 {
            width: 240px;
        }

        .auto-style2 {
            width: 215px;
        }

        .auto-style3 {
            height: 120px;
        }

        .auto-style4 {
            height: 112px;
        }
    </style>
    <script src="../Scripts/jquery-2.1.1.min.js"></script>
    <script src="../Scripts/jquery.price_format.2.0.min.js"></script>
    <script>
        $('#ContentPlaceHolder1_valor_compra').priceFormat({
            prefix: 'R$ ',
            centsSeparator: '.',
            thousandsSeparator: ','
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center><h1>Insere Créditos</h1></center>
    <hr />
    <div style="width: 70%; height: 136px">
        <div style="width: 125%; height: 150px">
            <table>
                <tr>
                    <td class="auto-style1">
                        <br />
                        <label for="id_usuario">ID do Usuário</label><br />
                        <asp:TextBox ID="btn_id_usuario" CssClass="form-control" runat="server" MaxLength="8" onkeypress="return VerificaNumero(event);" Width="200px"></asp:TextBox><br />
                        <br />
                        <asp:Button ID="btn_id" CssClass="btn btn-primary" runat="server" Text="Buscar ID" OnClick="btn_id_Click" Height="41px"></asp:Button>
                    </td>
                    <td class="auto-style2">
                        <br />
                        <label for="documento">Documento Usuário</label><br />
                        <asp:TextBox ID="btn_documento" CssClass="form-control" MaxLength="14" onkeypress="return VerificaNumero(event);" runat="server" Width="200px"></asp:TextBox><br />
                        <br />
                        <asp:Button ID="btn_doc" CssClass="btn btn-primary" Class="dinheiro" runat="server" Text="Buscar Documento" OnClick="btn_doc_Click" Height="38px"></asp:Button>
                    </td>
                    <td class="auto-style1">
                        <br />
                        <label for="nome">Nome do Usuário</label><br />
                        <asp:TextBox ID="nome" CssClass="form-control" runat="server" MaxLength="60" onkeypress="return VerificaString(event);" Width="200px"></asp:TextBox><br />
                        <br />
                        <asp:Button ID="btn_nome" CssClass="btn btn-primary" runat="server" Text="Buscar por Nome" OnClick="btn_nome_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <br />
    <br />
    <br />
    <br />
    <div style="width: 35%; height: 100px; margin-top: 0px;" id="formulario" visible="false" enableviewstate="true" runat="server">
        <div style="width: 50%; height: 121px; float: left">
            <table>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label7" runat="server" Text="Nome do Usuário"></asp:Label><br />
                        <asp:TextBox ID="nome_usuario" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="ID do Usuário"></asp:Label><br />
                        <asp:TextBox ID="id_usuario" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Parceiro"></asp:Label><br />
                        <asp:DropDownList ID="lista_parceiro" AutoPostBack="true" CssClass="form-control" runat="server" Width="146px" OnSelectedIndexChanged="lista_parceiro_SelectedIndexChanged"></asp:DropDownList><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Data Compra"></asp:Label><br />
                        <asp:TextBox ID="data_compra" CssClass="form-control" runat="server" MaxLength="10" onkeypress="TabulaData(event,'ContentPlaceHolder1_data_compra')"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Status"></asp:Label><br />
                        <asp:DropDownList ID="lista_status" CssClass="form-control" runat="server" Width="146px">
                        </asp:DropDownList><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Número da Nota Fiscal"></asp:Label><br />
                        <asp:TextBox ID="nota_fiscal" MaxLength="8" CssClass="form-control" runat="server" onkeypress="return VerificaNumero(event);"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 50%; height: 121px; float: right">
            <table>
                <tr>
                    <td class="auto-style4">
                        <asp:Label ID="Label9" runat="server" Text="Documento"></asp:Label><br />
                        <asp:TextBox ID="documento" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Data da Credito"></asp:Label><br />
                        <asp:TextBox ID="data_credito" CssClass="form-control" runat="server" ReadOnly="true" MaxLength="10" onkeydown="TabulaData(event,'ContentPlaceHolder1_data_credito');"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Mecânica"></asp:Label><br />
                        <asp:DropDownList ID="lista_mecanica" CssClass="form-control" runat="server" Width="146px">
                        </asp:DropDownList><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Motivo da criação"></asp:Label><br />
                        <asp:TextBox ID="motivo" MaxLength="100" CssClass="form-control" runat="server" onkeypress="return VerificaString(event);"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <form>
                        <asp:Label ID="Label10" runat="server" Text="Valor da Compra (R$)"></asp:Label><br />
                        <asp:TextBox ID="valor_compra" MaxLengh="8" CssClass="form-control" runat="server" OnKeyPress=""></asp:TextBox><br />
                        <br />
                        </form>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:Button ID="btn_credito" runat="server" Text="Creditar" CssClass="btn btn-primary" OnClick="btn_credito_Click" />
        </div>
    </div>
    <br />
    <br />
    <asp:GridView ID="selecionaNomes" CssClass="table table-hover table-striped" Visible="False" GridLines="None" runat="server" Width="1264px" OnRowCommand="selecionaNomes_command" Height="99px">
        <Columns>
            <asp:TemplateField HeaderText=" ">
                <ItemTemplate>
                    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnComandoGrid" Text="Selecionar" CommandName="Editar" CommandArgument='<%#Eval("id_usuario") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <!-- Inicio Modal Alerta -->
    <asp:Panel ID="ModalAlerta" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
        <asp:Panel ID="ModalBoxAlerta" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
            <div class="modal-box-conteudo">
                <div style="height: 60px">
                    <h1 class="lead" style="width: 100px; margin: 50px auto;">Atenção:</h1>
                </div>
                <asp:TextBox CssClass="form-control" ID="txtAlerta" runat="server" TextMode="MultiLine" Visible="true" Height="140px" Width="500px" Style="margin: 0 auto;"></asp:TextBox>
            </div>
            <asp:Panel ID="ModalFecharAlerta" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                <asp:Button CssClass="btn btn-danger" ID="btnFecharModalAlerta" Text="X" runat="server" OnClick="btnFecharModalAlerta_Click" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
    <!-- Final Modal Alerta -->
</asp:Content>
