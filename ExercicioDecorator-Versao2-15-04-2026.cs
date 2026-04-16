using System;

abstract class Bebida
{
    public abstract string Descricao();
    public abstract double Preco();
}

class CafeExpresso : Bebida
{
    public override string Descricao()
    {
        return "Cafe expresso";
    }
    public override double Preco()
    {
        return 5.0;
    }
}

class Cappuccino : Bebida
{
    public override string Descricao()
    {
        return "Cappuccino";
    }
    public override double Preco()
    {
        return 7.5;
    }
}

class Cha : Bebida
{
    public override string Descricao()
    {
        return "Cha simples";
    }
    public override double Preco()
    {
        return 4.0;
    }
}

abstract class Adicional : Bebida
{
    protected Bebida bebida;

    public Adicional(Bebida b)
    {
        bebida = b;
    }
}

class Leite : Adicional
{
    public Leite(Bebida b) : base(b) { }

    public override string Descricao()
    {
        return bebida.Descricao() + " + leite";
    }
    public override double Preco()
    {
        return bebida.Preco() + 1.5;
    }
}

class Chantilly : Adicional
{
    public Chantilly(Bebida b) : base(b) { }

    public override string Descricao()
    {
        return bebida.Descricao() + " + chantilly";
    }
    public override double Preco()
    {
        return bebida.Preco() + 2.0;
    }
}

class Canela : Adicional
{
    public Canela(Bebida b) : base(b) { }

    public override string Descricao()
    {
        return bebida.Descricao() + " + canela";
    }
    public override double Preco()
    {
        return bebida.Preco() + 0.75;
    }
}

class Chocolate : Adicional
{
    public Chocolate(Bebida b) : base(b) { }

    public override string Descricao()
    {
        return bebida.Descricao() + " + calda de chocolate";
    }
    public override double Preco()
    {
        return bebida.Preco() + 2.5;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Sistema cafeteria\n");

        Bebida pedido1 = new CafeExpresso();
        pedido1 = new Leite(pedido1);
        pedido1 = new Canela(pedido1);

        Console.WriteLine("Pedido 1:");
        Console.WriteLine("Bebida: " + pedido1.Descricao());
        Console.WriteLine("Preco total: R$ " + pedido1.Preco());
        Console.WriteLine();

        Bebida pedido2 = new Cappuccino();
        pedido2 = new Chantilly(pedido2);
        pedido2 = new Chocolate(pedido2);

        Console.WriteLine("Pedido 2:");
        Console.WriteLine("Bebida: " + pedido2.Descricao());
        Console.WriteLine("Preco total: R$ " + pedido2.Preco());
        Console.WriteLine();

        Bebida pedido3 = new Cha();
        pedido3 = new Leite(pedido3);
        pedido3 = new Chantilly(pedido3);

        Console.WriteLine("Pedido 3:");
        Console.WriteLine("Bebida: " + pedido3.Descricao());
        Console.WriteLine("Preco total: R$ " + pedido3.Preco());

        Console.WriteLine("\nFim dos pedidos");
        Console.ReadLine();
    }
}