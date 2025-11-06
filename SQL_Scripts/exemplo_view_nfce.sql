-- ========================================
-- Script de Exemplo - View NFCe
-- SQL Server 2014 ou superior
-- ========================================

-- IMPORTANTE: Este é um script de EXEMPLO
-- Ajuste conforme a estrutura real do seu banco de dados

-- ========================================
-- Exemplo 1: View Simples
-- ========================================
-- Caso você tenha uma tabela chamada NFCe com os campos básicos

CREATE VIEW vw_NFCe AS
SELECT 
    ChaveAcesso,
    NumeroNFCe,      -- CAMPO OBRIGATÓRIO para validação
    ValorTotal,
    DataEmissao,
    CNPJ,
    RazaoSocial,
    Status
FROM 
    TabelaNFCe
WHERE 
    Status = 'Aprovada';

GO

-- ========================================
-- Exemplo 2: View com JOIN de Múltiplas Tabelas
-- ========================================
-- Caso seus dados estejam distribuídos em várias tabelas

/*
CREATE VIEW vw_NFCe AS
SELECT 
    n.ChaveAcesso,
    n.NumeroNFCe,        -- CAMPO OBRIGATÓRIO
    n.ValorTotal,
    n.DataEmissao,
    c.CNPJ,
    c.RazaoSocial,
    n.Status,
    s.Serie,
    m.Modelo
FROM 
    NotasFiscais n
    INNER JOIN Clientes c ON n.ClienteID = c.ClienteID
    INNER JOIN Series s ON n.SerieID = s.SerieID
    INNER JOIN Modelos m ON n.ModeloID = m.ModeloID
WHERE 
    n.Status = 'Aprovada'
    AND m.Codigo = '65';  -- Modelo 65 = NFCe

GO
*/

-- ========================================
-- Exemplo 3: View com Filtros e Formatações
-- ========================================

/*
CREATE VIEW vw_NFCe AS
SELECT 
    REPLACE(ChaveAcesso, ' ', '') AS ChaveAcesso,
    LTRIM(RTRIM(NumeroNFCe)) AS NumeroNFCe,  -- CAMPO OBRIGATÓRIO
    CAST(ValorTotal AS DECIMAL(18,2)) AS ValorTotal,
    CONVERT(DATE, DataEmissao) AS DataEmissao,
    REPLACE(REPLACE(CNPJ, '.', ''), '/', '') AS CNPJ,
    RazaoSocial,
    Status,
    DataInclusao
FROM 
    TabelaNFCe
WHERE 
    Status IN ('Aprovada', 'Autorizada')
    AND DataEmissao >= DATEADD(YEAR, -1, GETDATE())  -- Apenas notas do último ano
    AND NumeroNFCe IS NOT NULL
    AND ValorTotal > 0;

GO
*/

-- ========================================
-- Testar a View
-- ========================================

-- Verificar se a view foi criada
SELECT * FROM sys.views WHERE name = 'vw_NFCe';

-- Consultar os dados da view
SELECT TOP 10 * FROM vw_NFCe ORDER BY NumeroNFCe DESC;

-- Contar registros
SELECT COUNT(*) AS TotalRegistros FROM vw_NFCe;

-- Testar consulta específica (igual ao que o sistema fará)
DECLARE @NumeroTeste VARCHAR(20) = '123456';
SELECT COUNT(*) FROM vw_NFCe WHERE NumeroNFCe = @NumeroTeste;

-- ========================================
-- Permissões (se necessário)
-- ========================================

-- Conceder permissão de SELECT para um usuário específico
-- GRANT SELECT ON vw_NFCe TO usuario_aplicacao;

-- ========================================
-- Remover a View (caso precise recriar)
-- ========================================

-- DROP VIEW IF EXISTS vw_NFCe;

-- ========================================
-- ESTRUTURA MÍNIMA NECESSÁRIA
-- ========================================

/*
A view precisa retornar OBRIGATORIAMENTE:
- NumeroNFCe (VARCHAR/NVARCHAR) - Usado para comparação

Campos opcionais (mas recomendados):
- ChaveAcesso
- ValorTotal
- DataEmissao
- CNPJ
- RazaoSocial
*/
