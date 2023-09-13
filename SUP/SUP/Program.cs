using System;
using System.Collections.Generic;
using System.Linq;




class PessoaFisica
{
    public string Usuario { get; }
    public string Senha { get; }

    public PessoaFisica(string usuario, string senha)
    {
        Usuario = usuario;
        Senha = senha;
    }
}

class Empresa
{
    public string Usuario { get; }
    public string Senha { get; }

    public Empresa(string usuario, string senha)
    {
        Usuario = usuario;
        Senha = senha;
    }
}

class Produto
{
    public string NomeEmpresa { get; }
    public string NomeProduto { get; }
    public double Preco { get; }
    public double Frete { get; }

    public Produto(string nomeEmpresa, string nomeProduto, double preco, double frete)
    {
        NomeEmpresa = nomeEmpresa;
        NomeProduto = nomeProduto;
        Preco = preco;
        Frete = frete;
    }
}


class Program
{
    static void Main()
    {
        DataAccessLayer dal = new DataAccessLayer();

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1. Cadastrar Pessoa Física");
            Console.WriteLine("2. Cadastrar Empresa");
            Console.WriteLine("3. Cadastrar Produto");
            Console.WriteLine("4. Exibir produtos cadastrados por empresa");
            Console.WriteLine("5. Exibir produtos iguais em diferentes empresas");
            Console.WriteLine("6. Exibir produto mais barato");
            Console.WriteLine("7. Sair\n");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    
                    Console.Write("Nome de usuário: ");
                    string usuarioPessoaFisica = Console.ReadLine();
                    Console.Write("Senha: ");
                    string senhaPessoaFisica = Console.ReadLine();

                    PessoaFisica pessoaFisica = new PessoaFisica(usuarioPessoaFisica, senhaPessoaFisica);
                    dal.CadastrarPessoaFisica(pessoaFisica);
                    break;

                case "2":
                    
                    Console.Write("Nome de usuário: ");
                    string usuarioEmpresa = Console.ReadLine();
                    Console.Write("Senha: ");
                    string senhaEmpresa = Console.ReadLine();

                    Empresa empresa = new Empresa(usuarioEmpresa, senhaEmpresa);
                    dal.CadastrarEmpresa(empresa);
                    break;

                case "3":
                    
                    Console.Write("Nome da empresa: ");
                    string nomeEmpresa = Console.ReadLine();
                    Console.Write("Nome do produto: ");
                    string nomeProduto = Console.ReadLine();
                    Console.Write("Preço: ");
                    double preco = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Frete:");
                    double frete = Convert.ToDouble(Console.ReadLine());

                    Produto produto = new Produto(nomeEmpresa, nomeProduto, preco, frete);
                    dal.CadastrarProduto(produto);
                    break;

                case "4":
                    
                    Console.Write("\nDigite o nome da empresa: ");
                    string nomeEmpresaConsulta = Console.ReadLine();
                    dal.ExibirProdutosPorEmpresa(nomeEmpresaConsulta);
                    break;

                case "5":
                   
                    Console.Write("\nDigite o nome do produto: ");
                    string nomeProdutoConsulta = Console.ReadLine();
                    dal.ExibirProdutosIguais(nomeProdutoConsulta);
                    break;

                case "6":
                    
                    Console.Write("\nDigite o nome do produto para encontrar o mais barato: ");
                    string nomeProdutoBarato = Console.ReadLine();
                    dal.ExibirProdutoMaisBarato(nomeProdutoBarato);
                    break;

                case "7":
                   
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("\nOpção inválida. Tente novamente.\n");
                    break;
            }
        }
    }
}


class DataAccessLayer
{
    private List<PessoaFisica> pessoasFisicas = new List<PessoaFisica>();
    private List<Empresa> empresas = new List<Empresa>();
    private List<Produto> produtos = new List<Produto>();

    public void CadastrarPessoaFisica(PessoaFisica pessoaFisica)
    {
        pessoasFisicas.Add(pessoaFisica);
    }

    public void CadastrarEmpresa(Empresa empresa)
    {
        empresas.Add(empresa);
    }

    public void CadastrarProduto(Produto produto)
    {
        produtos.Add(produto);
    }

    public void ExibirProdutosPorEmpresa(string nomeEmpresa)
    {
        var produtosDaEmpresa = produtos.Where(p => p.NomeEmpresa == nomeEmpresa);
        Console.WriteLine($"\nProdutos cadastrados pela empresa '{nomeEmpresa}':\n");
        foreach (var produto in produtosDaEmpresa)
        {
            Console.WriteLine($"\n{produto.NomeProduto} - Preço: {produto.Preco:C} - Frete: {produto.Frete:C}\n");
        }
    }

    public void ExibirProdutosIguais(string nomeProduto)
    {
        var produtosIguais = produtos.Where(p => p.NomeProduto == nomeProduto);
        Console.WriteLine($"\nProdutos com o nome '{nomeProduto}' cadastrados por diferentes empresas:\n");
        foreach (var produto in produtosIguais)
        {
            Console.WriteLine($"\n\nEmpresa: {produto.NomeEmpresa} - Preço: {produto.Preco:C} - Frete: {produto.Frete:C}\n\n");
        }
    }



    /*aqui existe uma contradição

     claro que existe a possibilidade de fazer essa conta levando em consideração o preço do frete
    entretanto nosso grupo está trabalhando com a possibilidade do cliente levar mais produtos da loja
    e como você paga o frete uma vez por entrega, não incluímos no calculo o frete 
     
     */


    public void ExibirProdutoMaisBarato(string nomeProduto)
    {
        var produtosDoProduto = produtos.Where(p => p.NomeProduto == nomeProduto);
        if (!produtosDoProduto.Any())
        {
            Console.WriteLine($"\nNenhum produto com o nome '{nomeProduto}' encontrado.\n");
            return;
        }

        var produtosMaisBaratos = produtosDoProduto.OrderBy(p => p.Preco);
        var produtoMaisBarato = produtosMaisBaratos.First();

        Console.WriteLine($"\n\no resultado pra nossa busca para encontrar o(a) |{nomeProduto}| mais barato foi:");
        Console.WriteLine($"Empresa: {produtoMaisBarato.NomeEmpresa} - Preço: {produtoMaisBarato.Preco:C} - Frete: {produtoMaisBarato.Frete:C}\n\n");
    }
}
