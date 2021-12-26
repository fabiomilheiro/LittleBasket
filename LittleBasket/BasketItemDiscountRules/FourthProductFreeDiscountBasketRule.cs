namespace LittleBasket.BasketItemDiscountRules;

public class FourthProductFreeDiscountBasketRule : IBasketItemDiscountRule
{
    private readonly Product product;

    public FourthProductFreeDiscountBasketRule(Product product)
    {
        this.product = product;
    }

    public BasketResultItem? Apply(Basket basket, BasketItem basketItem)
    {
        if (basketItem.Product != this.product)
        {
            return null;
        }

        if (basketItem.Quantity < 4)
        {
            return null;
        }

        return new BasketResultItem(
            basketItem.Product,
            basketItem.Quantity,
            CalculateFinalPrice(basketItem));
    }

    private static decimal CalculateFinalPrice(BasketItem basketItem)
    {
        var (product, quantity) = basketItem;
        var numberOfUnitsToDiscount = quantity / 4;
        var numberOfUnitsNotToDiscount = quantity - numberOfUnitsToDiscount;

        return product.Price * numberOfUnitsNotToDiscount;
    }
}