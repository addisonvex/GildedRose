using GildedRose.Console;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        // input lists composed by identifying the numerical constants
        // from the specification and extended to test the edge cases
        private readonly List<int> _sellInEdgeCaseList = new List<int> {-1, 0, 1, 4, 5, 6, 9, 10, 11};
        private readonly List<int> _qualityEdgeCaseList = new List<int> { -1, 0, 1, 47, 48, 49, 50 };

        private void AssertThatSellInDegradesNormally()
        {
            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList)
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().SellIn, sellIn - 1);
                }
            }
        }

        [Fact]
        public void TestAgedBrieSellInDegradesNormally()
        {
            Program.Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };
            AssertThatSellInDegradesNormally();
        }

        [Fact]
        public void TestAgedBrieQualityImprovesNormallyBeforeSellIn()
        {
            Program.Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => s > 0))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => q < 50))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality + 1);
                }
            }
        }

        [Fact]
        public void TestAgedBrieQualityLimitedto50()
        {
            Program.Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => q > 48))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, 50);
                }
            }
        }

        [Fact]
        public void TestAgedBrieQualityImprovesTwiceAsFastAfterSellIn()
        {
            Program.Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => (s <= 0)))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => q < 49))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality + 2);
                }
            }
        }

        [Fact]
        public void TestBackstagePassSellInDegradesNormally()
        {
            Program.Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };
            AssertThatSellInDegradesNormally();
        }

        [Fact]
        public void TestBackstagePassQualityImprovesNormallyBefore10Days()
        {
            Program.Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => (s > 10)))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q < 50)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality + 1);
                }
            }
        }

        [Fact]
        public void TestBackstagePassQualityImprovesTwiceAsFastBefore5Days()
        {
            Program.Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => (s > 5 && s <= 10)))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q < 49)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality + 2);
                }
            }
        }

        [Fact]
        public void TestBackstagePassQualityImprovesThriceAsFastWithin5Days()
        {
            Program.Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => (s > 0 && s <= 5)))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q < 48)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality + 3);
                }
            }
        }

        [Fact]
        public void TestBackstagePassQualityIsZeroAfterConcert()
        {
            Program.Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => (s <= 0)))
            {
                foreach (var quality in _qualityEdgeCaseList)
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, 0);
                }
            }
        }

        [Fact]
        public void TestConjuredSellinDegradesNormally()
        {
            Program.Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };
            AssertThatSellInDegradesNormally();
        }

        [Fact]
        public void TestConjuredQualityDegradesTwiceAsFastBeforeSellin()
        {
            Program.Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => s > 0))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q > 1)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.True(Program.Items.First().Quality == quality - 2, "Are you using the old UpdateQuality method?");
                }
            }
        }

        [Fact]
        public void TestConjuredQualityDegradesFourTimesAsFastAfterSellin()
        {
            Program.Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => s <= 0))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q > 1)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.True(Program.Items.First().Quality == quality - 4, "Are you using the old UpdateQuality method?");
                }
            }
        }

        [Fact]
        public void TestConjuredQualityCannotBeNegative()
        {
            Program.Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 } };

            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q >= 0)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.True(Program.Items.First().Quality >= 0);
                }
            }
        }

        [Fact]
        public void TestLegendarySellInDoesntChange()
        {
            Program.Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };

            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList)
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().SellIn, sellIn);
                }
            }
        }

        [Fact]
        public void TestLegendaryQualityDoesntChange()
        {
            Program.Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } };

            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList)
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality);
                }
            }
        }

        [Fact]
        public void TestNormalSellInDegradesNormally()
        {
            Program.Items = new List<Item> { new Item { Name = "Some item", SellIn = 0, Quality = 0 } };
            AssertThatSellInDegradesNormally();
        }

        [Fact]
        public void TestNormalQualityDegradesNormallyBeforeSellin()
        {
            Program.Items = new List<Item> { new Item { Name = "Some item", SellIn = 0, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => s > 0))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => q > 0))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality - 1);
                }
            }
        }

        [Fact]
        public void TestNormalQualityDegradesTwiceAsFastAfterSellin()
        {
            Program.Items = new List<Item> { new Item { Name = "Some item", SellIn = 0, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList.Where(s => s <= 0))
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => q > 1))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.Equal(Program.Items.First().Quality, quality - 2);
                }
            }
        }

        [Fact]
        public void TestNormalQualityCannotBeNegative()
        {
            Program.Items = new List<Item> { new Item { Name = "Some item", SellIn = 0, Quality = 0 } };

            foreach (var sellIn in _sellInEdgeCaseList)
            {
                foreach (var quality in _qualityEdgeCaseList.Where(q => (q >= 0)))
                {
                    Program.Items.First().SellIn = sellIn;
                    Program.Items.First().Quality = quality;
                    Program.UpdateQuality();
                    Assert.True(Program.Items.First().Quality >= 0);
                }
            }
        }
    }
}
