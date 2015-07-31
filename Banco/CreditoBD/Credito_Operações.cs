using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DzAnalyzer.Banco;
using DzAnalyzer.Models.Credito;
using DzAnalyzer.Models.Cadastro;
using System.Collections;
using DzAnalyzer.Models.Parceiro;

namespace DzAnalyzer.Banco.CreditoBD
{
    public class Credito_Operações
    {

        /// <summary>
        /// Vê se a ID do Status para a criação do crédito é válida no banco
        /// </summary>
        /// <param name="id_status"></param>
        /// <returns>True ou False</returns>
        public Boolean ValidaStatus(String id_status)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select * from status_credito_troca where id_status=" + id_status, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (!rd.Read())
            {
                conn.closeConnection();
                return false;
            }
            conn.closeConnection();
            return true;
        }

        /// <summary>
        /// Retorna uma lista com todos os nomes existentes no banco iguais ao do parâmetro
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Lista com nome e ID dos usuários</returns>
        public List<ID_Nome> ListaNomes(string nome)
        {

            ConexaoBD conn = new ConexaoBD();
            List<ID_Nome> lista = new List<ID_Nome>();

            conn.openConnection();

            var cmd = new SqlCommand("select id_usuario, nome_usuario, documento from cadastro where nome_usuario like '%" + nome + "%' ", conn.getConnection());
            var rd = cmd.ExecuteReader();


            while (rd.Read())
            {
                ID_Nome id_nome = new ID_Nome();

                id_nome.id_usuario = rd.GetInt32(0);
                id_nome.nome = rd.GetString(1);
                id_nome.documento = double.Parse(rd.GetString(2));
                lista.Add(id_nome);
            }
            cmd.Dispose();
            rd.Dispose();
            conn.closeConnection();
            return lista;
        }

