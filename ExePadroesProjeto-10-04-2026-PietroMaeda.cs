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
            " | Prioridade: " + prioridade + 
            " | Msg: " + texto);
    }
}

class AdaptadorSmsLegado : AlertaBase {
    private ServicoSmsLegado servico = new ServicoSmsLegado();
    private string numeroPadrao = "11 12345-6789";

    public override void Disparar(string conteudo) {
        servico.EnviarSMSExterno(numeroPadrao, conteudo, 1);
    }
}

class MontadorDeAlertas {
    public static AlertaBase GerarAlerta(string canal) {
        canal = canal.ToLower();

        if (canal == "correio") {
            return new AlertaCorreio();
        }

        if (canal == "celular") {
            return new AdaptadorSmsLegado();
        }

        if (canal == "app") {
            return new AlertaApp();
        }

        Console.WriteLine("Canal desconhecido, criando alerta......");
        return new AlertaBase();
    }
}

class Program {
    static void Main(string[] args) {

        var parametros = CentralDeParametros.PegarParametros();

        parametros.nomeDoProjeto = "Central de avisos internos";
        parametros.hostDeDisparo = "teste.interno.local";
        parametros.limiteDeReenvio = 3;
        parametros.buildAtual = "beta-alpha-tester";

        Console.WriteLine("Inicializando rotina de avisos...");
        Console.WriteLine("Projeto: " + parametros.nomeDoProjeto);
        Console.WriteLine("Gateway configurado: " + parametros.hostDeDisparo);
        Console.WriteLine();

        var alerta1 = MontadorDeAlertas.GerarAlerta("correio");
        alerta1.Disparar("Seu cadastro foi atualizado com sucesso");

        var alerta2 = MontadorDeAlertas.GerarAlerta("celular");
        alerta2.Disparar("Token temporario: 8841");

        var alerta3 = MontadorDeAlertas.GerarAlerta("app");
        alerta3.Disparar("Existe uma nova atividade pendente");

        Console.WriteLine();
        Console.WriteLine("Rotina finalizada.");
        Console.ReadLine();
    }
}
