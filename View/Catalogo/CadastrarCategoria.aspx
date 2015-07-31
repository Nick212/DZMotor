<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="CadastrarCategoria.aspx.cs" Inherits="DzAnalyzer.View.Catalogo.EditarProduto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <link href="../CSS/Catalogo.css" rel="stylesheet" />

            <div style="width: 100%; height: 600px;">
                <div style="width: 100%; height: 100px; padding: 30px;">
                    <h1 style="width: 400px; margin: 0 auto;">Cadastrar Categoria</h1>
                    <hr />
                </div>
                <div style="width: 100%; height: 150px;">
                    <div style="width: 50%; height: 150px; float: left">
                        <asp:Panel runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                            <asp:Button CssClass="btn btn-primary" ID="btnCadastarCategoria" runat="server" Text="Cadastrar Categoria" Height="40px" Width="200px" OnClick="btnCadastarCategoria_Click" />
                        </asp:Panel>
                        <asp:Panel ID="PanelCategoria" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" Visible="false">
                            <p class="lead" style="width: 150px; margin: 0 auto;">Categoria:</p>
                            <br />
                            <asp:TextBox CssClass="form-control" ID="txtCategoria" runat="server" Width="230px" MaxLength="250" onkeypress="return VerificaString(event);" Style="margin: 0 auto;"></asp:TextBox>
                            <br />
                            <p class="lead" style="width: 150px; margin: 0 auto;">Sub Categoria:</p>
                            <br />
                            <asp:TextBox CssClass="form-control" ID="txtCategoriaSubCategoria" runat="server" MaxLength="250" onkeypress="return VerificaString(event);" Width="230px" Style="margin: 0 auto;"></asp:TextBox>
                        </asp:Panel>
                    </div>
                    <div style="width: 50%; height: 150px; float: right">
                        <asp:Panel runat="server" Width="100%" Height="100%" HorizontalAlign="Center">
                            <asp:Button CssClass="btn btn-primary" ID="btnCadastrarSubCategoria" runat="server" Text="Cadastrar Sub Categoria" Height="40px" Width="200px" OnClick="btnCadastrarSubCategoria_Click" />
                        </asp:Panel>
                        <asp:Panel ID="PanelSubCategoria" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" Visible="false">
                            <p class="lead" style="width: 150px; margin: 0 auto;">Categoria:</p>
                            <br />
                            <asp:DropDownList CssClass="form-control" ID="ddlCategoria" runat="server" Width="230px" Style="margin: 0 auto;" AutoPostBack="true"></asp:DropDownList>
                            <br />
                            <p class="lead" style="width: 150px; margin: 0 auto;">Sub Categoria:</p>
                            <br />
                            <asp:TextBox CssClass="form-control" ID="txtSubCategoriaSubCategoria" runat="server" Width="230px" MaxLength="250" onkeypress="return VerificaString(event);" Style="margin: 0 auto;"></asp:TextBox>
                        </asp:Panel>
                    </div>
                </div>
            </div>

            <div style="width: 100%; height: 50px;">
                <asp:Panel ID="PanelRodape" runat="server" Width="100%" Height="100%" HorizontalAlign="Center" Visible="false">
                    <asp:Button CssClass="btn btn-primary" ID="btnCadastrarCategoria" runat="server" Text="Cadastrar" Height="40px" Width="200px" OnClick="btnCadastrarCategoria_Click" />
                </asp:Panel>
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
