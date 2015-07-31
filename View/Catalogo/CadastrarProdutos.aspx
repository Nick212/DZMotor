<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="CadastrarProdutos.aspx.cs" Inherits="DzAnalyzer.View.Catalogo.CadastrarProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%; height: 1024px;">
                <div style="width: 100%; height: 100px; padding: 30px;">
                    <h1 style="width: 300px; margin: 0 auto;">Cadastra Produto</h1>
                    <hr />
                </div>
                <div style="width: 100%; height: 650px;">
                    <div style="width: 50%; height: 650px; float: left;">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <p class="lead" style="width: 150px; margin: 0 auto;">Fornecedor:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlFornecedor" runat="server" Width="230px" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Categoria:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlCategoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" Width="230px" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">SubCategoria:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlSubCategoria" runat="server" Width="230px" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Status:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server" Width="230px" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Nome: </p>
                        <br />
                        <asp:TextBox CssClass="form-control" ID="txtNome" runat="server" Width="230px" MaxLength="250" Style="margin: 0 auto;"></asp:TextBox>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Marca: </p>
                        <br />
                        <asp:TextBox CssClass="form-control" ID="txtMarca" runat="server" Width="230px" MaxLength="250" Style="margin: 0 auto;"></asp:TextBox>
                    </div>
                    <div style="width: 50%; height: 650px; float: right;">
                        <p class="lead" style="width: 150px; margin: 0 auto;">Valor em R$: </p>
                        <br />
                        <div style="width: 240px; height: 50px; margin: 0 auto">
                            <asp:TextBox CssClass="form-control" ID="txtValor" runat="server" Width="190px" onkeypress="return VerificaNumero(event);" MaxLength="6" Style="margin: 0 auto; float: left"></asp:TextBox>
                            <asp:TextBox CssClass="form-control" ID="txtCentavos" runat="server" Width="50px" onkeypress="return VerificaNumero(event);" MaxLength="2" Style="margin: 0 auto; float: right;"></asp:TextBox>
                        </div>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Valor em DZ: </p>
                        <br />
                        <div style="width: 240px; height: 50px; margin: 0 auto">
                            <asp:TextBox CssClass="form-control" ID="txtdz" ReadOnly ="true" runat="server" Width="150px" MaxLength="8" Style="margin: 0 auto; float: left"></asp:TextBox>
                            <asp:Button CssClass="btn btn-primary" ID="CalcularDz" runat="server" Text="Calcular" OnClick="CalcularDz_Click" Width="90px" Style="float: right" />
                        </div>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Quantidade: </p>
                        <br />
                        <asp:TextBox CssClass="form-control" ID="txtQuantidade" ReadOnly ="false" runat="server" Width="230px" onkeypress="return VerificaNumero(event);" MaxLength="3" Style="margin: 0 auto;"></asp:TextBox>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Descrição:</p>
                        <br />
                        <asp:TextBox CssClass="form-control" ID="txtDesc" runat="server" Height="100px" MaxLength="250" Rows="10" TextMode="MultiLine" Width="250px" Style="margin: 0 auto;"></asp:TextBox>
                        <br />
                    </div>
                </div>
                <div style="width: 100%; height: 50px;">
                    <asp:Panel ID="PanelRodape" runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                        <asp:Button CssClass="btn btn-primary" ID="btnCadastarProduto" runat="server" Text="Cadastrar Produto" OnClick="btnCadastarProduto_Click" Height="40px" Width="200px" />
                    </asp:Panel>
                </div>
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


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
