namespace api_estoque.Entities
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int QuantidadeEstoque { get; set; }
        public double Preco { get; set; }
        public string LocalizacaoDeposito { get; set; }
        public DateTime DataAdicionado { get; set; }
    }
}
