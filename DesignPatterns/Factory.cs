namespace DesignPatterns;

// factories 
public class FoodFactory : IFactory
{
	public IConsumable CreateAThing()
	{
		return new Burger();
	}
}
public class DrinkFactory : IFactory
{
	public IConsumable CreateAThing()
	{
		return new Beer();
	}
}

//concrete result classes 
public class Beer : IConsumable
{
	public int Kcal { get; set; } = 400;
	public int Cost { get; set; } = 3;
	public string Brand = "Vergina";
}
public class Burger : IConsumable
{
	public int Kcal { get; set; } = 800;
	public int Cost { get; set; } = 48;
	public bool Spicy = true;
}

//interfaces 
public interface IConsumable
{
	public int Kcal { get; set; }
	public int Cost { get; set; }
}
public interface IFactory
{
	public IConsumable CreateAThing();
}