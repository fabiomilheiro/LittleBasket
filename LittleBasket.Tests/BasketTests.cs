using FluentAssertions;
using Xunit;

namespace LittleBasket.Tests;

public class BasketTests
{
    private readonly Basket sut;

    public BasketTests()
    {
        this.sut = new Basket();
    }

    [Fact]
    public void Add_DoesNotExist_AddBasketItemWithOneUnit()
    {
        this.sut.Add(Products.Bread);

        var result = this.sut.GetItems();
        result.Should().BeEquivalentTo(
            new[]
            {
                new BasketItem(Products.Bread, 1)
            });
    }

    [Fact]
    public void Add_AlreadyExists_IncreaseProductQuantity()
    {
        this.sut.Add(Products.Bread);

        this.sut.Add(Products.Bread);

        var result = this.sut.GetItems();
        result.Should().BeEquivalentTo(
            new[]
            {
                new BasketItem(Products.Bread, 2)
            });
    }

    [Fact]
    public void Subtract_NotInBasket_DoesNothing()
    {
        this.sut.Add(Products.Bread);
        this.sut.Add(Products.Bread);

        this.sut.Subtract(Products.Milk);

        var result = this.sut.GetItems();
        result.Should().BeEquivalentTo(
            new[]
            {
                new BasketItem(Products.Bread, 2)
            });
    }

    [Fact]
    public void Subtract_ExistsInBasketWithOneUnit_Removes()
    {
        this.sut.Add(Products.Milk);

        this.sut.Subtract(Products.Milk);

        var result = this.sut.GetItems();
        result.Should().BeEmpty();
    }

    [Fact]
    public void Subtract_ExistsInBasketWithMoreThanOneUnit_SubtractsQuantity()
    {
        this.sut.Add(Products.Milk);
        this.sut.Add(Products.Milk);

        this.sut.Subtract(Products.Milk);

        var result = this.sut.GetItems();
        result.Should().BeEquivalentTo(
            new[]
            {
                new BasketItem(Products.Milk, 1)
            });
    }

    [Fact]
    public void Remove_NotInBasket_DoesNothing()
    {
        this.sut.Add(Products.Bread);
        this.sut.Add(Products.Bread);

        this.sut.Remove(Products.Milk);

        var result = this.sut.GetItems();
        result.Should().BeEquivalentTo(
            new[]
            {
                new BasketItem(Products.Bread, 2)
            });
    }

    [Fact]
    public void Remove_ExistsInBasket_Removes()
    {
        this.sut.Add(Products.Milk);
        this.sut.Add(Products.Milk);
        this.sut.Add(Products.Milk);

        this.sut.Remove(Products.Milk);

        var result = this.sut.GetItems();
        result.Should().BeEmpty();
    }
}