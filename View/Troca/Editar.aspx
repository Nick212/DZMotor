<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="Editar.aspx.cs" Inherits="DzAnalyzer.View.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Inicio Modal Alerta -->
                <asp:Panel ID="pModalAlerta" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
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
    <div id="dadosTroca_troca">
        <h1>Cadastrar/Editar Troca</h1>
        <div style="float: left; width: 50%">
            <asp:Label runat="server" ID="lblNumTroca" Text="Num.Troca" />
            <asp:TextBox   ID="txbNumTroca" runat="server" />
            <asp:Button   ID="btnPesquisarTroca" runat="server" Text="Pesquisar Troca" Width="150px" OnClick="btnPesquisarTroca_Click" />
            <asp:Button   ID="btnNovaTroca" runat="server" Text="Nova Troca" OnClick="btnNovaTroca_Click" />
        </div>
        <div style="float: left; width: 50%; text-align: center">
            <asp:Label runat="server" ID="lblDataTroca" Text="Data Troca" />
            <asp:TextBox   ID="txbDataTroca" runat="server" Width="200px" />
        </div>
    </div>
    <div id="dadosCliente_troca" style="vertical-align: middle">
        <asp:Label ID="lblCliente" runat="server" Visible="false" Font-Size="Larger" Text="Cliente" />
        <p>
            <asp:RadioButton ID="rbtnPessoaFisica" runat="server" Text="Pessoa Física" GroupName="rd" OnCheckedChanged="PessoaFisica_CheckedChanged1" AutoPostBack="True" Visible="false" />
            <asp:RadioButton ID="rbtnPessoaJuridica" runat="server" Text="Pessoa Jurídica" GroupName="rd" OnCheckedChanged="PessoaJuridica_CheckedChanged" AutoPostBack="True" Visible="false" />
            <br />
            <asp:Label ID="lblDocumentoFisico" runat="server" Text="CPF" Visible="false" />
            <asp:Label ID="lbldocumentoJuridico" runat="server" Text="CNPJ" Visible="false" />
            <asp:TextBox   ID="txbDocumento" runat="server" Visible="False" Width="250px" />
            <asp:Button   ID="btnPesquisarClienteTroca" runat="server" Text="Pesquisar Cliente" Visible="False" Width="150px" OnClick="btnPesquisarClienteTroca_Click" />
        </p>
        <div>
            <asp:Label runat="server" ID="lblNomeCliente" Text="Nome cliente" Visible="false" /><asp:Label ID="lblRazaoSocial" runat="server" Text="Razão Social" Visible="false" />
            <br />
            <asp:TextBox   ID="txbNomeClienteTroca" runat="server" Width="400px" Visible="false" /><asp:TextBox   ID="txbRazaoSocial" runat="server" Visible="false" Width="400px" />
            <br />
            <asp:Label ID="lblSaldoCliente" runat="server" Text="Saldo" Visible="false" />
            <br />
            <asp:TextBox   ID="txbSaldoClienteTroca" runat="server" Width="100px" Visible="false"/>
            <br />
            <asp:Label ID="lblEmailCliente" runat="server" Text="E-mail" Visible="false" />
            <br />
            <asp:TextBox   ID="txbEmailClienteTroca" runat="server" Width="399px" Visible="false" />
            <br />
            <asp:Label ID="lblEmailReserva" runat="server" Text="E-mail Reserva" Visible="false" />
            <br />
            <asp:TextBox   ID="txbEmail2ClienteTroca" runat="server" Width="400px" Visible="false" />
            <br />
        </div>
        <hr />
    </div>
    <div id="Produtos_troca">
        <asp:Label ID="lblProdutosDisponiveis" runat="server" Visible="false" Font-Size="Larger" Text="Produtos Disponíveis" />
        <br />
        <br />
        <asp:Label ID="lblProdutosIndisponiveis" text="Não há Produtos disponíveis para seu saldo atual!" runat="server" Visible="false" Width="400px" />
        <asp:GridView   ID="gvProdutosDisponiveis" runat="server" Visible="False" AutoGenerateColumns="false" ItemType="DzAnalyzer.Catalogo.Produto" OnRowCommand="gvProdutosDisponiveis_RowCommand">
            <Columns>
                <asp:BoundField DataField="idProduto" HeaderText="IdProduto" />
                <asp:BoundField DataField="nomeProduto" HeaderText="Nome" />
                <asp:BoundField DataField="descricaoProduto" HeaderText="Descrição" />
                <asp:BoundField DataField="marcaProduto" HeaderText="Marca" />
                <asp:BoundField DataField="dotzProduto" HeaderText="Valor Dotz" />
                <asp:ButtonField CommandName="SelecionaParceiro" Text="Selecionar" />
            </Columns>
            <SelectedRowStyle BackColor="LightGray"/>
        </asp:GridView>
        <asp:GridView   ID="gvEdicao" runat="server" Visible="false" AutoGenerateColumns="false" ItemType="DzAnalyzer.Catalogo.Produto" OnRowCommand="gvEdicao_rowCommand" >
            <Columns>
                <asp:BoundField DataField="idProduto" HeaderText="IdProduto" />
                <asp:BoundField DataField="nomeProduto" HeaderText="Nome" />
                <asp:BoundField DataField="descricaoProduto" HeaderText="Descrição" />
                <asp:BoundField DataField="marcaProduto" HeaderText="Marca" />
                <asp:BoundField DataField="dotzProduto" HeaderText="Valor Dotz" />
                <asp:ButtonField CommandName="EditarProduto" Text="Editar" />
            </Columns>
        </asp:GridView>
    </div>
    <hr />    
    <div id="dadosParceiro_troca">
        <asp:Label ID="lblParceiro" runat="server" Visible="false" Font-Size="Larger" Text="Fornecedor" />
        <div>
            <asp:Label ID="lblCNPJParceiro" runat="server" Text="CNPJ Fornecedor" Visible="false" />
            <br />
            <asp:TextBox   ID="txbCNPJParceiro" runat="server" Width="400px" Visible="false" />
            <br />
            <asp:Label ID="lblRazaoSocialParceiro" runat="server" Visible="false" Text="Razão Social" />
            <br />
            <asp:TextBox   ID="txbRazaoSocialParceiro" runat="server" Width="400px" Visible="false" />
            <br />
            <asp:Label ID="lblNomeFantasiaParceiro" runat="server" Visible="false" Text="Nome Fantasia" />
            <br />
            <asp:TextBox   ID="txbNomeFantasiaParceiro" runat="server" Visible="false" Width="400px" />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button   ID="btnDeletarTrocar" runat="server" Text="Deletar Troca" Visible="false" />
            <asp:Button   ID="btnAtualizarTroca" runat="server" Text="Atualizar Troca" Visible="false" OnClick="btnAtualizarTroca_Click" />
            <asp:Button   ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" />
            <asp:Button   ID="btnCadastrarTroca" runat="server" Text="Solicitar Troca" Visible="False" OnClick="btnCadastrarTroca_Click" />
            <asp:Literal ID="litResposta" runat="server" /> 
            <asp:Literal ID="litAtualiza" runat="server" />

        </div>
    </div>


</asp:Content>
