<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="AtendimentoCliente.aspx.cs" Inherits="DzAnalyzer.View.Atendimento.AtendimentoCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="View/Scripts/BibliotecaJavaScript.js"></script>
    <style type="text/css">

        

       .inputText{
            width:400px;
            height:25px;
        }

       .botaoConsultar{
            margin: 5px 5px 5px 8px;
	        border-radius: 10px;
            height:25px;
       }

        #ladoEsquerdo {
            height: 128px;
            width: 573px;
            float:left;
        }
        #ladoDireito {
            height: 102px;
            width: 571px;
            float:left;
        }

        #ladoBaixo{
            margin: 10px;
            height:473px;
            width:auto;
            float:left;

        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <div style="text-align:center">

                    <h2>Atendimento ao Cliente</h2>
                    <hr />
            </div>


                <div style="height: 36px; width: 1210px">
                    <b><i>Nª Chamado:</i></b> <asp:Label ID="numberChamado" runat="server" Text="000000"></asp:Label><br />       
                </div>
    
    
                <div id="ladoEsquerdo">
                     CPF<br />
                    <asp:TextBox ID="txtCPF" runat="server" onkeypress="return VerificaNumero(event);" AutoFocus="true" CssClass="form-control" Width="497px" ></asp:TextBox>
            
                    <asp:Button ID="btnConsultaCPF" runat="server" Text="Consultar"  CssClass="btn btn-primary" OnClientClick="ValidaCPFAtendimento();" OnClick="btnConsultaCPF_Click"/><br /><br />
            
                    Nome:<br />
                    <asp:TextBox ID="nomeUsuario" runat="server" Width="500" ReadOnly="true" CssClass="form-control"></asp:TextBox> <br /><br />            
                </div>
        
     

        
                <div id="ladoDireito">
            
                     
                        Tipo de atendimento<br />
                   
                            

                            <asp:DropDownList ID="tipoAtendimento" runat="server" CssClass="form-control"  AutoPostBack="True" >
                            </asp:DropDownList>
                        
                                <br /><br /><br />
             
                        Data Nasc: <br />
                            <asp:TextBox ID="dataNasc" runat="server" Width="100px" ReadOnly="true" CssClass="form-control" ></asp:TextBox> <br /><br/>

                </div>

        
                <div id="ladoBaixo">
                        <br /><br />
                        Descrição do Atedimento: <br />
                            <asp:TextBox id="descAtendimento" TextMode="multiline" Columns="200" Rows="15" runat="server" CssClass="form-control"/><br />
            

                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                        
                                Data Abertura Chamado
                        <br />
                            <asp:TextBox ID="dataAbertura" runat="server" Width="300" ReadOnly="true" CssClass="form-control" ></asp:TextBox> <br />
                   
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <br />
             

                
               
            
            
                <!--<button   id="btnAtendimentoCliente" onclick="AtendimentoClienteAbrirChamado();" class="botaoConsultar">Abrir Chamado</button>-->
                    <asp:Button ID="btnAtendimentoCliente" runat="server" CssClass="btn btn-primary" Text="Abrir Chamado" OnClientClick="ValidaAtendimentoCliente();" OnClick="btnAtendimentoCliente_Click"  />
                    <asp:Button ID="btnLimpar" runat="server" CssClass="btn btn-primary" Text="Limpar Dados" OnClick="btnLimparDados_Click" />
          
                        <asp:TextBox ID="idUsuario" runat="server" OnTextChanged="TextBox1_TextChanged" Visible="false"></asp:TextBox>

                </div>

        <!-- Inicio Modal Alerta -->
        <asp:Panel ID="ModalAlerta" runat="server" Style="width: 100%; height: 100%; position: fixed; background-color: rgba(0,0,0,0.5); left: 0; top: 0;" Visible="false">
            <asp:Panel ID="ModalBoxAlerta" runat="server" Style="width: 30%; height: 30%; position: absolute; background-color: #FFF; left: 50%; top: 50%; margin-left: -15%; margin-top: -15%; border-radius: 5px;">
                <div class="modal-box-conteudo">
                    <div style="height: 60px">
                        <h1 class="lead" style="width: 100px; margin: 50px auto;">Atenção:</h1>
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



