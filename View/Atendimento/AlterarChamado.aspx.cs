using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DzAnalyzer.Banco;
using System.Data.SqlClient;
using DzAnalyzer.Banco.Atendimento;


namespace DzAnalyzer.View.Atendimento
{
    public partial class AlterarChamado : System.Web.UI.Page
    {
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

                var status = atendTipoAtendimento.ListarStatus();
                statusChamado.DataValueField = "id_status";
                statusChamado.DataTextField = "descricao";
                statusChamado.DataSource = status;
                statusChamado.DataBind();
            }

            //Carrega atributos do Tipo de Atendimento 
            //tipoAtendimento.DataSource = new string[] { "--Selecione o Tipo--", "Elogios", "Reclamação de Troca", "Reclamação de Compra", "Reclamação de Atendimento", "Problemas com a Fatura", "Problema com Dotz" };
            //tipoAtendimento.DataBind();
        }



        protected void btnLimparDados_Click(object sender, EventArgs e)
        {
            nChamado.Text = "";
            cpfUsuario.Text = "";
            nChamadoParceiro.Text = "";
            cnpjEmpresa.Text = "";
            txtCPF.Text = "";
            tipoAtendimento.Text = "--Selecione o Tipo--";
            nomeUsuario.Text = "";
            dataNasc.Text = "";
            descAtendimento.Text = "";
            dataAbertura.Text = "";
            dataFechamento.Text = "";
            
        }

        protected void btnAlterar_Click(object sender, EventArgs e)
        {
            
            if (nChamadoParceiro.Text != "" || cnpjEmpresa.Text != "")
            {
                try
                {
                    ConexaoBD conn = new ConexaoBD();

                    conn.openConnection();
                    //não funciona
                    var cmd = new SqlCommand(@"UPDATE CHAMADO_ATENDIMENTO_PARCEIRO SET CNPJ = '" + txtCPF.Text + "', TIPO_ATENDIMENTO = '" + tipoAtendimento.Text + "', DESCRICAO_CHAMADO ='" + descAtendimento.Text + "',DATA_ABERTURA_CHAMADO = '" + dataAbertura.Text + "', DATA_FECHAMENTO_CHAMADO = GETDATE(), ID_STATUS =" + statusChamado.SelectedValue + " WHERE ID_N_CHAMADO = " + txtIdNChamado.Text, conn.getConnection());

                    cmd.ExecuteNonQuery();

                    conn.closeConnection();

                }
                catch (Exception ee)
                {
                    txtAlerta.Text = "Erro " + ee;
                    ModalAlerta.Visible = true;
                }
                finally
                {
                    nChamado.Text = "";
                    cpfUsuario.Text = "";
                    nChamadoParceiro.Text = "";
                    cnpjEmpresa.Text = "";
                    txtCPF.Text = "";
                    tipoAtendimento.Text = "--Selecione o Tipo--";
                    nomeUsuario.Text = "";
                    dataNasc.Text = "";
                    descAtendimento.Text = "";
                    dataAbertura.Text = "";
                    dataFechamento.Text = "";

                    txtAlerta.Text = "Informações Alteradas com Sucesso!!!";
                    ModalAlerta.Visible = true;
                }

            }
            else if (nChamado.Text != "" || txtCPF.Text != "") 
            {
                try
                {
                    ConexaoBD conn = new ConexaoBD();

                    conn.openConnection();

                    //Se o chmado estiver Aberto Faça
                    if (statusChamado.Text.Equals("Aberto"))
                    {
                        var cmd = new SqlCommand(@"UPDATE CHAMADO_ATENDIMENTO_CLIENTE SET CPF = '" + txtCPF.Text + "', TIPO_ATENDIMENTO = '" + tipoAtendimento.Text + "', DESCRICAO_CHAMADO ='" + descAtendimento.Text + "',DATA_ABERTURA_CHAMADO = '" + dataAbertura.Text + "', ID_STATUS =" + statusChamado.SelectedValue + " WHERE ID_N_CHAMADO = " + txtIdNChamado.Text, conn.getConnection());


                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        //Se o Chamado Estiver Fechado Faça
                        var cmd = new SqlCommand(@"UPDATE CHAMADO_ATENDIMENTO_CLIENTE SET CPF = '" + txtCPF.Text + "', TIPO_ATENDIMENTO = '" + tipoAtendimento.Text + "', DESCRICAO_CHAMADO ='" + descAtendimento.Text + "',DATA_ABERTURA_CHAMADO = '" + dataAbertura.Text + "', DATA_FECHAMENTO_CHAMADO = GETDATE(), ID_STATUS = " + statusChamado.SelectedValue + " WHERE ID_N_CHAMADO = " + txtIdNChamado.Text, conn.getConnection());

                        cmd.ExecuteNonQuery();
                    }
                    conn.closeConnection();

                }
                catch (Exception ee)
                {
                    txtAlerta.Text = "Erro " + ee;
                    ModalAlerta.Visible = true;
                }
                finally
                {
                    nChamado.Text = "";
                    cpfUsuario.Text = "";
                    nChamadoParceiro.Text = "";
                    cnpjEmpresa.Text = "";
                    txtCPF.Text = "";
                    tipoAtendimento.Text = "--Selecione o Tipo--";
                    nomeUsuario.Text = "";
                    dataNasc.Text = "";
                    descAtendimento.Text = "";
                    dataAbertura.Text = "";
                    dataFechamento.Text = "";

                    txtAlerta.Text = "Dados Alterados com Sucesso!!!";
                    ModalAlerta.Visible = true;
                   
                }
            }

            else
            {
                txtAlerta.Text = "Dados não atualizados, Por Favor verifique os Dados se estão corretos!!!";
                ModalAlerta.Visible = true;
            }
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            

            if (cpfUsuario.Text != "" || nChamado.Text != "")
            {

              
                    
                    Int32 nchamado = 0;
                    //Caso o nchamado for nulo ele vai zerar para não ocorrer erro no UPDATE do BD
                    if (nChamado.Text == "") { nchamado = 0; }

                    else {
                        nchamado = Convert.ToInt32(nChamado.Text);
                    }
                    

                       

                        var cmd = new SqlCommand("SELECT CPF, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO, DATA_FECHAMENTO_CHAMADO, ID_STATUS,C.NOME_USUARIO, C.DATA_USUARIO, ID_N_CHAMADO FROM CHAMADO_ATENDIMENTO_CLIENTE INNER JOIN CADASTRO C ON CPF = C.DOCUMENTO WHERE CPF='" + cpfUsuario.Text + "' OR ID_N_CHAMADO = " + nchamado, conn.getConnection());
                        var rd = cmd.ExecuteReader();

                        DateTime data;
                        DateTime data2;
                        DateTime dataNascimento;
                        Int32 status = 0;

                        if (rd.Read())
                        {
                            txtIdNChamado.Text = rd.GetInt32(8).ToString();
;                           txtCPF.Text = rd.GetString(0);
                            nomeUsuario.Text = rd.GetString(6);
                            tipoAtendimento.Text = rd.GetString(1);
                            descAtendimento.Text = rd.GetString(2);
                            dataNascimento = rd.GetDateTime(7);
                            dataNasc.Text = dataNascimento.ToString("dd/MM/yyyy");
                            
                            data = rd.GetDateTime(3);
                            dataAbertura.Text = data.ToString("yyyy/MM/dd HH:mm:ss");


                            status = rd.GetInt32(5);


                            if (status == 1)
                            {
                                statusChamado.SelectedValue = status.ToString();
                                dataFechamento.Text = "";
                            }
                            else if (status == 2)
                            {
                                statusChamado.SelectedValue  = status.ToString() ;
                                dataFechamento.Text = "";
                            }
                            else if (status == 3)
                            {
                                statusChamado.SelectedValue = status.ToString();
                                data2 = rd.GetDateTime(4);
                                dataFechamento.Text = data2.ToString();
                            }

                        }

                       
                    conn.closeConnection();
                }
            else if (cnpjEmpresa.Text != "" || nChamadoParceiro.Text != "")
            {

                Int32 nchamado = 0;
                //Caso o nchamado for nulo ele vai zerar para não ocorrer erro no UPDATE do BD
                if (nChamadoParceiro.Text == "") { nchamado = 0; }

                else
                {
                    nchamado = Convert.ToInt32(nChamadoParceiro.Text);
                }



                
                var cmd = new SqlCommand("SELECT CNPJ, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO, DATA_FECHAMENTO_CHAMADO, ID_STATUS, C.NOME_USUARIO, C.DATA_USUARIO,ID_N_CHAMADO FROM CHAMADO_ATENDIMENTO_PARCEIRO INNER JOIN CADASTRO C ON CNPJ = C.DOCUMENTO WHERE CNPJ='" + cnpjEmpresa.Text + "' OR ID_N_CHAMADO = " + nchamado, conn.getConnection());
                var rd = cmd.ExecuteReader();

                DateTime data;
                DateTime data2;
                DateTime dataN;
                Int32 status=0;

                if (rd.Read())
                {
                    txtIdNChamado.Text = rd.GetInt32(8).ToString();
                    txtCPF.Text = rd.GetString(0);
                    nomeUsuario.Text = rd.GetString(6);
                    tipoAtendimento.Text = rd.GetString(1);
                    descAtendimento.Text = rd.GetString(2);
                    data = rd.GetDateTime(3);
                    dataAbertura.Text = data.ToString();
                    status = rd.GetInt32(5);
                    dataN = rd.GetDateTime(7);
                    dataNasc.Text = dataN.ToString();



                    if (status == 1)
                    {
                        statusChamado.SelectedValue = status.ToString();
                        dataFechamento.Text = "";
                    }
                    else if (status == 2)
                    {
                        statusChamado.SelectedValue = status.ToString();
                        dataFechamento.Text = "";
                    }
                    else if (status == 3)
                    {
                        statusChamado.SelectedValue = status.ToString();
                        data2 = rd.GetDateTime(4);
                        dataFechamento.Text = data2.ToString();
                    }

                }


                conn.closeConnection();
            }

            else
            {
                txtAlerta.Text = "Usuário não Encontrado !!!";
                ModalAlerta.Visible = true;
            }


        }

        protected void btnFecharModalAlerta_Click(object sender, EventArgs e)
        {
            ModalAlerta.Visible = false;
        }

        
    }
}