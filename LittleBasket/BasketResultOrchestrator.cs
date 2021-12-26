using LittleBasket.BasketItemDiscountRules;

namespace LittleBasket;

public class BasketResultOrchestrator
{
    public BasketResult Execute(Basket basket, IEnumerable<IBasketItemDiscountRule> basketItemDiscountRules)
    {
        //foreach (var basketItem in basket.GetItems())
        //{
              //var itemResults = new List<BasketResultItem>();
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