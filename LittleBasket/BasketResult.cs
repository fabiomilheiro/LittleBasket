using LittleBasket.BasketItemDiscountRules;

namespace LittleBasket;

public record BasketResult(IEnumerable<BasketResultItem> Items);