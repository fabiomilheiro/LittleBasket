using System.Linq;
using FluentAssertions;
using LittleBasket.BasketItemDiscountRules;
using Xunit;

namespace LittleBasket.Tests.BasketItemDiscountRules;

public class FourthProductFreeDiscountBasketRuleTests
{
    private readonly Basket basket;
    private readonly FourthProductFreeDiscountBasketRule sut;

    public FourthProductFreeDiscountBasketRuleTests()
    {
        this.basket = new Basket();
        this.sut = new FourthProductFreeDiscountBasketRule(Products.Milk);
    }

    [Fact]
    public void Apply_IsNotMilk_ReturnsNull()
    {
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Butter);
        var nonMilkBasketItem = this.basket.GetItems().Last();

        var result = this.sut.Apply(this.basket, nonMilkBasketItem);

        result.Should().BeNull();
    }
    
    [Fact]
    public void Apply_LessThanFourMilks_ReturnsNull()
    {
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        var milkBasketItem = this.basket.GetItems().Last();

        var result = this.sut.Apply(this.basket, milkBasketItem);

        result.Should().BeNull();
    }

    [Fact]
    public void Apply_FourMilks_ReturnsOneMilkFree()
    {
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        var milkBasketItem = this.basket.GetItems().Last();

        var result = this.sut.Apply(this.basket, milkBasketItem);

        result.Should().BeEquivalentTo(
            new BasketResultItem(
                milkBasketItem.Product,
                4,
                milkBasketItem.Product.Price * 3));
    }

    [Fact]
    public void Apply_EightMilks_ReturnsTwoMilksFree()
    {
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        var milkBasketItem = this.basket.GetItems().Last();

        var result = this.sut.Apply(this.basket, milkBasketItem);

        result.Should().BeEquivalentTo(
            new BasketResultItem(
                milkBasketItem.Product,
                8,
                milkBasketItem.Product.Price * 6));
    }
}