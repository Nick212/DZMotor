<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="ListaTrocas.aspx.cs" Inherits="DzAnalyzer.View.WebForm2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Consulta de Trocas</h1>
    <br />
    <asp:Label ID="lblDocumentoListaDeTrocas" runat="server" Visible="true" Text="CPF/CNPJ" />
    <asp:TextBox ID="txbDocumentoListaDeTrocas" runat="server" Visible="true" />
    <br />
    <asp:Button ID="btnPesquisarListaDeTrocas" runat="server" Visible="true" Text="Pesquisar" OnClick="btnPesquisarListaDeTrocas_Click" />
    <br />
    <asp:GridView ID="gvListaDeTrocas" runat="server" Visible="true" AutoGenerateColumns="false" ItemType="DzAnalyzer.Models.Troca.ListaDeTrocas">
        <Columns>
            <asp:BoundField DataField="id_troca" HeaderText="Id Troca" />
            <asp:BoundField DataField="nomeUsuario" HeaderText="Nome Cliente" />
            <asp:BoundField DataField="nomeFantasia" HeaderText="Fornecedor" />
            <asp:BoundField DataField="nomeProduto" HeaderText="Nome do Produto" />
            <asp:BoundField DataField="Descricao" HeaderText="Descrição do Produto" />
            <asp:BoundField DataField="data" HeaderText="Data da Troca" />
        </Columns>
    </asp:GridView>
</asp:Content>
