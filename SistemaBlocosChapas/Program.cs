using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Senha { get; set; } = "";
}

public class Bloco
{
    public int Id { get; set; }
    public decimal Espressura { get; set; }
    public decimal Metragem { get; set; }
    public decimal Valor { get; set; }
    public string NotaFiscal { get; set; } = "";
    public int ArmazemId { get; set; }
    public int TipoBlocoId { get; set; }
}

public class Chapa
{
    public int Id { get; set; }
    public int BlocoOrigemId { get; set; }
    public string Tipo { get; set; } = "";
    public string Dimensoes { get; set; } = "";
    public decimal Valor { get; set; }
}

public class SistemaContext : DbContext
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Bloco> Blocos => Set<Bloco>();
    public DbSet<Chapa> Chapas => Set<Chapa>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connStr = "server=localhost;database=MARMSISTEMAS;user=root;password=123456";
        options.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
    }
}

class Program
{
    static void Main()
    {
        using var db = new SistemaContext();
        Console.WriteLine("=== Sistema de Blocos e Chapas ===");

        if (!Login(db))
            return;

        while (true)
        {
            Console.WriteLine("\n1 - Cadastrar Usuário");
            Console.WriteLine("2 - Cadastrar Bloco");
            Console.WriteLine("3 - Cadastrar Chapa");
            Console.WriteLine("4 - Listar Blocos");
            Console.WriteLine("5 - Listar Chapas");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");
            var opc = Console.ReadLine();

            switch (opc)
            {
                case "1": CadastrarUsuario(db); break;
                case "2": CadastrarBloco(db); break;
                case "3": CadastrarChapa(db); break;
                case "4": ListarBlocos(db); break;
                case "5": ListarChapas(db); break;
                case "0": return;
                default: Console.WriteLine("Opção inválida!"); break;
            }
        }
    }

    static bool Login(SistemaContext db)
    {
        Console.Write("Usuário: ");
        var nome = Console.ReadLine();
        Console.Write("Senha: ");
        var senha = Console.ReadLine();

        var user = db.Usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
        if (user != null)
        {
            Console.WriteLine("Login realizado com sucesso!");
            return true;
        }

        Console.WriteLine("Usuário ou senha inválidos.");
        return false;
    }

    static void CadastrarUsuario(SistemaContext db)
    {
        Console.Write("Nome: ");
        var nome = Console.ReadLine();
        Console.Write("Senha: ");
        var senha = Console.ReadLine();
        db.Usuarios.Add(new Usuario { Nome = nome, Senha = senha });
        db.SaveChanges();
        Console.WriteLine("Usuário cadastrado.");
    }

    static void CadastrarBloco(SistemaContext db)
    {
        Console.Write("Espessura: ");
        var esp = decimal.Parse(Console.ReadLine()!);
        Console.Write("Metragem: ");
        var metragem = decimal.Parse(Console.ReadLine()!);
        Console.Write("Valor: ");
        var valor = decimal.Parse(Console.ReadLine()!);
        Console.Write("Nota Fiscal: ");
        var nf = Console.ReadLine();
        Console.Write("ID Armazém: ");
        var arm = int.Parse(Console.ReadLine()!);
        Console.Write("ID Tipo de Bloco: ");
        var tipo = int.Parse(Console.ReadLine()!);

        db.Blocos.Add(new Bloco { Espressura = esp, Metragem = metragem, Valor = valor, NotaFiscal = nf!, ArmazemId = arm, TipoBlocoId = tipo });
        db.SaveChanges();
        Console.WriteLine("Bloco cadastrado.");
    }

    static void CadastrarChapa(SistemaContext db)
    {
        Console.Write("ID do Bloco de Origem: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Tipo: ");
        var tipo = Console.ReadLine();
        Console.Write("Dimensões: ");
        var dim = Console.ReadLine();
        Console.Write("Valor: ");
        var valor = decimal.Parse(Console.ReadLine()!);

        db.Chapas.Add(new Chapa { BlocoOrigemId = id, Tipo = tipo!, Dimensoes = dim!, Valor = valor });
        db.SaveChanges();
        Console.WriteLine("Chapa cadastrada.");
    }

    static void ListarBlocos(SistemaContext db)
    {
        var blocos = db.Blocos.ToList();
        foreach (var b in blocos)
        {
            Console.WriteLine($"ID: {b.Id} | Metragem: {b.Metragem}m³ | Valor: R${b.Valor} | NF: {b.NotaFiscal}");
        }
    }

    static void ListarChapas(SistemaContext db)
    {
        var chapas = db.Chapas.ToList();
        foreach (var c in chapas)
        {
            Console.WriteLine($"ID: {c.Id} | Origem: {c.BlocoOrigemId} | Dimensões: {c.Dimensoes} | Valor: R${c.Valor}");
        }
    }
}
