<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="ListarProdutos.aspx.cs" Inherits="DzAnalyzer.View.Catalogo.ListarProdutos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%; height: 1024px;">
                <div style="width: 100%; height: 100px; padding: 30px;">
                    <h1 style="width: 270px; margin: 0 auto;">Listar Produtos</h1>
                    <hr />
                </div>
                <div style="width: 100%; height: 350px;">
                    <div style="width: 50%; height: 350px; float: left">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                        <p class="lead" style="width: 150px; margin: 0 auto;">Fornecedor:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlFornecedor" runat="server" Width="230px" Style="margin: 0 auto;"></asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">Status:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlStatus" runat="server" Width="230px" Style="margin: 0 auto;"></asp:DropDownList>
                        <br />
                    </div>
                    <div style="width: 50%; height: 350px; float: right">
                        <p class="lead" style="width: 150px; margin: 0 auto;">Categoria:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlCategoria" runat="server" AutoPostBack="True" Width="230px" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />
                        <p class="lead" style="width: 150px; margin: 0 auto;">SubCategoria:</p>
                        <br />
                        <asp:DropDownList CssClass="form-control" ID="ddlSubCategoria" runat="server" AutoPostBack="True" Width="230px" Style="margin: 0 auto;">
                        </asp:DropDownList>
                        <br />

                    </div>
                </div>
                <div style="width: 100%; height: 50px;">
                    <asp:Panel ID="PanelBotao" runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                        <asp:Button CssClass="btn btn-primary" ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" Height="40px" Width="200px" />
                    </asp:Panel>
                </div>
                <div style="width: 100%; height: 350px;">
                    <asp:GridView ID="gvProdutos" runat="server" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="True" OnPageIndexChanging="gvProdutos_PageIndexChanging" OnRowCommand="gvProdutos_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Selecao">
                                <ItemTemplate>
                                    <asp:Button CssClass="btn btn-primary" ID="btnEditar" Text="Editar" runat="server" Height="40px" CommandName="Editar" CommandArgument='<%#Eval("idProduto") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Inicio Modal Grid -->
                <asp:Panel ID="Modal" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
                    <div>
                        <asp:Panel ID="ModalBox" runat="server" Style="width: 30%; height: 80%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -20%; border-radius: 5px;">
                            <div class="modal-box-conteudo">
                                <asp:Panel ID="PanelCabecalho" runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                                    <br />
                                    <asp:Label CssClass="lead" runat="server" Text="Nome: "></asp:Label>
                                    <asp:Label CssClass="lead" ID="LabelNome" runat="server"></asp:Label>
                                    <br />
                                    <asp:Label CssClass="lead" runat="server" Text="ID: "></asp:Label>
                                    <asp:Label CssClass="lead" ID="LabelID" runat="server"></asp:Label>
                                </asp:Panel>
                                <hr />
                                <p class="lead" style="width: 150px; margin: 0 auto;">Marca: </p>
                                <br />
                                <asp:TextBox CssClass="form-control" ID="txtMarca" runat="server" Width="230px" MaxLength="250" Style="margin: 0 auto;"></asp:TextBox>
                                <p class="lead" style="width: 150px; margin: 0 auto;">Status:</p>
                                <br />
                                <asp:DropDownList CssClass="form-control" ID="ddlStatusModal" runat="server" Width="230px" Style="margin: 0 auto;">
                                </asp:DropDownList>
                                <p class="lead" style="width: 150px; margin: 0 auto;">Valor em R$: </p>
                                <br />
                                <div style="width: 240px; height: 45px; margin: 0 auto">
                                    <asp:TextBox CssClass="form-control" ID="txtValor" runat="server" Width="190px" onkeypress="return VerificaNumero(event);" MaxLength="6" Style="margin: 0 auto; float: left"></asp:TextBox>
                                    <asp:TextBox CssClass="form-control" ID="txtCentavos" runat="server" Width="50px" onkeypress="return VerificaNumero(event);" MaxLength="2" Style="margin: 0 auto; float: right;"></asp:TextBox>
                                </div>
                                <p class="lead" style="width: 150px; margin: 0 auto;">Valor em DZ: </p>
                                <br />
                                <div style="width: 240px; height: 50px; margin: 0 auto">
                                    <asp:TextBox CssClass="form-control" ID="txtDz" ReadOnly="true" runat="server" Width="150px" MaxLength="8" Style="margin: 0 auto; float: left"></asp:TextBox>
                                    <asp:Button CssClass="btn btn-primary" ID="CalcularDz" runat="server" Text="Calcular" OnClick="CalcularDz_Click" Width="90px" Style="float: right" />
                                </div>
                                <p class="lead" style="width: 150px; margin: 0 auto;">Quantidade: </p>
                                <br />
                                <asp:TextBox CssClass="form-control" ID="txtQuantidade" runat="server" Width="230px" onkeypress="return VerificaNumero(event);" MaxLength="3" Style="margin: 0 auto;"></asp:TextBox>
                                <p class="lead" style="width: 150px; margin: 0 auto;">Descrição:</p>
                                <br />
                                <asp:TextBox CssClass="form-control" ID="txtDesc" runat="server" Height="100px" MaxLength="250" Rows="10" TextMode="MultiLine" Width="250px" Style="margin: 0 auto;"></asp:TextBox>
                                <hr />
                                <asp:Panel ID="PanelSalvar" runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                                    <asp:Button CssClass="btn btn-primary" ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                                </asp:Panel>
                            </div>
                            <asp:Panel ID="ModalBodFechar" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                                <asp:Button CssClass="btn btn-danger" ID="btn_fechar" Text="X" runat="server" OnClick="btn_fechar_Click" />
                            </asp:Panel>
                        </asp:Panel>
                </asp:Panel>
                <!-- Final Modal Grid -->

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
