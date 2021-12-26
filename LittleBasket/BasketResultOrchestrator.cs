using LittleBasket.BasketItemDiscountRules;

namespace LittleBasket;

public class BasketResultOrchestrator
{
    public BasketResult Execute(Basket basket, IEnumerable<IBasketItemDiscountRule> basketItemDiscountRules)
    {
        var basketResultItems = GetBasketResultItems(basket, basketItemDiscountRules);

        return new BasketResult(basketResultItems.Sum(i => i.FinalPrice));
    }

    private static IEnumerable<BasketResultItem> GetBasketResultItems(
        Basket basket,
        IEnumerable<IBasketItemDiscountRule> basketItemDiscountRules)
    {
        var basketItemDiscountRulesArray = basketItemDiscountRules.ToArray();

        foreach (var basketItem in basket.GetItems())
        {
            var basketResultItems =
                GetBasketItemPossibleDiscountResults(basket, basketItemDiscountRulesArray, basketItem)
                    .ToArray();

            if (basketResultItems.Any())
            {
                yield return GetCheapestBasketDiscountResult(basketResultItems);
            }
            else
            {
                yield return CreateBasketResultItemWithoutDiscount(basketItem);
            }
        }
    }

    private static BasketResultItem CreateBasketResultItemWithoutDiscount(BasketItem basketItem)
    {
        return new BasketResultItem(
            basketItem.Product,
            basketItem.Quantity,
            basketItem.Product.Price * basketItem.Quantity);
    }

    private static BasketResultItem GetCheapestBasketDiscountResult(BasketResultItem[] basketResultItems)
    {
        return basketResultItems.OrderByDescending(i => i.FinalPrice).First();
    }

    private static IEnumerable<BasketResultItem> GetBasketItemPossibleDiscountResults(
        Basket basket,
        IBasketItemDiscountRule[] basketItemDiscountRulesArray,
        BasketItem basketItem)
    {
        foreach (var basketItemDiscountRule in basketItemDiscountRulesArray)
        {
            var basketResultItem = basketItemDiscountRule.Apply(basket, basketItem);

            if (basketResultItem != null)
            {
                yield return basketResultItem;
            }
        }
    }
}