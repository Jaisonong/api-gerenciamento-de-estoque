# API RESTful de Gerenciamento de Estoque
## Descrição

Esta é uma API para gerenciar um sistema de estoque. Ela permite o cadastro, consulta, atualização, controle de saída de produtos e outras operações relacionadas ao gerenciamento de produtos.

##  🛠 Tecnologias Utilizadas

  - **C#** com **ASP.NET** Core para desenvolvimento da API.
  - **Entity Framework Core** para integração com o banco de dados.
  - Banco de dados: **SQL Server.**

##  📂 Funcionalidades da API

### Endpoints Disponíveis

### **Cadastro de Produto**
- **Endpoint**: `POST /produto/cadastro`
- **Descrição**: Permite cadastrar um novo produto no sistema.
- **Exemplo de Resposta**:
  ```json
  {
    "nome": "Produto A",
    "categoria": "Categoria 1",
    "quantidadeEstoque": 100,
    "preco": 50.00,
    "localizacaoDeposito": "A1",
    "dataAdicionado": "2025-01-01"
  }
    ```
### **Consulta de Produto e Saídas**
- **Endpoint**: `GET /produto/consulta/{id}`
- **Descrição**: Retorna os detalhes de um produto específico e seu histórico de saídas.
- **Exemplo de Resposta**:
  ```json
  {
    "produto": {
      "id": 1,
      "nome": "Produto A",
      "categoria": "Categoria 1",
      "quantidadeEstoque": 90,
      "preco": 50.00,
      "localizacaoDeposito": "A1",
      "dataAdicionado": "2025-01-01"
    },
    "saidas": [
      {
        "id": 1,
        "produtoId": 1,
        "quantidadeRemovida": 10,
        "dataSaida": "2025-01-02",
        "nomeProduto": "Produto A",
        "categoria": "Categoria 1",
        "precoUnitario": 50.00,
        "localizacaoDeposito": "A1"
      }
    ]
  }
    ```
### **Atualização de Produto**
- **Endpoint**: `PUT /produto/atualizar/{id}`
- **Descrição**: Atualiza os detalhes de um produto existente.
- **Exemplo de Resposta**:
     ```json
    {
    "nome": "Produto B",
    "categoria": "Categoria 2",
    "quantidadeEstoque": 150,
    "preco": 60.00,
    "localizacaoDeposito": "B2"
    }
     ```
### **Consulta de Estoque Baixo**
- **Endpoint**: `GET /produto/estoque-baixo/quantidadeMinima={valor}`
- **Descrição**: Retorna produtos com estoque igual ou inferior ao valor especificado.
- **Parâmetro Padrão**: `quantidadeMinima = 10`

### **Consulta de Estoque Alto**
- **Endpoint**: `GET /produto/estoque-alto/quantidadeMaxima={valor}`
- **Descrição**: Retorna produtos com estoque igual ou superior ao valor especificado.
- **Parâmetro Padrão**: `quantidadeMaxima = 50`,

### **Consulta de Localização de Produto**
- **Endpoint**: `GET /produto/consultar-localizacao/localizacao={valor}`
- **Descrição**: Retorna produtos localizados em um depósito específico.
- **Exemplo de Resposta**:
     ```json
    {
    "id": 2,
    "nome": "Produto C",
    "categoria": "Categoria 3",
    "quantidadeEstoque": 25,
    "preco": 80.00,
    "localizacaoDeposito": "C1",
    "dataAdicionado": "2025-01-10"
    }
    ```
### **Registro de Saída**
- **Endpoint**: `PUT /produto/{id}/registrar-saida`
- **Descrição**: Registra a saída de um produto pelo ID, bem como salva automaticamente a data de saída do produto.

### **🛠️ Configuração e Execução**
  - Instale o **.NET SDK.**
  - Configure um banco de dados (SQL Server, MySQL, etc.).
  - Ferramentas de gerenciamento de banco de dados, como MySQL Workbench ou SQL Server Management Studio.

## Passos para Configuração

### 1. Clone o Repositório:
 ```
git clone https://github.com/seu-usuario/api-estoque.git
cd api-estoque
 ```

### 2. Configure o Banco de Dados:
Edite o arquivo **appsettings.json** para configurar a string de conexão:

    "ConnectionStrings": {
    "DefaultConnection": "sua-string-de-conexao"
    }
    

### 3. Crie o Banco de Dados:

Execute as migrações:
```
dotnet ef database update
```

### 4. Inicie a Aplicação:
```
dotnet run
```
