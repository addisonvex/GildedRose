namespace GildedRose.Console.RefinedItems
{
    public class ConjuredItem : NormalItem
    {
        // degrade twice as fast as normal items
        protected override int QualityMultiplier => ItemRef.SellIn < 0 ? 4 : 2;

        public ConjuredItem(Item item) : base(item)
        {
        }
    }
}
