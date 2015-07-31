<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="DzAnalyzer.View.Credito.Editar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Editar Créditos</title>
    <style type="text/css">
        .auto-style1 {
            width: 247px;
        }

        .auto-style2 {
            width: 205px;
        }
        .auto-style4 {
            width: 239px;
        }
        .auto-style5 {
            width: 238px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center><h1>Edita Créditos</h1></center>
    <hr />
    <div>        
        <table>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label1" runat="server" Text="ID da Transação"></asp:Label><br />
                    <asp:TextBox ID="id_credito" CssClass="form-control" MaxLength="8" OnKeyPress="return VerificaNumero(event)" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btn_credito" CssClass="btn btn-primary" runat="server" Text="Editar por Crédtio" OnClick="btn_credito_Click" />
                </td>
                <td class="auto-style5">
                    <asp:Label ID="Label2" runat="server" Text="Nome"></asp:Label><br />
                    <asp:TextBox ID="nome" CssClass="form-control" MaxLength="60" OnKeyPress="return VerificaString(event)" runat="server" Width="164px"></asp:TextBox><br />
                    <asp:Button ID="btn_nome" CssClass="btn btn-primary" runat="server" Text="Editar por Nome" OnClick="btn_nome_Click" />
                </td>
                <td class="auto-style5">
                    <asp:Label ID="Label3" runat="server" Text="Documento"></asp:Label><br />
                    <asp:TextBox ID="documento" OnKeyPress="return VerificaNumero(event)" MaxLength="14" MaxLenght="14" CssClass="form-control" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btn_documento" CssClass="btn btn-primary" runat="server" Text="Editar por Documento" OnClick="btn_documento_Click" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div id="alterar" Visible="false" runat="server">
        <table>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label4" runat="server" Text="Valor da Compra (R$)"></asp:Label><br />
                    <asp:TextBox ID="valor_compra" MaxLength="8" OnKeyPress="return ValidaMoney(event)" CssClass="form-control" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label5" runat="server" Text="Parceiro"></asp:Label>
                    <asp:TextBox ID="parceiro" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label6" runat="server" Text="Mecânica"></asp:Label>
                    <asp:DropDownList ID="lista_mecanica" CssClass="form-control" runat="server"></asp:DropDownList><br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label7" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="lista_status" CssClass="form-control" runat="server"></asp:DropDownList><br />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="Label8" runat="server" Text="Data de Atualização do Crédito"></asp:Label>
                    <asp:TextBox ID="data_credito" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox><br />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Button ID="btn_editar" CssClass="btn btn-primary" runat="server" Text="Editar" OnClick="btn_editar_Click" Width="82px" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView ID="lista_credito" CssClass="table table-hover table-striped" Visible="false" GridLines="None" runat="server" Width="1284px" OnRowCommand="lista_credito_command">
        <Columns>
            <asp:TemplateField HeaderText="Comando">
                <ItemTemplate>
                    <asp:Button runat="server" CssClass="btn btn-primary" ID="btnComandoGrid" Text="Selecionar" CommandName="Editar" CommandArgument='<%#Eval("id_credito") %>' />
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
    <asp:HiddenField runat="server" ID="hf_credito" />
    <asp:HiddenField runat="server" ID="hf_mecanica" />
    <asp:HiddenField runat="server" ID="hf_valor" />
    <asp:HiddenField runat="server" ID="hf_status" />
</asp:Content>
