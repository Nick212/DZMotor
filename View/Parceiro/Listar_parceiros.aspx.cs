using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DzAnalyzer.Banco;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Banco.CadastroBD;

namespace DzAnalyzer.View.Parceiro
{
    public partial class Listar_parceiros : System.Web.UI.Page
    {
        /*private CadastroOperacoes cadastroUF = new CadastroOperacoes();
        List<ListaDeDados> parceiros = new List<ListaDeDados>();*/

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        
        protected void DefineCampos()
        {
            GridCampos.HeaderRow.Cells[0].Text = "RAZÃO SOCIAL";
            GridCampos.HeaderRow.Cells[1].Text = "CNPJ";
            GridCampos.HeaderRow.Cells[2].Text = "DATA DE FUNDAÇÃO";
            GridCampos.HeaderRow.Cells[3].Text = "EMAIL";
            GridCampos.HeaderRow.Cells[4].Text = "DDD";
            GridCampos.HeaderRow.Cells[5].Text = "TELEFONE";
            GridCampos.HeaderRow.Cells[6].Text = "NOME FANTASIA";
            GridCampos.HeaderRow.Cells[7].Text = "CEP";
            GridCampos.HeaderRow.Cells[8].Text = "ENDEREÇO";
            GridCampos.HeaderRow.Cells[9].Text = "NUMERO";
            GridCampos.HeaderRow.Cells[10].Text = "CIDADE";
            GridCampos.HeaderRow.Cells[11].Text = "ESTADO";
            GridCampos.HeaderRow.Cells[12].Text = "TIPO DE PARCEIRO";
        }

        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            
            var cnpj = cnpj_text.Text;
            
            

            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();




            var cmd = new SqlCommand(@" SELECT C.NOME_USUARIO, C.DATA_USUARIO, C.DOCUMENTO, C.EMAIL, CO.DDD, CO.TELEFONE, P.NOME_FANTASIA_PARCEIRO,
                                         E.RUA, E.NUMERO, E.CEP,  CI.NOME_CIDADE, ES.NOME_ESTADO,T.DESCRICAO
                                        FROM CADASTRO C LEFT JOIN CONTATO CO
                                        ON C.ID_USUARIO = CO.ID_USUARIO 
                                        INNER JOIN PARCEIRO P 
                                        ON C.ID_USUARIO = P.ID_USUARIO
                                        INNER JOIN TIPO_PARCEIRO T
                                        ON P.TIPO_PARCEIRO = T.ID_TIPO_PARCEIRO
                                        INNER JOIN ENDERECO E 
                                        ON C.ID_USUARIO = E.ID_USUARIO
                                        INNER JOIN CIDADE CI 
                                        ON E.ID_CIDADE = CI.ID_CIDADE 
                                        INNER JOIN ESTADO ES 
                                        ON E.ID_ESTADO = ES.ID_ESTADO
                                        WHERE DOCUMENTO = '" + cnpj + "'", conexao.getConnection());
                var rd = cmd.ExecuteReader();
                DateTime data;
                //Int32 ddd;

            IList<ListaDeDados> lista = new List<ListaDeDados>();
                while (rd.Read())
                {

                    var consulta = new ListaDeDados();
                    
                    consulta.nomeUsuario = rd.GetString(0);
                    consulta.documentoUsuario = rd.GetString(2);
                    consulta.dataUsuario = rd.GetDateTime(1);
                    consulta.emailUsuario = rd.GetString(3);
                    consulta.dddUsuario = rd.GetString(4);
                    consulta.telefoneUsuario = rd.GetString(5);
                    consulta.nomeFantasiaParceiro = rd.GetString(6);
                    consulta.cepUsuario = rd.GetString(9);
                    consulta.enderecoUsuario = rd.GetString(7);
                    consulta.numeroUsuario = rd.GetString(8);
                    consulta.nomeCidadeUsuario = rd.GetString(10);
                    consulta.nomeEstadoUsuario = rd.GetString(11);
                    consulta.descricaoTipoParceiro = rd.GetString(12);
                   
                    lista.Add(consulta);


                }
                conexao.closeConnection();
                GridCampos.DataSource = lista;
                GridCampos.DataBind();
                DefineCampos();
                
            
        }

