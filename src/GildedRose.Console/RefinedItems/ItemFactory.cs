namespace GildedRose.Console.RefinedItems
{
    public class ItemFactory
    {
        public static NormalItem GetItem(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    return new AgedBrieItem(item);

                case "Backstage passes to a TAFKAL80ETC concert":
                    return new BackstagePassItem(item);

                case "Sulfuras, Hand of Ragnaros":
                    return new LegendaryItem(item);

                case "Conjured Mana Cake":
                    return new ConjuredItem(item);

                default:
                    return new NormalItem(item);
            }
        }
    }
}
