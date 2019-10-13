using Domain.Aggregates.Expenses;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace Domain.UnitTests.ValueObjects
{
    public class MoneyTests
    {
        public static IEnumerable<object[]> AmountData = new List<object[]>
        {
            new object[] { decimal.Zero, 100.0m },
            new object[] { 123456.0m, 100.0m },
            new object[] { 256.9123123m, 256.911231231241234973m },
            new object[] { 100000000000000000.0m, 1000000000000.0m },
            new object[] { decimal.Zero, decimal.Zero }
        };

        public static IEnumerable<object[]> CurrencyData = new List<object[]>
        {
            new object[] { Currency.Eur, Currency.Rsd },
            new object[] { Currency.Rsd, Currency.Eur },
            new object[] { Currency.Eur, Currency.Eur },
            new object[] { Currency.Rsd, Currency.Rsd }
        };

        [Fact]
        public void MoneyShouldHaveCorrectAmountAndCurrency()
        {
            var money = new Money(111.1m, Currency.Eur);

            money.Amount.ShouldBe(111.1m);

            money.Currency.ShouldBe(Currency.Eur);
        }

        [Fact]
        public void MoneysShouldBeEqualIfAmountAndCurrencyAreEqual()
        {
            var money1 = new Money(111.1m, Currency.Eur);
            var money2 = new Money(111.1m, Currency.Eur);

            money1.ShouldBe(money2);
        }

        [Fact]
        public void MoneysShouldNotBeEqualIfAmountsAreNotEqual()
        {
            var money1 = new Money(111.1m, Currency.Eur);
            var money2 = new Money(222.2m, Currency.Eur);

            money1.ShouldNotBe(money2);
        }

        [Fact]
        public void MoneysShouldNotBeEqualIfCurrenciesAreNotEqual()
        {
            var money1 = new Money(111.1m, Currency.Eur);
            var money2 = new Money(111.1m, Currency.Rsd);

            money1.ShouldNotBe(money2);
        }

        [Theory]
        [MemberData(nameof(AmountData))]
        public void MoneyShouldChangeAmount(decimal initialAmount, decimal expectedAmount)
        {
            var money = new Money(initialAmount, Currency.Eur);

            money = money.ChangeAmount(expectedAmount);

            money.Amount.ShouldBe(expectedAmount);
        }

        [Theory]
        [MemberData(nameof(CurrencyData))]
        public void MoneyShouldChangeCurrency(Currency initialCurrency, Currency expectedCurrency)
        {
            var money = new Money(100.0m, initialCurrency);

            money = money.ChangeCurrency(expectedCurrency);

            money.Currency.ShouldBe(expectedCurrency);
        }
    }
}
