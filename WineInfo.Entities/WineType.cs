using System.ComponentModel;

namespace WineInfo.Entities
{
    public enum WineType
    {
        [Description("Tinto")]
        Tinto = 1,
        [Description("Merlot")]
        Merlot = 2,
        [Description("Cabernet Sauvignon")]
        CabernetSauvignon = 3,
        [Description("Chardonnay")]
        Chardonnay = 4
    }
}