        /// <summary>
        /// Retorna uma lista com todos os nomes existentes no banco iguais ao do parâmetro
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Lista com nome e ID de crédito das trasações dos usuários</returns>
        public List<Credito_Nome> ListaNomes_Editar(string nome)
        {
            ConexaoBD conn = new ConexaoBD();
            List<Credito_Nome> lista = new List<Credito_Nome>();

            conn.openConnection();

            var cmd = new SqlCommand(@"SELECT C.ID_CREDITO, U.NOME_USUARIO, C.VALOR_COMPRA, U.DOCUMENTO, C.VALOR_DOTZ
                                    FROM CREDITO C
                                    INNER JOIN CADASTRO U 
                                    ON C.ID_USUARIO = U.ID_USUARIO 
                                    WHERE NOME_USUARIO LIKE '" + nome + "%' ", conn.getConnection());
            var rd = cmd.ExecuteReader();


            while (rd.Read())
            {
                Credito_Nome credito_nome = new Credito_Nome();

                credito_nome.id_credito = rd.GetInt32(0);
                credito_nome.nome = rd.GetString(1);
                credito_nome.valor_compra = rd.GetDecimal(2);
                credito_nome.documento = double.Parse(rd.GetString(3));
                credito_nome.valor_dotz = rd.GetInt32(4);
                lista.Add(credito_nome);
            }
            cmd.Dispose();
            rd.Dispose();
            conn.closeConnection();
            return lista;
        }

        /// <summary>
        /// Busca o valor e ID do crédito em função do parâmetro
        /// </summary>
        /// <param name="id_credito"></param>
        /// <returns>Devolve um objeto com valor e ID ou um objeto vazio</returns>
        public Valor_Mecanica ListaValor_Mecanica(int id_credito)
        {
            ConexaoBD conn = new ConexaoBD();
            Valor_Mecanica vm = new Valor_Mecanica();

            conn.openConnection();

            var cmd = new SqlCommand("select valor_compra, id_mecanica, id_status from credito where id_credito =" + id_credito, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                vm.valor_compra = rd.GetDecimal(0);
                vm.id_mecanica = rd.GetInt32(1);
                vm.id_status = rd.GetInt32(2);

                return vm;
            }

            conn.closeConnection();
            return vm;
        }

        /// <summary>
        /// Vê se a ID de crédito é válida no banco
        /// </summary>
        /// <param name="id_credito"></param>
        /// <returns>True ou False</returns>
        public Boolean ValidaCredito(int id_credito)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select * from credito where id_credito =" + id_credito, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                conn.closeConnection();
                return true;
            }
            conn.closeConnection();
            return false;
        }

        /// <summary>
        /// Retorna uma lista com todas as transações de um usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns>Lista com todos os atributos de um crédito</returns>
        public List<ModeloCredito> ListaID(int id_usuario)
        {
            ConexaoBD conn = new ConexaoBD();
            List<ModeloCredito> lista = new List<ModeloCredito>();

            conn.openConnection();

            var cmd = new SqlCommand("select * from credito where id_usuario =" + id_usuario, conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ModeloCredito crd = new ModeloCredito();

                crd.id_credito = rd.GetInt32(0);
                crd.data_compra = rd.GetDateTime(1);
                crd.id_usuario = rd.GetInt32(2);
                crd.id_status = rd.GetInt32(3);
                crd.nota_fiscal = Int32.Parse(rd.GetString(4));
                crd.id_mecanica = rd.GetInt32(5);
                crd.data_credito = rd.GetDateTime(6);
                crd.motivo = rd.GetString(7);

                var faturaID = rd.GetSqlInt32(8);

                crd.id_fatura = faturaID.IsNull ? 0 : Convert.ToInt32(faturaID.Value);
                
                crd.valor_compra = rd.GetDecimal(9);
                crd.valor_dotz = rd.GetInt32(10);

                lista.Add(crd);
            }
            cmd.Dispose();
            rd.Dispose();
            conn.closeConnection();
            return lista;
        }

        /// <summary>
        /// Retorna lista com todas as transações entre uma data
        /// </summary>
        /// <param name="data"></param>
        /// <param name="valor"></param>
        /// <returns>Lista com todos os atritubos de um crédito</returns>
        public List<ModeloCredito> ListaDatas(DateTime data, int valor)
        {
            ConexaoBD conn = new ConexaoBD();
            List<ModeloCredito> lista = new List<ModeloCredito>();

            conn.openConnection();

            DateTime de = data.AddDays(-valor);

            var cmd = new SqlCommand("select * from credito where data_credito between '" + de.ToString("yyyy-MM-dd hh:mm:ss") + "' and '" + data.ToString("yyyy-MM-dd hh:mm:ss") + "'", conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ModeloCredito crd = new ModeloCredito();

                crd.id_credito = rd.GetInt32(0);
                crd.data_compra = rd.GetDateTime(1);
                crd.id_usuario = rd.GetInt32(2);
                crd.id_status = rd.GetInt32(3);
                crd.nota_fiscal = Int32.Parse(rd.GetString(4));
                crd.id_mecanica = rd.GetInt32(5);
                crd.data_credito = rd.GetDateTime(6);
                crd.motivo = rd.GetString(7);

                var faturaID = rd.GetSqlInt32(8);

                crd.id_fatura = faturaID.IsNull ? 0 : Convert.ToInt32(faturaID.Value);

                crd.valor_compra = rd.GetDecimal(9);
                crd.valor_dotz = rd.GetInt32(10);

                lista.Add(crd);
            }
            cmd.Dispose();
            rd.Dispose();
            conn.closeConnection();

            return lista;
        }

        /// <summary>
        /// Retorna atributos do crédito em função do parâmetro
        /// </summary>
        /// <param name="id_credito"></param>
        /// <returns>Lista com os atributos do crédito</returns>
        public List<ModeloCredito> ListaCredito(int id_credito)
        {
            ConexaoBD conn = new ConexaoBD();
            List<ModeloCredito> lista = new List<ModeloCredito>();

            conn.openConnection();

            var cmd = new SqlCommand("select * from credito where id_credito = " + id_credito, conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                ModeloCredito crd = new ModeloCredito();

                crd.id_credito = rd.GetInt32(0);
                crd.data_compra = rd.GetDateTime(1);
                crd.id_usuario = rd.GetInt32(2);
                crd.id_status = rd.GetInt32(3);
                crd.nota_fiscal = Int32.Parse(rd.GetString(4));
                crd.id_mecanica = rd.GetInt32(5);
                crd.data_credito = rd.GetDateTime(6);
                crd.motivo = rd.GetString(7);

                var faturaID = rd.GetSqlInt32(8);

                crd.id_fatura = faturaID.IsNull ? 0 : Convert.ToInt32(faturaID.Value);

                crd.valor_compra = rd.GetDecimal(9);
                crd.valor_dotz = rd.GetInt32(10);

                lista.Add(crd);
            }
            cmd.Dispose();
            rd.Dispose();
            conn.closeConnection();

            return lista;
        }

        /// <summary>
        /// Vê se a ID da Mecânica é válida
        /// </summary>
        /// <param name="id_mecanica"></param>
        /// <returns>True ou False</returns>
        public Boolean ValidaMecanica(int id_mecanica)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select * from mecanica where id_mecanica=" + id_mecanica + " and flag_funcionamento = 1", conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (!rd.Read())
            {
                conn.closeConnection();
                return false;
            }
            conn.closeConnection();
            return true;
        }

        /// <summary>
        /// Insere o crédito no banco de dados
        /// </summary>
        /// <param name="credito"></param>
        /// <returns>True or false</returns>
        public Boolean FazCredito(ModeloCredito credito, ref int id_credito, ref int valor_credito)
        {
            try
            {

                ConexaoBD conn = new ConexaoBD();

                conn.openConnection();

                int mecanica;
                int real;

                var cmd = new SqlCommand("select vl_dotz, vl_real from mecanica where id_mecanica =" + credito.id_mecanica, conn.getConnection());
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    
                    mecanica = rd.GetInt32(0);
                    real = rd.GetInt32(1);

                    cmd.Dispose();
                    rd.Dispose();

                    SqlTransaction transacao = conn.getConnection().BeginTransaction("transacao");

                    try
                    {

                        valor_credito = (int)Math.Truncate(credito.valor_compra * (mecanica / real));

                        cmd = new SqlCommand("insert into credito values('" + credito.data_compra.ToString("yyyy-MM-dd hh:mm:ss") + "'," + credito.id_usuario + "," + credito.id_status + ",'" + credito.nota_fiscal + "'," +
                                              credito.id_mecanica + ",'" + credito.data_credito.ToString("yyyy-MM-dd hh:mm:ss") + "','" + credito.motivo + "', null, " + credito.valor_compra.ToString().Replace(",", ".") + "," + valor_credito + ", 0)", conn.getConnection(), transacao);
                        cmd.ExecuteNonQuery();

                        cmd.Dispose();

                        cmd = new SqlCommand("update cadastro set saldo = saldo +" + Math.Truncate(credito.valor_compra * (mecanica / real)) + " where id_usuario = " + credito.id_usuario, conn.getConnection(), transacao);
                        cmd.ExecuteNonQuery();

                        transacao.Commit();
                    }
                    catch (SqlException e) { transacao.Rollback(); return false; }
                    finally
                    {
                        cmd.Dispose();
                        rd.Dispose();
                    }
                }

                rd.Dispose();

                cmd = new SqlCommand("SELECT ident_current('Credito')",conn.getConnection());
                rd = cmd.ExecuteReader();
                
                if (rd.Read())
                {
                    id_credito = Int32.Parse(rd.GetDecimal(0).ToString());
                }

                cmd.Dispose();
                rd.Dispose();

                conn.closeConnection();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Retorna a ID do usuário para a ID passada
        /// </summary>
        /// <param name="id_credito"></param>
        /// <returns>ID do Usuário</returns>
        public static int RetornaID(int id_credito)
        {
            ConexaoBD conn = new ConexaoBD();

            int id_usuario = 0;

            conn.openConnection();

            var cmd = new SqlCommand("select id_usuario from credito where id_credito = " + id_credito, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                id_usuario = rd.GetInt32(0);
                conn.closeConnection();
                return id_usuario;
            }
            conn.closeConnection();
            return id_usuario;
        }

        /// <summary>
        /// Desfaz o crédito para a edição
        /// </summary>
        /// <param name="id_credito"></param>
        /// <param name="valor_compra"></param>
        /// <param name="id_mecanica"></param>
        /// <returns>True ou false</returns>
        public Boolean LimpaCriacao(int id_credito, decimal valor_compra, int id_mecanica)
        {


            int mecanica = 0;
            int real = 0;
            ConexaoBD conn = new ConexaoBD();
            conn.openConnection();

            var cmd = new SqlCommand("select vl_dotz, vl_real from mecanica where id_mecanica =" + id_mecanica, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                mecanica = rd.GetInt32(0);
                real = rd.GetInt32(1);
            }

            cmd.Dispose();
            rd.Dispose();

            var valor_compra_limitado = Math.Round(valor_compra, 0);

            cmd = new SqlCommand("update cadastro set saldo = saldo - " + valor_compra_limitado * (mecanica / real) + " where id_usuario = (select id_usuario from credito where id_credito = " + id_credito + ")", conn.getConnection());
            cmd.ExecuteNonQuery();

            cmd.Dispose();
            rd.Dispose();
            
            conn.closeConnection();

            return true;
        }

        /// <summary>
        /// Atualiza o crédito após a edição
        /// </summary>
        /// <param name="crd"></param>
        /// <returns>True or false</returns>
        public Boolean UpdateCredito(ModeloCredito crd)
        {
            int mecanica = 0;
            int real = 0;
            ConexaoBD conn = new ConexaoBD();
            conn.openConnection();

            var cmd = new SqlCommand("select vl_dotz, vl_real from mecanica where id_mecanica =" + crd.id_mecanica, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                mecanica = rd.GetInt32(0);
                real = rd.GetInt32(1);

                cmd.Dispose();
                rd.Dispose();

                SqlTransaction transacao = conn.getConnection().BeginTransaction("transacao");

                try
                {
                    cmd = new SqlCommand(@"update credito set valor_compra = " + crd.valor_compra.ToString().Replace(",", ".") + ", valor_dotz = " + Math.Truncate(crd.valor_compra * (mecanica / real)) +
                                           ", id_mecanica = " + crd.id_mecanica + ", id_status =" + crd.id_status + ", data_credito = '" + crd.data_credito.ToString("yyyy-MM-dd hh:mm:ss") + "' where id_credito = " + crd.id_credito, conn.getConnection(),transacao);
                    cmd.ExecuteNonQuery();

                    cmd.Dispose();
                    rd.Dispose();

                    cmd = new SqlCommand("update cadastro set saldo = saldo +" + Math.Truncate(crd.valor_compra * (mecanica / real)) + " where id_usuario = " + crd.id_usuario, conn.getConnection(),transacao);
                    cmd.ExecuteNonQuery();

                    transacao.Commit();
                }
                catch (SqlException e) 
                {
                    transacao.Rollback(); 
                    return false; 
                }
            }
            conn.closeConnection();

            return true;
        }

        /// <summary>
        /// Atualiza o saldo após a criação de um crédito
        /// </summary>
        /// <param name="credito"></param>
        /// <returns>True or false</returns>
        public Boolean FazUpdate(ModeloCredito credito)
        {
            try
            {
                int mecanica = 0;
                int real = 0;
                ConexaoBD conn = new ConexaoBD();
                conn.openConnection();

                var cmd = new SqlCommand("select vl_dotz, vl_real from mecanica where id_mecanica =" + credito.id_mecanica, conn.getConnection());
                var rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    mecanica = rd.GetInt32(0);
                    real = rd.GetInt32(1);
                }

                cmd.Dispose();
                rd.Dispose();

                cmd = new SqlCommand("update cadastro set saldo = saldo +" + Math.Truncate(credito.valor_compra * (mecanica / real)) + " where id_usuario = " + credito.id_usuario, conn.getConnection());
                cmd.ExecuteNonQuery();

                conn.closeConnection();

                return true;
            }
            catch (Exception e) { return false; }
        }

        /// <summary>
        /// Vê se o documento digitado existe no banco
        /// </summary>
        /// <param name="documento"></param>
        /// <returns>Retorna a ID do usuário para o documento</returns>
        public int ValidaDocumento(double documento)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select id_usuario from cadastro where documento = '" + Math.Truncate(documento) + "'", conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                int id = rd.GetInt32(0);
                conn.closeConnection();
                return id;
            }
            else
            {
                int id = 0;
                conn.closeConnection();
                return id;
            }
        }

        /// <summary>
        /// Valida a ID do usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns> True ou False</returns>
        public Boolean ValidaID(int id_usuario)
        {
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select * from cadastro where id_usuario =" + id_usuario, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }

        /// <summary>
        /// Retorna o nome do usuario que tem o documento passado pelo parametro
        /// </summary>
        /// <param name="documento"></param>
        /// <returns>Retorna o nome do funcionario</returns>
        public string RetornaNome(double documento)
        {
            string nome = null;

            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select nome_usuario from cadastro where documento like '"+ documento +"'", conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                nome = rd.GetString(0);
                conn.closeConnection();
                return nome;
            }
            return nome;
        }

        /// <summary>
        /// Lista todos os status disponiveis
        /// </summary>
        /// <returns>Lista com todos os status</returns>
        public List<Status> ListaStatus()
        {
            List<Status> lista = new List<Status>();
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select * from STATUS_CREDITO_TROCA WHERE ID_STATUS IN(8,9,12)",conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Status st = new Status();

                st.id_status = rd.GetInt32(0);
                st.descricao_status = rd.GetString(1);

                lista.Add(st);
            }

            conn.closeConnection();
            return lista;
        }

        /// <summary>
        /// Lista todas as mecanicas disponiveis
        /// </summary>
        /// <returns>lista com mecanicas</returns>
        public List<Mecanica> ListaMecanica(int id_parceiro)
        {
            List<Mecanica> lista = new List<Mecanica>();
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select id_mecanica, desc_mecanica from mecanica where id_parceiro = " + id_parceiro + " and flag_funcionamento = 1", conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Mecanica st = new Mecanica();

                st.id_mecanica = rd.GetInt32(0);
                st.desc_mecanica = rd.GetString(1);

                lista.Add(st);
            }

            conn.closeConnection();
            return lista;
        }

        public List<Mecanica> ListaMecanica_Editar(int id_credito, ref string nome_parceiro)
        {
            List<Mecanica> lista = new List<Mecanica>();
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand(@"SELECT M.ID_MECANICA, M.DESC_MECANICA, P.NOME_FANTASIA_PARCEIRO FROM MECANICA M INNER JOIN PARCEIRO P ON M.ID_PARCEIRO = P.ID_PARCEIRO
                                       WHERE M.ID_PARCEIRO = (SELECT ID_PARCEIRO FROM MECANICA WHERE ID_MECANICA = (SELECT ID_MECANICA FROM CREDITO WHERE ID_CREDITO = " + id_credito + "))", conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Mecanica mecanica = new Mecanica();

                mecanica.id_mecanica = rd.GetInt32(0);
                mecanica.desc_mecanica = rd.GetString(1);

                nome_parceiro = rd.GetString(2);

                lista.Add(mecanica);
            }

            return lista;
        }
        public List<Parceiro> ListaParceiro()
        {
            List<Parceiro> lista = new List<Parceiro>();
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select p.id_parceiro, p.nome_fantasia_parceiro from parceiro p inner join mecanica m on p.id_parceiro = m.id_parceiro order by nome_fantasia_parceiro asc", conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Parceiro st = new Parceiro();

                st.id_parceiro = rd.GetInt32(0);
                st.nome_fantasia = rd.GetString(1);

                lista.Add(st);
            }

            conn.closeConnection();
            return lista;
        }

        public ID_Nome RetornaID_Nome(int id_usuario)
        {
            ID_Nome id_nome = null;
            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand("select id_usuario, nome_usuario, documento from cadastro where id_usuario =" + id_usuario, conn.getConnection());
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                id_nome = new ID_Nome();

                id_nome.id_usuario = rd.GetInt32(0);
                id_nome.nome = rd.GetString(1);
                id_nome.documento = double.Parse(rd.GetString(2));
            }

            conn.closeConnection();
            return id_nome;
        }
    }
}