# Loja do Seu Manoel - API de Empacotamento 3D

API para otimização de empacotamento de produtos em caixas, desenvolvida em .NET 8.

## 📋 Descrição

Sistema que recebe pedidos com produtos e suas dimensões, aplicando algoritmo de empacotamento 3D para determinar a melhor forma de organizá-los em caixas pré-definidas, otimizando o uso do espaço.

## 🛠️ Tecnologias

- **.NET 8** - Web API
- **Entity Framework Core** - ORM
- **SQL Server** - Banco de dados
- **JWT** - Autenticação
- **Docker** - Containerização
- **Swagger** - Documentação da API

## 🎯 Funcionamento

1. **Autenticação**: Login via JWT para acesso aos endpoints
2. **Processamento**: Recebe lista de produtos com dimensões (altura, largura, comprimento)
3. **Algoritmo**: Aplica estratégia "First Fit Decreasing" para empacotamento 3D
4. **Otimização**: Seleciona as menores caixas possíveis e maximiza aproveitamento do espaço
5. **Resposta**: Retorna configuração otimizada com caixas utilizadas e produtos organizados

## 🚀 Como Executar

### Pré-requisitos
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Executar o projeto
```bash
# 1. Clone o repositório
git clone https://github.com/seu-usuario/loja-do-seu-manoel.git
cd loja-do-seu-manoel

# 2. Construa e suba os containers
docker-compose up --build -d

# 3. Acesse a API
# Swagger: http://localhost:5000

```

## 🔐 Autenticação

**Login:** 
```json
  "usuario": "admin",
  "senha": "admin123"
```

## 📖 Uso Básico

**Processar Pedido:** `POST /api/pedidos`
```json
{
  "pedidos": [
    {
      "pedido_id": 1,
      "produtos": [
        {
          "produto_id": "Produto1",
          "dimensoes": {
            "altura": 10,
            "largura": 20,
            "comprimento": 30
          }
        }
      ]
    }
  ]
}
```

**Tipos de Caixas:** `GET /api/caixas`
- Caixa 1: 30x40x80 cm
- Caixa 2: 80x50x40 cm  
- Caixa 3: 50x80x60 cm

## 📁 Exemplos de Teste

Para facilitar os testes da API, utilize os arquivos de exemplo disponíveis:

- **[Entrada de Exemplo](exemploEntradaSaidaJson/entrada.json)** - 10 pedidos com diversos produtos para testar o algoritmo
- **[Saída Esperada](exemploEntradaSaidaJson/saida.json)** - Resultado otimizado do empacotamento dos pedidos de exemplo


