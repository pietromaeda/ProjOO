using System;

class ConfigSistema
{
    private static ConfigSistema instancia;
    public string nomeAplicacao;
    public string servidorEnvio;
    public int maxTentativas;
    public string versaoSistema;
    private ConfigSistema() { }

    public static ConfigSistema GetConfig()
    {
        if (instancia == null)
        {
            instancia = new ConfigSistema();
        }

        return instancia;
    }
}

class Notificacao
{
    public virtual void Enviar(string mensagem)
    {
        Console.WriteLine("Enviando notificacao: " + mensagem);
    }
}

class Email : Notificacao
{
    public override void Enviar(string mensagem)
    {
        Console.WriteLine("Email enviado: " + mensagem);
    }
}

class SMS : Notificacao
{
    public override void Enviar(string mensagem)
    {
        Console.WriteLine("SMS enviado -> " + mensagem);
    }
}

class Push : Notificacao
{
    public override void Enviar(string mensagem)
    {
        Console.WriteLine("Push notification enviada: " + mensagem);
    }
}

class FabricaNotificacao
{
    public static Notificacao CriarNotificacao(string tipo)
    {
        if (tipo == "email")
        {
            return new Email();
        }
        else if (tipo == "sms")
        {
        return new SMS();
        }
        else if (tipo == "push")
        {
            return new Push();
        }
        else
        {
            Console.WriteLine("Tipo de notificacao nao reconhecido, usando padrao");
            return new Notificacao();
        }
    }
}

class Program
{
    static void Main()
    {
        ConfigSistema config = ConfigSistema.GetConfig();

        config.nomeAplicacao = "Sistema de notificacoes";
        config.servidorEnvio = "server.empresa.com";
        config.maxTentativas = 3;

        Console.WriteLine("Sistema iniciado: " + config.nomeAplicacao);
        Console.WriteLine("Servidor: " + config.servidorEnvio);
        Console.WriteLine("Tentativas maximas: " + config.maxTentativas);
        Console.WriteLine();

        Notificacao n1 = FabricaNotificacao.CriarNotificacao("email");
        n1.Enviar("Bem vindo ao sistema");

        Notificacao n2 = FabricaNotificacao.CriarNotificacao("sms");
        n2.Enviar("Seu codigo eh 1234");

        Notificacao n3 = FabricaNotificacao.CriarNotificacao("push");
        n3.Enviar("Voce recebeu uma nova mensagem");

        Notificacao teste = FabricaNotificacao.CriarNotificacao("outro");
        teste.Enviar("teste de notificacao");

        Console.ReadLine();
    }
}
