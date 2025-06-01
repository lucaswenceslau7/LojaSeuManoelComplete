# Loja do Seu Manoel - Sistema de Empacotamento 3D

Sistema completo para otimização de empacotamento de produtos em caixas, desenvolvido em .NET 8 com testes unitários.

## 📋 Descrição

Solução que recebe pedidos com produtos e suas dimensões, aplicando algoritmo de empacotamento 3D para determinar a melhor forma de organizá-los em caixas pré-definidas, otimizando o uso do espaço e reduzindo custos de envio.

## 🏗️ Estrutura do Projeto

```
LojaSeuManoel/
├── LojaSeuManoel.WebApi/          # API principal
│   ├── Controllers/               # Controladores da API
│   ├── Models/                    # Modelos de dados
│   ├── Services/                  # Lógica de negócio
│   ├── DTOs/                      # Objetos de transferência
│   └── README.md                  # Documentação da API
├── LojaSeuManoel.WebApi.Test/     # Projeto de testes
│   ├── Tests/UnitTests/           # Testes unitários
│   ├── Tests/IntegrationTests/    # Testes de integração
│   └── README.md                  # Documentação dos testes
└── README.md                      # Este arquivo
```

## 🛠️ Tecnologias

- **.NET 8** - Framework principal
- **Entity Framework Core** - ORM para acesso a dados
- **SQL Server** - Banco de dados
- **JWT** - Autenticação e autorização
- **MSTest** - Framework de testes
- **Docker** - Containerização
- **Swagger** - Documentação da API

## 🎯 Funcionalidades

### API Principal (`LojaSeuManoel.WebApi`)
- **Autenticação JWT** - Sistema de login seguro
- **Empacotamento 3D** - Algoritmo otimizado para organização de produtos
- **Gestão de Pedidos** - CRUD completo de pedidos
- **Tipos de Caixa** - Configuração de diferentes tamanhos de caixas
- **Dockerização** - Deploy simplificado

### Testes (`LojaSeuManoel.WebApi.Test`)
- **Testes Unitários** - Validação de métodos de cálculo e processamento
- **Testes de Modelos** - Verificação de regras de negócio
- **Testes de Serviços** - Validação da lógica de empacotamento
- **Cobertura Abrangente** - Testes para AuthService, Models e Services

## 🚀 Como Executar

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (opcional)
- [SQL Server](https://www.microsoft.com/sql-server) ou SQL Server LocalDB

### Executar a API

```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/LojaSeuManoel.git
cd LojaSeuManoel

# 2. Executar com Docker (recomendado)
cd LojaSeuManoel.WebApi
docker-compose up --build -d

# OU executar localmente
cd LojaSeuManoel.WebApi
dotnet restore
dotnet run

# 3. Acesse a API
# Swagger: http://localhost:5000
# API: http://localhost:5000/api
```

### Executar os Testes

```bash
# Executar todos os testes
cd LojaSeuManoel.WebApi.Test
dotnet test

# Executar com relatório de cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes específicos
dotnet test --filter "TestCategory=UnitTest"
```

## 🔐 Autenticação

Para acessar os endpoints protegidos, faça login:

```bash
POST /api/auth/login
{
  "usuario": "admin",
  "senha": "admin123"
}
```

## 📊 Algoritmo de Empacotamento

O sistema utiliza estratégia **"First Fit Decreasing"**:

1. **Ordenação**: Produtos ordenados por volume (maior → menor)
2. **Seleção**: Escolha da menor caixa que comporte o produto
3. **Otimização**: Máximo aproveitamento do espaço disponível
4. **Resultado**: Configuração otimizada com mínimo de caixas

## 🧪 Testes Implementados

### Modelos Testados
- ✅ **Produto**: Cálculo de volume, validações
- ✅ **TipoCaixa**: Volume da caixa, verificação de capacidade
- ✅ **Pedido**: Volume total, inicialização de listas

### Serviços Testados
- ✅ **AuthService**: Hash de senhas, validação de credenciais
- ✅ **Métodos de Cálculo**: Algoritmos de volume e empacotamento
- ✅ **Validações**: Regras de negócio e tratamento de erros

## 📁 Exemplos de Uso

Veja os arquivos de exemplo na raiz do projeto:
- `entrada-corrigida.json` - Exemplo de pedido válido
- `teste-pedido.json` - Casos de teste
- `novo-teste-pedido.json` - Novos cenários

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

