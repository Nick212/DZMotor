﻿<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="CriarFaturas.aspx.cs" Inherits="DzAnalyzer.View.Financeiro.CriarFaturas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h1>Criar Fatura</h1>
        <br />
        <br />
    </div>
    <div style="width: 49%; height: 150px; float: left">
        <asp:Label ID="Label5" runat="server" Text="Selecione para qual parceiro deseja criar a fatura:"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblParceiro" runat="server" Text="Parceiro:"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlParceiros" runat="server"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblDesconto" runat="server" Text="Desconto:"></asp:Label>
        <br />
        <asp:TextBox ID="txbDesconto" onkeypress="return VerificaNumero(event);" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btnCriarFatura" runat="server" CssClass="btn btn-primary" Text="Criar Fatura" OnClick="btnCriarFatura_Click" />
        <br />
        <br />
        <br />
        <asp:GridView ID="gvFatura" CssClass="table table-hover table-striped" Visible="false" GridLines="None" runat="server" Height="119px" Width="1283px">
        </asp:GridView>
        <br />
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
