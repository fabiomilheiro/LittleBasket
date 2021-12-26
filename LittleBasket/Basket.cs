namespace LittleBasket;

public class Basket
{
    private readonly List<BasketItem> items;

    public Basket()
    {
        this.items = new List<BasketItem>();
    }

    public void Add(Product product)
    {
        throw new NotImplementedException();
    }

    public void Subtract(Product product)
    {
        throw new NotImplementedException();
    }

    public void Remove(Product product)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BasketItem> GetItems()
    {
        throw new NotImplementedException();
    }
}