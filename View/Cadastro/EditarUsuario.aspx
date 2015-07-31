<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="DzAnalyzer.View.Cadastro.EditarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h1 style="font-family: Arial;">Nesta tela você pode consultar nossos Clientes</h1>

    <div style="width: 1500px; height: 1000px;">
        <div style="width: 600px; height: 400px; float: left;">
            <asp:Label ID="Label13" runat="server" Text="Label">CPF/CNPJ</asp:Label><br />
            <asp:TextBox ID="TextBox13" onkeypress="return VerificaNumero(event);" runat="server" Width="331px" CssClass="form-control"></asp:TextBox><br />
            <br />
            <br />
            <asp:Button Text="BUSCAR" onkeypress="return VerificaString(event);" runat="server" OnClientClick="return ValidaBusca1();" ID="btnBuscar1" Height="41px" OnClick="btnBuscar1_Click" Width="125px" CssClass="btn btn-primary" />
            <br />
        </div>
        <div style="width: 600px; height: 400px; float: left;">
            <asp:Label ID="Label14" runat="server" Text="Label">NOME</asp:Label><br />
            <asp:TextBox ID="TextBox14" onkeypress="return VerificaString(event);" runat="server" Width="385px" CssClass="form-control"></asp:TextBox><br />
            <br />
            <br />
            <asp:Button Text="BUSCAR" runat="server" OnClientClick="return ValidaBusca2();" ID="btnBuscar2" OnClick="btnBuscar2_Click" Height="41px" Width="125px" CssClass="btn btn-primary" />
        </div>

        <asp:GridView ID="GridView1" runat="server" Height="358px" Width="809px" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="True">
            <Columns>
                <asp:TemplateField HeaderText="Selecao">
                    <ItemTemplate>
                        <asp:Button CssClass="btn btn-primary" ID="btnEditar" Text="Editar" runat="server" Height="40px" CommandName="Editar" CommandArgument='<%#Eval("idUsuario") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <asp:Panel ID="ModalAlerta" runat="server" Style="visibility: hidden; width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;">
        <asp:Panel ID="ModalBoxAlerta" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
            <div class="modal-box-conteudo">
                <div style="height: 60px">
                    <h1 class="lead" style="width: 100px; margin: 50px auto;">Atenção!!</h1>
                </div>
                <asp:TextBox CssClass="form-control" ID="txtAlerta" runat="server" TextMode="MultiLine" Visible="true" Height="140px" Width="500px" Style="margin: 0 auto;"></asp:TextBox>
            </div>
            <asp:Panel ID="ModalFecharAlerta" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                <asp:Button CssClass="btn btn-danger" ID="btnFecharModalAlerta" Text="X" runat="server" OnClientClick="escondeModal()" />
            </asp:Panel>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
