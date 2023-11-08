using System;
using System.Collections.Generic;

// Classe para representar um livro
class Livro
{
    public int ISBN { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}

// Classe para representar um usuário
class Usuario
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
}

// Classe para representar uma compra
class Compra
{
    public List<Livro> LivrosComprados { get; set; }
    public string NumeroCartaoCredito { get; set; }
    public bool ProcessarCompra()
    {
        // Lógica para processar a compra com a operadora de cartão de crédito
        return true; // Retorne true se a compra for bem-sucedida, ou false em caso de falha
    }
}

// Classe para o sistema de vendas
class SistemaVendaLivros
{
    public List<Livro> estoqueLivros = new List<Livro>();
    public List<Usuario> usuariosCadastrados = new List<Usuario>();

    public void AdicionarLivro(Livro livro)
    {
        estoqueLivros.Add(livro);
    }

    public bool RealizarLogin(string login, string senha)
    {
        // Lógica para verificar se o login e senha são válidos
        // Verifique a lista de usuários cadastrados
        return usuariosCadastrados.Exists(u => u.Login == login && u.Senha == senha);
    }

    public bool RealizarCompra(Usuario usuario, Compra compra)
    {
        // Verificar a disponibilidade de livros no estoque
        foreach (var livro in compra.LivrosComprados)
        {
            var livroNoEstoque = estoqueLivros.Find(l => l.ISBN == livro.ISBN);
            if (livroNoEstoque == null || livroNoEstoque.Estoque == 0)
            {
                // Livro indisponível, processar reserva
                ProcessarReserva(usuario, livro);
                return false;
            }
            else
            {
                // Livro disponível, diminuir o estoque
                livroNoEstoque.Estoque--;
            }
        }

        // Processar a compra com o cartão de crédito
        bool compraBemSucedida = compra.ProcessarCompra();
        if (compraBemSucedida)
        {
            // Lógica para completar a compra bem-sucedida
            // Por exemplo, atualizar o histórico de compras do usuário
        }

        return compraBemSucedida;
    }

    public void ProcessarReserva(Usuario usuario, Livro livro)
    {
        // Lógica para processar a reserva do livro
        // Por exemplo, adicionar o livro à lista de reservas do usuário
    }
}

class Program
{
    static void Main()
    {
        // Exemplo de uso de venda
        SistemaVendaLivros sistema = new SistemaVendaLivros();

        // Adicione livros ao estoque
        sistema.AdicionarLivro(new Livro { ISBN = 1, Titulo = "Livro 1", Autor = "Autor 1", Preco = 19.99m, Estoque = 10 }); //livro 1
        sistema.AdicionarLivro(new Livro { ISBN = 2, Titulo = "Livro 2", Autor = "Autor 2", Preco = 14.99m, Estoque = 5 });
//livro 2

        // Cadastre usuários
        sistema.usuariosCadastrados.Add(new Usuario { Nome = "Usuario 1", Login = "user1", Senha = "senha1" }); // usuario 1
      sistema.usuariosCadastrados.Add(new Usuario { Nome = "Usuario 2", Login = "user2", Senha = "senha2" }); // usuario 2

        // Informações para realizar o Login
        bool loginSucesso = sistema.RealizarLogin("user2", "senha2");

        if (loginSucesso)
        {
            // Exemplo de compra
            var compra = new Compra
            {
                LivrosComprados = new List<Livro>
                {
                    new Livro { ISBN = 10, Titulo = "Livro 1" }, //quantidade e qual livro foi comprado
                    
                },
                NumeroCartaoCredito = "1234-5678-9012-3456" // Número fictício
            };

            bool compraSucesso = sistema.RealizarCompra(sistema.usuariosCadastrados[0], compra);

            if (compraSucesso)
            {
                Console.WriteLine("Sua compra bem-sucedida!");
            }
            else
            {
                Console.WriteLine("Sua compra falhou.");
            }
        }
        else
        {
            Console.WriteLine("Usuario ou Senha errado.");
        }
    }
}
