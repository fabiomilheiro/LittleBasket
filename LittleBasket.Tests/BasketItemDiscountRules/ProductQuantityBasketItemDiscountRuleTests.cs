using System.Linq;
using FluentAssertions;
using LittleBasket.BasketItemDiscountRules;
using Xunit;

namespace LittleBasket.Tests.BasketItemDiscountRules;

public class ProductQuantityDiscountBasketRuleTests
{
    private readonly Basket basket;

    public ProductQuantityDiscountBasketRuleTests()
    {
        this.basket = new Basket();
    }

    [Fact]
    public void Apply_ProductToTriggerNotFound_ReturnsNull()
    {
        this.basket.Add(Products.Bread);
        var basketItemToDiscount = this.basket.GetItems().Last();

        var sut = new ProductQuantityDiscountBasketRule(
            new Product("A", "Product not in basket", 1),
            2,
            basketItemToDiscount.Product,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeNull();
    }

    [Fact]
    public void Apply_ProductToDiscountDoesNotMatchBasketItem_ReturnsNull()
    {
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Milk);
        var basketItemToDiscount = this.basket.GetBasketItemByProduct(Products.Bread);

        var sut = new ProductQuantityDiscountBasketRule(
            Products.Bread,
            2,
            Products.Milk,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeNull();
    }

    [Fact]
    public void Apply_ProductQuantityLessThanRequired_ReturnsNull()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        var basketItemToDiscount = this.basket.GetItems().Last();

        var sut = new ProductQuantityDiscountBasketRule(
            Products.Butter,
            2,
            basketItemToDiscount.Product,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeNull();
    }

    [Fact]
    public void Apply_RequirementsMetForSingleQuantity_AppliesDiscount()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        var basketItemToDiscount = this.basket.GetItems().Last();
        var productToDiscount = basketItemToDiscount.Product;

        var sut = new ProductQuantityDiscountBasketRule(
            Products.Butter,
            2,
            productToDiscount,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeEquivalentTo(
            new BasketResultItem(
                productToDiscount,
                basketItemToDiscount.Quantity,
                productToDiscount.Price * .9m));
    }

    [Fact]
    public void Apply_RequirementsMetForOnlyOneUnit_AppliesDiscountOnce()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Bread);
        var basketItemToDiscount = this.basket.GetItems().Last();
        var productToDiscount = basketItemToDiscount.Product;

        var sut = new ProductQuantityDiscountBasketRule(
            Products.Butter,
            2,
            productToDiscount,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeEquivalentTo(
            new BasketResultItem(
                productToDiscount,
                basketItemToDiscount.Quantity,
                productToDiscount.Price * .9m
                + productToDiscount.Price));
    }

    [Fact]
    public void Apply_RequirementsMetTwice_AppliesDiscountTwice()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Bread);
        var basketItemToDiscount = this.basket.GetItems().Last();
        var productToDiscount = basketItemToDiscount.Product;

        var sut = new ProductQuantityDiscountBasketRule(
            Products.Butter,
            2,
            productToDiscount,
            .1m);

        var result = sut.Apply(this.basket, basketItemToDiscount);

        result.Should().BeEquivalentTo(
            new BasketResultItem(
                productToDiscount,
                basketItemToDiscount.Quantity,
                productToDiscount.Price * 2 * .9m));
    }
}