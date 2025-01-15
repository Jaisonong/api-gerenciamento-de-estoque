namespace api_estoque.Entities
{

        public class ProdutoSaida
        {
            public int Id { get; set; }
            public int ProdutoId { get; set; } // Referência ao produto
            public int QuantidadeRemovida { get; set; } // Quantidade removida
            public DateTime DataSaida { get; set; } // Data de saída

            // Relacionamento com o produto
            public Produto Produto { get; set; }
    }
}
