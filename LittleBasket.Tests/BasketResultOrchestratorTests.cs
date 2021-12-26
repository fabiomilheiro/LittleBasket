using System;
using System.Collections.Generic;
using FluentAssertions;
using LittleBasket.BasketItemDiscountRules;
using Xunit;

namespace LittleBasket.Tests;

public class BasketResultOrchestratorTests
{
    private readonly Basket basket;
    private readonly List<IBasketItemDiscountRule> basketItemDiscountRules;
    private readonly BasketResultOrchestrator sut;

    public BasketResultOrchestratorTests()
    {
        this.basket = new Basket();
        this.basketItemDiscountRules = new List<IBasketItemDiscountRule>
        {
            new ProductQuantityDiscountBasketRule(
                Products.Butter,
                2,
                Products.Bread,
                .5m),
            new NthProductFreeDiscountBasketRule(Products.Milk, 4)
        };
        this.sut = new BasketResultOrchestrator();
    }

    [Fact]
    public void Calculate_NoRules_ReturnsTotalWithoutDiscount()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        var result = this.sut.Execute(this.basket, new List<IBasketItemDiscountRule>());

        result.Total.Should().Be(Products.Butter.Price * 2 + Products.Bread.Price);
    }

    [Fact]
    public void Calculate_1BreadButterAndMilk_ReturnsWithoutDiscount()
    {
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Milk);
        var result = this.sut.Execute(this.basket, this.basketItemDiscountRules);

        result.Total.Should().Be(Products.Bread.Price + Products.Butter.Price + Products.Milk.Price);
    }

    [Fact]
    public void Calculate_TwoButtersAndTwoBreads_ReturnsWithOneBread50Percent()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Bread);

        var result = this.sut.Execute(this.basket, this.basketItemDiscountRules);

        result.Total.Should().Be(
            Products.Butter.Price * 2
            + Products.Bread.Price
            + Products.Bread.Price * .5m);
    }

    [Fact]
    public void Calculate_FourMilks_ReturnsWithOneMilkFree()
    {
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);

        var result = this.sut.Execute(this.basket, this.basketItemDiscountRules);

        result.Total.Should().Be(Products.Milk.Price * 3);
    }

    [Fact]
    public void Calculate_TwoButtersOneBreadsEightMilks_ReturnsOneBread50PercentAndTwoFreeMilks()
    {
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Butter);
        this.basket.Add(Products.Bread);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);
        this.basket.Add(Products.Milk);

        var result = this.sut.Execute(this.basket, this.basketItemDiscountRules);

        result.Total.Should().Be(
            Products.Butter.Price * 2
            + Products.Bread.Price * .5m
            + Products.Milk.Price * 6);
    }
}