-- ========================================
-- Script SQL - View NFCe para Auditoria SPED
-- Com 5 Validações Críticas
-- ========================================

-- IMPORTANTE: Adapte conforme sua estrutura de banco de dados

-- ========================================
-- VIEW COMPLETA PARA VALIDAÇÃO
-- ========================================

CREATE VIEW vw_NFCe AS
SELECT 
    -- Identificação da Nota
    ChaveAcesso,
    Serie,
    NumeroNFCe,
    Modelo,  -- Deve ser 65 para NFCe
    
    -- VALIDAÇÃO 1: VALOR
    ValorTotal,
    
    -- VALIDAÇÃO 2: DATA
    DataEmissao,
    DataEntradaSaida,
    
    -- VALIDAÇÃO 3: CFOP
    CFOP,
    NaturezaOperacao,
    
    -- VALIDAÇÃO 4: DOCUMENTO DESTINATÁRIO
    CASE 
        WHEN CNPJDestinatario IS NOT NULL THEN CNPJDestinatario
        WHEN CPFDestinatario IS NOT NULL THEN CPFDestinatario
        ELSE NULL
    END AS DocumentoDestinatario,
    
    -- VALIDAÇÃO 5: STATUS
    Status,  -- 'A' = Ativa, 'C' = Cancelada, 'I' = Inutilizada
    
    -- Informações Adicionais (opcionais)
    NumeroProtocolo,
    DataAutorizacao,
    Ambiente,  -- 1 = Produção, 2 = Homologação
    NomeDestinatario
    
FROM 
    TabelaNotasFiscais
WHERE 
    Modelo = 65  -- Apenas NFCe
    AND Ambiente = 1  -- Apenas Produção
    AND Serie IS NOT NULL
    AND NumeroNFCe IS NOT NULL;

GO

-- ========================================
-- EXEMPLO ADAPTADO PARA ESTRUTURAS COMUNS
-- ========================================

/*
-- Caso 1: Dados em uma única tabela
CREATE VIEW vw_NFCe AS
SELECT 
    ChaveAcesso,
    Serie,
    NumeroNFCe,
    65 AS Modelo,
    ValorTotal,
    DataEmissao,
    DataEmissao AS DataEntradaSaida,
    CFOP,
    NaturezaOperacao,
    COALESCE(CNPJCliente, CPFCliente) AS DocumentoDestinatario,
    StatusNota AS Status,
    Protocolo AS NumeroProtocolo,
    DataAutorizacao,
    1 AS Ambiente,
    NomeCliente AS NomeDestinatario
FROM 
    NotasFiscais
WHERE 
    TipoNota = 'NFCe'
    AND Cancelada = 0;

GO
*/

/*
-- Caso 2: Dados em múltiplas tabelas (com JOINs)
CREATE VIEW vw_NFCe AS
SELECT 
    nf.ChaveAcesso,
    nf.Serie,
    nf.NumeroNFCe,
    65 AS Modelo,
    nf.ValorTotal,
    nf.DataEmissao,
    nf.DataEmissao AS DataEntradaSaida,
    op.CFOP,
    op.NaturezaOperacao,
    COALESCE(cl.CNPJ, cl.CPF) AS DocumentoDestinatario,
    nf.Status,
    nf.NumeroProtocolo,
    nf.DataAutorizacao,
    1 AS Ambiente,
    cl.Nome AS NomeDestinatario
FROM 
    NotasFiscais nf
    INNER JOIN Operacoes op ON nf.OperacaoID = op.ID
    LEFT JOIN Clientes cl ON nf.ClienteID = cl.ID
WHERE 
    nf.TipoDocumento = 'NFCe'
    AND nf.Cancelada = 0
    AND nf.Inutilizada = 0;

GO
*/

-- ========================================
-- ÍNDICES RECOMENDADOS PARA PERFORMANCE
-- ========================================

-- Índice no número da NFCe (campo mais consultado)
CREATE INDEX IX_NFCe_Numero 
ON TabelaNotasFiscais(NumeroNFCe);

-- Índice na chave de acesso (para detecção de duplicidade)
CREATE INDEX IX_NFCe_Chave 
ON TabelaNotasFiscais(ChaveAcesso);

-- Índice composto para melhor performance
CREATE INDEX IX_NFCe_Serie_Numero 
ON TabelaNotasFiscais(Serie, NumeroNFCe);

GO

-- ========================================
-- TESTES DA VIEW
-- ========================================

-- 1. Verificar se a view foi criada
SELECT * FROM sys.views WHERE name = 'vw_NFCe';

-- 2. Verificar estrutura da view
SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'vw_NFCe'
ORDER BY ORDINAL_POSITION;

-- 3. Testar consulta simples
SELECT TOP 10 
    NumeroNFCe,
    ValorTotal,
    DataEmissao,
    CFOP,
    DocumentoDestinatario,
    Status
