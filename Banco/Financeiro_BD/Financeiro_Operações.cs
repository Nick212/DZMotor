using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using System.Configuration;
using DzAnalyzer.Models.Parceiro;
using DzAnalyzer.Models.Fatura;
using DzAnalyzer.Models.Financeiro;

namespace DzAnalyzer.Banco.Financeiro_BD
{
    public class Financeiro_Operações
    {
        ConexaoBD conn = new ConexaoBD();

        public List<FaturaModelViewParceiro> ListarParceiros()
        {
            List<FaturaModelViewParceiro> parceiros = new List<FaturaModelViewParceiro>();
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                var cmd = new SqlCommand(@"SELECT A.ID_PARCEIRO,B.NOME_FANTASIA_PARCEIRO FROM MECANICA A WITH(NOLOCK)
                                            INNER JOIN PARCEIRO B WITH(NOLOCK)
                                            ON A.ID_PARCEIRO = B.ID_PARCEIRO", conn.getConnection());
                var rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    FaturaModelViewParceiro parceiro = new FaturaModelViewParceiro();

                    parceiro.idParceiro = rd.GetInt32(0);
                    parceiro.nomeFantasiaParceiro = rd.GetString(1);
                    parceiros.Add(parceiro);
                }
            }
            return parceiros;
        }

