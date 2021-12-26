namespace LittleBasket.BasketItemDiscountRules;

public class ProductQuantityDiscountBasketRule : IBasketItemDiscountRule
{
    public ProductQuantityDiscountBasketRule(
        Product productToTrigger,
        int quantity,
        Product productToDiscount,
        decimal discount)
    {
        this.ProductToTrigger = productToTrigger;
        this.Quantity = quantity;
        this.ProductToDiscount = productToDiscount;
        this.Discount = discount;
    }

    public Product ProductToTrigger { get; }
    public int Quantity { get; }
    public Product ProductToDiscount { get; }
    public decimal Discount { get; }

    public BasketResultItem? Apply(Basket basket, BasketItem basketItem)
    {
        throw new NotImplementedException();
    }
}