FROM vw_NFCe
ORDER BY NumeroNFCe DESC;

-- 4. Contar registros
SELECT COUNT(*) AS TotalNFCe FROM vw_NFCe;

-- 5. Testar busca por número (simulando aplicação)
DECLARE @NumeroTeste VARCHAR(20) = '123456';
SELECT * FROM vw_NFCe WHERE NumeroNFCe = @NumeroTeste;

-- 6. Verificar status das notas
SELECT 
    Status,
    COUNT(*) AS Quantidade
FROM vw_NFCe
GROUP BY Status;

-- 7. Verificar CFOPs mais utilizados
SELECT 
    CFOP,
    COUNT(*) AS Quantidade,
    SUM(ValorTotal) AS ValorTotal
FROM vw_NFCe
GROUP BY CFOP
ORDER BY Quantidade DESC;

-- ========================================
-- CAMPOS OBRIGATÓRIOS
-- ========================================

/*
Para o sistema funcionar corretamente, a view DEVE retornar:

CAMPO                       TIPO           OBRIGATÓRIO  VALIDAÇÃO
-------------------------   ------------   -----------  ------------------
NumeroNFCe                  VARCHAR(20)    SIM          Identificação
ValorTotal                  DECIMAL(18,2)  SIM          Validação 1: Valor
DataEmissao                 DATETIME       SIM          Validação 2: Data
CFOP                        VARCHAR(4)     SIM          Validação 3: CFOP
DocumentoDestinatario       VARCHAR(18)    SIM          Validação 4: Doc
Status                      VARCHAR(1)     SIM          Validação 5: Status

CAMPOS OPCIONAIS (mas recomendados):
ChaveAcesso                 VARCHAR(44)    NÃO          Duplicidade
Serie                       VARCHAR(3)     NÃO          Identificação
NaturezaOperacao            VARCHAR(60)    NÃO          Informação
NumeroProtocolo             VARCHAR(15)    NÃO          Rastreio
DataAutorizacao             DATETIME       NÃO          Informação
NomeDestinatario            VARCHAR(60)    NÃO          Informação
*/

-- ========================================
-- VALORES VÁLIDOS PARA STATUS
-- ========================================

/*
O campo Status deve retornar:
- 'A' = Ativa (nota válida)
- 'C' = Cancelada (nota cancelada - não deve ir ao SPED)
- 'I' = Inutilizada (nota inutilizada - não deve ir ao SPED)

Exemplo de conversão:
CASE 
    WHEN Cancelada = 1 THEN 'C'
    WHEN Inutilizada = 1 THEN 'I'
    ELSE 'A'
END AS Status
*/

-- ========================================
-- PERMISSÕES
-- ========================================

-- Conceder SELECT ao usuário da aplicação
-- GRANT SELECT ON vw_NFCe TO usuario_aplicacao;

-- ========================================
-- REMOVER A VIEW (se precisar recriar)
-- ========================================

-- DROP VIEW IF EXISTS vw_NFCe;

-- ========================================
-- CONSULTAS ÚTEIS PARA ANÁLISE
-- ========================================

-- Notas com valor acima de R$ 1.000
SELECT * 
FROM vw_NFCe 
WHERE ValorTotal > 1000
ORDER BY ValorTotal DESC;

-- Notas emitidas hoje
SELECT * 
FROM vw_NFCe 
WHERE CAST(DataEmissao AS DATE) = CAST(GETDATE() AS DATE);

-- Notas por CFOP
SELECT 
    CFOP,
    COUNT(*) AS Qtd,
    SUM(ValorTotal) AS Total
FROM vw_NFCe
GROUP BY CFOP
ORDER BY Total DESC;

-- Notas canceladas (não devem ir ao SPED)
SELECT * 
FROM vw_NFCe 
WHERE Status = 'C';

-- Verificar duplicidade de chaves
SELECT 
    ChaveAcesso,
    COUNT(*) AS Vezes
FROM vw_NFCe
GROUP BY ChaveAcesso
HAVING COUNT(*) > 1;

-- ========================================
-- MANUTENÇÃO
-- ========================================

-- Atualizar estatísticas para melhor performance
UPDATE STATISTICS vw_NFCe;

-- Verificar fragmentação dos índices
SELECT 
    object_name(ips.object_id) AS TableName,
    ips.index_id,
    name AS IndexName,
    ips.avg_fragmentation_in_percent
FROM 
    sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ips
    INNER JOIN sys.indexes i ON ips.object_id = i.object_id 
        AND ips.index_id = i.index_id
WHERE 
    object_name(ips.object_id) LIKE '%NotasFiscais%'
ORDER BY 
    ips.avg_fragmentation_in_percent DESC;

-- ========================================
-- FIM DO SCRIPT
-- ========================================