        public FaturaModelViewCredito ValoresFatura(string idParceiro)
        {
            using (conn = new ConexaoBD())
            {
                conn.openConnection();
                SqlTransaction transacao = conn.getConnection().BeginTransaction("transacao");
                try
                {
                    FaturaModelViewCredito creditos = new FaturaModelViewCredito();
                    
                    var cmd = new SqlCommand(@"SELECT	COUNT(0) AS 'TRANSAÇÕES'
                                            ,		ISNULL(SUM(VALOR_DOTZ),0) AS 'VALOR EM DOTZ'
                                            ,		ISNULL(SUM(VALOR_DOTZ) * 0.02,0) AS 'VALOR EM REAIS'
                                            ,		getdate() AS DATA_ABERTURA
                                            ,		getdate()+30 AS DATA_FECHAMENTO
                                            FROM CREDITO WITH(NOLOCK)
                                            WHERE ID_MECANICA IN (SELECT ID_MECANICA FROM MECANICA WITH(NOLOCK) WHERE ID_PARCEIRO = @ID_PARCEIRO)
                                            AND ID_STATUS = 9
                                            AND ID_FATURA is null
                    and flag_faturamento = 0", conn.getConnection(), transacao);
                    cmd.Parameters.Add(new SqlParameter("@ID_PARCEIRO", idParceiro));
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        creditos.transacoes = rd.GetInt32(0);
                        creditos.valorDotz = rd.GetInt32(1);
                        creditos.valorReais = rd.GetDecimal(2);
                        creditos.dataAbertura = rd.GetDateTime(3);
                        creditos.dataFechamento = rd.GetDateTime(4);
                    }
                    rd.Close();
                    rd.Dispose();


                    var cmd1 = new SqlCommand(@"UPDATE CREDITO 
                                            SET flag_faturamento = 1, id_status = 8
                                            WHERE ID_MECANICA IN (SELECT ID_MECANICA FROM MECANICA WITH(NOLOCK) WHERE ID_PARCEIRO = @ID_PARCEIRO)
                                            AND ID_STATUS = 9
                                            AND ID_FATURA is null
                    and flag_faturamento = 0", conn.getConnection(), transacao);
                    cmd1.Parameters.Add(new SqlParameter("@ID_PARCEIRO", idParceiro));

                    cmd1.ExecuteNonQuery();

                    transacao.Commit();
                    conn.closeConnection();
                    return creditos;
                }
                catch (SqlException e)
                {
                    transacao.Rollback();
                    
                    throw;
                }
               
            }
        }

        public int InserirFatura(FaturaModelViewCredito inserirfatura, string idParceiro)
        {
            using(conn = new ConexaoBD())
            {
                
                conn.openConnection();
                SqlTransaction transacao = conn.getConnection().BeginTransaction("transacao");

                try
                {
                    var cmd = new SqlCommand(@"INSERT FATURA (ID_PARCEIRO, DATA_ABERTURA, DATA_FECHAMENTO, DESCONTO, VALOR_FATURA, ID_STATUS) 
                                                VALUES (@ID_PARCEIRO, @DATA_ABERTURA, @DATA_FECHAMENTO, @DESCONTO, @VALOR_FATURA, 1)", conn.getConnection(),transacao);
                    cmd.Parameters.Add(new SqlParameter("@ID_PARCEIRO", idParceiro));
                    cmd.Parameters.Add(new SqlParameter("@DATA_ABERTURA", inserirfatura.dataAbertura));
                    cmd.Parameters.Add(new SqlParameter("@DATA_FECHAMENTO", inserirfatura.dataFechamento));
                    cmd.Parameters.Add(new SqlParameter("@DESCONTO", inserirfatura.desconto));
                    cmd.Parameters.Add(new SqlParameter("@VALOR_FATURA", inserirfatura.valorReais));
                    cmd.ExecuteNonQuery();

                    var cmd1 = new SqlCommand(@"UPDATE CREDITO 
                                            SET ID_FATURA = IDENT_CURRENT('fatura')
                                            WHERE ID_MECANICA IN (SELECT ID_MECANICA FROM MECANICA WITH(NOLOCK) WHERE ID_PARCEIRO = @ID_PARCEIRO)
                                            AND ID_STATUS = 8
                                            AND flag_faturamento = 1", conn.getConnection(), transacao);
                    cmd1.Parameters.Add(new SqlParameter("@ID_PARCEIRO", idParceiro));

                    cmd1.ExecuteNonQuery();

                    transacao.Commit();
                    conn.closeConnection();
                    return 0;
                }
                catch (SqlException e)
                {
                    transacao.Rollback();
                    return e.Errors[0].Number;
                    throw;
                }
            }
        }

        public List<Fatura> ListaFaturas(int id_parceiro)
        {
            List<Fatura> lista = new List<Fatura>();

            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand(@"SELECT A.ID_FATURA,
												B.NOME_FANTASIA_PARCEIRO,
												A.DATA_ABERTURA,
												A.DATA_FECHAMENTO,
												A.DESCONTO,
												A.VALOR_FATURA,
												C.DESCRICAO
											FROM FATURA A WITH(NOLOCK)
											INNER JOIN PARCEIRO B WITH(NOLOCK)
											ON A.ID_PARCEIRO = B.ID_PARCEIRO
											INNER JOIN STATUS_FATURA C WITH(NOLOCK)
											ON A.ID_STATUS = C.ID_STATUS 
                                            WHERE a.ID_PARCEIRO = " + id_parceiro, conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Fatura fatura = new Fatura();

                fatura.idFatura = rd.GetInt32(0);
                fatura.nomeParceiro = rd.GetString(1);
                fatura.dataAbertura = rd.GetDateTime(2);
                fatura.dataFechamento = rd.GetDateTime(3);
                fatura.desconto = rd.GetDecimal(4);
                fatura.valorFatura = rd.GetDecimal(5);
                fatura.descStatus = rd.GetString(6);

                lista.Add(fatura);
            }

            return lista;
        }

        public List<Fatura> ListaTodasFaturas()
        {
            List<Fatura> lista = new List<Fatura>();

            ConexaoBD conn = new ConexaoBD();

            conn.openConnection();

            var cmd = new SqlCommand(@"SELECT A.ID_FATURA,
												B.NOME_FANTASIA_PARCEIRO,
												A.DATA_ABERTURA,
												A.DATA_FECHAMENTO,
												A.DESCONTO,
												A.VALOR_FATURA,
												C.DESCRICAO
											FROM FATURA A WITH(NOLOCK)
											INNER JOIN PARCEIRO B WITH(NOLOCK)
											ON A.ID_PARCEIRO = B.ID_PARCEIRO
											INNER JOIN STATUS_FATURA C WITH(NOLOCK)
											ON A.ID_STATUS = C.ID_STATUS", conn.getConnection());
            var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                Fatura fatura = new Fatura();

                fatura.idFatura = rd.GetInt32(0);
                fatura.nomeParceiro = rd.GetString(1);
                fatura.dataAbertura = rd.GetDateTime(2);
                fatura.dataFechamento = rd.GetDateTime(3);
                fatura.desconto = rd.GetDecimal(4);
                fatura.valorFatura = rd.GetDecimal(5);
                fatura.descStatus = rd.GetString(6);

                lista.Add(fatura);
            }

            return lista;
        }
    }
}