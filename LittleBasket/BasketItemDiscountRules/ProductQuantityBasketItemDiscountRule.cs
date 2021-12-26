namespace LittleBasket.BasketItemDiscountRules;

public class ProductQuantityBasketItemDiscountRule : IBasketItemDiscountRule
{
    private readonly Product productToTrigger;
    private readonly int quantity;
    private readonly Product productToDiscount;
    private readonly decimal discountPercentage;

    public ProductQuantityBasketItemDiscountRule(
        Product productToTrigger,
        int quantity,
        Product productToDiscount,
        decimal discountPercentage)
    {
        this.productToTrigger = productToTrigger;
        this.quantity = quantity;
        this.productToDiscount = productToDiscount;
        this.discountPercentage = discountPercentage;
    }

    public BasketResultItem? Apply(Basket basket, BasketItem basketItem)
    {
        var productToTriggerBasketItem = basket.GetBasketItemByProductOrNull(this.productToTrigger);

        if (productToTriggerBasketItem == null)
        {
            return null;
        }

        var productToDiscountBasketItem = basket.GetBasketItemByProductOrNull(this.productToDiscount);

        if (productToDiscountBasketItem?.Product != basketItem.Product)
        {
            return null;
        }

        if (productToTriggerBasketItem.Quantity < this.quantity)
        {
            return null;
        }

        return new BasketResultItem(
            this.productToDiscount,
            basketItem.Quantity,
            this.CalculateFinalPrice(productToTriggerBasketItem, productToDiscountBasketItem));
    }

    private decimal CalculateFinalPrice(BasketItem productToTriggerBasketItem, BasketItem productToDiscountBasketItem)
    {
        var numberOfUnitsToDiscount = productToTriggerBasketItem.Quantity / this.quantity;
        var numberOfUnitsNotToDiscount = productToDiscountBasketItem.Quantity - numberOfUnitsToDiscount;
        
        var finalPrice =
            numberOfUnitsToDiscount * this.productToDiscount.Price * (1 - this.discountPercentage)
            + numberOfUnitsNotToDiscount * this.productToDiscount.Price;
       
        return finalPrice;
    }
}