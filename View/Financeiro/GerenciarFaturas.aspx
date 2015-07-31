<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="GerenciarFaturas.aspx.cs" Inherits="DzAnalyzer.View.Financeiro.GerenciarFaturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <h1>Gerenciar Faturas</h1>
        <br />
        <br />
    </div>
    <asp:Label ID="Label3" runat="server" Text="Parceiro:"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlParceirosGerenciar" runat="server"></asp:DropDownList>
    <br />
    <br />
    <br />
    <div style="margin-top: 2px">
        <asp:Button ID="btnPesquisar" runat="server" CssClass="btn btn-primary" Text="Pesquisar" OnClick="btnPesquisar_Click" />
    </div>
    <br />
    <br />
    <asp:GridView ID="gvGerenciar" CssClass="table table-hover table-striped" Visible="false" GridLines="None"  runat="server" Height="119px" Width="1283px">
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DZPP14-1ConnectionString %>" SelectCommand="SELECT A.ID_FATURA, B.NOME_FANTASIA_PARCEIRO, A.DATA_ABERTURA, A.DATA_FECHAMENTO, A.DESCONTO, C.DESCRICAO, A.VALOR_FATURA FROM FATURA AS A WITH (NOLOCK) INNER JOIN PARCEIRO AS B WITH (NOLOCK) ON A.ID_PARCEIRO = B.ID_PARCEIRO INNER JOIN STATUS_FATURA AS C WITH (NOLOCK) ON A.ID_STATUS = C.ID_STATUS"></asp:SqlDataSource>
    
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
