using DzAnalyzer.Banco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DzAnalyzer.Banco.Atendimento;
using DzAnalyzer.Models.Atendimento;

namespace DzAnalyzer.View.Atendimento
{
    public partial class AtendimentoCliente : System.Web.UI.Page
    {

        //Declaração de Variável global;
        static AtendimentoCliente att = new AtendimentoCliente();
        Int64 abert;
         
        
        //Instancia do Banco- Atendimento para resgatar as informações do tipo atendimento não esquecendo de importar a classe no using
        private Atendimento_Operacoes atendTipoAtendimento = new Atendimento_Operacoes();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var tipo = atendTipoAtendimento.ListarTipo();
                //tipoAtendimento.DataValueField = "idTipoAtendimento";
                tipoAtendimento.DataTextField = "tipoAtendimento";
                tipoAtendimento.DataSource = tipo;
                tipoAtendimento.DataBind();

                NChamadoCliente_Parceiro number = new NChamadoCliente_Parceiro();
                number = atendTipoAtendimento.NChamadoCliente();
                //numberChamado.DataSource = nCliente;

                numberChamado.Text = number.id_n_chamadoCliente.ToString();

            }

            //Controle de Hora instantanea
            dataAbertura.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        

        protected void btnConsultaCPF_Click(object sender, EventArgs e)
        {
            
             //Atendimento_Operacoes atendCliente = new Atendimento_Operacoes();
                      
            if (txtCPF.Text != "" && txtCPF.Text.Length == 11)
            {
                ConexaoBD conn = new ConexaoBD();

                conn.openConnection();

                var cmd = new SqlCommand("select ID_USUARIO,NOME_USUARIO,DATA_USUARIO  from cadastro where documento ='" + txtCPF.Text + "'", conn.getConnection());
                var rd = cmd.ExecuteReader();

                DateTime data;
                Int32 idUs;

                //var idUsuario = 0;
                if (rd.Read())
                {
                    nomeUsuario.Text = rd.GetString(1);
                    data = rd.GetDateTime(2);
                    dataNasc.Text = data.ToString("dd/MM/yyyy");
                    idUs = rd.GetInt32(0);
                    idUsuario.Text = idUs.ToString();
                    //dataAbertura.Text= 
                    //idUsuarioAtend  = (int)rd[0];
                   
                }

               conn.closeConnection();
            }
            else
            {
                txtAlerta.Text = "Usuário não Encontrado !!!";
                ModalAlerta.Visible = true;
            }
            
        }
        
        
        
   
        protected void btnAtendimentoCliente_Click(object sender, EventArgs e)
        {

            var cpf = txtCPF.Text; 
            var nome = nomeUsuario.Text;
            var tipoAtend = tipoAtendimento.Text;
            var descAtend = descAtendimento.Text;
            var dataAbertCham = dataAbertura.Text;
            var idUser = idUsuario.Text;

            if ( cpf.Length == 11 && nome != ("") && tipoAtend != ("--Selecione o Tipo--") && descAtend != (""))
            {
                try
                {
                    ConexaoBD conn = new ConexaoBD();
                    conn.openConnection();
                    
                    var status = 1;

                    var cmd1 = new SqlCommand("INSERT INTO CHAMADO_ATENDIMENTO_CLIENTE (ID_STATUS,ID_USUARIO,CPF,TIPO_ATENDIMENTO,DESCRICAO_CHAMADO,DATA_ABERTURA_CHAMADO) VALUES(" + status + "," + idUser + ",'" + cpf + "','" + tipoAtend + "','" + descAtend + "','" + dataAbertCham + "')", conn.getConnection());
                    cmd1.ExecuteNonQuery();

                    conn.closeConnection();

                }
                catch (Exception oi)
                {
                    txtAlerta.Text = "Erro " + oi;
                    ModalAlerta.Visible = true;
                }
                finally
                {
                    txtCPF.Text = "";
                    tipoAtendimento.Text = "--Selecione o Tipo--";
                    nomeUsuario.Text = "";
                    dataNasc.Text = "";
                    descAtendimento.Text = "";

                    txtAlerta.Text = "Chamado Cadastrado com Sucesso !!!";
                    ModalAlerta.Visible = true;
                }
            }
            else
            {
                txtAlerta.Text = "Erro ao Cadastrar o Chamado, Verifique os Dados do Usuário!!!";
                ModalAlerta.Visible = true;
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnLimparDados_Click(object sender, EventArgs e)
        {
            txtCPF.Text = "";
            tipoAtendimento.Text = "--Selecione o Tipo--";
            nomeUsuario.Text = "";
            dataNasc.Text = "";
            descAtendimento.Text = "";
            
        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }
    }
}