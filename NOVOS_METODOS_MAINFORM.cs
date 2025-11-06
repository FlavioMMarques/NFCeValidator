// ===== ADICIONAR ESTES MÉTODOS NO MainForm.cs =====

// Método para carregar dados da view
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
        _listaView = _repository.GetTodasNFCesDoPeriodo(txtNomeView.Text, dataInicial, dataFinal);
        
        dgvView.DataSource = null;
        dgvView.DataSource = _listaView;
        
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

// Método atualizado de validação com comparação
private void btnValidar_Click(object sender, EventArgs e)
{
    if (_listaNotas.Count == 0)
    {
        MessageBox.Show("Carregue os arquivos XML primeiro!", "Atenção",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    if (_listaView.Count == 0)
    {
        MessageBox.Show("Carregue os dados da view primeiro!", "Atenção",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    try
    {
        Cursor = Cursors.WaitCursor;
        
        int notasOK = 0;
        int notasComAlerta = 0;
        int notasComErro = 0;

        // Comparar cada XML com a View
        foreach (NFCeInfo notaXML in _listaNotas)
        {
            // Buscar nota correspondente na view
            NFCeInfo notaView = _listaView.FirstOrDefault(v => 
                v.NumeroNFCe == notaXML.NumeroNFCe && 
                v.Serie == notaXML.Serie);
            
            if (notaView != null)
            {
                // Nota encontrada - comparar dados
                notaXML.ExisteNaView = true;
                notaXML.ValorNaView = notaView.ValorTotal;
                notaXML.DataNaView = notaView.DataEmissao;
                notaXML.CFOPNaView = notaView.CFOP;
                notaXML.DocumentoNaView = notaView.DocumentoDestinatario;
                notaXML.StatusNaView = notaView.StatusNaView;
                
                // ===== VALIDAÇÃO 1: VALOR =====
                if (notaXML.ValorNaView.HasValue)
                {
                    decimal diferenca = Math.Abs(notaXML.ValorTotal - notaXML.ValorNaView.Value);
                    notaXML.ValorDivergente = diferenca >= 0.01m;
                }
                
                // ===== VALIDAÇÃO 2: DATA =====
                if (notaXML.DataEmissao.HasValue && notaXML.DataNaView.HasValue)
                {
                    TimeSpan diferencaDias = notaXML.DataEmissao.Value.Date - notaXML.DataNaView.Value.Date;
                    notaXML.DataDivergente = Math.Abs(diferencaDias.TotalDays) > 1;
                }
                
                // ===== VALIDAÇÃO 3: CFOP =====
                if (!string.IsNullOrEmpty(notaXML.CFOP) && !string.IsNullOrEmpty(notaXML.CFOPNaView))
                {
                    notaXML.CFOPDivergente = notaXML.CFOP != notaXML.CFOPNaView;
                }
                
                // ===== VALIDAÇÃO 4: DOCUMENTO DESTINATÁRIO =====
                if (!string.IsNullOrEmpty(notaXML.DocumentoDestinatario) && !string.IsNullOrEmpty(notaXML.DocumentoNaView))
                {
                    string docXml = notaXML.DocumentoDestinatario.Replace(".", "").Replace("-", "").Replace("/", "");
                    string docView = notaXML.DocumentoNaView.Replace(".", "").Replace("-", "").Replace("/", "");
                    notaXML.DocumentoDivergente = docXml != docView;
                }
                
                // ===== VALIDAÇÃO 5: STATUS (Cancelada/Inutilizada) =====
                if (!string.IsNullOrEmpty(notaXML.StatusNaView))
                {
                    notaXML.StatusIncorreto = (notaXML.StatusNaView.ToUpper() == "C" || notaXML.StatusNaView.ToUpper() == "I");
                }
                
                // Montar status e detalhes
                MontarStatusValidacao(notaXML);
            }
            else
            {
                // Nota não encontrada na view
                notaXML.ExisteNaView = false;
                notaXML.Status = "❌ NÃO ENCONTRADA NA VIEW";
                notaXML.DetalhesValidacao = "NFCe não localizada nos dados da view do período selecionado";
                notasComErro++;
            }
        }

        // Verificar notas que estão na view mas não nos XMLs
        List<NFCeInfo> notasApenasDaView = _listaView.Where(v => 
            !_listaNotas.Any(x => x.NumeroNFCe == v.NumeroNFCe && x.Serie == v.Serie)).ToList();
        
        // Atualizar visualizações
        dgvXML.DataSource = null;
        dgvXML.DataSource = _listaNotas;
        
        // Calcular totais
        notasOK = _listaNotas.Count(n => n.Status != null && n.Status.Contains("✓"));
        notasComAlerta = _listaNotas.Count(n => n.Status != null && n.Status.Contains("⚠"));
        notasComErro = _listaNotas.Count(n => n.Status != null && n.Status.Contains("❌"));

        // Mostrar resumo
        StringBuilder resumo = new StringBuilder();
        resumo.AppendLine("═════════════════════════════════════");
        resumo.AppendLine("     RESULTADO DA COMPARAÇÃO");
        resumo.AppendLine("═════════════════════════════════════");
        resumo.AppendLine();
        resumo.AppendLine("📄 XMLs:");
        resumo.AppendLine($"   Total: {_listaNotas.Count}");
        resumo.AppendLine($"   ✓ OK: {notasOK}");
        resumo.AppendLine($"   ⚠ Alertas: {notasComAlerta}");
        resumo.AppendLine($"   ❌ Erros: {notasComErro}");
        resumo.AppendLine();
        resumo.AppendLine("💾 View:");
        resumo.AppendLine($"   Total: {_listaView.Count}");
        resumo.AppendLine($"   Apenas na View: {notasApenasDaView.Count}");
        resumo.AppendLine();
        
        if (notasApenasDaView.Count > 0)
        {
            resumo.AppendLine("⚠ ATENÇÃO: Há notas na view que não têm XML correspondente!");
            resumo.AppendLine($"   Quantidade: {notasApenasDaView.Count}");
            resumo.AppendLine($"   Primeiras: {string.Join(", ", notasApenasDaView.Take(5).Select(n => n.NumeroNFCe))}");
            if (notasApenasDaView.Count > 5)
                resumo.AppendLine($"   ... e mais {notasApenasDaView.Count - 5}");
        }
        
        MessageBox.Show(resumo.ToString(), "Comparação Concluída",
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

// Método auxiliar para montar status (já existe, mas replicando aqui)
private void MontarStatusValidacao(NFCeInfo nota)
{
    List<string> problemas = new List<string>();
    List<string> detalhes = new List<string>();
    
    if (nota.ValorDivergente)
    {
        problemas.Add("Valor divergente");
        detalhes.Add($"⚠ VALOR: XML=R$ {nota.ValorTotal:F2} | View=R$ {nota.ValorNaView:F2}");
    }
    
    if (nota.DataDivergente)
    {
        problemas.Add("Data divergente");
        detalhes.Add($"⚠ DATA: XML={nota.DataEmissao?.ToString("dd/MM/yyyy")} | View={nota.DataNaView?.ToString("dd/MM/yyyy")}");
    }
    
    if (nota.CFOPDivergente)
    {
        problemas.Add("CFOP divergente");
        detalhes.Add($"⚠ CFOP: XML={nota.CFOP} | View={nota.CFOPNaView}");
    }
    
    if (nota.DocumentoDivergente)
    {
        problemas.Add("Documento divergente");
        detalhes.Add($"⚠ DOCUMENTO: XML={nota.DocumentoDestinatario} | View={nota.DocumentoNaView}");
    }
    
    if (nota.StatusIncorreto)
    {
        problemas.Add("Nota cancelada/inutilizada");
        detalhes.Add($"❌ STATUS: Nota está como '{nota.StatusNaView}' no sistema");
    }
    
    if (nota.ChaveDuplicada)
    {
        problemas.Add("Chave duplicada");
        detalhes.Add("❌ DUPLICIDADE: Mesma chave de acesso para números diferentes");
    }
    
    // Montar status final
    if (problemas.Count == 0)
    {
        nota.Status = "✓ OK - Dados conferem";
        nota.DetalhesValidacao = "Todos os dados conferem entre XML e sistema.";
    }
    else if (nota.StatusIncorreto || nota.ChaveDuplicada)
    {
        nota.Status = $"❌ ERRO - {string.Join(", ", problemas)}";
        nota.DetalhesValidacao = string.Join("\n", detalhes);
    }
    else
    {
        nota.Status = $"⚠ ALERTA - {string.Join(", ", problemas)}";
        nota.DetalhesValidacao = string.Join("\n", detalhes);
    }
}

// Atualizar método btnLimpar para limpar ambas as listas
private void btnLimpar_Click(object sender, EventArgs e)
{
    _listaNotas.Clear();
    _listaView.Clear();
    dgvXML.DataSource = null;
    dgvView.DataSource = null;
    txtCaminhoPasta.Text = "";
    lblQuantidade.Text = "Quantidade de Notas: 0";
    lblValorTotal.Text = "Valor Total: R$ 0,00";
    btnValidar.Enabled = false;
}
