using NFCeValidator.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace NFCeValidator.Data
{
    public class NFCeRepository
    {
        private readonly string _connectionString;

        public NFCeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        

        public NFCeInfo GetDadosNFCeCompleto(string numeroNFCe, string nomeView = "vw_NFCe")
        {
            return GetDadosNFCeCompleto(numeroNFCe, nomeView, null, null, "");
        }

        public List<string> GetEmpresas()
        {
            List<string> empresas = new List<string>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    Loja
                FROM Integrar_Lojas
                WHERE Desativado = 0";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Adiciona o nome da loja na lista de empresas
                        empresas.Add(reader["Loja"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Caso ocorra algum erro, você pode registrar ou lidar com o erro
                MessageBox.Show($"Erro ao carregar empresas: {ex.Message}");
            }

            return empresas;
        }


        public NFCeInfo GetDadosNFCeCompleto(string numeroNFCe, string nomeView, DateTime? dataInicial, DateTime? dataFinal, string Loja)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = $@"
                        SELECT TOP 1 
                            NumeroNFCe,
                            ValorTotal,
                            DataEmissao,
                            CFOP,
                            DocumentoDestinatario,
                            Status
                        FROM {nomeView} 
                        WHERE NumeroNFCe = @NumeroNFCe";

                    // Adicionar filtro de período se fornecido
                    if (dataInicial.HasValue && dataFinal.HasValue)
                    {
                        query += " AND DataEmissao BETWEEN @DataInicial AND @DataFinal";
                    }
                    if (Loja != "")
                    {
                        query += " AND Loja_Origem = @LojaOrigem";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumeroNFCe", numeroNFCe);

                        if (dataInicial.HasValue && dataFinal.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@DataInicial", dataInicial.Value.Date);
                            cmd.Parameters.AddWithValue("@DataFinal", dataFinal.Value.Date.AddDays(1).AddSeconds(-1)); // Até 23:59:59
                            
                        }
                        if (Loja != "")
                        {
                            cmd.Parameters.AddWithValue("@LojaOrigem", Loja);
                        }
                        {
                            cmd.Parameters.AddWithValue("@NumeroNFCe", numeroNFCe);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    NFCeInfo info = new NFCeInfo();

                                    // Valor
                                    if (!reader.IsDBNull(reader.GetOrdinal("ValorTotal")))
                                        info.ValorNaView = reader.GetDecimal(reader.GetOrdinal("ValorTotal"));

                                    // Data
                                    if (!reader.IsDBNull(reader.GetOrdinal("DataEmissao")))
                                        info.DataNaView = reader.GetDateTime(reader.GetOrdinal("DataEmissao"));

                                    // CFOP
                                    if (!reader.IsDBNull(reader.GetOrdinal("CFOP")))
                                        info.CFOPNaView = reader.GetString(reader.GetOrdinal("CFOP")).Trim();

                                    // Documento
                                    if (!reader.IsDBNull(reader.GetOrdinal("DocumentoDestinatario")))
                                        info.DocumentoNaView = reader.GetString(reader.GetOrdinal("DocumentoDestinatario")).Trim();

                                    // Status
                                    if (!reader.IsDBNull(reader.GetOrdinal("Status")))
                                        info.StatusNaView = reader.GetString(reader.GetOrdinal("Status")).Trim();

                                    return info;
                                }
                            }
                        }
                    }

                    return null; // Não encontrado
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar dados completos na view: {ex.Message}");
            }
        }

        public bool ExisteNFCeNaView(string numeroNFCe, string nomeView = "vw_NFCe")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = $"SELECT COUNT(*) FROM {nomeView} WHERE NumeroNFCe = @NumeroNFCe";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumeroNFCe", numeroNFCe);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar view: {ex.Message}");
            }
        }

        public bool ExisteChaveAcessoDuplicada(string chaveAcesso, string numeroNFCe, string nomeView = "vw_NFCe")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Verifica se existe a mesma chave com número diferente
                    string query = $@"
                        SELECT COUNT(*) 
                        FROM {nomeView} 
                        WHERE ChaveAcesso = @ChaveAcesso 
                        AND NumeroNFCe <> @NumeroNFCe";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ChaveAcesso", chaveAcesso);
                        cmd.Parameters.AddWithValue("@NumeroNFCe", numeroNFCe);

                        int count = (int)cmd.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false; // Em caso de erro, considera que não há duplicidade
            }
        }

        public DataTable GetAllNFCeFromView(string nomeView = "vw_NFCe")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = $"SELECT * FROM {nomeView}";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar dados da view: {ex.Message}");
            }
        }

        public List<NFCeInfo> GetTodasNFCesDoPeriodo(string nomeView, DateTime dataInicial, DateTime dataFinal, string Loja)
        {
            List<NFCeInfo> lista = new List<NFCeInfo>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = $@"
                        SELECT 
                            NumeroNFCe,
                            Serie,
                            ValorTotal,
                            DataEmissao,
                            CFOP,
                            DocumentoDestinatario,
                            Status
                        FROM {nomeView} 
                        WHERE DataEmissao BETWEEN @DataInicial AND @DataFinal";

                    if (Loja != "")
                    {
                        query += " AND LojaOrigem = @LojaOrigem ORDER BY NumeroNFCe";
                    }else
                                           {
                        query += " ORDER BY NumeroNFCe";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DataInicial", dataInicial.Date);
                        cmd.Parameters.AddWithValue("@DataFinal", dataFinal.Date.AddDays(1).AddSeconds(-1));
                        if (Loja != "")
                        {
                            cmd.Parameters.AddWithValue("@LojaOrigem", Loja);
                        }
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                NFCeInfo info = new NFCeInfo
                                {
                                    NumeroNFCe = reader["NumeroNFCe"].ToString(),
                                    Serie = reader["Serie"] != DBNull.Value ? reader["Serie"].ToString() : "",
                                    ValorTotal = reader["ValorTotal"] != DBNull.Value ? Convert.ToDecimal(reader["ValorTotal"]) : 0,
                                    DataEmissao = reader["DataEmissao"] != DBNull.Value ? Convert.ToDateTime(reader["DataEmissao"]) : (DateTime?)null,
                                    CFOP = reader["CFOP"] != DBNull.Value ? reader["CFOP"].ToString() : "",
                                    DocumentoDestinatario = reader["DocumentoDestinatario"] != DBNull.Value ? reader["DocumentoDestinatario"].ToString() : "",
                                    StatusNaView = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : ""
                                };

                                lista.Add(info);
                            }
                        }
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar NFCe do período: {ex.Message}");
            }
        }
    }
}
