namespace GildedRose.Console.RefinedItems
{
    public class AgedBrieItem : NormalItem
    {
        // aged brie increases in quality
        protected override int QualityDelta => 1;

        public AgedBrieItem(Item item) : base(item)
        {
        }
    }
}