        protected void BotaoConsulta_Click(object sender, EventArgs e)
        {
            var rSocial = nome_razao_social.Text;
            
            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();

            var cmd = new SqlCommand(@"SELECT C.NOME_USUARIO, C.DATA_USUARIO, C.DOCUMENTO, C.EMAIL, CO.DDD, CO.TELEFONE, P.NOME_FANTASIA_PARCEIRO,
                                         E.RUA, E.NUMERO, E.CEP,  CI.NOME_CIDADE, ES.NOME_ESTADO,T.DESCRICAO
                                        FROM CADASTRO C LEFT JOIN CONTATO CO
                                        ON C.ID_USUARIO = CO.ID_USUARIO 
                                        INNER JOIN PARCEIRO P 
                                        ON C.ID_USUARIO = P.ID_USUARIO
                                        INNER JOIN TIPO_PARCEIRO T
                                        ON P.TIPO_PARCEIRO = T.ID_TIPO_PARCEIRO
                                        INNER JOIN ENDERECO E 
                                        ON C.ID_USUARIO = E.ID_USUARIO
                                        INNER JOIN CIDADE CI 
                                        ON E.ID_CIDADE = CI.ID_CIDADE 
                                        INNER JOIN ESTADO ES 
                                        ON E.ID_ESTADO = ES.ID_ESTADO
                                        WHERE NOME_USUARIO = '" + rSocial + "'", conexao.getConnection());
            var rd = cmd.ExecuteReader();
            DateTime data;

            IList<ListaDeDados> lista = new List<ListaDeDados>();
            while (rd.Read())
            {
                var consulta = new ListaDeDados();

                consulta.nomeUsuario = rd.GetString(0);
                consulta.documentoUsuario = rd.GetString(2);
                consulta.dataUsuario = rd.GetDateTime(1);
                consulta.emailUsuario = rd.GetString(3);
                consulta.dddUsuario = rd.GetString(4);
                consulta.telefoneUsuario = rd.GetString(5);
                consulta.nomeFantasiaParceiro = rd.GetString(6);
                consulta.cepUsuario = rd.GetString(9);
                consulta.enderecoUsuario = rd.GetString(7);
                consulta.numeroUsuario = rd.GetString(8);
                consulta.nomeCidadeUsuario = rd.GetString(10);
                consulta.nomeEstadoUsuario = rd.GetString(11);
                consulta.descricaoTipoParceiro = rd.GetString(12);

                lista.Add(consulta);

            }

            conexao.closeConnection();
            GridCampos.DataSource = lista;
            GridCampos.DataBind();
            /*DefineCampos1();*/
        }

        protected void ConsultaID_Click(object sender, EventArgs e)
        {
            var idParceiro = id_parceiro.Text;

            ConexaoBD conexao = new ConexaoBD();
            conexao.openConnection();

            var cmd = new SqlCommand(@"SELECT C.NOME_USUARIO, C.DATA_USUARIO, C.DOCUMENTO, C.EMAIL, CO.DDD, CO.TELEFONE, P.NOME_FANTASIA_PARCEIRO,
                                         E.RUA, E.NUMERO, E.CEP,  CI.NOME_CIDADE, ES.NOME_ESTADO,T.DESCRICAO
                                        FROM CADASTRO C LEFT JOIN CONTATO CO
                                        ON C.ID_USUARIO = CO.ID_USUARIO 
                                        INNER JOIN PARCEIRO P 
                                        ON C.ID_USUARIO = P.ID_USUARIO
                                        INNER JOIN TIPO_PARCEIRO T
                                        ON P.TIPO_PARCEIRO = T.ID_TIPO_PARCEIRO
                                        INNER JOIN ENDERECO E 
                                        ON C.ID_USUARIO = E.ID_USUARIO
                                        INNER JOIN CIDADE CI 
                                        ON E.ID_CIDADE = CI.ID_CIDADE 
                                        INNER JOIN ESTADO ES 
                                        ON E.ID_ESTADO = ES.ID_ESTADO
                                        WHERE ID_PARCEIRO = '" + idParceiro + "'", conexao.getConnection());
                var rd = cmd.ExecuteReader();
                DateTime data;

                IList<ListaDeDados> lista = new List<ListaDeDados>();

                while(rd.Read()){

                    var consulta = new ListaDeDados();

                    consulta.nomeUsuario = rd.GetString(0);
                    consulta.documentoUsuario = rd.GetString(2);
                    consulta.dataUsuario = rd.GetDateTime(1);
                    consulta.emailUsuario = rd.GetString(3);
                    consulta.dddUsuario = rd.GetString(4);
                    consulta.telefoneUsuario = rd.GetString(5);
                    consulta.nomeFantasiaParceiro = rd.GetString(6);
                    consulta.cepUsuario = rd.GetString(9);
                    consulta.enderecoUsuario = rd.GetString(7);
                    consulta.numeroUsuario = rd.GetString(8);
                    consulta.nomeCidadeUsuario = rd.GetString(10);
                    consulta.nomeEstadoUsuario = rd.GetString(11);
                    consulta.descricaoTipoParceiro = rd.GetString(12);

                    lista.Add(consulta);

                }
             conexao.closeConnection();
             GridCampos.DataSource = lista;
             GridCampos.DataBind();
             /*DefineCampos();*/
        }


        protected void btnApagaCampos_Click(object sender, EventArgs e)
        {
            cnpj_text.Text = "";
            nome_razao_social.Text = "";
            id_parceiro.Text = "";
            razao_social_text.Text = "";
            data_funda_text.Text = "";
            email_text.Text = "";
            telefone_text.Text = "";
            nome_fantasia_text.Text = "";
            rua_text.Text = "";
            numero_text.Text = "";
            cep_text.Text = "";
            cidade_text.Text = "";
            estado_text.Text = "";
            documento_text.Text = "";
            tipo_parceiro_text.Text = "";
            GridCampos.Visible = false;
            
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            
        }
    }
}