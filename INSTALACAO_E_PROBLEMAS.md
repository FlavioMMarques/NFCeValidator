# Guia de Instalação e Solução de Problemas

## Requisitos do Sistema

### Hardware Mínimo
- Processador: 1 GHz ou superior
- Memória RAM: 2 GB
- Espaço em disco: 100 MB

### Software Necessário
- Windows 7 SP1 ou superior (recomendado: Windows 10/11)
- .NET Framework 4.7.2 ou superior
- SQL Server 2014 ou superior (Express Edition funciona)

## Instalação

### 1. Verificar o .NET Framework

Abra o Prompt de Comando e execute:
```cmd
reg query "HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full" /v Release
```

Se o valor for menor que 461808, baixe o .NET Framework 4.7.2:
https://dotnet.microsoft.com/download/dotnet-framework/net472

### 2. Instalação do Sistema

1. Copie o executável `NFCeValidator.exe` para uma pasta no computador
2. Na primeira execução, clique em "Configurar Banco"
3. Configure a string de conexão
4. Teste a conexão antes de salvar

### 3. Configuração do SQL Server

#### Habilitar TCP/IP (se necessário)

1. Abra o SQL Server Configuration Manager
2. Expanda "SQL Server Network Configuration"
3. Clique em "Protocols for SQLEXPRESS" (ou seu instance)
4. Clique com botão direito em "TCP/IP" → Enable
5. Reinicie o serviço do SQL Server

#### Criar Usuário para a Aplicação (Recomendado)

```sql
-- Criar login
CREATE LOGIN app_nfce WITH PASSWORD = 'SenhaSegura123!';

-- Usar o banco de dados
USE [SeuBancoDeDados];

-- Criar usuário
CREATE USER app_nfce FOR LOGIN app_nfce;

-- Conceder permissões apenas na view
GRANT SELECT ON vw_NFCe TO app_nfce;
```

Configuração na aplicação:
```
Servidor\Instância: SERVIDOR\SQLEXPRESS
Porta: 5433 (ou 1433)
Banco de Dados: SeuBanco
Usuário: app_nfce
Senha: SenhaSegura123!
Timeout: 120
```

## Solução de Problemas Comuns

### Erro: "Não foi possível conectar ao banco de dados"

**Possíveis causas:**

1. **SQL Server não está rodando**
   - Solução: Abra Services.msc e inicie o serviço "SQL Server (SQLEXPRESS)"

2. **Firewall bloqueando**
   - Solução: Adicione exceção para a porta 1433 no Windows Firewall

3. **TCP/IP desabilitado**
   - Solução: Siga as instruções na seção "Habilitar TCP/IP"

4. **Credenciais inválidas**
   - Solução: Verifique usuário e senha na connection string

5. **Named Pipes não configurado**
   - Solução: Use `(local)\SQLEXPRESS` ou `localhost\SQLEXPRESS` ao invés do nome do servidor

### Erro: "View não encontrada"

**Verificar se a view existe:**

```sql
SELECT * FROM sys.views WHERE name = 'vw_NFCe';
```

**Criar a view:**
- Use o script fornecido em `SQL_Scripts/exemplo_view_nfce.sql`
- Ajuste conforme sua estrutura de dados

### Erro: "Invalid object name 'vw_NFCe'"

**Causas:**
1. View não existe no banco de dados especificado
2. Usuário não tem permissão de SELECT na view
3. View está em outro schema

**Solução:**

```sql
-- Verificar schema da view
SELECT SCHEMA_NAME(schema_id) AS SchemaName, name AS ViewName
FROM sys.views
WHERE name = 'vw_NFCe';

-- Se a view está em outro schema, use o nome completo:
-- dbo.vw_NFCe ou NomeDoSchema.vw_NFCe
```

No campo "Nome da View" da aplicação, coloque: `dbo.vw_NFCe`

### XMLs não estão sendo lidos corretamente

**Verificações:**

1. **Formato do XML**
   - Certifique-se que os XMLs estão no padrão da Receita Federal
   - Use o arquivo de exemplo em `Exemplos_XML/exemplo_nfce.xml` para referência

