using NFCeValidator.Data;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NFCeValidator.Forms
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            CarregarConfiguracao();
        }

        private void CarregarConfiguracao()
        {
            DatabaseConfig.ConfigData config = DatabaseConfig.LoadConfiguration();

            if (!string.IsNullOrEmpty(config.Servidor))
            {
                txtServidor.Text = config.Servidor;
                txtPorta.Text = config.Porta ?? "1433";
                cmbBancoDados.Text = config.BancoDados;
                txtUsuario.Text = config.Usuario ?? "";
                txtSenha.Text = config.Senha ?? "";
                txtTimeout.Text = config.Timeout ?? "120";
            }
            else
            {
                // Valores padrão
                txtServidor.Text = "localhost\\SQLEXPRESS";
                txtPorta.Text = "1433";
                txtTimeout.Text = "120";
            }

            chkMostrarSenha.Checked = false;
            txtSenha.UseSystemPasswordChar = true;
        }

        private void ParseConnectionString(string connStr)
        {
            try
            {
                string[] parts = connStr.Split(';');
                foreach (string part in parts)
                {
                    string[] keyValue = part.Split('=');
                    if (keyValue.Length == 2)
                    {
                        string key = keyValue[0].Trim().ToLower();
                        string value = keyValue[1].Trim();

                        switch (key)
                        {
                            case "server":
                            case "data source":
                                // Separar servidor e porta se houver vírgula
                                if (value.Contains(","))
                                {
                                    string[] serverPort = value.Split(',');
                                    txtServidor.Text = serverPort[0];
                                    txtPorta.Text = serverPort[1];
                                }
                                else
                                {
                                    txtServidor.Text = value;
                                }
                                break;
                            case "database":
                            case "initial catalog":
                                cmbBancoDados.Text = value;
                                break;
                            case "user id":
                            case "uid":
                                txtUsuario.Text = value;
                                break;
                            case "password":
                            case "pwd":
                                txtSenha.Text = value;
                                break;
                            case "connection timeout":
                                txtTimeout.Text = value;
                                break;
                            case "integrated security":
                                // Se usar autenticação Windows, limpar usuário e senha
                                if (value.ToLower() == "true" || value.ToLower() == "sspi")
                                {
                                    txtUsuario.Text = "";
                                    txtSenha.Text = "";
                                }
                                break;
                        }
                    }
                }
            }
            catch
            {
                // Se não conseguir parsear, deixa os campos vazios
            }
        }

        private void btnListarBancos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                MessageBox.Show("Informe o servidor/instância primeiro!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServidor.Focus();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                btnListarBancos.Enabled = false;
                btnListarBancos.Text = "Carregando...";

                // Construir connection string temporária para listar bancos
                string servidor = txtServidor.Text.Trim();
                string porta = txtPorta.Text.Trim();
                string usuario = txtUsuario.Text.Trim();
                string senha = txtSenha.Text;

                // Adicionar porta ao servidor se informada
                if (!string.IsNullOrEmpty(porta) && porta != "1433")
                {
                    servidor = $"{servidor},{porta}";
                }

                string connStrMaster = $"Server={servidor};Database=master;";

                // Autenticação
                if (!string.IsNullOrEmpty(usuario))
                {
                    connStrMaster += $"User Id={usuario};Password={senha};";
                }
                else
                {
                    connStrMaster += "Integrated Security=true;";
                }

                connStrMaster += "Connection Timeout=10;";

                // Listar bancos de dados
                List<string> bancos = ListarBancosDados(connStrMaster);

                if (bancos.Count > 0)
                {
                    cmbBancoDados.Items.Clear();
                    foreach (string banco in bancos)
                    {
                        cmbBancoDados.Items.Add(banco);
                    }

                    MessageBox.Show($"{bancos.Count} banco(s) de dados encontrado(s)!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (cmbBancoDados.Items.Count > 0 && string.IsNullOrEmpty(cmbBancoDados.Text))
                    {
                        cmbBancoDados.DroppedDown = true;
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum banco de dados encontrado!", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao listar bancos de dados:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnListarBancos.Enabled = true;
                btnListarBancos.Text = "🔄 Listar Bancos";
            }
        }

        private List<string> ListarBancosDados(string connectionString)
        {
            List<string> bancos = new List<string>();

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    SELECT name 
                    FROM sys.databases 
                    WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')
                    AND state_desc = 'ONLINE'
                    ORDER BY name";

                using (System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(query, conn))
                {
                    using (System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bancos.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return bancos;
        }

        private string ConstruirConnectionString()
        {
            string servidor = txtServidor.Text.Trim();
            string porta = txtPorta.Text.Trim();
            string banco = cmbBancoDados.Text.Trim();
            string usuario = txtUsuario.Text.Trim();
            string senha = txtSenha.Text;
            string timeout = txtTimeout.Text.Trim();

            // Adicionar porta ao servidor se informada e diferente da padrão
            if (!string.IsNullOrEmpty(porta) && porta != "1433")
            {
                servidor = $"{servidor},{porta}";
            }

            string connStr = $"Server={servidor};Database={banco};";

            // Se usuário foi informado, usar autenticação SQL
            if (!string.IsNullOrEmpty(usuario))
            {
                connStr += $"User Id={usuario};Password={senha};";
            }
            else
            {
                // Caso contrário, usar autenticação Windows
                connStr += "Integrated Security=true;";
            }

            // Adicionar timeout se diferente do padrão
            if (!string.IsNullOrEmpty(timeout) && timeout != "15")
            {
                connStr += $"Connection Timeout={timeout};";
            }

            return connStr;
        }

        private void btnTestar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                string connStr = ConstruirConnectionString();
                NFCeRepository repo = new NFCeRepository(connStr);

                if (repo.TestConnection())
                {
                    MessageBox.Show("Conexão realizada com sucesso!", "Sucesso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Não foi possível conectar ao banco de dados!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao testar conexão: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtServidor.Text))
            {
                MessageBox.Show("Informe o servidor/instância!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServidor.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbBancoDados.Text))
            {
                MessageBox.Show("Informe o banco de dados!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBancoDados.Focus();
                return false;
            }

            return true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            try
            {
                DatabaseConfig.SaveConfiguration(
                    txtServidor.Text.Trim(),
                    txtPorta.Text.Trim(),
                    cmbBancoDados.Text.Trim(),
                    txtUsuario.Text.Trim(),
                    txtSenha.Text,
                    txtTimeout.Text.Trim()
                );

                MessageBox.Show("Configuração salva com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar configuração: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.UseSystemPasswordChar = !chkMostrarSenha.Checked;
        }
    }
}