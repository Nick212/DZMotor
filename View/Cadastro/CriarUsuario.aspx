<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="CriarUsuario.aspx.cs" Inherits="DzAnalyzer.View.Cadastro.CriarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1 style="font-family: Arial; height: 204px;">Cadastro de Novos Usuários</h1>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div style="width: 1200px; height: 517px;">

                <div style="width: 583px; height: 538px; float: left;">
                    <asp:Label ID="Label1" runat="server" Text="Label">NOME: </asp:Label><br />
                    <asp:TextBox ID="TextBox1" onkeypress="return VerificaString(event);" runat="server" CssClass="form-control" TabIndex="1" Width="323px"></asp:TextBox><br />

                    <asp:Label ID="Label13" runat="server" Text="Label">1 - PF/PJ:</asp:Label><br />
                    <asp:DropDownList ID="ddl_tipo" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList><br />

                    <asp:Label ID="Label2" runat="server" Text="Label">CPF/CNPJ:</asp:Label><br />
                    <asp:TextBox ID="TextBox2" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="3" Width="324px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label3" runat="server" Text="Label">DATA NASCIMENTO/INICIO:</asp:Label><br />
                    <asp:TextBox ID="TextBox3" onkeypress="return VerificaString(event);" type="date" runat="server" TabIndex="5" Width="177px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label4" runat="server" Text="Label">EMAIL:</asp:Label><br />
                    <asp:TextBox ID="TextBox4" runat="server" TabIndex="7" Width="318px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label5" runat="server" Text="Label">EMAIL RESERVA:</asp:Label><br />
                    <asp:TextBox ID="TextBox5" runat="server" TabIndex="7" Width="317px" CssClass="form-control"></asp:TextBox><br />


                    <asp:Label ID="Label6" runat="server" Text="Label">ESTADO:</asp:Label><br />
                    <asp:DropDownList ID="ddl_est" runat="server" OnSelectedIndexChanged="ddl_est_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control"></asp:DropDownList><br />

                </div>
                <div style="width: 600px; height: 540px; float: right;">
                    <asp:Label ID="Label7" runat="server" Text="Label">CIDADE:</asp:Label><br />
                    <asp:DropDownList ID="ddl_cid" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList><br />

                    <asp:Label ID="Label8" runat="server" Text="Label">CEP:</asp:Label><br />
                    <asp:TextBox ID="TextBox8" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="2" Width="356px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label9" runat="server" Text="Label">RUA:</asp:Label><br />
                    <asp:TextBox ID="TextBox9" runat="server" TabIndex="4" Width="356px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label10" runat="server" Text="Label">NUMERO</asp:Label><br />
                    <asp:TextBox ID="TextBox10" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="6" Width="58px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label11" runat="server" Text="Label">DDD:</asp:Label><br />
                    <asp:TextBox ID="TextBox11" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="7" Width="57px" CssClass="form-control"></asp:TextBox><br />

                    <asp:Label ID="Label12" runat="server" Text="Label">TELEFONE/CELULAR:</asp:Label><br />
                    <asp:TextBox ID="TextBox12" onkeypress="return VerificaNumero(event);" runat="server" TabIndex="7" Width="349px" CssClass="form-control"></asp:TextBox><br />

                </div>
                <div style="height: 51px">
                    <%--<button id="btn1" onclick="ValidaCampos();" runat="server">ENVIAR</button>--%>
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
                    <asp:Panel ID="ModalSucesso" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
                        <asp:Panel ID="Panel2" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
                            <div class="modal-box-conteudo">
                                <div style="height: 60px">
                                    <h1 class="lead" style="width: 100px; margin: 50px auto;">Parabens!!</h1>
                                </div>
                                <asp:TextBox CssClass="form-control" ID="sucesso" runat="server" TextMode="MultiLine" Visible="true" Height="140px" Width="500px" Style="margin: 0 auto;"></asp:TextBox>
                            </div>
                            <asp:Panel ID="Panel3" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                                <asp:Button CssClass="btn btn-danger" ID="btnFecharModalAlerta2" Text="X" runat="server" OnClick="btnFecharModalAlerta2_Click" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Panel ID="erroDocumento" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
                        <asp:Panel ID="Panel4" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
                            <div class="modal-box-conteudo">
                                <div style="height: 60px">
                                    <h1 class="lead" style="width: 100px; margin: 50px auto;">Parabens!!</h1>
                                </div>
                                <asp:TextBox CssClass="form-control" ID="erroBox" runat="server" TextMode="MultiLine" Visible="true" Height="140px" Width="500px" Style="margin: 0 auto;"></asp:TextBox>
                            </div>
                            <asp:Panel ID="Panel5" runat="server" Style="position: absolute; right: 3px; cursor: pointer; top: 3px;">
                                <asp:Button CssClass="btn btn-danger" ID="btnFecharModalAlerta3" Text="X" runat="server" OnClick="btnFecharModalAlerta3_Click" />
                            </asp:Panel>
                        </asp:Panel>
                    </asp:Panel>
                    <asp:Button Text="ENVIAR" runat="server" OnClientClick="return ValidaCampos();" ID="btnEnviar" OnClick="btnEnviar_Click" Width="125px" Height="41px" CssClass="btn btn-primary" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
