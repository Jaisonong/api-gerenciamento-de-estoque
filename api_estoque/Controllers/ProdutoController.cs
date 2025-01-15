using api_estoque.Context;
using api_estoque.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Runtime.CompilerServices;

namespace api_estoque.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly ProdutoContext _context;

        public ProdutoController(ProdutoContext context)
        {
            _context = context;
        }

        [HttpPost("cadastro")]
        public ActionResult Create(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
            return Ok(produto);

        }

        [HttpGet("consulta/{id}")]
        public ActionResult Produto(int id)
        {
            var produto = _context.produtos.Find(id);

            var produtoSaida = _context.ProdutosSaida
        .Where(ps => ps.ProdutoId == id)
        .Select(ps => new
        {
            ps.Id,
            ps.ProdutoId,
            ps.QuantidadeRemovida,
            ps.DataSaida,
            ps.NomeProduto,
            ps.Categoria,
            ps.PrecoUnitario,
            ps.LocalizacaoDeposito
        }).ToList();

            if (produto == null)
            {
                return NotFound();
            }

            var resultado = new
            {
                Produto = new
                {
                    produto.Id,
                    produto.Nome,
                    produto.Categoria,
                    produto.QuantidadeEstoque,
                    produto.Preco,
                    produto.LocalizacaoDeposito,
                    produto.DataAdicionado
                },
                Saidas = produtoSaida
            };

            return Ok(resultado);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult ProdutoPorCodigo(int id, Produto produto)
        {
            var produtoBanco = _context.produtos.Find(id);

            if (produto == null)
            {
                return NotFound();
            }

            produtoBanco.Nome = produto.Nome;
            produtoBanco.LocalizacaoDeposito = produto.LocalizacaoDeposito;
            produtoBanco.Categoria = produto.Categoria;
            produtoBanco.Preco = produto.Preco;
            produtoBanco.QuantidadeEstoque = produto.QuantidadeEstoque;

            _context.produtos.Update(produtoBanco);
            _context.SaveChanges();

            return Ok(produtoBanco);
        }

        [HttpGet("estoque-baixo")]
        public IActionResult ProdutoEstoqueMinimo(int quantidadeMinima = 10)
        {
            var produto = _context.produtos.Where(p => p.QuantidadeEstoque <= quantidadeMinima).ToList();

            if (!produto.Any())
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpGet("estoque-alto")]
        public IActionResult ProdutoEstoqueMaximo(int quantidadeMaxima = 50)
        {
            var produto = _context.produtos.Where(p => p.QuantidadeEstoque >= quantidadeMaxima).ToList();

            if (!produto.Any())
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpGet("consultar-localizacao")]
        public IActionResult ProdutoLocalizacao(string localizacao)
        {
            var produto = _context.produtos.Where(p => p.LocalizacaoDeposito == localizacao).ToList();

            if (!produto.Any())
            {
                return NotFound("Não há produtos nesta localização.");
            }

            return Ok(produto);
        }

        [HttpPut("{id}/registrar-saida")]
        public IActionResult RegistrarSaida(int id, int quantidadeRemover)
        {
            var produtoOriginal = _context.produtos.FirstOrDefault(p => p.Id == id);
            var estoqueAtual = _context.produtos.Where(p => p.QuantidadeEstoque == quantidadeRemover);

            if (produtoOriginal == null)
            {
                return NotFound();
            }

            if (produtoOriginal.QuantidadeEstoque < quantidadeRemover)
            {
                return BadRequest($"Quantidade a ser removida maior que o estoque. Estoque atual {produtoOriginal.QuantidadeEstoque}");
            }

            if (produtoOriginal.QuantidadeEstoque <= 0)
            {
                return BadRequest($"Quantidade não pode ser menor ou igual a 0");
            }

            produtoOriginal.QuantidadeEstoque -= quantidadeRemover;

            var produtoSaida = new ProdutoSaida
            {
                ProdutoId = produtoOriginal.Id,
                NomeProduto = produtoOriginal.Nome,
                Categoria = produtoOriginal.Categoria,
                QuantidadeRemovida = quantidadeRemover,
                PrecoUnitario = produtoOriginal.Preco,
                LocalizacaoDeposito = produtoOriginal.LocalizacaoDeposito,
                DataSaida = DateTime.Now
            };

            _context.ProdutosSaida.Add(produtoSaida);
            _context.SaveChanges();

            return Ok(new
            {
                produtoOriginal.Id,
                produtoOriginal.Nome,
                produtoOriginal.QuantidadeEstoque,
                produtoSaida.QuantidadeRemovida,
                produtoSaida.DataSaida
            });

        }

    }

}

