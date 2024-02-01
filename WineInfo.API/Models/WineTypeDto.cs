using System.ComponentModel;

namespace WineInfo.API.Models
{
    public enum WineTypeDto
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