2. **Encoding do arquivo**
   - XMLs devem estar em UTF-8
   - Verifique se não há caracteres especiais corrompidos

3. **Estrutura mínima necessária:**
```xml
<infNFe Id="NFe43210512345678...">  <!-- Chave de Acesso -->
  <ide>
    <nNF>123456</nNF>                 <!-- Número da NFCe -->
  </ide>
  <total>
    <ICMSTot>
      <vNF>150.00</vNF>               <!-- Valor Total -->
    </ICMSTot>
  </total>
</infNFe>
```

### Erro: "Could not load file or assembly"

**Causa:** Falta alguma DLL necessária

**Solução:**
1. Certifique-se que o .NET Framework 4.7.2 está instalado
2. Copie todas as DLLs da pasta bin junto com o executável

### Performance Lenta com Muitos XMLs

**Otimizações:**

1. **No SQL Server:**
```sql
-- Criar índice na coluna de busca
CREATE INDEX IX_NFCe_Numero ON vw_NFCe(NumeroNFCe);

-- Atualizar estatísticas
UPDATE STATISTICS vw_NFCe;
```

2. **Na Aplicação:**
   - Processe XMLs em lotes menores
   - Feche outras aplicações pesadas durante o processamento

### Erro de Permissão ao Salvar Configuração

**Causa:** Aplicação não tem permissão para modificar o App.config

**Solução:**
1. Execute a aplicação como Administrador (botão direito → "Executar como administrador")
2. Ou mova a aplicação para uma pasta com permissões de escrita (ex: C:\NFCeValidator)

## Boas Práticas

### Segurança

1. **Não use SA (System Administrator)**
   - Crie um usuário específico com permissões limitadas

2. **Use senhas fortes**
   - Mínimo 8 caracteres
   - Misture letras, números e símbolos

3. **Restrinja permissões**
   - Conceda apenas SELECT na view necessária

### Backup

1. **Faça backup da configuração**
   - Copie o arquivo `NFCeValidator.exe.config` para um local seguro

2. **Documente sua connection string**
   - Mantenha um registro seguro das configurações

### Performance

1. **Mantenha índices atualizados**
```sql
-- Verificar fragmentação
SELECT 
    object_name(ips.object_id) AS TableName,
    ips.index_id,
    ips.avg_fragmentation_in_percent
FROM 
    sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ips
WHERE 
    ips.avg_fragmentation_in_percent > 30;

-- Reorganizar índices fragmentados
ALTER INDEX ALL ON vw_NFCe REORGANIZE;
```

2. **Limite o tamanho da view**
   - Use filtros de data para não retornar registros muito antigos
   - Exemplo: `WHERE DataEmissao >= DATEADD(YEAR, -2, GETDATE())`

## Logs e Diagnóstico

### Verificar Logs do SQL Server

```sql
-- Últimos erros no SQL Server
EXEC sp_readerrorlog 0, 1, 'failed';

-- Logins recentes
SELECT 
    login_name,
    program_name,
    client_interface_name,
    login_time
FROM 
    sys.dm_exec_sessions
WHERE 
    login_name = 'app_nfce'
ORDER BY 
    login_time DESC;
```

### Modo Debug

Para desenvolvedores:

1. Abra o projeto no Visual Studio
2. Coloque breakpoints no código
3. Execute com F5
4. Acompanhe a execução passo a passo

## Contato e Suporte

Para reportar bugs ou solicitar melhorias:
1. Documente o erro com prints
2. Anote a mensagem de erro completa
3. Informe versão do Windows e SQL Server
4. Descreva os passos para reproduzir o problema

## Checklist de Instalação

- [ ] .NET Framework 4.7.2 instalado
- [ ] SQL Server rodando
- [ ] View criada no banco de dados
- [ ] Connection string configurada
- [ ] Conexão testada com sucesso
- [ ] XML de teste validado
- [ ] Permissões concedidas ao usuário

## Atualizações

### Versão 1.0.0 (Novembro 2025)
- Lançamento inicial
- Leitura de XMLs de NFCe
- Validação com SQL Server
- Exportação para CSV
- Interface gráfica com Windows Forms
