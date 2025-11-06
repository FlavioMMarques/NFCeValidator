# 🚀 Início Rápido - Validador de NFCe

## 📋 O que foi criado

Sistema completo em C# .NET Framework 4.7.2 com Windows Forms para validar NFCe através de XML contra SQL Server.

## 📁 Estrutura de Arquivos

```
NFCeValidator/
├── NFCeValidator.sln                  # Solução Visual Studio
├── README.md                          # Documentação completa
├── INSTALACAO_E_PROBLEMAS.md         # Guia de troubleshooting
│
├── NFCeValidator/                     # Projeto principal
│   ├── Models/
│   │   └── NFCeInfo.cs               # Modelo de dados
│   ├── Data/
│   │   ├── DatabaseConfig.cs         # Configuração do banco
│   │   └── NFCeRepository.cs         # Consultas SQL
│   ├── Services/
│   │   └── XmlProcessor.cs           # Processamento de XML
│   ├── Forms/
│   │   ├── MainForm.cs               # Tela principal
│   │   ├── MainForm.Designer.cs      # Designer da tela principal
│   │   ├── ConfigForm.cs             # Tela de configuração
│   │   └── ConfigForm.Designer.cs    # Designer da configuração
│   ├── Program.cs                     # Ponto de entrada
│   ├── App.config                     # Configurações
│   └── NFCeValidator.csproj          # Arquivo do projeto
│
├── SQL_Scripts/
│   └── exemplo_view_nfce.sql         # Script de exemplo da view
│
└── Exemplos_XML/
    └── exemplo_nfce.xml              # Exemplo de XML de NFCe
```

## ⚡ Passos Rápidos

### 1. Abrir o Projeto
- Abra o arquivo `NFCeValidator.sln` no Visual Studio 2017 ou superior

### 2. Compilar
- Pressione `Ctrl + Shift + B` para compilar
- O executável estará em `bin/Debug/` ou `bin/Release/`

### 3. Configurar o Banco
- Execute a aplicação
- Clique em "Configurar Banco"
- Preencha os campos:
  - Servidor\Instância
  - Porta (padrão: 5433)
  - Banco de Dados
  - Usuário (opcional para Windows Auth)
  - Senha (opcional para Windows Auth)
  - Timeout (padrão: 120)
- Teste a conexão

### 4. Criar a View no SQL Server
```sql
CREATE VIEW vw_NFCe AS
SELECT 
    ChaveAcesso,
    NumeroNFCe,
    ValorTotal,
    DataEmissao
FROM 
    SuaTabelaDeNFCe
WHERE 
    Status = 'Aprovada';
```

### 5. Validar NFCe
- Clique em "Selecionar Pasta"
- Escolha a pasta com os XMLs
- Clique em "Validar NFCe"
- Veja os resultados com cores:
  - 🟢 Verde = Encontrada
  - 🔴 Vermelho = Não encontrada
  - 🟡 Amarelo = Erro

## 🎯 Funcionalidades Principais

✅ Leitura de arquivos XML de NFCe  
✅ Extração de Chave de Acesso, Número e Valor Total  
✅ Validação contra view do SQL Server  
✅ Totalização de valores  
✅ Indicadores visuais de status  
✅ Exportação para CSV  
✅ Configuração persistente do banco  
✅ Formulários editáveis no Designer

## 🔧 Configuração do Banco

### Campos de Configuração
| Campo | Descrição | Exemplo |
|-------|-----------|---------|
| Servidor\Instância | Nome do servidor SQL | `localhost\SQLEXPRESS` |
| Porta | Porta do SQL Server | `5433` ou `1433` |
| Banco de Dados | Nome do banco | `BaseSoftShop9.20.0.0` |
| Usuário | Login SQL (vazio = Windows Auth) | `sa` |
| Senha | Senha do usuário | `********` |
| Timeout | Tempo limite em segundos | `120` |

### Autenticação SQL Server
```
Servidor: localhost\SQLEXPRESS
Porta: 5433
Banco: NomeBanco
Usuário: sa
Senha: suaSenha
Timeout: 120
```

### Autenticação Windows
```
Servidor: localhost\SQLEXPRESS
Porta: 5433
Banco: NomeBanco
Usuário: (deixe vazio)
Senha: (deixe vazio)
Timeout: 120
```

## 📝 Campos Extraídos do XML

| Campo | Local no XML | Obrigatório |
|-------|-------------|-------------|
| Chave de Acesso | `infNFe/@Id` | Sim |
| Número NFCe | `ide/nNF` | Sim |
| Valor Total | `total/ICMSTot/vNF` | Sim |

## 🎨 Editando os Formulários

1. No Solution Explorer, expanda o formulário desejado
2. Clique duas vezes no arquivo `.cs` ou `.Designer.cs`
3. Pressione `Shift + F7` para abrir o Designer
4. Arraste componentes da Toolbox
5. Ajuste propriedades no painel Properties

## 📊 View no SQL Server

### Estrutura Mínima
```sql
CREATE VIEW vw_NFCe AS
SELECT 
    NumeroNFCe  -- CAMPO OBRIGATÓRIO
    -- outros campos opcionais
FROM SuaTabela;
```

### A view deve retornar:
- `NumeroNFCe` (obrigatório para validação)
- Outros campos são opcionais

## 🔍 Testando

### 1. XML de Teste
Use o arquivo em `Exemplos_XML/exemplo_nfce.xml`

### 2. Testar View no SQL
```sql
SELECT COUNT(*) FROM vw_NFCe WHERE NumeroNFCe = '123456';
```

### 3. Verificar Conexão
```sql
SELECT @@VERSION;
SELECT DB_NAME();
```

## ❓ Problemas Comuns

### "Não foi possível conectar"
- Verifique se SQL Server está rodando
- Teste a connection string no SQL Management Studio
- Verifique firewall

### "View não encontrada"
- Execute o script de criação da view
- Verifique se a view está no banco correto
- Use nome completo: `dbo.vw_NFCe`

### "XMLs não carregam"
- Verifique se os arquivos têm extensão .xml
- Certifique-se que estão no formato da Receita Federal
- Use o arquivo de exemplo como referência

## 📦 Requisitos

- Windows 7 SP1 ou superior
- .NET Framework 4.7.2
- SQL Server 2014 ou superior
- Visual Studio 2017+ (para desenvolvimento)

## 🎯 Próximos Passos

1. ✅ Compile o projeto
2. ✅ Configure a connection string
3. ✅ Crie a view no banco
4. ✅ Teste com XMLs de exemplo
5. ✅ Valide contra sua base de dados
6. ✅ Customize conforme necessário

## 📞 Documentação Completa

- `README.md` - Documentação detalhada
- `INSTALACAO_E_PROBLEMAS.md` - Troubleshooting
- `SQL_Scripts/exemplo_view_nfce.sql` - Exemplos SQL

## ✨ Dicas

💡 Use o botão "Ver Exemplo" para ver um modelo de connection string  
💡 Teste a conexão antes de salvar  
💡 O nome da view pode ser alterado na tela principal  
💡 Exporte os resultados em CSV para análise  
💡 Os formulários podem ser editados no Visual Studio Designer  

---

**Desenvolvido para .NET Framework 4.7.2 + SQL Server 2014**
