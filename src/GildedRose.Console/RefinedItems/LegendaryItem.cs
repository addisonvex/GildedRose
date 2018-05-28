
namespace GildedRose.Console.RefinedItems
{
    public class LegendaryItem : NormalItem
    {
        public LegendaryItem(Item item) : base(item)
        {
        }

        // legendary items never decrease sellIn or quality so don't update
        public override void Update()
        {
        }
    }
}
