using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DzAnalyzer.Models.Atendimento;
using System.Data.SqlClient;




namespace DzAnalyzer.Banco.Atendimento
{
    public class Atendimento_Operacoes
    {
        private ConexaoBD conexao;
        private NChamadoCliente_Parceiro numberChamado = new NChamadoCliente_Parceiro();

        public List<TipoAtendimento> ListarTipo()
        {
            List<TipoAtendimento> tipo = new List<TipoAtendimento>();
            String select = "select * from TIPO_ATENDIMENTO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    TipoAtendimento atendi = new TipoAtendimento();
                    atendi.idTipoAntendimento = rd.GetInt32(0);
                    atendi.tipoAtendimento = rd.GetString(1);

                    tipo.Add(atendi);
                }


                conexao.closeConnection();
            }
            return tipo;
        }

        public List<StatusAtendimento> ListarStatus(){

            List<StatusAtendimento> status = new List<StatusAtendimento>();
            String select = "select * from STATUS_ATENDIMENTO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    StatusAtendimento stat = new StatusAtendimento();
                    stat.id_status = rd.GetInt32(0);
                    stat.descricao = rd.GetString(1);

                    status.Add(stat);
                }
                conexao.closeConnection();
            }
            return status;
        }
        public List<NChamadoCliente_Parceiro> ListarNChamadoCliente()
        {
            List<NChamadoCliente_Parceiro> nchamCliente = new List<NChamadoCliente_Parceiro>();
            String select = "Select MAX(ID_N_CHAMADO + 1) FROM CHAMADO_ATENDIMENTO_CLIENTE ";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    NChamadoCliente_Parceiro numberChamado = new NChamadoCliente_Parceiro();
                    numberChamado.id_n_chamadoCliente = rd.GetInt32(0);

                    nchamCliente.Add(numberChamado);
                }
                conexao.closeConnection();
            }
            return nchamCliente;
        }

        public NChamadoCliente_Parceiro NChamadoCliente()
        {
            String select = "Select MAX(ID_N_CHAMADO + 1) FROM CHAMADO_ATENDIMENTO_CLIENTE ";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {

                    numberChamado.id_n_chamadoCliente = rd.GetInt32(0);

                }
                conexao.closeConnection();
            }
            return numberChamado;
        }

        public NChamadoCliente_Parceiro NChamadoParceiro()
        {
            String select = "Select MAX(ID_N_CHAMADO + 1) FROM CHAMADO_ATENDIMENTO_PARCEIRO";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    numberChamado.id_n_chamadoParceiro = rd.GetInt32(0);

                }
                conexao.closeConnection();
            }
            return numberChamado;
        }
       
        public List<ListaChamadoParceiro> ListaChamadoParceiro()
        {
            List<ListaChamadoParceiro> listChamadoParceiro = new List<ListaChamadoParceiro>();
            String select = "SELECT ID_N_CHAMADO,CNPJ,C.NOME_USUARIO, C.DATA_USUARIO, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO, DATA_FECHAMENTO_CHAMADO, ID_STATUS FROM CHAMADO_ATENDIMENTO_PARCEIRO INNER JOIN CADASTRO C ON CNPJ = C.DOCUMENTO";
            using (conexao = new ConexaoBD()){
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ListaChamadoParceiro atCliente = new ListaChamadoParceiro();
                    atCliente.id_n_chamado = rd.GetInt32(0);
                    atCliente.cpf = rd.GetString(1);
                    atCliente.nomeUser = rd.GetString(2);
                    atCliente.tipo_atendimento = rd.GetString(4);
                    atCliente.descricao_chamado = rd.GetString(5);
                    atCliente.data_abertura_chamado = rd.GetDateTime(6);
                    var dataFec = rd.GetSqlDateTime(7);

                    var resp = dataFec.IsNull;

                    if (resp)
                    {
                        atCliente.data_fechamento_chamado = null;
                    
                    }
                    else
                    {
                        atCliente.data_fechamento_chamado = dataFec.Value;
                    
                    }
                    
                    var status = rd.GetInt32(8);
                    if (status == 1)
                    {
                        atCliente.id_status = "Aberto";
                    }
                    else if (status == 2)
                    {
                        atCliente.id_status = "Em Andamento";
                    }
                    else { atCliente.id_status = "Fechado"; }
                    
                    listChamadoParceiro.Add(atCliente);
                }
                conexao.closeConnection();
            }
            return listChamadoParceiro;
        }

        public List<ListaChamadoCliente> ListaChamadoCliente()
        {
            List<ListaChamadoCliente> listChamadoCliente = new List<ListaChamadoCliente>();
            String select = "SELECT ID_N_CHAMADO,CPF,C.NOME_USUARIO, C.DATA_USUARIO, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO, DATA_FECHAMENTO_CHAMADO, ID_STATUS FROM CHAMADO_ATENDIMENTO_CLIENTE INNER JOIN CADASTRO C ON CPF = C.DOCUMENTO";
            using (conexao = new ConexaoBD()){
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ListaChamadoCliente atCliente = new ListaChamadoCliente();
                    atCliente.id_n_chamado = rd.GetInt32(0);
                    atCliente.cpf = rd.GetString(1);
                    atCliente.nomeUser = rd.GetString(2);
                    atCliente.tipo_atendimento = rd.GetString(4);
                    atCliente.descricao_chamado = rd.GetString(5);
                    atCliente.data_abertura_chamado = rd.GetDateTime(6);
                    var dataFec = rd.GetSqlDateTime(7);

                    var resp = dataFec.IsNull;

                    if (resp)
                    {
                        atCliente.data_fechamento_chamado = null;
                    
                    }
                    else
                    {
                        atCliente.data_fechamento_chamado = dataFec.Value;
                    
                    }
                    
                    var status = rd.GetInt32(8);
                    if (status == 1)
                    {
                        atCliente.id_status = "Aberto";
                    }
                    else if (status == 2)
                    {
                        atCliente.id_status = "Em Andamento";
                    }
                    else { atCliente.id_status = "Fechado"; }
                    
                    listChamadoCliente.Add(atCliente);
                }
                conexao.closeConnection();
            }
            return listChamadoCliente;
        }

        public List<ListaChamadoCliente> ListaChamadoCliet()
        {
            List<ListaChamadoCliente> listChamadoCliente = new List<ListaChamadoCliente>();
            String select = "SELECT ID_N_CHAMADO,CPF,C.NOME_USUARIO, C.DATA_USUARIO, TIPO_ATENDIMENTO, DESCRICAO_CHAMADO, DATA_ABERTURA_CHAMADO, DATA_FECHAMENTO_CHAMADO, ID_STATUS FROM CHAMADO_ATENDIMENTO_CLIENTE INNER JOIN CADASTRO C ON CPF = C.DOCUMENTO ";
            using (conexao = new ConexaoBD())
            {
                conexao.openConnection();
                SqlCommand cmd = new SqlCommand(select, conexao.getConnection());
                var rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    ListaChamadoCliente atCliente = new ListaChamadoCliente();
                    atCliente.id_n_chamado = rd.GetInt32(0);
                    atCliente.cpf = rd.GetString(1);
                    atCliente.nomeUser = rd.GetString(2);
                    atCliente.tipo_atendimento = rd.GetString(4);
                    atCliente.descricao_chamado = rd.GetString(5);
                    atCliente.data_abertura_chamado = rd.GetDateTime(6);
                    var dataFec = rd.GetSqlDateTime(7);

                    var resp = dataFec.IsNull;

                    if (resp)
                    {
                        atCliente.data_fechamento_chamado = null;

                    }
                    else
                    {
                        atCliente.data_fechamento_chamado = dataFec.Value;

                    }

                    var status = rd.GetInt32(8);
                    if (status == 1)
                    {
                        atCliente.id_status = "Aberto";
                    }
                    else if (status == 2)
                    {
                        atCliente.id_status = "Em Andamento";
                    }
                    else { atCliente.id_status = "Fechado"; }
                    listChamadoCliente.Add(atCliente);
                }
                conexao.closeConnection();
            }
            return listChamadoCliente;
        }


    }
}