<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Listar.aspx.cs" Inherits="DzAnalyzer.View.Credito.Listar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Listar</title>
    <style type="text/css">
        .auto-style6 {
            width: 246px;
        }
        .auto-style7 {
            width: 245px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center><h1>Lista Créditos</h1></center>
    <hr />
    <div style="width: 1266px; height: 682px; margin-right: 36px;">
        <table>
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="Label1"  runat="server" Text="ID da Transação"></asp:Label><br />
                    <asp:TextBox ID="id_credito" MaxLength="8" onkeypress="return VerificaNumero(event)" CssClass="form-control" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btn_credito" CssClass="btn btn-primary" runat="server" Text="Listar por Transação" OnClick="btn_credito_Click" Width="172px" />
                </td>
                <td class="auto-style7">
                    <asp:Label ID="Label4"  runat="server" Text="Nome do Usuário"></asp:Label><br />
                    <asp:TextBox ID="nome" MaxLength="60" CssClass="form-control"  OnKeyPress ="return VerificaString(event)" runat="server"></asp:TextBox><br />
                    <asp:Button ID="btn_nome" CssClass="btn btn-primary" runat="server" Text="Listar por Nome" OnClick="btn_nome_Click" />
                </td>
                <td class="auto-style7">
                    <asp:Label ID="Label5"  runat="server" Text="ID do Usuário"></asp:Label>
                    <asp:TextBox ID="id_usuario" MaxLength="8" CssClass="form-control"  OnKeyPress ="return VerificaNumero(event)"  runat="server" Height="30px"></asp:TextBox><br />
                    <asp:Button ID="btn_tudo" CssClass="btn btn-primary" runat="server" Text="Listar por ID" Width="173px" Height="36px" OnClick="btn_tudo_Click" />
                </td>
                <td class="auto-style6">
                    <asp:Label ID="Label6" runat="server" Text="Listar por Data"></asp:Label><br />
                    <asp:DropDownList CssClass="form-control" ID="opcoesData" runat="server" Width="164px" Height="41px">
                        <asp:ListItem Value="0"> Selecione</asp:ListItem>
                        <asp:ListItem Value="7">7 dias</asp:ListItem>
                        <asp:ListItem Value="15">15 dias</asp:ListItem>
                        <asp:ListItem Value="30">1 mês</asp:ListItem>
                        <asp:ListItem Value="60">2 meses</asp:ListItem>
                        <asp:ListItem Value="90">3 meses</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Listar por Data" OnClick="Button3_Click" Height="36px" Width="172px" /><br />
                    <br />
                </td>
            </tr>
        </table>
        
        <asp:GridView ID="listagem" CssClass="table table-hover table-striped" Visible="false" GridLines="None"  runat="server" Height="119px" Width="1283px">
        </asp:GridView>
        <asp:GridView ID="lista_nome" CssClass="table table-hover table-striped" Visible="false" GridLines="None" runat="server" Width="1284px" OnSelectedIndexChanged="lista_nome_SelectedIndexChanged" OnRowCommand="lista_nome_command">
            <Columns>
                <asp:TemplateField HeaderText="Comando">
                    <ItemTemplate>
                        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnComandoGrid" Text="Selecionar" CommandName="Editar" CommandArgument='<%#Eval("id_usuario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>                
            </Columns>
        </asp:GridView>        
    </div>
    <!-- Inicio Modal Alerta -->
    <asp:Panel ID="ModalAlerta" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
        <asp:Panel ID="ModalBoxAlerta" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
            <div class="modal-box-conteudo">
                <div style="height: 60px">
                    <h1 class="lead" style="width: 100px; margin: 50px auto;">Atenção!!</h1>
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