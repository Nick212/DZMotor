<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="EditarFaturas.aspx.cs" Inherits="DzAnalyzer.View.Financeiro.EditarFaturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Editar Faturas</h1>
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="Label9" runat="server" Text="Fatura ID:"></asp:Label>
        <br />
        <asp:TextBox ID="faturaID" runat="server" onkeypress="return VerificaNumero(event);"></asp:TextBox><br />
        <br />
        <asp:Button ID="btnBuscar" CssClass="btn btn-primary" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
    </div>
    <br />
    <div>
        <!-- <button id="btnPesquisar" onclick="ValidaFinanceiro();">Pesquisar</button>
        <button id="btnSalvar" onclick="ValidaFinanceiro();">Salvar</button>
        <button id="btnExcluir" onclick="ValidaFinanceiro();">Excluir</button> -->

    </div>
    <br />
    <div>
    <asp:GridView ID="gvEditar" runat="server" AutoGenerateColumns="False" Visible="False" DataKeyNames="ID_FATURA" DataSourceID="SqlDataSource1" Width="1053px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Height="73px" OnSelectedIndexChanged="gvEditar_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID_FATURA" HeaderText="ID_FATURA" InsertVisible="False" ReadOnly="True" SortExpression="ID_FATURA" />
            <asp:BoundField DataField="NOME_FANTASIA_PARCEIRO" HeaderText="NOME_FANTASIA_PARCEIRO" SortExpression="NOME_FANTASIA_PARCEIRO" />
            <asp:BoundField DataField="DATA_ABERTURA" HeaderText="DATA_ABERTURA" SortExpression="DATA_ABERTURA" />
            <asp:BoundField DataField="DATA_FECHAMENTO" HeaderText="DATA_FECHAMENTO" SortExpression="DATA_FECHAMENTO" />
            <asp:BoundField DataField="DESCONTO" HeaderText="DESCONTO" SortExpression="DESCONTO" />
            <asp:BoundField DataField="DESCRICAO" HeaderText="DESCRICAO" SortExpression="DESCRICAO" />
            <asp:BoundField DataField="VALOR_FATURA" HeaderText="VALOR_FATURA" SortExpression="VALOR_FATURA" />
            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="Excluir" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
        <RowStyle ForeColor="#000066" />
        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#007DBB" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#00547E" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DZPP14-1ConnectionString %>" SelectCommand="SELECT A.ID_FATURA, B.NOME_FANTASIA_PARCEIRO, A.DATA_ABERTURA, A.DATA_FECHAMENTO, A.DESCONTO, C.DESCRICAO, A.VALOR_FATURA FROM FATURA AS A WITH (NOLOCK) INNER JOIN PARCEIRO AS B WITH (NOLOCK) ON A.ID_PARCEIRO = B.ID_PARCEIRO INNER JOIN STATUS_FATURA AS C WITH (NOLOCK) ON A.ID_STATUS = C.ID_STATUS"></asp:SqlDataSource>
    <br />
    <asp:Button ID="btnSalvar" Visible="false" CssClass="btn btn-primary" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </div>
</asp:Content>
