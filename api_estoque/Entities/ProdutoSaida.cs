namespace api_estoque.Entities
{
    public class ProdutoSaida
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int QuantidadeRemovida { get; set; }
        public DateTime DataSaida { get; set; } 
        public string NomeProduto { get; set; } 
        public string Categoria { get; set; }
        public double PrecoUnitario { get; set; } 
        public string LocalizacaoDeposito { get; set; } 

        
        public Produto Produto { get; set; }
    }
}
