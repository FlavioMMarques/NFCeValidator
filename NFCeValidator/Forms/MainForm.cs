using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NFCeValidator.Data;
using NFCeValidator.Models;
using NFCeValidator.Services;

namespace NFCeValidator.Forms
{
    public partial class MainForm : Form
    {
        private List<NFCeInfo> _listaNotas;
        private List<NFCeInfo> _listaView;
        private XmlProcessor _xmlProcessor;
        private NFCeRepository _repository;
        
        public MainForm()
        {
            InitializeComponent();
            _listaNotas = new List<NFCeInfo>();
            _listaView = new List<NFCeInfo>();
            _xmlProcessor = new XmlProcessor();

            ConfigurarDataGridViews();
            InicializarDatas();
            VerificarConfiguracao();
            CarregarEmpresas();
        }

        private void InicializarDatas()
        {
            // Define data inicial como primeiro dia do mês atual
            dtpDataInicial.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Define data final como último dia do mês atual
            dtpDataFinal.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
        }

        private void VerificarConfiguracao()
        {
            if (!DatabaseConfig.IsConfigured())
            {
                MessageBox.Show("É necessário configurar a conexão com o banco de dados antes de continuar.",
                    "Configuração Necessária", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AbrirConfiguracoes();
            }
        }

        private void ConfigurarDataGridViews()
        {
            // Configurar grade de XMLs
            dgvXML.AutoGenerateColumns = false;
            dgvXML.AllowUserToAddRows = false;
            dgvXML.AllowUserToDeleteRows = false;
            dgvXML.ReadOnly = true;
            dgvXML.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvXML.MultiSelect = false;

            // Configurar grade da View
            dgvView.AutoGenerateColumns = false;
            dgvView.AllowUserToAddRows = false;
            dgvView.AllowUserToDeleteRows = false;
            dgvView.ReadOnly = true;
            dgvView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvView.MultiSelect = false;

            // Configurar colunas do dgvXML
            DataGridViewTextBoxColumn colNumeroXML = new DataGridViewTextBoxColumn();
            colNumeroXML.HeaderText = "Número";
            colNumeroXML.DataPropertyName = "NumeroNFCe";
            colNumeroXML.Width = 80;
            dgvXML.Columns.Add(colNumeroXML);

            DataGridViewTextBoxColumn colSerieXML = new DataGridViewTextBoxColumn();
            colSerieXML.HeaderText = "Série";
            colSerieXML.DataPropertyName = "Serie";
            colSerieXML.Width = 50;
            dgvXML.Columns.Add(colSerieXML);

            DataGridViewTextBoxColumn colDataXML = new DataGridViewTextBoxColumn();
            colDataXML.HeaderText = "Data";
            colDataXML.DataPropertyName = "DataEmissao";
            colDataXML.Width = 90;
            colDataXML.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvXML.Columns.Add(colDataXML);

            DataGridViewTextBoxColumn colValorXML = new DataGridViewTextBoxColumn();
            colValorXML.HeaderText = "Valor";
            colValorXML.DataPropertyName = "ValorTotal";
            colValorXML.Width = 90;
            colValorXML.DefaultCellStyle.Format = "C2";
            colValorXML.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvXML.Columns.Add(colValorXML);

            DataGridViewTextBoxColumn colCFOPXML = new DataGridViewTextBoxColumn();
            colCFOPXML.HeaderText = "CFOP";
            colCFOPXML.DataPropertyName = "CFOP";
            colCFOPXML.Width = 60;
            dgvXML.Columns.Add(colCFOPXML);

            DataGridViewTextBoxColumn colStatusXML = new DataGridViewTextBoxColumn();
            colStatusXML.HeaderText = "Status";
            colStatusXML.DataPropertyName = "Status";
            colStatusXML.Width = 250;
            dgvXML.Columns.Add(colStatusXML);

            // Configurar colunas do dgvView
            DataGridViewTextBoxColumn colNumeroView = new DataGridViewTextBoxColumn();
            colNumeroView.HeaderText = "Número";
            colNumeroView.DataPropertyName = "NumeroNFCe";
            colNumeroView.Width = 80;
            dgvView.Columns.Add(colNumeroView);

            DataGridViewTextBoxColumn colSerieView = new DataGridViewTextBoxColumn();
            colSerieView.HeaderText = "Série";
            colSerieView.DataPropertyName = "Serie";
            colSerieView.Width = 50;
            dgvView.Columns.Add(colSerieView);

            DataGridViewTextBoxColumn colDataView = new DataGridViewTextBoxColumn();
            colDataView.HeaderText = "Data";
            colDataView.DataPropertyName = "DataEmissao";
            colDataView.Width = 90;
            colDataView.DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvView.Columns.Add(colDataView);

            DataGridViewTextBoxColumn colValorView = new DataGridViewTextBoxColumn();
            colValorView.HeaderText = "Valor";
            colValorView.DataPropertyName = "ValorTotal";
            colValorView.Width = 90;
            colValorView.DefaultCellStyle.Format = "C2";
            colValorView.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvView.Columns.Add(colValorView);

            DataGridViewTextBoxColumn colCFOPView = new DataGridViewTextBoxColumn();
            colCFOPView.HeaderText = "CFOP";
            colCFOPView.DataPropertyName = "CFOP";
            colCFOPView.Width = 60;
            dgvView.Columns.Add(colCFOPView);

            DataGridViewTextBoxColumn colStatusView = new DataGridViewTextBoxColumn();
            colStatusView.HeaderText = "Status";
            colStatusView.DataPropertyName = "StatusNaView";
            colStatusView.Width = 250;
            dgvView.Columns.Add(colStatusView);
        }

        private void DgvNotas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                NFCeInfo nota = dgvXML.Rows[e.RowIndex].DataBoundItem as NFCeInfo;
                if (nota != null)
                {
                    MostrarDetalhesValidacao(nota);
                }
            }
        }
        private void CarregarEmpresas()
        {
            try
            {
                // Inicializar o repository com a connection string
                string connectionString = DatabaseConfig.GetConnectionString();
                if (string.IsNullOrEmpty(connectionString))
                {
                    MessageBox.Show("Configure a conexão com o banco de dados primeiro!", "Atenção",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _repository = new NFCeRepository(connectionString);

                List<string> empresas = _repository.GetEmpresas();

                // Limpa qualquer item anterior no ComboBox
                cmbLoja.Items.Clear();

                // Adiciona cada empresa ao ComboBox
                foreach (var empresa in empresas)
                {
                    cmbLoja.Items.Add(empresa);
                }

                // Opcional: Seleciona o primeiro item, se houver
                if (cmbLoja.Items.Count > 0)
                {
                    cmbLoja.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar empresas: {ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarDetalhesValidacao(NFCeInfo nota)
        {
            System.Text.StringBuilder detalhes = new System.Text.StringBuilder();

            detalhes.AppendLine("═══════════════════════════════════════");
            detalhes.AppendLine($"  DETALHES DA VALIDAÇÃO - NFCe {nota.NumeroNFCe}");
            detalhes.AppendLine("═══════════════════════════════════════");
            detalhes.AppendLine();

            detalhes.AppendLine($"Status: {nota.Status}");
            detalhes.AppendLine();

            detalhes.AppendLine("DADOS DO XML:");
            detalhes.AppendLine($"  Chave: {nota.ChaveAcesso}");
            detalhes.AppendLine($"  Série: {nota.Serie}");
            detalhes.AppendLine($"  Número: {nota.NumeroNFCe}");
            detalhes.AppendLine($"  Valor: R$ {nota.ValorTotal:F2}");
            detalhes.AppendLine($"  Data: {(nota.DataEmissao.HasValue ? nota.DataEmissao.Value.ToString("dd/MM/yyyy") : "N/A")}");
            detalhes.AppendLine($"  CFOP: {nota.CFOP}");
            detalhes.AppendLine($"  Documento: {nota.DocumentoDestinatario} ({nota.TipoDocumento})");
            detalhes.AppendLine();

            if (nota.ExisteNaView)
            {
                detalhes.AppendLine("DADOS DO SISTEMA:");
                detalhes.AppendLine($"  Valor: R$ {(nota.ValorNaView.HasValue ? nota.ValorNaView.Value.ToString("F2") : "N/A")}");
                detalhes.AppendLine($"  Data: {(nota.DataNaView.HasValue ? nota.DataNaView.Value.ToString("dd/MM/yyyy") : "N/A")}");
                detalhes.AppendLine($"  CFOP: {nota.CFOPNaView}");
                detalhes.AppendLine($"  Documento: {nota.DocumentoNaView}");
                detalhes.AppendLine($"  Status: {nota.StatusNaView}");
                detalhes.AppendLine();
            }

            detalhes.AppendLine("RESULTADO DAS VALIDAÇÕES:");
            detalhes.AppendLine(nota.DetalhesValidacao);

            MessageBox.Show(detalhes.ToString(), "Detalhes da Validação",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSelecionarPasta_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Selecione a pasta contendo os arquivos XML das NFCe";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    txtCaminhoPasta.Text = folderDialog.SelectedPath;
                    CarregarXmls();
                }
            }
        }

        private void CarregarXmls()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                _listaNotas = _xmlProcessor.ProcessarPastaXml(txtCaminhoPasta.Text);

                dgvXML.DataSource = null;
                dgvXML.DataSource = _listaNotas;

                AtualizarTotalizadores();

                MessageBox.Show($"{_listaNotas.Count} arquivo(s) XML carregado(s) com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnValidar.Enabled = _listaNotas.Count > 0 && _listaView.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar XMLs: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            if (_listaNotas.Count == 0)
            {
                MessageBox.Show("Carregue os arquivos XML primeiro!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseConfig.IsConfigured())
            {
                MessageBox.Show("Configure a conexão com o banco de dados primeiro!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AbrirConfiguracoes();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                string connectionString = DatabaseConfig.GetConnectionString();
                _repository = new NFCeRepository(connectionString);

                int notasOK = 0;
                int notasComAlerta = 0;
                int notasComErro = 0;

                foreach (NFCeInfo nota in _listaNotas)
                {
                    try
                    {
                        // Obter período selecionado
                        DateTime dataInicial = dtpDataInicial.Value.Date;
                        DateTime dataFinal = dtpDataFinal.Value.Date;

                        // Buscar dados completos da view com filtro de período
                        NFCeInfo dadosView = _repository.GetDadosNFCeCompleto(nota.NumeroNFCe, txtNomeView.Text, dataInicial, dataFinal, cmbLoja.Text);

                        if (dadosView != null)
                        {
                            nota.ExisteNaView = true;
                            nota.ValorNaView = dadosView.ValorNaView;
                            nota.DataNaView = dadosView.DataNaView;
                            nota.CFOPNaView = dadosView.CFOPNaView;
                            nota.DocumentoNaView = dadosView.DocumentoNaView;
                            nota.StatusNaView = dadosView.StatusNaView;

                            // ===== VALIDAÇÃO 1: VALOR =====
                            if (nota.ValorNaView.HasValue)
                            {
                                decimal diferenca = Math.Abs(nota.ValorTotal - nota.ValorNaView.Value);
                                nota.ValorDivergente = diferenca >= 0.01m;
                            }

                            // ===== VALIDAÇÃO 2: DATA =====
                            if (nota.DataEmissao.HasValue && nota.DataNaView.HasValue)
                            {
                                TimeSpan diferencaDias = nota.DataEmissao.Value.Date - nota.DataNaView.Value.Date;
                                nota.DataDivergente = Math.Abs(diferencaDias.TotalDays) > 1;
                            }

                            // ===== VALIDAÇÃO 3: CFOP =====
                            if (!string.IsNullOrEmpty(nota.CFOP) && !string.IsNullOrEmpty(nota.CFOPNaView))
                            {
                                nota.CFOPDivergente = nota.CFOP != nota.CFOPNaView;
                            }

                            // ===== VALIDAÇÃO 4: DOCUMENTO DESTINATÁRIO =====
                            if (!string.IsNullOrEmpty(nota.DocumentoDestinatario) && !string.IsNullOrEmpty(nota.DocumentoNaView))
                            {
                                string docXml = nota.DocumentoDestinatario.Replace(".", "").Replace("-", "").Replace("/", "");
                                string docView = nota.DocumentoNaView.Replace(".", "").Replace("-", "").Replace("/", "");
                                nota.DocumentoDivergente = docXml != docView;
                            }

                            // ===== VALIDAÇÃO 5: STATUS (Cancelada/Inutilizada) =====
                            if (!string.IsNullOrEmpty(nota.StatusNaView))
                            {
                                nota.StatusIncorreto = (nota.StatusNaView.ToUpper() == "C" || nota.StatusNaView.ToUpper() == "I");
                            }

                            // ===== VALIDAÇÃO EXTRA: DUPLICIDADE =====
                            if (!string.IsNullOrEmpty(nota.ChaveAcesso))
                            {
                                nota.ChaveDuplicada = _repository.ExisteChaveAcessoDuplicada(
                                    nota.ChaveAcesso, nota.NumeroNFCe, txtNomeView.Text);
                            }

                            // Montar status e detalhes
                            MontarStatusValidacao(nota);
                        }
                        else
                        {
                            // Nota não encontrada
                            nota.ExisteNaView = false;
                            nota.Status = "✗ NÃO ENCONTRADA";
                            nota.DetalhesValidacao = "NFCe não localizada no sistema";
                            notasComErro++;
                        }
                    }
                    catch (Exception ex)
                    {
                        nota.Status = "ERRO";
                        nota.DetalhesValidacao = ex.Message;
                        notasComErro++;
                    }

                    // Contar por gravidade
                    int gravidade = nota.GetNivelGravidade();
                    if (gravidade == 0)
                        notasOK++;
                    else if (gravidade == 1)
                        notasComAlerta++;
                    else if (gravidade == 2 && nota.ExisteNaView)
                        notasComErro++;
                }

                // Atualizar o grid
                dgvXML.DataSource = null;
                dgvXML.DataSource = _listaNotas;

                // Colorir linhas
                ColorirLinhas();

                // Montar mensagem detalhada
                string mensagem = MontarMensagemResultado(notasOK, notasComAlerta, notasComErro);

                MessageBox.Show(mensagem, "Resultado da Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao validar: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void MontarStatusValidacao(NFCeInfo nota)
        {
            System.Text.StringBuilder status = new System.Text.StringBuilder();
            System.Text.StringBuilder detalhes = new System.Text.StringBuilder();

            // Verificar erros críticos primeiro
            if (nota.ChaveDuplicada)
            {
                status.Append("❌ CHAVE DUPLICADA");
                detalhes.AppendLine("• Chave de acesso duplicada no sistema");
            }
            else if (nota.StatusIncorreto)
            {
                status.Append("❌ CANCELADA/INUTILIZADA");
                detalhes.AppendLine($"• Nota com status: {nota.StatusNaView}");
            }
            else
            {
                // Verificar divergências
                bool temDivergencia = false;

                if (nota.ValorDivergente)
                {
                    temDivergencia = true;
                    detalhes.AppendLine($"• Valor divergente: XML=R$ {nota.ValorTotal:F2} | Sistema=R$ {nota.ValorNaView:F2}");
                }

                if (nota.CFOPDivergente)
                {
                    temDivergencia = true;
                    detalhes.AppendLine($"• CFOP divergente: XML={nota.CFOP} | Sistema={nota.CFOPNaView}");
                }

                if (nota.DataDivergente)
                {
                    temDivergencia = true;
                    detalhes.AppendLine($"• Data divergente: XML={nota.DataEmissao:dd/MM/yyyy} | Sistema={nota.DataNaView:dd/MM/yyyy}");
                }

                if (nota.DocumentoDivergente)
                {
                    temDivergencia = true;
                    detalhes.AppendLine($"• Documento divergente: XML={nota.DocumentoDestinatario} | Sistema={nota.DocumentoNaView}");
                }

                if (temDivergencia)
                {
                    status.Append("⚠ DIVERGÊNCIAS");
                }
                else
                {
                    status.Append("✓ OK");
                    detalhes.AppendLine("• Todas as validações passaram com sucesso");
                }
            }

            nota.Status = status.ToString();
            nota.DetalhesValidacao = detalhes.ToString().Trim();
        }

        private string MontarMensagemResultado(int ok, int alertas, int erros)
        {
            int total = _listaNotas.Count;
            double percOK = total > 0 ? (ok * 100.0 / total) : 0;
            double percAlertas = total > 0 ? (alertas * 100.0 / total) : 0;
            double percErros = total > 0 ? (erros * 100.0 / total) : 0;

            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            msg.AppendLine("VALIDAÇÃO CONCLUÍDA!");
            msg.AppendLine();
            msg.AppendLine($"Total de notas: {total}");
            msg.AppendLine();
            msg.AppendLine($"✓ OK: {ok} ({percOK:F1}%)");
            msg.AppendLine($"⚠ Com Alertas: {alertas} ({percAlertas:F1}%)");
            msg.AppendLine($"❌ Com Erros: {erros} ({percErros:F1}%)");

            // Contar tipos de problemas
            int valorDiv = _listaNotas.FindAll(n => n.ValorDivergente).Count;
            int cfopDiv = _listaNotas.FindAll(n => n.CFOPDivergente).Count;
            int dataDiv = _listaNotas.FindAll(n => n.DataDivergente).Count;
            int docDiv = _listaNotas.FindAll(n => n.DocumentoDivergente).Count;
            int statusInc = _listaNotas.FindAll(n => n.StatusIncorreto).Count;
            int chaveDup = _listaNotas.FindAll(n => n.ChaveDuplicada).Count;
            int naoEnc = _listaNotas.FindAll(n => !n.ExisteNaView).Count;

            if (alertas > 0 || erros > 0)
            {
                msg.AppendLine();
                msg.AppendLine("DETALHAMENTO DOS PROBLEMAS:");
                if (valorDiv > 0) msg.AppendLine($"  • Valor divergente: {valorDiv}");
                if (cfopDiv > 0) msg.AppendLine($"  • CFOP divergente: {cfopDiv}");
                if (dataDiv > 0) msg.AppendLine($"  • Data divergente: {dataDiv}");
                if (docDiv > 0) msg.AppendLine($"  • Documento divergente: {docDiv}");
                if (statusInc > 0) msg.AppendLine($"  • Status incorreto (Canc/Inut): {statusInc}");
                if (chaveDup > 0) msg.AppendLine($"  • Chave duplicada: {chaveDup}");
                if (naoEnc > 0) msg.AppendLine($"  • Não encontradas: {naoEnc}");
            }

            return msg.ToString();
        }

        private void ColorirLinhas()
        {
            foreach (DataGridViewRow row in dgvXML.Rows)
            {
                if (row.DataBoundItem is NFCeInfo nota)
                {
                    int gravidade = nota.GetNivelGravidade();

                    if (gravidade == 2) // Erro
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        row.DefaultCellStyle.Font = new System.Drawing.Font(dgvXML.Font, System.Drawing.FontStyle.Bold);
                    }
                    else if (gravidade == 1) // Alerta
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else // OK
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.DefaultCellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }
        }

        private void AtualizarTotalizadores()
        {
            decimal valorTotal = 0;
            int quantidadeNotas = _listaNotas.Count;

            foreach (NFCeInfo nota in _listaNotas)
            {
                valorTotal += nota.ValorTotal;
            }

            lblQuantidade.Text = $"Quantidade de Notas: {quantidadeNotas}";
            lblValorTotal.Text = $"Valor Total: {valorTotal:C2}";
        }

        

        private void AtualizarTotalizadoresView()
        {
            decimal valorTotal = 0;
            int quantidadeNotas = _listaView.Count;

            foreach (NFCeInfo nota in _listaView)
            {
                valorTotal += nota.ValorTotal;
            }

            lblQuantidadeView.Text = $"Quantidade de Notas: {quantidadeNotas}";
            lblValorTotalView.Text = $"Valor Total: {valorTotal:C2}";
        }

        private void btnCarregarView_Click(object sender, EventArgs e)
        {
            if (!DatabaseConfig.IsConfigured())
            {
                MessageBox.Show("Configure a conexão com o banco de dados primeiro!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                AbrirConfiguracoes();
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;

                string connectionString = DatabaseConfig.GetConnectionString();
                _repository = new NFCeRepository(connectionString);

                DateTime dataInicial = dtpDataInicial.Value.Date;
                DateTime dataFinal = dtpDataFinal.Value.Date;

                // Carregar todos os registros da view no período
                _listaView = _repository.GetTodasNFCesDoPeriodo(txtNomeView.Text, dataInicial, dataFinal,cmbLoja.Text);

                dgvView.DataSource = null;
                dgvView.DataSource = _listaView;

                // Atualizar totalizadores
                AtualizarTotalizadoresView();

                MessageBox.Show($"{_listaView.Count} registro(s) carregado(s) da view!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnValidar.Enabled = _listaNotas.Count > 0 && _listaView.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados da view:\n\n" + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void btnConfigurar_Click(object sender, EventArgs e)
        {
            AbrirConfiguracoes();
        }

        private void AbrirConfiguracoes()
        {
            ConfigForm configForm = new ConfigForm();
            configForm.ShowDialog();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            _listaNotas.Clear();
            _listaView.Clear();
            dgvXML.DataSource = null;
            dgvView.DataSource = null;
            txtCaminhoPasta.Text = string.Empty;
            lblQuantidade.Text = "Quantidade de Notas: 0";
            lblValorTotal.Text = "Valor Total: R$ 0,00";
            lblQuantidadeView.Text = "Quantidade de Notas: 0";
            lblValorTotalView.Text = "Valor Total: R$ 0,00";
            btnValidar.Enabled = false;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (_listaNotas.Count == 0)
            {
                MessageBox.Show("Não há dados para exportar!", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Arquivo CSV (*.csv)|*.csv";
                saveDialog.FileName = $"NFCe_Validacao_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ExportarParaCsv(saveDialog.FileName);
                        MessageBox.Show("Dados exportados com sucesso!", "Sucesso",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao exportar: " + ex.Message, "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportarParaCsv(string caminhoArquivo)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(caminhoArquivo))
            {
                writer.WriteLine("Chave de Acesso;Série;Número NFCe;Valor Total;Status");

                foreach (NFCeInfo nota in _listaNotas)
                {
                    writer.WriteLine($"{nota.ChaveAcesso};{nota.Serie};{nota.NumeroNFCe};{nota.ValorTotal};{nota.Status}");
                }
            }
        }


    }
}