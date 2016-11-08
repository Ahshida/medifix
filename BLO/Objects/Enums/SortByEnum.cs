using System.ComponentModel;

namespace BLO.Objects.Enums
{
    public enum SortByEnum
    {
        [Description("Sort By Date")]
        SortByDate,
        [Description("Lowest Price First")]
        LowestPriceFirst,
        [Description("Highest Price First")]
        HighestPriceFirst
    }
}
