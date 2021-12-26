using LittleBasket.BasketItemDiscountRules;

namespace LittleBasket;

public class BasketResultOrchestrator
{
    public BasketResult Execute(Basket basket, IEnumerable<IBasketItemDiscountRule> basketItemDiscountRules)
    {
        //var itemResults = new List<BasketResultItem>();
        
        //foreach (var basketItem in basket.GetItems())
        //{
        //    foreach (var basketItemDiscountRule in basketItemDiscountRules)
        //    {
        //        var itemResult = basketItemDiscountRule.Apply(basket, basketItem);

        //        if (itemResult != null)
        //        {
        //            itemResults.Add(itemResult);
        //        }
        //    }
        //}

        //return new BasketResult(itemResults);
        throw new NotImplementedException();
    }
}