namespace LittleBasket.BasketItemDiscountRules;

public interface IBasketItemDiscountRule
{
    BasketResultItem Apply(Basket basket, BasketItem basketItem);
}