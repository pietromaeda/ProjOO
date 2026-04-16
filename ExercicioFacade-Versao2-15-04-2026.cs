using System;

class TV {
    public void Ligar() {
        Console.WriteLine("TV foi ligada");
    }
    public void Desligar() {
        Console.WriteLine("TV foi desligada");
    }
}

class Projetor {
    public void Ligar() {
        Console.WriteLine("Projetor iniciou");
    }
    public void Desligar() {
        Console.WriteLine("Projetor desligado");
    }
}

class Receiver {
    public void Ligar() {
        Console.WriteLine("Receiver ligado");
    }
    public void Desligar() {
        Console.WriteLine("Receiver desligado");
    }
    public void AjustarVolume() {
        Console.WriteLine("Volume ajustado no receiver");
    }
}

class PlayerMidia {
    public void Ligar() {
        Console.WriteLine("Player de mídia iniciado");
    }
    public void ReproduzirFilme() {
        Console.WriteLine("Filme começou a rodar");
    }
    public void ReproduzirMusica() {
        Console.WriteLine("Música começou a tocar");
    }
    public void Desligar() {
        Console.WriteLine("Player desligado");
    }
}

class LuzAmbiente {
    public void Diminuir() {
        Console.WriteLine("Luz ambiente ficou mais baixa");
    }
    public void Aumentar() {
        Console.WriteLine("Luz ambiente voltou ao normal");
    }
}

class HomeTheaterFacade {
    private TV tv;
    private Projetor projetor;
    private Receiver receiver;
    private PlayerMidia player;
    private LuzAmbiente luz;
    public HomeTheaterFacade() {
        tv = new TV();
        projetor = new Projetor();
        receiver = new Receiver();
        player = new PlayerMidia();
        luz = new LuzAmbiente();
    }
    public void AssistirFilme() {
        Console.WriteLine("\nPreparando tudo para assistir um filme.......\n");
        luz.Diminuir();
        tv.Ligar();
        projetor.Ligar();
        receiver.Ligar();
        receiver.AjustarVolume();
        player.Ligar();
        player.ReproduzirFilme();
    }
    public void OuvirMusica() {
        Console.WriteLine("\nConfigurando sistema para ouvir música...\n");
        receiver.Ligar();
        receiver.AjustarVolume();
        player.Ligar();
        player.ReproduzirMusica();
    }
    public void EncerrarSistema() {
        Console.WriteLine("\nEncerrando o sistema do Home Theater...\n");
        player.Desligar();
        receiver.Desligar();
        projetor.Desligar();
        tv.Desligar();
        luz.Aumentar();
    }
}

class Program {
    static void Main(string[] args) {
        HomeTheaterFacade sistema = new HomeTheaterFacade();
        Console.WriteLine("Sistema de Home Theater iniciado");
        sistema.AssistirFilme();
        Console.WriteLine("\n------ pausa -----\n");
        sistema.EncerrarSistema();
        Console.WriteLine("\nAgora vamos testar o modo música\n");
        sistema.OuvirMusica();
        Console.WriteLine("\nPressione algo para finalizar......");
        Console.ReadLine();
    }
}