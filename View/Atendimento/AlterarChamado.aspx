<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="AlterarChamado.aspx.cs" Inherits="DzAnalyzer.View.Atendimento.AlterarChamado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src ="Scripts/BibliotecaJavaScript.js"></script>
        <style type="text/css">

        

       
      
        #ladoEsquerdo {
            height: 195px;
            width: 573px;
            float:left;
        }
        #ladoDireito {
            height: 193px;
            width: 571px;
            float:left;
        }

        #ladoBaixo{
            height:auto;
            width:auto;
            float:left;

        }

        #lad1{
            height:60px;
            width:348px;
            float:left;
        }
        #lad2{
            height:60px;
            width:342px;
            float:left;

        }
        #lad3{
            height:60px;
            width:359px;
            float:left;

        }
        #consult{
            background-position-x:center;
        }


    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <div style="text-align:center">

                    <h2>Editar Chamados Abertos</h2>
                    <hr style="margin-left: 0px" />
            </div>


                <div >
                        <div id="lad1">
                            <b>Nª Chamado Clientes:</b> <asp:TextBox ID="nChamado" runat="server" CssClass="form-control" onkeypress="return VerificaNumero(event);"></asp:TextBox>
                        </div>
            
                        <div id="lad2">
                            <b>CPF</b> <asp:TextBox ID="cpfUsuario" runat="server" CssClass="form-control" onkeypress="return VerificaNumero(event);"></asp:TextBox>
                        </div>
            
                        <div id="lad2">
                            <b>Nª Chamado Parceiro:</b> <asp:TextBox ID="nChamadoParceiro" runat="server" CssClass="form-control" onkeypress="return VerificaNumero(event);"></asp:TextBox>
                        </div>

                        <div id="lad3">
                            <b>CNPJ</b> <asp:TextBox ID="cnpjEmpresa" runat="server" CssClass="form-control" onkeypress="return VerificaNumero(event);"></asp:TextBox>
                        </div>
             
                

                    <br />
                        <div style="text-align:center" >
                    
                            <br /><br /><br />
                    
                            <asp:Button ID="btnVerificar" runat="server" Text="Consultar Chamado"  CssClass="btn btn-primary" Height="39px"  OnClick="btnVerificar_Click"/> <br />
                        </div>
            
                </div>
               <hr /> <br />
    
  
    
    
                <div id="ladoEsquerdo"> <!--style="visibility: hidden"-->

          
                     CPF / CNPJ<br />
                    <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" onkeypress="return VerificaNumero(event);"></asp:TextBox><br /><br />
           
                    Nome Cliente / Empresa:<br />
                    <asp:TextBox ID="nomeUsuario" runat="server" Width="500" CssClass="form-control" onkeypress="return VerificaString(event);" ReadOnly="true"></asp:TextBox> <br /><br />             
                </div>
        
     

        
                <div id="ladoDireito"> <!--style="visibility: hidden"-->
                                
                       Tipo de atendimento<br />
                    
                        
                            <asp:DropDownList ID="tipoAtendimento" runat="server"  CssClass="form-control" style="height:35px;"  AutoPostBack="True" ></asp:DropDownList>
                            
                        
                                <br /><br />
             
                        Data Nasc: / Data Fundação Emresa:<br />
                            <asp:TextBox ID="dataNasc" runat="server" Width="100px" CssClass="form-control" onkeypress="return VerificaNumero(event);" ReadOnly="true"></asp:TextBox> <br /><br/>

                </div>
  
        
                <div id="ladoBaixo"> <!--style="visibility: hidden"-->
                        Descrição do Atedimento: <br />
                            <asp:TextBox id="descAtendimento" TextMode="multiline" Columns="200" Rows="15" runat="server" CssClass="form-control"/><br /><br />
             
                        <div id="lad1">
                            Data Abertura Chamado
                            <br />
                            <asp:TextBox ID="dataAbertura" runat="server" Width="300" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        </div>
                
                        <div id="lad2">
                            Data Fechamento Chamado <br />
                            <asp:TextBox ID="dataFechamento" runat="server" Width="300" ReadOnly="true" CssClass="form-control"></asp:TextBox><br /><br />        
                        </div>
                
                        Status do Chamado <br />
                        <asp:DropDownList ID="statusChamado" runat="server"  CssClass="form-control" style="height:35px; width:200px;"  AutoPostBack="True" ></asp:DropDownList>
                    </div>
                <div id ="lad1">
                    <br />
                     <asp:Button ID="btnAlterar" runat="server" Text="Alterar Chamado"  CssClass="btn btn-primary" OnClick="btnAlterar_Click"/>
                                 <asp:Button ID="btnLimparDados" runat="server" Text="Limpar Dados" CssClass="btn btn-primary" OnClick="btnLimparDados_Click"/> 
                    <asp:TextBox ID="txtIdNChamado" runat="server" Visible="false"></asp:TextBox>
                    <br />           
        
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
