<%@ Page Title="" Language="C#" MasterPageFile="~/View/MasterPageAnalyzer.Master" AutoEventWireup="true" CodeBehind="ConsultarChamado.aspx.cs" Inherits="DzAnalyzer.View.Atendimento.ConsultarChamado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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

    </style>


    <div style="text-align:center">

            <h2>Consultar Chamado</h2>
            <hr style="margin-left: 0px" />
    </div>

        <div id="ladoDireito" style="width: 300px; height:auto;">
            CPF / CNPJ<asp:TextBox ID="cpfUsuario" runat="server" AutoFocus="true" CssClass="form-control"></asp:TextBox>
        </div>
                
        <div id="ladoEsquerdo">
            Nº Chamado Cliente / Parceiro <asp:TextBox ID="nChamado" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div id="ladoBaixo">
            <asp:Button ID="btnConsultarChamado" runat="server" Text="Consultar Chamado Clientes" OnClick="btnConsultarChamado_Click" CssClass="btn btn-primary"/><br />
            <asp:Button ID="btnConsultarChamadoParceiro" runat="server" Text="Consultar Chamado Parceiros" OnClick="btnConsultarChamado_Click_Parceiro" CssClass="btn btn-primary"/>
        </div> 
          
        <div>
         <asp:GridView ID="GridView1" runat="server" Height="358px" Width="1389px" CssClass="table table-hover table-striped" GridLines="None" AllowPaging="True" ></asp:GridView>      
        </div>
    
</asp:Content>
