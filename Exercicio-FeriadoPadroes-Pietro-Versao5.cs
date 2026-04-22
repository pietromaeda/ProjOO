using System;

class ConfiguracaoPlataforma
{
    private static ConfiguracaoPlataforma instancia;

    public string nomePlataforma;
    public string semestreAtual;
    public string responsavel;

    private ConfiguracaoPlataforma() { }

    public static ConfiguracaoPlataforma PegarInstancia()
    {
        if (instancia == null)
        {
            instancia = new ConfiguracaoPlataforma();
        }

        return instancia;
    }
}

class Relatorio
{
    public virtual void Gerar(string aluno)
    {
        Console.WriteLine("Gerando relatorio generico para " + aluno);
    }
}

class BibliotecaPDFExterna
{
    public void CriarDocumento(string nomeAluno)
    {
        Console.WriteLine("Biblioteca externa gerou um PDF para " + nomeAluno);
    }
}

class AdaptadorPDF : Relatorio
{
    private BibliotecaPDFExterna biblioteca = new BibliotecaPDFExterna();

    public override void Gerar(string aluno)
    {
        biblioteca.CriarDocumento(aluno);
    }
}

class RelatorioCSV : Relatorio
{
    public override void Gerar(string aluno)
    {
        Console.WriteLine("Relatorio CSV criado para o aluno " + aluno);
    }
}

class RelatorioHTML : Relatorio
{
    public override void Gerar(string aluno)
    {
        Console.WriteLine("Relatorio HTML criado para o aluno " + aluno);
    }
}

class DecoradorRelatorio : Relatorio
{
    protected Relatorio relatorio;

    public DecoradorRelatorio(Relatorio r)
    {
        relatorio = r;
    }

    public override void Gerar(string aluno)
    {
        relatorio.Gerar(aluno);
    }
}

class MarcaDagua : DecoradorRelatorio
{
    public MarcaDagua(Relatorio r) : base(r) { }

    public override void Gerar(string aluno)
    {
        relatorio.Gerar(aluno);
        Console.WriteLine("Marca de agua adicionada ao relatorio");
    }
}

class AssinaturaDigital : DecoradorRelatorio
{
    public AssinaturaDigital(Relatorio r) : base(r) { }

    public override void Gerar(string aluno)
    {
        relatorio.Gerar(aluno);
        Console.WriteLine("Relatorio assinado digitalmente");
    }
}

class FabricaRelatorio
{
    public static Relatorio CriarRelatorio(string tipo)
    {
        tipo = tipo.ToLower();

        if (tipo == "pdf")
            return new AdaptadorPDF();

        if (tipo == "csv")
            return new RelatorioCSV();

        if (tipo == "html")
            return new RelatorioHTML();

        Console.WriteLine("Tipo nao reconhecido, usando relatorio simples");
        return new Relatorio();
    }
}

class ProxyRelatorio : Relatorio
{
    private Relatorio relatorioReal;
    private static int contador = 0;
    private int limite = 5;

    public ProxyRelatorio(Relatorio r)
    {
        relatorioReal = r;
    }

    public override void Gerar(string aluno)
    {
        if (contador >= limite)
        {
            Console.WriteLine("Limite de geracao de relatorios atingido");
            return;
        }

        Console.WriteLine("Log: gerando relatorio para " + aluno);

        relatorioReal.Gerar(aluno);

        contador++;
    }
}

class SistemaRelatorios
{
    public void GerarRelatorioAluno(string tipo, string aluno)
    {
        Relatorio relatorio = FabricaRelatorio.CriarRelatorio(tipo);

        relatorio = new MarcaDagua(relatorio);
        relatorio = new AssinaturaDigital(relatorio);

        relatorio = new ProxyRelatorio(relatorio);

        relatorio.Gerar(aluno);
    }
}

class Program
{
    static void Main()
    {
        var config = ConfiguracaoPlataforma.PegarInstancia();

        config.nomePlataforma = "StudyTrack PRO master Zeta";
        config.semestreAtual = "1 semestre de 2026";
        config.responsavel = "Professor Fabio";

        Console.WriteLine("Plataforma: " + config.nomePlataforma);
        Console.WriteLine("Semestre: " + config.semestreAtual);
        Console.WriteLine();

        SistemaRelatorios sistema = new SistemaRelatorios();

        sistema.GerarRelatorioAluno("pdf", "Ana");
        sistema.GerarRelatorioAluno("csv", "Bruno");
        sistema.GerarRelatorioAluno("html", "Carla");
        sistema.GerarRelatorioAluno("pdf", "Daniel");
        sistema.GerarRelatorioAluno("pdf", "Julia");
        sistema.GerarRelatorioAluno("pdf", "Teste");

        Console.ReadLine();
    }
}