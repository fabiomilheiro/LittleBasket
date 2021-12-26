# Basket coding exercise

## Relevant parts
- `Basket`: domain class with a collection of items which are (product, quantity) pairs and simple API to manage state. 
- `IBasketItemDiscountRule`: rule to apply on basket items. We can have multiple implementations.
- `BasketResultOrchestrator`: takes the original basket inputs and calculates the final basket as per the rules specified.

## Decisions

1. I chose to have basket data flowing one way (Basket ➡ BasketResult) because it simplifies the problem e.g. in the 2 x butter resulting in a 50% bread discount, we don't have to undo anything - just re-calculating the basket result.
2. I purposefully called the the discount rules a mouthful because those rules are applied specifically to basket items which does not fit all types of basket rules possible we can apply on a basket e.g. adding free products and coupons to give discounts on the final price.
3. The orchestrator can apply these rules and potentially others.
4. I kept the BasketResult with only a single property (Total) because nothing else was asked.
5. There is some overlap between the individual rules unit tests and the orchestrator tests. The former also covers execution paths in which the discount is not applied. The latter focuses on bringing it all together as per the project description.
6. At the end, I ended up concluding that the `NthProduct` rule ends up being redudant as we can achieve the same result with the `ProductQuantity` rule. Please see [explanation inline in the orchestrator tests](https://github.com/fabiomilheiro/LittleBasket/blob/main/LittleBasket.Tests/BasketResultOrchestratorTests.cs#L25).

Hope all is clear and look forward to talking to you.