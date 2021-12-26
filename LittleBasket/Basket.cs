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
        var item = this.GetBasketItemByProductOrNull(product);
        if (item != null)
        {
            this.ReplaceBasketItem(item, new BasketItem(product, item.Quantity + 1));
        }
        else
        {
            this.items.Add(new BasketItem(product, 1));
        }
    }

    public void Subtract(Product product)
    {
        var item = this.GetBasketItemByProductOrNull(product);

        if (item == null)
        {
            return;
        }

        if (item.Quantity > 1)
        {
            this.ReplaceBasketItem(item, new BasketItem(product, item.Quantity - 1));
        }
        else
        {
            this.items.Remove(item);
        }
    }

    public void Remove(Product product)
    {
        var item = this.GetBasketItemByProductOrNull(product);

        if (item == null)
        {
            return;
        }

        this.items.Remove(item);
    }

    public IEnumerable<BasketItem> GetItems()
    {
        return this.items.ToArray();
    }

    public BasketItem GetBasketItemByProduct(Product product)
    {
        return this.GetBasketItemByProductOrNull(product)!;
    }

    public BasketItem? GetBasketItemByProductOrNull(Product product)
    {
        return this.items.SingleOrDefault(i => i.Product == product);
    }

    private void ReplaceBasketItem(BasketItem oldItem, BasketItem newItem)
    {
        var index = this.items.IndexOf(oldItem);
        this.items[index] = newItem;
    }
}