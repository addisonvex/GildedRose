namespace GildedRose.Console.RefinedItems
{
    public class NormalItem
    {
        protected Item ItemRef;

        // lowers value for every item
        protected virtual int QualityDelta => -1;

        // once the sell by date has passed quality degrades twice as fast
        protected virtual int QualityMultiplier => ItemRef.SellIn < 0 ? 2 : 1;

        public NormalItem(Item item)
        {
            ItemRef = item;
        }

        public virtual void Update()
        {
            ItemRef.SellIn--;
            ItemRef.Quality += QualityDelta * QualityMultiplier;

            // the quality of an item is never negative
            if (ItemRef.Quality < 0)
                ItemRef.Quality = 0;

            // the Quality of an item is never more than 50
            if (ItemRef.Quality > 50)
                ItemRef.Quality = 50;
        }
    }
}
