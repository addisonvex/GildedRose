namespace GildedRose.Console.RefinedItems
{
    public class BackstagePassItem : NormalItem
    {
        // increase by 3 when there are 5 days or less, 2 when 10 days, or 1 otherwise
        protected override int QualityDelta => ItemRef.SellIn < 5 ? 3 : ItemRef.SellIn < 10 ? 2 : 1;

        public BackstagePassItem(Item item) : base(item)
        {
        }

        public override void Update()
        {
            base.Update();

            // quality drops to 0 after the concert
            if (ItemRef.SellIn < 0)
                ItemRef.Quality = 0;
        }
    }
}
