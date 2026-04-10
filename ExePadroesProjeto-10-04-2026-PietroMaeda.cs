using System;

class CentralDeParametros {
    private static CentralDeParametros instanciaUnica;
    public string nomeDoProjeto;
    public string hostDeDisparo;
    public int limiteDeReenvio;
    public string buildAtual;
    private CentralDeParametros() { }

    public static CentralDeParametros PegarParametros() {
        if (instanciaUnica == null) {
            instanciaUnica = new CentralDeParametros();
        }
        return instanciaUnica;
    }
}

class AlertaBase {
    public virtual void Disparar(string conteudo) {
        Console.WriteLine("Disparo generico de alerta -> " + conteudo);
    }
}

class AlertaCorreio : AlertaBase {
    public override void Disparar(string conteudo) {
        Console.WriteLine("Email enviado: " + conteudo);
    }
}

class AlertaApp : AlertaBase {
    public override void Disparar(string conteudo) {
        Console.WriteLine("Aviso push no aplicativo: " + conteudo);
    }
}

class ServicoSmsLegado {
    public void EnviarSMSExterno(string numeroDestino, string texto, int prioridade) {
        Console.WriteLine("API LEGADA -> SMS enviado para "
            + numeroDestino +
            " | prioridade: " + prioridade +
            " | msg: " + texto);
    }
}

class AdaptadorSmsLegado : AlertaBase {
    private ServicoSmsLegado servico = new ServicoSmsLegado();
    private string numeroPadrao = "11 12345-6789";

    public override void Disparar(string conteudo) {
        servico.EnviarSMSExterno(numeroPadrao, conteudo, 1);
    }
}

class ProxyAlerta : AlertaBase {

    private AlertaBase alertaReal;
    private int tentativas = 0;

    public ProxyAlerta(AlertaBase alerta) {
        alertaReal = alerta;
    }

    public override void Disparar(string conteudo) {

        var config = CentralDeParametros.PegarParametros();

        Console.WriteLine("[LOGGERR] tentativa de envio registrada");

        if (tentativas >= config.limiteDeReenvio) {
            Console.WriteLine("[BLOQUEADO!!] limite de tentativas atingido");
            return;
        }

        tentativas++;

        Console.WriteLine("[LOGGERR] enviando alerta...");
        alertaReal.Disparar(conteudo);
    }
}

class MontadorDeAlertas {
    public static AlertaBase GerarAlerta(string canal) {

        canal = canal.ToLower();
        AlertaBase alerta;

        if (canal == "correio") {
            alerta = new AlertaCorreio();
        }
        else if (canal == "celular") {
            alerta = new AdaptadorSmsLegado();
        }
        else if (canal == "app") {
            alerta = new AlertaApp();
        }
        else {
            Console.WriteLine("Canal desconhecido, criando alerta generico");
            alerta = new AlertaBase();
        }
        return new ProxyAlerta(alerta);
    }
}

class Program {
    static void Main(string[] args) {

        var parametros = CentralDeParametros.PegarParametros();

        parametros.nomeDoProjeto = "Central de avisos internos";
        parametros.hostDeDisparo = "teste.interno.local";
        parametros.limiteDeReenvio = 2;
        parametros.buildAtual = "beta-alpha-tester";

        Console.WriteLine("Inicializando rotina de avisos...");
        Console.WriteLine("Projeto: " + parametros.nomeDoProjeto);
        Console.WriteLine("Gateway configurado: " + parametros.hostDeDisparo);
        Console.WriteLine();

        var alerta1 = MontadorDeAlertas.GerarAlerta("correio");

        alerta1.Disparar("Seu cadastro foi atualizado");
        alerta1.Disparar("Mensagem repetida");
        alerta1.Disparar("Terceira tentativa bloqueada");

        Console.WriteLine();
        Console.WriteLine("Rotina finalizada.");
        Console.ReadLine();
    }
}
