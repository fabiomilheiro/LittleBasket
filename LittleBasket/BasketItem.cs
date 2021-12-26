namespace LittleBasket;

public class BasketItem
{
    public BasketItem(Product product, int quantity)
    {
        this.Product = product;
        this.Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; set; }
}