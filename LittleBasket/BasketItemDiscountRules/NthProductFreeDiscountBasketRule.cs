namespace LittleBasket.BasketItemDiscountRules;

public class NthProductFreeDiscountBasketRule : IBasketItemDiscountRule
{
    private readonly Product product;
    private readonly int quantity;

    public NthProductFreeDiscountBasketRule(Product product, int quantity)
    {
        this.product = product;
        this.quantity = quantity;
    }

    public BasketResultItem? Apply(Basket basket, BasketItem basketItem)
    {
        if (basketItem.Product != this.product)
        {
            return null;
        }

        if (basketItem.Quantity < this.quantity)
        {
            return null;
        }

        return new BasketResultItem(
            basketItem.Product,
            basketItem.Quantity,
            this.CalculateFinalPrice(basketItem));
    }

    private decimal CalculateFinalPrice(BasketItem basketItem)
    {
        var numberOfUnitsToDiscount = basketItem.Quantity / this.quantity;
        var numberOfUnitsNotToDiscount = basketItem.Quantity - numberOfUnitsToDiscount;

        return basketItem.Product.Price * numberOfUnitsNotToDiscount;
    }
}