# Validador de NFCe - XML

Sistema desenvolvido em C# .NET Framework 4.7.2 para validação de arquivos XML de NFCe através de consulta em banco de dados SQL Server 2014.

## Funcionalidades

- ✅ Leitura de arquivos XML de NFCe de uma pasta selecionada
- ✅ Extração automática de Chave de Acesso, Série, Número da NFCe e Valor Total
- ✅ Totalização dos valores das notas fiscais
- ✅ Consulta ao SQL Server para validação das notas
- ✅ Validação de valores: compara valor do XML com valor da view
- ✅ Interface visual com indicadores de status coloridos
- ✅ Configuração de connection string através de interface gráfica
- ✅ Exportação dos resultados para CSV
- ✅ Formulários editáveis no Visual Studio Designer

## Requisitos

- Visual Studio 2017 ou superior
- .NET Framework 4.7.2
- SQL Server 2014 ou superior
- Windows 7 ou superior

## Estrutura do Projeto

```
NFCeValidator/
├── Models/
│   └── NFCeInfo.cs              # Modelo de dados da NFCe
├── Data/
│   ├── DatabaseConfig.cs        # Gerenciamento de configurações do banco
│   └── NFCeRepository.cs        # Repositório de dados (consultas SQL)
├── Services/
│   └── XmlProcessor.cs          # Processamento dos arquivos XML
└── Forms/
    ├── MainForm.cs              # Tela principal
    ├── MainForm.Designer.cs     # Designer da tela principal
    └── ConfigForm.cs            # Tela de configuração do banco
```

## Como Usar

### 1. Configuração do Banco de Dados

Na primeira execução ou clicando no botão "Configurar Banco":

1. Informe os dados de conexão:
   - **Servidor\Instância**: Nome do servidor SQL (ex: `localhost\SQLEXPRESS`)
   - **Porta**: Porta do SQL Server (padrão: `5433` ou `1433`)
   - **Banco de Dados**: Nome do banco de dados
   - **Usuário**: Login do SQL Server (deixe vazio para autenticação Windows)
   - **Senha**: Senha do usuário (deixe vazio para autenticação Windows)
   - **Timeout**: Tempo limite de conexão em segundos (padrão: `120`)
2. Use o checkbox "Mostrar Senha" para visualizar a senha digitada
3. Clique em "Testar Conexão" para verificar a conectividade
4. Clique em "Salvar" para armazenar a configuração

**Exemplos de Configuração:**

Com Autenticação SQL Server:
```
Servidor\Instância: SERVIDOR\SQLEXPRESS
Porta: 5433
Banco de Dados: NomeBanco
Usuário: sa
Senha: suaSenha123
Timeout: 120
```

Com Autenticação Windows (deixe usuário e senha vazios):
```
Servidor\Instância: SERVIDOR\SQLEXPRESS
Porta: 5433
Banco de Dados: NomeBanco
Usuário: (vazio)
Senha: (vazio)
Timeout: 120
```

### 2. Criar a View no SQL Server

Execute o seguinte script no seu banco de dados para criar a view de exemplo:

```sql
CREATE VIEW vw_NFCe AS
SELECT 
    ChaveAcesso,
    NumeroNFCe,
    ValorTotal,
    DataEmissao,
    CNPJ,
    RazaoSocial
FROM 
    TabelaNFCe
WHERE 
    Status = 'Aprovada'
```

**Importante:** Ajuste o script conforme sua estrutura de dados. O campo essencial é o `NumeroNFCe` que será usado na validação.

### 3. Processar os XMLs

1. Clique em "Selecionar Pasta" e escolha a pasta contendo os arquivos XML
2. Os XMLs serão carregados automaticamente na grid
3. Verifique os totalizadores na parte inferior
4. Ajuste o nome da view se necessário (padrão: vw_NFCe)
5. Clique em "Validar NFCe" para verificar quais notas existem no banco

### 4. Interpretar os Resultados

As linhas do grid serão coloridas conforme o status:

- 🟢 **Verde**: NFCe encontrada na view com valor correto (Status: "✓ OK - Encontrada")
- 🟡 **Amarelo**: NFCe encontrada mas com valor diferente (Status: "⚠ Encontrada - Valor Divergente (View: R$ XX,XX)")
- 🔴 **Vermelho**: NFCe não encontrada na view (Status: "✗ Não Encontrada")
- 🟠 **Laranja**: Erro ao processar (Status: "Erro: ...")

### 5. Exportar Resultados

Clique em "Exportar CSV" para salvar um relatório com todos os dados validados.

## Estrutura do XML da NFCe

O sistema espera que os XMLs estejam no padrão da Receita Federal:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<nfeProc xmlns="http://www.portalfiscal.inf.br/nfe">
  <NFe>
    <infNFe Id="NFe43210512345678901234567890123456789012345">
      <ide>
        <serie>1</serie>
        <nNF>123456</nNF>
        ...
      </ide>
      <total>
        <ICMSTot>
          <vNF>150.00</vNF>
        </ICMSTot>
      </total>
    </infNFe>
  </NFe>
</nfeProc>
```

O sistema extrai:
- **Chave de Acesso**: Atributo `Id` do elemento `infNFe` (sem o prefixo "NFe")
- **Série**: Elemento `serie` dentro de `ide`
- **Número da NFCe**: Elemento `nNF` dentro de `ide`
- **Valor Total**: Elemento `vNF` dentro de `ICMSTot`

## Personalização

### Alterar o Nome da View

Por padrão, o sistema consulta a view `vw_NFCe`. Para usar outra view:

1. Digite o novo nome no campo "Nome da View" na tela principal
2. A consulta será feita na view especificada

### Modificar a Estrutura da View

A view deve retornar pelo menos as colunas `NumeroNFCe` e `ValorTotal` para que a validação completa funcione.

Exemplo de consulta executada:
```sql
-- Verifica se existe
SELECT COUNT(*) FROM vw_NFCe WHERE NumeroNFCe = @NumeroNFCe

-- Busca o valor para comparação
SELECT TOP 1 ValorTotal FROM vw_NFCe WHERE NumeroNFCe = @NumeroNFCe
```

## Tratamento de Erros

O sistema possui tratamento de erros para:

- Arquivos XML corrompidos ou inválidos
- Falhas de conexão com o banco de dados
- Pastas não encontradas
- Erros na leitura dos XMLs

Erros são exibidos em mensagens amigáveis ao usuário.

## Compilação

1. Abra o arquivo `NFCeValidator.sln` no Visual Studio
2. Configure a solução como **Release** (ou Debug para desenvolvimento)
3. Pressione `Ctrl + Shift + B` para compilar
4. O executável estará em `bin/Release/NFCeValidator.exe`

## Edição dos Formulários

Os formulários foram criados com Windows Forms Designer e podem ser editados:

1. No Solution Explorer, expanda o formulário desejado
2. Clique duas vezes no arquivo `.Designer.cs` ou `.cs`
3. Pressione `Shift + F7` ou clique com botão direito → "View Designer"
4. Arraste componentes da Toolbox para editar visualmente

## Notas Técnicas

- A configuração da connection string é salva no arquivo `App.config`
- O sistema suporta XMLs com ou sem namespace
- Valores decimais são tratados tanto com ponto quanto vírgula
- A aplicação usa `DataGridView` com data binding para melhor performance

## Suporte

Para dúvidas ou problemas:
1. Verifique se a view existe no banco de dados
2. Teste a conexão através do botão "Testar Conexão"
3. Verifique se os XMLs estão no formato padrão da Receita Federal
4. Consulte os logs de erro nas mensagens do sistema

## Licença

Projeto desenvolvido para uso interno.
